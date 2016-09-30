using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Test.Surgical
{
	public class SurgicalCMMCNteView : YellowstonePathology.Business.HL7View.CMMC.CMMCNteView
	{
        protected YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
        protected string m_DateFormat = "yyyyMMddHHmm";
        protected string m_ReportNo;

		public SurgicalCMMCNteView(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo)
		{
            this.m_AccessionOrder = accessionOrder;
            this.m_ReportNo = reportNo;            
		}		

		public override void ToXml(XElement document)
		{
			SurgicalTestOrder panelSetOrderSurgical = (SurgicalTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_ReportNo);

			this.AddCompanyHeader(document);
			this.AddBlankNteElement(document);
			this.AddNextNteElement("Surgical Pathology Report: ", document);
			this.AddNextNteElement("Master Accession #: " + panelSetOrderSurgical.MasterAccessionNo, document);
			this.AddNextNteElement("Report #: " + panelSetOrderSurgical.ReportNo, document);

			this.AddBlankNteElement(document);

			foreach (SurgicalSpecimen surgicalSpecimen in panelSetOrderSurgical.SurgicalSpecimenCollection)
			{
				this.AddNextNteElement("Specimen: " + surgicalSpecimen.SpecimenOrder.SpecimenNumber.ToString(), document);
				this.HandleLongString(surgicalSpecimen.SpecimenOrder.Description, document);

				this.AddBlankNteElement(document);

				this.AddNextNteElement("Diagnosis: ", document);
				this.HandleLongString(surgicalSpecimen.Diagnosis, document);

				this.AddBlankNteElement(document);
			}

			if (string.IsNullOrEmpty(panelSetOrderSurgical.Comment) == false)
			{
				this.AddNextNteElement("Comment: ", document);
				this.HandleLongString(panelSetOrderSurgical.Comment, document);
				this.AddBlankNteElement(document);
			}

			if (string.IsNullOrEmpty(panelSetOrderSurgical.CancerSummary) == false)
			{
				this.AddNextNteElement("Cancer Summary: ", document);
				this.HandleLongString(panelSetOrderSurgical.CancerSummary, document);
				this.AddBlankNteElement(document);

				if (string.IsNullOrEmpty(panelSetOrderSurgical.AJCCStage) == false)
				{
					this.HandleLongString("Pathologic TNM Stage: " + panelSetOrderSurgical.AJCCStage, document);
					this.AddBlankNteElement(document);
				}
			}

			this.AddNextNteElement("Pathologist: " + panelSetOrderSurgical.Signature, document);
			this.AddNextNteElement("*** E-signed: " + panelSetOrderSurgical.FinalTime.Value.ToString("MM/dd/yyyy HH:mm") + " ***", document);

			this.AddBlankNteElement(document);

            foreach (YellowstonePathology.Business.Amendment.Model.Amendment amendment in panelSetOrderSurgical.AmendmentCollection)
			{
				this.AddNextNteElement(amendment.AmendmentType + ": " + amendment.AmendmentDate.Value.ToString("MM/dd/yyyy"), document);
				this.HandleLongString(amendment.Text, document);
				if (amendment.RequirePathologistSignature == true)
				{
					this.AddNextNteElement("Signature: " + amendment.PathologistSignature, document);
				}
				this.AddBlankNteElement(document);
			}

			this.AddNextNteElement("Microscopic Description: ", document);
			this.HandleLongString(panelSetOrderSurgical.MicroscopicX, document);

			this.AddBlankNteElement(document);

			if (panelSetOrderSurgical.TypingStainCollection.Count > 0)
			{
				this.AddNextNteElement("Ancillary Studies:", document);
				string ancillaryComment = panelSetOrderSurgical.GetAncillaryStudyComment();
				this.HandleLongString(ancillaryComment, document);

				foreach (YellowstonePathology.Business.Test.Surgical.SurgicalSpecimen surgicalSpecimen in panelSetOrderSurgical.SurgicalSpecimenCollection)
				{
					if (surgicalSpecimen.StainResultItemCollection.Count > 0)
					{
						this.HandleLongString(surgicalSpecimen.SpecimenOrder.SpecimenNumber.ToString() + ". " + surgicalSpecimen.SpecimenOrder.Description, document);

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

								this.HandleLongString("Test: " + stainResultItem.ProcedureName + "  Result: " + stainResult, document);
							}
						}
					}
				}
				this.AddBlankNteElement(document);
			}

			this.AddNextNteElement("Gross Description: ", document);
			this.HandleLongString(panelSetOrderSurgical.GrossX, document);
			this.AddBlankNteElement(document);

			this.AddNextNteElement("Clinical Information: ", document);
			this.HandleLongString(this.m_AccessionOrder.ClinicalHistory, document);
		}
	}
}
