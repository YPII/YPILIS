using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Label.Model
{
    public class MaterialStorageLabel
    {
        public MaterialStorageLabel()
        {

        }

        public static string GetCommands(int startNumber, int quantity, string caseType)
        {            
            StringBuilder commands = new StringBuilder();

            int barcodeNumber = startNumber;
            for(int i=0; i<quantity; i++)
            {
                commands.AppendLine("^XA");
                commands.AppendLine($"^FT95,140^BY2^A0N,50,30^BC,100,N,N,N,A^FDSLST{caseType}{barcodeNumber.ToString()}^FS");
                commands.AppendLine($"^FO135,150^ADN,30,30^FD{caseType}{barcodeNumber.ToString()}^FS");
                commands.AppendLine("^XZ");
                barcodeNumber += 1;
            }    
            //Console.WriteLine(commands.ToString());
            return commands.ToString();
        }
    }
}
