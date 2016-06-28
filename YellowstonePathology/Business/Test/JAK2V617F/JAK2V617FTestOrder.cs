using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Test.JAK2V617F
{
	[PersistentClass("tblJAK2V617FTestOrder", "tblPanelSetOrder", "YPIDATA")]
	public class JAK2V617FTestOrder : YellowstonePathology.Business.Test.PanelSetOrder
	{

		private string m_Result;
		private string m_Interpretation;
		private string m_Comment;
		private string m_Method;
		private string m_Reference;
        private string m_Disclosure;

        public JAK2V617FTestOrder()
        {
            
        }

		public JAK2V617FTestOrder(string masterAccessionNo, string reportNo, string objectId,
            YellowstonePathology.Business.PanelSet.Model.PanelSet panelSet,
            YellowstonePathology.Business.Interface.IOrderTarget orderTarget,
			bool distribute)
			: base(masterAccessionNo, reportNo, objectId, panelSet, orderTarget, distribute)
		{
            this.m_Disclosure = "This test was developed and its performance characteristics determined by Yellowstone Pathology " +
                "Institute, Inc.  It has not been cleared or approved by the U.S. Food and Drug Administration.  The FDA has determined " +
                "that such clearance or approval is not necessary.  This test is used for clinical purposes.  It should not be regarded " +
                "as investigational or for research.  This laboratory is certified under the Clinical Laboratory Improvement Amendments " +
                "of 1988 (CLIA-88) as qualified to perform high complexity clinical laboratory testing.";
        }

        public YellowstonePathology.Business.Rules.MethodResult IsOkToSetResults()
		{
			YellowstonePathology.Business.Rules.MethodResult result = new YellowstonePathology.Business.Rules.MethodResult();
            if(string.IsNullOrEmpty(this.m_ResultCode) == true)
            {
                result.Success = false;
                result.Message = "A Result must be selected before the result can be set.";
            }
            else if(this.m_Accepted == true)
			{
				result.Success = false;
				result.Message = "Results may not be set because the results already have been accepted.";
			}
			return result;
		}

        public override YellowstonePathology.Business.Rules.MethodResult IsOkToAccept()
        {
            YellowstonePathology.Business.Rules.MethodResult result = base.IsOkToAccept();
            if (result.Success == true)
            {
                if (string.IsNullOrEmpty(this.m_ResultCode) == true)
                {
                    result.Success = false;
                    result.Message = "The results cannot be accepted because the Result is not set.";
                }
            }
            return result;
        }

        public override YellowstonePathology.Business.Audit.Model.AuditResult IsOkToFinalize(Test.AccessionOrder accessionOrder)
        {
            Audit.Model.AuditResult auditResult = base.IsOkToFinalize(accessionOrder);
            if (auditResult.Status == Audit.Model.AuditStatusEnum.OK)
            {
                if (string.IsNullOrEmpty(this.m_ResultCode) == true)
                {
                    auditResult.Status = Audit.Model.AuditStatusEnum.Failure;
                    auditResult.Message = "This case cannot be finalized because the results have not been set.";
                }
            }
            return auditResult;
        }

        [PersistentProperty()]
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
		public string Reference
		{
			get { return this.m_Reference; }
			set
			{
				if (this.m_Reference != value)
				{
					this.m_Reference = value;
					this.NotifyPropertyChanged("Reference");
				}
			}
		}

        [PersistentProperty()]
        public string Disclosure
        {
            get { return this.m_Disclosure; }
            set
            {
                if (this.m_Disclosure != value)
                {
                    this.m_Disclosure = value;
                    this.NotifyPropertyChanged("Disclosure");
                }
            }
        }

        public override string ToResultString(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {
            StringBuilder result = new StringBuilder();
            result.AppendLine("Result: " + this.Result);
            result.AppendLine();

            result.AppendLine("Comment: " + this.m_Comment);
            result.AppendLine();
            
			result.AppendLine("Interpretation: " + this.m_Interpretation);
			result.AppendLine();

            result.AppendLine("Method: " + this.m_Method);
            result.AppendLine();

            return result.ToString();
        }
	}
}
