using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using System.Diagnostics;
using System.Timers;
using System.Collections;
using System.IO;

namespace WindowsFormsApplication
{
    public partial class Form1 : Form
    {
        public static Form1 form;

        public Form1()
        {
            //метод визуального дизайнера.Вручную его лучше не править
            InitializeComponent();
            this.FormClosing += new FormClosingEventHandler(Form1_Closing);
            form = this;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //всплывающие подсказки
            ToolTip t = new ToolTip();
            t.SetToolTip(button_V, "Напряжение аккумулятора");
            t.SetToolTip(button_R, "Сопротивление резистора аккумулятора");
            t.SetToolTip(button_T, "Температура VCXO");

            //блокируем тестовую кнопку
            button18.Enabled = false;

            this.button_V.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));

            //перенаправляем консольные сообщения в наш textBox
            Console.SetOut(new TextBoxStreamWriter(this.textBox1));

            //получаем список портов и добавляем в comoBox1
            try
            {
                this.comboBox1.Items.AddRange(System.IO.Ports.SerialPort.GetPortNames());
                this.comboBox1.SelectedIndex = 0;
            }
            catch { }

            new Serial();
            //Serial.serialPort.DataReceived += new SerialDataReceivedEventHandler(serialPort1_DataReceived);

        }

        private void Form1_Closing(object sender, FormClosingEventArgs e)
        {
            var result = MessageBox.Show("Выйти из приложения", "",
                                MessageBoxButtons.YesNo,
                                MessageBoxIcon.Question);

            if (result == DialogResult.No)
            {
                //BFC.SwitchFromBfcToRccpMode();// Переводим в AT режим               
                e.Cancel = true;
            }
        }

        private bool connected = false;

/****************************************************************************************************/

        private void OpenCloseCom_button_Click(object sender, EventArgs e)
        {
            if (connected == false)
            {
                try
                {
                    //устанавливаем параметры com порта
                    Serial.serialPort.PortName = this.comboBox1.SelectedItem.ToString();
                    Serial.serialPort.BaudRate = 115200;
                    Serial.serialPort.Parity = Parity.None;
                    Serial.serialPort.DataBits = 8;
                    Serial.serialPort.StopBits = StopBits.One;
                    Serial.serialPort.Open();
                    connected = true;
                    Serial.serialPort.WriteLine("AT^SQWE=1\r\n");
                    this.button1.Text = "Отключить";
                    this.textBox1.AppendText("Соединение установлено\n");
                    return;
                }
                catch { MessageBox.Show("Не могу открыть порт", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Information); }
            }
            if (connected == true)
            {
                Serial.serialPort.Close();
                connected = false;
                this.button1.Text = "Подключить";
                this.textBox1.AppendText("Соединение закрыто\n");
                return;
            }
        }

/****************************************************************************************************/

        private void comboBox1_Click(object sender, EventArgs e)
        {
            //получаем список портов и добавляем в comoBox1
            try
            {
                this.comboBox1.Items.Clear();
                this.comboBox1.Items.AddRange(System.IO.Ports.SerialPort.GetPortNames());
                this.comboBox1.SelectedIndex = 0;

            }
            catch { }
        }

/****************************************************************************************************/

        public void GetStatUpInfo(int answPing)
        {
            Serial.serialPort.ReadTimeout = 500;
            Serial.ComTimeout = 1000;
            TimeSpan interval;
            DateTime t0 = DateTime.Now;
            int bytesToRead;

            //this.textBox1.Clear();

            if (this.InvokeRequired)
            {
                this.BeginInvoke(new Action<int>(GetStatUpInfo), new object[] { answPing });
                return;
            }

            while (((interval = DateTime.Now - t0).Milliseconds) < Serial.ComTimeout) //время ожидания ответа 1 сек 
            {
                if ((bytesToRead = Serial.serialPort.BytesToRead) > 2)
                {
                    System.Threading.Thread.Sleep(100);
                    Serial.ComTimeout = 500;
                    //button2_Click_1(null, null);
                    break;
                }
            }

        }

/****************************************************************************************************/
        private void SENSOR_READ_button_Click(object sender, EventArgs e)
        {
            var btn = sender as Button;

            switch(btn.Name)
            {
                case "button_VBAT_ADCREAD":
                    try
                    {
                        Console.Write("Получение данных из АЦП...");
                        UInt16 analogValue = BFC.AmcGetAnalogDataValue(0, 2);
                        this.textBox5.Clear();
                        this.textBox5.AppendText("" + analogValue);
                        Console.WriteLine("ОК");
                    }
                    catch (Exception ex) { Console.WriteLine(ex.Message); }
                    break;

                case "button_AKKU_TYP_ADCREAD":
                    try
                    {
                        Console.Write("Получение данных из АЦП...");
                        short analogValue = (short)BFC.AmcGetAnalogDataValue(2,3);
                       /*8 if (analogValue <= 0)
                        {
                            this.textBox6.Clear();
                            throw new Exception("Резистор не подключен:(");
                        }*/
                        //тут нужно разобраться UInt16 analogValue2 = BFC.AmcGetAnalogDataValue(0xB,3);
                        this.textBox6.Clear();
                        this.textBox6.AppendText("" + analogValue);
                        Console.WriteLine("ОК");
                    }
                    catch (Exception ex) { Console.WriteLine(ex.Message); }
                    break;

                case "button_TVCXO_ADCREAD":
                    try
                    {
                        Console.Write("Получение данных из АЦП...");
                        UInt16 analogValue = BFC.AmcGetAnalogDataValue(3, 3);
                        this.textBox9.Clear();
                        this.textBox9.AppendText("" + analogValue);
                        Console.WriteLine("ОК");
                    }
                    catch (Exception ex) { Console.WriteLine(ex.Message); }
                    break;
            }
        }

/****************************************************************************************************/

        private void CALDATA_READ_button_Click(object sender, EventArgs e)
        {
            var btn = sender as Button;

            switch (btn.Name)
            {
                case "button_VBAT_CALDATA_READ":
                    try
                    {
                        Console.Write("Чтение из блока 67...");
                        byte[] data = BFC.EeReadBlock(67, 0x34, 0x4);
                        Console.WriteLine("Ок");
                        this.textBox4.Clear();
                        this.textBox2.Clear();
                        this.textBox4.AppendText("" + BitConverter.ToUInt16(new byte[] { data[0], data[1] }, 0));
                        this.textBox2.AppendText("" + BitConverter.ToUInt16(new byte[] { data[2], data[3] }, 0));
                    }
                    catch (Exception ex) {Console.WriteLine(ex.Message);}
                    break;

                case "button_AKKU_TYP_CALDATA_READ":
                    try
                    {
                        Console.Write("Чтение из блока 67...");
                        byte[] data = BFC.EeReadBlock(67, 0x3C, 0x4);
                        Console.WriteLine("Ок");
                        this.textBox7.Clear();
                        this.textBox3.Clear();
                        this.textBox7.AppendText("" + BitConverter.ToUInt16(new byte[] { data[0], data[1] }, 0));
                        this.textBox3.AppendText("" + BitConverter.ToUInt16(new byte[] { data[2], data[3] }, 0));
                    }
                    catch (Exception ex) { Console.WriteLine(ex.Message); }
                    break;

                case "button_TVCXO_CALDATA_READ":
                    try
                    {
                        Console.Write("Чтение из блока 67...");
                        byte[] data = BFC.EeReadBlock(67, 0x44, 0x4);
                        Console.WriteLine("Ок");
                        this.textBox10.Clear();
                        this.textBox8.Clear();
                        UInt16 temp = BitConverter.ToUInt16(new byte[] { data[0], data[1] }, 0);
                        UInt16 offset = BitConverter.ToUInt16(new byte[] { data[2], data[3] }, 0);
                        this.textBox10.AppendText(String.Format("{0:00.00}", (float)temp/100 ));
                        this.textBox8.AppendText("" + (short)offset);
                    }
                    catch (Exception ex) { Console.WriteLine(ex.Message); }
                    break;
            }
        }

/****************************************************************************************************/
        private void CALCK_CONSTANT_button_Click(object sender, EventArgs e)
        {
            var btn = sender as Button;

            switch (btn.Name)
            {
                case "button_VBAT_CALK_CONSTANT":
                    try
                    {
                        if (this.textBox5.Text.Length == 0) throw new Exception("Ошибка!..Пожалуйста прочитайте показания сенсора");
                        if (this.textBox4.Text.Length == 0) throw new Exception("Ошибка!..Пожалуйста введите показания вольтметра в милливольтах");
                        UInt16 sensorValue = Convert.ToUInt16(this.textBox5.Text);
                        UInt16 voltage = Convert.ToUInt16(this.textBox4.Text);
                        if (voltage > 3050 || voltage < 2950) throw new Exception("Ошибка!..Калибровка должна производиться при напряжении 2950-3050 mV)");
                        UInt16 constant = (UInt16)((voltage * sensorValue / 1000) - voltage);
                        this.textBox2.Clear();
                        this.textBox2.AppendText(String.Format("{0}", constant));
                    }
                    catch (Exception ex) { Console.WriteLine(ex.Message); }
                    break;

                case "button_AKKU_TYP_CALK_CONSTANT":
                    try
                    {
                        if (this.textBox6.Text.Length == 0) throw new Exception("Ошибка!..Поле 'Показания сенсора' не может быть пустым");
                        if (this.textBox7.Text.Length == 0) throw new Exception("Ошибка!..Пожалуйста укажите значение сопротивления резистора в омах");
                        UInt16 sensorValue = Convert.ToUInt16(this.textBox6.Text);
                        UInt16 resistance = Convert.ToUInt16(this.textBox7.Text);
                        if (resistance > 4050 || resistance < 3950) throw new Exception("Ошибка!..Калибровка должна производиться при сопротивлении резистора 3950-4050 оМ)");
                        UInt16 constant = (UInt16)((resistance * sensorValue / 1000) + resistance);
                        this.textBox3.Clear();
                        this.textBox3.AppendText(String.Format("{0}", constant));
                    }
                    catch (Exception ex) { Console.WriteLine(ex.Message); }
                    break;
                case "button_TVCXO_CALK_CONSTANT":
                    try
                    {
                        if (this.textBox9.Text.Length == 0) throw new Exception("Ошибка!..Поле 'Показания сенсора' не может быть пустым");
                        if (this.textBox10.Text.Length == 0) throw new Exception("Ошибка!..Пожалуйста укажите значение температуры в °C");
                        UInt16 sensorValue = (UInt16)Convert.ToInt16(this.textBox9.Text);
                        UInt16 temp = (UInt16)Convert.ToInt16(this.textBox10.Text.Remove(2,1));
                        if (temp > 1000 || temp < 950) throw new Exception("Отмена операции...Калибровка должна производиться при температуре 9.50-10 °C)) "+temp);
                        UInt16 p = TVCXO.calcTempDat(temp);
                        short offset = (short)((temp * sensorValue / 1000)-p);
                        //short p = (short)(temp * sensorValue / 1000) - offset);
                        this.textBox8.Clear();
                        this.textBox8.AppendText(String.Format("{0}", offset));
                    }
                    catch (Exception ex) { Console.WriteLine(ex.Message); }
                    break;
            }
        }

/****************************************************************************************************/

        private void CALDATA_WRITE_button_Click(object sender, EventArgs e)
        {
            byte ver;
            UInt16 size;

            int freeb = -1;
            int freea = -1;
            int freed = -1;

            var btn = sender as Button;

            var result = MessageBox.Show("Обновить данные блока 67 в EEPROM?", "",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

            if (result == DialogResult.No)
                return;

            try
            {
                UInt16 hwid = BFC.GetHardwareIdentification();
                switch (hwid)
                {                    
                    case 0x190://S75
                    case 0x194://S68
                    case 0x195://M81
                    case 0x197://EL71
                    case 0x19B://SL75
                    case 0x19C://C81
                    case 0x19D://E71
                    break;
                    default: throw new Exception("Модель не поддерживается");
                }

                if (BFC.GetCurrentMode() != 0x16) throw new Exception("Отмена операции...Запись в EEPROM возможна только в сервисном режиме");
                BFC.EeGiveBlockVersionAndSize(67, out size, out ver);
                byte[] rdata = BFC.EeReadBlock(67, 0, size);
                BFC.EeSpaceInfo(0, out freeb, out freea, out freed);
                //Console.WriteLine("Свободно {0} байт",freeb);
                if (freeb < (size + 2048)) throw new Exception("Отмена операции...Требуется дефрагментация EELITE");

                switch (btn.Name)
                {
                    case "button_VBAT_CALDATA_WRITE":
                        if (this.textBox4.Text.Length == 0) throw new Exception("Отмена операции...Поле 'Показания вольтметра' не может быть пустым");
                        if (this.textBox2.Text.Length == 0) throw new Exception("Отмена операции...Поле 'Смещение' не может быть пустым");
                        UInt16 voltage = Convert.ToUInt16(this.textBox4.Text);
                        UInt16 offsetVBAT = Convert.ToUInt16(this.textBox2.Text);
                        if (voltage > 3050 || voltage < 2950) throw new Exception("Отмена операции...Калибровка должна производиться при напряжении 2950-3050 mV)");
                        rdata[0x34] = (byte)voltage;
                        rdata[0x35] = (byte)(voltage >> 8);
                        rdata[0x36] = (byte)offsetVBAT;
                        rdata[0x37] = (byte)(offsetVBAT >> 8);
                        break;

                    case "button_AKKU_TYP_CALDATA_WRITE":
                        if (this.textBox7.Text.Length == 0) throw new Exception("Отмена операции...Поле 'Сопротивление резистора' не может быть пустым");
                        if (this.textBox3.Text.Length == 0) throw new Exception("Отмена операции...Поле 'Смещение' не может быть пустым");
                        UInt16 resistance = Convert.ToUInt16(this.textBox7.Text);
                        UInt16 offsetAKKU_TYP = Convert.ToUInt16(this.textBox3.Text);
                        if (resistance > 4050 || resistance < 3950) throw new Exception("Ошибка!..Калибровка должна производиться при сопротивлении резистора 3950-4050 оМ)");
                        rdata[0x3C] = (byte)resistance;
                        rdata[0x3D] = (byte)(resistance >> 8);
                        rdata[0x3E] = (byte)offsetAKKU_TYP;
                        rdata[0x3F] = (byte)(offsetAKKU_TYP >> 8);
                        break;

                    case "button_TVCXO_CALDATA_WRITE":
                        if (this.textBox10.Text.Length == 0) throw new Exception("Отмена операции...Поле 'Температура' не может быть пустым");
                        if (this.textBox8.Text.Length == 0) throw new Exception("Отмена операции...Поле 'Смещение' не может быть пустым");
                        UInt16 temp = Convert.ToUInt16(this.textBox10.Text.Remove(2, 1));                       
                        short offsetTVCXO = Convert.ToInt16(this.textBox8.Text);
                        if (offsetTVCXO > 0) throw new Exception("Отмена операции...Значение смещения не может быть больше нуля");
                        if (temp > 1000 || temp < 950) throw new Exception("Отмена операции...Калибровка должна производиться при температуре 9.50-10.00 °C))");
                        rdata[0x44] = (byte)temp;
                        rdata[0x45] = (byte)(temp >> 8);
                        rdata[0x46] = (byte)offsetTVCXO;
                        rdata[0x47] = (byte)(offsetTVCXO >> 8);
                        rdata[0x4C] = (byte)temp;
                        rdata[0x4D] = (byte)(temp >> 8);
                        rdata[0x4E] = (byte)offsetTVCXO;
                        rdata[0x4F] = (byte)(offsetTVCXO >> 8);
                        rdata[0x54] = (byte)temp;
                        rdata[0x55] = (byte)(temp >> 8);
                        rdata[0x56] = (byte)offsetTVCXO;
                        rdata[0x57] = (byte)(offsetTVCXO >> 8);
                        break;
                }

                String imei = BFC.ReadIMEI();
                String model = BFC.GetPhoneModel();
                String sw = BFC.GetSoftWareVer();

                Console.Write("Создание бэкапа блока... ");
                string dir = Application.StartupPath;
                if (!Directory.Exists(dir + "\\backup")) Directory.CreateDirectory(dir + "//backup");
                String path = dir + "\\backup\\" + model + 'v' + sw + '_' + imei + '_' + DateTime.Now.ToString("dd.MM.yyyy_HH-mm-ss") + ".eep";
                FileStream fs = File.Create(path);
                if (fs != null)
                {
                    fs.Write(BitConverter.GetBytes((int)1), 0, sizeof(int));     //кол-во блоков EELITE в файле
                    fs.Write(BitConverter.GetBytes((int)0), 0, sizeof(int));     //кол-во блоков EEFULL в файле
                    fs.Write(BitConverter.GetBytes((int)67), 0, sizeof(int));    //номер блока
                    fs.Write(BitConverter.GetBytes((int)size), 0, sizeof(int));  //размер блока
                    fs.Write(BitConverter.GetBytes((int)ver), 0, sizeof(int));   //версия блока
                    fs.Write(rdata, 0, rdata.Length);                            //сам блок
                    fs.Write(BitConverter.GetBytes(hwid), 0, sizeof(short));     //HWID телефона в конце блока
                    fs.Close();
                    Console.WriteLine("Ок");
                    
                }

                Console.Write("Создание блока 67...");
                BFC.EeCreateBlock(67, (UInt16)rdata.Length, 1);
                Console.WriteLine("Ok");
                System.Threading.Thread.Sleep(500);
                Console.Write("Запись в блок 67...");
                BFC.EeWriteBlock(67, 0, rdata);
                System.Threading.Thread.Sleep(500);
                BFC.EeFinishBlock(67);
                Console.WriteLine("Ok");
                System.Threading.Thread.Sleep(500);
                Console.Write("Иницализация блока калибровоки...");
                System.Threading.Thread.Sleep(500);
                BFC.AmcResetCommonCalibrationBlock();
                Console.WriteLine("Ok");

                switch (btn.Name)
                {
                    case "button_VBAT_CALDATA_WRITE":
                        Console.WriteLine("Калибровки вольтметра успешно обновлены!");
                        break;
                    case "button_AKKU_TYP_CALDATA_WRITE":
                        Console.WriteLine("Калибровки AKKU_TYP успешно обновлены!");
                        break;
                    case "button_TVCXO_CALDATA_WRITE":
                        Console.WriteLine("Калибровки термометра успешно обновлены!");
                        break;
                }                    
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }

        }

/****************************************************************************************************/

        private void SERVICE_MODE_button_Click(object sender, EventArgs e)
        {
            //string imei;
            //if ((imei = BFC.BFC_ReadIMEI()) != null) Console.WriteLine("imei: "+imei);
            //else
            //Console.WriteLine("нет ответа");
            //this.textBox1.Clear();
            Boot.SendBoot();

        }

/****************************************************************************************************/

        private void SrvToNorm_button_Click(object sender, EventArgs e)
        {
            try
            {
                BFC.SwitchFromServiceToNormalMode();
                Console.WriteLine("Телефон включается...");
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
        }

/****************************************************************************************************/

        private void SimSim_button_Click(object sender, EventArgs e)
        {
            try
            {
                Serial.serialPort.WriteLine("AT^SQWE=1\r\n");
                System.Threading.Thread.Sleep(100);
                BFC.SimulateSim();
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
        }

/****************************************************************************************************/

        private void SwitchMobileOff_button_Click(object sender, EventArgs e)
        {
            try
            {
                Serial.serialPort.WriteLine("AT^SQWE=1\r\n");
                System.Threading.Thread.Sleep(100);
                BFC.SwitchMobileOff();
                Console.WriteLine("Телефон выключается...");
                //Console.WriteLine(BitConverter.ToString(BFC.EeReadBlock(67, 0x2F, 42)));
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
        }

/*********************************************************************************************************************/

        private void button9_Click(object sender, EventArgs e)
        {
            byte level;
            byte fade;

            try
            {   
                BFC.LightGetIllumination(0, out level, out fade);

                if (level > 0)
                {
                    //выкл. подсветку дисплея
                    BFC.LightSwitch(0, 0, 0);
                    //выкл. подсветку клавиатуры
                    BFC.LightSwitch(1, 0, 0);
                    Console.WriteLine("Подсветка выключена.");
                }
                else
                {
                    //вкл. подсветку дисплея
                    BFC.LightSwitch(0, 100, 0);
                    //вкл. подсветку клавиатуры
                    BFC.LightSwitch(1, 100, 0);
                    Console.WriteLine("Подсветка включена.");
                }
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
        }

/****************************************************************************************************/

        private void SAVE_BLOCK67_button_Click(object sender, EventArgs e)
        {
            byte[] rdata = null;
            UInt16 size;
            byte ver;
            Stream myStream;
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.FileName = null; // Default file name
            saveFileDialog1.DefaultExt = ".eep"; // Default file extension
            saveFileDialog1.Filter = "eep files (*.eep)|*.eep";
            saveFileDialog1.FilterIndex = 0;
            saveFileDialog1.RestoreDirectory = true;

            try
            {
                UInt16 hwid = BFC.GetHardwareIdentification();
                String model = BFC.GetPhoneModel();
                String sw = BFC.GetSoftWareVer();

                Console.Write("Чтение блока 67...");
                BFC.EeGiveBlockVersionAndSize(67, out size, out ver);
                rdata = BFC.EeReadBlock(67, 0, (UInt16)size);
                Console.WriteLine("Ок");

                saveFileDialog1.FileName = model + 'v' + sw + '_' + DateTime.Now.ToString("dd.MM.yyyy");

                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    if ((myStream = saveFileDialog1.OpenFile()) != null)
                    {
                        myStream.Write(BitConverter.GetBytes((int)1), 0, sizeof(int));     //кол-во блоков EELITE в файле
                        myStream.Write(BitConverter.GetBytes((int)0), 0, sizeof(int));     //кол-во блоков EEFULL в файле
                        myStream.Write(BitConverter.GetBytes((int)67), 0, sizeof(int));    //номер блока
                        myStream.Write(BitConverter.GetBytes((int)size), 0, sizeof(int));  //размер блока
                        myStream.Write(BitConverter.GetBytes((int)ver), 0, sizeof(int));   //версия блока
                        myStream.Write(rdata, 0, rdata.Length);                            //сам блок
                        myStream.Write(BitConverter.GetBytes(hwid), 0, sizeof(short));     //HWID телефона в конце блока
                        myStream.Close();
                        Console.WriteLine("Блок сохранен в " + saveFileDialog1.FileName);
                    }
                }
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
        }

/****************************************************************************************************/

        private void WRITE_BLOCK67_button_Click(object sender, EventArgs e) //запись блока в телефон
        {
            byte[] rdata = null;
            Stream myStream;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "eep files (*.eep)|*.eep|All files (*.*)|*.*";
            openFileDialog.Title = "Select a epp file";

            try
            {
                if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    if ((myStream = openFileDialog.OpenFile()) != null) ;
                    {
                        myStream.Read(rdata = new byte[myStream.Length], 0, Convert.ToInt32(myStream.Length));
                        myStream.Close();
                    }

                    EEP.Write(rdata, 67);
                }

            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }

        }

/*********************************************************************************************************************/

        private void Read_VRT_button_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;

            try
            {
                Serial.serialPort.WriteLine("AT^SQWE=1\r\n");
                System.Threading.Thread.Sleep(100);

                if(button == button_V)
                    Console.WriteLine("Напряжение аккумулятора: {0} мВ", BFC.GetVBat());
                else
                    if (button == button_R)
                    {
                        short r1 = BFC.GetAkku(2, 3);
                        short r2 = BFC.GetAkku(0xB, 3);
                        if (r2 - r1 > 1000)
                            Console.WriteLine("Сопротивление резистора аккумулятора: ~{0} Ом", BFC.GetAkku(0xB, 3));
                        else
                            Console.WriteLine("Сопротивление резистора аккумулятора: ~{0} Ом", BFC.GetAkku(2, 3));
                    }
                if (button == button_T)
                    Console.WriteLine("Температура VCXO: {0: ##.##} °C", (float)(BFC.GetAkku(3,3) - 0xAAA) / 10);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
        }

/*********************************************************************************************************************/     
   
        private void textBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(!(Char.IsDigit(e.KeyChar)))
            {
                if(sender == this.textBox8)//здесь разрешаем знак минуса только вначале строки
                {
                    if ((e.KeyChar != 45 | this.textBox8.Text.Length > 0) && e.KeyChar != (char)Keys.Back)
                        e.Handled = true;
                }
                else
                    if(e.KeyChar != (char)Keys.Back) 
                        e.Handled = true;
            }
        }

 /*********************************************************************************************************************/

        private void textBoxFloat_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox t = sender as TextBox;

            if (!(Char.IsDigit(e.KeyChar)))
            {
                e.Handled = true;
                return;
            }
            else
            {
                int pos = t.SelectionStart;
                if (pos == 2 || pos > 4 )
                {
                    if (pos == 2) t.SelectionStart = 3;
                    e.Handled = true;
                    return;
                }
                else
                {
                    t.Text=t.Text.Remove(pos, 1);
                    t.SelectionStart = pos;
                }
            }
        }

/*********************************************************************************************************************/

        private void About_button_Click(object sender, EventArgs e)
        {
            MessageBox.Show("SensorTool v1.0\n(c) alfinant, 2016\nalfinant@yandex.ru\nC81,M81,S68,S75,SL75,E71,EL71", "О программе",
                       System.Windows.Forms.MessageBoxButtons.OK,
                       System.Windows.Forms.MessageBoxIcon.Information);

        }

/*********************************************************************************************************************/

        private void Test_button_Click(object sender, EventArgs e)
        {
            int freeb = -1;
            int freea = -1;
            int freed = -1;            

            // Serial.serialPort.WriteLine("AT^SQWE=1\r\n");
            //System.Threading.Thread.Sleep(100);
          
            try
            {
                UInt16 size;
                byte ver;
               // Console.WriteLine("AkkuUnkCmd3: {0}", BFC.AkkuUnkCmd5());
                UInt16 hwid = BFC.GetHardwareIdentification();
                String imei = BFC.ReadIMEI();
                String model = BFC.GetPhoneModel();
                String sw = BFC.GetSoftWareVer();
                BFC.EeGiveBlockVersionAndSize(67, out size, out ver);
                byte[] rdata = BFC.EeReadBlock(67, 0, (UInt16)size);
            
                //Console.WriteLine("BT-1: {0} Om", BFC.GetAkku(0xb, 3)); //сопротивление резистора аккума(в мониторе указан как "BT"), 

               // Console.WriteLine("BT-2: {0} Om", BFC.GetAkku(0xB, 3));
 /*               search_EELITE();
                BFC.EeSpaceInfo(0, out freeb, out freea, out freed);
                Console.WriteLine("EeSpaceInfo:\r\nfreeb: {0}\r\nfreea: {1} \r\nfreed: {2}", freeb, freea, freed);
                Console.WriteLine("Тест функции=>calcTemp: {0} °C", (float)(TVCXO.calcTemp(2532) - 0xAAA) / 10);
                Console.WriteLine("Тест функции=>calcTempDat: {0}", TVCXO.calcTempDat(2080));
                Console.WriteLine("HWID: " + BFC.GetHardwareIdentification());
                Console.WriteLine("Phone: " + BFC.GetPhoneModel());
                Console.WriteLine("SW: " + BFC.GetSoftWareVer());
                Console.WriteLine("IMEI: " + BFC.ReadIMEI());
  */
                /*
                                byte[] data = BFC.EeReadBlock(67, 0x44, 0x4);
                                UInt16 p1 = (UInt16)(data[0] | data[1] << 8);
                                short p2 = (short) (data[2] | data[3] << 8);

                                UInt16 tempADC;
                                //Console.WriteLine("VBAT: {0} mV", BFC.GetAkku(0, 3));
                                Console.WriteLine("TENV: {0: ##.##} °C   ADC: ({1})  In: {2}", (float)(BFC.GetAkku(3, 3) - 0xAAA) / 10, tempADC = BFC.AmcGetAnalogDataValue(3, 3), ((p1 * tempADC)/1000)-p2);
                                //Console.WriteLine("TBAT: {0: ##.##} °C", (float)(BFC.GetAkku(1, 3) - 0xAAA) / 10);
                                //Console.WriteLine("BT: {0} Om", BFC.GetAkku(2, 3));
                                //Console.WriteLine("BT-2: {0} Om", BFC.GetAkku(0xB, 3));

                
                                //Console.WriteLine("TVCXO ADC: {0} °C ", tempADC=BFC.AmcGetAnalogDataValue(1,3));
                             //0xDC / ((0x958-adc)*0xA / 0x2C)
                               //BFC.AmcResetCommonCalibrationBlock(); //макс 24 раза можно вызывать
                               //BFC.EeSpaceInfo(0, out freeb, out freea, out freed);
                               //Console.WriteLine("EeSpaceInfo:\r\nfreeb: 0x{0:X8}\r\nfreea: 0x{1:X8} \r\nfreed: 0x{2:X8}", freeb, freea, freed);
                               //Console.WriteLine("EeLite: {0}", BFC.EeMaxBlockId(0));
                               //Console.WriteLine("EeFull: {0}", BFC.EeMaxBlockId(1));
                               //Console.WriteLine("GetSliderKeypad: " + BitConverter.ToString(BFC.GetSliderKeypad()));
                               //Console.WriteLine("TestSliderKeyState: {0:X}", BFC.TestSliderKeyState());
                               //Console.WriteLine("SetSliderKeyInterruptState: " + BitConverter.ToString(BFC.SetSliderKeyInterruptState(0x73, 1)));
                       */
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
        }

/*********************************************************************************************************************/

        private void search_EELITE()
        {
            UInt16 size;
            byte ver;
            int count = 0;

            try
            {
                Console.WriteLine("Поиск EELITE блоков с 0 по 350...");
                for (int c = 0; c <= 350; c++)
                {
                    BFC.EeGiveBlockVersionAndSize((UInt16)c, out size, out ver);
                    if (size != 0xffff) count++;
                }
                Console.WriteLine("Найдено EEP блоков: {0}", count);

            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
        }

/*********************************************************************************************************************/

        private void tabControl1_Selected(object sender, TabControlEventArgs e)
        {
            TabControl tabCtrl = sender as TabControl;
            int index = tabCtrl.SelectedIndex;
            System.Drawing.FontStyle style0 = 0;
            System.Drawing.FontStyle style1 = 0;
            System.Drawing.FontStyle style2 = 0;

            switch(index)
            {
                case 0:
                    if (this.button_V.Font.Bold) style0 = System.Drawing.FontStyle.Regular;
                    else style0 = System.Drawing.FontStyle.Bold;
                    style1 = System.Drawing.FontStyle.Regular;
                    style2 = System.Drawing.FontStyle.Regular;
                    break;
                case 1:
                    if (this.button_R.Font.Bold) style1 = System.Drawing.FontStyle.Regular;
                    else style1 = System.Drawing.FontStyle.Bold;
                    style0 = System.Drawing.FontStyle.Regular;
                    style2 = System.Drawing.FontStyle.Regular;
                    break;
                case 2:
                    if (this.button_T.Font.Bold) style2 = System.Drawing.FontStyle.Regular;
                    else style2 = System.Drawing.FontStyle.Bold;
                    style0 = System.Drawing.FontStyle.Regular;
                    style1 = System.Drawing.FontStyle.Regular;
                    break;
            }

            this.button_V.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, style0, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button_R.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, style1, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button_T.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, style2, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
        }


    }
}

