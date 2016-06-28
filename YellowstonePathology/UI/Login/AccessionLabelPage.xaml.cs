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
	public partial class AccessionLabelPage : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

		public delegate void CloseEventHandler(object sender, EventArgs e);
		public event CloseEventHandler Close;

        private string m_PageHeaderText = "Accession Label Page";

		public AccessionLabelPage(YellowstonePathology.Business.Search.ReportSearchList reportSearchList)
		{            			
			InitializeComponent();
			this.DataContext = this;            
		}

		private void AddLabelsFromSearchList(YellowstonePathology.Business.Search.ReportSearchList reportSearchList)
        {
			foreach (YellowstonePathology.Business.Search.ReportSearchItem reportSearchItem in reportSearchList)
            {

            }
        }

        public string PageHeaderText
        {
            get { return this.m_PageHeaderText; }
        }

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }             

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this).Close(); 
        }        		
	}
}
