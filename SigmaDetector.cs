using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.Threading;
using System.Management;
using System.Text.RegularExpressions;


namespace BikeDB2024
{
    public class SigmaDetector
    {
        private const int TIMEOUT = 3000;

        /// <summary>
        /// Versucht asynchron einen Sigma-Tacho zu erkennen
        /// </summary>
        public async Task<BikeComputerInfo> TryDetectAsync(
            CancellationToken token)
        {
            string port = FindSigmaComPort();
            if (string.IsNullOrEmpty(port))
                return null;

            return await TryDetectOnPortAsync(port, token);
        }

        /// <summary>
        /// Reconnect-Variante: probiert mehrfach
        /// </summary>
        public async Task<BikeComputerInfo> TryDetectWithReconnectAsync(
            int retries,
            TimeSpan delay,
            CancellationToken token)
        {
            for (int i = 0; i < retries; i++)
            {
                token.ThrowIfCancellationRequested();

                var info = await TryDetectAsync(token);
                if (info != null)
                    return info;

                await Task.Delay(delay, token);
            }

            return null;
        }

        private async Task<BikeComputerInfo> TryDetectOnPortAsync(
            string portName,
            CancellationToken token)
        {
            return await Task.Run(() =>
            {
                try
                {
                    using (var port = new SerialPort(portName, 9600, Parity.None, 8, StopBits.One))
                    {
                        port.ReadTimeout = TIMEOUT;
                        port.WriteTimeout = TIMEOUT;
                        port.Open();

                        if (!IsUnitPresent(port))
                            return null;

                        port.DiscardInBuffer();
                        port.Write([0xfe], 0, 1);
                        Thread.Sleep(500);

                        port.Write([0xfb], 0, 1);
                        Thread.Sleep(1500);

                        byte[] buf = new byte[11];
                        int read = ReadBytes(port, buf, 11);

                        if (read != 11)
                            return null;

                        var info = new BikeComputerInfo
                        {
                            Type = buf[0],
                            Version = buf[6],
                            SerialNumber = $"{buf[2]}{buf[3]}{buf[4]}{buf[5]}"
                        };

                        info.Model = buf[1] switch
                        {
                            0x15 => BikeComputerModel.BC1612,
                            0x12 => BikeComputerModel.BC1212,
                            0x16 => BikeComputerModel.BC1612_STS,
                            _ => BikeComputerModel.Unknown
                        };

                        info.ModelName = info.Model switch
                        {
                            BikeComputerModel.BC1612 => "BC 16.12",
                            BikeComputerModel.BC1212 => "BC 12.12",
                            BikeComputerModel.BC1612_STS => "BC 16.12 STS",
                            _ => $"Unknown (0x{buf[1]:x2})"
                        };

                        return info;
                    }
                }
                catch
                {
                    return null;
                }
            }, token);
        }

        private bool IsUnitPresent(SerialPort port)
        {
            port.DiscardInBuffer();
            port.Write([0xf4], 0, 1);
            Thread.Sleep(150);

            return port.BytesToRead > 0 && port.ReadByte() == 1;
        }

        private int ReadBytes(SerialPort port, byte[] buffer, int expected)
        {
            int total = 0;

            for (int i = 0; i < 30 && total < expected; i++)
            {
                if (port.BytesToRead > 0)
                    total += port.Read(buffer, total, expected - total);
                else
                    Thread.Sleep(100);
            }

            return total;
        }

        // 🔽 dein bestehender Port-Finder
        private string FindSigmaComPort()
        {
            var searcher = new ManagementObjectSearcher(
                "SELECT * FROM Win32_PnPEntity WHERE Name LIKE '%(COM%'");

            foreach (ManagementObject device in searcher.Get())
            {
                string name = device["Name"]?.ToString() ?? "";

                if (name.IndexOf("SIGMA USB", StringComparison.OrdinalIgnoreCase) < 0)
                    continue;

                var match = Regex.Match(name, @"\(COM\d+\)");
                if (match.Success)
                    return match.Value.Trim('(', ')');
            }

            return null;
        }
    }
}
