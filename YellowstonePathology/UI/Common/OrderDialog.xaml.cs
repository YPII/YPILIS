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
using System.Xml;
using System.Xml.Linq;
using System.Windows.Resources;
using System.ComponentModel;

namespace YellowstonePathology.UI.Common
{
	/// <summary>
	/// Interaction logic for OrderDialog.xaml
	/// </summary>
	public partial class OrderDialog : Window, INotifyPropertyChanged
	{
        public delegate void PropertyChangedNotificationHandler(String info);
        public event PropertyChangedEventHandler PropertyChanged;

        private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;        
        private OrderItemView m_OrderItemView;
        private YellowstonePathology.UI.Login.FinalizeAccession.AliquotAndStainOrderView m_AliquotAndStainOrderView;		
        private string m_PanelOrderComment;
		private YellowstonePathology.Business.Test.PanelSetOrder m_PanelSetOrder;
        private YellowstonePathology.Business.Visitor.StainAcknowledgementTaskOrderVisitor m_StainAcknowledgementTaskOrderVisitor;

		public OrderDialog(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder)
		{
            this.m_AccessionOrder = accessionOrder;
			this.m_PanelSetOrder = panelSetOrder;            

            this.m_OrderItemView = new OrderItemView();
            this.m_StainAcknowledgementTaskOrderVisitor = new Business.Visitor.StainAcknowledgementTaskOrderVisitor(this.m_PanelSetOrder);
            this.m_AliquotAndStainOrderView = new Login.FinalizeAccession.AliquotAndStainOrderView(this.m_AccessionOrder, this.m_PanelSetOrder);

            InitializeComponent();

			this.DataContext = this;			
            this.Loaded += new RoutedEventHandler(OrderWorkspace_Loaded);
            this.Closing += OrderDialog_Closing;

		}

        private void OrderDialog_Closing(object sender, CancelEventArgs e)
        {
            //YellowstonePathology.Business.Persistence.DocumentGateway.Instance.Save();
        }

        private void OrderWorkspace_Loaded(object sender, RoutedEventArgs e)
        {
            this.ButtonOrder.Focus();
        }

        public string PanelOrderComment
        {
            get { return this.m_PanelOrderComment; }
            set 
            {
                if (this.m_PanelOrderComment != value)
                {
                    this.m_PanelOrderComment = value;
                    this.NotifyPropertyChanged("PanelOrderComment");
                }
            }
        }

        public YellowstonePathology.Business.Test.AccessionOrder AccessionOrder
        {
            get { return this.m_AccessionOrder; }
        }

		public string ReportNo
		{
			get { return this.m_PanelSetOrder.ReportNo; }
		}

        public YellowstonePathology.UI.Login.FinalizeAccession.AliquotAndStainOrderView AliquotAndStainOrderView
        {
            get { return this.m_AliquotAndStainOrderView; }
        }

        public OrderItemView OrderItemView
        {
            get { return this.m_OrderItemView; }
        }	        	   

		private void ButtonClose_Click(object sender, RoutedEventArgs e)
		{			
            this.Close();
		}				

		private void ContextMenuCancelPanel_Opened(object sender, RoutedEventArgs e)
		{
			ContextMenu contextMenu = (ContextMenu)sender;
			if (((YellowstonePathology.Business.Test.PanelOrder)contextMenu.Tag).PanelId == 14 ||
				((YellowstonePathology.Business.Test.PanelOrder)contextMenu.Tag).TestOrderCollection.Count > 0)
			{
				foreach (MenuItem menuItem in contextMenu.Items)
				{
					menuItem.IsEnabled = false;
				}
			}
		}				
		
		private void CheckBoxStain_Checked(object sender, RoutedEventArgs e)
		{
			XElement elementToSelect = ((XElement)((CheckBox)sender).Tag);
            this.m_OrderItemView.SelectAnElement(elementToSelect);
		}

		private void CheckBoxStain_Unchecked(object sender, RoutedEventArgs e)
		{
			XElement elementToUnselect = ((XElement)((CheckBox)sender).Tag);
            this.m_OrderItemView.UnSelectAnElement(elementToUnselect);
		}			

		private void MoveToNextElement()
		{
			IInputElement focusedElement = Keyboard.FocusedElement;
			FrameworkElement element = (FrameworkElement)focusedElement;
			element.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
		}		

		private void CheckBoxStainColor_Checked(object sender, RoutedEventArgs e)
		{
            this.m_PanelOrderComment += "Melanoma stains are to be brown. ";
            this.NotifyPropertyChanged("TestOrderComment");
		}

		private void CheckBoxStainColor_Unchecked(object sender, RoutedEventArgs e)
		{
            this.m_PanelOrderComment = this.m_PanelOrderComment.Replace("Melanoma stains are to be brown. ", string.Empty);
            this.NotifyPropertyChanged("TestOrderComment");
		}		        

        private void ButtonOrder_Click(object sender, RoutedEventArgs e)
        {
            ((MainWindow)Application.Current.MainWindow).MoveKeyboardInputToNext();

            YellowstonePathology.Business.Test.AliquotOrderCollection selectedAliquots = this.m_AliquotAndStainOrderView.GetSelectedAliquots();
            if (selectedAliquots.Count > 0)
            {
                YellowstonePathology.Business.Test.Model.TestCollection selectedTests = this.m_OrderItemView.GetSelectedTests();
                List<YellowstonePathology.Business.Test.Model.DualStain> selectedDualStains = this.m_OrderItemView.GetSelectedDualStains();

                if (selectedTests.Count > 0 || selectedDualStains.Count > 0)
                {                    
                    this.HandleOrderingTests(selectedAliquots, selectedTests);
                    this.HandleOrderingDualStains(selectedAliquots, selectedDualStains);

                    this.m_StainAcknowledgementTaskOrderVisitor.TaskOrderDetailComment = this.m_PanelOrderComment;
                    this.m_AccessionOrder.TakeATrip(this.m_StainAcknowledgementTaskOrderVisitor);

                    this.m_OrderItemView.ClearSelectedItems();
                    this.NotifyPropertyChanged("OrderItemView");

                    this.m_AliquotAndStainOrderView.Refresh(false, this.m_PanelSetOrder);                    
                    this.NotifyPropertyChanged("AliquotAndStainOrderView");

                    this.m_PanelOrderComment = null;
                    this.NotifyPropertyChanged("PanelOrderComment");
                }               
                else
                {
                    MessageBox.Show("There are no tests selected.");
                }
            }
            else
            {
                MessageBox.Show("There are no aliquots selected.");
            }
        }       

        private void HandleOrderingDualStains(YellowstonePathology.Business.Test.AliquotOrderCollection selectedAliquots, List<YellowstonePathology.Business.Test.Model.DualStain> selectedDualStains)
        {            
            foreach (YellowstonePathology.Business.Test.AliquotOrder aliquotOrder in selectedAliquots)
            {
                foreach (YellowstonePathology.Business.Test.Model.DualStain dualStain in selectedDualStains)
                {
                    bool orderAsDual = true;
                    bool acknowledgeOnOrder = false;

                    YellowstonePathology.Business.Visitor.OrderTestVisitor orderTestVisitorFirstTest = new Business.Visitor.OrderTestVisitor(this.m_PanelSetOrder.ReportNo, dualStain.FirstTest, dualStain.FirstTest.OrderComment, this.m_PanelOrderComment, orderAsDual, aliquotOrder, acknowledgeOnOrder, false, this.m_AccessionOrder.TaskOrderCollection);
                    this.m_AccessionOrder.TakeATrip(orderTestVisitorFirstTest);
                    this.m_StainAcknowledgementTaskOrderVisitor.AddTestOrder(orderTestVisitorFirstTest.TestOrder);

                    YellowstonePathology.Business.Visitor.OrderTestVisitor orderTestVisitorSecondTest = new Business.Visitor.OrderTestVisitor(this.m_PanelSetOrder.ReportNo, dualStain.SecondTest, dualStain.SecondTest.OrderComment, this.m_PanelOrderComment, orderAsDual, aliquotOrder, acknowledgeOnOrder, false, this.m_AccessionOrder.TaskOrderCollection);
                    this.m_AccessionOrder.TakeATrip(orderTestVisitorSecondTest);
                    this.m_StainAcknowledgementTaskOrderVisitor.AddTestOrder(orderTestVisitorSecondTest.TestOrder);
                }
            }                
        }

        private void HandleOrderingTests(YellowstonePathology.Business.Test.AliquotOrderCollection selectedAliquots, YellowstonePathology.Business.Test.Model.TestCollection selectedTests)
        {            
            foreach (YellowstonePathology.Business.Test.AliquotOrder aliquotOrder in selectedAliquots)
            {
                foreach (YellowstonePathology.Business.Test.Model.Test test in selectedTests)
                {                                                        
                    bool orderAsDual = false;
                    bool acknowledgeOnOrder = false;                    

                    YellowstonePathology.Business.Visitor.OrderTestVisitor orderTestVisitorTest = new Business.Visitor.OrderTestVisitor(this.m_PanelSetOrder.ReportNo, test, test.OrderComment, this.m_PanelOrderComment, orderAsDual, aliquotOrder, acknowledgeOnOrder, false, this.m_AccessionOrder.TaskOrderCollection);
                    this.m_AccessionOrder.TakeATrip(orderTestVisitorTest);
                    this.m_StainAcknowledgementTaskOrderVisitor.AddTestOrder(orderTestVisitorTest.TestOrder);
                }
            }            
        }		

        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Test.Model.TestOrderCollection selectedTestOrders = this.m_AliquotAndStainOrderView.GetSelectedTestOrders();            

            if (selectedTestOrders.Count != 0)
            {
                foreach (YellowstonePathology.Business.Test.Model.TestOrder testOrder in selectedTestOrders)
                {
                    if (this.m_AliquotAndStainOrderView.HaveSlidesBeenMadeForTestOrder(testOrder) == false)
                    {
                        YellowstonePathology.Business.Visitor.RemoveTestOrderVisitor removeTestOrderVisitor = new Business.Visitor.RemoveTestOrderVisitor(testOrder.TestOrderId);
                        this.m_AccessionOrder.TakeATrip(removeTestOrderVisitor);
                        this.m_StainAcknowledgementTaskOrderVisitor.RemoveTestOrder(testOrder);
                    }
                    else
                    {
                        MessageBox.Show("The test " + testOrder.TestName + " cannot be deleted because slides have been made.");
                    }
                }

                this.m_AccessionOrder.TakeATrip(this.m_StainAcknowledgementTaskOrderVisitor);
                this.m_AliquotAndStainOrderView.Refresh(false, this.m_PanelSetOrder);
                this.NotifyPropertyChanged("AliquotAndStainOrderView");
            }
            else
            {
                MessageBox.Show("No tests selected.");
            }
        }

        protected void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        private void CheckBoxPanelTest_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void CheckBoxPanelTest_Unchecked(object sender, RoutedEventArgs e)
        {

        }              
	}
}
