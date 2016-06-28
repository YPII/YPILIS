using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Label.Model
{
    public class SlideLabelPrinter
    {
        private System.Drawing.Printing.PrintDocument m_PrintDocument;
        private YellowstonePathology.Business.Test.AliquotOrderCollection m_AliquotOrderCollection;
        private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
        private Queue<SlideLabel> m_SlideLabelQueue;

        public SlideLabelPrinter(YellowstonePathology.Business.Test.AliquotOrderCollection aliquotOrderCollection, YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {
            this.m_AliquotOrderCollection = aliquotOrderCollection;
            this.m_AccessionOrder = accessionOrder;
            this.Initialize();
        }

        private void Initialize()
        {
            this.m_SlideLabelQueue = new Queue<SlideLabel>();            
            foreach (YellowstonePathology.Business.Test.AliquotOrder aliquotOrder in this.m_AliquotOrderCollection)
            {
                if (aliquotOrder.IsSlide() == true)
                {
                    if (aliquotOrder.LabelType == YellowstonePathology.Business.Specimen.Model.AliquotLabelType.PaperLabel == true)
                    {
                        SlideLabel slideLabel = new SlideLabel(aliquotOrder, this.m_AccessionOrder);                        
                        this.m_SlideLabelQueue.Enqueue(slideLabel);
                    }
                }
            }            
        }

        public bool HasItemsToPrint()
        {
            bool result = false;
            if (this.m_SlideLabelQueue.Count > 0) result = true;
            return result;
        }        

        public void Print()
        {
            this.m_PrintDocument = new System.Drawing.Printing.PrintDocument();
            System.Printing.PrintServer printServer = new System.Printing.LocalPrintServer();
            System.Printing.PrintQueue printQueue = printServer.GetPrintQueue(YellowstonePathology.Business.User.UserPreferenceInstance.Instance.UserPreference.HistologySlideLabelPrinter);
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
                SlideLabel slideLabel = this.m_SlideLabelQueue.Dequeue();
                slideLabel.DrawLabel(x, y, e);
                slideLabel.AliquotOrder.Printed = true;
                x = x + 106;
                count += 1;
                if (this.m_SlideLabelQueue.Count == 0) break;
            }

            if (this.m_SlideLabelQueue.Count == 0)
            {
                e.HasMorePages = false;
            }
            else
            {
                e.HasMorePages = true;
            }
        }  
    }
}
