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
namespace YellowstonePathology.UI.Login.FinalizeAccession
{
	/// <summary>
	/// Interaction logic for AcknowledgementPage.xaml
	/// </summary>
	public partial class AcknowledgementPage : UserControl
	{
		public delegate void ReturnEventHandler(object sender, UI.Navigation.PageNavigationReturnEventArgs e);
		public event ReturnEventHandler Return;

		private string m_PageHeaderText = "Acknowledge Orders";
		private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
		private YellowstonePathology.Business.User.SystemIdentity m_SystemIdentity;
		private ObservableCollection<YellowstonePathology.Business.Test.PanelOrder> m_PanelOrderCollection;		

		public AcknowledgementPage(YellowstonePathology.Business.Test.AccessionOrder accessionOrder,			
			YellowstonePathology.Business.User.SystemIdentity systemIdentity)
		{			
			this.m_AccessionOrder = accessionOrder;
			this.m_SystemIdentity = systemIdentity;
            this.m_PanelOrderCollection = this.m_AccessionOrder.PanelSetOrderCollection.GetUnAcknowledgedPanelOrders();

			InitializeComponent();
			DataContext = this;			
		}                

		public string PageHeaderText
		{
			get { return this.m_PageHeaderText; }
		}

		public ObservableCollection<YellowstonePathology.Business.Test.PanelOrder> PanelOrderCollection
		{
			get { return this.m_PanelOrderCollection; }
		}

		private void ButtonAcknowledge_Click(object sender, RoutedEventArgs e)
		{
			DateTime acknowledgementDate = DateTime.Today;
			DateTime acknowledgementTime = DateTime.Now;
			StringBuilder panelOrderIds = new StringBuilder();
			
			foreach (YellowstonePathology.Business.Test.PanelOrder panelOrder in this.m_PanelOrderCollection)
			{
				if (panelOrder.Acknowledged == false)
				{
					panelOrderIds.Append(panelOrder.PanelOrderId + ",");
					panelOrder.Acknowledged = true;
					panelOrder.AcknowledgedById = this.m_SystemIdentity.User.UserId;
					panelOrder.AcknowledgedDate = acknowledgementDate;
					panelOrder.AcknowledgedTime = acknowledgementTime;
				}
			}

			if (panelOrderIds.Length > 1)
			{
				panelOrderIds.Remove(panelOrderIds.Length - 1, 1);
			}
			
			if (panelOrderIds.Length > 0)
			{
				YellowstonePathology.Business.Reports.LabOrderSheet labOrderSheet = new YellowstonePathology.Business.Reports.LabOrderSheet();
				labOrderSheet.CreateReport(panelOrderIds.ToString(), acknowledgementDate, acknowledgementTime);
			}
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
