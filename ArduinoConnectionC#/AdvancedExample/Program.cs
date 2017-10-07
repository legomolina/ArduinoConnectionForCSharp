/*
 * This example demonstrates you how to use the ArduinoConnection while sending data to an Arduino board and how to say to Arduino that the program is closed. 
 */

using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace AdvancedExample {
    public class Program {
        private static bool isClosing = false;

        private byte[] handshake;
        private static ArduinoConnection.ArduinoConnection connection;
        private static bool arduinoConnected;

        //Give public visibility for other classes
        public bool ArduinoConnected {
            get => arduinoConnected;
        }

        public Program() {
            //Create custom handshake and connection
            handshake = new byte[] { 16, 4, 64, 32 };
            connection = new ArduinoConnection.ArduinoConnection(handshake);

            //Attach connected and disconnected events from connection
            connection.ArduinoConnected += Connection_ArduinoConnected;
            connection.ArduinoDisconnected += Connection_ArduinoDisconnected;

            //Start the connection
            connection.Start();

            while(true) {
                SendData(1);

                //Don't worry about sleeping this thread, connection has its own thread
                Thread.Sleep(1000);
            }
        }

        private static void SendData(byte programStatus) {
            //Send custom data to Arduino Board (this case it's just random bytes)
            byte[] dataToSend = new byte[] { programStatus, RandomBytes(), RandomBytes(), RandomBytes(), RandomBytes() };

            //Check if arduino is connected or it maybe throw an exception if it is disconnected
            if (arduinoConnected)
                connection.ArduinoPort.Write(dataToSend, 0, dataToSend.Length);
        }

        private static byte RandomBytes() {
            return Convert.ToByte(new Random().Next(0, 50));
        }

        private void Connection_ArduinoConnected(object connection, ArduinoConnection.ArduinoConnection.ConnectionEventArgs connectionInformation) {
            //Set connection to true
            arduinoConnected = true;
            Console.WriteLine("Arduino connected!"); //Debug
        }

        private void Connection_ArduinoDisconnected(object connection, ArduinoConnection.ArduinoConnection.ConnectionEventArgs connectionInformation) {
            //Set connection to false
            arduinoConnected = false;
            Console.WriteLine("Arduino disconnected"); //Debug
        }

        public static void Main(string[] args) {
            new Program();

            //Check for console closing event
            SetConsoleCtrlHandler(new HandlerRoutine(ConsoleCtrlCheck), true);

            while (!isClosing) ;
        }

        /* This code is for handle the console close event */
        private static bool ConsoleCtrlCheck(CtrlTypes ctrlType) {
            switch(ctrlType) {
                case CtrlTypes.CTRL_CLOSE_EVENT:
                    //I call SendData with the status 0
                    SendData(0);
                    isClosing = true;
                    break;
            }

            return true;
        }

        [DllImport("Kernel32.dll")]
        public static extern bool SetConsoleCtrlHandler(HandlerRoutine Handler, bool Add);

        public delegate bool HandlerRoutine(CtrlTypes CtrlType);

        public enum CtrlTypes {
            CTRL_C_EVENT = 0,
            CTRL_BREAK_EVENT,
            CTRL_CLOSE_EVENT,
            CTRL_LOGOFF_EVENT = 5,
            CTRL_SHUTDOWN_EVENT
        }
    }
}
