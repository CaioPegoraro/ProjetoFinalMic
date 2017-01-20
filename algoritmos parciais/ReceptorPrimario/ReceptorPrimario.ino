//Receptor primário
//Modelo: Arduino UNO
//Objetivo: primeiro receptor, está conectado ao receptor wireless que é capaz
//de receber e enviar comandos, tem uma rotina de checagem por instruções
//do emissor, em caso positivo, envia as mesmas via IC2 para o receptor secundário.

#include <SPI.h>
#include <nRF24L01.h>
#include <RF24.h>
#include "pacote.h"

//canais de comunicação sem fio
//como ele pode alternar entre emissor
//e receptor temos que indicar o endereço
//para leitura/escrita
//no emissor primario os valores estao invertidos
//pois sempre um for emissor o outro eh o receptor
RF24 radio(48, 49);
const byte rxAddr[6] = "00002";
const byte wxAddr[6] = "00001";

//Configuração para comunicação IC2 com o arduino mega
#include <Wire.h>
#define SLAVE_ADDRESS 0x60

pacote dados;
byte buff[2];

//Variáveis de hardware
int pin_buzzer = 10;
int status_buzzer = 0;//inicialmente desligado
unsigned long previousMillis = 0;
const long interval = 700;

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


void loop()
{

  //Descrição do loop: Mantém uma checagem contínua pelo sinal de wireless vindo do emissor primário,
  //                   ao receber um comando checa o tipo, caso seja simples executa a tarefa ou transmite
  //                   para o receptor secundário, caso seja composto, produz os dados necessários e
  //                   faz o envio de retorno ao emissor primário
  if (radio.available())
  {
    radio.read(&dados, sizeof(dados));
    int tmp = dados.cmd;
    //dados.cmd = dados.valor;
    //dados.valor = tmp;

    Serial.print("\n cmd: ");
    Serial.println(dados.cmd);
    //Serial.print("\n valor: ");
    //Serial.println(dados.valor);

    //Serial.println("Rprimário recebido: " + dados.cmd);
    //Serial.println("valor: " + dados.valor);
    //Serial.println(dados.cmd);
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
