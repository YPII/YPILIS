using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Label.Model
{
    public class ZPLPrinterUSB
    {
        private string m_PrinterName;

        public ZPLPrinterUSB(string printerName)
        {
            this.m_PrinterName = printerName;
        }

        public void Print(ZPLCommand zplCommand)
        {            
            RawPrinterHelper.SendStringToPrinter(this.m_PrinterName, $"^XA{zplCommand.GetCommand()}^XZ");
        }

        public void Print(string zplCommand)
        {
            RawPrinterHelper.SendStringToPrinter(this.m_PrinterName, $"^XA{zplCommand}^XZ");
        }

    }
}
