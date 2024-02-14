using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace YellowstonePathology.UI.Cutting
{
    public class TestOrderPanelSetOrderViewCollection : ObservableCollection<TestOrderPanelSetOrderView>
    {
        public void UpdateCollection()
        {
            foreach (TestOrderPanelSetOrderView item in this)
            {
                item.Update();
            }
        }

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

        public void PrintLabels(Business.Test.AccessionOrder accessionOrder, Business.Test.AliquotOrder aliquotOrder)
        {
            Business.Test.Model.Test kappa = Business.Test.Model.TestCollectionInstance.GetClone("360"); // KappaByISH();
            Business.Test.Model.Test lambda = Business.Test.Model.TestCollectionInstance.GetClone("361"); // LambdaByISH();
            Business.Test.Model.Test u6 = Business.Test.Model.TestCollectionInstance.GetClone("383"); // U6();

            //add test order that need to be ordered automatically
            if(accessionOrder.PanelSetOrderCollection.DoesStainOrderExist(kappa.TestId) == true && accessionOrder.PanelSetOrderCollection.DoesStainOrderExist(lambda.TestId) == true)
            {
                if(accessionOrder.PanelSetOrderCollection.DoesStainOrderExist(u6.TestId) == false)
                {
                    Business.Test.PanelSetOrder panelSetOrder =  accessionOrder.PanelSetOrderCollection.GetPanelSetOrderByTestId(kappa.TestId);                    
                    YellowstonePathology.Business.Visitor.OrderTestVisitor orderTestVisitor = new Business.Visitor.OrderTestVisitor(panelSetOrder.ReportNo, u6, null, null, false, aliquotOrder, false, false, accessionOrder.TaskOrderCollection);
                    accessionOrder.TakeATrip(orderTestVisitor);
                    this.Add(new TestOrderPanelSetOrderView(panelSetOrder, orderTestVisitor.TestOrder));
                }
            }

            //add slides and print.
            foreach (TestOrderPanelSetOrderView item in this)
            {
                Business.Test.Model.TestOrder testOrder = accessionOrder.PanelSetOrderCollection.GetTestOrderByTestOrderId(item.TestOrderId);
                if(testOrder.SlideOrderCollection.Count == 0)
                {                                        
                    YellowstonePathology.Business.Visitor.AddSlideOrderVisitor addSlideOrderVisitor = new Business.Visitor.AddSlideOrderVisitor(aliquotOrder, testOrder);
                    accessionOrder.TakeATrip(addSlideOrderVisitor);
                    
                    Business.HL7View.VentanaStainOrder ventanaStainOrder = new Business.HL7View.VentanaStainOrder();
                    ventanaStainOrder.HandleOrder(accessionOrder, addSlideOrderVisitor.NewSlideOrder);
                }
            }
        }
    }
}
