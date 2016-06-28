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
using System.Windows.Shapes;
using System.ComponentModel;

namespace YellowstonePathology.UI.Login
{
    /// <summary>
    /// Interaction logic for EventEntryDialog.xaml
    /// </summary>
    public partial class LabEventLogEntryDialog : Window, INotifyPropertyChanged
    {
        public delegate void PropertyChangedNotificationHandler(String info);
        public event PropertyChangedEventHandler PropertyChanged;

		YellowstonePathology.Business.Domain.OrderCommentLog m_OrderCommentLog;       
		YellowstonePathology.Business.User.SystemIdentity m_SystemIdentity;

		public LabEventLogEntryDialog(YellowstonePathology.Business.User.SystemIdentity systemIdentity)
        {
            this.m_SystemIdentity = systemIdentity;

            InitializeComponent();

            this.DataContext = this;
            this.Loaded += new RoutedEventHandler(LabEventLogEntryDialog_Loaded);
        }

        private void LabEventLogEntryDialog_Loaded(object sender, RoutedEventArgs e)
        {
            this.TextBoxComment.Focus();
        }

		public YellowstonePathology.Business.Domain.OrderCommentLog OrderCommentLog
        {
			get { return this.m_OrderCommentLog; }
			set { this.m_OrderCommentLog = value; }
        }

		public void SetEventLog(YellowstonePathology.Business.Domain.OrderCommentLog orderCommentLog)
        {
			this.m_OrderCommentLog = orderCommentLog;			
        }

		public YellowstonePathology.Business.Domain.OrderCommentLog CreateOrderCommentLog(YellowstonePathology.Business.Interface.IOrder order, YellowstonePathology.Business.Interface.IOrderComment orderComment)
        {
			string objectId = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
			this.m_OrderCommentLog = new YellowstonePathology.Business.Domain.OrderCommentLog(objectId);
			this.m_OrderCommentLog.SetDefaultValues(this.m_SystemIdentity.User);
			this.m_OrderCommentLog.ClientId = order.ClientId;
			this.m_OrderCommentLog.MasterAccessionNo = order.MasterAccessionNo;
			this.m_OrderCommentLog.SpecimenLogId = order.SpecimenLogId;
			this.m_OrderCommentLog.FromEvent(orderComment);

            YellowstonePathology.Business.Persistence.DocumentGateway.Instance.InsertDocument(this.m_OrderCommentLog, this);
			return this.m_OrderCommentLog;
        }

        private void ButtonOK_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            YellowstonePathology.Business.Persistence.DocumentGateway.Instance.Push(this);
            this.Close();
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
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
