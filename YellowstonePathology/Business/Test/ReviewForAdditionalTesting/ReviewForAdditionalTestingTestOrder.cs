using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Test.ReviewForAdditionalTesting
{
	[PersistentClass("tblReviewForAdditionalTestingTestOrder", "tblPanelSetOrder", "YPIDATA")]
	public class ReviewForAdditionalTestingTestOrder : YellowstonePathology.Business.Test.PanelSetOrder
	{
		private string m_Comment;
		private string m_Result;
		
		public ReviewForAdditionalTestingTestOrder()
        {

        }

        public ReviewForAdditionalTestingTestOrder(string masterAccessionNo, string reportNo, string objectId,
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

		public override string ToResultString(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
		{
			StringBuilder result = new StringBuilder();

			result.AppendLine("Result: " + this.m_Result);
			result.AppendLine();
			result.AppendLine("Comment: " + this.m_Comment);
			result.AppendLine();			

			return result.ToString();
		}

		public override YellowstonePathology.Business.Rules.MethodResult IsOkToAccept()
		{
			YellowstonePathology.Business.Rules.MethodResult result = new YellowstonePathology.Business.Rules.MethodResult();
			if (this.Final == true)
			{
				result.Success = false;
				result.Message = "The results cannot be accepted because the case is already Final.";
			}
			return result;
		}

		public override YellowstonePathology.Business.Rules.MethodResult IsOkToFinalize()
		{
			YellowstonePathology.Business.Rules.MethodResult result = new YellowstonePathology.Business.Rules.MethodResult();
			if (this.Final == true)
			{
				result.Success = false;
				result.Message = "This case cannot be finalized because it is already finalized.";
			}
			else if (string.IsNullOrEmpty(this.Result) == true)
			{
				result.Success = false;
				result.Message = "This case cannot be finalized because the results have not been set.";
			}
			return result;
		}
	}
}
