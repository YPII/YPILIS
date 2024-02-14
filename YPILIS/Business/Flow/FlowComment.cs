using System;
using System.Windows.Data;

namespace YellowstonePathology.Business.Flow
{
    public class FlowComment : BaseData
    {
        Flow.FlowCommentCollection m_FlowCommentCollection;
        ListCollectionView m_FlowCommentCollectionView;
        string m_CommentFilter = "Lymphocytes";

        public FlowComment()
        {
            this.m_FlowCommentCollection = new FlowCommentCollection();
            this.m_FlowCommentCollectionView = new ListCollectionView(this.m_FlowCommentCollection);            
            this.m_FlowCommentCollectionView.CurrentChanged += new EventHandler(m_FlowCommentCollectionView_CurrentChanged);                        
        }

        public bool FilterByCategory(object o)
        {            
            FlowCommentItem item = (FlowCommentItem)o;
            return (item.Category == this.m_CommentFilter);
        }

        public string CommentFilter
        {
            get { return this.m_CommentFilter; }
            set
            {
                this.m_CommentFilter = value;
                this.m_FlowCommentCollectionView.Filter = new Predicate<object>(FilterByCategory);        
            }
        }

        void m_FlowCommentCollectionView_CurrentChanged(object sender, EventArgs e)
        {
            this.NotifyPropertyChanged("CurrentItem");
        }

        public FlowCommentItem CurrentItem
        {
            get { return (FlowCommentItem)this.m_FlowCommentCollectionView.CurrentItem; }
        }

        public FlowCommentCollection FlowCommentCollection
        {
            get { return this.m_FlowCommentCollection; }
        }

        public ListCollectionView FlowCommentCollectionView
        {
            get { return this.m_FlowCommentCollectionView; }
        }
    }
}
