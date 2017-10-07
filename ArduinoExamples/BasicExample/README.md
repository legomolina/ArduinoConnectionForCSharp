# Arduino Connection For C#: Basic example
This example demonstrates you how to attach events to the Arduino connection and turn the built-in led on when arduino is connected.

## Code 
C# code is under Basic Example project in Solution explorer and BasicExample folder inside ArduinoExamples folder.

### C# code
First of all is setting the handshake. Handshake is the way that the PC knows which Arduino is looking for, so it's very important that you use the same handshake in the C# code and ino code.

With the handshake setted, you can create a connection and attach the connected and disconnected events. This events are fired when an Arduino with the same handshake is detected and saved, and when that Arduino is disconnected.

Last step is Starting the connection. I created this method because the connection works in custom thread so you can start and stop it when you want.

I write a little code to show when the Arduino is connected and when is disconnected in both events.

### Ino code
Set the handshake the same you have in the C# code.

The ino code just sets the built-in led to output and, when the program connects to the board it sets it to high.