using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Test.EGFRToALKReflexAnalysis
{
	public class EGFRToALKReflexAnalysisEPICNTEView : YellowstonePathology.Business.HL7View.EPIC.EPICBeakerObxView
    {
        public EGFRToALKReflexAnalysisEPICNTEView(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo, int obxCount) 
            : base(accessionOrder, reportNo, obxCount)
		{
			
		}

        public override void ToXml(XElement document)
        {
            YellowstonePathology.Business.Test.EGFRToALKReflexAnalysis.EGFRToALKReflexAnalysisTestOrder egfrToALKReflexAnalysisTestOrder = (YellowstonePathology.Business.Test.EGFRToALKReflexAnalysis.EGFRToALKReflexAnalysisTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_ReportNo);
            YellowstonePathology.Business.Test.EGFRMutationAnalysis.EGFRMutationAnalysisTestOrder egfrMutationAnalysisTestOrder = (YellowstonePathology.Business.Test.EGFRMutationAnalysis.EGFRMutationAnalysisTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(60);

            if(egfrMutationAnalysisTestOrder != null)
            {
                //Add the first element as narrative for Nikki to see.
                Business.HL7View.EPIC.EPICBeakerNarrativeOBXView.AddElement(document);

                this.AddCompanyHeaderNTE(document);
                this.AddNextNTEElement(egfrMutationAnalysisTestOrder.PanelSetName, document);
                this.AddNextNTEElement("Report No: " + egfrMutationAnalysisTestOrder.ReportNo, document);
                this.AddNextNTEElement("", document);

                this.AddNextNTEElement("EGFR Mutation Analysis: " + egfrMutationAnalysisTestOrder.Result, document);
                this.AddNextNTEElement("", document);

                this.AddNextNTEElement("Comment: ", document);
                this.AddNextNTEElement(egfrMutationAnalysisTestOrder.Comment, document);
                this.AddNextNTEElement("", document);
            }			

            if(String.IsNullOrEmpty(egfrToALKReflexAnalysisTestOrder.ALKForNSCLCByFISHResult) == false)
            {
			    this.AddNextNTEElement("ALK Rearrangement Analysis: " + egfrToALKReflexAnalysisTestOrder.ALKForNSCLCByFISHResult, document);
                this.AddNextNTEElement("", document);
            }

            if (String.IsNullOrEmpty(egfrToALKReflexAnalysisTestOrder.ROS1ByFISHResult) == false)
            {
                this.AddNextNTEElement("ROS1 Rearrangement Analysis: " + egfrToALKReflexAnalysisTestOrder.ROS1ByFISHResult, document);
                this.AddNextNTEElement("", document);
            }

            if (string.IsNullOrEmpty(egfrToALKReflexAnalysisTestOrder.PDL1SP142StainPercent) == false)
            {
                this.AddNextNTEElement("PD-L1 (SP142): " + egfrToALKReflexAnalysisTestOrder.PDL1SP142StainPercent, document);
                this.AddNextNTEElement("", document);
            }

            if (String.IsNullOrEmpty(egfrToALKReflexAnalysisTestOrder.PDL122C3Result) == false)
            {
                this.AddNextNTEElement("PD-L1 (22C3): " + egfrToALKReflexAnalysisTestOrder.PDL122C3Result, document);
                this.AddNextNTEElement("", document);
            }

            if (String.IsNullOrEmpty(egfrToALKReflexAnalysisTestOrder.BRAFMutationAnalysisResult) == false)
            {
                this.AddNextNTEElement("BRAF Mutation Analysis: " + egfrToALKReflexAnalysisTestOrder.BRAFMutationAnalysisResult, document);
                this.AddNextNTEElement("", document);
            }

            if(string.IsNullOrEmpty(egfrToALKReflexAnalysisTestOrder.Comment) == false)
            {
                this.AddNextNTEElement("QNS: " + EGFRToALKReflexAnalysisTestOrder.QNSStatement, document);
                this.AddNextNTEElement("", document);
            }

            this.AddNextNTEElement("Pathologist: " + egfrToALKReflexAnalysisTestOrder.Signature, document);
            if (egfrToALKReflexAnalysisTestOrder.FinalTime.HasValue == true)
            {
                this.AddNextNTEElement("E-signed " + egfrToALKReflexAnalysisTestOrder.FinalTime.Value.ToString("MM/dd/yyyy HH:mm"), document);
            }
            this.AddNextNTEElement("", document);
            this.AddAmendments(document);

            this.AddNextNTEElement("Interpretation: ", document);
			this.AddNextNTEElement(egfrToALKReflexAnalysisTestOrder.Interpretation, document);
            this.AddNextNTEElement("", document);

			this.AddNextNTEElement("Method: ", document);
			this.AddNextNTEElement(egfrToALKReflexAnalysisTestOrder.Method, document);
			this.AddNextNTEElement("", document);

			this.AddNextNTEElement("References: ", document);
			this.AddNextNTEElement(egfrToALKReflexAnalysisTestOrder.ReportReferences, document);
			this.AddNextNTEElement("", document);

            this.AddNextNTEElement(egfrToALKReflexAnalysisTestOrder.GetLocationPerformedComment(), document);
            this.AddNextNTEElement(string.Empty, document);
        }
	}
}
