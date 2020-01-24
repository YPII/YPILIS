using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Billing.Model
{
    public class BillableObjectNeoFlowCytometry : BillableObject
    {        
        //YellowstonePathology.Business.Billing.Model.PanelSetCptCodeCollection m_PanelSetCptCodeCollection;

        public BillableObjectNeoFlowCytometry(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo) 
            : base(accessionOrder, reportNo)
        {
                   
        }

        public override void PostGlobal(string billTo, string billBy)
        {
			if (this.IsOkToPost() == true)
			{
				YellowstonePathology.Business.Test.PanelSetOrderCPTCodeCollection panelSetOrderCPTCodeSummaryCollection = this.m_PanelSetOrder.PanelSetOrderCPTCodeCollection.GetSummaryCollection();

                foreach (YellowstonePathology.Business.Test.PanelSetOrderCPTCode panelSetOrderCPTCode in panelSetOrderCPTCodeSummaryCollection)
				{
					YellowstonePathology.Business.Test.PanelSetOrderCPTCodeBill item = this.m_PanelSetOrder.PanelSetOrderCPTCodeBillCollection.GetNextItem(this.m_PanelSetOrder.ReportNo);
					item.FromPanelSetOrderCPTCode(panelSetOrderCPTCode);
					item.BillTo = billTo;
					item.BillBy = billBy;
					this.m_PanelSetOrder.PanelSetOrderCPTCodeBillCollection.Add(item);
				}
			}
        }

		public override void PostTechnical(string billTo, string billby)
		{
			if (this.IsOkToPost() == true)
			{
				YellowstonePathology.Business.Test.PanelSetOrderCPTCodeCollection panelSetOrderCPTCodeSummaryCollection = this.m_PanelSetOrder.PanelSetOrderCPTCodeCollection.GetSummaryCollection();

                foreach (YellowstonePathology.Business.Test.PanelSetOrderCPTCode panelSetOrderCPTCode in panelSetOrderCPTCodeSummaryCollection)
				{
					YellowstonePathology.Business.Billing.Model.CptCode cptCode = Store.AppDataStore.Instance.CPTCodeCollection.GetClone(panelSetOrderCPTCode.CPTCode, panelSetOrderCPTCode.Modifier);
					if (cptCode.HasTechnicalComponent == true)
					{
						YellowstonePathology.Business.Test.PanelSetOrderCPTCodeBill item = this.m_PanelSetOrder.PanelSetOrderCPTCodeBillCollection.GetNextItem(this.m_PanelSetOrder.ReportNo);
						item.FromPanelSetOrderCPTCode(panelSetOrderCPTCode);
						item.BillTo = billTo;
						item.BillBy = billby;
						item.Modifier = null;
						this.m_PanelSetOrder.PanelSetOrderCPTCodeBillCollection.Add(item);
					}
				}
			}
		}
	}
}
