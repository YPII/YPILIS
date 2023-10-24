using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Test.WomensHealthProfile
{
    public class WomensHealthProfileWPHOBXView : YellowstonePathology.Business.HL7View.WPH.WPHOBXView
    {
        public WomensHealthProfileWPHOBXView(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo, int obxCount) 
            : base(accessionOrder, reportNo, obxCount)
		{

        }

        public override void ToXml(XElement document)
        {
            WomensHealthProfileTestOrder womensHealthProfileTestOrder = (WomensHealthProfileTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_ReportNo);
            WomensHealthProfileResult womensHealthProfileResult = new WomensHealthProfileResult(this.m_AccessionOrder);

            ThinPrepPap.ThinPrepPapTest thinPrepPapTest = new ThinPrepPap.ThinPrepPapTest();
            bool hasPap = this.m_AccessionOrder.PanelSetOrderCollection.Exists(thinPrepPapTest.PanelSetId);

            this.AddHeader(document, womensHealthProfileTestOrder, "Women's Health Profile");
            this.AddNextObxElement("", document, "F");

            if (hasPap == true)
            {
                this.AddNextObxElement("PAP TEST RESULT: ", document, "F");

                YellowstonePathology.Business.Test.ThinPrepPap.PanelSetOrderCytology panelSetOrderCytology = (YellowstonePathology.Business.Test.ThinPrepPap.PanelSetOrderCytology)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(15);
                if (string.IsNullOrEmpty(panelSetOrderCytology.ScreeningImpression) == false)
                {
                    this.AddNextObxElement("Epithelial Cell Description: ", document, "F");
                    this.HandleLongString(panelSetOrderCytology.ScreeningImpression, document, "F");
                    this.AddNextObxElement("", document, "F");
                }

                this.AddNextObxElement("Specimen Adequacy:", document, "F");
                this.AddNextObxElement(panelSetOrderCytology.SpecimenAdequacy, document, "F");
                this.AddNextObxElement("", document, "F");

                if (string.IsNullOrEmpty(panelSetOrderCytology.OtherConditions) == false)
                {
                    this.AddNextObxElement("Other Conditions:", document, "F");
                    this.HandleLongString(panelSetOrderCytology.OtherConditions, document, "F");
                    this.AddNextObxElement("", document, "F");
                }

                if (string.IsNullOrEmpty(panelSetOrderCytology.ReportComment) == false)
                {
                    this.AddNextObxElement("Comment:", document, "F");
                    this.HandleLongString(panelSetOrderCytology.ReportComment, document, "F");
                    this.AddNextObxElement("", document, "F");
                }

                if (string.IsNullOrEmpty(womensHealthProfileTestOrder.ManagementRecommendation) == false)
                {
                    this.AddNextObxElement("Management Recommendation:", document, "F");
                    this.HandleLongString(womensHealthProfileTestOrder.ManagementRecommendation, document, "F");
                    this.AddNextObxElement("", document, "F");
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

                YellowstonePathology.Business.User.SystemUser systemUser = Business.User.SystemUserCollectionInstance.Instance.SystemUserCollection.GetSystemUserById(screeningPanelOrder.ScreenedById);
                if (string.IsNullOrEmpty(systemUser.Signature) == false)
                {
                    this.AddNextObxElement("Screened By: " + systemUser.Signature, document, "F");
                }

                string cytoTechFinal = Business.Helper.DateTimeExtensions.DateAndTimeStringFromNullable(screeningPanelOrder.AcceptedTime);
                this.AddNextObxElement("***E - Signed " + cytoTechFinal + " * **", document, "F");
                this.AddNextObxElement("", document, "F");

                if (reviewPanelOrder != null)
                {
                    string reviewedBy = Business.User.SystemUserCollectionInstance.Instance.SystemUserCollection.GetSystemUserById(reviewPanelOrder.ScreenedById).Signature;
                    string reviewedByFinal = Business.Helper.DateTimeExtensions.DateStringFromNullable(reviewPanelOrder.AcceptedDate);

                    if (reviewedBy.IndexOf("MD") >= 0)
                    {
                        this.AddNextObxElement("Interpreted By: " + reviewedBy + " " + reviewedByFinal, document, "F");
                    }
                    else
                    {
                        this.AddNextObxElement("Reviewed By: " + reviewedBy + " " + reviewedByFinal, document, "F");
                    }
                    this.AddNextObxElement("", document, "F");
                }
            }

            this.AddAmendments(document);

            this.AddNextObxElement("CURRENT MOLECULAR TEST SUMMARY", document, "F");

            YellowstonePathology.Business.Test.HPV.HPVTest panelSetHPV = new YellowstonePathology.Business.Test.HPV.HPVTest();
            YellowstonePathology.Business.Test.HPV1618.HPV1618Test panelSetHPV1618 = new YellowstonePathology.Business.Test.HPV1618.HPV1618Test();
            YellowstonePathology.Business.Test.NGCT.NGCTTest panelSetNGCT = new YellowstonePathology.Business.Test.NGCT.NGCTTest();
            YellowstonePathology.Business.Test.Trichomonas.TrichomonasTest panelSetTrichomonas = new YellowstonePathology.Business.Test.Trichomonas.TrichomonasTest();

            if (this.m_AccessionOrder.PanelSetOrderCollection.Exists(panelSetHPV.PanelSetId) == false &&
                this.m_AccessionOrder.PanelSetOrderCollection.Exists(panelSetHPV1618.PanelSetId) == false &&
                this.m_AccessionOrder.PanelSetOrderCollection.Exists(panelSetNGCT.PanelSetId) == false &&
                this.m_AccessionOrder.PanelSetOrderCollection.Exists(panelSetTrichomonas.PanelSetId) == false)
            {
                this.AddNextObxElement("No tests performed ", document, "F");
                this.AddNextObxElement("", document, "F");
            }
            else
            {
                if (this.m_AccessionOrder.PanelSetOrderCollection.Exists(panelSetHPV.PanelSetId) == true)
                {
                    YellowstonePathology.Business.Test.HPV.HPVTestOrder hpvTestOrder = (YellowstonePathology.Business.Test.HPV.HPVTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(panelSetHPV.PanelSetId);
                    this.AddNextObxElement("High Risk HPV: " + hpvTestOrder.Result, document, "F");
                    this.AddNextObxElement("Reference: Negative", document, "F");
                    string hpvFinal = Business.Helper.DateTimeExtensions.DateAndTimeStringFromNullable(hpvTestOrder.FinalTime);
                    this.AddNextObxElement("Date Finalized: " + hpvFinal, document, "F");
                    this.AddNextObxElement("", document, "F");
                }

                if (this.m_AccessionOrder.PanelSetOrderCollection.Exists(panelSetHPV1618.PanelSetId) == true)
                {
                    YellowstonePathology.Business.Test.HPV1618.PanelSetOrderHPV1618 panelSetOrderHPV1618 = (YellowstonePathology.Business.Test.HPV1618.PanelSetOrderHPV1618)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(panelSetHPV1618.PanelSetId);
                    this.AddNextObxElement("HPV type 16: " + panelSetOrderHPV1618.HPV16Result, document, "F");
                    this.AddNextObxElement("Reference: Negative", document, "F");

                    this.AddNextObxElement("HPV type 18/45: " + panelSetOrderHPV1618.HPV18Result, document, "F");
                    this.AddNextObxElement("Reference: Negative", document, "F");
                    string hpvFinal = Business.Helper.DateTimeExtensions.DateAndTimeStringFromNullable(panelSetOrderHPV1618.FinalTime);
                    this.AddNextObxElement("Date Finalized: " + hpvFinal, document, "F");
                    this.AddNextObxElement("", document, "F");
                }

                if (this.m_AccessionOrder.PanelSetOrderCollection.Exists(panelSetNGCT.PanelSetId) == true)
                {
                    this.AddNextObxElement("Chlamydia Gonorrhea Screening ", document, "F");
                    YellowstonePathology.Business.Test.NGCT.NGCTTestOrder panelSetOrderNGCT = (YellowstonePathology.Business.Test.NGCT.NGCTTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(panelSetNGCT.PanelSetId);
                    this.AddNextObxElement("Chlamydia trachomatis: " + panelSetOrderNGCT.ChlamydiaTrachomatisResult, document, "F");
                    this.AddNextObxElement("Reference: Negative", document, "F");

                    this.AddNextObxElement("Neisseria gonorrhoeae: " + panelSetOrderNGCT.NeisseriaGonorrhoeaeResult, document, "F");
                    this.AddNextObxElement("Reference: Negative", document, "F");
                    string hpvFinal = Business.Helper.DateTimeExtensions.DateAndTimeStringFromNullable(panelSetOrderNGCT.FinalTime);
                    this.AddNextObxElement("Date Finalized: " + hpvFinal, document, "F");
                    this.AddNextObxElement("", document, "F");
                }

                if (this.m_AccessionOrder.PanelSetOrderCollection.Exists(panelSetTrichomonas.PanelSetId) == true)
                {
                    this.AddNextObxElement("Trichomonas Screening ", document, "F");
                    YellowstonePathology.Business.Test.Trichomonas.TrichomonasTestOrder reportOrderTrichomonas = (YellowstonePathology.Business.Test.Trichomonas.TrichomonasTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(panelSetTrichomonas.PanelSetId);
                    this.AddNextObxElement("Trichomonas vaginalis: " + reportOrderTrichomonas.Result, document, "F");
                    this.AddNextObxElement("Reference: Negative", document, "F");
                    string hpvFinal = Business.Helper.DateTimeExtensions.DateAndTimeStringFromNullable(reportOrderTrichomonas.FinalTime);
                    this.AddNextObxElement("Date Finalized: " + hpvFinal, document, "F");
                    this.AddNextObxElement("", document, "F");
                }
            }

            this.AddNextObxElement("Specimen Description: Thin Prep Fluid", document, "F");
            this.AddNextObxElement("Specimen Source: " + this.m_AccessionOrder.SpecimenOrderCollection[0].SpecimenSource, document, "F");
            string collectionDateTimeString = this.m_AccessionOrder.SpecimenOrderCollection[0].GetCollectionDateTimeString();
            this.AddNextObxElement("Collection Date/Time: " + collectionDateTimeString, document, "F");
            this.AddNextObxElement("", document, "F");

            this.AddNextObxElement("Clinical History: ", document, "F");
            this.HandleLongString(this.m_AccessionOrder.ClinicalHistory, document, "F");
            this.AddNextObxElement("", document, "F");

            this.AddNextObxElement("Method: ", document, "F");
            this.HandleLongString(womensHealthProfileResult.Method, document, "F");
            this.AddNextObxElement("", document, "F");

            this.AddNextObxElement("References: ", document, "F");
            this.HandleLongString(womensHealthProfileResult.References, document, "F");
            this.AddNextObxElement("", document, "F");

            string papComment = "This Pap test is only a screening test. A negative result does not definitively rule out the presence of disease. Women should, therefore, in consultation with their physician, have this test performed at mutually agreed intervals.";
            this.HandleLongString(papComment, document, "F");
            this.AddNextObxElement(string.Empty, document, "F");

            string locationPerformed = womensHealthProfileTestOrder.GetLocationPerformedComment();
            this.HandleLongString(locationPerformed, document, "F");
            this.AddNextObxElement(string.Empty, document, "F");            

            this.AddNextObxElement("PRIOR PAP AND GYN MOLECULAR TESTS", document, "F");
            YellowstonePathology.Business.Test.ThinPrepPap.ThinPrepPapTest panelSetThinPrepPap = new YellowstonePathology.Business.Test.ThinPrepPap.ThinPrepPapTest();
            DateTime cutoffDate = this.m_AccessionOrder.AccessionDate.Value.AddYears(-5);

            YellowstonePathology.Business.Domain.PatientHistory patientHistory = Business.Gateway.AccessionOrderGateway.GetPatientHistory(this.m_AccessionOrder.PatientId);
            YellowstonePathology.Business.Domain.PatientHistory priorPapRelatedHistory = patientHistory.GetPriorPapRelatedHistory(this.m_AccessionOrder.MasterAccessionNo, cutoffDate);

            if (priorPapRelatedHistory.Count == 0)
            {
                this.AddNextObxElement("No prior tests performed ", document, "F");
                this.AddNextObxElement("", document, "F");
            }
            else
            {
                foreach (YellowstonePathology.Business.Domain.PatientHistoryResult patientHistoryResult in priorPapRelatedHistory)
                {
                    YellowstonePathology.Business.Test.AccessionOrder accessionOrder = Business.Persistence.DocumentGateway.Instance.GetAccessionOrderByMasterAccessionNo(patientHistoryResult.MasterAccessionNo);
                    foreach (YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder in accessionOrder.PanelSetOrderCollection)
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
                            string finaldate = Business.Helper.DateTimeExtensions.DateStringFromNullable(panelSetOrder.FinalDate);
                            result = panelSetOrder.GetResultWithTestName();
                            this.AddNextObxElement("Test: " + panelSetOrder.PanelSetName + " Report No: " + reportNo + " Result: " + result + " Final Date: " + finaldate, document, "F");
                            this.AddNextObxElement("", document, "F");
                        }
                    }
                }
            }
            this.AddNextObxElement("", document, "F");            
        }
    }
}
