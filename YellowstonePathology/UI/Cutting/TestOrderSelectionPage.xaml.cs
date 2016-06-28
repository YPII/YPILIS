using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;

namespace YellowstonePathology.UI.Cutting
{    
	public partial class TestOrderSelectionPage : UserControl, INotifyPropertyChanged 
	{
		public event PropertyChangedEventHandler PropertyChanged;

        public delegate void TestOrderSelectedEventHandler(object sender, YellowstonePathology.UI.CustomEventArgs.TestOrderAliquotOrderReturnEventArgs eventArgs);
        public event TestOrderSelectedEventHandler TestOrderSelected;

        public delegate void BackEventHandler(object sender, YellowstonePathology.UI.CustomEventArgs.MasterAccessionNoReturnEventArgs eventArgs);
        public event BackEventHandler Back;
        
        private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
        private YellowstonePathology.Business.Test.AliquotOrder m_AliquotOrder;
        private TestOrderPanelSetOrderViewCollection m_TestOrderPanelSetOrderViewCollection;

        public TestOrderSelectionPage(TestOrderPanelSetOrderViewCollection testOrderPanelSetOrderViewCollection, 
            YellowstonePathology.Business.Test.AliquotOrder aliquotOrder,
            YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {
            this.m_TestOrderPanelSetOrderViewCollection = testOrderPanelSetOrderViewCollection;
            this.m_AliquotOrder = aliquotOrder;
            this.m_AccessionOrder = accessionOrder;            

			InitializeComponent();
			DataContext = this;            
		}

        public YellowstonePathology.Business.Test.AliquotOrder AliquotOrder
        {
            get { return this.m_AliquotOrder; }
        }

        public YellowstonePathology.Business.Test.AccessionOrder AccessionOrder
        {
            get { return this.m_AccessionOrder; }
        }

        public TestOrderPanelSetOrderViewCollection TestOrderPanelSetOrderViewCollection
        {
            get { return this.m_TestOrderPanelSetOrderViewCollection; }
        }        

        private void ListBoxTestOrders_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.ListViewTestOrderPanelSetOrderViewCollection.SelectedItem != null)
            {
                TestOrderPanelSetOrderView testOrderPanelSetOrderView = (TestOrderPanelSetOrderView)this.ListViewTestOrderPanelSetOrderViewCollection.SelectedItem;
                YellowstonePathology.Business.Test.Model.TestOrder testOrder = this.m_AccessionOrder.PanelSetOrderCollection.GetTestOrderByTestOrderId(testOrderPanelSetOrderView.TestOrderId);
                YellowstonePathology.Business.Test.AliquotOrder aliquotOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetAliquotOrderByTestOrderId(testOrder.TestOrderId);
                this.TestOrderSelected(this, new CustomEventArgs.TestOrderAliquotOrderReturnEventArgs(testOrder, aliquotOrder));
            }
        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            this.Back(this, new YellowstonePathology.UI.CustomEventArgs.MasterAccessionNoReturnEventArgs(this.m_AccessionOrder.MasterAccessionNo));
        }  		      

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }                                 
	}
}
