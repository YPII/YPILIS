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
	public partial class MissingInformationMonitorPage : UserControl, INotifyPropertyChanged, IMonitorPage
	{
		public event PropertyChangedEventHandler PropertyChanged;

        private YellowstonePathology.Business.Monitor.Model.MissingInformationCollection m_MissingInformationCollection;
        private Login.Receiving.LoginPageWindow m_LoginPageWindow;

        public MissingInformationMonitorPage()
		{         
            InitializeComponent();
            this.DataContext = this;            
		}

        private void LoadData()
        {
            YellowstonePathology.Business.Monitor.Model.MissingInformationCollection missingInformationCollection = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetMissingInformationCollection();
            missingInformationCollection.SetState();            
            this.m_MissingInformationCollection = missingInformationCollection;
            this.NotifyPropertyChanged("");
        }

        public void Refresh()
        {
            this.LoadData();
        }

        public YellowstonePathology.Business.Monitor.Model.MissingInformationCollection MissingInformationCollection
        {
            get { return this.m_MissingInformationCollection; }
        }

		public void NotifyPropertyChanged(String info)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(info));
			}
		}        	        		

        private void ListViewMissingInformation_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            if(this.ListViewMissingInformation.SelectedItem != null)
            {
                YellowstonePathology.Business.Monitor.Model.MissingInformation missingInformation = (YellowstonePathology.Business.Monitor.Model.MissingInformation)this.ListViewMissingInformation.SelectedItem;
                YellowstonePathology.Business.Test.AccessionOrder accessionOrder = YellowstonePathology.Business.Persistence.DocumentGateway.Instance.PullAccessionOrder(missingInformation.MasterAccessionNo, Window.GetWindow(this));                

                YellowstonePathology.Business.User.SystemIdentity systemIdentity = Business.User.SystemIdentity.Instance;
                this.m_LoginPageWindow = new Login.Receiving.LoginPageWindow();
                this.m_LoginPageWindow.Show();

                YellowstonePathology.Business.Test.MissingInformation.MissingInformtionTestOrder missingInformationTestOrder = (YellowstonePathology.Business.Test.MissingInformation.MissingInformtionTestOrder)accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(missingInformation.ReportNo);
                YellowstonePathology.UI.Test.ResultPathFactory resultPathFactory = new Test.ResultPathFactory();
                resultPathFactory.Start(missingInformationTestOrder, accessionOrder, this.m_LoginPageWindow.PageNavigator, System.Windows.Window.GetWindow(this), Visibility.Collapsed);
                resultPathFactory.Finished += ResultPathFactory_Finished;
            }
        }

        private void ResultPathFactory_Finished(object sender, EventArgs e)
        {             
            this.m_LoginPageWindow.Close();
        }
    }
}
