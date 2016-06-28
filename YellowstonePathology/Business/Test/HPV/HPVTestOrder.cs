using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Test.HPV
{
	[PersistentClass("tblHPVTestOrder", "tblPanelSetOrder", "YPIDATA")]
    public class HPVTestOrder : PanelSetOrder
	{        

		private string m_Result;
		private string m_Comment;
		private string m_References;
		private string m_TestInformation;
        private string m_ASRComment;

		public HPVTestOrder()
		{

		}

		public HPVTestOrder(string masterAccessionNo, string reportNo, string objectId,
			YellowstonePathology.Business.PanelSet.Model.PanelSet panelSet,
            YellowstonePathology.Business.Interface.IOrderTarget orderTarget,
			bool distribute)
			: base(masterAccessionNo, reportNo, objectId, panelSet, orderTarget, distribute)
		{
            this.m_TestInformation = HPVResult.TestInformation;
            this.m_References = HPVResult.References;
            this.m_ASRComment = HPVResult.ASRComment;
            this.m_TechnicalComponentInstrumentId = Instrument.HOLOGICPANTHERID;
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
		public string References
		{
			get { return this.m_References; }
			set
			{
				if (this.m_References != value)
				{
					this.m_References = value;
					this.NotifyPropertyChanged("References");
				}
			}
		}

		[PersistentProperty()]
		public string TestInformation
		{
			get { return this.m_TestInformation; }
			set
			{
				if (this.m_TestInformation != value)
				{
					this.m_TestInformation = value;
					this.NotifyPropertyChanged("TestInformation");
				}
			}
		}

        [PersistentProperty()]
        public string ASRComment
        {
            get { return this.m_ASRComment; }
            set
            {
                if (this.m_ASRComment != value)
                {
                    this.m_ASRComment = value;
                    this.NotifyPropertyChanged("ASRComment");
                }
            }
        }

        public override string GetResultWithTestName()
		{
			StringBuilder result = new StringBuilder();
			result.Append(this.m_PanelSetName);
			result.Append(": ");
			result.Append(this.m_Result);
			return result.ToString();
		}

		public override string ToResultString(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
		{
			return this.GetResultWithTestName();
		}

        public override YellowstonePathology.Business.Rules.MethodResult IsOkToAccept()
        {
            
            YellowstonePathology.Business.Rules.MethodResult result = base.IsOkToAccept();
            if (result.Success == true)
            {
                if (string.IsNullOrEmpty(this.ResultCode) == true)
                {
                    result.Success = false;
                    result.Message = "This case cannot be accepted because the results are not set.";
                }
            }
            return result;
        }

        public override YellowstonePathology.Business.Audit.Model.AuditResult IsOkToFinalize(Test.AccessionOrder accessionOrder)
        {
            YellowstonePathology.Business.Audit.Model.AuditResult result = base.IsOkToFinalize(accessionOrder);
            if(result.Status == Audit.Model.AuditStatusEnum.OK)
            {
                if(this.Accepted == false)
                {
                    YellowstonePathology.Business.Rules.MethodResult methodResult = this.IsOkToAccept();
                    if(methodResult.Success == false)
                    {
                        result.Status = Audit.Model.AuditStatusEnum.Failure;
                        result.Message = "This case cannot be finalized because the results are not set.";
                    }
                }
            }
            return result;
        }
    }
}
