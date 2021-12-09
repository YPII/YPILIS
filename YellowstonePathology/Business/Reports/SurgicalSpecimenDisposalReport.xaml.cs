using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Threading;
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
using System.Windows.Xps;
using System.Windows.Xps.Packaging;
using System.Windows.Markup;

namespace YellowstonePathology.Business.Reports
{    
    public partial class SurgicalSpecimenDisposalReport
    {
        private FixedDocument m_Document;
        private System.Windows.Threading.DispatcherPriority m_DispatcherPriority;
        private const string TemplatePath = "YellowstonePathology.Business.Reports.SurgicalSpecimenDisposalReport.xaml";
        private DisposalReportData m_DisposalReportData;

        public SurgicalSpecimenDisposalReport(DateTime disposalDate)
        {
            this.m_DisposalReportData = Business.Gateway.SlideDisposalReportGateway.GetSpecimenDisposalReport_1(disposalDate);
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
