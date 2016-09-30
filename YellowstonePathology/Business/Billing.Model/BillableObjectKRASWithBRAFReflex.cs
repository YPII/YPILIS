using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Billing.Model
{
    public class BillableObjectKRASWithBRAFReflex : BillableObject
    {        
        YellowstonePathology.Business.Billing.Model.PanelSetCptCodeCollection m_PanelSetCptCodeCollection;

        public BillableObjectKRASWithBRAFReflex(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo) 
            : base(accessionOrder, reportNo)
        {
            this.m_PanelSetCptCodeCollection = new YellowstonePathology.Business.Billing.Model.PanelSetCptCodeCollection();
            YellowstonePathology.Business.Billing.Model.PanelSetCptCode panelSetCptCodeCPT81275 = new YellowstonePathology.Business.Billing.Model.PanelSetCptCode(new YellowstonePathology.Business.Billing.Model.CptCodeDefinition.CPT81275(), 1);
            this.m_PanelSetCptCodeCollection.Add(panelSetCptCodeCPT81275);

            if (this.m_PanelSetOrder.PanelOrderCollection.PanelIdExists(24) == true)
            {                
                YellowstonePathology.Business.Billing.Model.PanelSetCptCode panelSetCptCodeCPT81210 = new YellowstonePathology.Business.Billing.Model.PanelSetCptCode(new YellowstonePathology.Business.Billing.Model.CptCodeDefinition.CPT81210(), 1);
                this.m_PanelSetCptCodeCollection.Add(panelSetCptCodeCPT81210);   
            }            
        }

        public override void SetPanelSetOrderCPTCodes()
        {
			if (this.IsOkToSet() == true)
			{
				YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrderByOrderTarget(this.m_PanelSetOrder.OrderedOnId);

				foreach (YellowstonePathology.Business.Billing.Model.PanelSetCptCode panelSetCptCode in this.m_PanelSetCptCodeCollection)
				{
					if (this.m_PanelSetOrder.PanelSetOrderCPTCodeCollection.Exists(panelSetCptCode.CptCode.Code, panelSetCptCode.Quantity) == false)
					{
						YellowstonePathology.Business.Test.PanelSetOrderCPTCode panelSetOrderCPTCode = this.m_PanelSetOrder.PanelSetOrderCPTCodeCollection.GetNextItem(this.m_PanelSetOrder.ReportNo);
						panelSetOrderCPTCode.Quantity = panelSetCptCode.Quantity;
						panelSetOrderCPTCode.CPTCode = panelSetCptCode.CptCode.Code;
                        panelSetOrderCPTCode.CodeType = panelSetCptCode.CptCode.CodeType.ToString();
						panelSetOrderCPTCode.Modifier = null;
						panelSetOrderCPTCode.CodeableDescription = "Specimen " + specimenOrder.SpecimenNumber + ": " + this.m_PanelSetOrder.PanelSetName;
						panelSetOrderCPTCode.CodeableType = "BillableTest";
						panelSetOrderCPTCode.EntryType = YellowstonePathology.Business.Billing.Model.PanelSetOrderCPTCodeEntryType.SystemGenerated;
						panelSetOrderCPTCode.SpecimenOrderId = specimenOrder.SpecimenOrderId;
						panelSetOrderCPTCode.ClientId = this.m_AccessionOrder.ClientId;
						this.m_PanelSetOrder.PanelSetOrderCPTCodeCollection.Add(panelSetOrderCPTCode);
					}
				}
			}
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

					YellowstonePathology.Business.Test.PanelSetOrderCPTCodeBill bill26 = this.m_PanelSetOrder.PanelSetOrderCPTCodeBillCollection.GetNextItem(this.m_PanelSetOrder.ReportNo);
					bill26.FromPanelSetOrderCPTCode(panelSetOrderCPTCode);
					bill26.BillTo = billTo;
					bill26.BillBy = billBy;
					bill26.Modifier = "26";
					this.m_PanelSetOrder.PanelSetOrderCPTCodeBillCollection.Add(bill26);
				}
			}
        }

		public override void PostTechnical(string billTo, string billby)
		{
			if (this.IsOkToPost() == true)
			{
				YellowstonePathology.Business.Billing.Model.CptCodeCollection cptCodeCollection = YellowstonePathology.Business.Billing.Model.CptCodeCollection.GetAll();
				YellowstonePathology.Business.Test.PanelSetOrderCPTCodeCollection panelSetOrderCPTCodeSummaryCollection = this.m_PanelSetOrder.PanelSetOrderCPTCodeCollection.GetSummaryCollection();

				foreach (YellowstonePathology.Business.Test.PanelSetOrderCPTCode panelSetOrderCPTCode in panelSetOrderCPTCodeSummaryCollection)
				{
					YellowstonePathology.Business.Billing.Model.CptCode cptCode = cptCodeCollection.GetCptCode(panelSetOrderCPTCode.CPTCode);
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
