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

namespace YellowstonePathology.UI.Test
{
	/// <summary>
	/// Interaction logic for NeogenomicsResultAccessionPage.xaml
	/// </summary>
	public partial class NeogenomicsResultAccessionPage : UserControl, INotifyPropertyChanged
	{        
		public delegate void PropertyChangedNotificationHandler(String info);
		public event PropertyChangedEventHandler PropertyChanged;        

		public delegate void NextEventHandler(object sender, YellowstonePathology.UI.CustomEventArgs.ReportNoReturnEventArgs e);
		public event NextEventHandler Next;

        public delegate void BackEventHandler(object sender, EventArgs e);
        public event BackEventHandler Back;

		private YellowstonePathology.Business.NeogenomicsResult m_NeogenomicsResult;
		private YellowstonePathology.Business.Search.ReportSearchList m_ReportSearchList;

		public NeogenomicsResultAccessionPage(YellowstonePathology.Business.NeogenomicsResult neogenomicsResult)
		{
            this.m_NeogenomicsResult = neogenomicsResult;

            List<object> parameterList = new List<object>();
            parameterList.Add(neogenomicsResult.PLastName);
            parameterList.Add(neogenomicsResult.PFirstName);

            this.m_ReportSearchList = YellowstonePathology.Business.Gateway.ReportSearchGateway.GetReportSearchListByPatientName(parameterList);
            this.NotifyPropertyChanged("ReportSearchList");

			InitializeComponent();
			this.DataContext = this;
		}

		public YellowstonePathology.Business.Search.ReportSearchList ReportSearchList
        {
            get { return this.m_ReportSearchList; }
        }

		public YellowstonePathology.Business.NeogenomicsResult NeogenomicsResult
        {
            get { return this.m_NeogenomicsResult; }
        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            if (this.Back != null) this.Back(this, new EventArgs());   
        }

		public void NotifyPropertyChanged(String info)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(info));
			}
		}        

        private void ListSearchList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void ButtonNext_Click(object sender, RoutedEventArgs e)
        {
            if (this.ListBoxSearchList.SelectedItem != null)
            {
				YellowstonePathology.Business.Search.ReportSearchItem reportSearchItem = (YellowstonePathology.Business.Search.ReportSearchItem)this.ListBoxSearchList.SelectedItem;
                YellowstonePathology.UI.CustomEventArgs.ReportNoReturnEventArgs eventArgs = new CustomEventArgs.ReportNoReturnEventArgs(reportSearchItem.ReportNo);
                this.Next(this, eventArgs);
            }
            else
            {
                MessageBox.Show("You need to select a case before continuing.");
            }
        }        
	}
}
