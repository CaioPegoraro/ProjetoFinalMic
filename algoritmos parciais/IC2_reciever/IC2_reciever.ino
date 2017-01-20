//Receptor secundário
//Modelo: Arduino MEGA
//Objetivo: segundo receptor, recebe um  comando via IC2 de um arduino uno, é responsável pelo controle de velocidade
//          dos motores do vant.

#include <Wire.h>

#define SLAVE_ADDRESS 0x60

byte x = 0x00;

typedef struct {
  int cmd;
  int valor;
} pacote;
int data;
pacote dados;

void setup()
{
  Wire.begin(SLAVE_ADDRESS);
  Wire.onReceive(receiveEvent);
  Wire.onRequest(requestEvent);

  Serial.begin(9600);
}

void loop()
{
  delay(100);
}

void requestEvent()
{
  Serial.print("Request from Master. Sending: ");
  Serial.print(x, HEX);
  Serial.print("\n");

  Wire.write(x);
}

void receiveEvent(int data)
{
  if (Wire.available() != 0)
  {
    for (int i = 0; i < bytes; i++)
    {
      data = Wire.read();
      Serial.print("Received: ");
      Serial.print(data);
      Serial.print("\n");
    }
  }
}
