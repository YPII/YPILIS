using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Label.Model
{
    public class ZPLPrinterUSB
    {        

        public ZPLPrinterUSB()
        {            
            
        }

        public void Print(ZPLCommand zplCommand)
        {
            string printerName = "ZDesigner GX430t";
            RawPrinterHelper.SendStringToPrinter(printerName, "^XA" + zplCommand.GetCommand() + "^XZ");
        }

    }
}
