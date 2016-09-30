using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Test.WomensHealthProfile
{
	public class WomensHealthProfileCMMCNteView : YellowstonePathology.Business.HL7View.CMMC.CMMCNteView
	{
		protected YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
		protected string m_DateFormat = "yyyyMMddHHmm";
		protected string m_ReportNo;

		public WomensHealthProfileCMMCNteView(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo)
		{
			this.m_AccessionOrder = accessionOrder;
			this.m_ReportNo = reportNo;
		}

		public override void ToXml(XElement document)
		{
			WomensHealthProfileTestOrder womensHealthProfileTestOrder = (WomensHealthProfileTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_ReportNo);
			WomensHealthProfileResult womensHealthProfileResult = new WomensHealthProfileResult(this.m_AccessionOrder);

			this.AddCompanyHeader(document);
			this.AddBlankNteElement(document);

			this.AddNextNteElement("Women's Health Profile", document);
			this.AddNextNteElement("Report #: " + womensHealthProfileTestOrder.ReportNo, document);
			this.AddBlankNteElement(document);

			this.AddNextNteElement("PAP TEST RESULT: ", document);

			YellowstonePathology.Business.Test.ThinPrepPap.PanelSetOrderCytology panelSetOrderCytology = (YellowstonePathology.Business.Test.ThinPrepPap.PanelSetOrderCytology)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(15);
			if (string.IsNullOrEmpty(panelSetOrderCytology.ScreeningImpression) == false)
			{
				this.AddNextNteElement("Epithelial Cell Description: ", document);
				this.AddNextNteElement(panelSetOrderCytology.ScreeningImpression, document);
				this.AddBlankNteElement(document);
			}

			this.AddNextNteElement("Specimen Adequacy:", document);
			this.AddNextNteElement(panelSetOrderCytology.SpecimenAdequacy, document);
			this.AddBlankNteElement(document);

			if (string.IsNullOrEmpty(panelSetOrderCytology.OtherConditions) == false)
			{
				this.AddNextNteElement("Other Conditions:", document);
				this.AddNextNteElement(panelSetOrderCytology.OtherConditions, document);
				this.AddBlankNteElement(document);
			}

			if (string.IsNullOrEmpty(panelSetOrderCytology.ReportComment) == false)
			{
				this.AddNextNteElement("Comment:", document);
				this.AddNextNteElement(panelSetOrderCytology.ReportComment, document);
				this.AddBlankNteElement(document);
			}

            YellowstonePathology.Business.Test.ThinPrepPap.PanelOrderCytology screeningPanelOrder = null;
            YellowstonePathology.Business.Test.ThinPrepPap.PanelOrderCytology reviewPanelOrder = null;

			foreach (YellowstonePathology.Business.Interface.IPanelOrder panelOrder in panelSetOrderCytology.PanelOrders)
			{
				Type objectType = panelOrder.GetType();
                if (typeof(YellowstonePathology.Business.Test.ThinPrepPap.PanelOrderCytology).IsAssignableFrom(objectType) == true)
				{
                    YellowstonePathology.Business.Test.ThinPrepPap.PanelOrderCytology cytologyPanelOrder = (YellowstonePathology.Business.Test.ThinPrepPap.PanelOrderCytology)panelOrder;
					if (cytologyPanelOrder.PanelId == 38)
					{
						if (cytologyPanelOrder.ScreeningType == "Primary Screening")
						{
							screeningPanelOrder = cytologyPanelOrder;
						}
						else if (cytologyPanelOrder.ScreeningType == "Pathologist Review")
						{
							reviewPanelOrder = cytologyPanelOrder;
						}
						else if (cytologyPanelOrder.ScreeningType == "Cytotech Review")
						{
							if (reviewPanelOrder == null || reviewPanelOrder.ScreeningType != "Pathologist Review")
							{
								reviewPanelOrder = cytologyPanelOrder;
							}
						}
					}
				}
			}

			YellowstonePathology.Business.User.SystemUser systemUser = YellowstonePathology.Business.User.SystemUserCollectionInstance.Instance.SystemUserCollection.GetSystemUserById(screeningPanelOrder.ScreenedById);
			if (string.IsNullOrEmpty(systemUser.Signature) == false)
			{
				this.AddNextNteElement("Screened By: " + systemUser.Signature, document);
			}

			string cytoTechFinal = YellowstonePathology.Business.Helper.DateTimeExtensions.DateStringFromNullable(screeningPanelOrder.AcceptedDate);
			this.AddNextNteElement("Date Finalized: " + cytoTechFinal, document);
			this.AddBlankNteElement(document);

			if (reviewPanelOrder != null)
			{
				string reviewedBy = YellowstonePathology.Business.User.SystemUserCollectionInstance.Instance.SystemUserCollection.GetSystemUserById(reviewPanelOrder.ScreenedById).Signature;
				string reviewedByFinal = YellowstonePathology.Business.Helper.DateTimeExtensions.DateStringFromNullable(reviewPanelOrder.AcceptedDate);

				if (reviewedBy.IndexOf("MD") >= 0)
				{
					this.AddNextNteElement("Interpreted By: " + reviewedBy + " " + reviewedByFinal, document);
				}
				else
				{
					this.AddNextNteElement("Reviewed By: " + reviewedBy + " " + reviewedByFinal, document);
				}
				this.AddBlankNteElement(document);
			}

			this.AddNextNteElement("CURRENT MOLECULAR TEST SUMMARY", document);

			YellowstonePathology.Business.Test.HPV.HPVTest panelSetHPV = new YellowstonePathology.Business.Test.HPV.HPVTest();
			YellowstonePathology.Business.Test.HPV1618.HPV1618Test panelSetHPV1618 = new YellowstonePathology.Business.Test.HPV1618.HPV1618Test();
            YellowstonePathology.Business.Test.NGCT.NGCTTest panelSetNGCT = new YellowstonePathology.Business.Test.NGCT.NGCTTest();
			YellowstonePathology.Business.Test.Trichomonas.TrichomonasTest panelSetTrichomonas = new YellowstonePathology.Business.Test.Trichomonas.TrichomonasTest();

			if (this.m_AccessionOrder.PanelSetOrderCollection.Exists(panelSetHPV.PanelSetId) == false &&
				this.m_AccessionOrder.PanelSetOrderCollection.Exists(panelSetHPV1618.PanelSetId) == false &&
				this.m_AccessionOrder.PanelSetOrderCollection.Exists(panelSetNGCT.PanelSetId) == false &&
				this.m_AccessionOrder.PanelSetOrderCollection.Exists(panelSetTrichomonas.PanelSetId) == false)
			{
				this.AddNextNteElement("No tests performed ", document);
				this.AddBlankNteElement(document);
			}
			else
			{
				if (this.m_AccessionOrder.PanelSetOrderCollection.Exists(panelSetHPV.PanelSetId) == true)
				{
					YellowstonePathology.Business.Test.HPV.HPVTestOrder hpvTestOrder = (YellowstonePathology.Business.Test.HPV.HPVTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(panelSetHPV.PanelSetId);
					this.AddNextNteElement("High Risk HPV: " + hpvTestOrder.Result, document);
					this.AddNextNteElement("Reference: Negative", document);
					string hpvFinal = YellowstonePathology.Business.Helper.DateTimeExtensions.DateStringFromNullable(hpvTestOrder.FinalDate);
					this.AddNextNteElement("Date Finalized: " + hpvFinal, document);
					this.AddBlankNteElement(document);
				}

				if (this.m_AccessionOrder.PanelSetOrderCollection.Exists(panelSetHPV1618.PanelSetId) == true)
				{
					YellowstonePathology.Business.Test.HPV1618.PanelSetOrderHPV1618 panelSetOrderHPV1618 = (YellowstonePathology.Business.Test.HPV1618.PanelSetOrderHPV1618)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(panelSetHPV1618.PanelSetId);
					this.AddNextNteElement("HPV type 16: " + panelSetOrderHPV1618.HPV16Result, document);
					this.AddNextNteElement("Reference: Negative", document);

					this.AddNextNteElement("HPV type 18: " + panelSetOrderHPV1618.HPV18Result, document);
					this.AddNextNteElement("Reference: Negative", document);
					string hpvFinal = YellowstonePathology.Business.Helper.DateTimeExtensions.DateStringFromNullable(panelSetOrderHPV1618.FinalDate);
					this.AddNextNteElement("Date Finalized: " + hpvFinal, document);
					this.AddBlankNteElement(document);
				}

				if (this.m_AccessionOrder.PanelSetOrderCollection.Exists(panelSetNGCT.PanelSetId) == true)
				{
					this.AddNextNteElement("Chlamydia Gonorrhea Screening ", document);
                    YellowstonePathology.Business.Test.NGCT.NGCTTestOrder panelSetOrderNGCT = (YellowstonePathology.Business.Test.NGCT.NGCTTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(panelSetNGCT.PanelSetId);
					this.AddNextNteElement("Chlamydia trachomatis: " + panelSetOrderNGCT.ChlamydiaTrachomatisResult, document);
					this.AddNextNteElement("Reference: Negative", document);

					this.AddNextNteElement("Neisseria gonorrhoeae: " + panelSetOrderNGCT.NeisseriaGonorrhoeaeResult, document);
					this.AddNextNteElement("Reference: Negative", document);
					string hpvFinal = YellowstonePathology.Business.Helper.DateTimeExtensions.DateStringFromNullable(panelSetOrderNGCT.FinalDate);
					this.AddNextNteElement("Date Finalized: " + hpvFinal, document);
					this.AddBlankNteElement(document);
				}

				if (this.m_AccessionOrder.PanelSetOrderCollection.Exists(panelSetTrichomonas.PanelSetId) == true)
				{
					this.AddNextNteElement("Trichomonas Screening ", document);
					YellowstonePathology.Business.Test.Trichomonas.TrichomonasTestOrder reportOrderTrichomonas = (YellowstonePathology.Business.Test.Trichomonas.TrichomonasTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(panelSetTrichomonas.PanelSetId);
					this.AddNextNteElement("Trichomonas vaginalis: " + reportOrderTrichomonas.Result, document);
					this.AddNextNteElement("Reference: Negative", document);
					string hpvFinal = YellowstonePathology.Business.Helper.DateTimeExtensions.DateStringFromNullable(reportOrderTrichomonas.FinalDate);
					this.AddNextNteElement("Date Finalized: " + hpvFinal, document);
					this.AddBlankNteElement(document);
				}
			}

			this.AddNextNteElement("Specimen Description: Thin Prep Fluid", document);
			this.AddNextNteElement("Specimen Source: " + this.m_AccessionOrder.SpecimenOrderCollection[0].SpecimenSource, document);
			string collectionDateTimeString = this.m_AccessionOrder.SpecimenOrderCollection[0].GetCollectionDateTimeString();
			this.AddNextNteElement("Collection Time: " + collectionDateTimeString, document);
			this.AddBlankNteElement(document);

			this.AddNextNteElement("Clinical History: ", document);
			this.HandleLongString(this.m_AccessionOrder.ClinicalHistory, document);
			this.AddBlankNteElement(document);

			this.AddNextNteElement("Method: ", document);
			this.HandleLongString(womensHealthProfileResult.Method, document);
			this.AddBlankNteElement(document);

			this.AddNextNteElement("References: ", document);
			this.HandleLongString(womensHealthProfileResult.References, document);
			this.AddBlankNteElement(document);

			string locationPerformed = womensHealthProfileTestOrder.GetLocationPerformedComment();
			this.HandleLongString(locationPerformed, document);
			this.AddNextNteElement(string.Empty, document);

			this.AddNextNteElement("PRIOR PAP AND GYN MOLECULAR TESTS", document);
			YellowstonePathology.Business.Test.ThinPrepPap.ThinPrepPapTest panelSetThinPrepPap = new YellowstonePathology.Business.Test.ThinPrepPap.ThinPrepPapTest();
			DateTime cutoffDate = this.m_AccessionOrder.AccessionDate.Value.AddYears(-5);

			YellowstonePathology.Business.Domain.PatientHistory patientHistory = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetPatientHistory(this.m_AccessionOrder.PatientId);
			YellowstonePathology.Business.Domain.PatientHistory priorPapRelatedHistory = patientHistory.GetPriorPapRelatedHistory(this.m_AccessionOrder.MasterAccessionNo, cutoffDate);

			if (priorPapRelatedHistory.Count == 0)
			{
				this.AddNextNteElement("No prior tests performed ", document);
				this.AddBlankNteElement(document);
			}
			else
			{
				foreach (YellowstonePathology.Business.Domain.PatientHistoryResult patientHistoryResult in priorPapRelatedHistory)
				{
					YellowstonePathology.Business.Test.AccessionOrder patientHistoryAccessionOrder = YellowstonePathology.Business.Persistence.DocumentGateway.Instance.GetAccessionOrderByMasterAccessionNo(patientHistoryResult.MasterAccessionNo);
					foreach (YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder in patientHistoryAccessionOrder.PanelSetOrderCollection)
					{
						string reportNo = null;
						string result = null;

						if (panelSetOrder.PanelSetId == panelSetThinPrepPap.PanelSetId ||
							panelSetOrder.PanelSetId == panelSetHPV.PanelSetId ||
							panelSetOrder.PanelSetId == panelSetHPV1618.PanelSetId ||
							panelSetOrder.PanelSetId == panelSetNGCT.PanelSetId ||
							panelSetOrder.PanelSetId == panelSetTrichomonas.PanelSetId)
						{
							reportNo = panelSetOrder.ReportNo;
							string finaldate = YellowstonePathology.Business.Helper.DateTimeExtensions.DateStringFromNullable(panelSetOrder.FinalDate);
							result = panelSetOrder.GetResultWithTestName();
							this.AddNextNteElement("Test: " + panelSetOrder.PanelSetName + " Report No: " + reportNo + " Result: " + result + " Final Date: " + finaldate, document);
							this.AddBlankNteElement(document);
						}
					}
				}
			}
			this.AddBlankNteElement(document);

            string disclaimer = "This Pap test is only a screening test. A negative result does not definitively rule out the presence of disease. Women should, therefore, in consultation with their physician, have this test performed at mutually agreed intervals.";
            this.AddNextNteElement(disclaimer, document);
            this.AddBlankNteElement(document);
        }
	}
}