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

  A programação nas placas é a mesma (utilizando a linguagem C) e não há diferenças significativas de hardware (considerando o propósito de uso nesse projeto, no geral o MEGA é superior), foram testadas duas alternativas antes de definir esse modelo final: a primeira era utilizar um chip Attiny85 para função de comunicação (o chip daria um processamento dedicado a recepção de dados), como o chip é pequeno seria possível economizar espaço e peso, mas houve incompatibilidade com o sensor de rádio frequência em conjunto com a comunicação IC2 (futuramente será reavaliado essa solução); a segunda tentativa foi utilizar um "Arduino Due", principalmente pela característica de processamento paralelo, na prática teriam várias rotinas executando simultaneamente para garantir as tarefas de tempo real, mas novamente houve incompatibilidade com algumas bibliotecas, o que impossibilitou o uso.

    1.1 Software e configuração do ambiente
Foram utilizados dois softwares principais para o desenvolvimento do projeto:
- Visual Studio 2015
A IDE do VS2015 foi utilizada para o desenvolvimento da interface de comando (em C) representada na Figura 2, que executa no ambiente windows, para estudantes da UFSCar pode ser obtido através do Dreamspark:http://www.dc.ufscar.br/dreamspark.
- Arduino IDE 1.6.7
A Arduino IDE foi utilizada para o desenvolvimento dos algoritmos que executam nas três placas arduinos (utilizando C), ela pode ser obtida através do endereço: https://www.arduino.cc/en/Main/Software.

Ambos softwares foram executados e um ambiente Windows (Windows 7 versão de 64 bits).


    1.2 Componentes e periféricos

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
   
Além de outros componentes como o braço mecãnico que componentes Componentes gerais: Resistores, LED’s, estanho, placa pcb,tubos termoretráteis, pinos e conectores, fios e chaves.

      2. METODOLOGIA


A metodologia aplicada partiu da análise individual para, posteriormente, avaliar uma interação em conjunto, isto é, primeiro foram avaliados os sensores e componentes individualmente e, uma vez que o funcionamento estivesse dominado, o mesmo foi integrado ao conjunto operacional.

Essa estratégia foi utilizada para todos os sensores (até por questão de manter uma abstração no código, então saber como o sensor funciona permite criar funções em um nível de abstração maior para operar cada componente de maneira mais simples).
A escolha dos micro controladores foi devido ao suporte à linguagem C/C++ e compatibilidade com uma série de sensores e bibliotecas já existentes. 

Na construção da estrutura do Rover foram avaliados pontos como resistência e peso, o polímero se mostrou uma boa saída já que é facilmente manipulado (é facilmente cortado e furado) e também possibilita um maior nível de personalização, já que o design pode ser alterado da maneira que for necessário para cada caso; caso fosse utilizado uma estrutura de plástico pronta ficaria mais restrito a esse tipo de expansão e adição de hardware.

Os dois Arduinos que são acoplados no VANT se comunicam por uma interface IC2 e outro Arduino se comunica via USB com o PC, então antes de uma integração foi necessário realizar testes de envio de pacotes entre essas estruturas, o uso de dois Arduinos no VANT foi justamente para priorizar o recebimento de um pacote de dados.



      3. RESULTADOS E DISCUSSÃO

 
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
   
  O painel de controle possui um funcionamento básico: ele conecta através de uma porta
serial ao emissor para enviar e receber um pacote de dados, no caso da porta serial manipulamos
as operações por um componente mostrado na Figura 3 abaixo.
![alt tag](C:\Users\Ivan\Desktop\Microcontrolador e Aplicações\trunk\imagens

  A conexão é feita por um botão e uma caixa de texto que lista todas as portas disponíveis (no caso o dispositivo conectado a entrada USB), o timer timerCOM é responsável por atualizar essa lista de portas através de uma rotina (código 2). Com o dispositivo conectado  a porta deve ser listada como disponível e basta clicar no botão conectar. A conexão é explicada pelo código 3. Ao conectar podemos transferir e receber dados da entrada serial (da mesma maneira que é possível utilizando o serial da própria Arduino IDE).

  
    3.3 Lista de comandos
  
   A única função "diferente"foi apresentada anteriormente (a conexão USB), todas as demais ações são baseadas no envio e recebimento de pacotes (contendo comandos e dados). Havia a necessidade de se transmitir comandos que eram acompanhados de determinados valores, outras vezes comandos apenas (sem um dado associado) e em alguns casos comandos para requisitar alguma informação. Dessa forma o mecanismo desenvolvido para a comunicação acabou composto por dois bytes: um byte para o comando (cmd) e outro byte para o dado (valor). Os dois bytes são enviados juntos (como em uma variável do tipo int de 8bits: 11110000, os 4 primeiros bits são correspondente ao comando e os 4 bits mais a direita correspondente ao dado).
  É um método bem simples de transferir um pacote de dados entre dispositivos mas em um futuro uma melhoria como criptografia seria ideal para proteção dos dados transmitidos. 
  Também foi adotado um intervalo de valores para os quais os comandos seriam do tipo "simples"ou "composto"; Comandos simples são uma via de mão única: eles são transferidos via USB para o emissor e então enviados ao ROVER, já comandos compostos precisam de um retorno. Esse tratamento se fez necessário devido ao funcionamento do componente de rádio frequência que, apesar de operar como transmissor e receptor, precisa que o modo de operação seja alterado manualmente, então quando um comando composto é acionado o "anteriormente"emissor
passa a operar como receptor até receber a informação do ROVER. O intervalo define que comandos entre 0000 até 0124 são tratados como simples, a partir do 0125 até 9999 são comandos compostos.

    3.4 LEDs de controle
 
 Antes de prosseguir para a análise dos demais controladores temos a abordagem sobre os LEDs utilizados para controle, no caso eles funcionam como um feedback visual para alguma mudança de estado ou execução de algum trecho chave do código. Esses sinais são muito
importantes pois é possível concluir que algo deu errado mais rapidamente, diferentemente de uma aplicação 100. Dessa maneira os LEDs foram equipados como mecanismos de confirmação para o sistema.

    3.5 Emissor Primário
  
  O emissor primário recebe os bytes da mensagem via serial e encaminha para o receptor no ROVER. A estrutura interna é mostrada na Figura 4, é composto por um conjunto de três LEDs e um componente de rádio frequência (modelo NRF24L01+). 
  O esquemático das ligações é mostrado na Figura 5. A rotina do emissor é receber a mensagem e armazenar em um inteiro (de 2 bytes) e em seguida enviar para o ROVER.
  Existe uma referência ao arquivo pacote.h, um código foi criado para abstrair a composição da mensagem que é enviada/recebida (os 2 bytes de mensagem), como sempre é nesse formato para todos os dispositivos usar um include evita a repetição de código.

![alt tag](https://github.com/CaioPegoraro/ProjetoFinalMic/blob/master/imagens/Emissorprimario.PNG)

                   Figura 4: Detalhes da estrutura do emissor primário.

![alt tag](https://github.com/CaioPegoraro/ProjetoFinalMic/blob/master/schematics/emissor_primario.png)

                   Figura 5: Ligação dos pinos no emissor primário.


  
    3.6 Receptor Primário
    
O receptor primário tem como prioridade receber os dados pelo sensor de rádio frequência e então repassar o comando para o controlador dos motores (no caso o Arduino MEGA, chamado de receptor secundário), executar uma ação por ele mesmo e/ou enviar uma mensagem de volta
para o emissor primário (o arduino ligado na USB do PC).

Ele é equipado com um LED indicador, um buzzer (para emitir um aviso sonoro), um sensor de rádio frequência. Utilizamos a conexão IC2 para comunicação entre os dois arduinos e adicionalmente foi utilizado uma porta analógica para leitura da voltagem da bateria de lipo
(que alimenta os motores).
  
  A placa construída pode ser vista na Figura 6, a visão logo abaixo não é espelhada, segue a mesma sequência de pinos (já foi invertida), na Figura 7 temos o esquema de ligações realizado na placa.
  
  O mecanismo de comunicação entre os dois Arduinos é chamado de IC2 (Inter-Integrated Circuit) e funciona com uma hierarquia mestre escravo, o receptor primário é o mestre e o receptor secundário o escravo, quando um comando precisa ser enviado ao Arduino Mega o Arduino Uno inicia a transferência que gera uma interrupção de execução no destinatário (para que essa recepção seja tratada no mesmo instante). Isso permite que múltiplos dispositivos se comuniquem por fio (inclusive o módulo do acelerômetro é operado por esse mesmo mecanismo, então a estrutura completa pode ser vista na Figura 8. Cada módulo é acessado por um endereço local pré-definido (no código de cada um), o acelerômetro já possui um endereçamento fixo (há a opção para mudar para outro valor acionando uma entrada do sensor, permitindo o uso de dois módulos simultaneamentes).
  
  O código tem uma chave de execução: trabalhar com uma variável globalizada (pacote dados) que é utilizada para receber os dados, para enviar os valores para o receptor secundário e também para enviar de volta ao emissor, caso necessário (por isso nas chamadas da função
enviaIC2 não há parâmetros, ela simplesmente considera que o que estiver nessa variável é o que deve ser enviado).

![alt tag](https://github.com/CaioPegoraro/ProjetoFinalMic/blob/master/imagens/receptor_primario.PNG)
                      
                      Figura 6: Detalhe da construção do receptor primário.
                      
![alt tag](https://github.com/CaioPegoraro/ProjetoFinalMic/blob/master/schematics/receptor_primario.PNG)
         
                     Figura 7: Esquema de ligação na placa do receptor primário.
                     
![alt tag](https://github.com/CaioPegoraro/ProjetoFinalMic/blob/master/schematics/estrutura%20de%20comunicacao.PNG)
         
                     Figura 8: Estrutura de comunicação local entre os dispositivos.
                     
    3.7 Receptor secundário
   
O receptor secundário executa rotinas de controle automatizado dos motores e recebe comandos pela comunicação IC2. Quando uma mensagem é recebida ocorre uma interrupção e essa chamada é atendida, depois retorna a execução do loop. 

Abaixo na Figura 9 na temos as ligações feitas na placa, note que ele é ligado no arduíno uno o sensor de temperatura, laser, display LCD, e os motores estão todos ligados no receptor primário como mostra abaixo.

![alt tag](https://github.com/CaioPegoraro/ProjetoFinalMic/blob/master/schematics/receptor_secundario2.png)
         
                     Figura 9: Esquema de ligações no receptor secundário.
     

    3.8 Alimentação do Circuito
     
O circuito é alimentado por uma bateria de 9v, não foi utilizado o pino Vin mas sim a entrada jack nos dois arduinos. Foi colocado uma chave de On/Off  para controle. Alternativamente o circuito oferece suporte a uma alimentação por fonte (no projeto: uma fonte comum de carregador com um regulador de tensão Lm2596s para 9v) que é utilizada para testes e depuração.

A bateria de lipo oferece saídas que são ligadas a um alarme que mostra visualmente a carga de cada cédula e exibe um sinal sonoro quando atinge níveis críticos. Uma dessas saídas (a 1s de até 3.3v aproximadamente) é ligada a entrada analógica do receptor primário, a leitura analógica exibe no painel de controle um nível de porcentagem restante de bateria (essa conexão é controlada por uma chave.
 
 
      4. CONCLUSÃO
 
A construção do projeto do estudo e análise de diversas técnologias e práticas,foram diferentes softwares e hardwares, diferentes arquiteturas e modelos de prototipagem formando algo bem diversificado.

O modelo de comunicação sem fio e transferência dos dados pode ser usado para as mais diversas aplicações, assim como a hierarquia do hardware pode ser expandida, montando uma verdadeira rede de micro controladores em paralelo.

O desenvolvimento da estrutura física envolveu muitas considerações que puderam ser comprovadas posteriormente, com relação ao design na composição e peso dos materiais empregados onde, futuramente pode ser substituída por uma construção em plástico (modelado por uma impressora 3D).

Os motores e a bateria corresponderam aos cálculos realizados e foram capazes de atender as necessidades básicas do Rover apesar de que a adição de outra bateria aumentaria consideravelmente o tempo total mesmo sem causar grandes alterações no peso final).

A interface de controle garante um maior nível de personalização (poderia construir um banco de dados para coleta de dados específicos) mas também pode ter certas complementações como desenvolvimento de um controle analógico ou uso de um smartphone para um melhor
controle.

Os micro controladores certamente foram suficientes para o processamento dos dados, apesar de ter sido utilizados duas unidades no Rover ainda sim podem ser considerados como uma opção viável, outras possibilidades como uso do raspberry podem ser estudadas e avaliadas.

O projeto como um todo foi um grande desafio, muitos testes e alterações até atingir o nível atual, a maior parte do esforço em desenvolver a arquitetura para interação entre todas os módulos garantindo que exista um processamento em tempo real, sem que algo tenha que ser propriamente sacrificado para isso. O Rover ainda será aprimorado com itens adicionais (um suporte para camera por exemplo, principalmente com um hardware para armazenamento de imagens). A ideia é incrementar o projeto mantendo do Rover representado na Figura 10 abaixo, a arquitetura desenvolvida inicialmente e aprimorar pontos que resultem em melhoras significativas.
 
![alt tag] (https://github.com/CaioPegoraro/ProjetoFinalMic/blob/master/imagens/Rover%203.JPG)
 
                     Figura 10: Projeto final.
                     
      5. REFERÊNCIAS BIBLIOGRÁFICAS

[1] MARGOLIS, Michael. Arduino Cookbook .ed. O’Reilly Media, 2011.

[2] McROBERTS, Michel, Arduino Básico, Ed. Novatec, 2011.

[3] MONK, Simon. Programação com Arduino: Começando com Sketches, Ed. Bookan, 2012.

[4] Arduino, <http://www.arduino.cc/>, acesso em acesso em acessado Janeiro de 2017.

[5] “Veículos aéreos não tripulados prometem revolucionar mercado de geotecnologia”, Massa Cinzenta, <http://bit.ly/2c1ptEK>, acesso em acessado Janeiro de 2017.

[6] VANTs e RPA, <http://bit.ly/2bvq4Sd>, acessado Janeiro de 2017.

[7] Visual Studio, <https://www.visualstudio.com/>, acessado Janeiro de 2017.

[8] Bluetooth, < http://bit.ly/1Pj9caw>, acessado Janeiro de 2017.

[9] FPV, <http://fpvbrasil.com.br/page/o-que-e>, acessado Janeiro de 2017.

[10] ANAC, <http://www.anac.gov.br/>, acessado Fevereiro de 2017..

[11] WIRELESS, <http://bit.ly/2bT6XRs>, acessado Fevereiro de 2017..

[12] “Quadcopter Design”,< http://bit.ly/2btvlIx>, acessado Fevereiro de 2017..

[13] “RF24 Driver release”, Maniacal Bits, <http://bit.ly/2c7EfwH>, acessado Fevereiro de 2017..

[14] “Giroscópio GY-521”, O mundo da programação, <http://bit.ly/2bvqUJT>, acessado Fevereiro de 2017..

[15] “Controle PID em sistemas embarcados”, Embarcados, <http://bit.ly/2c1Dx0E>, acessado Fevereiro de 2017..

[16] “Quadcopter PID Explained and Tuning”, OscarLiang, <http://bit.ly/1QJgfun>, acessado Fevereiro de 2017.

[17] “Estabilizador de voo”, Drones personalizados, <http://bit.ly/2bKZ9hd>, acessado Fevereiro de 2017.
