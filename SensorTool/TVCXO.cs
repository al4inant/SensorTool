using System;
using System.Collections.Generic;

namespace WindowsFormsApplication
{
    public static class TVCXO
    {
        public static UInt16[] table = { 3751, 3745, 3739, 3733, 3726, 3718, 3710, 3702,
                                         3693, 3684, 3674, 3663, 3652, 3640, 3628, 3614,
                                         3600, 3586, 3570, 3554, 3537, 3519, 3501, 3481,
                                         3461, 3440, 3417, 3394, 3370, 3345, 3319, 3292,
                                         3264, 3235, 3205, 3174, 3142, 3110, 3076, 3041,
                                         3006, 2932, 2894, 2856, 2816, 2776, 2735, 2694,
                                         2652, 2610, 2567, 2524, 2480, 2436, 2392, 2348,
                                         2304, 2260, 2215, 2171, 2127, 2083, 2039, 1996,
                                         1952, 1910, 1867, 1825, 1784, 1743, 1702, 1663,
                                         1624, 1585, 1547, 1510, 1474, 1438, 1403, 1369,
                                         1335, 1302, 1270, 1239, 1209, 1179, 1150, 1122,
                                         1095, 1068, 1042, 1017,  992,  969,  945,  923,
                                          901,  880,  860,  840,  821,  802,  784,  767,
                                          750,  733,  718,  702,  687,  673 };

/****************************************************************************************************/

        public static UInt16 calcTemp(UInt16 analog_v)
        {
            UInt16 r = 0;
            
            if (table[0] >= analog_v && table[table.Length - 1] <= analog_v)
            {
                UInt16 last_p = 0;
                int last_i = 0;

                for (int i = 0; i <= table.Length; i++ )
                {
                    if (table[i] < analog_v)
                    {
                        r = (UInt16)((byte)(((last_p - analog_v) * 0xA) / (last_p - table[i])) + ((last_i * 4 + last_i) * 2) + 0x97E);
                        break;
                    }

                    if (table[i] == analog_v)
                    {
                        r = (UInt16)(table[i] + ((last_i * 4 + last_i) * 2) + 0x97E);
                        break; 
                    }

                    last_p = table[i];
                    last_i = i;
                }
            }
            else
            {
                int i = 0;
                if (table[table.Length - 1] > analog_v)
                    i = table.Length / 2;
                r = (UInt16)(((i * 4 + i) * 2) + 0x97E);
            }
            return r;
        }

/****************************************************************************************************/
        //Температура с двумя знаками после запятой.Например temp==992 это - 9.92 °C 
        public static UInt16 calcTempDat(UInt16 temp) 
        {
            UInt16 r = 0;
            UInt16 i1 = 0;
            int t = temp/10;
            int d = temp%10;

            for (UInt16 i = 673; i <= 3751; i++)
            {
                UInt16 p = (UInt16)(calcTemp(i) - 0xAAA);
                if (t == p && i1 == 0) i1 = i;
                if (t > p)
                {
                    float c = 0xA/(i- i1);
                    int n = d/(int)c;
                    r = (UInt16)(i - n -1);
                    break;
                }
            }
            return r;
        }
    }
}
