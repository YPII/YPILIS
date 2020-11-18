using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Label.Model
{
    public class ReportNoLabelPrinter
    {                                
        public static void Print(ReportNoLabel label, string printerName, int columns)
        {
            StringBuilder result = new StringBuilder();
            int xOffset = 0;
            int yOffset = 23;            

            result.Append("^XA");
            for (int i = 0; i < columns; i++)
            {
                label.AppendCommands(result, xOffset, yOffset);
                xOffset += 310;
            }
            result.Append("^XZ");            
            RawPrinterHelper.SendStringToPrinter(printerName, result.ToString());
        }        
    }
}
