using System;
using System.Text;
using System.Timers;

namespace WindowsFormsApplication
{
    static class Boot
    {
        private static Timer aTimer;
        private static int timerN;

        private static byte[] srvBoot = {   0x30,0x57,0x00,
                                            0xF1,0x04,0xA0,0xE3,0x20,0x10,0x90,0xE5,0xFF,0x10,0xC1,0xE3,0xA5,0x10,0x81,0xE3,
                                            0x20,0x10,0x80,0xE5,0x1E,0xFF,0x2F,0xE1,0x04,0x01,0x08,0x00,0x00,0x00,0x00,0x00,
                                            0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x53,0x49,0x45,0x4D,0x45,0x4E,0x53,0x5F,
                                            0x42,0x4F,0x4F,0x54,0x43,0x4F,0x44,0x45,0x01,0x00,0x07,0x00,0x00,0x00,0x00,0x00,
                                            0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,
                                            0x01,0x04,0x05,0x00,0x8B,0x00,0x8B,
                                            0x96};

        public static void SendBoot()
        {
            if(aTimer == null)
            {
                aTimer = new Timer();
                aTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            }

            try
            {
                Serial.serialPort.DiscardInBuffer();
                timerN = 0;
                aTimer.Interval = 50;
                aTimer.Enabled = true;
                Serial.serialPort.ReadTimeout = 500;
                Console.WriteLine("Нажмите на красную кнопку...");
                //System.Threading.Thread.Sleep(100);
            }
            catch (Exception e) { Console.WriteLine(e.Message); }

        }

        private static Boolean Read()
        {
            if (Serial.serialPort.BytesToRead > 0)
            {
               // BFC.inBuffer = new byte[Serial.serialPort.BytesToRead];
                try
                {
                    Serial.serialPort.Read(BUF.IN, 0, Serial.serialPort.BytesToRead);
                    return true;
                }
                catch (Exception e) { Console.WriteLine(e.Message); }
            }
            return false;

        }

        private static void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            int answPing;

            Serial.serialPort.Write(new byte[]{0x41,0x54}, 0, 2);
            if(Read())
            {
                aTimer.Stop();
                answPing = BUF.IN[0];
                if (answPing == 0xC0 || BUF.IN[0] == 0xB0)
                {
                    Console.WriteLine("Пинг: ОК");
                    Serial.serialPort.Write(srvBoot, 0, srvBoot.Length);
                    System.Threading.Thread.Sleep(100);
                    if(Read())
                    {
                        int answBoot = (BUF.IN[0] | (BUF.IN[1] << 8));
                        if (answBoot == 0x06C1 || answBoot == 0x06B0)
                        {
                            Console.WriteLine("Бут загружен");
                            //Form1.form.GetStatUpInfo(answPing);
                        }

                    }
                    else Console.WriteLine("Бут не загружен");
                }

            }

            timerN++;
            if (timerN > 100)
            {
                aTimer.Stop();
                Console.WriteLine("Истекло время ожидания...");
            }
        }


    }
}
