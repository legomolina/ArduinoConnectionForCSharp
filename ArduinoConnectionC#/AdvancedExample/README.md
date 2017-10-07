# Arduino Connection For C#: Advanced example
This example demonstrates you how to use the ArduinoConnection while sending data to an Arduino board and how to say to Arduino that the program is closed.

## Code 
C# code is under Advanced Example project in Solution explorer and AdvancedExample folder inside ArduinoExamples folder.

### C# code
In this example I'm using [this code](https://stackoverflow.com/questions/9897247/run-code-on-console-close#answer-9897366 "Stack overflow") for console closing event.

First of all is setting the handshake. Handshake is the way that the PC knows which Arduino is looking for, so it's very important that you use the same handshake in the C# code and ino code.

With the handshake setted, you can create a connection and attach the connected and disconnected events. This events are fired when an Arduino with the same handshake is detected and saved, and when that Arduino is disconnected.

Last step is Starting the connection. I created this method because the connection works in custom thread so you can start and stop it when you want.

Also I've created a infinite loop that sends data to the arduino every second. It's important that to the `SendData()` method I'm sending a `1`. This is because I've decided that the first byte buffer is the status application so Arduino knows it (0 => off, 1 => on). If Arduino doesn't know if PC application is running may cause problems if you don't restart the board because the connection is always true.

The `SendData()` method just creates an array with the application status byte and 4 more random bytes. (Just for testing. It would be 5, 6 or 50).

It's important to check if arduino is connected before send any data. To do that I've created a boolean that holds if arduino is connected or not. I update this variable in the events that I've attached previously.

Finally in the `ConsoleCtrlCheck()` method I call `SendData()` with a '0' to indicate the application is no longer running.

### Ino code
Set the handshake the same you have in the C# code.

The ino code sets the built-in led to output, then it waits for any connection and if it is connected, sets the built-in led on and waits for 5 bytes of data (setted at the begining of the file). This number must be the same as the byte array length sended by C#. 

If there are 5 bytes of information, checks for the application status and if it's `0` sets the connection to false to indicate the program is closed and it should restart the program and wait for other connection.
Then assigns the readed data to an array and lets you to work with it.
