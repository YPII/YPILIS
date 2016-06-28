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
    public partial class ReportOrderMolecularAnalysisDialog : Window
    {
		private YellowstonePathology.Business.Test.MolecularAnalysis.MolecularAnalysisTestOrder m_ReportOrderMolecularAnalysis;

		public ReportOrderMolecularAnalysisDialog(YellowstonePathology.Business.Test.MolecularAnalysis.MolecularAnalysisTestOrder reportOrderMolecularAnalaysis)
        {
            this.m_ReportOrderMolecularAnalysis = reportOrderMolecularAnalaysis;
            InitializeComponent();
            this.DataContext = this;
        }

		public YellowstonePathology.Business.Test.MolecularAnalysis.MolecularAnalysisTestOrder ReportOrderMolecularAnalysis
        {
            get { return this.m_ReportOrderMolecularAnalysis; }
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }        
    }
}
