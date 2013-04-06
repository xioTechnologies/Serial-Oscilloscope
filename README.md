Serial-Oscilloscope
===================

Serial Oscilloscope plots comma-separated variables within any incoming serial steam as channels on a real-time oscilloscope using Michael Bernstein's [oscilloscope library](http://www.oscilloscope-lib.com/).  The application also functions as a basic serial terminal - received bytes are printed to the terminal output and typed characters are sent over serial.

The source files also include a Arduino Sketch to send ADC value over serial.  Up to 6 ADC channels can be enabled by sending the characters ‘1’ to ‘6’.  Enabling more channels will reduce the sample rate.

[Demo video](http://www.youtube.com/watch?v=jgMG0UQ2_pc) on YouTube and [binary download](http://www.x-io.co.uk/serial-oscilloscope/) on the x-io website.
