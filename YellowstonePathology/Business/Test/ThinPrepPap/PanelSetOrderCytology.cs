using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Test.ThinPrepPap
{
	[PersistentClass("tblPanelSetOrderCytology", "tblPanelSetOrder", "YPIDATA")]
	public class PanelSetOrderCytology : PanelSetOrder
	{		
		private int m_ScreenedById;
		private string m_ScreenedByName;		
		private string m_SpecimenAdequacy;
		private string m_ScreeningImpression;
		private string m_ScreeningImpressionComment;
		private string m_OtherConditions;
		private string m_ReportComment;
		private string m_InternalComment;
		private string m_ScreenerComment;
		private string m_OrderComment;
		private string m_ScreeningType;
		private bool m_QC;
		private bool m_ImagerError;		
		private bool m_PhysicianInterpretation;
		private int m_SlideCount;
		private bool m_ECCCheckPerformed;
		private bool m_ScreeningError;
        private string m_Method;

		public PanelSetOrderCytology()
		{

		}

		public PanelSetOrderCytology(string masterAccessionNo, string reportNo, string objectId,
			YellowstonePathology.Business.PanelSet.Model.PanelSet panelSet,
            YellowstonePathology.Business.Interface.IOrderTarget orderTarget,
			bool distribute)
			: base(masterAccessionNo, reportNo, objectId, panelSet, orderTarget, distribute)
		{
            this.HasProfessionalComponent = false;
            this.ProfessionalComponentFacilityId = null;
            this.ScreeningType = "Final Result";

            this.m_ReportReferences = YellowstonePathology.Business.Test.ThinPrepPap.ThinPrepPapResult.References;
            this.m_Method = YellowstonePathology.Business.Test.ThinPrepPap.ThinPrepPapResult.Method;			
		}

        public void UpdateFromScreening(YellowstonePathology.Business.Test.ThinPrepPap.PanelOrderCytology panelOrderCytology)
        {
            this.ResultCode = panelOrderCytology.ResultCode;
            this.SpecimenAdequacy = panelOrderCytology.SpecimenAdequacy;
            this.ScreeningImpression = panelOrderCytology.ScreeningImpression;
            this.OtherConditions = panelOrderCytology.OtherConditions;
            this.ReportComment = panelOrderCytology.ReportComment;            
            this.ScreenedById = panelOrderCytology.ScreenedById;
            this.ScreenedByName = panelOrderCytology.ScreenedByName;
        }

        public YellowstonePathology.Business.Test.ThinPrepPap.PanelOrderCytology GetPrimaryScreening()
		{
            YellowstonePathology.Business.Test.ThinPrepPap.PanelOrderCytology result = null;
			foreach (PanelOrder panelOrder in this.m_PanelOrderCollection)
			{
				if (panelOrder.PanelId == 38)
				{
                    if (((YellowstonePathology.Business.Test.ThinPrepPap.PanelOrderCytology)panelOrder).ScreeningType == "Primary Screening")
					{
                        result = (YellowstonePathology.Business.Test.ThinPrepPap.PanelOrderCytology)panelOrder;
						break;
					}
				}
			}
			return result;
		}

        public YellowstonePathology.Business.Test.ThinPrepPap.PanelOrderCytology GetPhysicianInterp()
		{
            YellowstonePathology.Business.Test.ThinPrepPap.PanelOrderCytology result = null;
			foreach (PanelOrder panelOrder in this.m_PanelOrderCollection)
			{
				if (panelOrder.PanelId == 38)
				{
                    if (((YellowstonePathology.Business.Test.ThinPrepPap.PanelOrderCytology)panelOrder).PhysicianInterpretation == true)
					{
                        result = (YellowstonePathology.Business.Test.ThinPrepPap.PanelOrderCytology)panelOrder;
						break;
					}
				}
			}
			return result;
		}

		public bool DoesPeerReviewExist()
		{
			bool result = false;
			foreach (YellowstonePathology.Business.Test.PanelOrder panelOrder in this.m_PanelOrderCollection)
			{
                Type objectType = panelOrder.GetType();
                if (typeof(YellowstonePathology.Business.Test.ThinPrepPap.PanelOrderCytology).IsAssignableFrom(objectType) == true)
                {
                    YellowstonePathology.Business.Test.ThinPrepPap.PanelOrderCytology cytologyPanelOrder = (YellowstonePathology.Business.Test.ThinPrepPap.PanelOrderCytology)panelOrder;
                    if (cytologyPanelOrder.ScreeningType.ToUpper().Contains("PEER REVIEW") == true)
                    {
                        result = true;
                        break;
                    }
                }				
			}
			return result;
		}

		public bool DoesPathologistReviewExist()
		{
			bool result = false;
			foreach (YellowstonePathology.Business.Test.PanelOrder panelOrder in this.m_PanelOrderCollection)
			{
                Type objectType = panelOrder.GetType();
                if (typeof(YellowstonePathology.Business.Test.ThinPrepPap.PanelOrderCytology).IsAssignableFrom(objectType) == true)
                {
                    YellowstonePathology.Business.Test.ThinPrepPap.PanelOrderCytology cytologyPanelOrder = (YellowstonePathology.Business.Test.ThinPrepPap.PanelOrderCytology)panelOrder;
					if (cytologyPanelOrder.ScreeningType.ToUpper().Contains("PATHOLOGIST REVIEW") == true)
					{
						result = true;
						break;
					}
				}
			}
			return result;
		}

		public bool DoesScreeningReviewExist()
		{
			bool result = false;
			foreach (YellowstonePathology.Business.Test.PanelOrder panelOrder in this.m_PanelOrderCollection)
			{
                Type objectType = panelOrder.GetType();
                if (typeof(YellowstonePathology.Business.Test.ThinPrepPap.PanelOrderCytology).IsAssignableFrom(objectType) == true)
                {
                    YellowstonePathology.Business.Test.ThinPrepPap.PanelOrderCytology cytologyPanelOrder = (YellowstonePathology.Business.Test.ThinPrepPap.PanelOrderCytology)panelOrder;
					if (cytologyPanelOrder.ScreeningType.ToUpper() == "PATHOLOGIST REVIEW" || cytologyPanelOrder.ScreeningType.ToUpper() == "CYTOTECH REVIEW")
					{
						result = true;
						break;
					}
				}
			}
			return result;
		}					

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "11", "null", "int")]
		public int ScreenedById
		{
			get { return this.m_ScreenedById; }
			set
			{
				if(this.m_ScreenedById != value)
				{
					this.m_ScreenedById = value;
					this.NotifyPropertyChanged("ScreenedById");
				}
			}
		}		

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "50", "null", "varchar")]
		public string ScreeningType
		{
			get { return this.m_ScreeningType; }
			set
			{
				if(this.m_ScreeningType != value)
				{
					this.m_ScreeningType = value;
					this.NotifyPropertyChanged("ScreeningType");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "150", "null", "varchar")]
		public string ScreenedByName
		{
			get { return this.m_ScreenedByName; }
			set
			{
				if(this.m_ScreenedByName != value)
				{
					this.m_ScreenedByName = value;
					this.NotifyPropertyChanged("ScreenedByName");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "500", "null", "varchar")]
		public string ScreeningImpression
		{
			get { return this.m_ScreeningImpression; }
			set
			{
				if(this.m_ScreeningImpression != value)
				{
					this.m_ScreeningImpression = value;
					this.NotifyPropertyChanged("ScreeningImpression");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "500", "null", "varchar")]
		public string ScreeningImpressionComment
		{
			get { return this.m_ScreeningImpressionComment; }
			set
			{
				if(this.m_ScreeningImpressionComment != value)
				{
					this.m_ScreeningImpressionComment = value;
					this.NotifyPropertyChanged("ScreeningImpressionComment");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "500", "null", "varchar")]
		public string OtherConditions
		{
			get { return this.m_OtherConditions; }
			set
			{
				if(this.m_OtherConditions != value)
				{
					this.m_OtherConditions = value;
					this.NotifyPropertyChanged("OtherConditions");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "500", "null", "varchar")]
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
		[PersistentDataColumnProperty(true, "1000", "null", "varchar")]
		public string ReportComment
		{
			get { return this.m_ReportComment; }
			set
			{
				if(this.m_ReportComment != value)
				{
					this.m_ReportComment = value;
					this.NotifyPropertyChanged("ReportComment");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "1000", "null", "varchar")]
		public string InternalComment
		{
			get { return this.m_InternalComment; }
			set
			{
				if(this.m_InternalComment != value)
				{
					this.m_InternalComment = value;
					this.NotifyPropertyChanged("InternalComment");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "50", "null", "varchar")]
		public string ScreenerComment
		{
			get { return this.m_ScreenerComment; }
			set
			{
				if(this.m_ScreenerComment != value)
				{
					this.m_ScreenerComment = value;
					this.NotifyPropertyChanged("ScreenerComment");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "50", "null", "varchar")]
		public string OrderComment
		{
			get { return this.m_OrderComment; }
			set
			{
				if(this.m_OrderComment != value)
				{
					this.m_OrderComment = value;
					this.NotifyPropertyChanged("ProfessionalComponentFacilityId");
				}
			}
		}
		
		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "1", "null", "bit")]
		public bool QC
		{
			get { return this.m_QC; }
			set
			{
				if(this.m_QC != value)
				{
					this.m_QC = value;
					this.NotifyPropertyChanged("QC");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "1", "null", "bit")]
		public bool ImagerError
		{
			get { return this.m_ImagerError; }
			set
			{
				if(this.m_ImagerError != value)
				{
					this.m_ImagerError = value;
					this.NotifyPropertyChanged("ImagerError");
				}
			}
		}		

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "1", "null", "bit")]
		public bool PhysicianInterpretation
		{
			get { return this.m_PhysicianInterpretation; }
			set
			{
				if(this.m_PhysicianInterpretation != value)
				{
					this.m_PhysicianInterpretation = value;
					this.NotifyPropertyChanged("PhysicianInterpretation");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "11", "null", "int")]
		public int SlideCount
		{
			get { return this.m_SlideCount; }
			set
			{
				if(this.m_SlideCount != value)
				{
					this.m_SlideCount = value;
					this.NotifyPropertyChanged("SlideCount");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "1", "null", "bit")]
		public bool ECCCheckPerformed
		{
			get { return this.m_ECCCheckPerformed; }
			set
			{
				if(this.m_ECCCheckPerformed != value)
				{
					this.m_ECCCheckPerformed = value;
					this.NotifyPropertyChanged("ECCCheckPerformed");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "1", "null", "bit")]
		public bool ScreeningError
		{
			get { return this.m_ScreeningError; }
			set
			{
				if(this.m_ScreeningError != value)
				{
					this.m_ScreeningError = value;
					this.NotifyPropertyChanged("ScreeningError");
				}
			}
		}

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "500", "null", "varchar")]
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

        public YellowstonePathology.Business.Test.ThinPrepPap.PanelOrderCytology GetScreeningPanelOrder()
        {
            YellowstonePathology.Business.Test.ThinPrepPap.PanelOrderCytology result = null;
            foreach (PanelOrder panelOrder in this.PanelOrderCollection)
            {
                if (panelOrder is YellowstonePathology.Business.Test.ThinPrepPap.PanelOrderCytology == true)
                {
                    YellowstonePathology.Business.Test.ThinPrepPap.PanelOrderCytology panelOrderCytology = (YellowstonePathology.Business.Test.ThinPrepPap.PanelOrderCytology)panelOrder;
                    if (panelOrderCytology.ScreeningType == "Primary Screening")
                    {
                        result = panelOrderCytology;
                        break;
                    }
                }
            }
            return result;
        }

        public YellowstonePathology.Business.Test.ThinPrepPap.PanelOrderCytology GetReviewPanelOrder()
        {
            YellowstonePathology.Business.Test.ThinPrepPap.PanelOrderCytology result = null;
            foreach (PanelOrder panelOrder in this.PanelOrderCollection)
            {
                if (panelOrder is YellowstonePathology.Business.Test.ThinPrepPap.PanelOrderCytology == true)
                {
                    YellowstonePathology.Business.Test.ThinPrepPap.PanelOrderCytology panelOrderCytology = (YellowstonePathology.Business.Test.ThinPrepPap.PanelOrderCytology)panelOrder;
                    if (panelOrderCytology.ScreeningType == "Primary Screening")
                    {
                        result = panelOrderCytology;
                        break;
                    }
                }
            }
            return result;
        }

		public override string GetResultWithTestName()
		{
			StringBuilder result = new StringBuilder();
			result.Append("Thin Prep Pap: ");
			result.Append(this.m_ScreeningImpression);
			return result.ToString();
		}

		public override string ToResultString(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
		{
			return this.GetResultWithTestName();
		}

        public Audit.Model.AuditResult IsOkToFinalize(YellowstonePathology.Business.Test.AccessionOrder accessionOrder,
            PanelOrderCytology panelOrderToFinalize,
            YellowstonePathology.Business.User.SystemIdentity systemIdentity,
            YellowstonePathology.Business.Rules.ExecutionStatus executionStatus)
        {
            Audit.Model.AuditCollection auditCollection = new Audit.Model.AuditCollection();
            auditCollection.Add(new Audit.Model.DistributionCanBeSetAudit(accessionOrder));
            auditCollection.Add(new Audit.Model.CanFinalizeCytologyPanelOrderAudit(panelOrderToFinalize, this, accessionOrder, systemIdentity, executionStatus));
            Audit.Model.AuditResult auditResult = auditCollection.Run2();

            if(auditResult.Status == Audit.Model.AuditStatusEnum.Failure)
            {
                if(executionStatus.Halted == false)
                {
                    executionStatus.AddMessage(auditResult.Message, true);
                    executionStatus.ShowMessage = true;
                    executionStatus.Halted = true;
                }
            }
            return auditResult;
        }
	}
}
