using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows;//.Devices.Usb;
using System.Text.RegularExpressions;
using LibUsbDotNet;
using LibUsbDotNet.Main;
using LibUsbDotNet.Info;
using System.Collections.ObjectModel;
using LibUsbDotNet.DeviceNotify;
using System.IO.Ports;
using System.Threading;
using ExcelLibrary.BinaryFileFormat;
using System.CodeDom.Compiler;

namespace BikeDB2024
{
    public partial class SigmaDsForm_test : Form
    {
        public static UsbDevice MyUsbDevice;

        #region SET YOUR USB Vendor and Product ID!
        int vid = 0x1D9D;
        int pid = 0x1011;

        public static UsbDeviceFinder MyUsbFinder;
        public static IDeviceNotifier UsbDeviceNotifier = DeviceNotifier.OpenDeviceNotifier();
        #endregion
        static SerialPort _serialPort;
        internal delegate void SerialPinChangedEventHandlerDelegate(object sender, SerialPinChangedEventArgs e);
        private SerialPinChangedEventHandler SerialPinChangedEventHandler1;

        Thread readThread = new Thread(Read);
        static bool _continue;
        static string text;
        public SigmaDsForm_test()
        {
            InitializeComponent();
            SerialPinChangedEventHandler1 = new SerialPinChangedEventHandler(PinChanged);
            MyUsbFinder = new UsbDeviceFinder(vid, pid);
            // Hook the device notifier event
            UsbDeviceNotifier.OnDeviceNotify += OnDeviceNotifyEvent;
            initComboBoxes();

            // Create a new SerialPort object with default settings.
            _serialPort = new System.IO.Ports.SerialPort("COM4", 9600, System.IO.Ports.Parity.None, 8, System.IO.Ports.StopBits.One);
            conSettingToolStripStatusLabel.Text = "COM4: 9600 Bits/s, Parity.None, 8 Datenbits, 1 Stoppbit, 500/500 ms Timeouts Read/Write";
            _serialPort.PinChanged += SerialPinChangedEventHandler1;
            // Set the read/write timeouts
            _serialPort.ReadTimeout = 500;
            _serialPort.WriteTimeout = 500;
            _continue = false;
            text = "";
            //Handshake hs = (Handshake)Enum.Parse(typeof(Handshake), "RequestToSendXOnXOff", true);
            //_serialPort.Handshake = hs;
            _serialPort.DataReceived += new SerialDataReceivedEventHandler(sp_DataReceived);
            statusToolStripStatusLabel.Text = "Keine Verbindung";
            connectToolStripButton.Enabled = true;
            disconnectToolStripButton.Enabled = false;
            sendButton.Enabled = false;
            consoleRichTextBox.Text = String.Empty;
        }

        private delegate void SetTextDeleg(string _text);

        void sp_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            Thread.Sleep(500);
            string data = _serialPort.ReadLine();
            this.BeginInvoke(new SetTextDeleg(si_DataReceived), new object[] { data });
        }

        private void si_DataReceived(string data) { consoleRichTextBox.Text += data; }

        private void getSerialPorts()
        {
            string[] ArrayComPortsNames = null;
            int index = -1;
            string ComPortName = null;
            portsComboBox.Items.Clear();

            ArrayComPortsNames = SerialPort.GetPortNames();
            do
            {
                index += 1;
                portsComboBox.Items.Add(ArrayComPortsNames[index]);
            } while (!((ArrayComPortsNames[index] == ComPortName) || (index == ArrayComPortsNames.GetUpperBound(0))));
            portsComboBox.SelectedIndex = index;
        }

        private void initComboBoxes()
        {
            getSerialPorts();
            cboBaudRate.Items.Clear();
            cboBaudRate.Items.Add(300);
            cboBaudRate.Items.Add(600);
            cboBaudRate.Items.Add(1200);
            cboBaudRate.Items.Add(2400);
            cboBaudRate.Items.Add(9600);    //Default
            cboBaudRate.Items.Add(14400);
            cboBaudRate.Items.Add(19200);
            cboBaudRate.Items.Add(38400);
            cboBaudRate.Items.Add(57600);
            cboBaudRate.Items.Add(115200);
            cboBaudRate.Items.ToString();
            cboHandShaking.Items.Clear();
            cboHandShaking.Items.Add("None");
            cboHandShaking.Items.Add("XOnXOff");
            cboHandShaking.Items.Add("RequestToSend");
            cboHandShaking.Items.Add("RequestToSendXOnXOff");
            //set default items in combo boxes
            cboBaudRate.Text = cboBaudRate.Items[4].ToString();
            databitsComboBox.Text = databitsComboBox.Items[4].ToString();
            cboHandShaking.Text = cboHandShaking.Items[0].ToString();
            stoppComboBox.Text = stoppComboBox.Items[0].ToString();
            //(Parity)Enum.Parse(typeof(Parity), parityComboBox.Text);
            parityComboBox.Items.Add(Parity.None.ToString());
            parityComboBox.Items.Add(Parity.Odd.ToString());
            parityComboBox.Items.Add(Parity.Even.ToString());
            parityComboBox.Items.Add(Parity.Mark.ToString());
            parityComboBox.Items.Add(Parity.Space.ToString());
            parityComboBox.Text = parityComboBox.Items[0].ToString();
        }

        private void getSerialPort()
        {
            using (var sp = new System.IO.Ports.SerialPort("COM4", 9600, System.IO.Ports.Parity.None, 8, System.IO.Ports.StopBits.One))
            {
                sp.Open();

                sp.WriteLine("Hello!");

                var readData = sp.ReadLine();
                consoleRichTextBox.Text += readData;
                //Console.WriteLine(readData);
            }
        }

        public static void Read()
        {
            while (_continue)
            {
                try
                {
                    string message = _serialPort.ReadLine();
                    Console.WriteLine(message);
                    text += message + "\n";
                }
                catch (TimeoutException) { }
            }
        }

        private void SigmaDsForm_Shown(object sender, EventArgs e)
        {
            //getSerialPort();
        }

        private void OnDeviceNotifyEvent(object sender, DeviceNotifyEventArgs e)
        {
            consoleRichTextBox.Text += e.ToString() + "\n";
            //readDevice();
            //showInfo();
        }

        private void SigmaDsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Unhook the device notifier event
            UsbDeviceNotifier.OnDeviceNotify -= OnDeviceNotifyEvent;
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            /*_serialPort.Open();
            foreach (string s in Enum.GetNames(typeof(Handshake)))
            {
                Console.WriteLine("   {0}", s);
            }
            Handshake hs = (Handshake)Enum.Parse(typeof(Handshake), "RequestToSendXOnXOff", true);
            //_serialPort.Handshake = hs;
            _continue = true;
            readThread.Start();*/
            try
            {
                if (!_serialPort.IsOpen)
                    _serialPort.Open();
                _serialPort.Write("RequestToSend\r\n");
            }
            catch (Exception)
            {

            }
            

        }

        private void stopButton_Click(object sender, EventArgs e)
        {
            _continue = false;
            //readThread.Join();
            _serialPort.Close();
            consoleRichTextBox.Text += text;
        }

        private void closeToolStripButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void changePortButton_Click(object sender, EventArgs e)
        {
            if (_serialPort.IsOpen)
                _serialPort.Close();
            _serialPort = null;
            _serialPort = new SerialPort();

            //Handshake
            Handshake hs = (Handshake)Enum.Parse(typeof(Handshake), cboHandShaking.Text, true);
            _serialPort.Handshake = hs;

            _serialPort.BaudRate = Convert.ToInt32(cboBaudRate.Text);
            _serialPort.DataBits = Convert.ToInt16(databitsComboBox.Text);
            _serialPort.StopBits = (StopBits)Enum.Parse(typeof(StopBits), stoppComboBox.Text);
            //Parity
            _serialPort.Parity = (Parity)Enum.Parse(typeof(Parity), parityComboBox.Text);
            // Set the read/write timeouts
            _serialPort.ReadTimeout = Convert.ToInt32(readTimeoutTextBox.Text);
            _serialPort.WriteTimeout = Convert.ToInt32(writeTimeoutTextBox.Text);
            //COM-Port
            _serialPort.PortName = portsComboBox.Text;
            conSettingToolStripStatusLabel.Text = $"{portsComboBox.Text}: {cboBaudRate.Text} Bits/s, Parity.{parityComboBox.Text}, " +
                $"{databitsComboBox.Text} Datenbits, {stoppComboBox.Text} Stoppbit(s), {readTimeoutTextBox.Text}/{writeTimeoutTextBox.Text} ms Timeouts Read/Write";
        }

        private void testButton_Click(object sender, EventArgs e)
        {
            SerialPinChangedEventHandler1 = new SerialPinChangedEventHandler(PinChanged);
            _serialPort.PinChanged += SerialPinChangedEventHandler1;
            _serialPort.Open();
            _serialPort.RtsEnable = true;
            _serialPort.DtrEnable = true;
            testButton.Enabled = false;
        }

        internal void PinChanged(object sender, SerialPinChangedEventArgs e)
        {
            SerialPinChange SerialPinChange1 = 0;
            bool signalState = false;
            SerialPinChange1 = e.EventType;
            lblCTSStatus.BackColor = Color.Green;
            lblDTRStatus.BackColor = Color.Green;
            lblRIStatus.BackColor = Color.Green;
            lblBreakStatus.BackColor = Color.Green;

            switch (SerialPinChange1)
            {
                case SerialPinChange.Break:
                    lblBreakStatus.BackColor = Color.Red;
                    //MessageBox.Show("Break is Set");
                    break;
                case SerialPinChange.CDChanged:
                    signalState = _serialPort.CtsHolding;
                    //MessageBox.Show("CD = " + signalState.ToString());
                    break;
                case SerialPinChange.CtsChanged:
                    signalState = _serialPort.CDHolding;
                    lblCTSStatus.BackColor = Color.Red;
                    //MessageBox.Show("CTS = " + signalState.ToString());
                    break;
                case SerialPinChange.DsrChanged:
                    signalState = _serialPort.DsrHolding;
                    lblDTRStatus.BackColor = Color.Red;
                    // MessageBox.Show("DSR = " + signalState.ToString());
                    break;
                case SerialPinChange.Ring:
                    lblRIStatus.BackColor = Color.Red;
                    //MessageBox.Show("Ring Detected");
                    break;
            }
        }

        private void btnPortState_Click(object sender, EventArgs e)
        {
            if (btnPortState.Text == "Closed")
            {
                btnPortState.Text = "Open";
                _serialPort.PortName = Convert.ToString(portsComboBox.Text);
                _serialPort.BaudRate = Convert.ToInt32(cboBaudRate.Text);
                _serialPort.DataBits = Convert.ToInt16(databitsComboBox.Text);
                _serialPort.StopBits = (StopBits)Enum.Parse(typeof(StopBits), stoppComboBox.Text);
                _serialPort.Handshake = (Handshake)Enum.Parse(typeof(Handshake), cboHandShaking.Text);
                _serialPort.Parity = (Parity)Enum.Parse(typeof(Parity), parityComboBox.Text);
                _serialPort.Open();
            }
            else if (btnPortState.Text == "Open")
            {
                btnPortState.Text = "Closed";
                _serialPort.Close();
            }
        }

        private void connectToolStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (!_serialPort.IsOpen)
                    _serialPort.Open();
                connectToolStripButton.Enabled = false;
                disconnectToolStripButton.Enabled = true;
                sendButton.Enabled = true;
                //if (!readThread.IsAlive)
                    readThread.Start();
                //else
                //    Thread.ResetAbort();
                _continue = true;
                statusToolStripStatusLabel.Text = "Verbindung gestartet: ";
                consoleRichTextBox.Text = "Neue Session gestartet: " + DateTime.Now.ToString("HH:mm:ss") + "\n";
                //byte[] bytes = { 0x14, 0x0 };
                //_serialPort.Write(bytes, 0, bytes.Length);
            }
            catch (Exception ex)
            {
                consoleRichTextBox.Text = "Achtung: Fehler! Verbindung kann nicht geöffnet werden.\n" +
                    "Beende alle Anwendungen, die auf den COM-Port zugreifen, z.B. das Sigma Data Center.\n" + ex.Message;
            }
        }

        private void disconnectToolStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (_serialPort.IsOpen)
                {
                    _continue = false;
                    //readThread.Abort();
                    Thread.ResetAbort();
                    readThread.Join();
                    _serialPort.Close();
                }
                   
                consoleRichTextBox.Text += $"Verbindung beendet: {portsComboBox.Text} " + DateTime.Now.ToString("HH:mm:ss") + "\n";
                connectToolStripButton.Enabled = true;
                disconnectToolStripButton.Enabled = false;
                sendButton.Enabled = false;
                
                statusToolStripStatusLabel.Text = "Verbindung beendet: ";
            }
            catch (Exception)
            {

            }
        }

        private void sendButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (!_serialPort.IsOpen)
                    _serialPort.Open();
                //_serialPort.Encoding = Encoding.Default;
                //_serialPort.Write($"{commandTextBox.Text}\r\n");
                byte temp;
                byte[] b = null;
                foreach (char c in commandTextBox.Text)
                {
                    temp = Convert.ToByte(c); 
                    b.Append(temp);
                }
                //byte[] bytes = { 0x14, 0x0 };
                _serialPort.Write(b, 0, b.Length);
                //_serialPort.WriteLine($"{commandTextBox.Text}");
                consoleRichTextBox.Text += "SEND: " + commandTextBox.Text + "\n";
                commandTextBox.Text = "";
            }
            catch (Exception)
            {

            }
        }
    }
}
