//Emissor primário
//Modelo: Arduino UNO
//Objetivo: Recebe um comando via USB do cliente (pc) e envia via wireless para o receptor primário no vant.

//bibliotecas utilizadas
#include <SPI.h>
#include <nRF24L01.h>
#include <RF24.h>

//pacote.h possui a declaração da estrutura da mensagem enviada/recebida
#include "pacote.h"

//inicialização do radio controle, caso fosse um arduino mega os pinos seriam: radio(48, 49);
RF24 radio(7, 8);

//Declaração dos canais (uma espécie de pipe) para leitura e escrita
//OBS: no receptor os endereços serão invertidos (para formar a dupla de leitura e escrita
const byte rxAddr[6] = "00001";
const byte wxAddr[6] = "00002";

//variáveis locais utilizadas

//a estrutura para armazenar um comando recebido
pacote dados;

//um buffer temporário que recebe um inteiro via USB
int buff;

//utilizado como controlador para
int done;

//declaração dos LEDs de status do emissor
#define PIN_STATUS 1 //LED 3
#define PIN_CON 2    //LED 2
#define PIN_MSG 3    //LED 1

void setup() {

  //Comunicação serial com timeout (para ajustar a velocidade de açãoo
  Serial.begin(9600);
  Serial.setTimeout(50);

  //inicialização da comunicação sem fio
  radio.begin();
  radio.setRetries(15, 15);
  radio.openWritingPipe(wxAddr);
  radio.openReadingPipe(0, rxAddr);
  radio.stopListening();

  //Leds de controle
  pinMode(PIN_STATUS, OUTPUT);//luz esq: ON/OFF
  pinMode(PIN_CON, OUTPUT);//meio: Conexao com drone
  pinMode(PIN_MSG, OUTPUT);//dir: enviar/receber comando

  digitalWrite(PIN_STATUS, HIGH); //dispositivo está ligado
}

void loop() {

  //Descrição do loop: O emissor fica checando continuamente a porta serial na espera de um comando enviado pelo
  //                   operador C#, quando recebe um comando (inteiro, de 2 bytes) ele envia para o receptor
  //                   primário e também uma resposta para o cliente c# (para casos em que é necessário receber
  //                   algum valor do vant.

  dados.cmd = 33;
  dados.valor = 20;

  radio.write(&dados, sizeof(dados));

  Serial.println(dados.cmd);
    
  delay(1000);
}
