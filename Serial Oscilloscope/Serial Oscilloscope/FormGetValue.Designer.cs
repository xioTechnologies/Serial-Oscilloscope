namespace Serial_Oscilloscope
{
    partial class FormGetValue
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
            this.textBoxValue = new System.Windows.Forms.TextBox();
            this.labelValue = new System.Windows.Forms.Label();
            this.buttonOK = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBoxValue
            // 
            this.textBoxValue.Location = new System.Drawing.Point(55, 12);
            this.textBoxValue.Name = "textBoxValue";
            this.textBoxValue.Size = new System.Drawing.Size(100, 20);
            this.textBoxValue.TabIndex = 1;
            this.textBoxValue.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxValue_KeyPress);
            // 
            // labelValue
            // 
            this.labelValue.AutoSize = true;
            this.labelValue.Location = new System.Drawing.Point(12, 15);
            this.labelValue.Name = "labelValue";
            this.labelValue.Size = new System.Drawing.Size(37, 13);
            this.labelValue.TabIndex = 0;
            this.labelValue.Text = "Value:";
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(161, 10);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 2;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // FormGetValue
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(246, 48);
            this.Controls.Add(this.labelValue);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.textBoxValue);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormGetValue";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Enter Value";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormGetValue_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxValue;
        private System.Windows.Forms.Label labelValue;
        private System.Windows.Forms.Button buttonOK;
    }
}