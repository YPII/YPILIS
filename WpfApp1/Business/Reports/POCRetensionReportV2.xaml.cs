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

namespace YellowstonePathology.Business.Reports
{
    /// <summary>
    /// Interaction logic for POCRetensionReportV2.xaml
    /// </summary>
    public partial class POCRetensionReportV2 : FixedDocument
    {
        private POCRetensionReportData m_POCRetensionReportData;

        public POCRetensionReportV2(DateTime startDate, DateTime endDate)
        {
            this.m_POCRetensionReportData = Gateway.SlideDisposalReportGateway.GetPOCRetensionReport(startDate, endDate);

            InitializeComponent();

            this.DataContext = this;
        }

        public POCRetensionReportData POCRetensionReportData
        {
            get { return this.m_POCRetensionReportData; }
        }
    }
}
