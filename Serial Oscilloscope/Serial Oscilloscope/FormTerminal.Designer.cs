namespace Serial_Oscilloscope
{
    partial class FormTerminal
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItemSerialPort = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemBaudRate = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem9600 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem19200 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem38400 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem57600 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem115200 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem230400 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem460800 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem921600 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemOther = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemTerminal = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemEnabled = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemClear = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemOsciloscope = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemChannels123 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemChannels456 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemChannels789 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemLogToFile = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemStartLogging = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemStopLogging = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemAbout0 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemSourceCode = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabelSamplesReceived = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelSampleRate = new System.Windows.Forms.ToolStripStatusLabel();
            this.textBox = new System.Windows.Forms.TextBox();
            this.serialPinsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rTSToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dTRToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemSerialPort,
            this.toolStripMenuItemBaudRate,
            this.serialPinsToolStripMenuItem,
            this.toolStripMenuItemTerminal,
            this.toolStripMenuItemOsciloscope,
            this.toolStripMenuItemLogToFile,
            this.toolStripMenuItemHelp});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(779, 28);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "menuStrip1";
            // 
            // toolStripMenuItemSerialPort
            // 
            this.toolStripMenuItemSerialPort.Name = "toolStripMenuItemSerialPort";
            this.toolStripMenuItemSerialPort.Size = new System.Drawing.Size(90, 24);
            this.toolStripMenuItemSerialPort.Text = "Serial Port";
            this.toolStripMenuItemSerialPort.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.toolStripMenuItemSerialPort_DropDownItemClicked);
            // 
            // toolStripMenuItemBaudRate
            // 
            this.toolStripMenuItemBaudRate.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem9600,
            this.toolStripMenuItem19200,
            this.toolStripMenuItem38400,
            this.toolStripMenuItem57600,
            this.toolStripMenuItem115200,
            this.toolStripMenuItem230400,
            this.toolStripMenuItem460800,
            this.toolStripMenuItem921600,
            this.toolStripMenuItemOther});
            this.toolStripMenuItemBaudRate.Name = "toolStripMenuItemBaudRate";
            this.toolStripMenuItemBaudRate.Size = new System.Drawing.Size(91, 24);
            this.toolStripMenuItemBaudRate.Text = "Baud Rate";
            this.toolStripMenuItemBaudRate.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.toolStripMenuItemBaudRate_DropDownItemClicked);
            // 
            // toolStripMenuItem9600
            // 
            this.toolStripMenuItem9600.Name = "toolStripMenuItem9600";
            this.toolStripMenuItem9600.Size = new System.Drawing.Size(140, 26);
            this.toolStripMenuItem9600.Text = "9600";
            // 
            // toolStripMenuItem19200
            // 
            this.toolStripMenuItem19200.Name = "toolStripMenuItem19200";
            this.toolStripMenuItem19200.Size = new System.Drawing.Size(140, 26);
            this.toolStripMenuItem19200.Text = "19200";
            // 
            // toolStripMenuItem38400
            // 
            this.toolStripMenuItem38400.Name = "toolStripMenuItem38400";
            this.toolStripMenuItem38400.Size = new System.Drawing.Size(140, 26);
            this.toolStripMenuItem38400.Text = "38400";
            // 
            // toolStripMenuItem57600
            // 
            this.toolStripMenuItem57600.Name = "toolStripMenuItem57600";
            this.toolStripMenuItem57600.Size = new System.Drawing.Size(140, 26);
            this.toolStripMenuItem57600.Text = "57600";
            // 
            // toolStripMenuItem115200
            // 
            this.toolStripMenuItem115200.Name = "toolStripMenuItem115200";
            this.toolStripMenuItem115200.Size = new System.Drawing.Size(140, 26);
            this.toolStripMenuItem115200.Text = "115200";
            // 
            // toolStripMenuItem230400
            // 
            this.toolStripMenuItem230400.Name = "toolStripMenuItem230400";
            this.toolStripMenuItem230400.Size = new System.Drawing.Size(140, 26);
            this.toolStripMenuItem230400.Text = "230400";
            // 
            // toolStripMenuItem460800
            // 
            this.toolStripMenuItem460800.Name = "toolStripMenuItem460800";
            this.toolStripMenuItem460800.Size = new System.Drawing.Size(140, 26);
            this.toolStripMenuItem460800.Text = "460800";
            // 
            // toolStripMenuItem921600
            // 
            this.toolStripMenuItem921600.Name = "toolStripMenuItem921600";
            this.toolStripMenuItem921600.Size = new System.Drawing.Size(140, 26);
            this.toolStripMenuItem921600.Text = "921600";
            // 
            // toolStripMenuItemOther
            // 
            this.toolStripMenuItemOther.Name = "toolStripMenuItemOther";
            this.toolStripMenuItemOther.Size = new System.Drawing.Size(140, 26);
            this.toolStripMenuItemOther.Text = "Other";
            // 
            // toolStripMenuItemTerminal
            // 
            this.toolStripMenuItemTerminal.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemEnabled,
            this.toolStripMenuItemClear});
            this.toolStripMenuItemTerminal.Name = "toolStripMenuItemTerminal";
            this.toolStripMenuItemTerminal.Size = new System.Drawing.Size(80, 24);
            this.toolStripMenuItemTerminal.Text = "Terminal";
            // 
            // toolStripMenuItemEnabled
            // 
            this.toolStripMenuItemEnabled.Checked = true;
            this.toolStripMenuItemEnabled.CheckOnClick = true;
            this.toolStripMenuItemEnabled.CheckState = System.Windows.Forms.CheckState.Checked;
            this.toolStripMenuItemEnabled.Name = "toolStripMenuItemEnabled";
            this.toolStripMenuItemEnabled.Size = new System.Drawing.Size(146, 26);
            this.toolStripMenuItemEnabled.Text = "Enabled";
            this.toolStripMenuItemEnabled.CheckStateChanged += new System.EventHandler(this.toolStripMenuItemEnabled_CheckStateChanged);
            // 
            // toolStripMenuItemClear
            // 
            this.toolStripMenuItemClear.Name = "toolStripMenuItemClear";
            this.toolStripMenuItemClear.Size = new System.Drawing.Size(146, 26);
            this.toolStripMenuItemClear.Text = "Clear";
            this.toolStripMenuItemClear.Click += new System.EventHandler(this.toolStripMenuItemClear_Click);
            // 
            // toolStripMenuItemOsciloscope
            // 
            this.toolStripMenuItemOsciloscope.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemChannels123,
            this.toolStripMenuItemChannels456,
            this.toolStripMenuItemChannels789});
            this.toolStripMenuItemOsciloscope.Name = "toolStripMenuItemOsciloscope";
            this.toolStripMenuItemOsciloscope.Size = new System.Drawing.Size(107, 24);
            this.toolStripMenuItemOsciloscope.Text = "Oscilloscope";
            // 
            // toolStripMenuItemChannels123
            // 
            this.toolStripMenuItemChannels123.CheckOnClick = true;
            this.toolStripMenuItemChannels123.Name = "toolStripMenuItemChannels123";
            this.toolStripMenuItemChannels123.Size = new System.Drawing.Size(219, 26);
            this.toolStripMenuItemChannels123.Text = "Channels 1, 2 and 3";
            this.toolStripMenuItemChannels123.CheckStateChanged += new System.EventHandler(this.toolStripMenuItemChannels123_CheckStateChanged);
            // 
            // toolStripMenuItemChannels456
            // 
            this.toolStripMenuItemChannels456.CheckOnClick = true;
            this.toolStripMenuItemChannels456.Name = "toolStripMenuItemChannels456";
            this.toolStripMenuItemChannels456.Size = new System.Drawing.Size(219, 26);
            this.toolStripMenuItemChannels456.Text = "Channels 3, 4 and 5";
            this.toolStripMenuItemChannels456.Click += new System.EventHandler(this.toolStripMenuItemChannels456_CheckStateChanged);
            // 
            // toolStripMenuItemChannels789
            // 
            this.toolStripMenuItemChannels789.CheckOnClick = true;
            this.toolStripMenuItemChannels789.Name = "toolStripMenuItemChannels789";
            this.toolStripMenuItemChannels789.Size = new System.Drawing.Size(219, 26);
            this.toolStripMenuItemChannels789.Text = "Channels 7, 8 and 9";
            this.toolStripMenuItemChannels789.CheckStateChanged += new System.EventHandler(this.toolStripMenuItemChannels789_CheckStateChanged);
            // 
            // toolStripMenuItemLogToFile
            // 
            this.toolStripMenuItemLogToFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemStartLogging,
            this.toolStripMenuItemStopLogging});
            this.toolStripMenuItemLogToFile.Name = "toolStripMenuItemLogToFile";
            this.toolStripMenuItemLogToFile.Size = new System.Drawing.Size(95, 24);
            this.toolStripMenuItemLogToFile.Text = "Log To File";
            // 
            // toolStripMenuItemStartLogging
            // 
            this.toolStripMenuItemStartLogging.Name = "toolStripMenuItemStartLogging";
            this.toolStripMenuItemStartLogging.Size = new System.Drawing.Size(182, 26);
            this.toolStripMenuItemStartLogging.Text = "Start Logging";
            this.toolStripMenuItemStartLogging.Click += new System.EventHandler(this.toolStripMenuItemStartLogging_Click);
            // 
            // toolStripMenuItemStopLogging
            // 
            this.toolStripMenuItemStopLogging.Enabled = false;
            this.toolStripMenuItemStopLogging.Name = "toolStripMenuItemStopLogging";
            this.toolStripMenuItemStopLogging.Size = new System.Drawing.Size(182, 26);
            this.toolStripMenuItemStopLogging.Text = "Stop Logging";
            this.toolStripMenuItemStopLogging.Click += new System.EventHandler(this.toolStripMenuItemStopLogging_Click);
            // 
            // toolStripMenuItemHelp
            // 
            this.toolStripMenuItemHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemAbout0,
            this.toolStripMenuItemSourceCode});
            this.toolStripMenuItemHelp.Name = "toolStripMenuItemHelp";
            this.toolStripMenuItemHelp.Size = new System.Drawing.Size(55, 24);
            this.toolStripMenuItemHelp.Text = "Help";
            // 
            // toolStripMenuItemAbout0
            // 
            this.toolStripMenuItemAbout0.Name = "toolStripMenuItemAbout0";
            this.toolStripMenuItemAbout0.Size = new System.Drawing.Size(176, 26);
            this.toolStripMenuItemAbout0.Text = "About";
            this.toolStripMenuItemAbout0.Click += new System.EventHandler(this.toolStripMenuItemAbout_Click);
            // 
            // toolStripMenuItemSourceCode
            // 
            this.toolStripMenuItemSourceCode.Name = "toolStripMenuItemSourceCode";
            this.toolStripMenuItemSourceCode.Size = new System.Drawing.Size(176, 26);
            this.toolStripMenuItemSourceCode.Text = "Source Code";
            this.toolStripMenuItemSourceCode.Click += new System.EventHandler(this.toolStripMenuItemSourceCode_Click);
            // 
            // statusStrip
            // 
            this.statusStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabelSamplesReceived,
            this.toolStripStatusLabelSampleRate});
            this.statusStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.statusStrip.Location = new System.Drawing.Point(0, 420);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Padding = new System.Windows.Forms.Padding(1, 0, 19, 0);
            this.statusStrip.Size = new System.Drawing.Size(779, 26);
            this.statusStrip.TabIndex = 1;
            this.statusStrip.Text = "statusStrip1";
            // 
            // toolStripStatusLabelSamplesReceived
            // 
            this.toolStripStatusLabelSamplesReceived.Name = "toolStripStatusLabelSamplesReceived";
            this.toolStripStatusLabelSamplesReceived.Size = new System.Drawing.Size(259, 20);
            this.toolStripStatusLabelSamplesReceived.Text = "toolStripStatusLabelSamplesReceived";
            // 
            // toolStripStatusLabelSampleRate
            // 
            this.toolStripStatusLabelSampleRate.Name = "toolStripStatusLabelSampleRate";
            this.toolStripStatusLabelSampleRate.Size = new System.Drawing.Size(223, 20);
            this.toolStripStatusLabelSampleRate.Text = "toolStripStatusLabelSampleRate";
            // 
            // textBox
            // 
            this.textBox.BackColor = System.Drawing.Color.Black;
            this.textBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox.Font = new System.Drawing.Font("Courier New", 8.25F);
            this.textBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.textBox.Location = new System.Drawing.Point(0, 28);
            this.textBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBox.Multiline = true;
            this.textBox.Name = "textBox";
            this.textBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox.Size = new System.Drawing.Size(779, 392);
            this.textBox.TabIndex = 1;
            this.textBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_KeyPress);
            // 
            // serialPinsToolStripMenuItem
            // 
            this.serialPinsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dTRToolStripMenuItem,
            this.rTSToolStripMenuItem});
            this.serialPinsToolStripMenuItem.Name = "serialPinsToolStripMenuItem";
            this.serialPinsToolStripMenuItem.Size = new System.Drawing.Size(90, 24);
            this.serialPinsToolStripMenuItem.Text = "Serial Pins";
            // 
            // rTSToolStripMenuItem
            // 
            this.rTSToolStripMenuItem.Name = "rTSToolStripMenuItem";
            this.rTSToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.rTSToolStripMenuItem.Text = "RTS";
            this.rTSToolStripMenuItem.Click += new System.EventHandler(this.rTSToolStripMenuItem_Click);
            // 
            // dTRToolStripMenuItem
            // 
            this.dTRToolStripMenuItem.Checked = true;
            this.dTRToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.dTRToolStripMenuItem.Name = "dTRToolStripMenuItem";
            this.dTRToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.dTRToolStripMenuItem.Text = "DTR";
            this.dTRToolStripMenuItem.Click += new System.EventHandler(this.dTRToolStripMenuItem_Click);
            // 
            // FormTerminal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(779, 446);
            this.Controls.Add(this.textBox);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.menuStrip);
            this.MainMenuStrip = this.menuStrip;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "FormTerminal";
            this.Text = "FormTerminal";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormTerminal_FormClosed);
            this.Load += new System.EventHandler(this.FormTerminal_Load);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.TextBox textBox;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemSerialPort;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemOsciloscope;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemHelp;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelSamplesReceived;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelSampleRate;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemBaudRate;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem9600;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem19200;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem38400;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem57600;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem115200;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem230400;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem460800;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem921600;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemChannels123;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemChannels456;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemChannels789;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemAbout0;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemSourceCode;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemOther;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemTerminal;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemEnabled;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemClear;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemLogToFile;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemStartLogging;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemStopLogging;
        private System.Windows.Forms.ToolStripMenuItem serialPinsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dTRToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rTSToolStripMenuItem;
    }
}

