using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Test.CysticFibrosis
{
	public class CysticFibrosisEPICObxView : YellowstonePathology.Business.HL7View.EPIC.EPICObxView
    {
		public CysticFibrosisEPICObxView(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo, int obxCount) 
            : base(accessionOrder, reportNo, obxCount)
		{
			
		}

        public override void ToXml(XElement document)
        {
			CysticFibrosisTestOrder panelSetOrder = (CysticFibrosisTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_ReportNo);

			this.AddHeader(document, panelSetOrder, "Cystic Fibrosis Carrier Screening");
            
			this.AddNextObxElement("", document, "F");
            this.AddNextObxElement("Result:", document, "F");
			this.AddNextObxElement(panelSetOrder.Result, document, "F");

			this.AddNextObxElement("", document, "F");
			this.AddNextObxElement("Mutations Detected:", document, "F");
			this.HandleLongString(panelSetOrder.MutationsDetected, document, "F");

			if (string.IsNullOrEmpty(panelSetOrder.Comment) == false)
			{
				this.AddNextObxElement("", document, "F");
				this.AddNextObxElement("Comment:", document, "F");
				this.HandleLongString(panelSetOrder.Comment, document, "F");
			}

            this.AddNextObxElement("", document, "F");
            this.AddNextObxElement("Pathologist: " + panelSetOrder.Signature, document, "F");
            if (panelSetOrder.FinalTime.HasValue == true)
            {
                this.AddNextObxElement("E-signed " + panelSetOrder.FinalTime.Value.ToString("MM/dd/yyyy HH:mm"), document, "F");
            }            

			this.AddNextObxElement("", document, "F");
            this.AddAmendments(document);

            this.AddNextObxElement("Specimen: Whole blood EDTA", document, "F");

            YellowstonePathology.Business.Test.CysticFibrosis.CysticFibrosisEthnicGroupCollection cysticFibrosisEthnicGroupCollection = new CysticFibrosisEthnicGroupCollection();
            YellowstonePathology.Business.Test.CysticFibrosis.CysticFibrosisEthnicGroup cysticFibrosisEthnicGroup = cysticFibrosisEthnicGroupCollection.GetCysticFibrosisEthnicGroup(panelSetOrder.EthnicGroupId);

			this.AddNextObxElement("", document, "F");
            this.AddNextObxElement("Ethnic Group: " + cysticFibrosisEthnicGroup.EthnicGroupName, document, "F");

            this.AddNextObxElement("", document, "F");
            this.AddNextObxElement("Interpretation:", document, "F");
			this.HandleLongString(panelSetOrder.Interpretation, document, "F");

			if (panelSetOrder.TemplateId == 2)
			{
				this.AddNextObxElement("", document, "F");
				string line = "The table below provides data to be used in the risk assessment of this individual.  Note that the detection rate and the CF carrier risk are not available for individuals from other ethnic populations.";
				this.AddNextObxElement("", document, "F");
				this.AddNextObxElement(line, document, "F");
				this.AddNextObxElement("Ethnic Group ", document, "F");
				this.AddNextObxElement("Ashkenazi Jewish", document, "F");
				this.AddNextObxElement("Detection Rate 94%", document, "F");
				this.AddNextObxElement("Before Test 1/24", document, "F");
				this.AddNextObxElement("After Negative Test 1/400", document, "F");
				this.AddNextObxElement("", document, "F");
				this.AddNextObxElement("European Caucasian", document, "F");
				this.AddNextObxElement("Detection Rate 88%", document, "F");
				this.AddNextObxElement("Before Test 1/25", document, "F");
				this.AddNextObxElement("After Negative Test 1/208", document, "F");
				this.AddNextObxElement("", document, "F");
				this.AddNextObxElement("African American", document, "F");
				this.AddNextObxElement("Detection Rate 65%", document, "F");
				this.AddNextObxElement("Before Test 1/65", document, "F");
				this.AddNextObxElement("After Negative Test 1/186", document, "F");
				this.AddNextObxElement("", document, "F");
				this.AddNextObxElement("Hispanic American", document, "F");
				this.AddNextObxElement("Detection Rate 72%", document, "F");
				this.AddNextObxElement("Before Test 1/46", document, "F");
				this.AddNextObxElement("After Negative Test 1/164", document, "F");
				this.AddNextObxElement("", document, "F");
				this.AddNextObxElement("Asian American ", document, "F");
				this.AddNextObxElement("Detection Rate 49%", document, "F");
				this.AddNextObxElement("Before Test 1/94", document, "F");
				this.AddNextObxElement("After Negative Test 1/184", document, "F");
			}

			this.AddNextObxElement("", document, "F");
			this.AddNextObxElement("Method:", document, "F");
			this.HandleLongString(panelSetOrder.Method, document, "F");

            this.AddNextObxElement("", document, "F");
            this.AddNextObxElement("Mutations Tested:", document, "F");
			this.HandleLongString(panelSetOrder.MutationsTested, document, "F");

			this.AddNextObxElement("", document, "F");
			this.AddNextObxElement("References:", document, "F");
			this.HandleLongString(panelSetOrder.References, document, "F");

            string locationPerformed = panelSetOrder.GetLocationPerformedComment();
			this.AddNextObxElement("", document, "F");
			this.AddNextObxElement(locationPerformed, document, "F");
            this.AddNextObxElement(string.Empty, document, "F");
		}
    }
}
