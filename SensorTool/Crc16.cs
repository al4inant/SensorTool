using System;

public static class Crc16
{
    public static UInt16 CalkBlkCRC16(byte[] buf)
    {
        UInt16 crc = 0xffff;

        foreach (byte b in buf)
        {
            crc ^= b;
            crc = (UInt16)(crc << 8 | crc >> 8);
            crc ^= (UInt16)((crc & 0xff00) << 4);
            crc ^= (UInt16)(crc >> 12);
            crc ^= (UInt16)((crc & 0xff00) >> 5);
        }
        return crc;
    }

    public static UInt16 CalkBlkCRC16(byte[] buf, int offset, int len)
    {
        UInt16 crc = 0xFFFF;

        for (int pos = offset; pos < (offset + len); pos++)
        {
            crc ^= (UInt16)buf[pos];
            crc = (UInt16)(crc << 8 | crc >> 8);
            crc ^= (UInt16)((crc & 0xff00) << 4);
            crc ^= (UInt16)(crc >> 12);
            crc ^= (UInt16)((crc & 0xff00) >> 5);
        }
        return crc;
    }
}

