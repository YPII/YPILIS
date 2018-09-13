using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace YellowstonePathology.UI.Test
{    
    public partial class PreviousResultDialog : Window
    {
        private Business.Test.PanelSetOrder m_PanelSetOrder;        
        private Business.Search.ReportSearchList m_ReportSearchList;
        private string m_TableName;

        public PreviousResultDialog(Business.Test.PanelSetOrder panelSetOrder)
        {            
            this.m_PanelSetOrder = panelSetOrder;

            this.m_TableName = Business.Persistence.PersistenceHelper.GetTableName(panelSetOrder.GetType());
            this.m_ReportSearchList = YellowstonePathology.Business.Gateway.ReportSearchGateway.GetReportSearchListByTestFinal(this.m_PanelSetOrder.PanelSetId, DateTime.Today.AddDays(-90), DateTime.Today, this.m_TableName);            

            InitializeComponent();
            DataContext = this;
        }    
        
        public Business.Test.PanelSetOrder PanelSetOrder
        {
            get { return this.m_PanelSetOrder; }
        }    

        public YellowstonePathology.Business.Search.ReportSearchList ReportSearchList
        {
            get { return this.m_ReportSearchList; }
        }

        private void ButtonOK_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ButtonSetResults_Click(object sender, RoutedEventArgs e)
        {
            if(this.ListViewPreviousResults.SelectedItem != null)
            {
                if(this.m_PanelSetOrder.Final == false && this.m_PanelSetOrder.Accepted == false)
                {
                    Business.Search.ReportSearchItem reportSearchItem = (Business.Search.ReportSearchItem)this.ListViewPreviousResults.SelectedItem;
                    Business.Test.AccessionOrder ao = Business.Persistence.DocumentGateway.Instance.PullAccessionOrder(reportSearchItem.MasterAccessionNo, this);
                    Business.Test.PanelSetOrder pso = ao.PanelSetOrderCollection.GetPanelSetOrder(reportSearchItem.ReportNo);                    
                    pso.SetPreviousResults(this.m_PanelSetOrder);
                }
            }
        }

        private void ButtonClear_Click(object sender, RoutedEventArgs e)
        {
            if (this.m_PanelSetOrder.Final == false && this.m_PanelSetOrder.Accepted == false)
            {
                this.m_PanelSetOrder.ClearPreviousResults();
            }
        }
    }
}
