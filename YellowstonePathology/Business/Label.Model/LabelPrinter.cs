using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Label.Model
{
    public class LabelPrinter
    {                
        protected System.Printing.PrintServer m_HostingPrintServer;
        protected System.Printing.PrintQueue m_PrintQueue;
        protected int m_PageHeight;
        protected int m_X;
        protected int m_Y;
        protected int m_ColumnCount;
        protected int m_ColumnWidth;

        private Queue<Label> m_Queue;

        public LabelPrinter(string printQueueName)
        {
            this.m_X = 0;
            this.m_Y = 0;
            this.m_Queue = new Queue<Label>();
            System.Printing.PrintServer localPrintServer = new System.Printing.PrintServer();
            System.Printing.PrintQueueCollection printQueueCollection = localPrintServer.GetPrintQueues(new[] { System.Printing.EnumeratedPrintQueueTypes.Local, System.Printing.EnumeratedPrintQueueTypes.Connections });
            foreach (System.Printing.PrintQueue printQueue in printQueueCollection)
            {
                if (printQueue.Name == printQueueName)
                {
                    this.m_PrintQueue = printQueue;
                    this.m_HostingPrintServer = printQueue.HostingPrintServer;
                }
            }
        }

        public Queue<Label> Queue
        {
            get { return this.m_Queue; }
        }        

        public virtual void Print()
        {            
            System.Drawing.Printing.PrintDocument printDocument = new System.Drawing.Printing.PrintDocument();
            printDocument.PrinterSettings.PrinterName = this.m_PrintQueue.FullName;
            printDocument.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(PrintDocument_PrintPage);
            printDocument.Print();
        }        

        private void PrintDocument_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {            
            for (int i = 0; i < this.m_ColumnCount; i++)
            {
                if (this.m_Queue.Count > 0)
                {
                    Label label = this.m_Queue.Dequeue();
                    label.DrawLabel(this.m_X, this.m_Y, e);
                    this.m_X += this.m_ColumnWidth;
                }
                else
                {
                    break;
                }
            }

            this.m_X = 0;
            if (this.m_Queue.Count > 0)
            {                
                e.HasMorePages = true;                
            }
            else
            {
                e.HasMorePages = false;
            }            
        }  
    }
}
