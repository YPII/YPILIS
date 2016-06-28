using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace YellowstonePathology.Business.HL7View
{
	public class SurgicalObxView : ObxView
	{                
		public SurgicalObxView(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo, int obxCount) 
            : base(accessionOrder, reportNo, obxCount)
		{
			
		}		

		public override void ToXml(XElement document)
		{
			YellowstonePathology.Business.Test.Surgical.SurgicalTestOrder panelSetOrderSurgical = (YellowstonePathology.Business.Test.Surgical.SurgicalTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_ReportNo);

			this.AddHeader(document, panelSetOrderSurgical, "Pathology Report");
			this.AddNextObxElement("Pathologist: " + panelSetOrderSurgical.Signature, document, "F");
            this.AddNextObxElement("", document, "F");

			foreach (YellowstonePathology.Business.Test.Surgical.SurgicalSpecimen surgicalSpecimen in panelSetOrderSurgical.SurgicalSpecimenCollection)
			{
                this.AddNextObxElement("Specimen: " + surgicalSpecimen.SpecimenOrder.SpecimenNumber.ToString(), document, "F");
                this.HandleLongString(surgicalSpecimen.SpecimenOrder.Description, document, "F");
                this.AddNextObxElement("", document, "F");

                this.AddNextObxElement("Diagnosis: ", document, "F");
                this.HandleLongString(surgicalSpecimen.Diagnosis, document, "F");
                this.AddNextObxElement("", document, "F");
			}

			if (string.IsNullOrEmpty(panelSetOrderSurgical.Comment) == false)
            {
                this.AddNextObxElement("Comment: ", document, "F");
				this.HandleLongString(panelSetOrderSurgical.Comment, document, "F");
                this.AddNextObxElement("", document, "F");
            }

            foreach (YellowstonePathology.Business.Amendment.Model.Amendment amendment in panelSetOrderSurgical.AmendmentCollection)
            {
                this.AddNextObxElement(amendment.AmendmentType + ": " + amendment.AmendmentDate.Value.ToString("MM/dd/yyyy"), document, "C");
                this.HandleLongString(amendment.Text, document, "C");
                if (amendment.RequirePathologistSignature == true)
                {
                    this.AddNextObxElement("Signature: " + amendment.PathologistSignature, document, "C");
                }
                this.AddNextObxElement("", document, "C");
            }

            this.AddNextObxElement("Microscopic Description: ", document, "F");
			this.HandleLongString(panelSetOrderSurgical.MicroscopicX, document, "F");
            this.AddNextObxElement(string.Empty, document, "F");

			if (panelSetOrderSurgical.TypingStainCollection.Count > 0)
			{
				this.AddNextObxElement("Ancillary Studies:", document, "F");
				string ancillaryComment = panelSetOrderSurgical.GetAncillaryStudyComment();
				this.HandleLongString(ancillaryComment, document, "F");

				foreach (YellowstonePathology.Business.Test.Surgical.SurgicalSpecimen surgicalSpecimen in panelSetOrderSurgical.SurgicalSpecimenCollection)
				{
					if (surgicalSpecimen.StainResultItemCollection.Count > 0)
					{
						this.HandleLongString(surgicalSpecimen.SpecimenOrder.SpecimenNumber.ToString() + ". " + surgicalSpecimen.SpecimenOrder.Description, document, "F");

						foreach (YellowstonePathology.Business.SpecialStain.StainResultItem stainResultItem in surgicalSpecimen.StainResultItemCollection)
						{
							if (stainResultItem.Reportable)
							{
								string stainResult = stainResultItem.Result;
                                if (string.IsNullOrEmpty(stainResult) == true)
                                {
                                    stainResult = "Pending";
                                }
								else if (stainResult.ToUpper() == "SEE COMMENT")
								{
									stainResult = stainResultItem.ReportComment;
								}
								else
								{
									string specialStainReportComment = stainResultItem.ReportComment;

									if (!string.IsNullOrEmpty(specialStainReportComment))
									{
										stainResult += " - " + specialStainReportComment;
									}
								}
								
								this.HandleLongString("Test: " + stainResultItem.ProcedureName + "  Result: " + stainResult, document, "F");
							}
						}
						this.AddNextObxElement(string.Empty, document, "F");
					}
				}
			}

			this.AddNextObxElement("Gross Description: ", document, "F");
			this.HandleLongString(panelSetOrderSurgical.GrossX, document, "F");
            this.AddNextObxElement("", document, "F");                        
		}        
	}
}
