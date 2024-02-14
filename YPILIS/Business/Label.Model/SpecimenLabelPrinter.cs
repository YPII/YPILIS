using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Label.Model
{
    public class SpecimenLabelPrinter
    {
        private System.Drawing.Printing.PrintDocument m_PrintDocument;        
        private Queue<SpecimenLabel> m_SpecimenLabelQueue;        

        public SpecimenLabelPrinter(YellowstonePathology.Business.Test.AliquotOrderCollection aliquotOrderCollection, YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {            
            this.m_SpecimenLabelQueue = new Queue<SpecimenLabel>();
            foreach (YellowstonePathology.Business.Test.AliquotOrder aliquotOrder in aliquotOrderCollection)
            {
                if (aliquotOrder.IsSpecimen() == true)
                {                                        
                    SpecimenLabel specimenLabel = new SpecimenLabel();
                    specimenLabel.FromAliquotOrder(aliquotOrder.AliquotOrderId, aliquotOrder.Label, accessionOrder.MasterAccessionNo, accessionOrder.PLastName, accessionOrder.PFirstName);
                    this.m_SpecimenLabelQueue.Enqueue(specimenLabel);
                    aliquotOrder.Printed = true;                    
                }
            }       
        }

        public SpecimenLabelPrinter(string aliquotOrderId, string aliquotLabel, string masterAccessionNo, string pLastName, string pFirstName)
        {
            this.m_SpecimenLabelQueue = new Queue<SpecimenLabel>();
            SpecimenLabel specimenLabel = new SpecimenLabel();
            specimenLabel.FromAliquotOrder(aliquotOrderId, aliquotLabel, masterAccessionNo, pLastName, pFirstName);
            this.m_SpecimenLabelQueue.Enqueue(specimenLabel);        
        }        

        public bool HasItemsToPrint()
        {
            bool result = false;
            if (this.m_SpecimenLabelQueue.Count > 0) result = true;
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
                SpecimenLabel specimenLabel = this.m_SpecimenLabelQueue.Dequeue();
                specimenLabel.DrawLabel(x, y, e);                
                x = x + 106;
                count += 1;
                if (this.m_SpecimenLabelQueue.Count == 0) break;
            }

            if (this.m_SpecimenLabelQueue.Count == 0)
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
