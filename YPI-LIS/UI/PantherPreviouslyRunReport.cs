using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI
{
    public partial class PantherPreviouslyRunReport 
    {
        private System.Drawing.Printing.PrintDocument m_PrintDocument;        
        private Queue<YellowstonePathology.Business.Test.PantherOrderListItem> m_PantherOrderListItemQueue;

        public PantherPreviouslyRunReport(YellowstonePathology.Business.Test.PantherOrderList pantherOrderList)
        {
            this.m_PantherOrderListItemQueue = new Queue<YellowstonePathology.Business.Test.PantherOrderListItem>();
            foreach(YellowstonePathology.Business.Test.PantherOrderListItem pantherOrderListItem in pantherOrderList)
            {
                this.m_PantherOrderListItemQueue.Enqueue(pantherOrderListItem);
            }
        }

        public void Print()
        {
            this.m_PrintDocument = new System.Drawing.Printing.PrintDocument();
            this.m_PrintDocument.PrinterSettings = new System.Drawing.Printing.PrinterSettings();
            this.m_PrintDocument.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(PrintDocument_PrintPage);
            this.m_PrintDocument.Print();
        }

        private void PrintDocument_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            int count = 1;
            int x = 50;
            int y = 50;

            this.DrawHeader(x, y, e);
            y = y + 50;

            for (int i = 0; i < 30; i++)
            {
                YellowstonePathology.Business.Test.PantherOrderListItem pantherOrderListItem = this.m_PantherOrderListItemQueue.Dequeue();
                this.DrawLine(x, y, pantherOrderListItem, e);
                y = y + 30;
                count += 1;
                if (this.m_PantherOrderListItemQueue.Count == 0) break;
            }

            if (this.m_PantherOrderListItemQueue.Count == 0)
            {
                e.HasMorePages = false;
            }
            else
            {
                e.HasMorePages = true;
            }
        }

        private void DrawHeader(int x, int y, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawString("Panther Aliquots Previously Run", new System.Drawing.Font("Verdana", 12, System.Drawing.FontStyle.Bold), System.Drawing.Brushes.Black, new System.Drawing.PointF(x, y));            
        }

        private void DrawLine(int x, int y, YellowstonePathology.Business.Test.PantherOrderListItem pantherOrderListItem, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawString(pantherOrderListItem.MasterAccessionNo, new System.Drawing.Font("Verdana", 9), System.Drawing.Brushes.Black, new System.Drawing.PointF(x, y));
            e.Graphics.DrawString(pantherOrderListItem.OrderTime.ToString("MM/dd/yyy HH:mm"), new System.Drawing.Font("Verdana", 9), System.Drawing.Brushes.Black, new System.Drawing.PointF(x + 150, y));
            e.Graphics.DrawString(pantherOrderListItem.PLastName, new System.Drawing.Font("Verdana", 9), System.Drawing.Brushes.Black, new System.Drawing.PointF(x + 350, y));
            e.Graphics.DrawString(pantherOrderListItem.PFirstName, new System.Drawing.Font("Verdana", 9), System.Drawing.Brushes.Black, new System.Drawing.PointF(x + 500, y));
        }
    }
}
