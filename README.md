# # ProjetoFinalMic
Repositório para o projeto final do laboratório de microcontroladores e aplicações 2016/2

== INTEGRANTES ==

Caio Cesar Almeida Pegoraro RA:489000

Silvio Custódio da Silva Júnior RA:595217

Ivan luiz de Oliveira Perazzoli RA:400521


== PROPOSTA ==

O projeto propoe a construção de um veiculo terrestre controlado remotamente.
O controle será feito por um terminal (PC) utilizando um hardware de comunicação.
Havera um receptor no veículo para troca de mensagens e dados.
O "rover" terá sensores de leitura ambiente (luminosidade, ultrasom, etc), os dados serão enviados para a central e exibidos em uma tela de comando, pelos quais será possível controlar a movimentação e outras ações.
Adicionalmente espera-se equipar o veículo com uma camera capaz de enviar fotos remotamente.

== LISTA DE TAREFAS -> Semana do dia 30/01 ==
- Montar o projeto do painel de controle;
- Sensor de temperatura funcionou corretamente;
- Sensor de distância ultrasom funcionou mas não fez a leitura corretamente;
- Escrever o documento do trabalho;
   * Descrever os instrumentos utilizados (Arduinos, chave (liga/desl), motores DC (rodinhas), Motor dos braços, ponte H;
   * Descrever os componentes utilizando uma imagem do Rover;
   * Objetivo;
   * Funcionamento;
   * Testar novamente o sensor ultrasom;

 == INTRODUÇÃO == 
![alt tag](https://github.com/CaioPegoraro/ProjetoFinalMic/blob/master/imagens/arquitetura_comunicacao.PNG)
 
 Figura 1: Arquitetura do sistema de hardware do VANT.
 
De acordo com a Figura 1 temos a arquitetura geral do projeto, os itens enumerados cor-
respondem a cada dispositivo encarregado de uma tarefa especíﬁca:

(1) Painel de controle: Um cliente escrito em C executando em um ambiente "Windows", pode
tanto exibir quando enviar comandos.

(2): Emissor primário: Uma placa "Arduino Uno"conectada a um componente transceiver, se
comunica via USB com o PC para receber e transmitir os comandos para o Rover.

(3) Receptor primário: Outro "Arduino Uno"conectado ao outro transceiver, tem uma rotina
que veriﬁca se algum sinal foi recebido e também pode enviar dados de volta ou encaminhar
para o receptor secundário.

(4) Receptor secundário: Um "Arduino Mega"que é utilizado para controle dos motores e dos sensores, pode receber comandos via IC2 do receptor primário para alteração da
aceleração dos motores e posição dos braços mecânicos.


----------Inserir foto dos motores e dos arduínos---------------





