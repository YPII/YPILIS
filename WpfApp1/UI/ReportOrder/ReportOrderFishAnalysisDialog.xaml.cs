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
    public partial class ReportOrderFishAnalysisDialog : Window
    {
		private YellowstonePathology.Business.Test.FishAnalysis.FishAnalysisTestOrder m_ReportOrderFishAnalysis;

		public ReportOrderFishAnalysisDialog(YellowstonePathology.Business.Test.FishAnalysis.FishAnalysisTestOrder reportOrderFishAnalaysis)
        {
            this.m_ReportOrderFishAnalysis = reportOrderFishAnalaysis;
            InitializeComponent();
            this.DataContext = this;
        }

		public YellowstonePathology.Business.Test.FishAnalysis.FishAnalysisTestOrder ReportOrderFishAnalysis
        {
            get { return this.m_ReportOrderFishAnalysis; }
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
