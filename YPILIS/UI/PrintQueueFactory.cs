using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI
{
    public class PrintQueueFactory
    {
        public static System.Printing.PrintQueue GetSlideLabelPrintQueue(string slideLabelPrinter)
        {
            System.Printing.PrintQueue printQueue = null;
            System.Printing.PrintServer printServer = null;

            switch (slideLabelPrinter)
            {
                case "Histology Slide Label Printer":
                    printServer = new System.Printing.LocalPrintServer();                   
                    printQueue = printServer.GetPrintQueue("Histology Slide Label Printer");                    
                    break;
                case "Cytology Slide Label Printer":
                    printServer = new System.Printing.PrintServer(@"\\YPIIHISTO03");
                    printQueue = printServer.GetPrintQueue("CytologySlideLabelPrinter");
                    break;
            }

            return printQueue;
        }
    }
}
