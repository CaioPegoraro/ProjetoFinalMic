//Receptor primário
//Modelo: Arduino UNO
//Objetivo: primeiro receptor, está conectado ao receptor wireless que é capaz
//de receber e enviar comandos, tem uma rotina de checagem por instruções
//do emissor, em caso positivo, envia as mesmas via IC2 para o receptor secundário.

#include <SPI.h>
#include <nRF24L01.h>
#include <RF24.h>
#include "pacote.h"
#include "OneWire.h"

#include <Wire.h>
#include <LiquidCrystal_I2C.h>

// Inicializa o display no endereco 0x27
LiquidCrystal_I2C lcd(0x27, 2, 1, 0, 4, 5, 6, 7, 3, POSITIVE);

//canais de comunicação sem fio
//como ele pode alternar entre emissor
//e receptor temos que indicar o endereço
//para leitura/escrita
//no emissor primario os valores estao invertidos
//pois sempre um for emissor o outro eh o receptor
RF24 radio(7, 8);
const byte rxAddr[6] = "00002";
const byte wxAddr[6] = "00001";

//Configuração para comunicação IC2 com o arduino mega
#define SLAVE_ADDRESS 0x60

pacote dados;
byte buff[2];

//Variáveis de hardware
int pin_buzzer = 10;
int status_buzzer = 0;//inicialmente desligado
unsigned long previousMillis = 0;
const long interval = 700;

//=== Variáveis de controle do sensor de temperatura ===//
byte i;
byte present = 0;
byte type_s;
byte data_tmp[12];
byte addr[8];
float celsius, fahrenheit;
OneWire  sensor_temp(6); //sensor de temperatura na porta pwm 8
float temp_atual = 0;

//=== FIM Variáveis de controle do sensor de temperatura ===//

//== Funcao de operacao do sensor de temperatura ==//

void ler_temperatura() {
  if ( !sensor_temp.search(addr)) {
    sensor_temp.reset_search();
    delay(250);
    return;
  }

  sensor_temp.reset();
  sensor_temp.select(addr);
  sensor_temp.write(0x44);

  delay(1000);

  present = sensor_temp.reset();
  sensor_temp.select(addr);
  sensor_temp.write(0xBE);


  for ( i = 0; i < 9; i++) {
    data_tmp[i] = sensor_temp.read();
    //Serial.print(data[i], HEX);
    //Serial.print(" ");
  }

  //converte o dado para temperatura
  int16_t raw = (data_tmp[1] << 8) | data_tmp[0];
  if (type_s) {
    raw = raw << 3;
    if (data_tmp[7] == 0x10) {
      raw = (raw & 0xFFF0) + 12 - data_tmp[6];
    }
  } else {
    byte cfg = (data_tmp[4] & 0x60);
    if (cfg == 0x00) raw = raw & ~7;
    else if (cfg == 0x20) raw = raw & ~3;
    else if (cfg == 0x40) raw = raw & ~1;
  }

  celsius = (float)raw / 16.0;
  fahrenheit = celsius * 1.8 + 32.0;
  /*
    Serial.print("  Temperature = ");
    Serial.print(celsius);
    Serial.print(" Celsius, ");
    Serial.print(fahrenheit);
    Serial.println(" Fahrenheit");
  */

  //atualiza o valor da temperatura atual.
  temp_atual = celsius;
}
//== FIM Funcao de operacao do sensor de temperatura ==//

//controle do lcd
int flag_lcd = 0;
int linha = 0;
int coluna = 0;

//leitura da bateria
int sensorValue;
int led_conexao = 4;

void setup()
{
  //IC2
  Wire.begin();

  //Serial
  while (!Serial);
  Serial.begin(9600);

  //Wireless
  //inicia as configurações para funcionar como
  //um receptor
  radio.begin();
  radio.openWritingPipe(wxAddr);
  radio.openReadingPipe(0, rxAddr);
  radio.startListening();

  //Hardware
  pinMode(led_conexao, OUTPUT);
  pinMode(pin_buzzer, OUTPUT);
  pinMode(A0, INPUT);

  //lcd
  lcd.begin (16, 2);

  lcd.setBacklight(HIGH);
  //lcd.setCursor(0,0);
  //lcd.print("Rover online!");
}

//Função para enviar uma resposta para o emissor primário
//eh feito a troca para função de "emissor", monta-se
//o pacote de dados e eh feito algumas tentativas de
//envio, dado que o emissor original também precisa
//de um tempo para trocar de contexto e tornar um receptor.
void enviaMsg(int cmd, int valor) {
  //Serial.println("Enviando resposta");
  radio.stopListening();
  dados.cmd = cmd;
  dados.valor = valor;
  //tentativas de enviar ao emissor original
  for (int i = 0; i < 10; i++) {
    radio.write(&dados, sizeof(dados));
  }
  //delay(1);
  radio.startListening();
}

void requestEvent() {
  //funcao originalmente para tratar de requisicoes
  //do slave, mas nao foi utilizada.
};

void buzzer() {
  //Serial.println("toca o som dj!!");
  tone(pin_buzzer, 4000, 300); //freq = 4000hz
  //delay(500);
}


//Função para enviar uma mensagem para o arduino mega
//(responsável pelo controle dos motores)
void enviaIC2() {
  Serial.println("enviando");
  //constroi-se o pacote de dados montando um inteiro
  //de 2 bytes, sendo cada byte formado pelo comando
  //e por um valor associado:
  int valor_pacote = dados.cmd * 1000 + dados.valor;
  //Serial.println(dados.valor);
  //Serial.println(dados.cmd);

  String pacote_ic2 = String(valor_pacote);
  Wire.beginTransmission(SLAVE_ADDRESS);
  for (int x = 0; x < 4; x++) {
    Wire.write(pacote_ic2.charAt(x));
  }
  //Serial.println(pacote_ic2);
  Wire.endTransmission();
}

void loop()
{
  if (status_buzzer == 1) {
    unsigned long currentMillis = millis();
    if (currentMillis - previousMillis >= interval) {
      previousMillis = currentMillis;
      buzzer();
    }
  }

  if (flag_lcd == 1) {
    flag_lcd = 0; //processa e insere o caractere no lcd

    if (dados.valor == 0) {
      lcd.setBacklight(LOW);

      lcd.setCursor(0, 0);
      for (int i = 0; i < 16; i++)
        lcd.print(" ");

      lcd.setCursor(0, 1);
      for (int i = 0; i < 16; i++)
        lcd.print(" ");

      delay(1000);
      lcd.setBacklight(HIGH);
      coluna = 0;
      linha = 0;
    }
    else {
      char c = dados.valor - 32;
      lcd.setCursor(coluna, linha);

      if (dados.valor != 32) //espaço em branco, ignora porque ja vai pular para a prox coluna dps
        lcd.print(c);
      else if (coluna == 0)
        coluna--;

      //lcd.print(dados.valor);
      coluna++;
      if (coluna == 16) {
        coluna = 0;
        linha = 1;
      }
    }
  }

  //Descrição do loop: Mantém uma checagem contínua pelo sinal de wireless vindo do emissor primário,
  //                   ao receber um comando checa o tipo, caso seja simples executa a tarefa ou transmite
  //                   para o receptor secundário, caso seja composto, produz os dados necessários e
  //                   faz o envio de retorno ao emissor primário
  if (radio.available())
  {
    radio.read(&dados, sizeof(dados));
    //int tmp = dados.cmd;
    //dados.cmd = dados.valor;
    //dados.valor = tmp;

    Serial.print("\n cmd: ");
    Serial.println(dados.cmd);
    Serial.print("\n valor: ");
    Serial.println(dados.valor);

    //Serial.println("Rprimário recebido: " + dados.cmd);
    //Serial.println("valor: " + dados.valor);
    //Serial.println(dados.cmd);

    //avalia o tipo do comando
    if (dados.cmd <= 124) { //comandos simples

      switch (dados.cmd) {
        case 14: //Acionamento do buzzer
          status_buzzer = 1;
          break;

        case 15: //Desligamento do buzzer
          status_buzzer = 0;
          break;

        case 1: //Aciona servo1
          enviaIC2();
          break;

        case 2: //Aciona servo2
          enviaIC2();
          break;

        case 3: //Aciona servo3
          enviaIC2();
          break;

        case 4: //Aciona servo4
          enviaIC2();
          break;

        case 5: //Aciona motores DC p/ frete
          enviaIC2();
          break;

        case 6: //Aciona motores DC p/ tras
          enviaIC2();
          break;

        case 7: //Desliga motores
          enviaIC2();
          break;

        case 8: //Aciona motores Dc para giro a direita
          enviaIC2();
          break;

        case 9: //Aciona motores Dc para giro a esquerda
          enviaIC2();
          break;

        case 16: // Desliga laser
          enviaIC2();
          break;

        case 17: //Liga laser
          enviaIC2();
          break;

        case 20: //Envia caractere para o display lcd
          flag_lcd = 1;
          break;
      }
    }
    else { // >=125 comandos compostos

      switch (dados.cmd) {
        case 125: //Conexao inicial
          digitalWrite(led_conexao, HIGH);
          enviaMsg(125, 0);
          break;

        case 126: //Ler carga da bateria
          /*sensorValue = analogRead(A0);
            float voltage = sensorValue * (5.0 / 1023.0);
            voltage = voltage * 100; //ajustar casas decimais para emular um inteiro
            int volt_tmp = voltage;
            enviaMsg(126, volt_tmp);
          */
          break;

        case 127: //leiutra da temperatura
          ler_temperatura();
          enviaMsg(127, temp_atual*100);
          Serial.println(temp_atual);
          break;
      }
    }
    /*
        if (dados.valor == 10) {
          enviaMsg(); //envia uma mensagem de volta ao controlador
        }
        else if (dados.valor == 15) {
          enviaIC2(); //envaminha o comando ao receptor secundário
        }
    */

  }
}
