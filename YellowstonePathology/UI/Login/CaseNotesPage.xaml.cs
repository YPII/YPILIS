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
		private YellowstonePathology.Business.Domain.CaseNotesKeyCollection m_CaseNotesKeyCollection;
		private string m_PageHeaderText = "Case Notes";		

		public CaseNotesPage(YellowstonePathology.UI.Navigation.PageNavigator pageNavigator, YellowstonePathology.Business.Domain.CaseNotesKeyCollection caseNotesKeyCollection)
		{
			this.m_PageNavigator = pageNavigator;
            this.m_SystemIdentity = YellowstonePathology.Business.User.SystemIdentity.Instance;
			this.m_CaseNotesKeyCollection = caseNotesKeyCollection;
			this.FillOrderCommentLog();

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

		private void BorderPanelSetOrderHeader_Loaded(object sender, RoutedEventArgs e)
		{

		}

		private void FillOrderCommentLog()
		{
			this.m_OrderCommentLogCollection = YellowstonePathology.Business.Gateway.OrderCommentGateway.OrderCommentLogCollectionFromCaseNotesKeys(this.m_CaseNotesKeyCollection);			
		}

		private void AddComment()
		{
			if (this.m_CaseNotesKeyCollection.HasId(YellowstonePathology.Business.Domain.CaseNotesKeyNameEnum.MasterAccessionNo))
			{
				string masterAccessionNo = this.m_CaseNotesKeyCollection.GetId(YellowstonePathology.Business.Domain.CaseNotesKeyNameEnum.MasterAccessionNo);
				this.m_OrderCommentLogCollection.Add(Business.Domain.OrderCommentEnum.ClientOrderAccessioned, this.m_SystemIdentity.User, masterAccessionNo, 0, string.Empty, string.Empty);
			}
			else if (this.m_CaseNotesKeyCollection.HasId(YellowstonePathology.Business.Domain.CaseNotesKeyNameEnum.ClientOrderId))
			{
				string clientOrderId = this.m_CaseNotesKeyCollection.GetId(YellowstonePathology.Business.Domain.CaseNotesKeyNameEnum.ClientOrderId);
				this.m_OrderCommentLogCollection.Add(Business.Domain.OrderCommentEnum.ClientOrderAccessioned, this.m_SystemIdentity.User, string.Empty, 0, clientOrderId, string.Empty);
			}
			
			this.FillOrderCommentLog();
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

		public YellowstonePathology.Business.Domain.OrderCommentLog SelectedOrderCommentLog
		{
			get { return (YellowstonePathology.Business.Domain.OrderCommentLog)this.ListViewOrderCommentList.SelectedItem; }
		}

		private void ButtonEditComment_Click(object sender, RoutedEventArgs e)
		{
			if (SelectedOrderCommentLog != null)
			{
				this.ShowCaseNoteDetailsPage(this.SelectedOrderCommentLog);
			}
		}

		private void ButtonAddComment_Click(object sender, RoutedEventArgs e)
		{
			this.AddComment();
		}

		private void ButtonDeleteComment_Click(object sender, RoutedEventArgs e)
		{
			if (SelectedOrderCommentLog != null)
			{
				this.m_OrderCommentLogCollection.Remove(this.SelectedOrderCommentLog);
			}
		}

		private void ButtonBack_Click(object sender, RoutedEventArgs e)
		{
			UI.Navigation.PageNavigationReturnEventArgs args = new UI.Navigation.PageNavigationReturnEventArgs(UI.Navigation.PageNavigationDirectionEnum.Back, null);
			this.Return(this, args);
		}		
	}
}
