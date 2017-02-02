using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Billing.Model
{
    public class BillableObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
        protected YellowstonePathology.Business.Test.PanelSetOrder m_PanelSetOrder;		

        public BillableObject(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo)
        {
            this.m_AccessionOrder = accessionOrder;
            this.m_PanelSetOrder = accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(reportNo);			
        }

        public YellowstonePathology.Business.Test.PanelSetOrder PanelSetOrder
        {
            get { return this.m_PanelSetOrder; }
        }

        public virtual void SetBillingType()
        {
			YellowstonePathology.Business.Client.Model.Client client = YellowstonePathology.Business.Gateway.PhysicianClientGateway.GetClientByClientId(this.m_AccessionOrder.ClientId);
            YellowstonePathology.Business.Billing.Model.BillingRuleSet billingRuleSet = YellowstonePathology.Business.Billing.Model.BillingRuleSetCollection.GetRuleSetByRuleSetId(client.BillingRuleSetId2);
			YellowstonePathology.Business.Billing.Model.BillingTypeEnum billingType = billingRuleSet.GetBillingType(this.m_AccessionOrder.PatientType, this.m_AccessionOrder.PrimaryInsurance, this.m_AccessionOrder.SecondaryInsurance, this.m_PanelSetOrder.Ordered14DaysPostDischarge, this.m_PanelSetOrder.PanelSetId);
			this.m_PanelSetOrder.BillingType = billingType.ToString();			
        }        

        public void Unpost()
        {
            this.m_PanelSetOrder.PanelSetOrderCPTCodeBillCollection.Unpost();
            this.m_PanelSetOrder.PanelSetOrderCPTCodeCollection.Unpost();
            if (this.m_PanelSetOrder.PanelSetOrderCPTCodeBillCollection.PostedItemsExist() == false)
            {
                this.m_PanelSetOrder.IsPosted = false;                
            }
        }

        public void Unset()
        {
            this.m_PanelSetOrder.BillingType = null;
            this.m_PanelSetOrder.PanelSetOrderCPTCodeCollection.Unset();            
        }        		

        public virtual void SetPanelSetOrderCPTCodes()
        {
            if (this.IsOkToSet() == true)
            {
                YellowstonePathology.Business.PanelSet.Model.PanelSetCollection panelSetCollection = YellowstonePathology.Business.PanelSet.Model.PanelSetCollection.GetAll();
                YellowstonePathology.Business.PanelSet.Model.PanelSet panelSet = panelSetCollection.GetPanelSet(this.m_PanelSetOrder.PanelSetId);
				YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrderByOrderTarget(this.m_PanelSetOrder.OrderedOnId);

                foreach (YellowstonePathology.Business.Billing.Model.PanelSetCptCode panelSetCptCode in panelSet.PanelSetCptCodeCollection)
                {
                    if (this.m_PanelSetOrder.PanelSetOrderCPTCodeCollection.Exists(panelSetCptCode.CptCode.Code, panelSetCptCode.Quantity) == false)
                    {
                        YellowstonePathology.Business.Test.PanelSetOrderCPTCode panelSetOrderCPTCode = this.m_PanelSetOrder.PanelSetOrderCPTCodeCollection.GetNextItem(this.m_PanelSetOrder.ReportNo);
                        panelSetOrderCPTCode.Quantity = panelSetCptCode.Quantity;
                        panelSetOrderCPTCode.CPTCode = panelSetCptCode.CptCode.Code;
                        panelSetOrderCPTCode.Modifier = null;
						panelSetOrderCPTCode.CodeableDescription = "Specimen " + specimenOrder.SpecimenNumber + ": " + this.m_PanelSetOrder.PanelSetName;
                        panelSetOrderCPTCode.CodeableType = "BillableTest";
                        panelSetOrderCPTCode.EntryType = YellowstonePathology.Business.Billing.Model.PanelSetOrderCPTCodeEntryType.SystemGenerated;
						panelSetOrderCPTCode.SpecimenOrderId = specimenOrder.SpecimenOrderId;
						panelSetOrderCPTCode.ClientId = this.m_AccessionOrder.ClientId;
                        this.m_PanelSetOrder.PanelSetOrderCPTCodeCollection.Add(panelSetOrderCPTCode);
                    }
                }

                this.m_PanelSetOrder.PanelSetOrderCPTCodeCollection.UpdateCodeType();
            }
        }                  

        public virtual void PostGlobal(string billTo, string billby)
        {
			if (this.IsOkToPost() == true)
			{
                YellowstonePathology.Business.Billing.Model.CptCodeCollection cptCodeCollection = YellowstonePathology.Business.Billing.Model.CptCodeCollection.GetAll();
				YellowstonePathology.Business.Test.PanelSetOrderCPTCodeCollection panelSetOrderCPTCodeSummaryCollection = this.m_PanelSetOrder.PanelSetOrderCPTCodeCollection.GetSummaryCollection();
				foreach (YellowstonePathology.Business.Test.PanelSetOrderCPTCode panelSetOrderCPTCode in panelSetOrderCPTCodeSummaryCollection)
				{
                    YellowstonePathology.Business.Billing.Model.CptCode cptCode = cptCodeCollection.GetCptCode(panelSetOrderCPTCode.CPTCode);

					YellowstonePathology.Business.Test.PanelSetOrderCPTCodeBill item = this.m_PanelSetOrder.PanelSetOrderCPTCodeBillCollection.GetNextItem(this.m_PanelSetOrder.ReportNo);
					item.FromPanelSetOrderCPTCode(panelSetOrderCPTCode);
					item.BillTo = billTo;
					item.BillBy = billby;

                    if (this.m_AccessionOrder.PrimaryInsurance == "Medicare")
                    {
                        if (cptCode.HasMedicareQuantityLimit == true)
                        {
                            item.Quantity = cptCode.MedicareQuantityLimit;
                        }
                    }

					this.m_PanelSetOrder.PanelSetOrderCPTCodeBillCollection.Add(item);
				}
			}
        }

        public virtual void PostProfessional(string billTo, string billby)
        {
			if (this.IsOkToPost() == true)
			{
				YellowstonePathology.Business.Billing.Model.CptCodeCollection cptCodeCollection = YellowstonePathology.Business.Billing.Model.CptCodeCollection.GetAll();
				YellowstonePathology.Business.Test.PanelSetOrderCPTCodeCollection panelSetOrderCPTCodeSummaryCollection = this.m_PanelSetOrder.PanelSetOrderCPTCodeCollection.GetSummaryCollection();

				foreach (YellowstonePathology.Business.Test.PanelSetOrderCPTCode panelSetOrderCPTCode in panelSetOrderCPTCodeSummaryCollection)
				{
					YellowstonePathology.Business.Billing.Model.CptCode cptCode = cptCodeCollection.GetCptCode(panelSetOrderCPTCode.CPTCode);
					if (cptCode.HasProfessionalComponent == true)
					{
						YellowstonePathology.Business.Test.PanelSetOrderCPTCodeBill item = this.m_PanelSetOrder.PanelSetOrderCPTCodeBillCollection.GetNextItem(this.m_PanelSetOrder.ReportNo);
						item.FromPanelSetOrderCPTCode(panelSetOrderCPTCode);
						item.BillTo = billTo;
						item.BillBy = billby;
						item.Modifier = cptCode.GetModifier(YellowstonePathology.Business.Billing.Model.BillingComponentEnum.Professional);

                        if (this.m_AccessionOrder.PrimaryInsurance == "Medicare")
                        {
                            if (cptCode.HasMedicareQuantityLimit == true)
                            {
                                item.Quantity = cptCode.MedicareQuantityLimit;
                            }
                        }

						this.m_PanelSetOrder.PanelSetOrderCPTCodeBillCollection.Add(item);
					}
				}
			}
        }

        public virtual void PostTechnical(string billTo, string billby)
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
						item.Modifier = cptCode.GetModifier(YellowstonePathology.Business.Billing.Model.BillingComponentEnum.Technical);

                        if (this.m_AccessionOrder.PrimaryInsurance == "Medicare" && cptCode.HasMedicareQuantityLimit == true && billTo == "Patient")
                        {                                                        
                            item.Quantity = cptCode.MedicareQuantityLimit;                            
                        }
                        else
                        {
                            item.Quantity = panelSetOrderCPTCode.Quantity;
                        }

						this.m_PanelSetOrder.PanelSetOrderCPTCodeBillCollection.Add(item);
					}
				}
			}
        }

		public virtual void PostClientGCodes(YellowstonePathology.Business.Billing.Model.BillingComponentEnum billingComponent)
		{
			//do nothing
		}

        public void PostManualEntriesGlobal(string billTo, string billBy)
        {
			if (this.IsOkToPost() == true)
			{
				YellowstonePathology.Business.Billing.Model.CptCodeCollection cptCodeCollection = YellowstonePathology.Business.Billing.Model.CptCodeCollection.GetAll();
				YellowstonePathology.Business.Test.PanelSetOrderCPTCodeCollection panelSetOrderCPTCodeSummaryCollection = this.m_PanelSetOrder.PanelSetOrderCPTCodeCollection.GetManualEntrySummaryCollection();

				foreach (YellowstonePathology.Business.Test.PanelSetOrderCPTCode panelSetOrderCPTCode in panelSetOrderCPTCodeSummaryCollection)
				{
                    YellowstonePathology.Business.Billing.Model.CptCode cptCode = cptCodeCollection.GetNewInstance(panelSetOrderCPTCode.CPTCode, panelSetOrderCPTCode.Modifier);
                    if (cptCode.IsBillable == true)
                    {
                        YellowstonePathology.Business.Test.PanelSetOrderCPTCodeBill item = this.m_PanelSetOrder.PanelSetOrderCPTCodeBillCollection.GetNextItem(this.m_PanelSetOrder.ReportNo);
                        item.FromPanelSetOrderCPTCode(panelSetOrderCPTCode);
                        item.ClientId = this.m_AccessionOrder.ClientId;
                        item.BillTo = billTo;
                        item.BillBy = billBy;
                        item.Modifier = panelSetOrderCPTCode.Modifier;                        

                        if (this.m_AccessionOrder.PrimaryInsurance == "Medicare")
                        {
                            if (cptCode.HasMedicareQuantityLimit == true)
                            {
                                item.Quantity = cptCode.MedicareQuantityLimit;
                            }
                        }

                        this.m_PanelSetOrder.PanelSetOrderCPTCodeBillCollection.Add(item);
                    }
				}
			}
        }

        public void PostManualEntriesProfessional(string billTo, string billBy)
        {
			if (this.IsOkToPost() == true)
			{
				YellowstonePathology.Business.Billing.Model.CptCodeCollection cptCodeCollection = YellowstonePathology.Business.Billing.Model.CptCodeCollection.GetAll();
				YellowstonePathology.Business.Test.PanelSetOrderCPTCodeCollection panelSetOrderCPTCodeSummaryCollection = this.m_PanelSetOrder.PanelSetOrderCPTCodeCollection.GetManualEntrySummaryCollection();

				foreach (YellowstonePathology.Business.Test.PanelSetOrderCPTCode panelSetOrderCPTCode in panelSetOrderCPTCodeSummaryCollection)
				{                    
					YellowstonePathology.Business.Billing.Model.CptCode cptCode = cptCodeCollection.GetNewInstance(panelSetOrderCPTCode.CPTCode, panelSetOrderCPTCode.Modifier);
                    if (cptCode.HasBillableProfessionalComponent() == true)
                    {                        
                        YellowstonePathology.Business.Test.PanelSetOrderCPTCodeBill item = this.m_PanelSetOrder.PanelSetOrderCPTCodeBillCollection.GetNextItem(this.m_PanelSetOrder.ReportNo);
                        item.FromPanelSetOrderCPTCode(panelSetOrderCPTCode);
                        item.ClientId = this.m_AccessionOrder.ClientId;
                        item.BillTo = billTo;
                        item.BillBy = billBy;
                        item.Modifier = cptCode.GetModifier(YellowstonePathology.Business.Billing.Model.BillingComponentEnum.Professional);

                        if (this.m_AccessionOrder.PrimaryInsurance == "Medicare")
                        {
                            if (cptCode.HasMedicareQuantityLimit == true)
                            {
                                item.Quantity = cptCode.MedicareQuantityLimit;
                            }
                        }

                        this.m_PanelSetOrder.PanelSetOrderCPTCodeBillCollection.Add(item);                        
                    }
				}
			}
        }

        public void PostManualEntriesTechnical(string billTo, string billBy)
        {
			if (this.IsOkToPost() == true)
			{
				YellowstonePathology.Business.Billing.Model.CptCodeCollection cptCodeCollection = YellowstonePathology.Business.Billing.Model.CptCodeCollection.GetAll();
				YellowstonePathology.Business.Test.PanelSetOrderCPTCodeCollection panelSetOrderCPTCodeSummaryCollection = this.m_PanelSetOrder.PanelSetOrderCPTCodeCollection.GetManualEntrySummaryCollection();

				foreach (YellowstonePathology.Business.Test.PanelSetOrderCPTCode panelSetOrderCPTCode in panelSetOrderCPTCodeSummaryCollection)
				{
                    YellowstonePathology.Business.Billing.Model.CptCode cptCode = cptCodeCollection.GetNewInstance(panelSetOrderCPTCode.CPTCode, panelSetOrderCPTCode.Modifier);
					if (cptCode.HasBillableTechnicalComponent() == true)
					{
						YellowstonePathology.Business.Test.PanelSetOrderCPTCodeBill item = this.m_PanelSetOrder.PanelSetOrderCPTCodeBillCollection.GetNextItem(this.m_PanelSetOrder.ReportNo);
						item.FromPanelSetOrderCPTCode(panelSetOrderCPTCode);
						item.ClientId = this.m_AccessionOrder.ClientId;
						item.BillTo = billTo;
						item.BillBy = billBy;
						item.Modifier = cptCode.GetModifier(YellowstonePathology.Business.Billing.Model.BillingComponentEnum.Technical);                        

                        if (this.m_AccessionOrder.PrimaryInsurance == "Medicare")
                        {
                            if (cptCode.HasMedicareQuantityLimit == true)
                            {
                                item.Quantity = cptCode.MedicareQuantityLimit;
                            }
                        }

						this.m_PanelSetOrder.PanelSetOrderCPTCodeBillCollection.Add(item);
					}
				}
			}
        }


        public YellowstonePathology.Business.Rules.MethodResult Set()
        {
            YellowstonePathology.Business.Rules.MethodResult methodResult = new Rules.MethodResult();
            if (this.m_PanelSetOrder.HoldBilling == false)
            {
                this.HandleUseBillingAgent();
                this.SetBillingType();
                this.SetPanelSetOrderCPTCodes();                
                methodResult.Success = true;
            }
            else
            {
                methodResult.Success = false;
                methodResult.Message = "This case cannot be set because it is on hold.";
            }
            return methodResult;
        }

        private void HandleUseBillingAgent()
        {
            YellowstonePathology.Business.Billing.Model.FeeScheduleCosmetic feeScheduleCosmetic = new YellowstonePathology.Business.Billing.Model.FeeScheduleCosmetic();
            if (this.m_AccessionOrder.FeeSchedule == feeScheduleCosmetic.Name)
            {
                this.m_AccessionOrder.UseBillingAgent = false;
            }
            else
            {
                this.m_AccessionOrder.UseBillingAgent = true;
            }
        }

        public YellowstonePathology.Business.Rules.MethodResult Post()
        {
            YellowstonePathology.Business.Rules.MethodResult methodResult = new Rules.MethodResult();
            if (this.m_PanelSetOrder.HoldBilling == false)
            {
                this.PostPQRICodes();

                BillingComponent billingComponent = BillingComponent.GetBillingComponent(this.m_PanelSetOrder);
                billingComponent.Post(this);

                this.m_PanelSetOrder.PanelSetOrderCPTCodeBillCollection.SetPostDate(DateTime.Today);
                this.m_PanelSetOrder.PanelSetOrderCPTCodeCollection.SetPostDate(DateTime.Today);
                this.m_PanelSetOrder.IsPosted = true;                
                methodResult.Success = true;
            }
            else
            {
                methodResult.Success = false;
                methodResult.Message = "This case cannot be posted because it is on hold.";
            }
            return methodResult;
        }        
        
        public void PostPQRICodes()
        {
			if (this.IsOkToPost() == true)
			{
				YellowstonePathology.Business.Billing.Model.CptCodeCollection cptCodeCollection = YellowstonePathology.Business.Billing.Model.CptCodeCollection.GetAll();
				foreach (YellowstonePathology.Business.Test.PanelSetOrderCPTCode panelSetOrderCPTCode in this.m_PanelSetOrder.PanelSetOrderCPTCodeCollection)
				{
					YellowstonePathology.Business.Billing.Model.CptCode cptCode = cptCodeCollection.GetCptCode(panelSetOrderCPTCode.CPTCode);
					if (cptCode is YellowstonePathology.Business.Billing.Model.PQRSCode == true)
					{
						YellowstonePathology.Business.Test.PanelSetOrderCPTCodeBill pqriCode = this.m_PanelSetOrder.PanelSetOrderCPTCodeBillCollection.GetNextItem(this.m_PanelSetOrder.ReportNo);
						pqriCode.ClientId = this.m_AccessionOrder.ClientId;
						pqriCode.FromPanelSetOrderCPTCode(panelSetOrderCPTCode);
						pqriCode.BillTo = null;
						pqriCode.BillBy = null;
						this.m_PanelSetOrder.PanelSetOrderCPTCodeBillCollection.Add(pqriCode);
					}
				}
			}
        }

		public bool IsOkToSet()
		{
			bool result = true;
			if (this.m_PanelSetOrder.Final == false)
			{
				result = false;
			}
			return result;
		}

        public bool IsOkToPost()
        {
            bool result = true;
            if (this.m_PanelSetOrder.Final == false)
            {
                result = false;
            }
            else if (this.m_PanelSetOrder.NoCharge == true)
            {
                result = false;
            }
            return result;
        }

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }        
    }
}
