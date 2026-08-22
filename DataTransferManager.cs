using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using ExcelLibrary.BinaryDrawingFormat;

namespace System.Runtime.CompilerServices
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    internal class IsExternalInit { }
}

namespace BikeDB2024
{
    public sealed class DataTransferManager
    {
        private readonly string _connectionString;

        public DataTransferManager(string connectionString)
        {
            _connectionString = connectionString;
        }

        // =========================
        // EXPORT
        // =========================
        public async Task ExportZipAsync(
            IEnumerable<TableExportDefinition> tables,
            string targetFolder,
            bool isAdmin,
            int currentUserId,
            IProgress<ExportProgress>? progress,
            CancellationToken token)
        {
            // -> CSV erzeugen
            // -> manifest.json schreiben
            // -> ZIP packen
            // (Code aus vorherigen Schritten hier zusammengeführt)
        }

        // =========================
        // PREVIEW (Dry-Run Analyse)
        // =========================
        public async Task<ImportPreviewResult> AnalyzeImportAsync(
            string zipPath,
            bool isAdmin,
            int currentUserId,
            CancellationToken token)
        {
            // ZIP entpacken
            // manifest laden & prüfen
            // Schema lesen
            // CSV analysieren
            // ImportPreviewResult zurückgeben
        }

        // =========================
        // IMPORT
        // =========================
        public async Task<List<ImportReport>> ImportZipAsync(
            string zipPath,
            bool isAdmin,
            int currentUserId,
            ImportMode mode,
            IProgress<ExportProgress>? progress,
            CancellationToken token)
        {
            // ZIP entpacken
            // manifest prüfen
            // pro Tabelle:
            //   - Preview optional
            //   - Transaktion
            //   - Insert / Upsert
            //   - Report sammeln
        }

        // =========================
        // HILFSMETHODEN
        // =========================
        // - LoadManifest
        // - LoadSchemaAsync
        // - ReadCsv
        // - RecordExistsAsync
        // - BuildInsert / Update
    }

    #region Models

    public enum ImportMode
    {
        DryRun,
        InsertOnly,
        Upsert
    }

    public record ExportProgress(string Table, int Current, int Total);



    public sealed class ExportManifest
    {
        public string App { get; init; } = "BikeDB2026";
        public DateTime ExportedAt { get; init; }
        public string Version { get; init; } = "";
        public bool IsAdminExport { get; init; }
        public List<string> Tables { get; init; } = new();
    }

    #endregion

    private async Task ImportTableAsync(
    SqlConnection con,
    SqlTransaction tx,
    string table,
    string csvPath,
    bool isAdmin,
    int userId,
    CancellationToken token)
        {
            var rows = ReadCsv(csvPath).ToList();
            if (rows.Count < 2) return;

            string[] headers = rows[0];

            for (int r = 1; r < rows.Count; r++)
            {
                var cmd = new SqlCommand
                {
                    Connection = con,
                    Transaction = tx
                };

                var cols = new List<string>();
                var pars = new List<string>();

                for (int c = 0; c < headers.Length; c++)
                {
                    string col = headers[c];

                    if (col.Equals("User", StringComparison.OrdinalIgnoreCase)
                        && !isAdmin)
                    {
                        cols.Add("[User]");
                        pars.Add("@user");
                        cmd.Parameters.AddWithValue("@user", userId);
                    }
                    else
                    {
                        cols.Add($"[{col}]");
                        pars.Add($"@p{c}");
                        cmd.Parameters.AddWithValue($"@p{c}", rows[r][c]);
                    }
                }

                cmd.CommandText =
                    $"INSERT INTO {table} ({string.Join(",", cols)}) " +
                    $"VALUES ({string.Join(",", pars)})";

                await cmd.ExecuteNonQueryAsync(token);
            }
        }

        private static ExportManifest LoadManifest(string folder)
        {
            string path = Path.Combine(folder, "manifest.json");
            if (!File.Exists(path))
                throw new InvalidOperationException("manifest.json fehlt");

            return JsonSerializer.Deserialize<ExportManifest>(
                File.ReadAllText(path))!;
        }

        private async Task ImportTableTransactionalAsync(
        SqlConnection con,
        string table,
        string csvPath,
        bool isAdmin,
        int userId,
        CancellationToken token)
        {
            await using var tx = con.BeginTransaction();

            try
            {
                await ImportTableAsync(
                    con,
                    tx,
                    table,
                    csvPath,
                    isAdmin,
                    userId,
                    token);

                await tx.CommitAsync(token);
            }
            catch
            {
                await tx.RollbackAsync(token);
                throw;
            }
        }
    }

    public sealed class ExportManifest
    {
        public string App { get; init; } = "BikeDB2024";
        public DateTime ExportedAt { get; init; }
        public string Version { get; init; } = "";
        public bool IsAdminExport { get; init; }
        public List<string> Tables { get; init; } = new();

    private async Task WriteManifestAsync(
    string folder,
    IEnumerable<TableExportDefinition> tables,
    bool isAdmin)
    {
        var manifest = new ExportManifest
        {
            ExportedAt = DateTime.Now,
            Version = BikeDB2024.Properties.Settings.Default.DBVersion,
            IsAdminExport = isAdmin,
            Tables = tables.Select(t => t.TableName).ToList()
        };

        string json = JsonSerializer.Serialize(
            manifest,
            new JsonSerializerOptions { WriteIndented = true });

        await File.WriteAllTextAsync(
            Path.Combine(folder, "manifest.json"),
            json,
            Encoding.UTF8);
    }
}



