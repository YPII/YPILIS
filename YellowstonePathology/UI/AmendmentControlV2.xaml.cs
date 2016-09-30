using System;
using System.Collections.Generic;
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
	public partial class AmendmentControlV2 : System.Windows.Controls.UserControl, INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;        
		
		private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
		private YellowstonePathology.Business.User.SystemIdentity m_SystemIdentity;
		private string m_ReportNo;

		public AmendmentControlV2(YellowstonePathology.Business.User.SystemIdentity systemIdentity,
			string reportNo,
			YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
		{
			this.m_SystemIdentity = systemIdentity;
			this.m_AccessionOrder = accessionOrder;
			this.m_ReportNo = reportNo;

			InitializeComponent();
			this.DataContext = this;            
		}        

        public YellowstonePathology.Business.User.SystemUser CurrentUser
		{
			get { return this.m_SystemIdentity.User; }
		}

		public YellowstonePathology.Business.Test.AccessionOrder AccessionOrder
		{
			get { return this.m_AccessionOrder; }
			private set
			{
				this.m_AccessionOrder = value;
				this.NotifyPropertyChanged("AccessionOrder");
			}
		}

        public void ContextMenuAddAmendment_Click(object sender, RoutedEventArgs args)
        {
			if (this.m_AccessionOrder != null)
			{                
				this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_ReportNo).AddAmendment();                
			}
        }

        public void ContextMenuEditAmendment_Click(object sender, RoutedEventArgs args)
        {
			if (this.TreeViewAmendment.SelectedItem != null)
			{
				if (this.TreeViewAmendment.SelectedItem.GetType().Name == "Amendment")
				{
                    YellowstonePathology.Business.Amendment.Model.Amendment amendment = (YellowstonePathology.Business.Amendment.Model.Amendment)this.TreeViewAmendment.SelectedItem;                    
					YellowstonePathology.UI.AmendmentV2 amendmentV2 = new AmendmentV2(amendment, this.m_AccessionOrder);
					amendmentV2.ShowDialog();                    
				}
			}
        }

        public void ContextMenuDeleteAmendment_Click(object sedner, RoutedEventArgs args)
        {
			if (this.TreeViewAmendment.SelectedItem != null)
			{
				if (this.TreeViewAmendment.SelectedItem.GetType().Name == "Amendment")
				{
					MessageBoxResult result = MessageBox.Show("Delete the selected item?", "Delete.", MessageBoxButton.OKCancel);
					if (result == MessageBoxResult.OK)
					{
                        YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder = this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(((YellowstonePathology.Business.Amendment.Model.Amendment)this.TreeViewAmendment.SelectedItem).ReportNo);
                        panelSetOrder.DeleteAmendment(((YellowstonePathology.Business.Amendment.Model.Amendment)this.TreeViewAmendment.SelectedItem).AmendmentId);
                        //YellowstonePathology.Business.Persistence.DocumentGateway.Instance.SubmitChanges(this.m_AccessionOrder, false);
					}
				}
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