using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Test.MPNExtendedReflex
{
	[PersistentClass("tblPanelSetOrderMPNExtendedReflex", "tblPanelSetOrder", "YPIDATA")]
	public class PanelSetOrderMPNExtendedReflex : YellowstonePathology.Business.Test.ReflexTesting.ReflexTestingPlan
	{
		private string m_Comment;
		private string m_Interpretation;
		private string m_Method;
        private string m_JAK2V617FResult;
        private string m_CalreticulinMutationAnalysisResult;
        private string m_JAK2Exon1214Result;
        private string m_MPLResult;
        private string m_JAK2Mutation;

        public PanelSetOrderMPNExtendedReflex()
		{

		}

		public PanelSetOrderMPNExtendedReflex(string masterAccessionNo, string reportNo, string objectId,
			YellowstonePathology.Business.PanelSet.Model.PanelSet panelSet,
			YellowstonePathology.Business.Interface.IOrderTarget orderTarget,
			bool distribute)
			: base(masterAccessionNo, reportNo, objectId, panelSet, orderTarget, distribute)
		{

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
		[PersistentDataColumnProperty(true, "5000", "null", "varchar")]
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
		[PersistentDataColumnProperty(true, "5000", "null", "varchar")]
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
        public string JAK2V617FResult
        {
            get { return this.m_JAK2V617FResult; }
            set
            {
                if (this.m_JAK2V617FResult != value)
                {
                    this.m_JAK2V617FResult = value;
                    this.NotifyPropertyChanged("JAK2V617FResult");
                }
            }
        }

        [PersistentProperty()]
        public string JAK2Exon1214Result
        {
            get { return this.m_JAK2Exon1214Result; }
            set
            {
                if (this.m_JAK2Exon1214Result != value)
                {
                    this.m_JAK2Exon1214Result = value;
                    this.NotifyPropertyChanged("JAK2Exon1214Result");
                }
            }
        }

        [PersistentProperty()]
        public string CalreticulinMutationAnalysisResult
        {
            get { return this.m_CalreticulinMutationAnalysisResult; }
            set
            {
                if (this.m_CalreticulinMutationAnalysisResult != value)
                {
                    this.m_CalreticulinMutationAnalysisResult = value;
                    this.NotifyPropertyChanged("CalreticulinMutationAnalysisResult");
                }
            }
        }

        [PersistentProperty()]
        public string MPLResult
        {
            get { return this.m_MPLResult; }
            set
            {
                if (this.m_MPLResult != value)
                {
                    this.m_MPLResult = value;
                    this.NotifyPropertyChanged("MPLResult");
                }
            }
        }

        [PersistentProperty()]
        public string JAK2Mutation
        {
            get { return this.m_JAK2Mutation; }
            set
            {
                if (this.m_JAK2Mutation != value)
                {
                    this.m_JAK2Mutation = value;
                    this.NotifyPropertyChanged("JAK2Mutation");
                }
            }
        }

        public override string ToResultString(AccessionOrder accessionOrder)
		{
			StringBuilder result = new StringBuilder();
            result.AppendLine("Interpretation:");
            result.AppendLine(this.m_Interpretation);
            result.AppendLine();

            result.AppendLine("Comment:");
            result.AppendLine(this.m_Comment);
            result.AppendLine();


            result.AppendLine("JAK2 V617F");
            result.AppendLine(this.m_JAK2V617FResult);
            result.AppendLine();

            result.AppendLine("JAK2 Exon 1214");
            result.AppendLine(this.m_JAK2Exon1214Result);
            result.AppendLine();

            result.AppendLine("Calreticulin Mutation Analysis");
            result.AppendLine(this.m_CalreticulinMutationAnalysisResult);
            result.AppendLine();

            result.AppendLine("MPL");
            result.AppendLine(this.m_MPLResult);
            result.AppendLine();

            return result.ToString();
        }

        public override void SetPreviousResults(PanelSetOrder panelSetOrder)
        {
            Business.Test.MPNExtendedReflex.PanelSetOrderMPNExtendedReflex panelSetOrderMPNExtendedReflex = (Business.Test.MPNExtendedReflex.PanelSetOrderMPNExtendedReflex)panelSetOrder;
            panelSetOrderMPNExtendedReflex.JAK2V617FResult = this.m_JAK2V617FResult;
            panelSetOrderMPNExtendedReflex.JAK2Exon1214Result = this.m_JAK2Exon1214Result;
            panelSetOrderMPNExtendedReflex.CalreticulinMutationAnalysisResult = this.m_CalreticulinMutationAnalysisResult;
            panelSetOrderMPNExtendedReflex.MPLResult = this.m_MPLResult;
            panelSetOrderMPNExtendedReflex.Comment = this.m_Comment;
            panelSetOrderMPNExtendedReflex.Interpretation = this.m_Interpretation;
            panelSetOrderMPNExtendedReflex.Method = this.m_Method;
            base.SetPreviousResults(panelSetOrder);
        }

        public override void ClearPreviousResults()
        {
            this.m_JAK2V617FResult = null;
            this.m_JAK2Exon1214Result = null;
            this.m_MPLResult = null;
            this.m_CalreticulinMutationAnalysisResult = null;
            this.m_Comment = null;
            this.m_Interpretation = null;
            this.m_Method = null;
            base.ClearPreviousResults();
        }

        public override Audit.Model.AuditResult IsOkToAccept(AccessionOrder accessionOrder)
        {
            Audit.Model.AuditResult result = base.IsOkToAccept(accessionOrder);
            if (result.Status == Audit.Model.AuditStatusEnum.OK)
            {
                this.AreTestResultsPresent(accessionOrder, result);
            }

            return result;
        }

        public override YellowstonePathology.Business.Audit.Model.AuditResult IsOkToFinalize(Test.AccessionOrder accessionOrder)
        {
            Audit.Model.AuditResult result = base.IsOkToFinalize(accessionOrder);
            if (result.Status == Audit.Model.AuditStatusEnum.OK)
            {
                this.AreTestResultsPresent(accessionOrder, result);
            }
            return result;
        }

        private void AreTestResultsPresent(AccessionOrder accessionOrder, Audit.Model.AuditResult result)
        {
            if(string.IsNullOrEmpty(this.m_JAK2V617FResult) == true)
            {
                result.Status = Audit.Model.AuditStatusEnum.Failure;
                result.Message += NotFilledMessage("JAK2V617FResult");
            }

            if (string.IsNullOrEmpty(this.m_JAK2Exon1214Result) == true)
            {
                result.Status = Audit.Model.AuditStatusEnum.Failure;
                result.Message += NotFilledMessage("JAK2Exon1214Result");
            }

            if (string.IsNullOrEmpty(this.m_MPLResult) == true)
            {
                result.Status = Audit.Model.AuditStatusEnum.Failure;
                result.Message += NotFilledMessage("MPLResult");
            }

            if (string.IsNullOrEmpty(this.m_CalreticulinMutationAnalysisResult) == true)
            {
                result.Status = Audit.Model.AuditStatusEnum.Failure;
                result.Message += NotFilledMessage("CalreticulinMutationAnalysisResult");
            }
        }
    }
}
