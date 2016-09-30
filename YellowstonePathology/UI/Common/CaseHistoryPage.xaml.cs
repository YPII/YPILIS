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

namespace YellowstonePathology.UI.Common
{
    /// <summary>
    /// Interaction logic for CaseHistoryControl.xaml
    /// </summary>
	public partial class CaseHistoryPage : UserControl, INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;
        YellowstonePathology.Business.Surgical.PathologistHistoryList m_PathologistHistoryList;
        YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;

        public CaseHistoryPage(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {
            this.m_AccessionOrder = accessionOrder;
            this.m_PathologistHistoryList = new YellowstonePathology.Business.Surgical.PathologistHistoryList();
			if (this.m_AccessionOrder.PatientId != "0")
            {
				this.m_PathologistHistoryList = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetPathologistPatientHistory(this.m_AccessionOrder.PatientId);
            }
            InitializeComponent();
            this.DataContext = this;
        }

        public YellowstonePathology.Business.Surgical.PathologistHistoryList PathologistHistoryList
        {
            get { return this.m_PathologistHistoryList; }
        }

        private void ListViewCaseHistoryList_DoublClick(object sender, RoutedEventArgs args)
        {
            if (this.listViewCaseHistoryList.SelectedItems.Count != 0)
            {
                YellowstonePathology.Business.Surgical.PathologistHistoryItem pathologistHistoryItem = (YellowstonePathology.Business.Surgical.PathologistHistoryItem)this.listViewCaseHistoryList.SelectedItem;
				YellowstonePathology.Business.OrderIdParser orderIdParser = new Business.OrderIdParser(pathologistHistoryItem.ReportNo);                
				string path = YellowstonePathology.Business.Document.CaseDocument.GetCaseFileNameDoc(orderIdParser);
				YellowstonePathology.Business.Document.CaseDocument.OpenWordDocumentWithWordViewer(path);
            }
        }

		public void SetAccessionOrder( YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
		{
			this.m_AccessionOrder = accessionOrder;
			this.m_PathologistHistoryList.Clear();
			YellowstonePathology.Business.Surgical.PathologistHistoryList list = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetPathologistPatientHistory(this.m_AccessionOrder.PatientId);
			if (list != null)
			{
				foreach (YellowstonePathology.Business.Surgical.PathologistHistoryItem item in list)
				{
					this.m_PathologistHistoryList.Add(item);
				}
			}
			NotifyPropertyChanged("");
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
