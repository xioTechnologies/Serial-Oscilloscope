using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.IO.Ports;
using System.Text.RegularExpressions;
using System.Globalization;

namespace Serial_Oscilloscope
{
    public partial class FormTerminal : Form
    {
        #region Variables and objects

        /// <summary>
        /// Timer to update terminal textbox at fixed interval.
        /// </summary>
        private System.Windows.Forms.Timer formUpdateTimer = new System.Windows.Forms.Timer();

        /// <summary>
        /// SerialPort object.
        /// </summary>
        private SerialPort serialPort = new SerialPort();

        /// <summary>
        /// Sample counter to calculate performance statics.
        /// </summary>
        private SampleCounter sampleCounter = new SampleCounter();

        /// <summary>
        /// TextBoxBuffer containing text printed to terminal.
        /// </summary>
        private TextBoxBuffer textBoxBuffer = new TextBoxBuffer(4096);

        /// <summary>
        /// ASCII buffer for decoding CSVs in serial stream.
        /// </summary>
        private string asciiBuf = "";

        /// <summary>
        /// Oscilloscope channel values decoded from serial stream.
        /// </summary>
        private float[] channels = new float[9] { 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f};

        /// <summary>
        /// Oscilloscope for channels 1, 2 and 3.
        /// </summary>
        private Oscilloscope oscilloscope123 = Oscilloscope.CreateScope("Oscilloscope/Oscilloscope_settings.ini", "");

        /// <summary>
        /// Oscilloscope for channels 4, 5 and 6.
        /// </summary>
        private Oscilloscope oscilloscope456 = Oscilloscope.CreateScope("Oscilloscope/Oscilloscope_settings.ini", "");

        /// <summary>
        /// Oscilloscope for channels 7, 8 and 9.
        /// </summary>
        private Oscilloscope oscilloscope789 = Oscilloscope.CreateScope("Oscilloscope/Oscilloscope_settings.ini", "");

        /// <summary>
        /// CSV file writer.
        /// </summary>
        private CsvFileWriter csvFileWriter = null;

        #endregion

        /// <summary>
        /// Constructor.
        /// </summary>
        public FormTerminal()
        {
            InitializeComponent();
        }

        #region Form load and close

        /// <summary>
        /// From load event.
        /// </summary>
        private void FormTerminal_Load(object sender, EventArgs e)
        {
            // Set form caption
            this.Text = Assembly.GetExecutingAssembly().GetName().Name + " (Port Closed)";

            // Set oscilloscope captions
            oscilloscope123.Caption = "Channels 1, 2 and 3";
            oscilloscope456.Caption = "Channels 4, 5 and 6";
            oscilloscope789.Caption = "Channels 7, 8 and 9";

            // Refresh serial port list
            RefreshSerialPortList();

            // Select default baud rate
            toolStripMenuItem115200.Checked = true;

            // Setup form update timer
            formUpdateTimer.Interval = 50;
            formUpdateTimer.Tick += new EventHandler(formUpdateTimer_Tick);
            formUpdateTimer.Start();
        }

        /// <summary>
        /// Form close event.
        /// </summary>
        private void FormTerminal_FormClosed(object sender, FormClosedEventArgs e)
        {
            CloseSerialPort();
            toolStripMenuItemStopLogging.PerformClick();
        }

        #endregion

        #region Terminal textbox

        /// <summary>
        /// formUpdateTimer Tick event to update terminal textbox.
        /// </summary>
        void formUpdateTimer_Tick(object sender, EventArgs e)
        {
            // Print textBoxBuffer to terminal
            if (textBox.Enabled && !textBoxBuffer.IsEmpty())
            {
                textBox.AppendText(textBoxBuffer.Get());
                if (textBox.Text.Length > textBox.MaxLength)    // discard first half of textBox when number of characters exceeds length
                {
                    textBox.Text = textBox.Text.Substring(textBox.Text.Length / 2, textBox.Text.Length - textBox.Text.Length / 2);
                }
            }
            else
            {
                textBoxBuffer.Clear();
            }

            // Update sample counter values
            toolStripStatusLabelSamplesReceived.Text = "Samples Recieved: " + sampleCounter.SamplesReceived.ToString();
            toolStripStatusLabelSampleRate.Text = "Sample Rate: " + sampleCounter.SampleRate.ToString();
        }

        /// <summary>
        /// textBox KeyPress to send character to serial port.
        /// </summary>
        private void textBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            SendSerialPort(e.KeyChar);
            e.Handled = true;   // don't print character
        }

        #endregion

        #region Menu strip

        /// <summary>
        /// toolStripMenuItemSerialPort DropDownItemClicke event to select or close serial port.
        /// </summary>
        private void toolStripMenuItemSerialPort_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            // Close serial port and refresh list is refresh item clicked
            if (e.ClickedItem.Text == "Refresh List")
            {
                CloseSerialPort();
                RefreshSerialPortList();
                return;
            }

            // Close serial port if checked port name clicked
            if (((ToolStripMenuItem)e.ClickedItem).Checked)
            {
                CloseSerialPort();
                ((ToolStripMenuItem)e.ClickedItem).Checked = false;
                return;
            }

            // Check only selected item
            foreach (ToolStripMenuItem toolStripMenuItem in ((ToolStripMenuItem)toolStripMenuItemSerialPort).DropDownItems)
            {
                toolStripMenuItem.Checked = false;
            }
            ((ToolStripMenuItem)e.ClickedItem).Checked = true;

            // Open serial port
            if (!OpenSerialPort())
            {
                ((ToolStripMenuItem)e.ClickedItem).Checked = false;     // uncheck serial port if open fails
            }
        }

        /// <summary>
        /// toolStripMenuItemSerialPort DropDownItemClicke event to select baud rate.
        /// </summary>
        private void toolStripMenuItemBaudRate_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if ((ToolStripMenuItem)e.ClickedItem == toolStripMenuItemOther)
            {
                FormGetValue formGetValue = new FormGetValue();
                formGetValue.ShowDialog();
                ((ToolStripMenuItem)e.ClickedItem).Text = "Other (" + formGetValue.value + ")";
                ((ToolStripMenuItem)e.ClickedItem).Checked = false;
            }

            // Do nothing if baud already selected
            if (((ToolStripMenuItem)e.ClickedItem).Checked)
            {
                return;
            }

            // Check only selected item
            foreach (ToolStripMenuItem toolStripMenuItem in ((ToolStripMenuItem)toolStripMenuItemBaudRate).DropDownItems)
            {
                toolStripMenuItem.Checked = false;
            }
            ((ToolStripMenuItem)e.ClickedItem).Checked = true;

            // Open serial port
            if (!OpenSerialPort())
            {
                RefreshSerialPortList();    // refresh port list if open fails, this also ensures port object is closed
            }
        }

        /// <summary>
        /// toolStripMenuItemEnabled CheckStateChanged event to toggle enabled state of the terminal text box.
        /// </summary>
        private void toolStripMenuItemEnabled_CheckStateChanged(object sender, EventArgs e)
        {
            if (toolStripMenuItemEnabled.Checked)
            {
                textBox.Enabled = true;
            }
            else
            {
                textBox.Enabled = false;
            }
        }

        /// <summary>
        /// toolStripMenuItemClear Click event to clear terminal text box.
        /// </summary>
        private void toolStripMenuItemClear_Click(object sender, EventArgs e)
        {
            textBox.Text = "";
        }

        /// <summary>
        /// toolStripMenuItemChannels123 CheckStateChanged event to toggle show state of oscilloscope form.
        /// </summary>
        private void toolStripMenuItemChannels123_CheckStateChanged(object sender, EventArgs e)
        {
            if (toolStripMenuItemChannels123.Checked)
            {
                oscilloscope123.ShowScope();
            }
            else
            {
                oscilloscope123.HideScope();
            }
        }

        /// <summary>
        /// toolStripMenuItemChannels456 CheckStateChanged event to toggle show state of oscilloscope form.
        /// </summary>
        private void toolStripMenuItemChannels456_CheckStateChanged(object sender, EventArgs e)
        {
            if (toolStripMenuItemChannels456.Checked)
            {
                oscilloscope456.ShowScope();
            }
            else
            {
                oscilloscope456.HideScope();
            }
        }

        /// <summary>
        /// toolStripMenuItemChannels789 CheckStateChanged event to toggle show state of oscilloscope form.
        /// </summary>
        private void toolStripMenuItemChannels789_CheckStateChanged(object sender, EventArgs e)
        {
            if (toolStripMenuItemChannels789.Checked)
            {
                oscilloscope789.ShowScope();
            }
            else
            {
                oscilloscope789.HideScope();
            }
        }

        /// <summary>
        /// toolStripMenuItemStartLogging Click event to select file location and start logging
        /// </summary>
        private void toolStripMenuItemStartLogging_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = "Select File Location";
            saveFileDialog.Filter = "CSV files (*.csv)|*.csv";
            saveFileDialog.OverwritePrompt = false;
            saveFileDialog.FileName = "LoggedData";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                csvFileWriter = new CsvFileWriter(saveFileDialog.FileName.ToString());
                toolStripMenuItemStartLogging.Enabled = false;
                toolStripMenuItemStopLogging.Enabled = true;
            }
        }

        /// <summary>
        /// toolStripMenuItemStopLogging Click event to stop logging and close file
        /// </summary>
        private void toolStripMenuItemStopLogging_Click(object sender, EventArgs e)
        {
            if (csvFileWriter != null)
            {
                csvFileWriter.CloseFile();
                csvFileWriter = null;
            }
            toolStripMenuItemStartLogging.Enabled = true;
            toolStripMenuItemStopLogging.Enabled = false;
        }

        /// <summary>
        /// toolStripMenuItemAbout Click event to display version details.
        /// </summary>
        private void toolStripMenuItemAbout_Click(object sender, EventArgs e)
        {
            MessageBox.Show(Assembly.GetExecutingAssembly().GetName().Name + " " + Assembly.GetExecutingAssembly().GetName().Version.Major.ToString() + "." + Assembly.GetExecutingAssembly().GetName().Version.Minor.ToString(), "About", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// toolStripMenuItemSourceCode Click event to open web browser.
        /// </summary>
        private void toolStripMenuItemSourceCode_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("https://github.com/xioTechnologies/Serial-Oscilloscope");
            }
            catch { }
        }

        #endregion

        #region Serial port

        /// <summary>
        /// Updates toolStripMenuItemSerialPort DropDownItems to include all available serial port.
        /// </summary>
        private void RefreshSerialPortList()
        {
            ToolStripItemCollection toolStripItemCollection = toolStripMenuItemSerialPort.DropDownItems;
            toolStripItemCollection.Clear();
            toolStripItemCollection.Add("Refresh List");
            foreach (string portName in System.IO.Ports.SerialPort.GetPortNames())
            {
                toolStripItemCollection.Add("COM" + Regex.Replace(portName.Substring("COM".Length, portName.Length - "COM".Length), "[^.0-9]", "\0"));
            }
        }

        /// <summary>
        /// Opens serial port. Displays error in MessageBox if unsuccessful.
        /// </summary>
        /// <returns>
        /// true if successful.
        /// </returns>
        private bool OpenSerialPort()
        {
            string portName = null;
            int baudRate = 0;

            // Get selected port name
            foreach (ToolStripMenuItem toolStripMenuItem in ((ToolStripMenuItem)toolStripMenuItemSerialPort).DropDownItems)
            {
                if (toolStripMenuItem.Checked)
                {
                    portName = toolStripMenuItem.Text;
                    break;
                }
            }
            if (portName == null)
            {
                return false;
            }

            // Get selected baud rate
            foreach (ToolStripMenuItem toolStripMenuItem in ((ToolStripMenuItem)toolStripMenuItemBaudRate).DropDownItems)
            {
                if (toolStripMenuItem.Checked)
                {
                    try
                    {
                        baudRate = Convert.ToInt32((new Regex("[^0-9]")).Replace(toolStripMenuItem.Text, ""));  // convert text to int ignoring all non-numerical characters
                    }
                    catch
                    {
                        baudRate = 0;
                    }
                    break;
                }
            }

            // Open serial port
            CloseSerialPort();
            try
            {
                serialPort = new SerialPort(portName, baudRate, Parity.None, 8, StopBits.One);
                serialPort.DataReceived += new SerialDataReceivedEventHandler(serialPort_DataReceived);
                serialPort.Open();
                this.Text = Assembly.GetExecutingAssembly().GetName().Name + " (" + portName + ", " + baudRate.ToString() + ")";
                sampleCounter.Reset();
                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return false;
            }
        }

        /// <summary>
        /// Closes serial port.
        /// </summary>
        private void CloseSerialPort()
        {
            try
            {
                serialPort.Close();
            }
            catch { }
            this.Text = Assembly.GetExecutingAssembly().GetName().Name + " (Port Closed)";
        }

        /// <summary>
        /// Sends character to serial port.
        /// </summary>
        /// <param name="c">
        /// Character to send to serial port.
        /// </param>
        private void SendSerialPort(char c)
        {
            try
            {
                serialPort.Write(new char[] { c }, 0, 1);
            }
            catch { }
        }

        /// <summary>
        /// serialPort DataReceived event to print characters to terminal and process bytes through serialDecoder.
        /// </summary>
        private void serialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                // Get bytes from serial port
                int bytesToRead = serialPort.BytesToRead;
                byte[] readBuffer = new byte[bytesToRead];
                serialPort.Read(readBuffer, 0, bytesToRead);

                // Process each byte
                foreach (byte b in readBuffer)
                {
                    // Parse character to textBoxBuffer
                    if ((b < 0x20 || b > 0x7F) && b != '\r')    // replace non-printable characters with '.'
                    {
                        textBoxBuffer.Put(".");
                    }
                    else if (b == '\r')     // replace carriage return with '↵' and valid new line
                    {
                        textBoxBuffer.Put("↵" + Environment.NewLine);
                    }
                    else
                    {
                        textBoxBuffer.Put(((char)b).ToString());
                    }

                    // Extract CSVs and parse to Oscilloscope
                    if (asciiBuf.Length > 128)
                    {
                        asciiBuf = "";  // prevent memory leak
                    }
                    if ((char)b == '\r')
                    {
                        // Split string to comma separated variables (ignore non numerical characters)
                        string[] csvs = (new Regex(@"[^0-9\-,.]")).Replace(asciiBuf, "").Split(',');

                        // Extract each CSV as oscilloscope channel 
                        int channelIndex = 0;
                        foreach (string csv in csvs)
                        {
                            if (csv != "" && channelIndex < 9)
                            {
                                channels[channelIndex++] = float.Parse(csv, CultureInfo.InvariantCulture);
                            }
                        }

                        // Update oscilloscopes if channel values changed
                        if (channelIndex > 0)
                        {
                            oscilloscope123.AddScopeData(channels[0], channels[1], channels[2]);
                            oscilloscope456.AddScopeData(channels[3], channels[4], channels[5]);
                            oscilloscope789.AddScopeData(channels[6], channels[7], channels[8]);
                            sampleCounter.Increment();
                        }

                        // Write to file if enabled
                        if (csvFileWriter != null)
                        {
                            csvFileWriter.WriteCSVline(channels);
                        }

                        // Reset buffer
                        asciiBuf = "";
                    }
                    else
                    {
                        asciiBuf += (char)b;
                    }
                }
            }
            catch { }
        }

        #endregion
    }
}
