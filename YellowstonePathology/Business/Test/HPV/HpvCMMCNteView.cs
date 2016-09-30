using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Test.HPV
{
	public class HPVCMMCNteView : YellowstonePathology.Business.HL7View.CMMC.CMMCNteView
    {
        protected YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
        protected string m_DateFormat = "yyyyMMddHHmm";
        protected string m_ReportNo;

        public HPVCMMCNteView(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo)
		{
            this.m_AccessionOrder = accessionOrder;
            this.m_ReportNo = reportNo;            
		}		

		public override void ToXml(XElement document)
		{			
            YellowstonePathology.Business.Test.HPV.HPVTestOrder panelSetOrder = (YellowstonePathology.Business.Test.HPV.HPVTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_ReportNo);

            this.AddCompanyHeader(document);
            this.AddBlankNteElement(document);

            this.AddNextNteElement("High Risk HPV Report", document);            
            this.AddNextNteElement("Master Accession #: " + panelSetOrder.MasterAccessionNo, document);
            this.AddNextNteElement("Report #: " + panelSetOrder.ReportNo, document);
            this.AddBlankNteElement(document);
            
            string resultText = "Result: " + panelSetOrder.Result;
            this.AddNextNteElement(resultText, document);
            this.AddNextNteElement("Reference: Negative", document);
            this.AddBlankNteElement(document);            

            this.AddNextNteElement("Specimen: ThinPrep fluid", document);
            this.AddBlankNteElement(document);

            bool hpvHasBeenOrdered = this.m_AccessionOrder.PanelSetOrderCollection.Exists(62);

            string additionalTestingComment = string.Empty;
            if (hpvHasBeenOrdered == true)
            {
                additionalTestingComment = YellowstonePathology.Business.Test.ThinPrepPap.ThinPrepPapWordDocument.HPV1618HasBeenOrderedComment;
            }
            else
            {
                additionalTestingComment = YellowstonePathology.Business.Test.ThinPrepPap.ThinPrepPapWordDocument.NoAdditionalTestingOrderedComment;
            }

            this.AddNextNteElement("Additional Testing:", document);
            this.AddNextNteElement(additionalTestingComment, document);
            this.AddBlankNteElement(document);

            this.AddNextNteElement("Test Information: ", document);
            this.HandleLongString(YellowstonePathology.Business.Test.HPV.HPVResult.TestInformation, document);
            this.AddBlankNteElement(document);
            
            this.AddNextNteElement("References: ", document);
            this.HandleLongString(YellowstonePathology.Business.Test.HPV.HPVResult.References, document);
            this.AddBlankNteElement(document);
            
            this.HandleLongString(panelSetOrder.ASRComment, document);            
		}        
	}
}
