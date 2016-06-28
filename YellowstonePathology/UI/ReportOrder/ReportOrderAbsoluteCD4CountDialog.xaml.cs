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

namespace YellowstonePathology.UI.ReportOrder
{
    /// <summary>
    /// Interaction logic for ReportOrderMolecularAnalysisDialog.xaml
    /// </summary>
    public partial class ReportOrderAbsoluteCD4CountDialog : Window
    {
		private YellowstonePathology.Business.Test.AbsoluteCD4Count.AbsoluteCD4CountTestOrder m_ReportOrder;

		public ReportOrderAbsoluteCD4CountDialog(YellowstonePathology.Business.Test.AbsoluteCD4Count.AbsoluteCD4CountTestOrder reportOrderAbsoluteCD4Count)
        {
            this.m_ReportOrder = reportOrderAbsoluteCD4Count;
            InitializeComponent();
            this.DataContext = this;
        }

		public YellowstonePathology.Business.Test.AbsoluteCD4Count.AbsoluteCD4CountTestOrder ReportOrder
        {
            get { return this.m_ReportOrder; }
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Gateway.ReportOrderGateway.UpdateReportOrderAbsoluteCD4Count(this.m_ReportOrder);
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }        
    }
}
