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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;

namespace YellowstonePathology.UI.Login
{
    /// <summary>
    /// Interaction logic for DeleteAccessionPage.xaml
    /// </summary>
    public partial class DeleteAccessionPage : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public delegate bool CloseTabsEventHandler(object sender, EventArgs e);
        public event CloseTabsEventHandler CloseTabs;

        public delegate void BackEventHandler(object sender, EventArgs e);
        public event BackEventHandler Back;
        public delegate void CloseEventHandler(object sender, EventArgs e);
        public event CloseEventHandler Close;

        private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
        private object m_Writer;
        private string m_PageHeaderText = "Delete Accession page";

        public DeleteAccessionPage(Business.Test.AccessionOrder accessionOrder, object writer)
        {
            this.m_AccessionOrder = accessionOrder;
            this.m_Writer = writer;

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

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            Business.Persistence.DocumentGateway.Instance.Push(this.m_Writer);
            this.Back(this, new EventArgs());
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            Business.Persistence.DocumentGateway.Instance.Push(this.m_Writer);
            this.Close(this, new EventArgs());
        }

        public string PageHeaderText
        {
            get { return this.m_PageHeaderText; }
        }

        public Business.Test.AccessionOrder AccessionOrder
        {
            get { return this.m_AccessionOrder; }
        }

        private void HyperLinkDeleteAccessionOrder_Click(object sender, RoutedEventArgs e)
        {
            bool tabsClosedResult = this.CloseTabs(this, EventArgs.Empty);
            if (tabsClosedResult == true)
            {
                MessageBoxResult result = MessageBox.Show("Are you sure you want to permanently delete Accession " + this.m_AccessionOrder.MasterAccessionNo + " for " + this.m_AccessionOrder.PatientDisplayName + "?", "Delete Accession", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
                if (result == MessageBoxResult.Yes)
                {
                    Business.Rules.MethodResult methodResult = AORemover.Remove(this.m_AccessionOrder, this.m_Writer);
                    if (methodResult.Success == false)
                    {
                        MessageBox.Show(methodResult.Message);
                    }
                    else
                    {
                        this.Back(this, new EventArgs());
                    }
                }
            }
        }

        private void HyperLinkDeletePanelSetOrder_Click(object sender, RoutedEventArgs e)
        {
            if (this.m_AccessionOrder.PanelSetOrderCollection.Count == 1)
            {
                MessageBox.Show("Unable to remove the only Panel Set for the Accession.");
            }
            else
            {
                bool tabsClosedResult = this.CloseTabs(this, EventArgs.Empty);
                if (tabsClosedResult == true)
                {
                    Business.Test.PanelSetOrder panelSetOrder = ((Hyperlink)sender).Tag as Business.Test.PanelSetOrder;
                    MessageBoxResult result = MessageBox.Show("Are you sure you want to permanently delete report " + panelSetOrder.ReportNo + " for " + this.m_AccessionOrder.PatientDisplayName + "?", "Delete Report", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
                    if (result == MessageBoxResult.Yes)
                    {
                        Business.Rules.MethodResult methodResult = AORemover.RemovePanelSet(panelSetOrder.ReportNo, this.m_AccessionOrder, this.m_Writer);
                        if (methodResult.Success == false)
                        {
                            MessageBox.Show(methodResult.Message);
                        }
                        else
                        {
                            this.m_AccessionOrder = Business.Persistence.DocumentGateway.Instance.PullAccessionOrder(this.m_AccessionOrder.MasterAccessionNo, this.m_Writer);
                            this.NotifyPropertyChanged("AccessionOrder");
                        }
                    }
                }
            }
        }
    }
}
