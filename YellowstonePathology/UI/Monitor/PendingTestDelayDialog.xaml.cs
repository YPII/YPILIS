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

namespace YellowstonePathology.UI.Monitor
{
    /// <summary>
    /// Interaction logic for PendingTestDelayDialog.xaml
    /// </summary>
    public partial class PendingTestDelayDialog : Window
    {
        private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
        private YellowstonePathology.Business.Test.PanelSetOrder m_PanelSetOrder;

        public PendingTestDelayDialog(string reportNo)
        {
            string masterAccessionNo = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetMasterAccessionNoFromReportNo(reportNo);
            this.m_AccessionOrder = YellowstonePathology.Business.Persistence.DocumentGateway.Instance.PullAccessionOrder(masterAccessionNo, this);
            this.m_PanelSetOrder = this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(reportNo);

            InitializeComponent();

            this.DataContext = this;

            Closing += PendingTestDelayDialog_Closing;                        
        }

        private void PendingTestDelayDialog_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            YellowstonePathology.Business.Persistence.DocumentGateway.Instance.Push(this);
        }

        public YellowstonePathology.Business.Test.PanelSetOrder PanelSetOrder
        {
            get { return this.m_PanelSetOrder; }
        }

        private void ButtonOK_Click(object sender, RoutedEventArgs e)
        {
            if (this.IsOKToClose() == true)
            {
                this.Close();
            }
        }

        private bool IsOKToClose()
        {
            bool result = true;
            if (this.m_PanelSetOrder.IsDelayed == true)
            {
                if (string.IsNullOrEmpty(this.m_PanelSetOrder.DelayComment) == true)
                {
                    result = false;
                    MessageBox.Show("Use must enter a comment explaining why this case is delayed.");
                }
            }
            return result;
        }

        private void ComboBoxDelayDays_SelectedChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem comboBoxItem = (ComboBoxItem)this.ComboBoxDelayDays.SelectedItem;
            int daysToDelay = Convert.ToInt32(comboBoxItem.Tag.ToString());
            TimeSpan timeSpanDelay = new TimeSpan(daysToDelay,0,0,0);
            this.m_PanelSetOrder.IsDelayed = true;
            this.m_PanelSetOrder.DelayedBy = YellowstonePathology.Business.User.SystemIdentity.Instance.User.DisplayName;
            this.m_PanelSetOrder.DelayedDate = DateTime.Now;

            this.m_PanelSetOrder.ExpectedFinalTime = YellowstonePathology.Business.Helper.DateTimeExtensions.GetEndDateConsideringWeekends(this.m_PanelSetOrder.ExpectedFinalTime.Value, timeSpanDelay);
        }
    }
}
