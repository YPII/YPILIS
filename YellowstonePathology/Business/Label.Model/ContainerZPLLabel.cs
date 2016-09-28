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
            string line1 = containerId.Substring(0, 12);
            string line2 = containerId.Substring(13, 12);
            string line3 = containerId.Substring(26);

            StringBuilder commands = new StringBuilder();
            commands.Append("^XA");
            commands.Append("^FO80,100^BXN,04,200^FD" + containerId + "^FS");
            commands.Append("^FO72,032^QDN,50^FD" + "YPI" + "^FS");
            //commands.Append("^FO70,032^ADN,50^FD" + "YPI" + "^FS");
            //commands.Append("^FO70,034^ADN,50^FD" + "YPI" + "^FS");
            commands.Append("^FO23,200^FB190,1,0,C,0^ADN,18^FD" + line1 + "^FS");
            commands.Append("^FO23,220^FB190,1,0,C,0^ADN,18^FD" + line2 + "^FS");
            commands.Append("^FO23,240^FB190,1,0,C,0^ADN,18^FD" + line3 + "^FS");
            //commands.Append("^FT20,120^BY2^A0N,50,30 ^BC,100,N,N,N,A^FD" + aliquotOrderId + "^FS");
            //commands.Append("^FO25,025^ADN,02,140^FD" + "YPI" + "^FS");
            //commands.Append("^FO20,150^ADN,18,10^FD" + patientName + "^FS");
            //commands.Append("^FO20,170^ADN,18,10^FD" + specimen + "^FS");
            commands.Append("^XZ");
            return commands.ToString();
        }
    }
}
