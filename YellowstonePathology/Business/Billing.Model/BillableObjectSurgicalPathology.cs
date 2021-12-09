using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Billing.Model
{
    public class BillableObjectSurgicalPathology : BillableObject
    {
        public BillableObjectSurgicalPathology(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo) 
            : base(accessionOrder, reportNo)
        {
            
        }        

        public override void SetPanelSetOrderCPTCodes()
        {
            BillableObjectStains billableObjectStains = new BillableObjectStains(this.m_AccessionOrder, this.m_PanelSetOrder.ReportNo);
            billableObjectStains.SetPanelSetOrderCPTCodes();
            this.HandleSetSCLHealthProstateCode();
            this.m_PanelSetOrder.PanelSetOrderCPTCodeCollection.UpdateCodeType();
        }                 

        public override void PostTechnical(string billTo, string billBy)
        {
            base.PostManualEntriesTechnical(billTo, billBy);
			BillableObjectStains billableObjectStains = new BillableObjectStains(this.m_AccessionOrder, this.m_PanelSetOrder.ReportNo);
			billableObjectStains.PostTechnical(billTo, billBy);
        }

        public override void PostProfessional(string billTo, string billBy)
        {
            base.PostManualEntriesProfessional(billTo, billBy);
            BillableObjectStains billableObjectStains = new BillableObjectStains(this.m_AccessionOrder, this.m_PanelSetOrder.ReportNo);
			billableObjectStains.PostProfessional(billTo, billBy);
        }

        public override void PostGlobal(string billTo, string billBy)
        {
            base.PostManualEntriesGlobal(billTo, billBy);
            BillableObjectStains billableObjectStains = new BillableObjectStains(this.m_AccessionOrder, this.m_PanelSetOrder.ReportNo);
			billableObjectStains.PostGlobal(billTo, billBy);            
        }

        public void HandleSetSCLHealthProstateCode()
        {
            List<string> clientGroupIds = new List<string>();
            clientGroupIds.Add("1");
            clientGroupIds.Add("2");
            YellowstonePathology.Business.Client.Model.ClientGroupClientCollection sclClients = Business.Gateway.PhysicianClientGateway.GetClientGroupClientCollectionByClientGroupId(clientGroupIds);
            if(sclClients.ClientIdExists(this.m_AccessionOrder.ClientId) == true)
            {
                if(this.m_PanelSetOrder.PanelSetId == 13)
                {
                    Rules.Surgical.WordSearchList wordSearchList = new Rules.Surgical.WordSearchList();
                    wordSearchList.Add(new Rules.Surgical.WordSearchListItem("prostate", true, "Found"));
                    if(this.m_AccessionOrder.SpecimenOrderCollection.FindWordsInDescription(wordSearchList) == true)
                    {
                        YellowstonePathology.Business.Billing.Model.CptCode cptCode = Store.AppDataStore.Instance.CPTCodeCollection.GetClone("G0416", null);
                        if (this.m_PanelSetOrder.PanelSetOrderCPTCodeCollection.Exists(cptCode.Code, 1) == false)
                        {
                            YellowstonePathology.Business.Test.PanelSetOrderCPTCode panelSetOrderCPTCode = this.m_PanelSetOrder.PanelSetOrderCPTCodeCollection.GetNextItem(this.m_PanelSetOrder.ReportNo);
                            panelSetOrderCPTCode.Quantity = 1;
                            panelSetOrderCPTCode.CPTCode = cptCode.Code;
                            panelSetOrderCPTCode.Modifier = cptCode.Modifier;
                            panelSetOrderCPTCode.CodeableDescription = "Manual Entry";
                            panelSetOrderCPTCode.CodeableType = "BillableTest";
                            panelSetOrderCPTCode.EntryType = Business.Billing.Model.PanelSetOrderCPTCodeEntryType.SystemGenerated;                            
                            panelSetOrderCPTCode.ClientId = this.m_AccessionOrder.ClientId;
                            panelSetOrderCPTCode.MedicalRecord = this.m_AccessionOrder.SvhMedicalRecord;
                            panelSetOrderCPTCode.Account = this.m_AccessionOrder.SvhAccount;
                            this.m_PanelSetOrder.PanelSetOrderCPTCodeCollection.Add(panelSetOrderCPTCode);
                        }
                    }
                }
            }
        }       
    }
}
