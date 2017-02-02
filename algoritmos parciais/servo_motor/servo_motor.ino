h #include <Servo.h>

#define SERVO1 5
#define SERVO2 4
#define SERVO3 3
#define SERVO4 2

Servo servo1;
Servo servo2;
Servo servo3;
Servo servo4;

int pos; // Posição Servo

int numero;
void setup ()
{
  servo1.attach(SERVO1);
  servo2.attach(SERVO2);
  servo3.attach(SERVO3);
  servo4.attach(SERVO4);

  Serial.begin(9600);
  //servo1.write(90); // Inicia motor posição zero
  //servo2.write(90);
  //servo3.write(90);
  //servo4.write(90);
}

void loop()
{

  if (Serial.available() > 0)

  {
    numero = Serial.parseInt();
    Serial.print(numero);
    servo4.write(numero);
  }

  //while (1) {
    servo3.write(90);
    delay(1000);
    servo3.write(50);
    delay(1000);
  //}

}
