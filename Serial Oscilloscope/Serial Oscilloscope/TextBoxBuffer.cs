using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Serial_Oscilloscope
{
    /// <summary>
    /// TextBox FIFO buffer.
    /// </summary>
    class TextBoxBuffer
    {
        /// <summary>
        /// Size of buffer in bytes.
        /// </summary>
        private int size;

        /// <summary>
        /// Buffer array.
        /// </summary>
        private char[] buffer;

        /// <summary>
        /// Buffer in position index.
        /// </summary>
        private int inPos = 0;

        /// <summary>
        /// Buffer out position index.
        /// </summary>
        private int outPos = 0;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="size">
        /// Size of buffer.
        /// </param>
        public TextBoxBuffer(int size)
        {
            this.size = size;
            buffer = new char[this.size];
        }

        /// <summary>
        /// Returns value indicating if buffer is empty.
        /// </summary>
        /// <returns>
        /// Value indicating if buffer is empty.
        /// </returns>
        public bool IsEmpty()
        {
            return inPos == outPos;
        }

        /// <summary>
        /// Adds string to buffer.
        /// </summary>
        /// <param name="str">
        /// String to add to buffer.
        /// </param>
        public void Put(string str)
        {
            foreach (char c in str)
            {
                buffer[inPos] = c;
                if (++inPos == size - 1)
                {
                    inPos = 0;
                }
            }
        }

        /// <summary>
        /// Gets buffer contents.
        /// </summary>
        /// <returns>
        /// Buffer contents.
        /// </returns>
        public string Get()
        {
            string str = "";
            while (inPos != outPos)
            {
                str += buffer[outPos];
                if (++outPos == size - 1)
                {
                    outPos = 0;
                }
            }
            return str;
        }

        /// <summary>
        /// Clears buffer.
        /// </summary>
        public void Clear()
        {
            inPos = 0;
            outPos = 0;
        }
    }
}