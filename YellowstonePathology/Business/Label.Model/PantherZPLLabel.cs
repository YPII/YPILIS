using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Label.Model
{
    public class PantherZPLLabel
    {
        public PantherZPLLabel()
        {

        }

        public static string GetCommands(string aliquotOrderId, DateTime pBirthdate, string patientName, string specimen)
        {            
            StringBuilder commands = new StringBuilder();
            commands.Append("^XA");
            commands.Append("^FT20,120^BY2^A0N,50,30 ^BC,100,N,N,N,A^FD" + aliquotOrderId + "^FS");
            commands.Append("^FO20,130^ADN,18,10^FD" + aliquotOrderId + ": " + pBirthdate.ToString("MM/dd/yyyy") + "^FS");
            commands.Append("^FO20,150^ADN,18,10^FD" + patientName + "^FS");
            commands.Append("^FO20,170^ADN,18,10^FD" + specimen + "^FS");
            commands.Append("^XZ");
            return commands.ToString();
        }
    }
}
