using System;
using System.Collections.Generic;
using System.Text;

namespace YellowstonePathology.Business.BarcodeScanning
{
	public static class CRC32
	{
        public static string ComputeCRC(string input)
        {
            string crcInput = input.PadLeft(15, '0');
            byte[] crcInputArray = new byte[15];
            byte local8BitPoly = 0x025;
            byte[] crcTable = new byte[256];
            byte crc = 0;
            byte crc_accum;

            for (int i = 0; i < 15; i++)
            {
                crcInputArray[i] = Convert.ToByte(crcInput.Substring(i, 1)[0]);
            }            
            
            for (int i = 0; i < 256; i++)
            {
                crc_accum = (byte)i;

                for (int j = 0; j < 8; j++)
                {
                    if ((crc_accum & 0x80) != 0)
                        crc_accum = (byte)((crc_accum << 1) ^ local8BitPoly);
                    else
                        crc_accum = (byte)(crc_accum << 1);
                }
                crcTable[i] = crc_accum;
            }
            for (int j = 0; j < 11; j++)
            {
                short i = (short)(((crc) ^ crcInputArray[j]) & 0xff);
                crc = (byte)((crc << 8) ^ crcTable[i]);
            }

            string crcZeroPadded = crc.ToString().PadLeft(3, '0');
            return crcZeroPadded;
        }        
	}
}
