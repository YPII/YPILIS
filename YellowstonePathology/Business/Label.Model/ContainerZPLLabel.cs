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

        public static string GetCommands(string containerId)
        {
            string line1 = containerId.Substring(0, 14);
            string line2 = containerId.Substring(15, 14);
            string line3 = containerId.Substring(28);

            StringBuilder commands = new StringBuilder();
            commands.Append("^XA");
            commands.Append("^FO75,070^BXN,04,200^FD" + containerId + "^FS");
            commands.Append("^FO85,015^AUN,50,50^FD" + "YPI" + "^FS");
            commands.Append("^FO30,190^FB190,1,0,C,0^ADN,20^FD" + line1 + "^FS");
            commands.Append("^FO30,210^FB190,1,0,C,0^ADN,20^FD" + line2 + "^FS");
            commands.Append("^FO25,240^FB190,1,0,C,0^ARN,18^FD" + line3 + "^FS");

            //commands.Append("^FT20,120^BY2^A0N,50,30 ^BC,100,N,N,N,A^FD" + aliquotOrderId + "^FS");
            //commands.Append("^FO25,025^ADN,02,140^FD" + "YPI" + "^FS");
            //commands.Append("^FO20,150^ADN,18,10^FD" + patientName + "^FS");
            //commands.Append("^FO20,170^ADN,18,10^FD" + specimen + "^FS");
            commands.Append("^XZ");
            return commands.ToString();
        }
    }
}
