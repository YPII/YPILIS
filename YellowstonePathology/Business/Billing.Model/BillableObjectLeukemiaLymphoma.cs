using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Billing.Model
{
    public class BillableObjectLeukemiaLymphoma : BillableObject
    {
        public BillableObjectLeukemiaLymphoma(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo) 
            : base(accessionOrder, reportNo)
        {

        }

        public override void SetPanelSetOrderCPTCodes()
        {
            Business.Test.LLP.PanelSetOrderLeukemiaLymphoma panelSetOrderLeukemiaLymphoma = (Business.Test.LLP.PanelSetOrderLeukemiaLymphoma)this.PanelSetOrder;

            if (panelSetOrderLeukemiaLymphoma.FlowMarkerCollection.Count > 0)
			{
				if (this.m_PanelSetOrder.PanelSetOrderCPTCodeCollection.Exists("88184", 1) == false)
				{
					YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrderByOrderTarget(this.m_PanelSetOrder.OrderedOnId);
					Test.LLP.PanelSetOrderLeukemiaLymphoma panelSetOrder = (Test.LLP.PanelSetOrderLeukemiaLymphoma)this.m_PanelSetOrder;

                    YellowstonePathology.Business.Billing.Model.CptCode cpt88184 = Store.AppDataStore.Instance.CPTCodeCollection.GetClone("88184", null);
					YellowstonePathology.Business.Test.PanelSetOrderCPTCode panelSetOrderCPTCode88184 = this.m_PanelSetOrder.PanelSetOrderCPTCodeCollection.GetNextItem(this.m_PanelSetOrder.ReportNo);
					panelSetOrderCPTCode88184.Quantity = 1;
                    panelSetOrderCPTCode88184.CPTCode = cpt88184.Code;
                    panelSetOrderCPTCode88184.CodeType = cpt88184.CodeType.ToString();
					panelSetOrderCPTCode88184.CodeableDescription = "Specimen " + specimenOrder.SpecimenNumber + ": " + "Flow Markers";
					panelSetOrderCPTCode88184.CodeableType = "FlowMarkers";
					panelSetOrderCPTCode88184.EntryType = Business.Billing.Model.PanelSetOrderCPTCodeEntryType.SystemGenerated;
					panelSetOrderCPTCode88184.SpecimenOrderId = specimenOrder.SpecimenOrderId;
					panelSetOrderCPTCode88184.ClientId = this.m_AccessionOrder.ClientId;
                    panelSetOrderCPTCode88184.MedicalRecord = this.m_AccessionOrder.SvhMedicalRecord;
                    panelSetOrderCPTCode88184.Account = this.m_AccessionOrder.SvhAccount;

                    this.m_PanelSetOrder.PanelSetOrderCPTCodeCollection.Add(panelSetOrderCPTCode88184);

					int markerCount = panelSetOrder.FlowMarkerCollection.CountOfBillableMarkers();

                    YellowstonePathology.Business.Billing.Model.CptCode cpt88185 = Store.AppDataStore.Instance.CPTCodeCollection.GetClone("88185", null);
					YellowstonePathology.Business.Test.PanelSetOrderCPTCode panelSetOrderCPTCode88185 = this.m_PanelSetOrder.PanelSetOrderCPTCodeCollection.GetNextItem(this.m_PanelSetOrder.ReportNo);
					panelSetOrderCPTCode88185.Quantity = markerCount - 1;
                    panelSetOrderCPTCode88185.CPTCode = cpt88185.Code;
                    panelSetOrderCPTCode88185.CodeType = cpt88185.CodeType.ToString();
					panelSetOrderCPTCode88185.CodeableDescription = "Specimen " + specimenOrder.SpecimenNumber + ": " + "Flow Markers";
					panelSetOrderCPTCode88185.CodeableType = "FlowMarkers";
					panelSetOrderCPTCode88185.EntryType = Business.Billing.Model.PanelSetOrderCPTCodeEntryType.SystemGenerated;
					panelSetOrderCPTCode88185.SpecimenOrderId = specimenOrder.SpecimenOrderId;
					panelSetOrderCPTCode88185.ClientId = this.m_AccessionOrder.ClientId;
                    panelSetOrderCPTCode88185.MedicalRecord = this.m_AccessionOrder.SvhMedicalRecord;
                    panelSetOrderCPTCode88185.Account = this.m_AccessionOrder.SvhAccount;

                    this.m_PanelSetOrder.PanelSetOrderCPTCodeCollection.Add(panelSetOrderCPTCode88185);

					if (markerCount >= 2 && markerCount <= 8)
					{
                        YellowstonePathology.Business.Billing.Model.CptCode cpt88187 = Store.AppDataStore.Instance.CPTCodeCollection.GetClone("88187", null);
						YellowstonePathology.Business.Test.PanelSetOrderCPTCode panelSetOrderCPTCode88187 = this.m_PanelSetOrder.PanelSetOrderCPTCodeCollection.GetNextItem(this.m_PanelSetOrder.ReportNo);
						panelSetOrderCPTCode88187.Quantity = 1;
                        panelSetOrderCPTCode88187.CPTCode = cpt88187.Code;
                        panelSetOrderCPTCode88187.CodeType = cpt88187.CodeType.ToString();
						panelSetOrderCPTCode88187.CodeableDescription = "Specimen " + specimenOrder.SpecimenNumber + ": " + "Leukemia Lymphoma";
						panelSetOrderCPTCode88187.CodeableType = "Leukemia Lymphoma";
						panelSetOrderCPTCode88187.EntryType = Business.Billing.Model.PanelSetOrderCPTCodeEntryType.SystemGenerated;
						panelSetOrderCPTCode88187.SpecimenOrderId = specimenOrder.SpecimenOrderId;
						panelSetOrderCPTCode88187.ClientId = this.m_AccessionOrder.ClientId;
                        panelSetOrderCPTCode88187.MedicalRecord = this.m_AccessionOrder.SvhMedicalRecord;
                        panelSetOrderCPTCode88187.Account = this.m_AccessionOrder.SvhAccount;
                        this.m_PanelSetOrder.PanelSetOrderCPTCodeCollection.Add(panelSetOrderCPTCode88187);
					}
					else if (markerCount >= 9 && markerCount <= 15)
					{
                        YellowstonePathology.Business.Billing.Model.CptCode cpt88188 = Store.AppDataStore.Instance.CPTCodeCollection.GetClone("88188", null);
						YellowstonePathology.Business.Test.PanelSetOrderCPTCode panelSetOrderCPTCode88188 = this.m_PanelSetOrder.PanelSetOrderCPTCodeCollection.GetNextItem(this.m_PanelSetOrder.ReportNo);
						panelSetOrderCPTCode88188.Quantity = 1;
                        panelSetOrderCPTCode88188.CPTCode = cpt88188.Code;
                        panelSetOrderCPTCode88188.CodeType = cpt88188.CodeType.ToString();
						panelSetOrderCPTCode88188.CodeableDescription = "Specimen " + specimenOrder.SpecimenNumber + ": " + "Leukemia Lymphoma";
						panelSetOrderCPTCode88188.CodeableType = "Leukemia Lymphoma";
						panelSetOrderCPTCode88188.EntryType = Business.Billing.Model.PanelSetOrderCPTCodeEntryType.SystemGenerated;
						panelSetOrderCPTCode88188.SpecimenOrderId = specimenOrder.SpecimenOrderId;
						panelSetOrderCPTCode88188.ClientId = this.m_AccessionOrder.ClientId;
                        panelSetOrderCPTCode88188.MedicalRecord = this.m_AccessionOrder.SvhMedicalRecord;
                        panelSetOrderCPTCode88188.Account = this.m_AccessionOrder.SvhAccount;
                        this.m_PanelSetOrder.PanelSetOrderCPTCodeCollection.Add(panelSetOrderCPTCode88188);
					}
					else if (markerCount >= 16)
					{
                        YellowstonePathology.Business.Billing.Model.CptCode cpt88189 = Store.AppDataStore.Instance.CPTCodeCollection.GetClone("88189", null);
						YellowstonePathology.Business.Test.PanelSetOrderCPTCode panelSetOrderCPTCode88189 = this.m_PanelSetOrder.PanelSetOrderCPTCodeCollection.GetNextItem(this.m_PanelSetOrder.ReportNo);
						panelSetOrderCPTCode88189.Quantity = 1;
                        panelSetOrderCPTCode88189.CPTCode = cpt88189.Code;
                        panelSetOrderCPTCode88189.CodeType = cpt88189.CodeType.ToString();
						panelSetOrderCPTCode88189.CodeableDescription = "Specimen " + specimenOrder.SpecimenNumber + ": " + "Leukemia Lymphoma";
						panelSetOrderCPTCode88189.CodeableType = "Leukemia Lymphoma";
						panelSetOrderCPTCode88189.EntryType = Business.Billing.Model.PanelSetOrderCPTCodeEntryType.SystemGenerated;
						panelSetOrderCPTCode88189.SpecimenOrderId = specimenOrder.SpecimenOrderId;
						panelSetOrderCPTCode88189.ClientId = this.m_AccessionOrder.ClientId;
                        panelSetOrderCPTCode88189.MedicalRecord = this.m_AccessionOrder.SvhMedicalRecord;
                        panelSetOrderCPTCode88189.Account = this.m_AccessionOrder.SvhAccount;
                        this.m_PanelSetOrder.PanelSetOrderCPTCodeCollection.Add(panelSetOrderCPTCode88189);
					}
				}
                this.m_PanelSetOrder.PanelSetOrderCPTCodeCollection.UpdateCodeType();
            }                       
        }        
    }
}
