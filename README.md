Serial-Oscilloscope
===================

Serial Oscilloscope is a Windows application that plots comma-separated variables within any incoming serial steam as channels on a real-time oscilloscope. The application also functions as a basic serial terminal, received bytes are printed to the terminal and typed characters are transmitted. The project uses Michael Bernstein's [oscilloscope library](http://www.oscilloscope-lib.com/) to plot up to 9 channels on 3 different oscilloscope with view and trigger menus.

Serial Oscilloscope is compatible with any serial stream containing comma-separated values terminated by a new-line character ("\r"). For example, "11,22,33\r" will be interpreted as values 11, 22 and 33 for channels 1, 2 and 3 respectively. The serial stream can also include non numerical characters which will be ignored. For example, "a=0.5,blue,x=3.14,t1t2t3,8\r\n" will be interpreted as values 0.5, 3.14, 123 and 8 for channels 1, 2, 3 and 4 respectively.

The source files also include an Arduino sketch to send analogue input values over serial.  Up to 6 ADC channels can be enabled by sending the characters "1" to "6" to the Arduino.  Enabling more channels will reduce the sample rate.

In the [YouTube video](http://www.youtube.com/watch?v=jgMG0UQ2_pc) I show the Arduino and Serial Oscilloscope being used to plot data from an [IR distance sensor]( https://www.sparkfun.com/products/242), a [triple-axis accelerometer]( https://www.sparkfun.com/products/9269) and a [microphone]( https://www.sparkfun.com/products/9964).

Precompiled binary files can be [downloaded](http://www.x-io.co.uk/serial-oscilloscope/) from the x-io website.

Version history
---------------

* **v1.0**  Initial release
* **v1.1**  Supports non-standard baud rates.  Disable terminal feature added.
* **v1.2**  Fixed memory leak and bug that prevent plotting of negative numbers.
* **v1.3**  No longer ignores "." for plotting decimal values.
* **v1.4**  Clear terminal menu item added
* **v1.5**  Log to file tool.  Remove non-numerical characters from port names.
