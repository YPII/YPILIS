using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Label.Model
{
    public class StorageSlideLabel
    {        
        private YellowstonePathology.Business.BarcodeScanning.SlideStorage m_Barcode;        

        public StorageSlideLabel(YellowstonePathology.Business.BarcodeScanning.SlideStorage barcode)
        {
            this.m_Barcode = barcode;            
        }

        public void AppendCommands(StringBuilder zplString, int xOffset, int yOffset)
        {                     
            zplString.Append("^FO" + (90 + xOffset) + "," + (40 + yOffset) + "^A@N,25,25,E:VERDANA.TTF^FD" + this.m_Barcode.StorageName + "^FS");
            string address = "C" + this.m_Barcode.CabinetNumber + ":" + "D" + this.m_Barcode.DrawerNumber;
            zplString.Append("^FO" + (90 + xOffset) + "," + (80 + yOffset) + "^A@N,30,30,E:VERDANA.TTF^FD" + address + "^FS");            
            zplString.Append("^FO" + (90 + xOffset) + "," + (130 + yOffset) + "^BXN,4,200^AD^FH^FD" + this.m_Barcode.m_SlideStorageId + "^FS");

            zplString.Append("^FO" + (200 + xOffset) + "," + (140 + yOffset) + "^A@N,25,25,E:VERDANA.TTF^FD" + this.m_Barcode.Facility + "^FS");
            zplString.Append("^FO" + (200 + xOffset) + "," + (170 + yOffset) + "^A@N,25,25,E:VERDANA.TTF^FD" + this.m_Barcode.Year + "^FS");

            string numberRange = this.m_Barcode.StartNumber + "-" + this.m_Barcode.EndNumber;
            zplString.Append("^FO" + (90 + xOffset) + "," + (240 + yOffset) + "^A@N,25,25,E:VERDANA.TTF^FD" + numberRange + "^FS");

        }

        public string TruncateString(string text, int width)
        {
            string result = string.Empty;
            if (string.IsNullOrEmpty(text) == false)
            {
                if (text.Length > width)
                {
                    result = text.Substring(0, width);
                }
                else
                {
                    result = text;
                }
            }
            return result;
        }
    }
}
