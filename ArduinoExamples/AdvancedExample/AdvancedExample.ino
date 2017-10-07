#define BAUD_RATE 9600
#define SERIAL_READ_DATA_LENGTH = 4

volatile bool connection = false;
byte handshake[] = { 16, 4, 64, 32 };
int handshakeLenght = sizeof(handshake);

void setup() {
    Serial.begin(BAUD_RATE);
    pinMode(LED_BUILTIN, OUTPUT);
}

void loop() {
    while (!connection) {
        //Turn built-in led off if connection is false
        digitalWrite(LED_BUILTIN, LOW);
        
        byte readData[handshakeLenght];

        while (Serial.available() < handshakeLenght) { }

        for (int i = 0; i < handshakeLenght; i++)
            readData[i] = Serial.read();

        if (arrayComparation(readData, handshake, handshakeLenght)) {
            connection = true;
            Serial.write(handshake, sizeof(handshake));
        }
    }

    //Turn built-in led on if connection is true
    digitalWrite(LED_BUILTIN, HIGH);
    
    if (Serial.available() > SERIAL_READ_DATA_LENGTH) {
        int connectionStatus = Serial.read();
        int randomData[4];

        if (connectionStatus == 0)
            connection = false;

        for (int i = 0; i < sizeof(randomData); i++)
            randomData[i] = Serial.read();

        //do whatever you want with data readed from pc
    }
}

bool arrayComparation(byte *a, byte *b, int arraySize) {
    for (int i = 0; i < arraySize; i++)
        if (a[i] != b[i])
            return false;

    return true;
}
