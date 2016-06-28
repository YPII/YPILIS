using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Billing.Model
{
    public class BillableObjectMountainViewNeo : BillableObject
    {
        public BillableObjectMountainViewNeo(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo) : base(accessionOrder, reportNo)
        {
            
        }

        public override void SetBillingType()
        {
            this.m_PanelSetOrder.BillingType = "Split";
        }

        public override void PostTechnical(string billTo, string billBy)
        {
            if (this.IsOkToPost() == true)
            {
                YellowstonePathology.Business.PanelSet.Model.PanelSetCollection allPanelSets = YellowstonePathology.Business.PanelSet.Model.PanelSetCollection.GetAll();
                YellowstonePathology.Business.PanelSet.Model.PanelSet panelSet = allPanelSets.GetPanelSet(this.m_PanelSetOrder.PanelSetId);

                YellowstonePathology.Business.Billing.Model.CptCodeCollection cptCodeCollection = YellowstonePathology.Business.Billing.Model.CptCodeCollection.GetAll();
                YellowstonePathology.Business.Test.PanelSetOrderCPTCodeCollection panelSetOrderCPTCodeSummaryCollection = this.m_PanelSetOrder.PanelSetOrderCPTCodeCollection.GetSummaryCollection();

                foreach (YellowstonePathology.Business.Test.PanelSetOrderCPTCode panelSetOrderCPTCode in panelSetOrderCPTCodeSummaryCollection)
                {
                    YellowstonePathology.Business.Billing.Model.CptCode cptCode = cptCodeCollection.GetCptCode(panelSetOrderCPTCode.CPTCode);
                    if (cptCode.HasTechnicalComponent == true)
                    {
                        YellowstonePathology.Business.Test.PanelSetOrderCPTCodeBill item = this.m_PanelSetOrder.PanelSetOrderCPTCodeBillCollection.GetNextItem(this.m_PanelSetOrder.ReportNo);
                        item.FromPanelSetOrderCPTCode(panelSetOrderCPTCode);
                        item.BillTo = "Client";
                        item.BillBy = "YPIBLGS";
                        item.Modifier = cptCode.GetModifier(YellowstonePathology.Business.Billing.Model.BillingComponentEnum.Technical);
                        this.m_PanelSetOrder.PanelSetOrderCPTCodeBillCollection.Add(item);
                    }
                }                
            }
        }
    }
}
