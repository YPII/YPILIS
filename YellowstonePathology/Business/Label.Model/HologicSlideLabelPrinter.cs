using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Label.Model
{
    public class HologicSlideLabelPrinter
    {                                
        public static void Print(HologicSlideLabel label, string printerName, int columns)
        {
            StringBuilder result = new StringBuilder();
            int xOffset = 0;
            int yOffset = 0;

            if(printerName.Contains("GK420") == true)
            {
                xOffset = 10;
                yOffset = 10;
            }

            result.Append("^XA");
            for (int i = 0; i < columns; i++)
            {
                label.AppendCommands(result, xOffset, yOffset);
                xOffset += 225;
            }
            result.Append("^XZ");            
            RawPrinterHelper.SendStringToPrinter(printerName, result.ToString());
        }        
    }
}
