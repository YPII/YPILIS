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
    /// <summary>
    /// Interaction logic for CaseHistoryDialog.xaml
    /// </summary>
    public partial class CaseHistoryDialog : Window
    {
        YellowstonePathology.UI.Common.CaseHistoryPage m_CaseHistoryPage;

        public CaseHistoryDialog(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {
            this.m_CaseHistoryPage = new CaseHistoryPage(accessionOrder);

            InitializeComponent();

            this.TextBoxPatientName.Text = accessionOrder.PatientDisplayName;
            this.HistoryControl.Content = this.m_CaseHistoryPage;
        }

        private void ButtonOK_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
