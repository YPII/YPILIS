using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Billing.Model
{
    public class BillableObjectInHouseMolecular : BillableObject
    {
        public BillableObjectInHouseMolecular(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo) 
            : base(accessionOrder, reportNo)
        {
            
        }

		public override void SetPanelSetOrderCPTCodes()
		{
			if (this.IsOkToSet() == true)
			{
				YellowstonePathology.Business.PanelSet.Model.PanelSetCollection panelSetCollection = YellowstonePathology.Business.PanelSet.Model.PanelSetCollection.GetAll();
				YellowstonePathology.Business.PanelSet.Model.PanelSet panelSet = panelSetCollection.GetPanelSet(this.m_PanelSetOrder.PanelSetId);
				YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrderByOrderTarget(this.m_PanelSetOrder.OrderedOnId);

				foreach (YellowstonePathology.Business.Billing.Model.PanelSetCptCode panelSetCptCode in panelSet.PanelSetCptCodeCollection)
				{
					if (panelSetCptCode.CptCode.IsBillable == true)
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
		}

        public override void PostGlobal(string billTo, string billBy)
        {
			if (this.IsOkToPost() == true)
			{
				YellowstonePathology.Business.PanelSet.Model.PanelSetCollection allPanelSets = YellowstonePathology.Business.PanelSet.Model.PanelSetCollection.GetAll();
				YellowstonePathology.Business.PanelSet.Model.PanelSetMolecularTest panelSet = (YellowstonePathology.Business.PanelSet.Model.PanelSetMolecularTest)allPanelSets.GetPanelSet(this.m_PanelSetOrder.PanelSetId);

				if (panelSet.HasSplitCPTCode == true)
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
						bill26.BillBy = YellowstonePathology.Business.Facility.Model.FacilityCollection.GetBillBy(m_PanelSetOrder.ProfessionalComponentBillingFacilityId, this.m_PanelSetOrder.TechnicalComponentBillingFacilityId, "Global", billTo);
						bill26.Modifier = "26";
						this.m_PanelSetOrder.PanelSetOrderCPTCodeBillCollection.Add(bill26);
					}
				}
				else
				{
					base.PostGlobal(billTo, billBy);
				}
			}
        }

		public override void PostTechnical(string billTo, string billBy)
		{
			if (this.IsOkToPost() == true)
			{
                YellowstonePathology.Business.PanelSet.Model.PanelSetCollection allPanelSets = YellowstonePathology.Business.PanelSet.Model.PanelSetCollection.GetAll();
                YellowstonePathology.Business.PanelSet.Model.PanelSetMolecularTest panelSet = (YellowstonePathology.Business.PanelSet.Model.PanelSetMolecularTest)allPanelSets.GetPanelSet(this.m_PanelSetOrder.PanelSetId);

				YellowstonePathology.Business.Billing.Model.CptCodeCollection cptCodeCollection = YellowstonePathology.Business.Billing.Model.CptCodeCollection.GetAll();
				YellowstonePathology.Business.Test.PanelSetOrderCPTCodeCollection panelSetOrderCPTCodeSummaryCollection = this.m_PanelSetOrder.PanelSetOrderCPTCodeCollection.GetSummaryCollection();

                if (panelSet.HasSplitCPTCode == true)
                {
                    foreach (YellowstonePathology.Business.Test.PanelSetOrderCPTCode panelSetOrderCPTCode in panelSetOrderCPTCodeSummaryCollection)
                    {
                        YellowstonePathology.Business.Billing.Model.CptCode cptCode = cptCodeCollection.GetCptCode(panelSetOrderCPTCode.CPTCode);
                        if (cptCode.HasTechnicalComponent == true)
                        {
                            YellowstonePathology.Business.Test.PanelSetOrderCPTCodeBill item = this.m_PanelSetOrder.PanelSetOrderCPTCodeBillCollection.GetNextItem(this.m_PanelSetOrder.ReportNo);
                            item.FromPanelSetOrderCPTCode(panelSetOrderCPTCode);
                            item.BillTo = billTo;
                            item.BillBy = billBy;
                            item.Modifier = null;
                            this.m_PanelSetOrder.PanelSetOrderCPTCodeBillCollection.Add(item);
                        }
                    }
                }
                else
                {
                    base.PostTechnical(billTo, billBy);
                }
			}
		}

        public override void PostProfessional(string billTo, string billBy)
        {
            if (this.IsOkToPost() == true)
            {
                YellowstonePathology.Business.PanelSet.Model.PanelSetCollection allPanelSets = YellowstonePathology.Business.PanelSet.Model.PanelSetCollection.GetAll();
                YellowstonePathology.Business.PanelSet.Model.PanelSetMolecularTest panelSet = (YellowstonePathology.Business.PanelSet.Model.PanelSetMolecularTest)allPanelSets.GetPanelSet(this.m_PanelSetOrder.PanelSetId);

                YellowstonePathology.Business.Billing.Model.CptCodeCollection cptCodeCollection = YellowstonePathology.Business.Billing.Model.CptCodeCollection.GetAll();
                YellowstonePathology.Business.Test.PanelSetOrderCPTCodeCollection panelSetOrderCPTCodeSummaryCollection = this.m_PanelSetOrder.PanelSetOrderCPTCodeCollection.GetSummaryCollection();

                if (panelSet.HasSplitCPTCode == true)
                {
                    foreach (YellowstonePathology.Business.Test.PanelSetOrderCPTCode panelSetOrderCPTCode in panelSetOrderCPTCodeSummaryCollection)
                    {
                        YellowstonePathology.Business.Billing.Model.CptCode cptCode = cptCodeCollection.GetCptCode(panelSetOrderCPTCode.CPTCode);
                        if (cptCode.HasProfessionalComponent == true)
                        {
                            YellowstonePathology.Business.Test.PanelSetOrderCPTCodeBill item = this.m_PanelSetOrder.PanelSetOrderCPTCodeBillCollection.GetNextItem(this.m_PanelSetOrder.ReportNo);
                            item.FromPanelSetOrderCPTCode(panelSetOrderCPTCode);
                            item.BillTo = billTo;
                            item.BillBy = billBy;
                            item.Modifier = "26";
                            this.m_PanelSetOrder.PanelSetOrderCPTCodeBillCollection.Add(item);
                        }
                    }
                }
                else
                {
                    base.PostProfessional(billTo, billBy);
                }
            }
        }
	}
}
