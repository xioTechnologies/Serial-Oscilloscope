using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Serial_Oscilloscope
{
    /// <summary>
    /// Sample counter. Tracks number of packets received and packet rate.
    /// </summary>
    class SampleCounter
    {
        /// <summary>
        /// Timer to calculate packet rate.
        /// </summary>
        private System.Windows.Forms.Timer timer;

        /// <summary>
        /// Number of packets received.
        /// </summary>
        public int SamplesReceived { get; private set; }

        /// <summary>
        /// Sample receive rate as packets per second.
        /// </summary>
        public int SampleRate { get; private set; }

        /// <summary>
        /// Variable used to calculate packet rate.
        /// </summary>
        private int prevSamplesReceived;

        /// <summary>
        /// Constructor.
        /// </summary>
        public SampleCounter()
        {
            // Initialise variables
            prevSamplesReceived = 0;
            SamplesReceived = 0;

            // Setup timer
            timer = new System.Windows.Forms.Timer();
            timer.Interval = 1000;
            timer.Tick += new EventHandler(timer_Tick);
            timer.Start();
        }

        /// <summary>
        /// Increments packet counter.
        /// </summary>
        public void Increment()
        {
            SamplesReceived++;
        }

        // Zeros packet counter.
        public void Reset()
        {
            prevSamplesReceived = 0;
            SamplesReceived = 0;
            SampleRate = 0;
        }

        /// <summary>
        /// timer Tick event to calculate packet rate.
        /// </summary>
        void timer_Tick(object sender, EventArgs e)
        {
            SampleRate = SamplesReceived - prevSamplesReceived;
            prevSamplesReceived = SamplesReceived;
        }
    }
}