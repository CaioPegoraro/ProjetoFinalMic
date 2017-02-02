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

  if (Serial.available() > 0) {
    //Inicio: leitura do valor enviado pelo pc via usb
    digitalWrite(PIN_MSG, HIGH); //LED: com aceso

    byte buff[2];
    Serial.readBytes(buff, 2);

    dados.cmd = (int)buff[0];
    dados.valor = (int)buff[1];

    //Serial.print("Comando enviado: ");
    //Serial.println(dados.cmd);
    //Serial.print("Valor associado: ");
    //Serial.println(dados.valor);

    //envia os dados via conexão sem fio para o receptor primário
    radio.write(&dados, sizeof(dados));
    digitalWrite(PIN_MSG, LOW);
    //Fim: leitura do valor enviado pelo pc via usb

    //avaliar tipo de comando: simples ou composto
    //simples: um comando de uma via, apenas é enviado e pronto
    //composto: é enviado mas precisa de uma resposta (como obter o nível da bateria atual)

    //por questões técnicas e de otimização foi considerado o intervalo númerico para
    //determinar se um comando é simples ou composto (uma faixa de valores para cada).
    //dessa forma não seria necessário adicionar mais um componente na estrutura da mensagem
    //permanecendo assim nos 2 bytes (ou no caso 1 inteiro).

    //se o valor for um comando composto, aguarda uma resposta do vant:
    if (dados.cmd >= 125) {
      //Serial.println("");
      //Serial.println("Aguardando resposta \n");
      radio.startListening();
      for (int i = 0; i < 500; i++) {
        done = radio.available();
        if (done) {
          break;
        }
        delay(4);
      }

      if (done) {
        //recebeu uma resposta do vant, agora precisa tratar de acordo com o comando original
        digitalWrite(PIN_CON, HIGH);
        radio.read(&dados, sizeof(dados));

        switch (dados.cmd) {
          case 125: //estabelecer conexao
            //Serial.println("");
            //Serial.println("== Retorno ==");
            //Serial.println("Conexao estabelecida!, 125, 1");
            //Serial.println(125);
            //Serial.println(1);

            dados.valor = 1;
            Serial.println(dados.cmd * 100000 + dados.valor);
            break;

          case 126: //leitura valor da bateria
            Serial.println(dados.cmd * 100000 + dados.valor);
            break;
        }
      }
      else {
        //não foi recebido uma resposta durante a analise
        //Serial.println("");
        //Serial.println("== Retorno ==");
        //Serial.println("Nenhum dado recebido, verificar alcance!, 125, 0");

        dados.valor = 0;
        Serial.println(dados.cmd * 100000 + dados.valor);

        digitalWrite(PIN_CON, LOW); //conexao deverá ser reestabelecida
      }
      radio.stopListening();
    }
  }
}
