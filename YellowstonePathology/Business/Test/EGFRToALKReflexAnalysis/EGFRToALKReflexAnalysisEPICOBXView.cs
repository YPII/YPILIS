using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Test.EGFRToALKReflexAnalysis
{
	public class EGFRToALKReflexAnalysisEPICOBXView : YellowstonePathology.Business.HL7View.EPIC.EPICObxView
    {
        public EGFRToALKReflexAnalysisEPICOBXView(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo, int obxCount) 
            : base(accessionOrder, reportNo, obxCount)
		{
			
		}

        public override void ToXml(XElement document)
        {
            YellowstonePathology.Business.Test.EGFRToALKReflexAnalysis.EGFRToALKReflexAnalysisTestOrder egfrToALKReflexAnalysisTestOrder = (YellowstonePathology.Business.Test.EGFRToALKReflexAnalysis.EGFRToALKReflexAnalysisTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_ReportNo);

            YellowstonePathology.Business.Test.EGFRMutationAnalysis.EGFRMutationAnalysisTestOrder egfrMutationAnalysisTestOrder = (YellowstonePathology.Business.Test.EGFRMutationAnalysis.EGFRMutationAnalysisTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(60);

			this.AddHeader(document, egfrToALKReflexAnalysisTestOrder, egfrToALKReflexAnalysisTestOrder.PanelSetName);
            this.AddNextObxElement("", document, "F");

            this.AddNextObxElement("EGFR Mutation Analysis: " + egfrMutationAnalysisTestOrder.Result, document, "F");
            this.AddNextObxElement("", document, "F");

            this.AddNextObxElement("Comment: ", document, "F");
            this.HandleLongString(egfrMutationAnalysisTestOrder.Comment, document, "F");
            this.AddNextObxElement("", document, "F");

            if(String.IsNullOrEmpty(egfrToALKReflexAnalysisTestOrder.ALKForNSCLCByFISHResult) == false)
            {
			    this.AddNextObxElement("ALK Rearrangement Analysis: " + egfrToALKReflexAnalysisTestOrder.ALKForNSCLCByFISHResult, document, "F");
                this.AddNextObxElement("", document, "F");
            }

            if (String.IsNullOrEmpty(egfrToALKReflexAnalysisTestOrder.ROS1ByFISHResult) == false)
            {
                this.AddNextObxElement("ROS1 Rearrangement Analysis: " + egfrToALKReflexAnalysisTestOrder.ROS1ByFISHResult, document, "F");
                this.AddNextObxElement("", document, "F");
            }

            if (string.IsNullOrEmpty(egfrToALKReflexAnalysisTestOrder.PDL1SP142StainPercent) == false)
            {
                this.AddNextObxElement("PD-L1 (SP142): " + egfrToALKReflexAnalysisTestOrder.PDL1SP142StainPercent, document, "F");
                this.AddNextObxElement("", document, "F");
            }

            if (String.IsNullOrEmpty(egfrToALKReflexAnalysisTestOrder.PDL122C3Result) == false)
            {
                this.AddNextObxElement("PD-L1 (22C3): " + egfrToALKReflexAnalysisTestOrder.PDL122C3Result, document, "F");
                this.AddNextObxElement("", document, "F");
            }

            if (String.IsNullOrEmpty(egfrToALKReflexAnalysisTestOrder.BRAFMutationAnalysisResult) == false)
            {
                this.AddNextObxElement("BRAF Mutation Analysis: " + egfrToALKReflexAnalysisTestOrder.BRAFMutationAnalysisResult, document, "F");
                this.AddNextObxElement("", document, "F");
            }



            this.AddNextObxElement("Pathologist: " + egfrToALKReflexAnalysisTestOrder.Signature, document, "F");
            if (egfrToALKReflexAnalysisTestOrder.FinalTime.HasValue == true)
            {
                this.AddNextObxElement("E-signed " + egfrToALKReflexAnalysisTestOrder.FinalTime.Value.ToString("MM/dd/yyyy HH:mm"), document, "F");
            }
            this.AddNextObxElement("", document, "F");
            this.AddAmendments(document);

            this.AddNextObxElement("Interpretation: ", document, "F");
			this.HandleLongString(egfrToALKReflexAnalysisTestOrder.Interpretation, document, "F");
            this.AddNextObxElement("", document, "F");

			this.AddNextObxElement("Method: ", document, "F");
			this.HandleLongString(egfrToALKReflexAnalysisTestOrder.Method, document, "F");
			this.AddNextObxElement("", document, "F");

			this.AddNextObxElement("References: ", document, "F");
			this.HandleLongString(egfrToALKReflexAnalysisTestOrder.ReportReferences, document, "F");
			this.AddNextObxElement("", document, "F");

            this.AddNextObxElement(egfrToALKReflexAnalysisTestOrder.GetLocationPerformedComment(), document, "F");
            this.AddNextObxElement(string.Empty, document, "F");
        }
	}
}
