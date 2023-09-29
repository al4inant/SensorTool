using System;
using System.Collections;
using System.Windows.Forms;

namespace WindowsFormsApplication
{
    public static class EEP
    {

        public static void Write(byte[] rdata, int blkNum)
        {
            var result = MessageBox.Show("Обновить данные блока 67 в EEPROM?", "",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.No)
                return;

            try
            {
                if (BFC.GetCurrentMode() != 0x16) throw new Exception("Отмена операции...Запись в EEPROM возможна только в сервисном режиме");
                ArrayList list = new ArrayList();
                int blks = BitConverter.ToInt32(rdata, 0) + BitConverter.ToInt32(rdata, 4);
                int blkOffset = 8;
                
                for (int i = 0; i < blks; i++)
                {
                    int blk = BitConverter.ToInt32(rdata, blkOffset);
                    int blkSize = BitConverter.ToInt32(rdata, blkOffset + 4);
                    int blkVer = BitConverter.ToInt32(rdata, blkOffset + 8);

                    list.Add(new ArraySegment<byte>(rdata, blkOffset, 0xC + blkSize));
                    blkOffset += (0xC + blkSize);
                }

                if ((blkOffset + 2) != rdata.Length)
                    throw new ArgumentException();

                Console.WriteLine("Найдено блоков в файле: {0}", list.Count);

                ushort hwid = BFC.GetHardwareIdentification();

                if (hwid != BitConverter.ToUInt16(rdata, blkOffset))
                {
                    var result2 = MessageBox.Show("Вы пытаетесь залить блок от другой модели.Всеравно продолжить?", "",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);

                    if (result2 == DialogResult.No)
                    {
                        Console.Write("Отмена записи");
                        return;
                    }
                }

                if (blkNum > -1)
                {
                    if(list.Count > 1)
                        foreach (ArraySegment<byte> seg in list)
                        {
                            if ((BitConverter.ToInt32(seg.Array, seg.Offset)) == blkNum)
                            {
                                list.Clear(); ;
                                list.Add(seg);
                                break;
                            }
                        }
                    if (list.Count != 1) throw new Exception(String.Format("Отмена...Блок {0} не найден.", blkNum));
                }              

                //если файл прошел проверку, начинаем цикл записи
                for ( int i = 0, offset = 0; i < list.Count; i++)
                {
                    ArraySegment<byte> seg = (ArraySegment<byte>)list[i];
                    byte[] data = new byte[seg.Count - 0xC];
                    Array.Copy(seg.Array, seg.Offset + 0xC, data, offset, seg.Count - 0xC);
                    offset += seg.Count - 0xC;

                    byte num = seg.Array[seg.Offset + 0];
                    byte size = seg.Array[seg.Offset + 4];
                    byte ver = seg.Array[seg.Offset + 8];

                    int freeb = -1;
                    int freea = -1;
                    int freed = -1;

                    BFC.EeSpaceInfo(0, out freeb, out freea, out freed);
                    Console.WriteLine("Свободно {0} байт",freeb);
                    if (freeb < (size + 10240)) throw new Exception("Отмена операции...Требуется дефрагментация EELITE");

                    Console.Write("Запись в блок {0} (size={1}, ver={2})...", num, size, ver);
                    BFC.EeCreateBlock(num, (ushort)data.Length, ver);
                    System.Threading.Thread.Sleep(500);
                    BFC.EeWriteBlock(num, 0, data);
                    System.Threading.Thread.Sleep(500);
                    BFC.EeFinishBlock(num);
                    Console.WriteLine("Ok");
                }
            }
            catch (ArgumentException ex) { throw new Exception("Ошибка!..Файл поврежден."); } // при нехватке длины массива
        }
    }
}
