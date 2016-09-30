using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Drawing;
using System.Drawing.Imaging;

namespace YellowstonePathology.UI.Login
{
    public class BarcodeLabelDocument
    {
        /*
        private System.Drawing.Printing.PrintDocument m_PrintDocument;
        private Queue<YellowstonePathology.Business.BarcodeScanning.IScanable> m_ScannableQueue;        
        
		public BarcodeLabelDocument(YellowstonePathology.Business.BarcodeScanning.BarcodePrefixEnum barcodePrefix, int pages)
        {
            this.m_PrintDocument = new System.Drawing.Printing.PrintDocument();
            this.m_ScannableQueue = new Queue<Business.BarcodeScanning.IScanable>();

            for (int p = 0; p < pages; p++)
            {
                for (int i = 0; i < 4; i++)
                {
                    YellowstonePathology.Business.BarcodeScanning.IScanable scannable = YellowstonePathology.Business.BarcodeScanning.ScanableFactory.GetScanable(barcodePrefix);
                    scannable.FromGuid();
                    this.m_ScannableQueue.Enqueue(scannable);
                }                
            }            
        }       

        public void Print(System.Printing.PrintQueue printQueue)
        {
            this.m_PrintDocument.PrinterSettings.PrinterName = printQueue.FullName;
            this.m_PrintDocument.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(PrintDocument_PrintPage);
            this.m_PrintDocument.Print();
        }

        private void PrintDocument_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            int count = 1;
            int x = 3;
            int y = 8;

            for (int i = 0; i < 4; i++)
            {
                YellowstonePathology.Business.BarcodeScanning.IScanable scannable = this.m_ScannableQueue.Dequeue();
                scannable.DrawLabel(x, y, e);
                x = x + 106;
                count += 1;
                if (this.m_ScannableQueue.Count == 0) break;
            }

            if (this.m_ScannableQueue.Count == 0)
            {
                e.HasMorePages = false;
            }
            else
            {
                e.HasMorePages = true;
            }
        }  
        */
    }
}
