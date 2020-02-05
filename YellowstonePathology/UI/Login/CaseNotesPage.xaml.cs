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

namespace YellowstonePathology.UI.Login
{
	/// <summary>
	/// Interaction logic for CaseNotesPage.xaml
	/// </summary>
	public partial class CaseNotesPage : UserControl, INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;
		public delegate void ReturnEventHandler(object sender, UI.Navigation.PageNavigationReturnEventArgs e);
		public event ReturnEventHandler Return;

		private YellowstonePathology.UI.Navigation.PageNavigator m_PageNavigator;
		private YellowstonePathology.Business.User.SystemIdentity m_SystemIdentity;
		private YellowstonePathology.Business.Domain.OrderCommentLogCollection m_OrderCommentLogCollection;
		private Business.Test.AccessionOrder m_AccessionOrder;
		private string m_PageHeaderText = "Case Notes";		

		public CaseNotesPage(YellowstonePathology.UI.Navigation.PageNavigator pageNavigator, Business.Test.AccessionOrder accessionOrder)
		{
			this.m_PageNavigator = pageNavigator;
			this.m_AccessionOrder = accessionOrder;
            this.m_SystemIdentity = YellowstonePathology.Business.User.SystemIdentity.Instance;
			this.m_OrderCommentLogCollection = YellowstonePathology.Business.Gateway.OrderCommentGateway.GetOrderCommentLogCollectionByMasterAccessionNo(this.m_AccessionOrder.MasterAccessionNo);

			InitializeComponent();
			DataContext = this;
		}

		public void NotifyPropertyChanged(String info)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(info));
			}
		}

		public string PageHeaderText
		{
			get { return this.m_PageHeaderText; }
		}

		public YellowstonePathology.Business.Domain.OrderCommentLogCollection OrderCommentLogCollection
		{
			get { return this.m_OrderCommentLogCollection; }
		}

		private void AddComment(string category)
		{
			this.m_OrderCommentLogCollection.Add(this.m_SystemIdentity.User, this.m_AccessionOrder.MasterAccessionNo, this.m_AccessionOrder.ClientId, string.Empty, category);
            YellowstonePathology.Business.Persistence.DocumentGateway.Instance.InsertDocument(this.m_OrderCommentLogCollection[this.m_OrderCommentLogCollection.Count - 1], this);

            this.NotifyPropertyChanged("OrderCommentLogCollection");
			this.ShowCaseNoteDetailsPage(this.m_OrderCommentLogCollection[this.m_OrderCommentLogCollection.Count - 1]);
		}

		private void ShowCaseNoteDetailsPage(YellowstonePathology.Business.Domain.OrderCommentLog orderCommentLog)
		{
			CaseNoteDetailsPage caseNoteDetailsPage = new CaseNoteDetailsPage(orderCommentLog);
			caseNoteDetailsPage.Return += new CaseNoteDetailsPage.ReturnEventHandler(CaseNoteDetailsPage_Return);
			this.m_PageNavigator.Navigate(caseNoteDetailsPage);
		}

		private void CaseNoteDetailsPage_Return(object sender, UI.Navigation.PageNavigationReturnEventArgs e)
		{
			switch (e.PageNavigationDirectionEnum)
			{
				case UI.Navigation.PageNavigationDirectionEnum.Next:
					this.m_PageNavigator.Navigate(this);
					break;
				case UI.Navigation.PageNavigationDirectionEnum.Back:
					this.m_PageNavigator.Navigate(this);
					break;
			}
		}

		private YellowstonePathology.Business.Domain.OrderCommentLog SelectedOrderCommentLog
		{
			get { return (YellowstonePathology.Business.Domain.OrderCommentLog)this.ListViewOrderCommentList.SelectedItem; }
		}

		private void ButtonEditComment_Click(object sender, RoutedEventArgs e)
		{
			if (SelectedOrderCommentLog != null)
			{
                YellowstonePathology.Business.Domain.OrderCommentLog orderCommentLog = YellowstonePathology.Business.Persistence.DocumentGateway.Instance.PullOrderCommentLog(this.SelectedOrderCommentLog.OrderCommentLogId, this);
                this.m_OrderCommentLogCollection[this.ListViewOrderCommentList.SelectedIndex] = orderCommentLog;
                this.ShowCaseNoteDetailsPage(orderCommentLog);
			}
		}

		private void ButtonAddComment_Click(object sender, RoutedEventArgs e)
		{
			this.AddComment(null);
		}

		private void ButtonDeleteComment_Click(object sender, RoutedEventArgs e)
		{
			if (SelectedOrderCommentLog != null)
			{
                YellowstonePathology.Business.Persistence.DocumentGateway.Instance.DeleteDocument(this.SelectedOrderCommentLog, this);
				this.m_OrderCommentLogCollection.Remove(this.SelectedOrderCommentLog);

            }
		}

		private void ButtonAddQualityImprovement_Click(object sender, RoutedEventArgs e)
		{
			this.AddComment("Quality Improvement");
		}

		private void ButtonDone_Click(object sender, RoutedEventArgs e)
		{
			UI.Navigation.PageNavigationReturnEventArgs args = new UI.Navigation.PageNavigationReturnEventArgs(UI.Navigation.PageNavigationDirectionEnum.Back, null);
			this.Return(this, args);
		}
	}
}
