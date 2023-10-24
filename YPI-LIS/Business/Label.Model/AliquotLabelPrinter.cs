using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Label.Model
{
    public class AliquotLabelPrinter
    {        
        private Queue<Business.Label.Model.AliquotPaperZPLLabel> m_AliquotLabelQueue;        

        public AliquotLabelPrinter(YellowstonePathology.Business.Test.AliquotOrderCollection aliquotOrderCollection, YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {            
            this.m_AliquotLabelQueue = new Queue<Business.Label.Model.AliquotPaperZPLLabel>();
            foreach (YellowstonePathology.Business.Test.AliquotOrder aliquotOrder in aliquotOrderCollection)
            {
                if (aliquotOrder.IsWash() == true)
                {
                    if (aliquotOrder.LabelType == Business.Specimen.Model.AliquotLabelType.PaperLabel == true)
                    {
                        YellowstonePathology.Business.OrderIdParser orderIdParser = new OrderIdParser(accessionOrder.MasterAccessionNo);
                        if (orderIdParser.IsLegacyMasterAccessionNo == false)
                        {
                            string initials = Business.Helper.PatientHelper.GetPatientInitials(accessionOrder.PFirstName, accessionOrder.PLastName);
                            Business.Label.Model.AliquotPaperZPLLabel aliquotLabel = new Business.Label.Model.AliquotPaperZPLLabel(aliquotOrder.AliquotOrderId, accessionOrder.PLastName, accessionOrder.PFirstName, aliquotOrder.Label, accessionOrder.MasterAccessionNo, accessionOrder.AccessionDate.Value); 
                            this.m_AliquotLabelQueue.Enqueue(aliquotLabel);
                            aliquotOrder.Printed = true;
                        }
                        else
                        {                            
                            string initials = Business.Helper.PatientHelper.GetPatientInitials(accessionOrder.PFirstName, accessionOrder.PLastName);
                            Business.Label.Model.AliquotPaperZPLLabel aliquotLabel = new Business.Label.Model.AliquotPaperZPLLabel(aliquotOrder.AliquotOrderId, accessionOrder.PFirstName, accessionOrder.PLastName, aliquotOrder.Label, accessionOrder.MasterAccessionNo, accessionOrder.AccessionDate.Value);
                            this.m_AliquotLabelQueue.Enqueue(aliquotLabel);
                            aliquotOrder.Printed = true;
                        }
                    }
                }
            }       
        }

        public AliquotLabelPrinter(string aliquotOrderId, string aliquotLabel, string masterAccessionNo, string pLastName, string pFirstName, DateTime accessionDate)
        {
            this.m_AliquotLabelQueue = new Queue<Business.Label.Model.AliquotPaperZPLLabel>();
            Business.Label.Model.AliquotPaperZPLLabel blockLabel = new Business.Label.Model.AliquotPaperZPLLabel(aliquotOrderId, pLastName, pFirstName, aliquotLabel, masterAccessionNo, accessionDate);
            this.m_AliquotLabelQueue.Enqueue(blockLabel);
        }        

        public bool HasItemsToPrint()
        {
            bool result = false;
            if (this.m_AliquotLabelQueue.Count > 0) result = true;
            return result;
        }        

        public void Print()
        {            
            while (this.m_AliquotLabelQueue.Count != 0)
            {                
                Business.Label.Model.AliquotPaperZPLLabel label = this.m_AliquotLabelQueue.Dequeue();
                this.PrintRow(label);
            }
        }

        private void PrintRow(Business.Label.Model.AliquotPaperZPLLabel label)
        {
            StringBuilder result = new StringBuilder();
            int xOffset = 0;
            
            result.Append("^XA");
            for (int i = 0; i < 4; i++)
            {                                  
                label.AppendCommands(result, xOffset);
                xOffset += 325;                
            }

            result.Append("^XZ");
            
            Business.Label.Model.ZPLPrinterTCP printer = new ZPLPrinterTCP("10.1.1.21");
            printer.Print(result.ToString());
        }        
    }
}
