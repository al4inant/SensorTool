namespace WindowsFormsApplication
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
            this.SERVICE_MODE_button = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.button_SAVE_BLOCK67 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.button9 = new System.Windows.Forms.Button();
            this.button_V = new System.Windows.Forms.Button();
            this.button17 = new System.Windows.Forms.Button();
            this.button18 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.button_VBAT_CALDATA_WRITE = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.button_VBAT_CALK_CONSTANT = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.button_VBAT_ADCREAD = new System.Windows.Forms.Button();
            this.button_VBAT_CALDATA_READ = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.button_AKKU_TYP_CALDATA_READ = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.button_AKKU_TYP_ADCREAD = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.button_AKKU_TYP_CALK_CONSTANT = new System.Windows.Forms.Button();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.textBox7 = new System.Windows.Forms.TextBox();
            this.button_AKKU_TYP_CALDATA_WRITE = new System.Windows.Forms.Button();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.button_TVCXO_CALDATA_WRITE = new System.Windows.Forms.Button();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.textBox8 = new System.Windows.Forms.TextBox();
            this.button_TVCXO_CALK_CONSTANT = new System.Windows.Forms.Button();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.textBox9 = new System.Windows.Forms.TextBox();
            this.button_TVCXO_ADCREAD = new System.Windows.Forms.Button();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.textBox10 = new System.Windows.Forms.TextBox();
            this.button_TVCXO_CALDATA_READ = new System.Windows.Forms.Button();
            this.groupBox10 = new System.Windows.Forms.GroupBox();
            this.groupBox12 = new System.Windows.Forms.GroupBox();
            this.button_R = new System.Windows.Forms.Button();
            this.button_T = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.groupBox9.SuspendLayout();
            this.groupBox10.SuspendLayout();
            this.groupBox12.SuspendLayout();
            this.SuspendLayout();
            // 
            // SERVICE_MODE_button
            // 
            this.SERVICE_MODE_button.Location = new System.Drawing.Point(147, 14);
            this.SERVICE_MODE_button.Name = "SERVICE_MODE_button";
            this.SERVICE_MODE_button.Size = new System.Drawing.Size(120, 21);
            this.SERVICE_MODE_button.TabIndex = 13;
            this.SERVICE_MODE_button.Text = "В сервисный режим";
            this.SERVICE_MODE_button.UseVisualStyleBackColor = true;
            this.SERVICE_MODE_button.Click += new System.EventHandler(this.SERVICE_MODE_button_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(99, 15);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(90, 40);
            this.button4.TabIndex = 14;
            this.button4.Text = "Вкл. телефон";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.SrvToNorm_button_Click);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(4, 15);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(90, 40);
            this.button6.TabIndex = 15;
            this.button6.Text = "Симуляция SIM карты";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.SimSim_button_Click);
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(195, 15);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(76, 40);
            this.button7.TabIndex = 16;
            this.button7.Text = "Выкл. телефон";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.SwitchMobileOff_button_Click);
            // 
            // button_SAVE_BLOCK67
            // 
            this.button_SAVE_BLOCK67.Location = new System.Drawing.Point(406, 14);
            this.button_SAVE_BLOCK67.Name = "button_SAVE_BLOCK67";
            this.button_SAVE_BLOCK67.Size = new System.Drawing.Size(98, 21);
            this.button_SAVE_BLOCK67.TabIndex = 22;
            this.button_SAVE_BLOCK67.Text = "Бэкап блока 67";
            this.button_SAVE_BLOCK67.UseVisualStyleBackColor = true;
            this.button_SAVE_BLOCK67.Click += new System.EventHandler(this.SAVE_BLOCK67_button_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Transparent;
            this.button1.Location = new System.Drawing.Point(71, 14);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(70, 21);
            this.button1.TabIndex = 1;
            this.button1.Text = "Открыть";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.OpenCloseCom_button_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.Location = new System.Drawing.Point(6, 14);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(59, 21);
            this.comboBox1.TabIndex = 0;
            this.comboBox1.Tag = "";
            this.comboBox1.Click += new System.EventHandler(this.comboBox1_Click);
            // 
            // button9
            // 
            this.button9.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.button9.Location = new System.Drawing.Point(273, 14);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(127, 21);
            this.button9.TabIndex = 20;
            this.button9.Text = "Вкл/Выкл. подсветку";
            this.button9.UseVisualStyleBackColor = true;
            this.button9.Click += new System.EventHandler(this.button9_Click);
            // 
            // button_V
            // 
            this.button_V.AccessibleDescription = "";
            this.button_V.AccessibleName = "";
            this.button_V.BackColor = System.Drawing.Color.Transparent;
            this.button_V.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.button_V.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button_V.ForeColor = System.Drawing.SystemColors.ControlText;
            this.button_V.Location = new System.Drawing.Point(371, 15);
            this.button_V.Name = "button_V";
            this.button_V.Size = new System.Drawing.Size(32, 40);
            this.button_V.TabIndex = 26;
            this.button_V.Tag = "Напряжение аккумулятора";
            this.button_V.Text = "V";
            this.button_V.UseVisualStyleBackColor = false;
            this.button_V.Click += new System.EventHandler(this.Read_VRT_button_Click);
            // 
            // button17
            // 
            this.button17.Location = new System.Drawing.Point(277, 15);
            this.button17.Name = "button17";
            this.button17.Size = new System.Drawing.Size(90, 40);
            this.button17.TabIndex = 27;
            this.button17.Text = "Запись блока 67 в телефон";
            this.button17.UseVisualStyleBackColor = true;
            this.button17.Click += new System.EventHandler(this.WRITE_BLOCK67_button_Click);
            // 
            // button18
            // 
            this.button18.Location = new System.Drawing.Point(480, 15);
            this.button18.Name = "button18";
            this.button18.Size = new System.Drawing.Size(90, 40);
            this.button18.TabIndex = 28;
            this.button18.Text = "Тест функций";
            this.button18.UseVisualStyleBackColor = true;
            this.button18.Click += new System.EventHandler(this.Test_button_Click);
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.textBox1.Location = new System.Drawing.Point(299, 50);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(286, 224);
            this.textBox1.TabIndex = 30;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(510, 14);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(62, 21);
            this.button2.TabIndex = 31;
            this.button2.Text = "Инфо";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.About_button_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(8, 50);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(284, 224);
            this.tabControl1.TabIndex = 24;
            this.tabControl1.Selected += new System.Windows.Forms.TabControlEventHandler(this.tabControl1_Selected);
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.Transparent;
            this.tabPage1.CausesValidation = false;
            this.tabPage1.Controls.Add(this.button_VBAT_CALDATA_WRITE);
            this.tabPage1.Controls.Add(this.groupBox4);
            this.tabPage1.Controls.Add(this.groupBox3);
            this.tabPage1.Controls.Add(this.button_VBAT_CALDATA_READ);
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(276, 198);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "VBAT";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // button_VBAT_CALDATA_WRITE
            // 
            this.button_VBAT_CALDATA_WRITE.Location = new System.Drawing.Point(152, 150);
            this.button_VBAT_CALDATA_WRITE.Name = "button_VBAT_CALDATA_WRITE";
            this.button_VBAT_CALDATA_WRITE.Size = new System.Drawing.Size(108, 38);
            this.button_VBAT_CALDATA_WRITE.TabIndex = 27;
            this.button_VBAT_CALDATA_WRITE.Text = "Применить новые параметры";
            this.button_VBAT_CALDATA_WRITE.UseVisualStyleBackColor = true;
            this.button_VBAT_CALDATA_WRITE.Click += new System.EventHandler(this.CALDATA_WRITE_button_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.textBox2);
            this.groupBox4.Controls.Add(this.button_VBAT_CALK_CONSTANT);
            this.groupBox4.Location = new System.Drawing.Point(13, 96);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(120, 92);
            this.groupBox4.TabIndex = 26;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Смещение";
            // 
            // textBox2
            // 
            this.textBox2.BackColor = System.Drawing.SystemColors.Window;
            this.textBox2.ForeColor = System.Drawing.SystemColors.WindowText;
            this.textBox2.Location = new System.Drawing.Point(19, 21);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(86, 20);
            this.textBox2.TabIndex = 18;
            this.textBox2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBox2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_KeyPress);
            // 
            // button_VBAT_CALK_CONSTANT
            // 
            this.button_VBAT_CALK_CONSTANT.Location = new System.Drawing.Point(19, 54);
            this.button_VBAT_CALK_CONSTANT.Name = "button_VBAT_CALK_CONSTANT";
            this.button_VBAT_CALK_CONSTANT.Size = new System.Drawing.Size(86, 26);
            this.button_VBAT_CALK_CONSTANT.TabIndex = 19;
            this.button_VBAT_CALK_CONSTANT.Text = "Рассчитать";
            this.button_VBAT_CALK_CONSTANT.UseVisualStyleBackColor = true;
            this.button_VBAT_CALK_CONSTANT.Click += new System.EventHandler(this.CALCK_CONSTANT_button_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.textBox5);
            this.groupBox3.Controls.Add(this.button_VBAT_ADCREAD);
            this.groupBox3.Location = new System.Drawing.Point(143, 8);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(124, 82);
            this.groupBox3.TabIndex = 25;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Показания сенсора";
            // 
            // textBox5
            // 
            this.textBox5.BackColor = System.Drawing.SystemColors.Window;
            this.textBox5.Location = new System.Drawing.Point(20, 19);
            this.textBox5.Name = "textBox5";
            this.textBox5.ReadOnly = true;
            this.textBox5.Size = new System.Drawing.Size(86, 20);
            this.textBox5.TabIndex = 18;
            this.textBox5.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // button_VBAT_ADCREAD
            // 
            this.button_VBAT_ADCREAD.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button_VBAT_ADCREAD.Location = new System.Drawing.Point(20, 46);
            this.button_VBAT_ADCREAD.Name = "button_VBAT_ADCREAD";
            this.button_VBAT_ADCREAD.Size = new System.Drawing.Size(86, 26);
            this.button_VBAT_ADCREAD.TabIndex = 19;
            this.button_VBAT_ADCREAD.Text = "Прочитать";
            this.button_VBAT_ADCREAD.UseVisualStyleBackColor = true;
            this.button_VBAT_ADCREAD.Click += new System.EventHandler(this.SENSOR_READ_button_Click);
            // 
            // button_VBAT_CALDATA_READ
            // 
            this.button_VBAT_CALDATA_READ.Location = new System.Drawing.Point(152, 101);
            this.button_VBAT_CALDATA_READ.Name = "button_VBAT_CALDATA_READ";
            this.button_VBAT_CALDATA_READ.Size = new System.Drawing.Size(108, 38);
            this.button_VBAT_CALDATA_READ.TabIndex = 23;
            this.button_VBAT_CALDATA_READ.Text = "Прочитать из EEPROM";
            this.button_VBAT_CALDATA_READ.UseVisualStyleBackColor = true;
            this.button_VBAT_CALDATA_READ.Click += new System.EventHandler(this.CALDATA_READ_button_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.Transparent;
            this.groupBox2.Controls.Add(this.textBox4);
            this.groupBox2.Location = new System.Drawing.Point(13, 8);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(120, 82);
            this.groupBox2.TabIndex = 24;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Показания вольтметра (~3000мВ)";
            // 
            // textBox4
            // 
            this.textBox4.BackColor = System.Drawing.SystemColors.Window;
            this.textBox4.Location = new System.Drawing.Point(18, 50);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(86, 20);
            this.textBox4.TabIndex = 30;
            this.textBox4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBox4.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_KeyPress);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.button_AKKU_TYP_CALDATA_READ);
            this.tabPage2.Controls.Add(this.groupBox5);
            this.tabPage2.Controls.Add(this.groupBox1);
            this.tabPage2.Controls.Add(this.groupBox6);
            this.tabPage2.Controls.Add(this.button_AKKU_TYP_CALDATA_WRITE);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(276, 198);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "AKKU_TYP";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // button_AKKU_TYP_CALDATA_READ
            // 
            this.button_AKKU_TYP_CALDATA_READ.Location = new System.Drawing.Point(152, 101);
            this.button_AKKU_TYP_CALDATA_READ.Name = "button_AKKU_TYP_CALDATA_READ";
            this.button_AKKU_TYP_CALDATA_READ.Size = new System.Drawing.Size(108, 38);
            this.button_AKKU_TYP_CALDATA_READ.TabIndex = 32;
            this.button_AKKU_TYP_CALDATA_READ.Text = "Прочитать из EEPROM";
            this.button_AKKU_TYP_CALDATA_READ.UseVisualStyleBackColor = true;
            this.button_AKKU_TYP_CALDATA_READ.Click += new System.EventHandler(this.CALDATA_READ_button_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.textBox6);
            this.groupBox5.Controls.Add(this.button_AKKU_TYP_ADCREAD);
            this.groupBox5.Location = new System.Drawing.Point(143, 8);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(124, 82);
            this.groupBox5.TabIndex = 30;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Показания сенсора";
            // 
            // textBox6
            // 
            this.textBox6.BackColor = System.Drawing.SystemColors.Window;
            this.textBox6.Location = new System.Drawing.Point(20, 19);
            this.textBox6.Name = "textBox6";
            this.textBox6.ReadOnly = true;
            this.textBox6.Size = new System.Drawing.Size(86, 20);
            this.textBox6.TabIndex = 18;
            // 
            // button_AKKU_TYP_ADCREAD
            // 
            this.button_AKKU_TYP_ADCREAD.Location = new System.Drawing.Point(20, 46);
            this.button_AKKU_TYP_ADCREAD.Name = "button_AKKU_TYP_ADCREAD";
            this.button_AKKU_TYP_ADCREAD.Size = new System.Drawing.Size(86, 26);
            this.button_AKKU_TYP_ADCREAD.TabIndex = 19;
            this.button_AKKU_TYP_ADCREAD.Text = "Прочитать";
            this.button_AKKU_TYP_ADCREAD.UseVisualStyleBackColor = true;
            this.button_AKKU_TYP_ADCREAD.Click += new System.EventHandler(this.SENSOR_READ_button_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.textBox3);
            this.groupBox1.Controls.Add(this.button_AKKU_TYP_CALK_CONSTANT);
            this.groupBox1.Location = new System.Drawing.Point(13, 96);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(120, 92);
            this.groupBox1.TabIndex = 31;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Смещение";
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(19, 21);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(86, 20);
            this.textBox3.TabIndex = 18;
            this.textBox3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBox3.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_KeyPress);
            // 
            // button_AKKU_TYP_CALK_CONSTANT
            // 
            this.button_AKKU_TYP_CALK_CONSTANT.Location = new System.Drawing.Point(19, 54);
            this.button_AKKU_TYP_CALK_CONSTANT.Name = "button_AKKU_TYP_CALK_CONSTANT";
            this.button_AKKU_TYP_CALK_CONSTANT.Size = new System.Drawing.Size(86, 26);
            this.button_AKKU_TYP_CALK_CONSTANT.TabIndex = 19;
            this.button_AKKU_TYP_CALK_CONSTANT.Text = "Рассчитать";
            this.button_AKKU_TYP_CALK_CONSTANT.UseVisualStyleBackColor = true;
            this.button_AKKU_TYP_CALK_CONSTANT.Click += new System.EventHandler(this.CALCK_CONSTANT_button_Click);
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.textBox7);
            this.groupBox6.Location = new System.Drawing.Point(13, 8);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(120, 82);
            this.groupBox6.TabIndex = 29;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Сопротивление резистра(~4000 Ом)";
            // 
            // textBox7
            // 
            this.textBox7.Location = new System.Drawing.Point(18, 50);
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new System.Drawing.Size(86, 20);
            this.textBox7.TabIndex = 17;
            this.textBox7.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBox7.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_KeyPress);
            // 
            // button_AKKU_TYP_CALDATA_WRITE
            // 
            this.button_AKKU_TYP_CALDATA_WRITE.Location = new System.Drawing.Point(152, 150);
            this.button_AKKU_TYP_CALDATA_WRITE.Name = "button_AKKU_TYP_CALDATA_WRITE";
            this.button_AKKU_TYP_CALDATA_WRITE.Size = new System.Drawing.Size(108, 38);
            this.button_AKKU_TYP_CALDATA_WRITE.TabIndex = 28;
            this.button_AKKU_TYP_CALDATA_WRITE.Text = "Применить новые параметры";
            this.button_AKKU_TYP_CALDATA_WRITE.UseVisualStyleBackColor = true;
            this.button_AKKU_TYP_CALDATA_WRITE.Click += new System.EventHandler(this.CALDATA_WRITE_button_Click);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.button_TVCXO_CALDATA_WRITE);
            this.tabPage3.Controls.Add(this.groupBox7);
            this.tabPage3.Controls.Add(this.groupBox8);
            this.tabPage3.Controls.Add(this.groupBox9);
            this.tabPage3.Controls.Add(this.button_TVCXO_CALDATA_READ);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(276, 198);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "TVCXO";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // button_TVCXO_CALDATA_WRITE
            // 
            this.button_TVCXO_CALDATA_WRITE.Location = new System.Drawing.Point(152, 150);
            this.button_TVCXO_CALDATA_WRITE.Name = "button_TVCXO_CALDATA_WRITE";
            this.button_TVCXO_CALDATA_WRITE.Size = new System.Drawing.Size(108, 38);
            this.button_TVCXO_CALDATA_WRITE.TabIndex = 32;
            this.button_TVCXO_CALDATA_WRITE.Text = "Применить новые параметры";
            this.button_TVCXO_CALDATA_WRITE.UseVisualStyleBackColor = true;
            this.button_TVCXO_CALDATA_WRITE.Click += new System.EventHandler(this.CALDATA_WRITE_button_Click);
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.textBox8);
            this.groupBox7.Controls.Add(this.button_TVCXO_CALK_CONSTANT);
            this.groupBox7.Location = new System.Drawing.Point(13, 96);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(120, 92);
            this.groupBox7.TabIndex = 31;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Смещение";
            // 
            // textBox8
            // 
            this.textBox8.ForeColor = System.Drawing.SystemColors.WindowText;
            this.textBox8.Location = new System.Drawing.Point(19, 21);
            this.textBox8.Name = "textBox8";
            this.textBox8.Size = new System.Drawing.Size(86, 20);
            this.textBox8.TabIndex = 18;
            this.textBox8.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBox8.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_KeyPress);
            // 
            // button_TVCXO_CALK_CONSTANT
            // 
            this.button_TVCXO_CALK_CONSTANT.Location = new System.Drawing.Point(19, 54);
            this.button_TVCXO_CALK_CONSTANT.Name = "button_TVCXO_CALK_CONSTANT";
            this.button_TVCXO_CALK_CONSTANT.Size = new System.Drawing.Size(86, 26);
            this.button_TVCXO_CALK_CONSTANT.TabIndex = 19;
            this.button_TVCXO_CALK_CONSTANT.Text = "Рассчитать";
            this.button_TVCXO_CALK_CONSTANT.UseVisualStyleBackColor = true;
            this.button_TVCXO_CALK_CONSTANT.Click += new System.EventHandler(this.CALCK_CONSTANT_button_Click);
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.textBox9);
            this.groupBox8.Controls.Add(this.button_TVCXO_ADCREAD);
            this.groupBox8.Location = new System.Drawing.Point(143, 8);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(124, 82);
            this.groupBox8.TabIndex = 30;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "Показания сенсора";
            // 
            // textBox9
            // 
            this.textBox9.Location = new System.Drawing.Point(20, 19);
            this.textBox9.Name = "textBox9";
            this.textBox9.ReadOnly = true;
            this.textBox9.Size = new System.Drawing.Size(86, 20);
            this.textBox9.TabIndex = 18;
            this.textBox9.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // button_TVCXO_ADCREAD
            // 
            this.button_TVCXO_ADCREAD.Location = new System.Drawing.Point(20, 46);
            this.button_TVCXO_ADCREAD.Name = "button_TVCXO_ADCREAD";
            this.button_TVCXO_ADCREAD.Size = new System.Drawing.Size(86, 26);
            this.button_TVCXO_ADCREAD.TabIndex = 19;
            this.button_TVCXO_ADCREAD.Text = "Прочитать";
            this.button_TVCXO_ADCREAD.UseVisualStyleBackColor = true;
            this.button_TVCXO_ADCREAD.Click += new System.EventHandler(this.SENSOR_READ_button_Click);
            // 
            // groupBox9
            // 
            this.groupBox9.Controls.Add(this.textBox10);
            this.groupBox9.Location = new System.Drawing.Point(13, 8);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Size = new System.Drawing.Size(120, 82);
            this.groupBox9.TabIndex = 29;
            this.groupBox9.TabStop = false;
            this.groupBox9.Text = "Температура (~10 °C)";
            // 
            // textBox10
            // 
            this.textBox10.Location = new System.Drawing.Point(18, 50);
            this.textBox10.Name = "textBox10";
            this.textBox10.Size = new System.Drawing.Size(86, 20);
            this.textBox10.TabIndex = 0;
            this.textBox10.Tag = "";
            this.textBox10.Text = "09,98";
            this.textBox10.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBox10.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxFloat_KeyPress);
            // 
            // button_TVCXO_CALDATA_READ
            // 
            this.button_TVCXO_CALDATA_READ.Location = new System.Drawing.Point(152, 101);
            this.button_TVCXO_CALDATA_READ.Name = "button_TVCXO_CALDATA_READ";
            this.button_TVCXO_CALDATA_READ.Size = new System.Drawing.Size(108, 38);
            this.button_TVCXO_CALDATA_READ.TabIndex = 28;
            this.button_TVCXO_CALDATA_READ.Text = "Прочитать из EEPROM";
            this.button_TVCXO_CALDATA_READ.UseVisualStyleBackColor = true;
            this.button_TVCXO_CALDATA_READ.Click += new System.EventHandler(this.CALDATA_READ_button_Click);
            // 
            // groupBox10
            // 
            this.groupBox10.BackColor = System.Drawing.Color.White;
            this.groupBox10.Controls.Add(this.button2);
            this.groupBox10.Controls.Add(this.button_SAVE_BLOCK67);
            this.groupBox10.Controls.Add(this.button9);
            this.groupBox10.Controls.Add(this.SERVICE_MODE_button);
            this.groupBox10.Controls.Add(this.button1);
            this.groupBox10.Controls.Add(this.comboBox1);
            this.groupBox10.ForeColor = System.Drawing.SystemColors.ControlText;
            this.groupBox10.Location = new System.Drawing.Point(8, -2);
            this.groupBox10.Name = "groupBox10";
            this.groupBox10.Size = new System.Drawing.Size(577, 46);
            this.groupBox10.TabIndex = 34;
            this.groupBox10.TabStop = false;
            // 
            // groupBox12
            // 
            this.groupBox12.BackColor = System.Drawing.Color.White;
            this.groupBox12.Controls.Add(this.button_R);
            this.groupBox12.Controls.Add(this.button_T);
            this.groupBox12.Controls.Add(this.button18);
            this.groupBox12.Controls.Add(this.button_V);
            this.groupBox12.Controls.Add(this.button17);
            this.groupBox12.Controls.Add(this.button7);
            this.groupBox12.Controls.Add(this.button4);
            this.groupBox12.Controls.Add(this.button6);
            this.groupBox12.Location = new System.Drawing.Point(8, 274);
            this.groupBox12.Name = "groupBox12";
            this.groupBox12.Size = new System.Drawing.Size(577, 65);
            this.groupBox12.TabIndex = 36;
            this.groupBox12.TabStop = false;
            // 
            // button_R
            // 
            this.button_R.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button_R.ForeColor = System.Drawing.SystemColors.ControlText;
            this.button_R.Location = new System.Drawing.Point(408, 15);
            this.button_R.Name = "button_R";
            this.button_R.Size = new System.Drawing.Size(32, 40);
            this.button_R.TabIndex = 30;
            this.button_R.Text = "R";
            this.button_R.UseVisualStyleBackColor = true;
            this.button_R.Click += new System.EventHandler(this.Read_VRT_button_Click);
            // 
            // button_T
            // 
            this.button_T.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button_T.ForeColor = System.Drawing.SystemColors.ControlText;
            this.button_T.Location = new System.Drawing.Point(444, 15);
            this.button_T.Name = "button_T";
            this.button_T.Size = new System.Drawing.Size(32, 40);
            this.button_T.TabIndex = 29;
            this.button_T.Text = "T";
            this.button_T.UseVisualStyleBackColor = true;
            this.button_T.Click += new System.EventHandler(this.Read_VRT_button_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(590, 343);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.groupBox12);
            this.Controls.Add(this.groupBox10);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "Form1";
            this.Tag = "";
            this.Text = "SensorTool";
            this.TransparencyKey = System.Drawing.Color.Red;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            this.groupBox9.ResumeLayout(false);
            this.groupBox9.PerformLayout();
            this.groupBox10.ResumeLayout(false);
            this.groupBox12.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button SERVICE_MODE_button;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button_SAVE_BLOCK67;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.Button button_V;
        private System.Windows.Forms.Button button17;
        private System.Windows.Forms.Button button18;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button button_AKKU_TYP_CALDATA_READ;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.TextBox textBox6;
        private System.Windows.Forms.Button button_AKKU_TYP_ADCREAD;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Button button_AKKU_TYP_CALK_CONSTANT;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.TextBox textBox7;
        private System.Windows.Forms.Button button_AKKU_TYP_CALDATA_WRITE;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Button button_TVCXO_CALDATA_WRITE;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.TextBox textBox8;
        private System.Windows.Forms.Button button_TVCXO_CALK_CONSTANT;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.TextBox textBox9;
        private System.Windows.Forms.Button button_TVCXO_ADCREAD;
        private System.Windows.Forms.GroupBox groupBox9;
        private System.Windows.Forms.TextBox textBox10;
        private System.Windows.Forms.Button button_TVCXO_CALDATA_READ;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Button button_VBAT_CALDATA_WRITE;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button button_VBAT_CALK_CONSTANT;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.Button button_VBAT_ADCREAD;
        private System.Windows.Forms.Button button_VBAT_CALDATA_READ;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.GroupBox groupBox10;
        private System.Windows.Forms.GroupBox groupBox12;
        private System.Windows.Forms.Button button_R;
        private System.Windows.Forms.Button button_T;


    }
}

