using System;
using System.Collections.Generic;
using System.Text;

namespace YellowstonePathology.Business.BarcodeScanning
{
	public static class CytycCRC32
	{		
        public static string ComputeCrc(string masterAccessionNo)
        {
            byte[] crcInputArray = new byte[11]; //looks like AAAAAAAYYYY 
            byte local8BitPoly = 0x025;			// some given variable A
            byte[] crcTable = new byte[256];	// used in the calc            
            byte crc = 0;						// the calculated value
            byte crc_accum;						// used to assist in calcing the correct value

            string masterAccessionNoWithoutYear = masterAccessionNo.Substring(3).PadLeft(7, '0');
            string masterAccessionNoYear = "20" + masterAccessionNo.Substring(0, 2);
            string crcInput = masterAccessionNoWithoutYear + masterAccessionNoYear;            

            for (int i = 0; i < 11; i++)
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

        public static bool IsCRCValid(string masterAccessionNo, string scannedCrc)
		{
			bool result = false;
            if (!string.IsNullOrEmpty(masterAccessionNo) && !string.IsNullOrEmpty(scannedCrc))
			{
                string computtedCrc = ComputeCrc(masterAccessionNo);
                if (computtedCrc == scannedCrc)
				{
					result = true;
				}                
			}            
			return result;
		}
	}
}
