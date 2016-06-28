using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YellowstonePathology.Business.Audit.Model;
using YellowstonePathology.Business.Persistence;
using YellowstonePathology.Business.Rules;

namespace YellowstonePathology.Business.Test.ErPrSemiQuantitative
{
	[PersistentClass("tblErPrSemiQuantitativeTestOrder", "tblPanelSetOrder", "YPIDATA")]
	public class ErPrSemiQuantitativeTestOrder : YellowstonePathology.Business.Test.PanelSetOrder
	{
		private string m_ErResult;
		private string m_ErIntensity;
		private string m_ErPercentageOfCells;
		private string m_PrResult;
		private string m_PrIntensity;
		private string m_PrPercentageOfCells;
		private string m_SpecimenAdequacy;
		private string m_InternalControls;
		private string m_ExternalControls;
		private string m_Interpretation;
		private string m_ReportReferences;
		private string m_ResultComment;
		private string m_SpecimenSiteAndType;
		private string m_SpecimenIdentification;
		private string m_Method;

		public ErPrSemiQuantitativeTestOrder()
		{

		}

		public ErPrSemiQuantitativeTestOrder(string masterAccessionNo, string reportNo, string objectId,
			YellowstonePathology.Business.PanelSet.Model.PanelSet panelSet,
            YellowstonePathology.Business.Interface.IOrderTarget orderTarget,
			bool distribute)
			: base(masterAccessionNo, reportNo, objectId, panelSet, orderTarget, distribute)
		{
			
		}        

		[PersistentProperty()]
		public string ErResult
		{
			get { return this.m_ErResult; }
			set
			{
				if (this.m_ErResult != value)
				{
					this.m_ErResult = value;
					this.NotifyPropertyChanged("ErResult");
				}
			}
		}

		[PersistentProperty()]
		public string ErIntensity
		{
			get { return this.m_ErIntensity; }
			set
			{
				if (this.m_ErIntensity != value)
				{
					this.m_ErIntensity = value;
					this.NotifyPropertyChanged("ErIntensity");
				}
			}
		}

		[PersistentProperty()]
		public string ErPercentageOfCells
		{
			get { return this.m_ErPercentageOfCells; }
			set
			{
				if (this.m_ErPercentageOfCells != value)
				{
					this.m_ErPercentageOfCells = value;
					this.NotifyPropertyChanged("ErPercentageOfCells");
				}
			}
		}

		[PersistentProperty()]
		public string PrResult
		{
			get { return this.m_PrResult; }
			set
			{
				if (this.m_PrResult != value)
				{
					this.m_PrResult = value;
					this.NotifyPropertyChanged("PrResult");
				}
			}
		}

		[PersistentProperty()]
		public string PrIntensity
		{
			get { return this.m_PrIntensity; }
			set
			{
				if (this.m_PrIntensity != value)
				{
					this.m_PrIntensity = value;
					this.NotifyPropertyChanged("PrIntensity");
				}
			}
		}

		[PersistentProperty()]
		public string PrPercentageOfCells
		{
			get { return this.m_PrPercentageOfCells; }
			set
			{
				if (this.m_PrPercentageOfCells != value)
				{
					this.m_PrPercentageOfCells = value;
					this.NotifyPropertyChanged("PrPercentageOfCells");
				}
			}
		}

		[PersistentProperty()]
		public string SpecimenAdequacy
		{
			get { return this.m_SpecimenAdequacy; }
			set
			{
				if (this.m_SpecimenAdequacy != value)
				{
					this.m_SpecimenAdequacy = value;
					this.NotifyPropertyChanged("SpecimenAdequacy");
				}
			}
		}

		[PersistentProperty()]
		public string InternalControls
		{
			get { return this.m_InternalControls; }
			set
			{
				if (this.m_InternalControls != value)
				{
					this.m_InternalControls = value;
					this.NotifyPropertyChanged("InternalControls");
				}
			}
		}

		[PersistentProperty()]
		public string ExternalControls
		{
			get { return this.m_ExternalControls; }
			set
			{
				if (this.m_ExternalControls != value)
				{
					this.m_ExternalControls = value;
					this.NotifyPropertyChanged("ExternalControls");
				}
			}
		}

		[PersistentProperty()]
		public string Interpretation
		{
			get { return this.m_Interpretation; }
			set
			{
				if (this.m_Interpretation != value)
				{
					this.m_Interpretation = value;
					this.NotifyPropertyChanged("Interpretation");
				}
			}
		}

		[PersistentProperty()]
		public string ReportReferences
		{
			get { return this.m_ReportReferences; }
			set
			{
				if (this.m_ReportReferences != value)
				{
					this.m_ReportReferences = value;
					this.NotifyPropertyChanged("ReportReferences");
				}
			}
		}

		[PersistentProperty()]
		public string ResultComment
		{
			get { return this.m_ResultComment; }
			set
			{
				if (this.m_ResultComment != value)
				{
					this.m_ResultComment = value;
					this.NotifyPropertyChanged("ResultComment");
				}
			}
		}

		[PersistentProperty()]
		public string SpecimenSiteAndType
		{
			get { return this.m_SpecimenSiteAndType; }
			set
			{
				if (this.m_SpecimenSiteAndType != value)
				{
					this.m_SpecimenSiteAndType = value;
					this.NotifyPropertyChanged("SpecimenSiteAndType");
				}
			}
		}

		[PersistentProperty()]
		public string SpecimenIdentification
		{
			get { return this.m_SpecimenIdentification; }
			set
			{
				if (this.m_SpecimenIdentification != value)
				{
					this.m_SpecimenIdentification = value;
					this.NotifyPropertyChanged("SpecimenIdentification");
				}
			}
		}

		[PersistentProperty()]
		public string Method
		{
			get { return this.m_Method; }
			set
			{
				if (this.m_Method != value)
				{
					this.m_Method = value;
					this.NotifyPropertyChanged("Method");
				}
			}
		}

		public override string ToResultString(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
		{
			StringBuilder result = new StringBuilder();
			result.AppendLine("Estrogen Receptor : " + this.ErResult);
			result.AppendLine("Progesterone Receptor : " + this.PrResult);
			return result.ToString();
		}

		public YellowstonePathology.Business.Rules.MethodResult IsOkToSetResults()
		{
			YellowstonePathology.Business.Rules.MethodResult result = new YellowstonePathology.Business.Rules.MethodResult();
			if (this.m_Accepted == true)
			{
				result.Success = false;
				result.Message = "Results may not be set because the results already have been accepted.";
			}
			else if (string.IsNullOrEmpty(this.m_ErResult) == true)
			{
				result.Success = false;
				result.Message = "Results may not be set because the ER Result has not been set.";
			}
			else if (string.IsNullOrEmpty(this.m_PrResult) == true)
			{
				result.Success = false;
				result.Message = "Results may not be set because the PR Result has not been set.";
			}

			return result;
		}

        public override bool ResultsAreSet()
        {
            return string.IsNullOrEmpty(this.m_ErResult) == false && string.IsNullOrEmpty(this.m_PrResult) == false;
        }

        public override MethodResult IsOkToAccept()
        {
            MethodResult methodResult = base.IsOkToAccept();
            if(methodResult.Success == true)
            {
                if(this.ResultsAreSet() == false)
                {
                    methodResult.Success = false;
                    methodResult.Message = "Unable to accept until results are set.";
                }
            }
            return methodResult;
        }

        public override AuditResult IsOkToFinalize(AccessionOrder accessionOrder)
        {
            AuditResult auditResult = base.IsOkToFinalize(accessionOrder);
            if (auditResult.Status == AuditStatusEnum.OK)
            {
                YellowstonePathology.Business.Rules.MethodResult methodResult = this.IsOkToFinalize();
                if(methodResult.Success == false)
                {
                    auditResult.Status = AuditStatusEnum.Failure;
                    auditResult.Message = methodResult.Message;
                }
            }
            return auditResult;
        }

        public override void Finish(Business.Test.AccessionOrder accessionOrder)
        {
            YellowstonePathology.Business.Test.PanelOrder panelOrder = this.PanelOrderCollection.GetPanelByPanelId(62);
            if (panelOrder.Accepted == false)
            {
                panelOrder.Accepted = true;
                panelOrder.AcceptedById = 5001;
                panelOrder.AcceptedDate = DateTime.Today;
                panelOrder.AcceptedTime = DateTime.Now;
            }

            base.Finish(accessionOrder);
        }
    }
}
