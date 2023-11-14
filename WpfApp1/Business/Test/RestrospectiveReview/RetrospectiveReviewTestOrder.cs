using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Test.RetrospectiveReview
{    
    [PersistentClass("tblRetrospectiveReviewTestOrder", "tblPanelSetOrder", "YPIDATA")]
	public class RetrospectiveReviewTestOrder : YellowstonePathology.Business.Test.PanelSetOrder
	{
		private string m_Result;
		private string m_Comment;
        private RetrospectiveReviewTestOrderDetailCollection m_RetrospectiveReviewTestOrderDetailCollection;
		
		public RetrospectiveReviewTestOrder()
        {
            this.m_RetrospectiveReviewTestOrderDetailCollection = new RetrospectiveReviewTestOrderDetailCollection();
        }

		public RetrospectiveReviewTestOrder(string masterAccessionNo, string reportNo, string objectId,
			YellowstonePathology.Business.PanelSet.Model.PanelSet panelSet,
			bool distribute)
			: base(masterAccessionNo, reportNo, objectId, panelSet, distribute)
		{
            this.m_RetrospectiveReviewTestOrderDetailCollection = new RetrospectiveReviewTestOrderDetailCollection();
		}

        [PersistentCollection()]
        public RetrospectiveReviewTestOrderDetailCollection RetrospectiveReviewTestOrderDetailCollection
        {
            get { return this.m_RetrospectiveReviewTestOrderDetailCollection; }
            set { this.m_RetrospectiveReviewTestOrderDetailCollection = value; }
        }

        [PersistentProperty()]
		[PersistentDataColumnProperty(true, "500", "null", "varchar")]
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
		[PersistentDataColumnProperty(true, "5000", "null", "varchar")]
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

        public override string ToResultString(Business.Test.AccessionOrder accessionOrder)
        {            
            string result = "Result: " + this.Result + Environment.NewLine;
            result += "Comment: " + this.Comment;
            return result;
        }       
    }
}
