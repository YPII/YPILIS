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
using System.Xml.Linq;

namespace YellowstonePathology.UI.Monitor
{
	public partial class ReportDistributionMonitorPage : UserControl, INotifyPropertyChanged, IMonitorPage
	{
		public event PropertyChangedEventHandler PropertyChanged;

        private YellowstonePathology.Business.Monitor.Model.DistributionCollection m_DistributionCollection;        

        public ReportDistributionMonitorPage()
		{            
            InitializeComponent();
            this.DataContext = this;
		}

        private void LoadData()
        {
            this.m_DistributionCollection = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetPendingDistributions();
            this.m_DistributionCollection.SetState();        
            this.NotifyPropertyChanged("");
        }

        public void Refresh()
        {
            this.LoadData();
        }

        public YellowstonePathology.Business.Monitor.Model.DistributionCollection DistributionCollection
        {
            get { return this.m_DistributionCollection; }
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
