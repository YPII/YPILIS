using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Test.WomensHealthProfile
{
    public class WomensHealthProfileRiverstoneOBXView : Business.HL7View.Riverstone.RiverstoneOBXView
    {
        public WomensHealthProfileRiverstoneOBXView(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo, int obxCount)
            : base(accessionOrder, reportNo, obxCount)
        {

        }

        public override void ToXml(XElement document, string resultStatus)
        {
            WomensHealthProfileTestOrder womensHealthProfileTestOrder = (WomensHealthProfileTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_ReportNo);
            WomensHealthProfileResult womensHealthProfileResult = new WomensHealthProfileResult(this.m_AccessionOrder);
            YellowstonePathology.Business.Amendment.Model.AmendmentCollection amendmentCollection = this.m_AccessionOrder.AmendmentCollection.GetAmendmentsForReport(this.m_ReportNo);                       

            ThinPrepPap.ThinPrepPapTest thinPrepPapTest = new ThinPrepPap.ThinPrepPapTest();
            bool hasPap = this.m_AccessionOrder.PanelSetOrderCollection.Exists(thinPrepPapTest.PanelSetId);

            this.AddNextNteElement($"Report No: {this.m_ReportNo}", document);

            if (hasPap == true)
            {                
                YellowstonePathology.Business.Test.ThinPrepPap.PanelSetOrderCytology panelSetOrderCytology = (YellowstonePathology.Business.Test.ThinPrepPap.PanelSetOrderCytology)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(15);
                if (string.IsNullOrEmpty(panelSetOrderCytology.ScreeningImpression) == false)
                {
                    this.AddNextNteElement($"Epithelial Cell Description: {panelSetOrderCytology.ScreeningImpression}", document);
                }

                this.AddNextNteElement($"Specimen Adequacy: {panelSetOrderCytology.SpecimenAdequacy}", document);

                if (string.IsNullOrEmpty(panelSetOrderCytology.OtherConditions) == false)
                {
                    this.AddNextNteElement($"Other Conditions: {panelSetOrderCytology.OtherConditions}", document);
                }

                if (string.IsNullOrEmpty(panelSetOrderCytology.ReportComment) == false)
                {
                    this.AddNextNteElement($"Comment: {panelSetOrderCytology.ReportComment}", document);
                }

                if (string.IsNullOrEmpty(womensHealthProfileTestOrder.ManagementRecommendation) == false)
                {
                    this.AddNextNteElement($"Management Recommendation: {womensHealthProfileTestOrder.ManagementRecommendation}", document);
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
                    this.AddNextNteElement($"Screened By: {systemUser.Signature}", document);
                }

                string cytoTechFinal = Business.Helper.DateTimeExtensions.DateStringFromNullable(screeningPanelOrder.AcceptedDate);
                this.AddNextNteElement($"E-Signed: {cytoTechFinal}", document);

                if (reviewPanelOrder != null)
                {
                    string reviewedBy = Business.User.SystemUserCollectionInstance.Instance.SystemUserCollection.GetSystemUserById(reviewPanelOrder.ScreenedById).Signature;
                    string reviewedByFinal = Business.Helper.DateTimeExtensions.DateStringFromNullable(reviewPanelOrder.AcceptedDate);

                    if (reviewedBy.IndexOf("MD") >= 0)
                    {
                        this.AddNextNteElement($"Interpreted By: {reviewedBy} {reviewedByFinal}", document);
                    }
                    else
                    {
                        this.AddNextNteElement($"Reviewed By: {reviewedBy} {reviewedByFinal}", document);
                    }
                }
                
                this.AddNextObxWithAttributeElement("THINPREPRESULT^Thin Prep Pap", panelSetOrderCytology.ScreeningImpression, document, resultStatus);
            }
            else
            {
                this.AddNextObxWithAttributeElement("THINPREPRESULT^Thin Prep Pap", "Not Performed", document, resultStatus);
            }

            if (amendmentCollection.Count != 0)
            {                
                foreach (YellowstonePathology.Business.Amendment.Model.Amendment amendment in amendmentCollection)
                {
                    if (amendment.Final == true)
                    {
                        this.AddNextNteElement($"{amendment.AmendmentType}: {amendment.AmendmentDate.Value.ToString("MM/dd/yyyy")}", document);
                        this.AddNextNteElement($"{amendment.Text}", document);
                        if (amendment.RequirePathologistSignature == true)
                        {
                            this.AddNextNteElement($"Signature: {amendment.PathologistSignature}", document);
                            this.AddNextNteElement($"E-signed: {amendment.FinalTime.Value.ToString("MM/dd/yyyy HH:mm")}", document);
                        }
                    }
                }
                this.AddNextNteElement("", document);                
            }
            
            YellowstonePathology.Business.Test.HPV.HPVTest panelSetHPV = new YellowstonePathology.Business.Test.HPV.HPVTest();
            YellowstonePathology.Business.Test.HPV1618.HPV1618Test panelSetHPV1618 = new YellowstonePathology.Business.Test.HPV1618.HPV1618Test();
            YellowstonePathology.Business.Test.NGCT.NGCTTest panelSetNGCT = new YellowstonePathology.Business.Test.NGCT.NGCTTest();
            YellowstonePathology.Business.Test.Trichomonas.TrichomonasTest panelSetTrichomonas = new YellowstonePathology.Business.Test.Trichomonas.TrichomonasTest();                                   

            if (this.m_AccessionOrder.PanelSetOrderCollection.Exists(panelSetHPV.PanelSetId) == false &&
                this.m_AccessionOrder.PanelSetOrderCollection.Exists(panelSetHPV1618.PanelSetId) == false &&
                this.m_AccessionOrder.PanelSetOrderCollection.Exists(panelSetNGCT.PanelSetId) == false &&
                this.m_AccessionOrder.PanelSetOrderCollection.Exists(panelSetTrichomonas.PanelSetId) == false)
            {
                this.AddNextNteElement("No tests performed", document);
            }
            else
            {
                if (this.m_AccessionOrder.PanelSetOrderCollection.Exists(panelSetHPV.PanelSetId) == true)
                {
                    YellowstonePathology.Business.Test.HPV.HPVTestOrder hpvTestOrder = (YellowstonePathology.Business.Test.HPV.HPVTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(panelSetHPV.PanelSetId);
                    this.AddNextNteElement($"High Risk HPV: {hpvTestOrder.Result}", document);
                    this.AddNextNteElement("Reference: Negative", document);
                    string hpvFinal = Business.Helper.DateTimeExtensions.DateStringFromNullable(hpvTestOrder.FinalDate);
                    this.AddNextNteElement($"Date Finalized: {hpvFinal}", document);

                    this.AddNextObxWithAttributeElement("HPVRESULT^High Risk HPV", hpvTestOrder.Result, document, resultStatus);
                }
                else
                {
                    this.AddNextObxWithAttributeElement("HPVRESULT^High Risk HPV", "Not Performed", document, resultStatus);
                }

                if (this.m_AccessionOrder.PanelSetOrderCollection.Exists(panelSetHPV1618.PanelSetId) == true)
                {
                    YellowstonePathology.Business.Test.HPV1618.PanelSetOrderHPV1618 panelSetOrderHPV1618 = (YellowstonePathology.Business.Test.HPV1618.PanelSetOrderHPV1618)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(panelSetHPV1618.PanelSetId);
                    this.AddNextNteElement($"HPV type 16: {panelSetOrderHPV1618.HPV16Result}", document);
                    this.AddNextNteElement($"Reference: Negative", document);

                    this.AddNextNteElement($"HPV type 18: {panelSetOrderHPV1618.HPV18Result}", document);
                    this.AddNextNteElement($"Reference: Negative", document);
                    string hpvFinal = Business.Helper.DateTimeExtensions.DateStringFromNullable(panelSetOrderHPV1618.FinalDate);
                    this.AddNextNteElement($"Date Finalized: {hpvFinal}", document);

                    this.AddNextObxWithAttributeElement("HPV16RESULT^HPV Genotypes 16", panelSetOrderHPV1618.HPV16Result, document, resultStatus);
                    this.AddNextObxWithAttributeElement("HPV18RESULT^HPV Genotypes 18", panelSetOrderHPV1618.HPV18Result, document, resultStatus);
                }
                else
                {
                    this.AddNextObxWithAttributeElement("HPV16RESULT^HPV Genotypes 16", "Not Performed", document, resultStatus);
                    this.AddNextObxWithAttributeElement("HPV18RESULT^HPV Genotypes 18", "Not Performed", document, resultStatus);
                }

                if (this.m_AccessionOrder.PanelSetOrderCollection.Exists(panelSetNGCT.PanelSetId) == true)
                {                    
                    YellowstonePathology.Business.Test.NGCT.NGCTTestOrder panelSetOrderNGCT = (YellowstonePathology.Business.Test.NGCT.NGCTTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(panelSetNGCT.PanelSetId);
                    this.AddNextNteElement($"Chlamydia trachomatis: {panelSetOrderNGCT.ChlamydiaTrachomatisResult}", document);
                    this.AddNextNteElement($"Reference: Negative", document);

                    this.AddNextNteElement($"Neisseria gonorrhoeae: {panelSetOrderNGCT.NeisseriaGonorrhoeaeResult}", document);
                    this.AddNextNteElement($"Reference: Negative", document);
                    string hpvFinal = Business.Helper.DateTimeExtensions.DateStringFromNullable(panelSetOrderNGCT.FinalDate);
                    this.AddNextNteElement($"Date Finalized: {hpvFinal}", document);
                }

                if (this.m_AccessionOrder.PanelSetOrderCollection.Exists(panelSetTrichomonas.PanelSetId) == true)
                {                 
                    YellowstonePathology.Business.Test.Trichomonas.TrichomonasTestOrder reportOrderTrichomonas = (YellowstonePathology.Business.Test.Trichomonas.TrichomonasTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(panelSetTrichomonas.PanelSetId);
                    this.AddNextNteElement($"Trichomonas vaginalis: {reportOrderTrichomonas.Result}", document);
                    this.AddNextNteElement($"Reference: Negative", document);
                    string hpvFinal = Business.Helper.DateTimeExtensions.DateStringFromNullable(reportOrderTrichomonas.FinalDate);
                    this.AddNextNteElement($"Date Finalized: {hpvFinal}", document);
                }
            }                       

            this.AddNextNteElement($"Specimen Description: Thin Prep Fluid", document);
            this.AddNextNteElement($"Specimen Source {this.m_AccessionOrder.SpecimenOrderCollection[0].SpecimenSource}", document);
            string collectionDateTimeString = this.m_AccessionOrder.SpecimenOrderCollection[0].GetCollectionDateTimeString();
            this.AddNextNteElement($"Collection Date/Time: {collectionDateTimeString}", document);
            
            this.AddNextNteElement($"Clinical History: ", document);
            this.HandleLongStringAddNTE(this.m_AccessionOrder.ClinicalHistory, document);

            this.AddNextNteElement("Method:", document);
            this.HandleLongStringAddNTE(womensHealthProfileResult.Method, document);

            this.AddNextNteElement("References:", document);
            this.HandleLongStringAddNTE(womensHealthProfileResult.References, document);            

            string locationPerformed = womensHealthProfileTestOrder.GetLocationPerformedComment();
            this.AddNextNteElement($"Location Performed {locationPerformed}", document);            

            YellowstonePathology.Business.Test.ThinPrepPap.ThinPrepPapTest panelSetThinPrepPap = new YellowstonePathology.Business.Test.ThinPrepPap.ThinPrepPapTest();
            DateTime cutoffDate = this.m_AccessionOrder.AccessionDate.Value.AddYears(-5);

            YellowstonePathology.Business.Domain.PatientHistory patientHistory = Business.Gateway.AccessionOrderGateway.GetPatientHistory(this.m_AccessionOrder.PatientId);
            YellowstonePathology.Business.Domain.PatientHistory priorPapRelatedHistory = patientHistory.GetPriorPapRelatedHistory(this.m_AccessionOrder.MasterAccessionNo, cutoffDate);

            if (priorPapRelatedHistory.Count == 0)
            {
                this.AddNextNteElement($"No prior tests performed", document);
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
                            this.AddNextNteElement($"Test: {panelSetOrder.PanelSetName} Report No: {reportNo} Result: {result} Final Date: {finaldate}", document);
                        }
                    }
                }
            }
        }                
    }
}
