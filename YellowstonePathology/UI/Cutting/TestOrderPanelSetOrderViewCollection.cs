using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace YellowstonePathology.UI.Cutting
{
    public class TestOrderPanelSetOrderViewCollection : ObservableCollection<TestOrderPanelSetOrderView>
    {
        public TestOrderPanelSetOrderViewCollection(YellowstonePathology.Business.Test.Model.TestOrderCollection_Base testOrderCollection, YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {            
            foreach (YellowstonePathology.Business.Test.Model.TestOrder_Base testOrder in testOrderCollection)
            {
                YellowstonePathology.Business.Test.Model.TestOrder realTestOrder = accessionOrder.PanelSetOrderCollection.GetTestOrderByTestOrderId(testOrder.TestOrderId);
                YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder = accessionOrder.PanelSetOrderCollection.GetPanelSetOrderByTestOrderId(testOrder.TestOrderId);
                TestOrderPanelSetOrderView testOrderPanelSetOrderView = new TestOrderPanelSetOrderView(panelSetOrder, realTestOrder);
                this.Add(testOrderPanelSetOrderView);
            }
        }
    }
}
