#include <Wire.h>
#include <Servo.h>

#include "I2Cdev.h"
#include "pacote.h"
#include "MPU6050.h"

//endereço para interface de comunicação IC2
#define RECEPTOR_S_ENDERECO 0x60

//==estrutura de dados para receber informacoes do outro arduino===
pacote dados;
String data = "";

//=== Variáveis de controle dos motores DC ===
//Controle por pondeH: IN1 e IN2 defidem o sentido de rotação, ENA eh o pino de controle de velocidade
//motor A
int IN1 = 47 ;
int IN2 = 51 ;
int ENA = 7;

//motor B
int IN3 = 49;
int IN4 = 53;
int ENB = 6;

int velocidade_dc = 80; //intensidade inicial da velocidade dos motores DC
//=== FIM Variáveis de controle dos motores DC ===

//=== Variáveis de controle servo motores ===
#define SERVO1 5
#define SERVO2 4
#define SERVO3 3
#define SERVO4 2

Servo servo1;
Servo servo2;
Servo servo3;
Servo servo4;


//=== FIM Variáveis de controle servo motores ===

//=== Variaveis Auxiliares ==
int intervalo_tmp = 3000; //tempo em ms para executao de ações, por exemplo, se acionar os motores dc será feito por 3s e depois volta ao respouso.
int led_ctrl = 44; //led de controle auxiliar
int angulo_servo[4];
int pino_Laser = 9;

//Flags de operação
int flag_dc = 0;
int flag_servo = 0;
int flag_led = 0;
int flag_tmp = 0; //sensor temperatura
int flag_laser = 0;

//=== FIM Variaveis Auxiliares ==


//=== FUNCOES AUXILIARES ===//

//MOTORES DC
void motor_dc_ambos(int sentido) {
  if (sentido == 1) { //frente
    digitalWrite(IN1, HIGH);
    digitalWrite(IN2, LOW);

    digitalWrite(IN3, HIGH);
    digitalWrite(IN4, LOW);
  }
  else { //tras
    digitalWrite(IN1, LOW);
    digitalWrite(IN2, HIGH);

    digitalWrite(IN3, LOW);
    digitalWrite(IN4, HIGH);
  }
  analogWrite(ENA, velocidade_dc);
  analogWrite(ENB, velocidade_dc);
}

void motor_dc_Dir() {
  digitalWrite(IN1, HIGH);
  digitalWrite(IN2, LOW);

  digitalWrite(IN3, LOW);
  digitalWrite(IN4, HIGH);

  analogWrite(ENA, velocidade_dc);
  analogWrite(ENB, velocidade_dc);
}

void motor_dc_Esq() {
  digitalWrite(IN1, LOW);
  digitalWrite(IN2, HIGH);

  digitalWrite(IN3, HIGH);
  digitalWrite(IN4, LOW);

  analogWrite(ENA, velocidade_dc);
  analogWrite(ENB, velocidade_dc);
}

void motor_dc_Stop() {
  analogWrite(ENA, 0);
  analogWrite(ENB, 0);
}
//FIM - MOTORES DC

//A função trata de eventos recebidos do mestre (pela hierarquia IC2)
//quando há um envio por parte dele o escravo (esse receptor)
//desvia para tratamento do dado recebido, que consiste em
//ativar um flag (como se fosse uma senha de banco) para o loop
//executar uma operação unicamente.
//e tb para salvar em uma variavel o valor do dado associado.
//a função foi previamente configurada no setup para esse tipo
//de tratamento.

void receiveEvent(int howMany) {
  data = "";
  while (Wire.available()) { //enqnto estiver disponivel bytes
    data += (char)Wire.read();
  }
  int valor_data = data.toInt();
  Serial.println(valor_data);
  if ((valor_data == 1700) || (valor_data == 1600)) {
    dados.cmd = valor_data/100;
    dados.valor = 0;
  }
  else {
    dados.cmd = valor_data / 1000;
    dados.valor = valor_data - dados.cmd * 1000;
  }

  //ja recebeu e separou os dados do comando

  if (dados.cmd <= 4) {
    flag_servo = 1; //indica flag de operacao nos servo motores
  }
  if ((dados.cmd >= 5) && (dados.cmd <= 9)) {
    flag_dc = 1; //indica flag de operação nos motores dc
  }
  if ((dados.cmd == 16) || (dados.cmd == 17)) {
    flag_laser = 1;
  }

  Serial.println(dados.cmd);
  Serial.println(dados.valor);
}

//=== FIM FUNCOES AUXILIARES ==//

void setup() {

  //Motores DC
  pinMode(IN1, OUTPUT);
  pinMode(IN2, OUTPUT);
  pinMode(IN3, OUTPUT);
  pinMode(IN4, OUTPUT);
  pinMode(ENA, OUTPUT);
  pinMode(ENB, OUTPUT);
  //FIM Motores DC

  pinMode(led_ctrl, OUTPUT);
  pinMode(pino_Laser, OUTPUT);

  //Servo motores
  servo1.attach(SERVO1);
  servo2.attach(SERVO2);
  servo3.attach(SERVO3);
  servo4.attach(SERVO4);
  //FIM Servo motores

  //Configuração comunicação IC2:
  Wire.begin(RECEPTOR_S_ENDERECO);
  Wire.onReceive(receiveEvent);

  Serial.begin(9600);
  Serial.setTimeout(50);

  //Variaveis de controle inicialização:
  dados.valor = 0;

}

void loop() {
  /*
    int flag_dc = 0;
    int flag_servo = 0;
    int flag_led = 0;

    int flag_tmp = 0; //sensor temperatura
    int flag_laser = 0;
  */

  //=OPERACAO SERVO MOTORES
  if (flag_servo == 1) {
    flag_servo = 0;
    //rotina de tratamento de operação nos servo motores
    switch (dados.cmd) {

      case 1: //servo1
        servo1.write(dados.valor);
        break;

      case 2: //servo2
        servo2.write(dados.valor);
        break;

      case 3: //servo3
        servo3.write(dados.valor);
        break;

      case 4: //servo4
        servo4.write(dados.valor);
        break;
    }
  }

  else if (flag_dc == 1) {
    flag_dc = 0;
    //rotina de tratamento de operação nos motores dc

    switch (dados.cmd) {

      case 5://motor dc para frente
        digitalWrite(IN1, HIGH);
        digitalWrite(IN2, LOW);

        digitalWrite(IN3, HIGH);
        digitalWrite(IN4, LOW);

        analogWrite(ENA, dados.valor);
        analogWrite(ENB, dados.valor);
        break;

      case 6: //motor dc pra tras
        digitalWrite(IN1, LOW);
        digitalWrite(IN2, HIGH);

        digitalWrite(IN3, LOW);
        digitalWrite(IN4, HIGH);

        analogWrite(ENA, dados.valor);
        analogWrite(ENB, dados.valor);
        break;

      case 7: //parar motores
        analogWrite(ENA, 0);
        analogWrite(ENB, 0);
        digitalWrite(IN1, LOW);
        digitalWrite(IN2, LOW);
        digitalWrite(IN3, LOW);
        digitalWrite(IN4, LOW);
        break;

      case 8://virar pra direita
        digitalWrite(IN1, HIGH);
        digitalWrite(IN2, LOW);

        digitalWrite(IN3, HIGH);
        digitalWrite(IN4, LOW);

        analogWrite(ENA, dados.valor);
        //analogWrite(ENB, dados.valor);

        break;

      case 9://virar pra esquerda

        digitalWrite(IN1, HIGH);
        digitalWrite(IN2, LOW);

        digitalWrite(IN3, HIGH);
        digitalWrite(IN4, LOW);

        //analogWrite(ENA, dados.valor);
        analogWrite(ENB, dados.valor);
        break;

    }
  }
  else if (flag_laser == 1) {
    flag_laser = 0;

    if (dados.cmd == 17) {
      digitalWrite(pino_Laser, HIGH);
      digitalWrite(led_ctrl, HIGH);
    }
    else if (dados.cmd == 16) {
      digitalWrite(pino_Laser, LOW);
      digitalWrite(led_ctrl, LOW);
    }
  }
  //FIM OPERACAO SERVO MOTORES

}
