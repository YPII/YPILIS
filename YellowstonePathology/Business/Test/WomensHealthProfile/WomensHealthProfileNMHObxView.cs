using System;
using System.Text;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Test.WomensHealthProfile
{
    class WomensHealthProfileNMHObxView : YellowstonePathology.Business.HL7View.NMH.NMHOBXView
    {
        public WomensHealthProfileNMHObxView(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo, int obxCount) 
            : base(accessionOrder, reportNo, obxCount)
		{

        }

        public override void ToXml(XElement document)
        {
            WomensHealthProfileTestOrder womensHealthProfileTestOrder = (WomensHealthProfileTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_ReportNo);
            WomensHealthProfileResult womensHealthProfileResult = new WomensHealthProfileResult(this.m_AccessionOrder);
            YellowstonePathology.Business.Amendment.Model.AmendmentCollection amendmentCollection = this.m_AccessionOrder.AmendmentCollection.GetAmendmentsForReport(this.m_ReportNo);

            ThinPrepPap.ThinPrepPapTest thinPrepPapTest = new ThinPrepPap.ThinPrepPapTest();
            bool hasPap = this.m_AccessionOrder.PanelSetOrderCollection.Exists(thinPrepPapTest.PanelSetId);

            this.AddNextObxElementV2("RN", "Report No", this.m_ReportNo, document, "F");

            if (hasPap == true)
            {
                StringBuilder papTest = new StringBuilder();

                YellowstonePathology.Business.Test.ThinPrepPap.PanelSetOrderCytology panelSetOrderCytology = (YellowstonePathology.Business.Test.ThinPrepPap.PanelSetOrderCytology)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(15);
                if (string.IsNullOrEmpty(panelSetOrderCytology.ScreeningImpression) == false)
                {
                    papTest.AppendLine("Epithelial Cell Description: " + panelSetOrderCytology.ScreeningImpression);
                }

                papTest.AppendLine("Specimen Adequacy: " + panelSetOrderCytology.SpecimenAdequacy);

                if (string.IsNullOrEmpty(panelSetOrderCytology.OtherConditions) == false)
                {
                    papTest.AppendLine("Other Conditions: " + panelSetOrderCytology.OtherConditions);
                }

                if (string.IsNullOrEmpty(panelSetOrderCytology.ReportComment) == false)
                {
                    papTest.AppendLine("Comment: " + panelSetOrderCytology.ReportComment);
                }

                if (string.IsNullOrEmpty(womensHealthProfileTestOrder.ManagementRecommendation) == false)
                {
                    papTest.AppendLine("Management Recommendation: " + womensHealthProfileTestOrder.ManagementRecommendation);
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
                    papTest.AppendLine("Screened By: " + systemUser.Signature);
                }

                string cytoTechFinal = Business.Helper.DateTimeExtensions.DateStringFromNullable(screeningPanelOrder.AcceptedDate);
                papTest.AppendLine("E-Signed: " + cytoTechFinal);

                if (reviewPanelOrder != null)
                {
                    string reviewedBy = Business.User.SystemUserCollectionInstance.Instance.SystemUserCollection.GetSystemUserById(reviewPanelOrder.ScreenedById).Signature;
                    string reviewedByFinal = Business.Helper.DateTimeExtensions.DateStringFromNullable(reviewPanelOrder.AcceptedDate);

                    if (reviewedBy.IndexOf("MD") >= 0)
                    {
                        papTest.AppendLine("Interpreted By: " + reviewedBy + " " + reviewedByFinal);
                    }
                    else
                    {
                        papTest.AppendLine("Reviewed By: " + reviewedBy + " " + reviewedByFinal);
                    }
                }

                this.AddNextObxElementV2("PAPRSLT", "PAP TEST RESULT", papTest.ToString(), document, "F");
            }

            if (amendmentCollection.Count != 0)
            {
                StringBuilder amendments = new StringBuilder();
                foreach (YellowstonePathology.Business.Amendment.Model.Amendment amendment in amendmentCollection)
                {
                    if (amendment.Final == true)
                    {
                        amendments.AppendLine(amendment.AmendmentType + ": " + amendment.AmendmentDate.Value.ToString("MM/dd/yyyy"));
                        amendments.AppendLine(amendment.Text);
                        if (amendment.RequirePathologistSignature == true)
                        {
                            amendments.AppendLine("Signature: " + amendment.PathologistSignature);
                            amendments.AppendLine("E-signed: " + amendment.FinalTime.Value.ToString("MM/dd/yyyy HH:mm"));
                        }
                    }
                }
                amendments.AppendLine();
                this.AddNextObxElementV2("AM", "Amendments", amendments.ToString(), document, "F");
            }

            StringBuilder testsPerformed = new StringBuilder();
            YellowstonePathology.Business.Test.HPV.HPVTest panelSetHPV = new YellowstonePathology.Business.Test.HPV.HPVTest();
            YellowstonePathology.Business.Test.HPV1618.HPV1618Test panelSetHPV1618 = new YellowstonePathology.Business.Test.HPV1618.HPV1618Test();
            YellowstonePathology.Business.Test.NGCT.NGCTTest panelSetNGCT = new YellowstonePathology.Business.Test.NGCT.NGCTTest();
            YellowstonePathology.Business.Test.Trichomonas.TrichomonasTest panelSetTrichomonas = new YellowstonePathology.Business.Test.Trichomonas.TrichomonasTest();

            if (this.m_AccessionOrder.PanelSetOrderCollection.Exists(panelSetHPV.PanelSetId) == false &&
                this.m_AccessionOrder.PanelSetOrderCollection.Exists(panelSetHPV1618.PanelSetId) == false &&
                this.m_AccessionOrder.PanelSetOrderCollection.Exists(panelSetNGCT.PanelSetId) == false &&
                this.m_AccessionOrder.PanelSetOrderCollection.Exists(panelSetTrichomonas.PanelSetId) == false)
            {
                testsPerformed.AppendLine("No tests performed");
            }
            else
            {
                if (this.m_AccessionOrder.PanelSetOrderCollection.Exists(panelSetHPV.PanelSetId) == true)
                {
                    YellowstonePathology.Business.Test.HPV.HPVTestOrder hpvTestOrder = (YellowstonePathology.Business.Test.HPV.HPVTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(panelSetHPV.PanelSetId);
                    testsPerformed.AppendLine("High Risk HPV: " + hpvTestOrder.Result);
                    testsPerformed.AppendLine("Reference: Negative");
                    string hpvFinal = Business.Helper.DateTimeExtensions.DateStringFromNullable(hpvTestOrder.FinalDate);
                    testsPerformed.AppendLine("Date Finalized: " + hpvFinal);
                }

                if (this.m_AccessionOrder.PanelSetOrderCollection.Exists(panelSetHPV1618.PanelSetId) == true)
                {
                    YellowstonePathology.Business.Test.HPV1618.PanelSetOrderHPV1618 panelSetOrderHPV1618 = (YellowstonePathology.Business.Test.HPV1618.PanelSetOrderHPV1618)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(panelSetHPV1618.PanelSetId);
                    testsPerformed.AppendLine("HPV type 16: " + panelSetOrderHPV1618.HPV16Result);
                    testsPerformed.AppendLine("Reference: Negative");

                    testsPerformed.AppendLine("HPV type 18: " + panelSetOrderHPV1618.HPV18Result);
                    testsPerformed.AppendLine("Reference: Negative");
                    string hpvFinal = Business.Helper.DateTimeExtensions.DateStringFromNullable(panelSetOrderHPV1618.FinalDate);
                    testsPerformed.AppendLine("Date Finalized: " + hpvFinal);
                }

                if (this.m_AccessionOrder.PanelSetOrderCollection.Exists(panelSetNGCT.PanelSetId) == true)
                {
                    //testsPerformed.AppendLine("Chlamydia Gonorrhea Screening");
                    YellowstonePathology.Business.Test.NGCT.NGCTTestOrder panelSetOrderNGCT = (YellowstonePathology.Business.Test.NGCT.NGCTTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(panelSetNGCT.PanelSetId);
                    testsPerformed.AppendLine("Chlamydia trachomatis: " + panelSetOrderNGCT.ChlamydiaTrachomatisResult);
                    testsPerformed.AppendLine("Reference: Negative");

                    testsPerformed.AppendLine("Neisseria gonorrhoeae: " + panelSetOrderNGCT.NeisseriaGonorrhoeaeResult);
                    testsPerformed.AppendLine("Reference: Negative");
                    string hpvFinal = Business.Helper.DateTimeExtensions.DateStringFromNullable(panelSetOrderNGCT.FinalDate);
                    testsPerformed.AppendLine("Date Finalized: " + hpvFinal);
                }

                if (this.m_AccessionOrder.PanelSetOrderCollection.Exists(panelSetTrichomonas.PanelSetId) == true)
                {
                    //testsPerformed.AppendLine("Trichomonas Screening");
                    YellowstonePathology.Business.Test.Trichomonas.TrichomonasTestOrder reportOrderTrichomonas = (YellowstonePathology.Business.Test.Trichomonas.TrichomonasTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(panelSetTrichomonas.PanelSetId);
                    testsPerformed.AppendLine("Trichomonas vaginalis: " + reportOrderTrichomonas.Result);
                    testsPerformed.AppendLine("Reference: Negative");
                    string hpvFinal = Business.Helper.DateTimeExtensions.DateStringFromNullable(reportOrderTrichomonas.FinalDate);
                    testsPerformed.AppendLine("Date Finalized: " + hpvFinal);
                }
            }

            this.AddNextObxElementV2("CURTS", "CURRENT MOLECULAR TEST SUMMARY", testsPerformed.ToString(), document, "F");

            this.AddNextObxElementV2("SD", "Specimen Description", "Thin Prep Fluid", document, "F");
            this.AddNextObxElementV2("SS", "Specimen Source", this.m_AccessionOrder.SpecimenOrderCollection[0].SpecimenSource, document, "F");
            string collectionDateTimeString = this.m_AccessionOrder.SpecimenOrderCollection[0].GetCollectionDateTimeString();
            this.AddNextObxElementV2("COLD", "Collection Date/Time", collectionDateTimeString, document, "F");

            this.AddNextObxElementV2("CH", "Clinical History", this.m_AccessionOrder.ClinicalHistory, document, "F");

            this.AddNextObxElementV2("MD", "Method", womensHealthProfileResult.Method, document, "F");

            this.AddNextObxElementV2("REF", "References", womensHealthProfileResult.References, document, "F");

            string locationPerformed = womensHealthProfileTestOrder.GetLocationPerformedComment();
            this.AddNextObxElementV2("LOC", "Location Performed", locationPerformed, document, "F");

            StringBuilder priorTests = new StringBuilder();

            YellowstonePathology.Business.Test.ThinPrepPap.ThinPrepPapTest panelSetThinPrepPap = new YellowstonePathology.Business.Test.ThinPrepPap.ThinPrepPapTest();
            DateTime cutoffDate = this.m_AccessionOrder.AccessionDate.Value.AddYears(-5);

            YellowstonePathology.Business.Domain.PatientHistory patientHistory = Business.Gateway.AccessionOrderGateway.GetPatientHistory(this.m_AccessionOrder.PatientId);
            YellowstonePathology.Business.Domain.PatientHistory priorPapRelatedHistory = patientHistory.GetPriorPapRelatedHistory(this.m_AccessionOrder.MasterAccessionNo, cutoffDate);

            if (priorPapRelatedHistory.Count == 0)
            {
                priorTests.AppendLine("No prior tests performed");
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
                            priorTests.AppendLine("Test: " + panelSetOrder.PanelSetName + " Report No: " + reportNo + " Result: " + result + " Final Date: " + finaldate);
                        }
                    }
                }
            }
            this.AddNextObxElementV2("PRPAP", "PRIOR PAP AND GYN MOLECULAR TESTS", priorTests.ToString(), document, "F");
        }
    }
}
