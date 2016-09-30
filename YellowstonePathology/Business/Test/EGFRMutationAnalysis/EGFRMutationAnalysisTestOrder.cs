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
		[PersistentDataColumnProperty(true, "1", "0", "bit")]
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

		public override Business.Rules.MethodResult IsOkToFinalize()
		{
			YellowstonePathology.Business.Rules.MethodResult result = base.IsOkToFinalize();
			if (result.Success == true)
			{
				if (string.IsNullOrEmpty(this.m_TumorNucleiPercentage) == true)
				{
					result.Success = false;
					result.Message = "The results cannot be finalized because the Tumor Nuclei Percentage has no value.";
				}
			}
			return result;
		}

        public override YellowstonePathology.Business.Audit.Model.AuditResult IsOkToFinalize(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {
            return base.IsOkToFinalize(accessionOrder);
        }

        public override MethodResult IsOkToAccept()
        {
            MethodResult result = base.IsOkToAccept();
            if(result.Success == true)
            {
                if(string.IsNullOrEmpty(this.m_ResultCode) == true)
                {
                    result.Success = false;
                    result.Message = "This case cannot be accepted because the results are not set.";
                }
            }

            return result;
        }
    }
}
