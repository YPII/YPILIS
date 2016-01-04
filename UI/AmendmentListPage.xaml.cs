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
	/// <summary>
	/// Interaction logic for AmendmentListPage.xaml
	/// </summary>
	public partial class AmendmentListPage : UserControl, INotifyPropertyChanged, Business.Interface.IPersistPageChanges
	{		
		public event PropertyChangedEventHandler PropertyChanged;
		
        public delegate void EditEventHandler(object sender, EventArgs e);
        public event EditEventHandler Edit;        
        public delegate void CloseEventHandler(object sender, EventArgs e);
        public event CloseEventHandler Close;        

        private string m_PageHeaderText;
		private AmendmentUI m_AmendmentUI;

		public AmendmentListPage(AmendmentUI amendmentUI)
		{
			this.m_AmendmentUI = amendmentUI;
            this.m_PageHeaderText = " Amendments For: " + this.m_AmendmentUI.AccessionOrder.PatientDisplayName;
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

        public YellowstonePathology.Business.Amendment.Model.AmendmentCollection Amendments
		{
			get { return this.m_AmendmentUI.AmendmentCollection; }
		}

        public string PageHeaderText
        {
            get { return this.m_PageHeaderText; }
        }

		public bool OkToSaveOnNavigation(Type pageNavigatingTo)
		{
			return true;
		}

		public bool OkToSaveOnClose()
		{
			return true;
		}

		public void Save()
		{
		}
        
		public void UpdateBindingSources()
		{

		}

		private void ListViewAmendments_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
            this.m_AmendmentUI.SelectedAmendment = (YellowstonePathology.Business.Amendment.Model.Amendment)this.ListViewAmendments.SelectedItem;
		}

		private void HyperLinkAddAmendment_Click(object sender, RoutedEventArgs e)
		{
            YellowstonePathology.Business.Amendment.Model.Amendment amendment = this.m_AmendmentUI.PanelSetOrder.AddAmendment();
			amendment.TestResultAmendmentFill(this.m_AmendmentUI.ReportNo, this.m_AmendmentUI.AssignedToId, "???");
			this.m_AmendmentUI.Save();			

			NotifyPropertyChanged("Amendments");
			this.ListViewAmendments.SelectedIndex = 0;
		}

		private void HyperLinkEditAmendment_Click(object sender, RoutedEventArgs e)
		{
			if (this.m_AmendmentUI.SelectedAmendment != null)
			{
				if(this.Edit != null) this.Edit(this, new EventArgs());
			}
		}

		private void HyperLinkDeleteAmendment_Click(object sender, RoutedEventArgs e)
		{
			if (this.ListViewAmendments.SelectedItem != null)
			{
				MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this amendment?", "Delete", MessageBoxButton.YesNo, MessageBoxImage.Question);
				if (result == MessageBoxResult.Yes)
				{
                    YellowstonePathology.Business.Amendment.Model.Amendment selectedAmendment = (YellowstonePathology.Business.Amendment.Model.Amendment)this.ListViewAmendments.SelectedItem;					
					this.m_AmendmentUI.PanelSetOrder.DeleteAmendment(selectedAmendment.AmendmentId);
					this.ListViewAmendments.SelectedIndex = -1;
				}
			}
		}

		private void ButtonClose_Click(object sender, RoutedEventArgs e)
		{
			if(this.Close != null) this.Close(this, new EventArgs());
		}

		private void ListViewAmendments_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			if (this.ListViewAmendments.SelectedItem != null)
			{
				if(this.Edit != null) this.Edit(this, new EventArgs());
			}
		}
	}
}
