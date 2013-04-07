using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Serial_Oscilloscope
{
    /// <summary>
    /// Dialog form to get text value from user.
    /// </summary>
    public partial class FormGetValue : Form
    {
        /// <summary>
        /// Value entered by user.
        /// </summary>
        public string value { get; private set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        public FormGetValue()
        {
            InitializeComponent();
        }

        /// <summary>
        /// textBoxValue KeyPress event to close form when Enter key pressed.
        /// </summary>
        private void textBoxValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                Close();
            }
        }

        /// <summary>
        /// buttonOK Click event to close form.
        /// </summary>
        private void buttonOK_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// FormGetValue FormClosed event to store value entered by user.
        /// </summary>
        private void FormGetValue_FormClosed(object sender, FormClosedEventArgs e)
        {
            value = textBoxValue.Text;
        }
    }
}
