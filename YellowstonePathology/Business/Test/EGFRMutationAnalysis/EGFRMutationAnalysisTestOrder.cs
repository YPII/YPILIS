using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YellowstonePathology.Business.Persistence;
using YellowstonePathology.Business.Rules;

namespace YellowstonePathology.Business.Test.EGFRMutationAnalysis
{
	[PersistentClass("tblEgfrMutationAnalysisTestOrder", "tblPanelSetOrder", "YPIDATA")]
	public class EGFRMutationAnalysisTestOrder : YellowstonePathology.Business.Test.PanelSetOrder, YellowstonePathology.Business.Interface.ISolidTumorTesting
    {                                                
        private string m_Result;
        private string m_Mutation;
        private string m_Method;
        private string m_Indication;
        private string m_Interpretation;
        private bool m_MicrodissectionPerformed;
        private string m_Comment;        
        private string m_TumorNucleiPercentage;
        private string m_ReportDisclaimer;

        public EGFRMutationAnalysisTestOrder()
        {
            
        }

		public EGFRMutationAnalysisTestOrder(string masterAccessionNo, string reportNo, string objectId,
			YellowstonePathology.Business.PanelSet.Model.PanelSet panelSet,
            YellowstonePathology.Business.Interface.IOrderTarget orderTarget,
			bool distribute)
			: base(masterAccessionNo, reportNo, objectId, panelSet, orderTarget, distribute)
		{
			
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "100", "null", "varchar")]
		public string Result
        {
            get { return this.m_Result; }
            set
            {
                if (this.m_Result != value)
                {
                    this.m_Result = value;
                    this.NotifyPropertyChanged("Result");
                }
            }
        }

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "500", "null", "varchar")]
		public string Mutation
        {
            get { return this.m_Mutation; }
            set
            {
                if (this.m_Mutation != value)
                {
                    this.m_Mutation = value;
                    this.NotifyPropertyChanged("Mutation");
                }
            }
        }

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "500", "null", "varchar")]
		public string Indication
        {
            get { return this.m_Indication; }
            set
            {
                if (this.m_Indication != value)
                {
                    this.m_Indication = value;
                    this.NotifyPropertyChanged("Indication");
                }
            }
        }

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "1000", "null", "varchar")]
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

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "1000", "null", "varchar")]
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
		[PersistentDataColumnProperty(true, "50", "null", "varchar")]
		public string TumorNucleiPercentage
        {
            get { return this.m_TumorNucleiPercentage; }
            set
            {
                if (this.m_TumorNucleiPercentage != value)
                {
                    this.m_TumorNucleiPercentage = value;
                    this.NotifyPropertyChanged("TumorNucleiPercentage");
                }
            }
        }

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "500", "null", "varchar")]
		public string Comment
        {
            get { return this.m_Comment; }
            set
            {
                if (this.m_Comment != value)
                {
                    this.m_Comment = value;
                    this.NotifyPropertyChanged("Comment");
                }
            }
        }

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "1", "0", "tinyint")]
		public bool MicrodissectionPerformed
        {
            get { return this.m_MicrodissectionPerformed; }
            set
            {
                if (this.m_MicrodissectionPerformed != value)
                {
                    this.m_MicrodissectionPerformed = value;
                    this.NotifyPropertyChanged("MicrodissectionPerformed");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "1000", "null", "varchar")]
        public string ReportDisclaimer
        {
            get { return this.m_ReportDisclaimer; }
            set
            {
                if (this.m_ReportDisclaimer != value)
                {
                    this.m_ReportDisclaimer = value;
                    this.NotifyPropertyChanged("ReportDisclaimer");
                }
            }
        }

        public override string ToResultString(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
		{
			StringBuilder result = new StringBuilder();

			result.AppendLine("Result: " + this.m_Result);
			result.AppendLine();

			result.AppendLine("Indication: " + this.m_Indication);
			result.AppendLine();

			result.AppendLine("Interpretation: " + this.m_Interpretation);
			result.AppendLine();

			result.AppendLine("Comment: " + this.m_Comment);
			result.AppendLine();

			result.AppendLine("Mutation: " + this.m_Mutation);
			result.AppendLine();

			result.AppendLine("Tumor Nuclei Percentage: " + this.m_TumorNucleiPercentage);
			result.AppendLine();

			return result.ToString();
		}

        public override void SetPreviousResults(PanelSetOrder pso)
        {
            EGFRMutationAnalysisTestOrder panelSetOrder = (EGFRMutationAnalysisTestOrder)pso;
            panelSetOrder.Result = this.m_Result;
            panelSetOrder.Mutation = this.m_Mutation;
            panelSetOrder.Method = this.m_Method;
            panelSetOrder.Indication = this.m_Indication;
            panelSetOrder.Interpretation = this.m_Interpretation;
            panelSetOrder.MicrodissectionPerformed = this.m_MicrodissectionPerformed;
            panelSetOrder.Comment = this.Comment;
            panelSetOrder.ReportDisclaimer = this.m_ReportDisclaimer;
            base.SetPreviousResults(pso);
        }

        public override void ClearPreviousResults()
        {
            this.m_Result = null;
            this.m_Mutation = null;
            this.m_Method = null;
            this.m_Indication = null;
            this.m_Interpretation = null;
            this.m_MicrodissectionPerformed = false;
            this.m_Comment = null;
            this.m_ReportDisclaimer = null;
            base.ClearPreviousResults();
        }

        public override Audit.Model.AuditResult IsOkToSetPreviousResults(PanelSetOrder panelSetOrder, AccessionOrder accessionOrder)
        {
            Audit.Model.AuditResult result = base.IsOkToSetPreviousResults(panelSetOrder, accessionOrder);
            if (result.Status == Audit.Model.AuditStatusEnum.OK)
            {
                EGFRMutationAnalysisTestOrder egfrMutationAnalysisTestOrder = (EGFRMutationAnalysisTestOrder)panelSetOrder;
                this.DoesFinalSummaryResultMatch(accessionOrder, egfrMutationAnalysisTestOrder.Result, result);
                if (result.Status == Audit.Model.AuditStatusEnum.Warning)
                {
                    result.Message += AskSetPreviousResults;
                }
            }

            return result;
        }

        public override Audit.Model.AuditResult IsOkToAccept(AccessionOrder accessionOrder)
        {
            Audit.Model.AuditResult result = base.IsOkToAccept(accessionOrder);
            if (result.Status == Audit.Model.AuditStatusEnum.OK)
            {
                if (string.IsNullOrEmpty(this.Result) == true)
                {
                    result.Status = Audit.Model.AuditStatusEnum.Failure;
                    result.Message = UnableToAccept;
                }
            }

            if (result.Status == Audit.Model.AuditStatusEnum.OK)
            {
                if (string.IsNullOrEmpty(this.m_TumorNucleiPercentage) == true)
                {
                    result.Status = Audit.Model.AuditStatusEnum.Failure;
                    result.Message = "The results cannot be accepted because the Tumor Nuclei Percentage has no value.";
                }
            }

            if (result.Status == Audit.Model.AuditStatusEnum.OK)
            {
                this.DoesFinalSummaryResultMatch(accessionOrder, this.m_Result, result);
                if (result.Status == Audit.Model.AuditStatusEnum.Warning)
                {
                    result.Message += AskAccept;
                }
            }

            return result;
        }

        public override YellowstonePathology.Business.Audit.Model.AuditResult IsOkToFinalize(Test.AccessionOrder accessionOrder)
        {
            Audit.Model.AuditResult result = base.IsOkToFinalize(accessionOrder);
            if (result.Status == Audit.Model.AuditStatusEnum.OK)
            {
                if (string.IsNullOrEmpty(this.m_Result) == true)
                {
                    result.Status = Audit.Model.AuditStatusEnum.Failure;
                    result.Message = UnableToFinal;
                }
            }

            if (result.Status == Audit.Model.AuditStatusEnum.OK)
            {
                if (string.IsNullOrEmpty(this.m_TumorNucleiPercentage) == true)
                {
                    result.Status = Audit.Model.AuditStatusEnum.Failure;
                    result.Message = "The results cannot be finalized because the Tumor Nuclei Percentage has no value.";
                }
            }

            if (result.Status == Audit.Model.AuditStatusEnum.OK)
            {
                this.DoesFinalSummaryResultMatch(accessionOrder, this.m_Result, result);
                if (result.Status == Audit.Model.AuditStatusEnum.Warning)
                {
                    result.Message += AskFinal;
                }
            }

            return result;
        }

        private void DoesFinalSummaryResultMatch(AccessionOrder accessionOrder, string result, Audit.Model.AuditResult auditResult)
        {
            Business.Test.EGFRToALKReflexAnalysis.EGFRToALKReflexAnalysisTest egfrToALKReflexAnalysisTest = new EGFRToALKReflexAnalysis.EGFRToALKReflexAnalysisTest();

            if (accessionOrder.PanelSetOrderCollection.Exists(egfrToALKReflexAnalysisTest.PanelSetId) == true)
            {
                Business.Test.EGFRToALKReflexAnalysis.EGFRToALKReflexAnalysisTestOrder egfrToALKReflexAnalysisTestOrder = (EGFRToALKReflexAnalysis.EGFRToALKReflexAnalysisTestOrder)accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(egfrToALKReflexAnalysisTest.PanelSetId);
                egfrToALKReflexAnalysisTestOrder.DoesEGFRMutationAnalysisResultMatch(result, auditResult);
            }
        }
    }
}
