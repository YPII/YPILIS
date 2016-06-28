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

			string alkResult = "ALK not performed";
			if (this.m_AccessionOrder.PanelSetOrderCollection.Exists(131) == true)
			{
                YellowstonePathology.Business.Test.ALKForNSCLCByFISH.ALKForNSCLCByFISHTestOrder alkFoNSCLCByFISHTestOrder = (YellowstonePathology.Business.Test.ALKForNSCLCByFISH.ALKForNSCLCByFISHTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(131);
				alkResult = alkFoNSCLCByFISHTestOrder.Result;
			}
			else if (this.m_AccessionOrder.PanelSetOrderCollection.Exists(68) == true)
			{
                YellowstonePathology.Business.Test.ALKForNSCLCByFISH.ALKForNSCLCByFISHTestOrder alkForNSCLCByFISHTestOrderReportedSeparately = new YellowstonePathology.Business.Test.ALKForNSCLCByFISH.ALKForNSCLCByFISHTestOrderReportedSeparately();
				alkResult = alkForNSCLCByFISHTestOrderReportedSeparately.Result;
			}
            else if(egfrToALKReflexAnalysisTestOrder.QNSForALK == true)
            {
                alkResult = "Quantity not sufficient to perform ALK";
            }            

			this.AddNextObxElement("ALK Rearrangement Analysis: " + alkResult, document, "F");
            this.AddNextObxElement("", document, "F");

            string ros1Result = null;
            if (this.m_AccessionOrder.PanelSetOrderCollection.Exists(204) == true)
            {
                YellowstonePathology.Business.Test.ROS1ByFISH.ROS1ByFISHTestOrder ros1ByFISHTestOrder = (YellowstonePathology.Business.Test.ROS1ByFISH.ROS1ByFISHTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(204);
                ros1Result = ros1ByFISHTestOrder.Result;
            }

            this.AddNextObxElement("ROS1 Rearrangement Analysis: " + ros1Result, document, "F");
            this.AddNextObxElement("", document, "F");

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
			this.HandleLongString(egfrToALKReflexAnalysisTestOrder.References, document, "F");
			this.AddNextObxElement("", document, "F");

            this.AddNextObxElement(egfrToALKReflexAnalysisTestOrder.GetLocationPerformedComment(), document, "F");
            this.AddNextObxElement(string.Empty, document, "F");
        }
	}
}
