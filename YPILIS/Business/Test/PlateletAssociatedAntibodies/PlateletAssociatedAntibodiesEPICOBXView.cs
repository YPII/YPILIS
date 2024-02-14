using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Test.PlateletAssociatedAntibodies
{
    public class PlateletAssociatedAntibodiesEPICOBXView : YellowstonePathology.Business.HL7View.EPIC.EPICObxView
    {
        YellowstonePathology.Business.Flow.FlowMarkerPanelList m_PanelList;
        public PlateletAssociatedAntibodiesEPICOBXView(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo, int obxCount)
            : base(accessionOrder, reportNo, obxCount)
        {
            this.m_PanelList = new YellowstonePathology.Business.Flow.FlowMarkerPanelList();
            this.m_PanelList.SetFillCommandByPanelId(8);
            this.m_PanelList.Fill();
        }

        public override void ToXml(XElement document)
        {
            Business.Test.LLP.PanelSetOrderLeukemiaLymphoma testOrder = (LLP.PanelSetOrderLeukemiaLymphoma)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_ReportNo);

            this.AddHeader(document, testOrder, "Platelet Associated Antibodies, Direct");

            this.AddNextObxElement("", document, "F");

            foreach (Flow.FlowMarkerItem marker in testOrder.FlowMarkerCollection)
            {
                this.AddNextObxElement("Test", document, "F");
                this.AddNextObxElement(marker.Name, document, "F");

                this.AddNextObxElement("Result", document, "F");
                this.AddNextObxElement(marker.Result, document, "F");

                foreach (YellowstonePathology.Business.Flow.FlowMarkerPanelListItem panelItem in this.m_PanelList)
                {
                    if (panelItem.MarkerName == marker.Name)
                    {
                        this.AddNextObxElement("Reference", document, "F");
                        this.AddNextObxElement(panelItem.Reference, document, "F");
                        break;
                    }
                }
            }            

            this.AddAmendments(document);

            this.AddNextObxElement("", document, "F");
            this.AddAmendments(document);

            this.AddNextObxElement("Specimen Information:", document, "F");
            YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrder(testOrder.OrderedOn, testOrder.OrderedOnId);
            this.AddNextObxElement("Specimen Description: " + specimenOrder.Description, document, "F");
            string collectionDateTimeString = Business.Helper.DateTimeExtensions.CombineDateAndTime(specimenOrder.CollectionDate, specimenOrder.CollectionTime);
            this.AddNextObxElement("Collection Date/Time: " + collectionDateTimeString, document, "F");

            this.AddNextObxElement("", document, "F");
            this.AddNextObxElement("Interpretation:", document, "F");
            string interpretation = "* Negative:  IgG and/or IgM values are not elevated.  There is no indication that immune mechanisms are involved in the thrombocytopenia.  Other etiologies should be considered." + Environment.NewLine +
                "* Weakly Positive: The moderately elevated IgG and / or IgM value suggests that immune mechanisms could be involved in the thrombocytopenia.  Other etiologies should also be considered." + Environment.NewLine +
                "* Positive: The elevated IgG and/ or IgM value suggests that immune mechanisms are involved in the thrombocytopenia." + Environment.NewLine +
                "* Strongly Positive: The IgG and / or IgM value is greatly elevated and indicates that immune mechanisms are involved in the thrombocytopenia.";
            this.HandleLongString(interpretation, document, "F");            
            this.AddNextObxElement("", document, "F");

            this.AddNextObxElement("Method:", document, "F");
            string method = "Qualitative Flow Cytometry";
            this.HandleLongString(method, document, "F");
            this.AddNextObxElement("", document, "F");

            string asrStatement = "Tests utilizing Analytic Specific Reagents (ASR's) were developed and performance characteristics determined by Yellowstone Pathology Institute, Inc.  They have not been cleared or approved by the U.S. Food and Drug Administration.  The FDA has determined that such clearance or approval is not necessary.  ASR's may be used for clinical purposes and should not be regarded as investigational or for research.  This laboratory is certified under the Clinical Laboratory Improvement Amendments of 1988 (CLIA-88) as qualified to perform high complexity clinical laboratory testing.";
            this.HandleLongString(asrStatement, document, "F");
            this.AddNextObxElement(string.Empty, document, "F");            

            string locationPerformed = testOrder.GetLocationPerformedComment();
            this.AddNextObxElement(locationPerformed, document, "F");
            this.AddNextObxElement(string.Empty, document, "F");
        }
    }
}
