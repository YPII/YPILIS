using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI.Mongo
{
    public class Testing
    {
        YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;

        public Testing(YellowstonePathology.Business.Test.AccessionOrder ao)
        {
            this.m_AccessionOrder = ao;
            this.m_AccessionOrder.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(AccessionOrder_PropertyChanged);
            this.m_AccessionOrder.PanelSetOrderCollection.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(PanelSetOrderCollection_CollectionChanged);

            foreach (YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder in this.m_AccessionOrder.PanelSetOrderCollection)
            {
                panelSetOrder.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(PanelSetOrder_PropertyChanged);
                panelSetOrder.PanelOrderCollection.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(PanelOrderCollection_CollectionChanged);

                foreach (YellowstonePathology.Business.Test.PanelOrder panelOrder in panelSetOrder.PanelOrderCollection)
                {
                    panelOrder.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(PanelOrder_PropertyChanged);
                }
            }
        }

        private void PanelOrder_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            
        }

        private void PanelOrderCollection_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            
        }

        private void PanelSetOrder_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            
        }

        private void PanelSetOrderCollection_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            
        }

        private void AccessionOrder_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            
        }
    }
}
