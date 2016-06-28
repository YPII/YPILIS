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

namespace YellowstonePathology.UI
{
	public partial class LabEventsControlTab : TabItem, INotifyPropertyChanged
	{
		public delegate void PropertyChangedNotificationHandler(String info);
		public event PropertyChangedEventHandler PropertyChanged;

		YellowstonePathology.Business.Domain.OrderCommentLogCollection m_OrderCommentLog;
		YellowstonePathology.Business.Domain.OrderCommentCollection m_OrderComments;

		YellowstonePathology.Business.Interface.IOrder m_CurrentOrder;
		YellowstonePathology.Business.User.SystemIdentity m_SystemIdentity;

		public LabEventsControlTab(YellowstonePathology.Business.User.SystemIdentity systemIdentity)
		{
            this.m_SystemIdentity = systemIdentity;

			this.m_OrderCommentLog = new YellowstonePathology.Business.Domain.OrderCommentLogCollection();
			this.m_OrderComments = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetAllLabEvents();

			InitializeComponent();
			this.DataContext = this;
		}

		public string LabEventLogHeader
		{
			get
			{
				string header = "Event Log";
				if (this.m_OrderCommentLog.Count > 0) header = header + " (" + this.m_OrderCommentLog.Count.ToString() + ")";
				return header;
			}
		}

		public YellowstonePathology.Business.Domain.OrderCommentLogCollection OrderCommentLog
		{
			get { return this.m_OrderCommentLog; }
			set
			{
				this.m_OrderCommentLog = value;
				this.NotifyPropertyChanged("OrderCommentLog");
			}
		}

		public YellowstonePathology.Business.Domain.OrderCommentCollection OrderComments
		{
			get { return this.m_OrderComments; }
		}

		public void LogOrderComment(YellowstonePathology.Business.Interface.IOrderComment orderComment)
		{			
			if (this.m_CurrentOrder == null)
			{
				MessageBox.Show("An case must be selected to create an event", "Select a case");
				return;
			}

			YellowstonePathology.UI.Login.LabEventLogEntryDialog labEventLogEntryDialog = new YellowstonePathology.UI.Login.LabEventLogEntryDialog(this.m_SystemIdentity);
			YellowstonePathology.Business.Domain.OrderCommentLog orderCommentLog = labEventLogEntryDialog.CreateOrderCommentLog(this.m_CurrentOrder, orderComment);

			this.m_OrderCommentLog.Add(orderCommentLog);
			labEventLogEntryDialog.ShowDialog();
			this.OrderCommentLog = YellowstonePathology.Business.Gateway.OrderCommentGateway.GetOrderCommentsForSpecimenLogId(this.m_CurrentOrder.SpecimenLogId);
			this.NotifyPropertyChanged("LabEventLogHeader");
		}

		public void SetCurrentOrder(YellowstonePathology.Business.Interface.IOrder order)
		{
            if (order != null)
            {
                this.m_CurrentOrder = order;
				this.OrderCommentLog = YellowstonePathology.Business.Gateway.OrderCommentGateway.GetOrderCommentsForSpecimenLogId(this.m_CurrentOrder.SpecimenLogId);
				if (this.m_OrderCommentLog == null)
				{
					this.m_OrderCommentLog = new Business.Domain.OrderCommentLogCollection();
				}
                this.NotifyPropertyChanged("LabEventLogHeader");
            }
		}

		private void ButtonLogEvent_Click(object sender, RoutedEventArgs e)
		{
			if (this.ListViewLabEvents.SelectedItem != null)
			{
				YellowstonePathology.Business.Interface.IOrderComment orderComment = (YellowstonePathology.Business.Interface.IOrderComment)this.ListViewLabEvents.SelectedItem;
				this.LogOrderComment(orderComment);
			}
			else
			{
				MessageBox.Show("Please select an event to log.");
			}
		}

		private void ButtonViewEvent_Click(object sender, RoutedEventArgs e)
		{
			if (this.ListViewLabEventLog.SelectedItem != null)
			{
				YellowstonePathology.Business.Domain.OrderCommentLog orderCommentLog = (YellowstonePathology.Business.Domain.OrderCommentLog)this.ListViewLabEventLog.SelectedItem;
				YellowstonePathology.UI.Login.LabEventLogEntryDialog labEventLogEntryDialog = new YellowstonePathology.UI.Login.LabEventLogEntryDialog(this.m_SystemIdentity);
				labEventLogEntryDialog.SetEventLog(orderCommentLog);
				labEventLogEntryDialog.ShowDialog();
			}
			else
			{
				MessageBox.Show("Please select an event log item to view.");
			}
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
