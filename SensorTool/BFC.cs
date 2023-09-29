//SWriteMap отправляет по 0x35 байт данных
//EEPROM_Tool - 0x1d

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication
{
    public static class BFC
    {
        private const string BFC_ERROR = "BFC_ERROR";

        private  struct BFCHeader
        {
            public Byte cmd;
            public Byte rx;
            public UInt16 dataLen;
            public Byte chkFlag;
            public Byte headXOR; 
        }
         
        private static void WriteBFC(byte cmd, byte chkFlag, byte[] buf)
        {
            BUF.OUT[0] = cmd;
            BUF.OUT[1] = 1;
            BUF.OUT[2] = (byte)(buf.Length >> 8);
            BUF.OUT[3] = (byte)buf.Length;
            BUF.OUT[4] = chkFlag;
            BUF.OUT[5] = (byte)(BUF.OUT[0] ^ BUF.OUT[1] ^ BUF.OUT[2] ^ BUF.OUT[3] ^ BUF.OUT[4]);

            buf.CopyTo(BUF.OUT, 6);

            if (chkFlag == 0x20)
            {
                UInt16 chkSum = Crc16.CalkBlkCRC16(BUF.OUT, 0, 6 + buf.Length);
                BUF.OUT[6 + buf.Length] = (byte)(chkSum >> 8);
                BUF.OUT[6 + buf.Length + 1] = (byte)chkSum;
            }

            try
            {
                Serial.serialPort.DiscardInBuffer();
                Serial.serialPort.Write(BUF.OUT, 0, 6 + buf.Length + (chkFlag >> 4));
                //Console.WriteLine("send: " + BitConverter.ToString(BUF.OUT));
            }
            catch { throw; }
            
        }

        private static byte[] ReadBFC()
        {
            int bytesToRead;
            int count = 0;
            byte[] data = null;

            Serial.serialPort.ReadTimeout = 500;
            TimeSpan interval;
            DateTime t0 = DateTime.Now;

            while (((interval = DateTime.Now - t0).Milliseconds) < Serial.ComTimeout) //время ожидания ответа 500 мс 
            {
                if (Serial.serialPort.BytesToRead > 0)
                {
                    while ((bytesToRead = Serial.serialPort.BytesToRead) > 0)
                    {
                        try
                        {
                            Serial.serialPort.Read(BUF.IN, count, bytesToRead);
                            count += bytesToRead;
                            System.Threading.Thread.Sleep(1); // пауза-может не все данные были прочитаны                    
                        }
                        catch { throw; }
                    }
                    //Console.WriteLine("read " + BitConverter.ToString(BUF.IN, 0, count));
                    data = parseBFC(BUF.IN, count);
                    break;
                }
            }

            if (count == 0) throw new BFCException("Нет ответа...");
            return data;
        }

        private static byte[] parseBFC(byte[] buf, int len)
        {
            UInt16 dataLen;
            byte xor;
            UInt16 chkSum;
            ArrayList list = new ArrayList();
            int count = 0;
            int offset = 0;
            int blkOffset = 0;

           if(len > 6)
           {  
               xor = (byte)(BUF.IN[0] ^ BUF.IN[1] ^ (BUF.IN[2] << 8) ^ BUF.IN[3] ^ BUF.IN[4]);

               if(xor == BUF.IN[5])
               {
                   dataLen = (UInt16) (BUF.IN[3] | (BUF.IN[2] << 8));

                   if (BUF.IN[4] == 0x04) //authentificate Command Group
                       list.Add(new ArraySegment<byte>(BUF.IN, 6, 2));

                   else
                       if (BUF.IN[4] == 0)//no checksum //приходит 2 блока после перехода в AT режим
                       {
                           list.Add(new ArraySegment<byte>(BUF.IN, 6, dataLen));
                       }
                       else
                           if (BUF.IN[4] == 0x20 || BUF.IN[4] == 0x21)//crc16 one block
                           {
                               chkSum = (UInt16)(BUF.IN[6 + dataLen + 1] | (BUF.IN[6 + dataLen] << 8));

                               if (chkSum == Crc16.CalkBlkCRC16(BUF.IN, 0, 6 + dataLen))
                                   list.Add(new ArraySegment<byte>(BUF.IN, 6 + (BUF.IN[4] & 0x0F), dataLen - (BUF.IN[4] & 0x0F)));
                               if (BUF.IN[4] == 0x21) //crc16 n blocks
                               {
                                   while ((BUF.IN[blkOffset + 6] & 0xF0) != 0x80)
                                   {
                                       blkOffset += (6 + dataLen + 2);
                                       dataLen = (UInt16)(BUF.IN[blkOffset + 3] | (BUF.IN[blkOffset + 2] << 8));
                                       xor = (byte)(BUF.IN[blkOffset] ^ BUF.IN[blkOffset + 1] ^ (BUF.IN[blkOffset + 2] << 8) ^ BUF.IN[blkOffset + 3] ^ BUF.IN[blkOffset + 4]);
                                       if (xor == BUF.IN[blkOffset + 5])
                                       {
                                           chkSum = (UInt16)(BUF.IN[blkOffset + 6 + dataLen + 1] | (BUF.IN[blkOffset + 6 + dataLen] << 8));
                                           if (chkSum == Crc16.CalkBlkCRC16(BUF.IN, blkOffset, 6 + dataLen))
                                               list.Add(new ArraySegment<byte>(BUF.IN, blkOffset + 6 + 1, dataLen - 1));
                                           else { list.Clear(); break; }
                                       }
                                   }
                               }
                           }
               }
           }
            if (list.Count == 0) throw new Exception("Данные повреждены");

            foreach (ArraySegment<byte> dataSeg in list)//считаем сколько байт данных имеем
            {
                count += dataSeg.Count;
            }

            byte[] data = new byte[count];
       
            foreach (ArraySegment<byte> dataSeg in list)
            {
                Array.Copy(dataSeg.Array, dataSeg.Offset, data, offset, dataSeg.Count);
                offset += dataSeg.Count;             
            }

            return data;

        }

        public static void InitCmdGroup(byte cmd)
        {
            byte[] rdata;
            try
            {
                WriteBFC(cmd, 0x04, new byte[] { 0x80, 0x11 });
                rdata = ReadBFC();
           
            }
            catch  { throw; }
        }

        private static byte[] BFC_Funcs00(byte cmd, byte[] data)
        {
            byte[] rdata;
            try
            {
                InitCmdGroup(cmd);
                WriteBFC(cmd, 0x0, data);
                rdata = ReadBFC();
            }
            catch { throw; }
            return rdata;

        }

        private static byte[] BFC_Funcs20(byte cmd, byte[] data)
        {
            byte[] rdata;

            try
            {
                InitCmdGroup(cmd);
                WriteBFC(cmd, 0x20, data);
                rdata = ReadBFC();
            }
            catch  { throw; }
            return rdata;
        }

/****************************************************************************************************/

        public static int GetCurrentMode()
        {// 0x12=NormalMode, 0x16=ServiceMode, 0x07=BurninMode, 0x03-Charge Mode, 0xFE-Format FFS??
            byte cmd = 0x02;
            try
            {
                byte[] rdata = BFC_Funcs20(0x19, new byte[] { cmd });
                if (rdata.Length == 2)
                    return rdata[1];
                else
                    if (rdata.Length == 1)
                    return 0;//Система не успела полностью загрузиться
                else
                        throw new BFCException("BFC_ERROR (GetCurrentMode)");

            }
            catch { throw; }
        }
        

/****************************************************************************************************/
        public static byte[] GetSecurityInfo()
        {
            byte cmd = 0x15;
            try
            {
                byte[] rdata = BFC_Funcs20(0x25, new byte[] { cmd, 8, 0, 0, });
                if (rdata[0] == cmd )
                    return rdata;
                else
                    throw new BFCException("BFC_ERROR (GetSecurityInfo)");
            }
            catch { throw; }
        }

/****************************************************************************************************/

        public static UInt16 GetHardwareIdentification()
        {
            byte cmd = 0x1;

            try
            {
                byte[] rdata = BFC_Funcs20(0x11, new byte[] { cmd });
                if (rdata.Length == 3 && rdata[0] == cmd)
                    return (UInt16)(rdata[1] | rdata[2] << 8);
                else
                    throw new BFCException("BFC_ERROR (GetHardwareIdentification)");
            }
            catch { throw; }
        }

/****************************************************************************************************/

        public static String GetSoftWareVer()
        {
            byte cmd = 0xB;
            try
            {
                byte[] rdata = BFC_Funcs20(0x11, new byte[] { cmd });
                if (rdata[0] == cmd)
                    return Encoding.ASCII.GetString(rdata, 1, rdata.Length - 2);
                else
                    throw new BFCException("BFC_ERROR (GetSoftWareVer)");
            }
            catch { throw; }
        }

/****************************************************************************************************/

        public static String GetPhoneModel()
        {
            byte cmd = 0xD;
            try
            {
                byte[] rdata = BFC_Funcs20(0x11, new byte[] { cmd });
                if (rdata[0] == cmd)
                    return Encoding.ASCII.GetString(rdata, 1, rdata.Length - 2);
                else
                    throw new BFCException("BFC_ERROR (GetPhoneModel)");
            }
            catch { throw; }
        }

/****************************************************************************************************/

        public static String ReadIMEI()
        {
            byte cmd = 5;           
            try
            {
                byte[] rdata = BFC_Funcs20(0x11, new byte[] { cmd });
                if (rdata.Length == 17 && rdata[0] == cmd)
                    return Encoding.ASCII.GetString(rdata, 1, rdata.Length - 2);
                else
                    throw new BFCException("BFC_ERROR (ReadIMEI)");
            }
            catch { throw; }
        }

/****************************************************************************************************/

        public static void EeSpaceInfo(int ee, out int freeb, out int freea, out int freed) //ee=0-EeLite, ee=1-EEFull
        {
            byte cmd = (byte)((ee > 0) ? 0x18 : 0x08);
            
            try
            {
                byte[] rdata = BFC_Funcs20(0x14, new byte[] { (byte)cmd });
                int EeErr = rdata.Length >= 2 ? rdata[1] : 0xff;
                if (rdata.Length == 14 && rdata[0] == cmd && rdata[1] == 0)
                {
                    freeb = rdata[5] << 24 | rdata[4] << 16 | rdata[3] << 8 | rdata[2];
                    freea = rdata[9] << 24 | rdata[8] << 16 | rdata[7] << 8 | rdata[6];
                    freed = rdata[13] << 24 | rdata[12] << 16 | rdata[11] << 8 | rdata[10];
                }
                else
                    throw new BFCException("BFC_ERROR (EeSpaceInfo): " + EeErr);
            }
            catch { throw; }
        }

/****************************************************************************************************/

        public static void EeGiveBlockVersionAndSize(UInt16 num, out UInt16 size, out byte version)
        {
            size = 0xffff;
            version = 0xff;

            byte cmd = (byte)((num >= 5000) ? 0x15 : 0x05);

            try
            {
                byte[] rdata = BFC_Funcs20(0x14, new byte[] { (byte)cmd, (byte)num, (byte)(num >> 8), 0x0, 0x0 });
                int EeErr = rdata.Length >= 2 ? rdata[1] : 0xff;
                if (rdata.Length == 7 && rdata[0] == cmd && rdata[1] == 0)
                {
                    size = (UInt16)(rdata[2] | (rdata[3] << 8));
                    version = rdata[6];
                }
                else
                    if (rdata.Length == 2 && rdata[1] == 2) ; //блок отсутствует, поэтому size == 0xffff
                    else
                        throw new BFCException("BFC_ERROR (EeGiveBlockVersionAndSize): " + EeErr);
            }
            catch { throw; }
        }

/****************************************************************************************************/

        public static UInt16 EeMaxBlockId(int ee)   //0-eelite, 1-eefull 
        {
            byte cmd = (byte)((ee > 0) ? 0x16 : 0x06);                         

            try
            {
                byte[] rdata = BFC_Funcs20(0x14, new byte[] { (byte)cmd });
                int EeErr = rdata.Length >= 2 ? rdata[1] : 0xff;
                if (rdata.Length == 6 && rdata[0] == cmd && rdata[1] == 0);
                else
                    throw new BFCException("BFC_ERROR (EeMaxBlockId): " + EeErr);
                return (UInt16)(rdata[2] | (rdata[3] << 8));
            }
            catch { throw; }
        }

/****************************************************************************************************/

        public static void EeCreateBlock(UInt16 num, UInt16 len, byte ver)
        {
            byte cmd = (byte)((num >= 5000) ? 0x11 : 0x01); ;

            try
            {
                byte[] rdata = BFC_Funcs20(0x14, new byte[] { (byte)cmd, (byte)num, (byte)(num >> 8), 0, 0, (byte)len, (byte)(len >> 8), 0, 0, ver });
                int EeErr = rdata.Length >= 2 ? rdata[1] : 0xff;
                if (rdata[0] == cmd && rdata[1] == 0);
                else
                    throw new BFCException("BFC_ERROR (EeCreateBlock): " + EeErr);
            }
            catch { throw; }
        }

/****************************************************************************************************/

        public static byte[] EeReadBlock(UInt16 num, UInt16 offset, UInt16 len)
        {
            byte cmd = (byte)((num >= 5000) ? 0x14 : 0x04);

            try
            {
                byte[] rdata = BFC_Funcs20(0x14, new byte[] { (byte)cmd, (byte)num, (byte)(num >> 8), 0, 0, (byte)offset, (byte)(offset >> 8), 0, 0, (byte)len, (byte)(len >> 8), 0, 0 });
                int EeErr = rdata.Length >= 2 ? rdata[1] : 0xff;
                if (len == (rdata.Length - 2) && rdata[0] == cmd && rdata[1] == 0)
                {
                    Array.Copy(rdata, 2, rdata, 0, len);
                    Array.Resize(ref rdata, len);
                }
                else
                    throw new BFCException("BFC_ERROR (EeReadBlock): " + EeErr);
                return rdata;
            }
            catch { throw; }

        }

/****************************************************************************************************/

        public static void EeWriteBlock(UInt16 num, UInt16 offset, byte[] buf)
        {
            ArrayList list = new ArrayList();
            byte cmd = (byte)(num >= 5000 ? 0x12 : 0x2);
            
            for(int off = 0; off < buf.Length; off += ((ArraySegment<byte>)list[list.Count-1]).Count) //получаем кол-во элементов ArrayList и из последнего элемента читаем длину сегмента
                list.Add(new ArraySegment<byte>(buf, off, (buf.Length - off) > 45 ? 45 : buf.Length - off ));
            foreach(ArraySegment<byte> seg in list)
            {
                byte[] data = new byte[9 + seg.Count];
                data[0] = cmd;
                data[1] = (byte)num;
                data[2] = (byte)(num >> 8);
                data[3] = 0;
                data[4] = 0;
                data[5] = (byte)(offset + seg.Offset);
                data[6] = (byte)((offset + seg.Offset) >> 8);
                data[7] = 0;
                data[8] = 0;
                
                try
                {
                    Array.Copy(seg.Array, seg.Offset, data, 9, seg.Count);
                    byte[] rdata = BFC_Funcs20(0x14, data);
                    int EeErr = rdata.Length >= 2 ? rdata[1] : 0xff;
                    if (rdata[0]==cmd && rdata[1]==0);
                    else
                        throw new BFCException("BFC_ERROR (EeWriteBlock): " + EeErr);
                }
                catch { throw; }
            }
        }

/****************************************************************************************************/

        public static void EeFinishBlock(UInt16 num)
        {
            byte cmd = (byte)((num >= 5000) ? 0x13 : 0x03);

            try
            {
                byte[] rdata = BFC_Funcs20(0x14, new byte[] {cmd, (byte)num, (byte)(num >> 8), 0, 0});
                int EeErr = rdata.Length >= 2 ? rdata[1] : 0xff;
                if (rdata.Length == 2 && rdata[0] == cmd && rdata[1] == 0);
                else
                    throw new BFCException("BFC_ERROR (EeFinishBlock): " + EeErr);
            }
            catch { throw; }
        }

/****************************************************************************************************/

        public static void EeDeleteBlock(UInt16 num)
        {
            byte cmd = (byte)((num >= 5000) ? 0x17 : 0x07);

            try
            {
                byte[] rdata = BFC_Funcs20(0x14, new byte[] { cmd, (byte)num, (byte)(num >> 8), 0, 0 });
                int EeErr = rdata.Length >= 2 ? rdata[1] : 0xff;
                if (rdata.Length == 2 && rdata[0] == cmd && rdata[1] == 0) ;
                else
                    throw new BFCException("BFC_ERROR (EeDeleteBlock): " + EeErr);
            }
            catch { throw; }
        }

/****************************************************************************************************/

        public static void EeEraseAll(int ee) //0-eelite, 1-eefull
        {
            byte cmd = (byte)((ee > 0) ? 0x19 : 0x09);

            try
            {
                byte[] rdata = BFC_Funcs20(0x14, new byte[] { cmd });
                int EeErr = rdata.Length >= 2 ? rdata[1] : 0xff;
                if (rdata[0] == cmd && rdata[1] == 0);
                else
                    throw new BFCException("BFC_ERROR (EeEraseAll): " + EeErr);
            }
            catch { throw; }
        }

/****************************************************************************************************/

        public static short GetVBat()
        {
            short voltage;

            try
            {
                byte[] rdata = BFC_Funcs20(0x0E, new byte[] { 0x02 });
                if ( rdata.Length == 3 && rdata[0] == 2)
                    voltage = (short)((rdata[1] << 8) | rdata[2]);
                else
                    throw new BFCException("BFC_ERROR (GetVBat)");
                return voltage;
            }
            catch { throw; }
        }

/****************************************************************************************************/

        public static void SwitchFromBfcToRccpMode()
        {
            byte[] rdata;
            try
            {
                rdata = BFC_Funcs20(0x17, new byte[] { 0x41, 0x54, 0x5E, 0x53, 0x51, 0x57, 0x45, 0x3D, 0x30, 0x0D }); //"AT^SQWE=0"+0x0D
            }
            catch (BFCException e) { throw e; }
        }

/****************************************************************************************************/

        public static void SwitchFromServiceToNormalMode()
        {
            try
            {
                BFC_Funcs20(0x19, new byte[] { 0x01 });
            }
            catch (BFCException e) { throw e; }
        }

/****************************************************************************************************/

        public static void SwitchMobileOff()
        {
            try
            {
                BFC_Funcs20(0x19, new byte[] { 0x03 });
            }
            catch { throw; }
        }

/****************************************************************************************************/

        public static void SimulateSim()
        {
            try
            {
                BFC_Funcs20(0x1C, new byte[] { 0x01 });
            }
            catch { throw; }
        }

/****************************************************************************************************/

        public static short GetAkku(int p1, int p2)
        {
            byte[] rdata;
            try
            {
                rdata = BFC_Funcs20(0x12, new byte[] { 0x12, 0x01, (byte)p1, (byte)p2 });
                if (rdata.Length == 3 && rdata[0] == 0x12)
                    return (short)((rdata[2] << 8) | rdata[1]);
                else
                    throw new BFCException("BFC_ERROR (GetAkku)");            
            }
            catch { throw; }
        }

/****************************************************************************************************/

        public static UInt16 AmcGetAnalogDataValue(int p1, int p2)
        {
            byte[] rdata;
            try
            {
                rdata = BFC_Funcs20(0x12, new byte[] { 0x12, 0x02, (byte)p1, (byte)p2 });
                if (rdata.Length == 3 && rdata[0] == 0x12)
                    return (UInt16)((rdata[2] << 8) | rdata[1]);
                else
                    throw new BFCException("BFC_ERROR (AmcGetAnalogDataValue)");
            }
            catch { throw; }
        }

/****************************************************************************************************/

        public static Int32 AmcGetRamAdr(int p1, int p2)
        {
            byte[] rdata;
            try
            {
                rdata = BFC_Funcs20(0x12, new byte[] { 0x12, 0x03, (byte)p1, (byte)p2 });
                if (rdata[0] == 0x12 && rdata.Length == 5)
                    return (rdata[1] | rdata[2] << 8 | rdata[3] << 16 | rdata[4] << 24);
                else
                    throw new BFCException("BFC_ERROR (AmcGetRamAdr)");
            }
            catch { throw; }
        }

/****************************************************************************************************/

        public static Int32 AkkuUnkCmd5()
        {
            byte[] rdata;
            try
            {
                rdata = BFC_Funcs20(0x12, new byte[] { 0x12, 0x05});
                if (rdata.Length == 3 && rdata[0] == 0x12)
                    return (rdata[1] | rdata[2] << 8);
                else
                    throw new BFCException("BFC_ERROR (AkkuUnkCmd3)");
            }
            catch { throw; }
        }

        /****************************************************************************************************/
       public static void AmcResetCommonCalibrationBlock()
        {
            try
            {
                byte[] rdata = BFC_Funcs20(0x12, new byte[] { 0x12, 0x08 });
                if (rdata.Length == 1 && rdata[0] == 0x12);
                else
                    throw new BFCException("BFC_ERROR (AmcResetCommonCalibrationBlock)");
            }
            catch { throw; }
        }

/****************************************************************************************************/

       public static void AmcCharge()
       {
           try
           {
               BFC_Funcs20(0x12, new byte[] { 0x12, 0x09 });
           }
           catch { throw; }
       }

/****************************************************************************************************/

        public static void LightSwitch(int channel, byte intensity, ushort fadeTime)
        {
            try
            {
                BFC_Funcs20(0x1A, new byte[] { 0x47, (byte)channel, 0xAF, 0x01, (byte)intensity, (byte)fadeTime, (byte)(fadeTime >> 8) });
            }
            catch { throw; }
        }

/****************************************************************************************************/

        public static void LightGetIllumination(int channel, out byte level, out byte fade )
        {
            try
            {
                byte[] rdata = BFC_Funcs20(0x1A, new byte[] { 0x48, (byte)channel, 0, 1 });
                if (rdata.Length == 4 && rdata[0] == 0x48)
                {
                    level = rdata[3];
                    fade = rdata[2];
                }
                else
                    throw new BFCException("BFC_ERROR (LightGetIllumination)");
            }
            catch { throw; }
        }

/****************************************************************************************************/

        public static byte[] GetSliderKeypad() 
        {
            byte[] rdata;
            try
            {
                rdata = BFC_Funcs20(9, new byte[] { 7 });
                if (rdata[0] == 7) return rdata;
                else
                    throw new BFCException("BFC_ERROR (GetSliderKeypad)");
            }
            catch { throw; }
        }

/****************************************************************************************************/

        public static byte[] SetSliderKeyInterruptState(int key, int state) //не работает.. p1=0x71,0x72,0x73,0x74; p2=0,1;
        {
            byte[] rdata;
            try
            {
                rdata = BFC_Funcs20(9, new byte[] { 5, (byte)key, (byte)state });
                if (rdata[0] == 5) return rdata;
                else
                    throw new BFCException("BFC_ERROR (SetSliderKeyInterruptState)");
            }
            catch { throw; }
        }
/****************************************************************************************************/

        public static UInt16 TestSliderKeyState() //1-close, 0x100-open, 0x202-not supported
        {
            byte[] rdata;
            try
            {
                rdata = BFC_Funcs20(9, new byte[] { 6 });
                if (rdata[0] == 6) return (UInt16)(rdata[2] << 8 | rdata[1]);
                else
                    throw new BFCException("BFC_ERROR (TestSliderKeyState)");
            }
            catch { throw; }
        }

/****************************************************************************************************/
    }
}

//GetAkku(0,3)- напряжение
//GetAkku(1,3)- температура аккума
//GetAkku(3,3)- температура VCXO
//GetAkku(2,3) и GetAkku(0xB,3) - при измерении сопротивления резистора


//TBAT: TENV - 0x1E; или GetAkku(1,3) - 0xAAA) / 10;
//TENV: GetAkku(3,3) - 0xAAA) / 10;

// ostatok=(tab[index+1]-val) * 0xA / (tab[index+1]-tab[index])
// ten=(index * 4 + index) * 2 + ostatok + 0x97E 
//Temp: %d,%d°C %d°F",(GetAkku(1,3)-0xAAA+15)/10,(GetAkku(1,3)-0xAAA+15)%10,((9*(GetAkku(1,3)-0xAAA+15))/50)+32,((9*(GetAkku(1,3)-0xAAA+15))/5)+32);
//Блок 67:

//NewSgold
// вычисление первого параметра в блоке: AV=AmcGetAnalogDataValue(0,2);  mV = ((v3 * AV) / 1000) - x3const;   v3const= (v3 * AV) / 1000) - mV; 
// вычисление второго параметра в блоке:
//AV=AmcGetAnalogDataValue(2,3);  ohm = const - ((resstanse * AV) / 1000) ;  const= (resist * AV) / 1000) 
//AV=AmcGetAnalogDataValue(0xb,3);  ohm = (const - ((resstanse * AV) / 1000)) * 2 ;  const= (resist * AV) / 1000)

//Sgold
// ((p2_0-AV) * 0x3EA)/p1_0 + p_1