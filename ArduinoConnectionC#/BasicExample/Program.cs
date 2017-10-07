/*
 * This example demonstrates you how to attach events to the Arduino connection and turn the built-in led on when arduino is connected
 * Read the README that it's located inside this project directory for more information
 */ 
using System;

namespace BasicExample {
    public class Program {
        private byte[] handshake;
        private ArduinoConnection.ArduinoConnection connection;

        public Program() {
            //IMPORTANT! Set the handshake same as the ino code is set

            //Create custom handshake and connection
            handshake = new byte[] { 16, 4, 64, 32 };
            connection = new ArduinoConnection.ArduinoConnection(handshake);

            //Attach connected and disconnected events from connection
            connection.ArduinoConnected += Connection_ArduinoConnected;
            connection.ArduinoDisconnected += Connection_ArduinoDisconnected;

            //Start the connection
            connection.Start();
        }

        private void Connection_ArduinoConnected(object connection, ArduinoConnection.ArduinoConnection.ConnectionEventArgs connectionInformation) {
            Console.WriteLine("Arduino connected!");
        }

        private void Connection_ArduinoDisconnected(object connection, ArduinoConnection.ArduinoConnection.ConnectionEventArgs connectionInformation) {
            Console.WriteLine("Arduino disconnected");
        }

        public static void Main(string[] args) {
            new Program();

            while (true) { }
        }
    }
}
