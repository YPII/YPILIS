using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Label.Model
{
    public class ContainerZPLLabel
    {
        public ContainerZPLLabel()
        {

        }

        public static string GetCommands()
        {                        
            StringBuilder result = new StringBuilder();
            int xOffset = 0;

            result.Append("^XA^PW1248");
            for (int i = 0; i < 4; i++)
            {
                YellowstonePathology.Business.BarcodeScanning.ContainerBarcode containerBarcode = Business.BarcodeScanning.ContainerBarcode.Parse();
                GetOne(containerBarcode.ToString(), result, xOffset);                
                xOffset += 320;
            }
            
            result.Append("^XZ");
            return result.ToString();
        }

        private static void GetOne(string containerId, StringBuilder result, int xOffset)
        {
            string line1 = containerId.Substring(0, 14);
            string line2 = containerId.Substring(14, 14);
            string line3 = containerId.Substring(28);


            result.Append("^FO" + (xOffset + 85) + ",020^AUN,50,50^FD" + "YPI" + "^FS");
            result.Append("^FO" + (xOffset + 75) + ",080^BXN,04,200^FD" + containerId + "^FS");
            result.Append("^FO" + (xOffset + 30) + ",190^FB190,1,0,C,0^ADN,20^FD" + line1 + "^FS");
            result.Append("^FO" + (xOffset + 30) + ",210^FB190,1,0,C,0^ADN,20^FD" + line2 + "^FS");
            result.Append("^FO" + (xOffset + 25) + ",240^FB190,1,0,C,0^ARN,18^FD" + line3 + "^FS");
        }
    }
}