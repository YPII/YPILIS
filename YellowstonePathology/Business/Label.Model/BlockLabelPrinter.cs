using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Label.Model
{
    public class BlockLabelPrinter
    {
        private System.Drawing.Printing.PrintDocument m_PrintDocument;        
        private Queue<BlockLabel> m_BlockLabelQueue;        

        public BlockLabelPrinter(YellowstonePathology.Business.Test.AliquotOrderCollection aliquotOrderCollection, YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {            
            this.m_BlockLabelQueue = new Queue<BlockLabel>();
            foreach (YellowstonePathology.Business.Test.AliquotOrder aliquotOrder in aliquotOrderCollection)
            {
                if (aliquotOrder.IsBlock() == true)
                {
                    if (aliquotOrder.LabelType == YellowstonePathology.Business.Specimen.Model.AliquotLabelType.PaperLabel == true)
                    {
                        YellowstonePathology.Business.OrderIdParser orderIdParser = new OrderIdParser(accessionOrder.MasterAccessionNo);
                        if (orderIdParser.IsLegacyMasterAccessionNo == false)
                        {
                            BlockLabel blockLabel = new BlockLabel();
                            blockLabel.FromAliquotOrder(aliquotOrder.AliquotOrderId, aliquotOrder.Label, accessionOrder.MasterAccessionNo, accessionOrder.PLastName, accessionOrder.PFirstName);
                            this.m_BlockLabelQueue.Enqueue(blockLabel);
                            aliquotOrder.Printed = true;
                        }
                        else
                        {
                            string reportNo = accessionOrder.PanelSetOrderCollection[0].ReportNo;
                            BlockLabelLegacy blockLabel = new BlockLabelLegacy();
                            blockLabel.FromLegacyAliquotOrder(aliquotOrder.AliquotOrderId, aliquotOrder.Label, reportNo, accessionOrder.PLastName, accessionOrder.PFirstName);
                            this.m_BlockLabelQueue.Enqueue(blockLabel);
                            aliquotOrder.Printed = true;
                        }
                    }
                }
            }       
        }

        public BlockLabelPrinter(string aliquotOrderId, string aliquotLabel, string masterAccessionNo, string pLastName, string pFirstName)
        {
            this.m_BlockLabelQueue = new Queue<BlockLabel>();
            BlockLabel blockLabel = new BlockLabel();
            blockLabel.FromAliquotOrder(aliquotOrderId, aliquotLabel, masterAccessionNo, pLastName, pFirstName);
            this.m_BlockLabelQueue.Enqueue(blockLabel);        
        }        

        public bool HasItemsToPrint()
        {
            bool result = false;
            if (this.m_BlockLabelQueue.Count > 0) result = true;
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
                BlockLabel blockLabel = this.m_BlockLabelQueue.Dequeue();
                blockLabel.DrawLabel(x, y, e);                
                x = x + 106;
                count += 1;
                if (this.m_BlockLabelQueue.Count == 0) break;
            }

            if (this.m_BlockLabelQueue.Count == 0)
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
