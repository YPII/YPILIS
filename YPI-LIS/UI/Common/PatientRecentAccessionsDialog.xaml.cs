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

namespace YellowstonePathology.UI.Common
{    
    public partial class PatientRecentAccessionsDialog : Window
    {
		YellowstonePathology.Business.View.RecentAccessionViewCollection m_RecentAccessionViewCollection;
		private bool m_AllowSelection;
		private string m_ReportNo;

		public PatientRecentAccessionsDialog(YellowstonePathology.Business.View.RecentAccessionViewCollection recentAccessionViewCollection, bool allowSelection)
        {
			this.m_RecentAccessionViewCollection = recentAccessionViewCollection;
			this.m_AllowSelection = allowSelection;
			this.m_ReportNo = string.Empty;

            InitializeComponent();
            this.DataContext = this;
        }

		public YellowstonePathology.Business.View.RecentAccessionViewCollection RecentAccessionViewCollection
		{
			get { return this.m_RecentAccessionViewCollection; }
		}

		public string ReportNo
		{
			get { return this.m_ReportNo; }
		}

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        private void ButtonOk_Click(object sender, RoutedEventArgs e)
        {
			if (this.m_AllowSelection)
			{
				if (!this.SetSelectionResults())
				{
					MessageBox.Show("Select an accession.", "No Selection", MessageBoxButton.OK, MessageBoxImage.Exclamation);
					return;
				}
			}

            this.DialogResult = true;
            this.Close();
        }

		private bool SetSelectionResults()
		{
			bool result = false;
			if(this.ListViewAccessionOrders.SelectedItem != null)
			{
				result = true;
				this.m_ReportNo = ((YellowstonePathology.Business.View.RecentAccessionView)this.ListViewAccessionOrders.SelectedItem).ReportNo;
			}
			return result;
		}
    }
}
