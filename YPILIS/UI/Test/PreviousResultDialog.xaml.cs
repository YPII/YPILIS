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
        private Business.Test.AccessionOrder m_AccessionOrder;
        private Business.PreviousResultCollection m_PreviousResultCollection;
        private string m_TableName;

        public PreviousResultDialog(Business.Test.PanelSetOrder panelSetOrder, Business.Test.AccessionOrder accessionOrder)
        {            
            this.m_PanelSetOrder = panelSetOrder;
            this.m_AccessionOrder = accessionOrder;

            this.m_TableName = Business.Persistence.PersistenceHelper.GetTableName(panelSetOrder.GetType());

            Business.Gateway.PreviousResultGateway previousResultGateway = new Business.Gateway.PreviousResultGateway();
            this.m_PreviousResultCollection = previousResultGateway.GetPreviousResultsByTestFinal(this.m_PanelSetOrder.PanelSetId, DateTime.Today.AddDays(-90), DateTime.Today, this.m_TableName);            

            InitializeComponent();
            DataContext = this;

            this.Left = (SystemParameters.PrimaryScreenWidth / 2);
            this.Top = (SystemParameters.PrimaryScreenHeight / 2) - (this.Height / 2);

        }

        public Business.Test.PanelSetOrder PanelSetOrder
        {
            get { return this.m_PanelSetOrder; }
        }    

        public YellowstonePathology.Business.PreviousResultCollection PreviousResultCollection
        {
            get { return this.m_PreviousResultCollection; }
        }

        private void ButtonOK_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ButtonSetResults_Click(object sender, RoutedEventArgs e)
        {
            if(this.ListViewPreviousResult.SelectedItem != null)
            {
                Business.PreviousResult previousResult = (Business.PreviousResult)this.ListViewPreviousResult.SelectedItem;
                Business.Test.AccessionOrder ao = Business.Persistence.DocumentGateway.Instance.PullAccessionOrder(previousResult.MasterAccessionNo, this);
                Business.Test.PanelSetOrder pso = ao.PanelSetOrderCollection.GetPanelSetOrder(previousResult.ReportNo);
                Business.Audit.Model.AuditResult auditResult = this.m_PanelSetOrder.IsOkToSetPreviousResults(pso, this.m_AccessionOrder);

                if (auditResult.Status == Business.Audit.Model.AuditStatusEnum.Failure)
                {
                    MessageBox.Show(auditResult.Message);
                }
                else if (auditResult.Status == Business.Audit.Model.AuditStatusEnum.Warning)
                {
                    string message = auditResult.Message;
                    MessageBoxResult messageBoxResult = MessageBox.Show(message, "Questionable Results", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
                    if (messageBoxResult == MessageBoxResult.Yes)
                    {
                        pso.SetPreviousResults(this.m_PanelSetOrder);
                    }
                }
                else
                {
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
            else
            {
                MessageBox.Show("The previous results may not be cleared because results have already been accepted.");
            }
        }
    }
}
