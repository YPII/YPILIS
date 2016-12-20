using System;
using System.Windows.Data;

namespace YellowstonePathology.Business.Flow
{
    public class Marker : BaseData
    {
        Flow.MarkerCollection m_MarkerCollection;
        ListCollectionView m_MarkerCollectionView;

        public Marker()
        {
            this.m_MarkerCollection = new MarkerCollection();
            this.m_MarkerCollectionView = new ListCollectionView(this.m_MarkerCollection);
            this.m_MarkerCollectionView.CurrentChanged += new EventHandler(m_MarkerCollectionView_CurrentChanged);
        }

        void m_MarkerCollectionView_CurrentChanged(object sender, EventArgs e)
        {
            this.NotifyPropertyChanged("CurrentItem");
        }

        public MarkerItem CurrentItem
        {
            get { return (MarkerItem)this.m_MarkerCollectionView.CurrentItem; }
        }

        public MarkerCollection MarkerCollection
        {
            get { return this.m_MarkerCollection; }
        }

        public ListCollectionView MarkerCollectionView
        {
            get { return this.m_MarkerCollectionView; }
        }
    }
}
