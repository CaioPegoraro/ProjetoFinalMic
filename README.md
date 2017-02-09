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



1. INTRODUÇÃO


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


----------Inserir foto dos motores e dos arduínos ???---------------

  A programação nas placas é a mesma (utilizando a linguagem C) e não há diferenças significativas de hardware (considerando o propósito de uso nesse projeto, no geral o MEGA é superior), foram testadas duas alternativas antes de definir esse modelo final: a primeira era utilizar um chip Attiny85 para função de comunicação (o chip daria um processamento dedicado a recepção de dados), como o chip é pequeno seria possível economizar espaço e peso, mas houve incompatibilidade com o sensor de rádio frequência em conjunto com a comunicação IC2 (futuramente será reavaliado essa solução); a segunda tentativa foi utilizar um "Arduino Due", principalmente pela característica de processamento paralelo, na prática teriam várias rotinas executando simultaneamente para garantir as tarefas de tempo real, mas novamente houve incompatibilidade com algumas bibliotecas, o que impossibilitou o uso.

  1.2 Software e configuração do ambiente
Foram utilizados dois softwares principais para o desenvolvimento do projeto:
- Visual Studio 2015
A IDE do VS2015 foi utilizada para o desenvolvimento da interface de comando (em C) representada na Figura 2, que executa no ambiente windows, para estudantes da UFSCar pode ser obtido através do Dreamspark:http://www.dc.ufscar.br/dreamspark.
- Arduino IDE 1.6.7
A Arduino IDE foi utilizada para o desenvolvimento dos algoritmos que executam nas três placas arduinos (utilizando C), ela pode ser obtida através do endereço: https://www.arduino.cc/en/Main/Software.

Ambos softwares foram executados e um ambiente Windows (Windows 7 versão de 64 bits).


  1.3 Componentes e periféricos

A lista dos componentes utilizados pode ser vista a seguir:

1. 2x: Módulo transreceptor: NRF24L01+ Wireless 2,4ghz;
2. Acelerômetro: MPU6050;
3. Alarme: Buzzer indicador de Tensão 1-8s Lipo;
4. Regulador de tensão: Lm2596;
5. 4x: Controlador de velocidade: ESC 30A;
6. 4x: Motores: brushless 2212 1000kv;
7. Carregador: Lipo 2s e 3s Hobbyking;
8. Cabo de alimentação: Xt60 to 4 X 3.5mm Bullet;
9. Bateria: Lipo Zippy 2200mah 3s 40/50c 11.1v;
10. Buzzer: Oscilador interno;
11. Bateria: 9v (alimentação do circuito no VANT);
12. Sensor de Temperatura: DS18B20;
13. 4x:Rodinhas;
14. Componentes gerais: Resistores, LED’s, estanho, placa pcb,tubos termoretráteis, pinos e conectores, fios e chaves.


2. Metodologia


A metodologia aplicada partiu da análise individual para, posteriormente, avaliar uma interação em conjunto, isto é, primeiro foram avaliados os sensores e componentes individualmente e, uma vez que o funcionamento estivesse dominado, o mesmo foi integrado ao conjunto operacional.

Essa estratégia foi utilizada para todos os sensores (até por questão de manter uma abstração no código, então saber como o sensor funciona permite criar funções em um nível de abstração maior para operar cada componente de maneira mais simples).
A escolha dos micro controladores foi devido ao suporte à linguagem C/C++ e compatibilidade com uma série de sensores e bibliotecas já existentes. 

Na construção da estrutura do Rover foram avaliados pontos como resistência e peso, o polímero se mostrou uma boa saída já que é facilmente manipulado (é facilmente cortado e furado) e também possibilita um maior nível de personalização, já que o design pode ser alterado da maneira que for necessário para cada caso; caso fosse utilizado uma estrutura de plástico pronta ficaria mais restrito a esse tipo de expansão e adição de hardware.

Os dois Arduinos que são acoplados no VANT se comunicam por uma interface IC2 e outro Arduino se comunica via USB com o PC, então antes de uma integração foi necessário realizar testes de envio de pacotes entre essas estruturas, o uso de dois Arduinos no VANT foi justamente para priorizar o recebimento de um pacote de dados.



3. Resultados e Discussão

 
 3.1 Painel de controle


O painel de controle é o caminho pelo qual executamos o input dos dados, em projetos convencionais é normalmente utilizado um rádio controle mas nesse projeto foi adotado um sistema diferente: um software mapeia comandos que são enviados para um dispositivo e este envia novamente por uma conexão sem fio os dados para o receptor (que está no Rover).

Essa abordagem foi seguida porque permite uma maior versatilidade do projeto, é possível personalizar e complementar as informações que são exibidas na tela do operador, esse tipo de programação ainda permite controlar outras aplicações sem fio de maneira genérica e prática.

Como se trata de uma disciplina acadêmica na área da Computação, utilizar um rádio controle seria uma perda deoportunidade de pôr em prática conceitos de controle e automação, além de que seria perdido também a oportunidade de experimentar a interação e comunicação entre plataformas distintas.

Focando no painel de controle: foi utilizado o Visual Studio 2015 para o desenvolvimento (por consequencia o software é feito para uso em sistemas windows) em linguagem C.

O projeto poderia ser desenvolvido em outra linguagem ou plataforma, mas foi adotado essa por conta de uma maior familiaridade com a tecnologia empregada.

Os elementos em destaque na Figura 2 são partes que devem ser explicadas (algumas partes deixadas sem a marcação tratam de itens opcionais ou são replicações de outros implementados).

![alt tag](https://github.com/CaioPegoraro/ProjetoFinalMic/blob/master/imagens/painel%20de%20controle.png)

                   Figura 2: Painel de controle do Rover utilizando Visual Studio 2015.
                   Colocar as marcações na imagem!!!

1. Botões de controle: são utilizados para acionar/desativar alguma função, todos enviam uma estrutura de dados que é utilizado pelo Rover (o detalhe do código e a lista de comandos estarão em a seguir).

2. Controle de velocidade: os comandos permitem o envio de um dado, nesse caso o controlador pode enviar um valor que será utilizado para determinar a velocidade de cada motor.

3. Controle do braço mecânico: os comandos permitem o envio de um dado, nesse caso o controlador pode enviar um valor que será utilizado para determinar a velocidade de cada motor.

4. Prompt: é uma espécie de prompt que exibe um histórico a cada comando enviado, quando recebe algum valor também é exibido nessa área.

5. Serial: o software se conecta ao emissor (um arduino ligado à USB) pela porta serial, da mesma forma que se utiliza essa porta para carregar um código na placa é possível conectar para comunicação de dados.

6. Componentes: os componentes servem para agilizar algumas configurações, o SerialPort serve para suporte a conexão serial (USB) para comunicação com o arduino Uno (emissor), os do tipo Timer são utilizados para programar algumas execuções automáticas a cada certo intervalo de tempo (no caso para checar as portas seriais disponíveis ou para enviar comandos periódicos ao Rover).

7. Display: Utilizado para informar o valor de temperatura e funcionamento do sistema em forma de texto.


   
   3.2 Porta Serial

