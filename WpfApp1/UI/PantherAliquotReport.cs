using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI
{
    public partial class PantherAliquotReport 
    {
        private System.Drawing.Printing.PrintDocument m_PrintDocument;        
        private Queue<YellowstonePathology.Business.Test.PantherAliquotListItem> m_PantherAliquotListItemQueue;

        public PantherAliquotReport(YellowstonePathology.Business.Test.PantherAliquotList pantherAliquotList)
        {
            this.m_PantherAliquotListItemQueue = new Queue<Business.Test.PantherAliquotListItem>();
            foreach(YellowstonePathology.Business.Test.PantherAliquotListItem pantherAliquotListItem in pantherAliquotList)
            {
                this.m_PantherAliquotListItemQueue.Enqueue(pantherAliquotListItem);
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
                YellowstonePathology.Business.Test.PantherAliquotListItem pantherAliquotListItem = this.m_PantherAliquotListItemQueue.Dequeue();
                this.DrawLine(x, y, pantherAliquotListItem, e);
                y = y + 30;
                count += 1;
                if (this.m_PantherAliquotListItemQueue.Count == 0) break;
            }

            if (this.m_PantherAliquotListItemQueue.Count == 0)
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
            e.Graphics.DrawString("Panther Orders Not Aliquoted", new System.Drawing.Font("Verdana", 12, System.Drawing.FontStyle.Bold), System.Drawing.Brushes.Black, new System.Drawing.PointF(x, y));            
        }

        private void DrawLine(int x, int y, YellowstonePathology.Business.Test.PantherAliquotListItem pantherAliquotListItem, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawString(pantherAliquotListItem.MasterAccessionNo, new System.Drawing.Font("Verdana", 9), System.Drawing.Brushes.Black, new System.Drawing.PointF(x, y));
            e.Graphics.DrawString(pantherAliquotListItem.AccessionTime.ToString("MM/dd/yyy HH:mm"), new System.Drawing.Font("Verdana", 9), System.Drawing.Brushes.Black, new System.Drawing.PointF(x + 150, y));
            e.Graphics.DrawString(pantherAliquotListItem.PLastName, new System.Drawing.Font("Verdana", 9), System.Drawing.Brushes.Black, new System.Drawing.PointF(x + 350, y));
            e.Graphics.DrawString(pantherAliquotListItem.PFirstName, new System.Drawing.Font("Verdana", 9), System.Drawing.Brushes.Black, new System.Drawing.PointF(x + 500, y));
        }
    }
}
