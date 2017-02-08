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


1 - INTRODUÇÃO

  A proposta foi a construção de um "Rover" controlado remotamente utilizando um microcontrolador de baixo custo com foco na aplicação em sistemas de detecção de dados. A execução envolveu uma série de conhecimentos multidisciplinares (entre disciplinas de computação, física e engenharia).
  O projeto foi inteiramente estruturado em cima do microcontrolador Arduino, isto é, todo controle de execução e comunicação é executado por um hardware arduino (menos no caso do cliente que executa em um PC). Ao todo foram utilizadas três Arduinos: dois do tipo "Arduino Uno"(um no emissor primário e outro como receptor secundário), o outro é do tipo "Arduino Mega"(funcionando como receptor secundário) como mostra a Figura 1 abaixo.
  
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

  A programação nas placas é a mesma (utilizando a linguagem C) e não há diferenças significativas de hardware (considerando o propósito de uso nesse projeto, no geral o MEGA é superior), foram testadas duas alternativas antes de definir esse modelo final: a primeira era utilizar um chip Attiny85 para função de comunicação (o chip daria um processamento dedicado a recepção de dados), como o chip é pequeno seria possível economizar espaço e peso, mas houve incompatibilidade com o sensor de rádio frequência em conjunto com a comunicação IC2 (futuramente será reavaliado essa solução); a segunda tentativa foi utilizar um "Arduino Due", principalmente pela característica de processamento paralelo, na prática teriam várias rotinas executando simultaneamente para garantir as tarefas de tempo real, mas novamente houve incompatibilidade com algumas bibliotecas, o que impossibilitou o uso.






