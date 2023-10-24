using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Label.Model
{
    public class FormalinAddedZPLLabel
    {
        public FormalinAddedZPLLabel()
        {

        }

        public static string GetCommands()
        {                        
            StringBuilder result = new StringBuilder();
            int xOffset = 0;

            result.Append("^XA");
            for (int i = 0; i < 4; i++)
            {
                GetOne(result, xOffset);
                xOffset += 325;
            }

            result.Append("^XZ");
            return result.ToString();
        }

        private static void GetOne(StringBuilder result, int xOffset)
        {
            result.Append("^FO" + (xOffset + 10) + ",030^ATN,50^FD" + "Formalin" + "^FS");
            result.Append("^FO" + (xOffset + 10) + ",080^ATN,50^FD" + "Added" + "^FS");
        }
    }
}