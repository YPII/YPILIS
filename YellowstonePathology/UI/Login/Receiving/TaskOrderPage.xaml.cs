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
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace YellowstonePathology.UI.Login.Receiving
{
	/// <summary>
	/// Interaction logic for TaskOrderPage.xaml
	/// </summary>
	public partial class TaskOrderPage : UserControl, INotifyPropertyChanged 
	{
		public event PropertyChangedEventHandler PropertyChanged;

		public delegate void NextEventHandler(object sender, EventArgs e);
		public event NextEventHandler Next;

		public delegate void BackEventHandler(object sender, EventArgs e);
		public event BackEventHandler Back;

		public delegate void CloseEventHandler(object sender, EventArgs e);
		public event CloseEventHandler Close;		
				
		private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;		
		private YellowstonePathology.Business.Task.Model.TaskOrder m_TaskOrder;
		private PageNavigationModeEnum m_PageNavigationMode;
        private List<string> m_TaskAssignmentList;

		public TaskOrderPage(YellowstonePathology.Business.Test.AccessionOrder accessionOrder,
			YellowstonePathology.Business.Task.Model.TaskOrder taskOrder,
			PageNavigationModeEnum pageNavigationMode)
		{
			this.m_AccessionOrder = accessionOrder;
			this.m_TaskOrder = taskOrder;
			this.m_PageNavigationMode = pageNavigationMode;			

			this.m_TaskAssignmentList = YellowstonePathology.Business.Task.Model.TaskAssignment.GetTaskAssignmentList();

			InitializeComponent();

			this.SetButtonVisibility();
			DataContext = this;

            Loaded += TaskOrderPage_Loaded;
            Unloaded += TaskOrderPage_Unloaded;
		}

        private void TaskOrderPage_Loaded(object sender, RoutedEventArgs e)
        {
             
        }

        private void TaskOrderPage_Unloaded(object sender, RoutedEventArgs e)
        {
             
        }

        public void NotifyPropertyChanged(String info)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(info));
			}
		}

		private void SetButtonVisibility()
		{
			switch (this.m_PageNavigationMode)
			{
				case PageNavigationModeEnum.Inline:
					this.ButtonBack.Visibility = System.Windows.Visibility.Visible;
					this.ButtonClose.Visibility = System.Windows.Visibility.Collapsed;
					this.ButtonNext.Visibility = System.Windows.Visibility.Visible;
					break;
				case PageNavigationModeEnum.Standalone:
					this.ButtonBack.Visibility = System.Windows.Visibility.Collapsed;
					this.ButtonClose.Visibility = System.Windows.Visibility.Visible;
					this.ButtonNext.Visibility = System.Windows.Visibility.Collapsed;
					break;
			}
		}

        public List<string> TaskAssignmentList
        {
            get { return this.m_TaskAssignmentList; }
        }

		public YellowstonePathology.Business.Task.Model.TaskOrder TaskOrder
		{
			get { return this.m_TaskOrder; }
		}

		public YellowstonePathology.Business.Test.AccessionOrder AccessionOrder
		{
			get { return this.m_AccessionOrder; }
		}

        private void ButtonNext_Click(object sender, RoutedEventArgs e)
        {
            if (this.Next != null)
            {
                YellowstonePathology.Business.Persistence.DocumentGateway.Instance.Save();
                this.Next(this, new EventArgs());
            }
        }

		private void ButtonBack_Click(object sender, RoutedEventArgs e)
		{
			if (this.Back != null) this.Back(this, new EventArgs());
		}

		private void ButtonClose_Click(object sender, RoutedEventArgs e)
		{
			if (this.Close != null) this.Close(this, new EventArgs());
		}        

		private void HyperLinkAfterSlidePreparation_Click(object sender, RoutedEventArgs e)
		{
			YellowstonePathology.Business.Task.Model.TaskAfterSlidePreparation task = new Business.Task.Model.TaskAfterSlidePreparation();
			this.HandleAddTask(task);
		}

		private void HyperLinkParaffinCurlPreparation_Click(object sender, RoutedEventArgs e)
		{
			YellowstonePathology.Business.Task.Model.TaskParaffinCurlPreparation task = new Business.Task.Model.TaskParaffinCurlPreparation();
			this.HandleAddTask(task);			
		}

		private void HyperLinkUnstainedSlidePreparation_Click(object sender, RoutedEventArgs e)
		{
			YellowstonePathology.Business.Task.Model.TaskUnstainedSlideWithAfterSlidePreparation task = new Business.Task.Model.TaskUnstainedSlideWithAfterSlidePreparation();
			this.HandleAddTask(task);
		}

		private void HyperLinkMicrodissectionForMolecular_Click(object sender, RoutedEventArgs e)
		{
			YellowstonePathology.Business.Task.Model.TaskMicrodissectionForMolecular task = new Business.Task.Model.TaskMicrodissectionForMolecular();
			this.HandleAddTask(task);			
		}

        private void HyperLinkSendBlockToNeogenomics_Click(object sender, RoutedEventArgs e)
        {
			YellowstonePathology.Business.Task.Model.TaskSendBlockToNeogenomics task = new Business.Task.Model.TaskSendBlockToNeogenomics();
            this.HandleAddTask(task);
        }

		private void HandleAddTask(YellowstonePathology.Business.Task.Model.Task task)
		{
			string taskOrderDetailId = YellowstonePathology.Business.OrderIdParser.GetNextTaskOrderDetailId(this.m_TaskOrder.TaskOrderDetailCollection, this.m_TaskOrder.TaskOrderId);
			string objectId = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
			YellowstonePathology.Business.Task.Model.TaskOrderDetail taskOrderDetail = new Business.Task.Model.TaskOrderDetail(taskOrderDetailId, this.m_TaskOrder.TaskOrderId, objectId, task);
            this.m_TaskOrder.TaskOrderDetailCollection.Add(taskOrderDetail);                            
		}        

        private void HyperlingPrintTaskOrder_Click(object sender, RoutedEventArgs e)
        {
            this.PrintTaskOrder(1);
        }

        private void HyperlingPrint2CopiesTaskOrder_Click(object sender, RoutedEventArgs e)
        {
            this.PrintTaskOrder(2);
        }

        private void PrintTaskOrder(int copyCount)
        {
            Receiving.TaskOrderDataSheet taskOrderDataSheet = new Receiving.TaskOrderDataSheet(this.m_TaskOrder, this.m_AccessionOrder);
            System.Printing.PrintQueue printQueue = new System.Printing.LocalPrintServer().DefaultPrintQueue;
            System.Windows.Controls.PrintDialog printDialog = new System.Windows.Controls.PrintDialog();
            printDialog.PrintTicket.PageOrientation = System.Printing.PageOrientation.Portrait;
            printDialog.PrintTicket.CopyCount = copyCount;
            printDialog.PrintQueue = printQueue;
            printDialog.PrintDocument(taskOrderDataSheet.FixedDocument.DocumentPaginator, "Task Order Data Sheet");
            MessageBox.Show("This task has been submitted to the printer.");
        }

        private void HyperLinkAcknowledge_Click(object sender, RoutedEventArgs e)
        {
            Hyperlink hyperLink = (Hyperlink)e.Source;
			YellowstonePathology.Business.Task.Model.TaskOrderDetail taskOrderDetail = (YellowstonePathology.Business.Task.Model.TaskOrderDetail)hyperLink.Tag;
            taskOrderDetail.Acknowledged = true;
            taskOrderDetail.AcknowledgedById = Business.User.SystemIdentity.Instance.User.UserId;
            taskOrderDetail.AcknowledgedDate = DateTime.Now;
            taskOrderDetail.AcknowledgedByInitials = Business.User.SystemIdentity.Instance.User.Initials;

            if (this.m_TaskOrder.TaskOrderDetailCollection.HasUnacknowledgeItems() == false)
            {
                this.m_TaskOrder.Acknowledged = true;
                this.m_TaskOrder.AcknowledgedDate = DateTime.Now;
            }
        }        

        private void HyperLinkDelete_Click(object sender, RoutedEventArgs e)
        {
            Hyperlink control = (Hyperlink)sender;
			YellowstonePathology.Business.Task.Model.TaskOrderDetail taskOrderDetail = (YellowstonePathology.Business.Task.Model.TaskOrderDetail)control.Tag;
            MessageBoxResult result = MessageBox.Show("Delete the selected Task", "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes);
            if (result == MessageBoxResult.Yes)
            {
                this.m_TaskOrder.TaskOrderDetailCollection.Remove(taskOrderDetail);
            }
        }

        private void HyperLinkUnacknowledge_Click(object sender, RoutedEventArgs e)
        {
            Hyperlink hyperLink = (Hyperlink)e.Source;
			YellowstonePathology.Business.Task.Model.TaskOrderDetail taskOrderDetail = (YellowstonePathology.Business.Task.Model.TaskOrderDetail)hyperLink.Tag;
            taskOrderDetail.Acknowledged = false;
            taskOrderDetail.AcknowledgedById = null;
            taskOrderDetail.AcknowledgedDate = null;
            taskOrderDetail.AcknowledgedByInitials = null;

            if (this.m_TaskOrder.TaskOrderDetailCollection.HasAcknowledgeItems() == false)
            {
                this.m_TaskOrder.Acknowledged = false;
                this.m_TaskOrder.AcknowledgedDate = null;
            }
        }             
	}
}
