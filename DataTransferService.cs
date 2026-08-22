using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BikeDB2024
{
    internal class DataTransferService
    {
        public async Task<ImportPreviewResult> AnalyzeImportAsync(
        string zipPath,
        bool isAdmin,
        int currentUserId,
        CancellationToken token)
        {
            string tempDir = ExtractZip(zipPath);

            var result = new ImportPreviewResult();
            var manifest = LoadManifest(tempDir);
            result.Manifest = manifest;

            // 🔴 Versions- / Rechteprüfung
            if (!isAdmin && manifest.IsAdminExport)
            {
                result.Tables.Add(new TablePreviewResult
                {
                    Table = "(global)",
                    Issues =
            {
                new ImportIssue
                {
                    Type = ImportIssueType.Error,
                    Message = "Admin-Export kann nicht von User importiert werden"
                }
            }
                });
                return result;
            }

            await using var con = new SqlConnection(_connectionString);
            await con.OpenAsync(token);

            foreach (string table in manifest.Tables)
            {
                var preview = await AnalyzeTableAsync(
                    con,
                    table,
                    Path.Combine(tempDir, table + ".csv"),
                    isAdmin,
                    currentUserId,
                    token);

                result.Tables.Add(preview);
            }

            return result;
        }

    private async Task<TablePreviewResult> AnalyzeTableAsync(
    SqlConnection con,
    string table,
    string csvPath,
    bool isAdmin,
    int userId,
    CancellationToken token)
        {
            var preview = new TablePreviewResult { Table = table };

            if (!File.Exists(csvPath))
            {
                preview.Issues.Add(new ImportIssue
                {
                    Type = ImportIssueType.Error,
                    Message = "CSV-Datei fehlt"
                });
                return preview;
            }

            var rows = ReadCsv(csvPath).ToList();
            preview.Rows = rows.Count - 1;

            if (rows.Count < 2)
                return preview;

            string[] headers = rows[0];

            // 🔴 Pflichtspalte Id
            if (!headers.Any(h => h.Equals("Id", StringComparison.OrdinalIgnoreCase)))
            {
                preview.Issues.Add(new ImportIssue
                {
                    Type = ImportIssueType.Error,
                    Message = "Pflichtspalte 'Id' fehlt"
                });
                return preview;
            }

            for (int i = 1; i < rows.Count; i++)
            {
                if (!int.TryParse(rows[i][0], out int id))
                {
                    preview.Issues.Add(new ImportIssue
                    {
                        Type = ImportIssueType.Warning,
                        Message = $"Zeile {i + 1}: Ungültige Id"
                    });
                    continue;
                }

                bool exists = await RecordExistsAsync(
                    con, null, table, id, token);

                if (exists) preview.Updates++;
                else preview.Inserts++;
            }

            return preview;
        }
    }

    public sealed class ImportPreviewResult
    {
        public ExportManifest Manifest { get; init; } = null!;
        public List<TablePreviewResult> Tables { get; } = new();

        public bool HasErrors =>
            Tables.Any(t => t.Issues.Any(i => i.Type == ImportIssueType.Error));
    }

    public sealed class ImportReport
    {
        public string Table { get; init; } = "";
        public int Inserts { get; set; }
        public int Updates { get; set; }
        public int Errors { get; set; }
    }

    public sealed class TablePreviewResult
    {
        public string Table { get; init; } = "";
        public int Rows { get; init; }

        public int Inserts { get; init; }
        public int Updates { get; init; }

        public List<ImportIssue> Issues { get; } = new();
    }

    public enum ImportIssueType
    {
        Error,
        Warning
    }

    public sealed class ImportIssue
    {
        public ImportIssueType Type { get; init; }
        public string Message { get; init; } = "";
    }

    public async Task<List<ImportReport>> ImportZipAsync(
    string zipPath,
    bool isAdmin,
    int currentUserId,
    ImportMode mode,
    IProgress<ExportProgress>? progress,
    CancellationToken token)
        {
            var reports = new List<ImportReport>();

            // manifest laden & validieren (wie vorher)

            foreach (string table in manifest.Tables)
            {
                var report = await ImportTableTransactionalAsync(
                    con,
                    table,
                    csvPath,
                    isAdmin,
                    currentUserId,
                    mode,
                    token);

                reports.Add(report);
            }

            return reports;
        }
    }
