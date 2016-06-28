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

namespace YellowstonePathology.UI.Login.FinalizeAccession
{
	/// <summary>
	/// Interaction logic for AssignmentPage.xaml
	/// </summary>
	public partial class PatientHistoryPage : UserControl
	{
		public delegate void ReturnEventHandler(object sender, UI.Navigation.PageNavigationReturnEventArgs e);
		public event ReturnEventHandler Return;

		private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
		private string m_PageHeaderText;
		private string m_Message;

        YellowstonePathology.UI.Common.CaseHistoryPage m_CaseHistoryPage;

        public PatientHistoryPage(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string message)
		{
			this.m_AccessionOrder = accessionOrder;
			this.m_Message = message;
            this.m_CaseHistoryPage = new Common.CaseHistoryPage(this.m_AccessionOrder);
            this.m_PageHeaderText = this.m_AccessionOrder.PatientDisplayName;

			InitializeComponent();

            this.HistoryControl.Content = this.m_CaseHistoryPage;
			DataContext = this;
            Loaded += new RoutedEventHandler(PatientHistoryPage_Loaded);
		}

		private void PatientHistoryPage_Loaded(object sender, RoutedEventArgs e)
		{
			
		}

		public string PageHeaderText
		{
			get { return this.m_PageHeaderText; }
		}
		
		public YellowstonePathology.Business.Test.AccessionOrder AccessionOrder
		{
			get { return this.m_AccessionOrder; }
		}

		public string Message
		{
			get { return this.m_Message; }
		}

		private void ButtonBack_Click(object sender, RoutedEventArgs e)
		{
			UI.Navigation.PageNavigationReturnEventArgs args = new UI.Navigation.PageNavigationReturnEventArgs(UI.Navigation.PageNavigationDirectionEnum.Back, null);
			this.Return(this, args);
		}

		private void ButtonNext_Click(object sender, RoutedEventArgs e)
		{
            YellowstonePathology.Business.Persistence.DocumentGateway.Instance.Save();
            UI.Navigation.PageNavigationReturnEventArgs args = new UI.Navigation.PageNavigationReturnEventArgs(UI.Navigation.PageNavigationDirectionEnum.Next, null);
			this.Return(this, args);
		}		
	}
}
