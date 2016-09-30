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

namespace YellowstonePathology.UI.Login
{
	/// <summary>
	/// Interaction logic for CaseNoteDetails.xaml
	/// </summary>
	public partial class CaseNoteDetailsPage : UserControl
	{
		public delegate void ReturnEventHandler(object sender, UI.Navigation.PageNavigationReturnEventArgs e);
		public event ReturnEventHandler Return;

		private YellowstonePathology.Business.Domain.OrderCommentLog m_OrderCommentLog;
		private string m_PageHeaderText = "Case Note Details";

		public CaseNoteDetailsPage(YellowstonePathology.Business.Domain.OrderCommentLog orderCommentLog)
		{
			this.m_OrderCommentLog = orderCommentLog;
			InitializeComponent();
			DataContext = this;
		}

		public string PageHeaderText
		{
			get { return this.m_PageHeaderText; }
		}

		public YellowstonePathology.Business.Domain.OrderCommentLog OrderCommentLog
		{
			get { return this.m_OrderCommentLog; }
		}

		private void ButtonBack_Click(object sender, RoutedEventArgs e)
		{
			UI.Navigation.PageNavigationReturnEventArgs args = new UI.Navigation.PageNavigationReturnEventArgs(UI.Navigation.PageNavigationDirectionEnum.Back, null);
			this.Return(this, args);
		}

		private void ButtonNext_Click(object sender, RoutedEventArgs e)
		{
			UI.Navigation.PageNavigationReturnEventArgs args = new UI.Navigation.PageNavigationReturnEventArgs(UI.Navigation.PageNavigationDirectionEnum.Next, null);
			this.Return(this, args);
		}		
	}
}
