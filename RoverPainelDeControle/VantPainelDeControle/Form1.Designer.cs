namespace VantPainelDeControle
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.btConectar = new System.Windows.Forms.Button();
            this.btnCalibrarMotores = new System.Windows.Forms.GroupBox();
            this.lbl_veloDC = new System.Windows.Forms.TextBox();
            this.button10 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btn_tras = new System.Windows.Forms.Button();
            this.btn_frente = new System.Windows.Forms.Button();
            this.btn_esq = new System.Windows.Forms.Button();
            this.btn_dir = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.lbl_temperatura = new System.Windows.Forms.TextBox();
            this.btnConexaoRemota = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.opçõesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fecharToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sobreToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.timerCOM = new System.Windows.Forms.Timer(this.components);
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txt_intervalo_angulo = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.btn_display = new System.Windows.Forms.Button();
            this.txt_display_1 = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btn_fechar = new System.Windows.Forms.Button();
            this.btn_abrir = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnM4menos = new System.Windows.Forms.Button();
            this.btnM4mais = new System.Windows.Forms.Button();
            this.btnM2menos = new System.Windows.Forms.Button();
            this.btnM2mais = new System.Windows.Forms.Button();
            this.btnM3menos = new System.Windows.Forms.Button();
            this.btnM3mais = new System.Windows.Forms.Button();
            this.btnM1menos = new System.Windows.Forms.Button();
            this.btnM1mais = new System.Windows.Forms.Button();
            this.btnM4 = new System.Windows.Forms.Button();
            this.btnM3 = new System.Windows.Forms.Button();
            this.btnM2 = new System.Windows.Forms.Button();
            this.btnM1 = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.lblM4 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.lblM3 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.lblM2 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.lblM1 = new System.Windows.Forms.TextBox();
            this.btnBuzzer = new System.Windows.Forms.Button();
            this.lblStatusBuzzer = new System.Windows.Forms.Label();
            this.lblStatusConexao = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.lblNivelBateria = new System.Windows.Forms.Label();
            this.textBoxReceber = new System.Windows.Forms.TextBox();
            this.button25 = new System.Windows.Forms.Button();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.btnAttBateria = new System.Windows.Forms.Button();
            this.timerStatusBateria = new System.Windows.Forms.Timer(this.components);
            this.timerStatusConexao = new System.Windows.Forms.Timer(this.components);
            this.lbl_laser = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.btnCalibrarMotores.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(93, 34);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(119, 21);
            this.comboBox1.TabIndex = 6;
            // 
            // btConectar
            // 
            this.btConectar.Location = new System.Drawing.Point(12, 32);
            this.btConectar.Name = "btConectar";
            this.btConectar.Size = new System.Drawing.Size(75, 23);
            this.btConectar.TabIndex = 5;
            this.btConectar.Text = "Conectar";
            this.btConectar.UseVisualStyleBackColor = true;
            this.btConectar.Click += new System.EventHandler(this.button9_Click);
            // 
            // btnCalibrarMotores
            // 
            this.btnCalibrarMotores.Controls.Add(this.lbl_veloDC);
            this.btnCalibrarMotores.Controls.Add(this.button10);
            this.btnCalibrarMotores.Controls.Add(this.label3);
            this.btnCalibrarMotores.Controls.Add(this.label2);
            this.btnCalibrarMotores.Controls.Add(this.btn_tras);
            this.btnCalibrarMotores.Controls.Add(this.btn_frente);
            this.btnCalibrarMotores.Controls.Add(this.btn_esq);
            this.btnCalibrarMotores.Controls.Add(this.btn_dir);
            this.btnCalibrarMotores.Controls.Add(this.label8);
            this.btnCalibrarMotores.Controls.Add(this.lbl_temperatura);
            this.btnCalibrarMotores.Location = new System.Drawing.Point(232, 89);
            this.btnCalibrarMotores.Name = "btnCalibrarMotores";
            this.btnCalibrarMotores.Size = new System.Drawing.Size(446, 542);
            this.btnCalibrarMotores.TabIndex = 4;
            this.btnCalibrarMotores.TabStop = false;
            this.btnCalibrarMotores.Text = "Controle de direção";
            // 
            // lbl_veloDC
            // 
            this.lbl_veloDC.Location = new System.Drawing.Point(202, 259);
            this.lbl_veloDC.Name = "lbl_veloDC";
            this.lbl_veloDC.Size = new System.Drawing.Size(54, 20);
            this.lbl_veloDC.TabIndex = 51;
            this.lbl_veloDC.Text = "150";
            this.lbl_veloDC.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // button10
            // 
            this.button10.Location = new System.Drawing.Point(36, 477);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(387, 29);
            this.button10.TabIndex = 42;
            this.button10.Text = "parar motores";
            this.button10.UseVisualStyleBackColor = true;
            this.button10.Click += new System.EventHandler(this.button10_Click_1);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(199, 442);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Terminal SUL";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(182, 85);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Terminal NORTE";
            // 
            // btn_tras
            // 
            this.btn_tras.Image = global::VantPainelDeControle.Properties.Resources.setaTraN;
            this.btn_tras.Location = new System.Drawing.Point(170, 330);
            this.btn_tras.Name = "btn_tras";
            this.btn_tras.Size = new System.Drawing.Size(121, 95);
            this.btn_tras.TabIndex = 7;
            this.btn_tras.Text = "tras";
            this.btn_tras.UseVisualStyleBackColor = true;
            this.btn_tras.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btn_tras_MouseDown);
            this.btn_tras.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btn_tras_MouseUp);
            // 
            // btn_frente
            // 
            this.btn_frente.Image = global::VantPainelDeControle.Properties.Resources.setaCimN;
            this.btn_frente.Location = new System.Drawing.Point(170, 122);
            this.btn_frente.Name = "btn_frente";
            this.btn_frente.Size = new System.Drawing.Size(121, 96);
            this.btn_frente.TabIndex = 6;
            this.btn_frente.Text = "frente";
            this.btn_frente.UseVisualStyleBackColor = true;
            this.btn_frente.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btn_frente_MouseDown);
            this.btn_frente.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btn_frente_MouseUp);
            // 
            // btn_esq
            // 
            this.btn_esq.Image = global::VantPainelDeControle.Properties.Resources.setaEsqN;
            this.btn_esq.Location = new System.Drawing.Point(36, 226);
            this.btn_esq.Name = "btn_esq";
            this.btn_esq.Size = new System.Drawing.Size(121, 85);
            this.btn_esq.TabIndex = 5;
            this.btn_esq.Text = "esquerda";
            this.btn_esq.UseVisualStyleBackColor = true;
            this.btn_esq.Click += new System.EventHandler(this.btn_esq_Click);
            this.btn_esq.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btn_esq_MouseDown);
            this.btn_esq.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btn_esq_MouseUp);
            // 
            // btn_dir
            // 
            this.btn_dir.Image = global::VantPainelDeControle.Properties.Resources.setaDirN;
            this.btn_dir.Location = new System.Drawing.Point(302, 226);
            this.btn_dir.Name = "btn_dir";
            this.btn_dir.Size = new System.Drawing.Size(121, 85);
            this.btn_dir.TabIndex = 4;
            this.btn_dir.Text = "direita";
            this.btn_dir.UseVisualStyleBackColor = true;
            this.btn_dir.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btn_dir_MouseDown);
            this.btn_dir.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btn_dir_MouseUp);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(15, 34);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(70, 13);
            this.label8.TabIndex = 37;
            this.label8.Text = "Temperatura:";
            this.label8.Click += new System.EventHandler(this.label8_Click);
            // 
            // lbl_temperatura
            // 
            this.lbl_temperatura.Location = new System.Drawing.Point(97, 31);
            this.lbl_temperatura.Name = "lbl_temperatura";
            this.lbl_temperatura.ReadOnly = true;
            this.lbl_temperatura.Size = new System.Drawing.Size(67, 20);
            this.lbl_temperatura.TabIndex = 36;
            // 
            // btnConexaoRemota
            // 
            this.btnConexaoRemota.Location = new System.Drawing.Point(563, 28);
            this.btnConexaoRemota.Name = "btnConexaoRemota";
            this.btnConexaoRemota.Size = new System.Drawing.Size(64, 44);
            this.btnConexaoRemota.TabIndex = 4;
            this.btnConexaoRemota.Text = "Conexão remota";
            this.btnConexaoRemota.UseVisualStyleBackColor = true;
            this.btnConexaoRemota.Click += new System.EventHandler(this.button10_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.opçõesToolStripMenuItem,
            this.sobreToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1125, 24);
            this.menuStrip1.TabIndex = 7;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // opçõesToolStripMenuItem
            // 
            this.opçõesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fecharToolStripMenuItem});
            this.opçõesToolStripMenuItem.Name = "opçõesToolStripMenuItem";
            this.opçõesToolStripMenuItem.Size = new System.Drawing.Size(59, 20);
            this.opçõesToolStripMenuItem.Text = "Opções";
            // 
            // fecharToolStripMenuItem
            // 
            this.fecharToolStripMenuItem.Name = "fecharToolStripMenuItem";
            this.fecharToolStripMenuItem.Size = new System.Drawing.Size(109, 22);
            this.fecharToolStripMenuItem.Text = "Fechar";
            // 
            // sobreToolStripMenuItem
            // 
            this.sobreToolStripMenuItem.Name = "sobreToolStripMenuItem";
            this.sobreToolStripMenuItem.Size = new System.Drawing.Size(49, 20);
            this.sobreToolStripMenuItem.Text = "Sobre";
            // 
            // serialPort1
            // 
            this.serialPort1.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPort1_DataReceived);
            // 
            // timerCOM
            // 
            this.timerCOM.Tick += new System.EventHandler(this.timerCOM_Tick_1);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txt_intervalo_angulo);
            this.groupBox2.Controls.Add(this.button2);
            this.groupBox2.Controls.Add(this.btn_display);
            this.groupBox2.Controls.Add(this.txt_display_1);
            this.groupBox2.Controls.Add(this.pictureBox1);
            this.groupBox2.Controls.Add(this.btn_fechar);
            this.groupBox2.Controls.Add(this.btn_abrir);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.btnM4menos);
            this.groupBox2.Controls.Add(this.btnM4mais);
            this.groupBox2.Controls.Add(this.btnM2menos);
            this.groupBox2.Controls.Add(this.btnM2mais);
            this.groupBox2.Controls.Add(this.btnM3menos);
            this.groupBox2.Controls.Add(this.btnM3mais);
            this.groupBox2.Controls.Add(this.btnM1menos);
            this.groupBox2.Controls.Add(this.btnM1mais);
            this.groupBox2.Controls.Add(this.btnM4);
            this.groupBox2.Controls.Add(this.btnM3);
            this.groupBox2.Controls.Add(this.btnM2);
            this.groupBox2.Controls.Add(this.btnM1);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.lblM4);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.lblM3);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.lblM2);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.lblM1);
            this.groupBox2.Location = new System.Drawing.Point(699, 89);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(414, 542);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Sensores";
            // 
            // txt_intervalo_angulo
            // 
            this.txt_intervalo_angulo.Location = new System.Drawing.Point(161, 223);
            this.txt_intervalo_angulo.Name = "txt_intervalo_angulo";
            this.txt_intervalo_angulo.Size = new System.Drawing.Size(48, 20);
            this.txt_intervalo_angulo.TabIndex = 54;
            this.txt_intervalo_angulo.Text = "5";
            this.txt_intervalo_angulo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(166, 159);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(119, 32);
            this.button2.TabIndex = 53;
            this.button2.Text = "Limpar display";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // btn_display
            // 
            this.btn_display.Location = new System.Drawing.Point(41, 159);
            this.btn_display.Name = "btn_display";
            this.btn_display.Size = new System.Drawing.Size(119, 32);
            this.btn_display.TabIndex = 52;
            this.btn_display.Text = "Alterar texto display";
            this.btn_display.UseVisualStyleBackColor = true;
            this.btn_display.Click += new System.EventHandler(this.btn_display_Click);
            // 
            // txt_display_1
            // 
            this.txt_display_1.BackColor = System.Drawing.Color.White;
            this.txt_display_1.Location = new System.Drawing.Point(75, 82);
            this.txt_display_1.Name = "txt_display_1";
            this.txt_display_1.Size = new System.Drawing.Size(272, 20);
            this.txt_display_1.TabIndex = 42;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(41, 31);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(334, 122);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 50;
            this.pictureBox1.TabStop = false;
            // 
            // btn_fechar
            // 
            this.btn_fechar.Location = new System.Drawing.Point(316, 429);
            this.btn_fechar.Name = "btn_fechar";
            this.btn_fechar.Size = new System.Drawing.Size(75, 23);
            this.btn_fechar.TabIndex = 49;
            this.btn_fechar.Text = "fechar";
            this.btn_fechar.UseVisualStyleBackColor = true;
            this.btn_fechar.Click += new System.EventHandler(this.btn_fechar_Click);
            // 
            // btn_abrir
            // 
            this.btn_abrir.Location = new System.Drawing.Point(316, 400);
            this.btn_abrir.Name = "btn_abrir";
            this.btn_abrir.Size = new System.Drawing.Size(75, 23);
            this.btn_abrir.TabIndex = 42;
            this.btn_abrir.Text = "abrir";
            this.btn_abrir.UseVisualStyleBackColor = true;
            this.btn_abrir.Click += new System.EventHandler(this.btn_abrir_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 226);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(138, 13);
            this.label1.TabIndex = 48;
            this.label1.Text = "Controle dos servo motores:";
            // 
            // btnM4menos
            // 
            this.btnM4menos.Location = new System.Drawing.Point(156, 492);
            this.btnM4menos.Name = "btnM4menos";
            this.btnM4menos.Size = new System.Drawing.Size(34, 23);
            this.btnM4menos.TabIndex = 31;
            this.btnM4menos.Text = "-";
            this.btnM4menos.UseVisualStyleBackColor = true;
            this.btnM4menos.Click += new System.EventHandler(this.btnM4menos_Click);
            // 
            // btnM4mais
            // 
            this.btnM4mais.Location = new System.Drawing.Point(189, 492);
            this.btnM4mais.Name = "btnM4mais";
            this.btnM4mais.Size = new System.Drawing.Size(34, 23);
            this.btnM4mais.TabIndex = 30;
            this.btnM4mais.Text = "+";
            this.btnM4mais.UseVisualStyleBackColor = true;
            this.btnM4mais.Click += new System.EventHandler(this.btnM4mais_Click);
            // 
            // btnM2menos
            // 
            this.btnM2menos.Location = new System.Drawing.Point(156, 353);
            this.btnM2menos.Name = "btnM2menos";
            this.btnM2menos.Size = new System.Drawing.Size(34, 23);
            this.btnM2menos.TabIndex = 29;
            this.btnM2menos.Text = "-";
            this.btnM2menos.UseVisualStyleBackColor = true;
            this.btnM2menos.Click += new System.EventHandler(this.btnM2menos_Click);
            // 
            // btnM2mais
            // 
            this.btnM2mais.Location = new System.Drawing.Point(189, 353);
            this.btnM2mais.Name = "btnM2mais";
            this.btnM2mais.Size = new System.Drawing.Size(34, 23);
            this.btnM2mais.TabIndex = 28;
            this.btnM2mais.Text = "+";
            this.btnM2mais.UseVisualStyleBackColor = true;
            this.btnM2mais.Click += new System.EventHandler(this.btnM2mais_Click);
            // 
            // btnM3menos
            // 
            this.btnM3menos.Location = new System.Drawing.Point(156, 422);
            this.btnM3menos.Name = "btnM3menos";
            this.btnM3menos.Size = new System.Drawing.Size(34, 23);
            this.btnM3menos.TabIndex = 27;
            this.btnM3menos.Text = "-";
            this.btnM3menos.UseVisualStyleBackColor = true;
            this.btnM3menos.Click += new System.EventHandler(this.btnM3menos_Click);
            // 
            // btnM3mais
            // 
            this.btnM3mais.Location = new System.Drawing.Point(189, 422);
            this.btnM3mais.Name = "btnM3mais";
            this.btnM3mais.Size = new System.Drawing.Size(34, 23);
            this.btnM3mais.TabIndex = 26;
            this.btnM3mais.Text = "+";
            this.btnM3mais.UseVisualStyleBackColor = true;
            this.btnM3mais.Click += new System.EventHandler(this.btnM3mais_Click);
            // 
            // btnM1menos
            // 
            this.btnM1menos.Location = new System.Drawing.Point(156, 282);
            this.btnM1menos.Name = "btnM1menos";
            this.btnM1menos.Size = new System.Drawing.Size(34, 23);
            this.btnM1menos.TabIndex = 25;
            this.btnM1menos.Text = "-";
            this.btnM1menos.UseVisualStyleBackColor = true;
            this.btnM1menos.Click += new System.EventHandler(this.btnM1menos_Click);
            // 
            // btnM1mais
            // 
            this.btnM1mais.Location = new System.Drawing.Point(189, 282);
            this.btnM1mais.Name = "btnM1mais";
            this.btnM1mais.Size = new System.Drawing.Size(34, 23);
            this.btnM1mais.TabIndex = 24;
            this.btnM1mais.Text = "+";
            this.btnM1mais.UseVisualStyleBackColor = true;
            this.btnM1mais.Click += new System.EventHandler(this.btnM1mais_Click);
            this.btnM1mais.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnM1mais_MouseUp);
            // 
            // btnM4
            // 
            this.btnM4.Location = new System.Drawing.Point(229, 467);
            this.btnM4.Name = "btnM4";
            this.btnM4.Size = new System.Drawing.Size(41, 48);
            this.btnM4.TabIndex = 23;
            this.btnM4.Text = "set";
            this.btnM4.UseVisualStyleBackColor = true;
            this.btnM4.Click += new System.EventHandler(this.btnM4_Click);
            // 
            // btnM3
            // 
            this.btnM3.Location = new System.Drawing.Point(229, 397);
            this.btnM3.Name = "btnM3";
            this.btnM3.Size = new System.Drawing.Size(41, 48);
            this.btnM3.TabIndex = 22;
            this.btnM3.Text = "set";
            this.btnM3.UseVisualStyleBackColor = true;
            this.btnM3.Click += new System.EventHandler(this.btnM3_Click);
            // 
            // btnM2
            // 
            this.btnM2.Location = new System.Drawing.Point(229, 324);
            this.btnM2.Name = "btnM2";
            this.btnM2.Size = new System.Drawing.Size(41, 52);
            this.btnM2.TabIndex = 21;
            this.btnM2.Text = "set";
            this.btnM2.UseVisualStyleBackColor = true;
            this.btnM2.Click += new System.EventHandler(this.btnM2_Click);
            // 
            // btnM1
            // 
            this.btnM1.Location = new System.Drawing.Point(229, 253);
            this.btnM1.Name = "btnM1";
            this.btnM1.Size = new System.Drawing.Size(41, 52);
            this.btnM1.TabIndex = 20;
            this.btnM1.Text = "set";
            this.btnM1.UseVisualStyleBackColor = true;
            this.btnM1.Click += new System.EventHandler(this.btnM1_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(38, 473);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(86, 13);
            this.label7.TabIndex = 19;
            this.label7.Text = "Motor 4 : (direita)";
            // 
            // lblM4
            // 
            this.lblM4.Location = new System.Drawing.Point(156, 470);
            this.lblM4.Name = "lblM4";
            this.lblM4.Size = new System.Drawing.Size(67, 20);
            this.lblM4.TabIndex = 18;
            this.lblM4.Text = "90";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(38, 403);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(82, 13);
            this.label6.TabIndex = 17;
            this.label6.Text = "Motor 3 : (garra)";
            // 
            // lblM3
            // 
            this.lblM3.Location = new System.Drawing.Point(156, 400);
            this.lblM3.Name = "lblM3";
            this.lblM3.Size = new System.Drawing.Size(67, 20);
            this.lblM3.TabIndex = 16;
            this.lblM3.Text = "90";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(38, 330);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(102, 13);
            this.label5.TabIndex = 15;
            this.label5.Text = "Motor 2 : (esquerda)";
            // 
            // lblM2
            // 
            this.lblM2.Location = new System.Drawing.Point(156, 327);
            this.lblM2.Name = "lblM2";
            this.lblM2.Size = new System.Drawing.Size(67, 20);
            this.lblM2.TabIndex = 14;
            this.lblM2.Text = "90";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(38, 259);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(81, 13);
            this.label4.TabIndex = 13;
            this.label4.Text = "Motor 1 : (base)";
            // 
            // lblM1
            // 
            this.lblM1.Location = new System.Drawing.Point(156, 256);
            this.lblM1.Name = "lblM1";
            this.lblM1.Size = new System.Drawing.Size(67, 20);
            this.lblM1.TabIndex = 10;
            this.lblM1.Text = "90";
            // 
            // btnBuzzer
            // 
            this.btnBuzzer.Location = new System.Drawing.Point(726, 28);
            this.btnBuzzer.Name = "btnBuzzer";
            this.btnBuzzer.Size = new System.Drawing.Size(64, 44);
            this.btnBuzzer.TabIndex = 13;
            this.btnBuzzer.Text = "Alarme [buzzer]";
            this.btnBuzzer.UseVisualStyleBackColor = true;
            this.btnBuzzer.Click += new System.EventHandler(this.btnBuzzer_Click);
            // 
            // lblStatusBuzzer
            // 
            this.lblStatusBuzzer.AutoSize = true;
            this.lblStatusBuzzer.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatusBuzzer.ForeColor = System.Drawing.Color.Red;
            this.lblStatusBuzzer.Location = new System.Drawing.Point(796, 37);
            this.lblStatusBuzzer.Name = "lblStatusBuzzer";
            this.lblStatusBuzzer.Size = new System.Drawing.Size(49, 24);
            this.lblStatusBuzzer.TabIndex = 14;
            this.lblStatusBuzzer.Text = "OFF";
            // 
            // lblStatusConexao
            // 
            this.lblStatusConexao.AutoSize = true;
            this.lblStatusConexao.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatusConexao.ForeColor = System.Drawing.Color.Red;
            this.lblStatusConexao.Location = new System.Drawing.Point(635, 36);
            this.lblStatusConexao.Name = "lblStatusConexao";
            this.lblStatusConexao.Size = new System.Drawing.Size(49, 24);
            this.lblStatusConexao.TabIndex = 12;
            this.lblStatusConexao.Text = "OFF";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(278, 37);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(74, 13);
            this.label9.TabIndex = 38;
            this.label9.Text = "Nível bateria :";
            // 
            // lblNivelBateria
            // 
            this.lblNivelBateria.AutoSize = true;
            this.lblNivelBateria.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNivelBateria.Location = new System.Drawing.Point(358, 32);
            this.lblNivelBateria.Name = "lblNivelBateria";
            this.lblNivelBateria.Size = new System.Drawing.Size(114, 24);
            this.lblNivelBateria.TabIndex = 39;
            this.lblNivelBateria.Text = "Analisando..";
            // 
            // textBoxReceber
            // 
            this.textBoxReceber.Enabled = false;
            this.textBoxReceber.Location = new System.Drawing.Point(12, 61);
            this.textBoxReceber.Multiline = true;
            this.textBoxReceber.Name = "textBoxReceber";
            this.textBoxReceber.Size = new System.Drawing.Size(200, 520);
            this.textBoxReceber.TabIndex = 10;
            // 
            // button25
            // 
            this.button25.Location = new System.Drawing.Point(12, 602);
            this.button25.Name = "button25";
            this.button25.Size = new System.Drawing.Size(200, 29);
            this.button25.TabIndex = 40;
            this.button25.Text = "Limpar prompt";
            this.button25.UseVisualStyleBackColor = true;
            this.button25.Click += new System.EventHandler(this.button25_Click);
            // 
            // btnAttBateria
            // 
            this.btnAttBateria.Location = new System.Drawing.Point(232, 32);
            this.btnAttBateria.Name = "btnAttBateria";
            this.btnAttBateria.Size = new System.Drawing.Size(40, 23);
            this.btnAttBateria.TabIndex = 41;
            this.btnAttBateria.Text = "Att";
            this.btnAttBateria.UseVisualStyleBackColor = true;
            this.btnAttBateria.Click += new System.EventHandler(this.btnAttBateria_Click);
            // 
            // timerStatusBateria
            // 
            this.timerStatusBateria.Interval = 10000;
            this.timerStatusBateria.Tick += new System.EventHandler(this.timerStatusBateria_Tick);
            // 
            // timerStatusConexao
            // 
            this.timerStatusConexao.Interval = 3000;
            this.timerStatusConexao.Tick += new System.EventHandler(this.timerStatusConexao_Tick);
            // 
            // lbl_laser
            // 
            this.lbl_laser.AutoSize = true;
            this.lbl_laser.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_laser.ForeColor = System.Drawing.Color.Red;
            this.lbl_laser.Location = new System.Drawing.Point(944, 37);
            this.lbl_laser.Name = "lbl_laser";
            this.lbl_laser.Size = new System.Drawing.Size(49, 24);
            this.lbl_laser.TabIndex = 43;
            this.lbl_laser.Text = "OFF";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(874, 28);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(64, 44);
            this.button1.TabIndex = 42;
            this.button1.Text = "Laser";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1125, 653);
            this.Controls.Add(this.lbl_laser);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnAttBateria);
            this.Controls.Add(this.button25);
            this.Controls.Add(this.lblNivelBateria);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.textBoxReceber);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.btConectar);
            this.Controls.Add(this.btnCalibrarMotores);
            this.Controls.Add(this.lblStatusBuzzer);
            this.Controls.Add(this.btnBuzzer);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.btnConexaoRemota);
            this.Controls.Add(this.lblStatusConexao);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Painel de controle - ROVER - Trabalho final MIC 2016/2";
            this.btnCalibrarMotores.ResumeLayout(false);
            this.btnCalibrarMotores.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button btConectar;
        private System.Windows.Forms.GroupBox btnCalibrarMotores;
        private System.Windows.Forms.Button btnConexaoRemota;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btn_tras;
        private System.Windows.Forms.Button btn_frente;
        private System.Windows.Forms.Button btn_esq;
        private System.Windows.Forms.Button btn_dir;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem opçõesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sobreToolStripMenuItem;
        private System.IO.Ports.SerialPort serialPort1;
        private System.Windows.Forms.Timer timerCOM;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lblStatusConexao;
        private System.Windows.Forms.Label lblNivelBateria;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ToolStripMenuItem fecharToolStripMenuItem;
        private System.Windows.Forms.TextBox lbl_temperatura;
        private System.Windows.Forms.Button btnM4menos;
        private System.Windows.Forms.Button btnM4mais;
        private System.Windows.Forms.Button btnM2menos;
        private System.Windows.Forms.Button btnM2mais;
        private System.Windows.Forms.Button btnM3menos;
        private System.Windows.Forms.Button btnM3mais;
        private System.Windows.Forms.Button btnM1menos;
        private System.Windows.Forms.Button btnM1mais;
        private System.Windows.Forms.Button btnM4;
        private System.Windows.Forms.Button btnM3;
        private System.Windows.Forms.Button btnM2;
        private System.Windows.Forms.Button btnM1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox lblM4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox lblM3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox lblM2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox lblM1;
        private System.Windows.Forms.Button btnBuzzer;
        private System.Windows.Forms.Label lblStatusBuzzer;
        private System.Windows.Forms.TextBox textBoxReceber;
        private System.Windows.Forms.Button button25;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.Button btnAttBateria;
        private System.Windows.Forms.Timer timerStatusBateria;
        private System.Windows.Forms.Timer timerStatusConexao;
        private System.Windows.Forms.Button button10;
        private System.Windows.Forms.TextBox lbl_veloDC;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_fechar;
        private System.Windows.Forms.Button btn_abrir;
        private System.Windows.Forms.Button btn_display;
        private System.Windows.Forms.TextBox txt_display_1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lbl_laser;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox txt_intervalo_angulo;
    }
}

