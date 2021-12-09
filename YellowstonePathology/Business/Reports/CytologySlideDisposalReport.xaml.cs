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
using System.Reflection;
using System.Windows.Markup;
using System.Windows.Threading;

namespace YellowstonePathology.Business.Reports
{    
    public partial class CytologySlideDisposalReport
    {
        private FixedDocument m_Document;
        private System.Windows.Threading.DispatcherPriority m_DispatcherPriority;
        private const string TemplatePath = "YellowstonePathology.Business.Reports.CytologySlideDisposalReport.xaml";
        private DisposalReportData m_DisposalReportData;

        public CytologySlideDisposalReport(DateTime disposalDate)
        {
            this.m_DisposalReportData = Business.Gateway.SlideDisposalReportGateway.GetCytologySlideDisposalReport_1(disposalDate);
            this.m_DispatcherPriority = DispatcherPriority.SystemIdle;
            this.LoadTemplate();
            this.InjectData();
        }

        private void LoadTemplate()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            this.m_Document = (FixedDocument)XamlReader.Load(assembly.GetManifestResourceStream(TemplatePath));
        }

        private void InjectData()
        {
            this.m_Document.DataContext = this;
            var dispatcher = Dispatcher.CurrentDispatcher;
            dispatcher.Invoke(this.m_DispatcherPriority, new DispatcherOperationCallback(delegate { return null; }), null);
        }

        public DisposalReportData DisposalReportData
        {
            get { return this.m_DisposalReportData; }
        }

        public FixedDocument Document
        {
            get { return this.m_Document; }
        }
    }
}
