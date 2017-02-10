using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;  // necessário para ter acesso as portas


namespace VantPainelDeControle
{
    public partial class Form1 : Form
    {

        string RxString;
        int pacote_recebido;
        int status_buzzer = 0;
        int status_motores = 0;
        int status_laser = 0;

        public Form1()
        {
            InitializeComponent();
            timerCOM.Enabled = true;
            //timerStatusBateria.Enabled = true;
            //timerStatusConexao.Enabled = true;
        }

        private void atualizaListaCOMs()
        {
            int i;
            bool quantDiferente;    //flag para sinalizar que a quantidade de portas mudou

            i = 0;
            quantDiferente = false;

            //se a quantidade de portas mudou
            if (comboBox1.Items.Count == SerialPort.GetPortNames().Length)
            {
                foreach (string s in SerialPort.GetPortNames())
                {
                    if (comboBox1.Items[i++].Equals(s) == false)
                    {
                        quantDiferente = true;
                    }
                }
                
            }
            else
            {
                quantDiferente = true;
            }

            //Se não foi detectado diferença
            if (quantDiferente == false)
            {
                return;                     //retorna
            }

            //limpa comboBox
            comboBox1.Items.Clear();

            //adiciona todas as COM diponíveis na lista 
            foreach (string s in SerialPort.GetPortNames())
            {
                comboBox1.Items.Add(s);
            }
            //seleciona a primeira posição da lista
            if(comboBox1.Items.Count > 0)
            {
                comboBox1.SelectedIndex = 0;
            }
            else
            {
                comboBox1.Text = " ";
            }
        }


        private void serialPort1_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {

            RxString = serialPort1.ReadLine();              //le o dado disponível na serialx

            pacote_recebido = Int32.Parse(RxString);

            this.Invoke(new EventHandler(trataDadoRecebido));   //chama outra thread para escrever o dado no text box
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen == false)
            {
                try
                {
                    serialPort1.PortName = comboBox1.Items[comboBox1.SelectedIndex].ToString();
                    serialPort1.Open();

                }
                catch
                {
                    return;

                }
                if (serialPort1.IsOpen)
                {
                    btConectar.Text = "Desconectar";
                    comboBox1.Enabled = false;

                    textBoxReceber.AppendText("\n\n == Conectado ao emissor == \n");
                }
            }
            else
            {

                try
                {
                    serialPort1.Close();
                    comboBox1.Enabled = true;
                    btConectar.Text = "Conectar";

                    textBoxReceber.AppendText("\n\n == Desconectado do emissor == \n");
                }
                catch
                {
                    return;
                }

            }
        }


        private void Form_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (serialPort1.IsOpen == true)  // se porta aberta 
                serialPort1.Close();            //fecha a porta
        }

        private void btEnviar_Click(object sender, EventArgs e)
        {
           
        }

        private void trataDadoRecebido(object sender, EventArgs e)
        {
            //Um dado recebido é composto por um cmd (comando) e um valor associado.
            //dessa forma é possível examinar qual ação tomar sem ter salvo o comando enviado anteriormente

            //textBoxReceber.AppendText(RxString + "\n");

           
            int cmd = pacote_recebido / 100000;
            int valor = pacote_recebido - cmd*100000;

            //Console.WriteLine("pct_recebido: " + pacote_recebido);
            //Console.WriteLine(cmd);
            //Console.WriteLine(valor);

            textBoxReceber.AppendText("\n\n == Recebido resposta << \n");
            textBoxReceber.AppendText("cmd: " + cmd.ToString() + "\n");
            textBoxReceber.AppendText("valor: " + valor.ToString() + "\n\n\n");

            //tratar a descrição e ação do comando recebido:

            switch (cmd){

                case 125:
                    //conexao requisitada
                    //porem, pode ter sucesso (Valor=1) ou falhado (valor=0);

                    if (valor == 1)
                    {
                        //conexao bem sucedida
                        lblStatusConexao.Text = "ON";
                        lblStatusConexao.ForeColor = System.Drawing.Color.Green;
                    }
                    else if(valor == 0)
                    {
                        lblStatusConexao.Text = "OFF";
                        lblStatusConexao.ForeColor = System.Drawing.Color.Red;
                    }
                    break;

                case 126:
                    //Leitura valor da bateria
                    //leitura cedula unica: 4.13v = 100%
                    //                      2.13v = 0% (nivel altamente critico)



                    double porcent_bat = ((double)(valor-270) / (double)(413-270))*100;
                    int bat_int = (int)porcent_bat;
                    lblNivelBateria.Text = bat_int.ToString() + "%";
                    //Console.WriteLine(porcent_bat);

                    break;
            }

        }

        private void btnConfirmaVelocidadeTotal_Click(object sender, EventArgs e)
        {
            

        }

        private void timerCOM_Tick_1(object sender, EventArgs e)
        {
            atualizaListaCOMs();
        }

        //btnConexaoRemota
        private void button10_Click(object sender, EventArgs e)
        {
            // 125 # Ligar LED de conexao
            // retorno: sim, cmd e valor (1 se sucesso, 0 se falhar)
            // valor: não

            // cmd = 0125
            //valor = 0000

            //                        cmd  valor
            byte[] data = new byte[] { 0125, 0000 };

            if (serialPort1.IsOpen == true)
            {//porta está aberta
                serialPort1.Write(data, 0, 2); //escreve o vetor de 2 bytes na saida serial

            }
            textBoxReceber.AppendText("\n\n == Comando enviado >> \n");
            textBoxReceber.AppendText("cmd: 0125 \n");
            textBoxReceber.AppendText("valor: 0000 \n\n");
        }

        private void button25_Click(object sender, EventArgs e)
        {
            textBoxReceber.Clear();
        }

        private void btnBuzzer_Click(object sender, EventArgs e)
        {
            // 0014 # Acionar buzzer
            // retorno: nao
            // valor: nao

            // cmd  = 0014
            //valor = 0000

            //                        cmd  valor

            if (this.status_buzzer == 1)
            {
                //inverter para desligado
                status_buzzer = 0;
                byte[] data = new byte[] { 0015, 0 };
                lblStatusBuzzer.Text = "OFF";
                lblStatusBuzzer.ForeColor = System.Drawing.Color.Red;

                if (serialPort1.IsOpen == true)
                {//porta está aberta
                    serialPort1.Write(data, 0, 2); //escreve o vetor de 2 bytes na saida serial
                    
                    //reportar comando enviado no prompt de comando
                    textBoxReceber.AppendText("\n\n == Comando enviado >> \n");
                    textBoxReceber.AppendText("cmd: 0015 \n");
                    textBoxReceber.AppendText("valor: 0000 \n\n");
                }

            }
            else if (this.status_buzzer == 0)
            {
                //inverter para ligado
                status_buzzer = 1;
                byte[] data = new byte[] { 0014, 0 };
                lblStatusBuzzer.Text = "ON";
                lblStatusBuzzer.ForeColor = System.Drawing.Color.Green;

                if (serialPort1.IsOpen == true)
                {//porta está aberta
                    serialPort1.Write(data, 0, 2); //escreve o vetor de 2 bytes na saida serial

                    //reportar comando enviado no prompt de comando
                    textBoxReceber.AppendText("\n\n == Comando enviado >> \n");
                    textBoxReceber.AppendText("cmd: 0014 \n");
                    textBoxReceber.AppendText("valor: 0000 \n\n");
                }

            }

            
        }

        private void btnAttBateria_Click(object sender, EventArgs e)
        {
            byte[] data = new byte[] { 126, 0 };

            if (serialPort1.IsOpen == true)
            {//porta está aberta
                serialPort1.Write(data, 0, 2); //escreve o vetor de 2 bytes na saida serial

            }
            textBoxReceber.AppendText("\n\n == Comando enviado >> \n");
            textBoxReceber.AppendText("cmd: 0126 \n");
            textBoxReceber.AppendText("valor: 0000 \n\n");
        }

        private void timerStatusBateria_Tick(object sender, EventArgs e)
        {
            byte[] data = new byte[] { 126, 0 };

            if (serialPort1.IsOpen == true)
            {//porta está aberta
                serialPort1.Write(data, 0, 2); //escreve o vetor de 2 bytes na saida serial

            }
            //textBoxReceber.AppendText("\n\n == Comando enviado >> \n");
            //textBoxReceber.AppendText("cmd: 0126 \n");
            //textBoxReceber.AppendText("valor: 0000 \n\n");
        }

        private void timerStatusConexao_Tick(object sender, EventArgs e)
        {
            byte[] data = new byte[] { 125, 0 };

            if (serialPort1.IsOpen == true)
            {//porta está aberta
                serialPort1.Write(data, 0, 2); //escreve o vetor de 2 bytes na saida serial
            }
        }

        private void button10_Click_1(object sender, EventArgs e)
        {
            //btn: parar motores dc
            //função: envia um comando para parar os motores dc

            byte[] data = new byte[] { 0007, 0000 };

            if (serialPort1.IsOpen == true)
            {//porta está aberta
                serialPort1.Write(data, 0, 2); //escreve o vetor de 2 bytes na saida serial
            }

            textBoxReceber.AppendText("\n\n == Comando enviado >> \n");
            textBoxReceber.AppendText("cmd: 0007 \n");
            textBoxReceber.AppendText("valor: 0000 \n\n");
        }

        private void btnLiberarMotores_Click(object sender, EventArgs e)
        {   

        }

        private void btnM1_Click(object sender, EventArgs e)
        {
            // cmd:
            // 0001 # controle da velocidade do motor 1
            // retorno: nao
            // valor: sim
            //                        cmd  valor
            byte temp = byte.Parse(lblM1.Text);
            byte[] data = new byte[] { 0001, temp };

            if (serialPort1.IsOpen == true)
            {//porta está aberta
                serialPort1.Write(data, 0, 2); //escreve o vetor de 2 bytes na saida serial

            }
            textBoxReceber.AppendText("\n\n == Comando enviado >> \n");
            textBoxReceber.AppendText("cmd: 0001 \n");
            textBoxReceber.AppendText("valor: " + lblM1.Text + "\n\n");


        }

        private void btnM2_Click(object sender, EventArgs e)
        {
            // cmd:
            // 0002 # controle da velocidade do motor 2
            // retorno: nao
            // valor: sim
            //                        cmd  valor
            byte temp = byte.Parse(lblM2.Text);
            byte[] data = new byte[] { 002, temp };

            if (serialPort1.IsOpen == true)
            {//porta está aberta
                serialPort1.Write(data, 0, 2); //escreve o vetor de 2 bytes na saida serial

            }
            textBoxReceber.AppendText("\n\n == Comando enviado >> \n");
            textBoxReceber.AppendText("cmd: 0002 \n");
            textBoxReceber.AppendText("valor: " + lblM2.Text + "\n\n");

        }

        private void btnM3_Click(object sender, EventArgs e)
        {
            // cmd:
            // 0003 # controle da velocidade do motor 3
            // retorno: nao
            // valor: sim
            //                        cmd  valor
            byte temp = byte.Parse(lblM3.Text);
            byte[] data = new byte[] { 003, temp };

            if (serialPort1.IsOpen == true)
            {//porta está aberta
                serialPort1.Write(data, 0, 2); //escreve o vetor de 2 bytes na saida serial

            }
            textBoxReceber.AppendText("\n\n == Comando enviado >> \n");
            textBoxReceber.AppendText("cmd: 0003 \n");
            textBoxReceber.AppendText("valor: " + lblM3.Text + "\n\n");
        }

        private void btnM4_Click(object sender, EventArgs e)
        {
            // cmd:
            // 0004 # controle da velocidade do motor 4
            // retorno: nao
            // valor: sim
            //                        cmd  valor
            byte temp = byte.Parse(lblM4.Text);
            byte[] data = new byte[] { 004, temp };

            if (serialPort1.IsOpen == true)
            {//porta está aberta
                serialPort1.Write(data, 0, 2); //escreve o vetor de 2 bytes na saida serial

            }
            textBoxReceber.AppendText("\n\n == Comando enviado >> \n");
            textBoxReceber.AppendText("cmd: 0004 \n");
            textBoxReceber.AppendText("valor: " + lblM4.Text + "\n\n");
        }

        private void btnM1mais_Click(object sender, EventArgs e)
        {
            //incrementa o valor da textbox do motor1 e set a nova velocidade

            int numero;
            int.TryParse(lblM1.Text, out numero);
            lblM1.Text = (numero + Convert.ToInt16(txt_intervalo_angulo.Text)).ToString();

            //                        cmd  valor
            byte temp = byte.Parse(lblM1.Text);
            byte[] data = new byte[] { 001, temp };

            if (serialPort1.IsOpen == true)
            {//porta está aberta
                serialPort1.Write(data, 0, 2); //escreve o vetor de 2 bytes na saida serial

            }
            textBoxReceber.AppendText("\n\n == Comando enviado >> \n");
            textBoxReceber.AppendText("cmd: 0001 \n");
            textBoxReceber.AppendText("valor: " + lblM1.Text + "\n\n");
        }

        private void btnM1menos_Click(object sender, EventArgs e)
        {
            //decrementa o valor da textbox do motor1 e set a nova velocidade

            int numero;
            int.TryParse(lblM1.Text, out numero);
            lblM1.Text = (numero - Convert.ToInt16(txt_intervalo_angulo.Text)).ToString();


            //                        cmd  valor
            byte temp = byte.Parse(lblM1.Text);
            byte[] data = new byte[] { 001, temp };

            if (serialPort1.IsOpen == true)
            {//porta está aberta
                serialPort1.Write(data, 0, 2); //escreve o vetor de 2 bytes na saida serial

            }
            textBoxReceber.AppendText("\n\n == Comando enviado >> \n");
            textBoxReceber.AppendText("cmd: 0001 \n");
            textBoxReceber.AppendText("valor: " + lblM1.Text + "\n\n");
        }

        private void btnM2mais_Click(object sender, EventArgs e)
        {
            int numero;
            int.TryParse(lblM2.Text, out numero);
            lblM2.Text = (numero + Convert.ToInt16(txt_intervalo_angulo.Text)).ToString();


            //                        cmd  valor
            byte temp = byte.Parse(lblM2.Text);
            byte[] data = new byte[] { 002, temp };

            if (serialPort1.IsOpen == true)
            {//porta está aberta
                serialPort1.Write(data, 0, 2); //escreve o vetor de 2 bytes na saida serial

            }
            textBoxReceber.AppendText("\n\n == Comando enviado >> \n");
            textBoxReceber.AppendText("cmd: 0002 \n");
            textBoxReceber.AppendText("valor: " + lblM2.Text + "\n\n");
        }

        private void btnM2menos_Click(object sender, EventArgs e)
        {
            int numero;
            int.TryParse(lblM2.Text, out numero);
            lblM2.Text = (numero - Convert.ToInt16(txt_intervalo_angulo.Text)).ToString();


            //                        cmd  valor
            byte temp = byte.Parse(lblM2.Text);
            byte[] data = new byte[] { 002, temp };

            if (serialPort1.IsOpen == true)
            {//porta está aberta
                serialPort1.Write(data, 0, 2); //escreve o vetor de 2 bytes na saida serial

            }
            textBoxReceber.AppendText("\n\n == Comando enviado >> \n");
            textBoxReceber.AppendText("cmd: 0002 \n");
            textBoxReceber.AppendText("valor: " + lblM2.Text + "\n\n");
        }

        private void btnM3mais_Click(object sender, EventArgs e)
        {
            int numero;
            int.TryParse(lblM3.Text, out numero);
            lblM3.Text = (numero + Convert.ToInt16(txt_intervalo_angulo.Text)).ToString();


            //                        cmd  valor
            byte temp = byte.Parse(lblM3.Text);
            byte[] data = new byte[] { 003, temp };

            if (serialPort1.IsOpen == true)
            {//porta está aberta
                serialPort1.Write(data, 0, 2); //escreve o vetor de 2 bytes na saida serial

            }
            textBoxReceber.AppendText("\n\n == Comando enviado >> \n");
            textBoxReceber.AppendText("cmd: 0003 \n");
            textBoxReceber.AppendText("valor: " + lblM3.Text + "\n\n");
        }

        private void btnM3menos_Click(object sender, EventArgs e)
        {
            int numero;
            int.TryParse(lblM3.Text, out numero);
            lblM3.Text = (numero - Convert.ToInt16(txt_intervalo_angulo.Text)).ToString();


            //                        cmd  valor
            byte temp = byte.Parse(lblM3.Text);
            byte[] data = new byte[] { 003, temp };

            if (serialPort1.IsOpen == true)
            {//porta está aberta
                serialPort1.Write(data, 0, 2); //escreve o vetor de 2 bytes na saida serial

            }
            textBoxReceber.AppendText("\n\n == Comando enviado >> \n");
            textBoxReceber.AppendText("cmd: 0003 \n");
            textBoxReceber.AppendText("valor: " + lblM3.Text + "\n\n");
        }

        private void btnM4mais_Click(object sender, EventArgs e)
        {
            int numero;
            int.TryParse(lblM4.Text, out numero);
            lblM4.Text = (numero + Convert.ToInt16(txt_intervalo_angulo.Text)).ToString();


            //                        cmd  valor
            byte temp = byte.Parse(lblM4.Text);
            byte[] data = new byte[] { 004, temp };

            if (serialPort1.IsOpen == true)
            {//porta está aberta
                serialPort1.Write(data, 0, 2); //escreve o vetor de 2 bytes na saida serial

            }
            textBoxReceber.AppendText("\n\n == Comando enviado >> \n");
            textBoxReceber.AppendText("cmd: 0004 \n");
            textBoxReceber.AppendText("valor: " + lblM4.Text + "\n\n");
        }

        private void btnM4menos_Click(object sender, EventArgs e)
        {
            int numero;
            int.TryParse(lblM4.Text, out numero);
            lblM4.Text = (numero - Convert.ToInt16(txt_intervalo_angulo.Text)).ToString();


            //                        cmd  valor
            byte temp = byte.Parse(lblM4.Text);
            byte[] data = new byte[] { 004, temp };

            if (serialPort1.IsOpen == true)
            {//porta está aberta
                serialPort1.Write(data, 0, 2); //escreve o vetor de 2 bytes na saida serial

            }
            textBoxReceber.AppendText("\n\n == Comando enviado >> \n");
            textBoxReceber.AppendText("cmd: 0004 \n");
            textBoxReceber.AppendText("valor: " + lblM4.Text + "\n\n");
        }

        private void btnM1parar_Click(object sender, EventArgs e)
        {
            //envia valor para parar o motor1

            int numero = 65;
            lblM1.Text = (numero).ToString();

            //                        cmd  valor
            byte temp = byte.Parse(lblM1.Text);
            byte[] data = new byte[] { 001, temp };

            if (serialPort1.IsOpen == true)
            {//porta está aberta
                serialPort1.Write(data, 0, 2); //escreve o vetor de 2 bytes na saida serial

            }
            textBoxReceber.AppendText("\n\n == Comando enviado >> \n");
            textBoxReceber.AppendText("cmd: 0001 \n");
            textBoxReceber.AppendText("valor: " + lblM1.Text + "\n\n");

        }

        private void btnM2parar_Click(object sender, EventArgs e)
        {
            //envia valor para parar o motor2

            int numero = 65;
            lblM2.Text = (numero).ToString();

            //                        cmd  valor
            byte temp = byte.Parse(lblM2.Text);
            byte[] data = new byte[] { 002, temp };

            if (serialPort1.IsOpen == true)
            {//porta está aberta
                serialPort1.Write(data, 0, 2); //escreve o vetor de 2 bytes na saida serial

            }
            textBoxReceber.AppendText("\n\n == Comando enviado >> \n");
            textBoxReceber.AppendText("cmd: 0002 \n");
            textBoxReceber.AppendText("valor: " + lblM2.Text + "\n\n");
        }

        private void btnM3parar_Click(object sender, EventArgs e)
        {
            //envia valor para parar o motor3

            int numero = 65;
            lblM3.Text = (numero).ToString();

            //                        cmd  valor
            byte temp = byte.Parse(lblM3.Text);
            byte[] data = new byte[] { 003, temp };

            if (serialPort1.IsOpen == true)
            {//porta está aberta
                serialPort1.Write(data, 0, 2); //escreve o vetor de 2 bytes na saida serial

            }
            textBoxReceber.AppendText("\n\n == Comando enviado >> \n");
            textBoxReceber.AppendText("cmd: 0003 \n");
            textBoxReceber.AppendText("valor: " + lblM3.Text + "\n\n");
        }

        private void btnM4parar_Click(object sender, EventArgs e)
        {
            //envia valor para parar o motor4

            int numero = 65;
            lblM4.Text = (numero).ToString();

            //                        cmd  valor
            byte temp = byte.Parse(lblM4.Text);
            byte[] data = new byte[] { 004, temp };

            if (serialPort1.IsOpen == true)
            {//porta está aberta
                serialPort1.Write(data, 0, 2); //escreve o vetor de 2 bytes na saida serial

            }
            textBoxReceber.AppendText("\n\n == Comando enviado >> \n");
            textBoxReceber.AppendText("cmd: 0004 \n");
            textBoxReceber.AppendText("valor: " + lblM4.Text + "\n\n");
        }

        private void btnMparar_Click(object sender, EventArgs e)
        {
         
        }

        private void btnMmais_Click(object sender, EventArgs e)
        {
           
        }

        private void btnMmenos_Click(object sender, EventArgs e)
        {
           
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void btn_frente_MouseDown(object sender, MouseEventArgs e)
        {
            //cliclou no botao para frente: envia comando para motores dc 
            // cmd:
            // 0005 # motores dc para frente
            // retorno: nao
            // valor: sim (velocidade)

            //                        cmd  valor
            byte temp = byte.Parse(lbl_veloDC.Text);
            byte[] data = new byte[] { 0005, temp };

            if (serialPort1.IsOpen == true)
            {//porta está aberta
                serialPort1.Write(data, 0, 2); //escreve o vetor de 2 bytes na saida serial

            }
            textBoxReceber.AppendText("\n\n == Comando enviado >> \n");
            textBoxReceber.AppendText("cmd: 0005 \n");
            textBoxReceber.AppendText("valor: " + lbl_veloDC.Text + "\n\n");

        }

        private void btn_frente_MouseUp(object sender, MouseEventArgs e)
        {
            //enviar comando para parar
            byte[] data = new byte[] { 0007, 0000 };

            if (serialPort1.IsOpen == true)
            {//porta está aberta
                serialPort1.Write(data, 0, 2); //escreve o vetor de 2 bytes na saida serial

            }
            textBoxReceber.AppendText("\n\n == Comando enviado >> \n");
            textBoxReceber.AppendText("cmd: 0007 \n");
            textBoxReceber.AppendText("valor: 0000 \n\n");
        }

        private void btn_abrir_Click(object sender, EventArgs e)
        {
            // cmd:
            // 0003 # controledo angulo personalizado (abrir garra)
            // retorno: nao
            // valor: sim
            byte[] data = new byte[] { 0003, 0090 };

            if (serialPort1.IsOpen == true)
            {//porta está aberta
                serialPort1.Write(data, 0, 2); //escreve o vetor de 2 bytes na saida serial

            }
            textBoxReceber.AppendText("\n\n == Comando enviado >> \n");
            textBoxReceber.AppendText("cmd: 0003 \n");
            textBoxReceber.AppendText("valor: 0090 \n\n");
        }

        private void btn_fechar_Click(object sender, EventArgs e)
        {
            // cmd:
            // 0003 # controledo angulo personalizado (fechar garra)
            // retorno: nao
            // valor: sim
            byte[] data = new byte[] { 0003, 0050 };

            if (serialPort1.IsOpen == true)
            {//porta está aberta
                serialPort1.Write(data, 0, 2); //escreve o vetor de 2 bytes na saida serial

            }
            textBoxReceber.AppendText("\n\n == Comando enviado >> \n");
            textBoxReceber.AppendText("cmd: 0003 \n");
            textBoxReceber.AppendText("valor: 0050 \n\n");
        }

        private void btn_tras_MouseDown(object sender, MouseEventArgs e)
        {
            //cliclou no botao para frente: envia comando para motores dc 
            // cmd:
            // 0006 # motores dc para tras
            // retorno: nao
            // valor: sim (velocidade)

            //                        cmd  valor
            byte temp = byte.Parse(lbl_veloDC.Text);
            byte[] data = new byte[] { 0006, temp };

            if (serialPort1.IsOpen == true)
            {//porta está aberta
                serialPort1.Write(data, 0, 2); //escreve o vetor de 2 bytes na saida serial

            }
            textBoxReceber.AppendText("\n\n == Comando enviado >> \n");
            textBoxReceber.AppendText("cmd: 0006 \n");
            textBoxReceber.AppendText("valor: " + lbl_veloDC.Text + "\n\n");
        }

        private void btn_tras_MouseUp(object sender, MouseEventArgs e)
        {
            //enviar comando para parar
            byte[] data = new byte[] { 0007, 0000 };

            if (serialPort1.IsOpen == true)
            {//porta está aberta
                serialPort1.Write(data, 0, 2); //escreve o vetor de 2 bytes na saida serial

            }
            textBoxReceber.AppendText("\n\n == Comando enviado >> \n");
            textBoxReceber.AppendText("cmd: 0007 \n");
            textBoxReceber.AppendText("valor: 0000 \n\n");
        }

        private void btn_dir_MouseDown(object sender, MouseEventArgs e)
        {
            //cliclou no botao para direita: envia comando para motores dc 
            // cmd:
            // 0008 # motores dc para direita (lados alternados
            // retorno: nao
            // valor: sim (velocidade)

            //                        cmd  valor
            byte temp = byte.Parse(lbl_veloDC.Text);
            byte[] data = new byte[] { 0008, temp };

            if (serialPort1.IsOpen == true)
            {//porta está aberta
                serialPort1.Write(data, 0, 2); //escreve o vetor de 2 bytes na saida serial

            }
            textBoxReceber.AppendText("\n\n == Comando enviado >> \n");
            textBoxReceber.AppendText("cmd: 0008 \n");
            textBoxReceber.AppendText("valor: " + lbl_veloDC.Text + "\n\n");
        }

        private void btn_esq_MouseDown(object sender, MouseEventArgs e)
        {
            //cliclou no botao para esquerda: envia comando para motores dc 
            // cmd:
            // 0009 # motores dc para esquerda (lados alternados)
            // retorno: nao
            // valor: sim (velocidade)

            //                        cmd  valor
            byte temp = byte.Parse(lbl_veloDC.Text);
            byte[] data = new byte[] { 0009, temp };

            if (serialPort1.IsOpen == true)
            {//porta está aberta
                serialPort1.Write(data, 0, 2); //escreve o vetor de 2 bytes na saida serial

            }
            textBoxReceber.AppendText("\n\n == Comando enviado >> \n");
            textBoxReceber.AppendText("cmd: 0009 \n");
            textBoxReceber.AppendText("valor: " + lbl_veloDC.Text + "\n\n");
        }

        private void btn_esq_MouseUp(object sender, MouseEventArgs e)
        {
            //enviar comando para parar
            byte[] data = new byte[] { 0007, 0000 };

            if (serialPort1.IsOpen == true)
            {//porta está aberta
                serialPort1.Write(data, 0, 2); //escreve o vetor de 2 bytes na saida serial

            }
            textBoxReceber.AppendText("\n\n == Comando enviado >> \n");
            textBoxReceber.AppendText("cmd: 0007 \n");
            textBoxReceber.AppendText("valor: 0000 \n\n");
        }

        private void btn_dir_MouseUp(object sender, MouseEventArgs e)
        {
            //enviar comando para parar
            byte[] data = new byte[] { 0007, 0000 };

            if (serialPort1.IsOpen == true)
            {//porta está aberta
                serialPort1.Write(data, 0, 2); //escreve o vetor de 2 bytes na saida serial

            }
            textBoxReceber.AppendText("\n\n == Comando enviado >> \n");
            textBoxReceber.AppendText("cmd: 0007 \n");
            textBoxReceber.AppendText("valor: 0000 \n\n");
        }

        private void btn_esq_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // 0016 e 0017 # Acionar laser
            // retorno: nao
            // valor: nao

            // cmd  = 0016
            //valor = 0000

            //                        cmd  valor

            if (this.status_laser == 1)
            {
                //inverter para desligado
                status_laser = 0;
                byte[] data = new byte[] { 16, 0 };
                lbl_laser.Text = "OFF";
                lbl_laser.ForeColor = System.Drawing.Color.Red;

                if (serialPort1.IsOpen == true)
                {//porta está aberta
                    serialPort1.Write(data, 0, 2); //escreve o vetor de 2 bytes na saida serial

                    //reportar comando enviado no prompt de comando
                    textBoxReceber.AppendText("\n\n == Comando enviado >> \n");
                    textBoxReceber.AppendText("cmd: 0016 \n");
                    textBoxReceber.AppendText("valor: 0000 \n\n");
                }

            }
            else if (this.status_laser == 0)
            {
                //inverter para ligado
                status_laser = 1;
                byte[] data = new byte[] { 17, 0000 };
                lbl_laser.Text = "ON";
                lbl_laser.ForeColor = System.Drawing.Color.Green;

                if (serialPort1.IsOpen == true)
                {//porta está aberta
                    serialPort1.Write(data, 0, 2); //escreve o vetor de 2 bytes na saida serial

                    //reportar comando enviado no prompt de comando
                    textBoxReceber.AppendText("\n\n == Comando enviado >> \n");
                    textBoxReceber.AppendText("cmd: 0017 \n");
                    textBoxReceber.AppendText("valor: 0000 \n\n");
                }

            }

        }

        private void btnM1mais_MouseUp(object sender, MouseEventArgs e)
        {
            //envia sinal para parar a movimentacao

        }

        private void btn_display_Click(object sender, EventArgs e)
        {
            //comando para operar o display lcd
            char[] strArr = txt_display_1.Text.ToCharArray();

            //textBoxReceber.AppendText(strArr[0].ToString());
            int tam = strArr.Length; //quantidade de caracteres

            //textBoxReceber.AppendText(tam.ToString());

            byte[] data = new byte[] { 0020, 0000 };

            for (int i = 0; i < tam; i++)
            {
                //envia caractere por caractere para o display lcd
                data[0] = 0020;
                data[1] = Convert.ToByte(strArr[i]); //converte 1 caractere para byte e envia

                if (serialPort1.IsOpen == true)
                {//porta está aberta
                    serialPort1.Write(data, 0, 2); //escreve o vetor de 2 bytes na saida serial

                }
                textBoxReceber.AppendText("\n\n == Comando enviado >> \n");
                textBoxReceber.AppendText("cmd: 0020 \n");
                textBoxReceber.AppendText("valor: " + data[1].ToString() + " \n\n");
            }

            /*
            byte[] data = new byte[] { 0020, 0001 };

            if (serialPort1.IsOpen == true)
            {//porta está aberta
                serialPort1.Write(data, 0, 2); //escreve o vetor de 2 bytes na saida serial

            }
            textBoxReceber.AppendText("\n\n == Comando enviado >> \n");
            textBoxReceber.AppendText("cmd: 0020 \n");
            textBoxReceber.AppendText("valor: 0001 \n\n");
            */
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //limpar display
            byte[] data = new byte[] { 0020, 0000 };

                if (serialPort1.IsOpen == true)
                {//porta está aberta
                    serialPort1.Write(data, 0, 2); //escreve o vetor de 2 bytes na saida serial

                }
                textBoxReceber.AppendText("\n\n == Comando enviado >> \n");
                textBoxReceber.AppendText("cmd: 0020 \n");
                textBoxReceber.AppendText("valor: 0000 \n\n");

             txt_display_1.Clear();


        }
    }
}
