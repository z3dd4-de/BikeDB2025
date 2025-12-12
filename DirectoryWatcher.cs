using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json;

namespace BikeDB2024
{
    public class RideData
    {
        public string TachoName { get; set; }
        public string BikeName { get; set; }
        public DateTime Timestamp { get; set; }
        public uint DistanceMeters { get; set; }
        public uint TimeSeconds { get; set; }
        public double MeanSpeedKmh { get; set; }
        public double MaxSpeedKmh { get; set; }
        public byte Cadence { get; set; }
        public uint TripSectionDistanceMeters { get; set; }
        public uint TripSectionTimeSeconds { get; set; }

        public double DistanceKm => DistanceMeters / 1000.0;
        public TimeSpan Duration => TimeSpan.FromSeconds(TimeSeconds);
        public TimeSpan TripSectionDuration => TimeSpan.FromSeconds(TripSectionTimeSeconds);
    }

    public class DirectoryWatcher
    {
        private FileSystemWatcher watcher;
        private bool newFileDetected = false;
        private string latestFileTimeStamp = string.Empty;
        static string filename = "bikedata.json";
        public bool IsWatcherActive = false;

        // Eigene Events definieren
        public event EventHandler<string> FileCreated;
        public event EventHandler<string> FileChanged;

        public DirectoryWatcher()
        {
            if (Properties.Settings.Default.SigmaDsEnabled && Properties.Settings.Default.SigmaDirectory != String.Empty)
            {
                InitializeWatcher(Properties.Settings.Default.SigmaDirectory);
                if (File.Exists(Path.Combine(Properties.Settings.Default.SigmaDirectory, filename)))
                {
                    CheckTimeStamp(Path.Combine(Properties.Settings.Default.SigmaDirectory, filename));
                }
            }           
        }

        private void InitializeWatcher(string sigmaDsPath)
        {
            watcher = new FileSystemWatcher();
            watcher.Path = sigmaDsPath;
            watcher.Filter = "*.json";
            watcher.IncludeSubdirectories = false;
            watcher.Created += OnFileCreated;
            watcher.Changed += OnFileChanged;
            watcher.Error += OnError;
            watcher.EnableRaisingEvents = true;
            IsWatcherActive = true;
        }

        private void CheckTimeStamp(string filePath)
        {
            RideData rideData = ParseRideData(filePath);
            if (rideData != null)
            {
                string fileTimeStamp = rideData.Timestamp.ToString("yyyyMMddHHmmss");
                if (fileTimeStamp != latestFileTimeStamp)
                {
                    latestFileTimeStamp = fileTimeStamp;
                    newFileDetected = true;
                    Console.WriteLine($"Neue Fahrt erkannt: {rideData.BikeName} am {rideData.Timestamp}, Distanz: {rideData.DistanceKm} km, Dauer: {rideData.Duration}");
                    Properties.Settings.Default.SigmaLastTimeStamp = latestFileTimeStamp;
                }
            }
        }

        private void OnFileChanged(object sender, FileSystemEventArgs e)
        {
            if (e.ChangeType != WatcherChangeTypes.Changed)
            {
                FileChanged.Invoke(this, e.FullPath);
            }
            Console.WriteLine($"Datei geändert: {e.FullPath} ({e.ChangeType})");
        }

        private void OnFileCreated(object sender, FileSystemEventArgs e)
        {
            FileCreated.Invoke(this, e.FullPath);
            Console.WriteLine($"Datei erstellt: {e.FullPath} ({e.ChangeType})");
        }

        private RideData ParseRideData(string filePath)
        {
            try
            {
                string jsonString = File.ReadAllText(filePath);
                RideData rideData = JsonSerializer.Deserialize<RideData>(jsonString);
                return rideData;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fehler beim Parsen der Datei {filePath}: {ex.Message}");
                return null;
            }
        }

        private static void OnError(object sender, ErrorEventArgs e) =>
            PrintException(e.GetException());

        private static void PrintException(Exception ex)
        {
            if (ex != null)
            {
                Console.WriteLine($"Message: {ex.Message}");
                Console.WriteLine("Stacktrace:");
                Console.WriteLine(ex.StackTrace);
                Console.WriteLine();
                PrintException(ex.InnerException);
            }
        }
    }
}
