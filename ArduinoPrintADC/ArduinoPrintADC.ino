/*
    ArduinoPrintADC.ino

    Author: Seb Madgwick

    Sends up to all 6 analogue inputs values in ASCII as comma separated values
    over serial.  Each line is terminated with a carriage return character ('\r').
    The number of channels is sent by sending a character value of '1' to '6' to 
    the Arduino.
    
    Tested with "arduino-1.0.3" and "Arduino Uno".

 */

#include <stdlib.h> // div, div_t

void setup() {

    // Enable pull-ups to avoid floating inputs
    digitalWrite(A0, HIGH);
    digitalWrite(A1, HIGH);
    digitalWrite(A2, HIGH);
    digitalWrite(A3, HIGH);
    digitalWrite(A6, HIGH);
    digitalWrite(A7, HIGH);

    // Init serial
    Serial.begin(115200);
}

void loop() {
    static int numChans = 1;

    // Received character sets number of active channels
    while(Serial.available() > 0) {
        char c = Serial.read();
        if(c >= '1' && c <= '6') {
            numChans = c - '0';
        }
    }

    // Print ADC results for active channels
    PrintInt(analogRead(A0));
    if(numChans > 1) {
        Serial.write(',');
        PrintInt(analogRead(A1));
    }
    if(numChans > 2) {
        Serial.write(',');
        PrintInt(analogRead(A2));
    }
    if(numChans > 3) {
        Serial.write(',');
        PrintInt(analogRead(A3));
    }
    if(numChans > 4) {
        Serial.write(',');
        PrintInt(analogRead(A6));
    }
    if(numChans > 5) {
        Serial.write(',');
        PrintInt(analogRead(A7));
    }
    Serial.write('\r'); // print new line
}

// Fast int to ASCII conversion
void PrintInt(int i) {
    static const char asciiDigits[10] = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
    div_t n;
    int print = 0;
    if(i < 0) {
        Serial.write('-');
        i = -i;
    }
    if(i >= 10000) {
        n = div(i, 10000);
        Serial.write(asciiDigits[n.quot]);
        i = n.rem;
        print = 1;
    }
    if(i >= 1000 || print) {
        n = div(i, 1000);
        Serial.write(asciiDigits[n.quot]);
        i = n.rem;
        print = 1;
    }
    if(i >= 100 || print) {
        n = div(i, 100);
        Serial.write(asciiDigits[n.quot]);
        i = n.rem;
        print = 1;
    }
    if(i >= 10 || print) {
        n = div(i, 10);
        Serial.write(asciiDigits[n.quot]);
        i = n.rem;
    }
    Serial.write(asciiDigits[i]);
}
