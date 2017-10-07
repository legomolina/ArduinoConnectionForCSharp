#define BAUD_RATE 9600

volatile bool connection = false;
byte handshake[] = { 16, 4, 64, 32 };
int handshakeLenght = sizeof(handshake);

void setup() {
    Serial.begin(BAUD_RATE);
    pinMode(LED_BUILTIN, OUTPUT);
}

void loop() {
    while (!connection) {
        byte readData[handshakeLenght];

        while (Serial.available() < handshakeLenght) { }

        for (int i = 0; i < handshakeLenght; i++)
            readData[i] = Serial.read();

        if (arrayComparation(readData, handshake, handshakeLenght)) {
            connection = true;
            Serial.write(handshake, sizeof(handshake));
        }
    }

    digitalWrite(LED_BUILTIN, HIGH);
}

bool arrayComparation(byte *a, byte *b, int arraySize) {
    for (int i = 0; i < arraySize; i++)
        if (a[i] != b[i])
            return false;

    return true;
}
