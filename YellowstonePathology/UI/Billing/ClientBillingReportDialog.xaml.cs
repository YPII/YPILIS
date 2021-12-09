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
using System.Xml;
using System.Xml.Linq;

namespace YellowstonePathology.UI.Billing
{    
    public partial class ClientBillingReportDialog : Window
    {
        private Nullable<DateTime> m_PostDateStart;
        private Nullable<DateTime> m_PostDateEnd;        
        private YellowstonePathology.Business.Client.Model.ClientGroup m_ClientGroup;                        
        private YellowstonePathology.Business.Client.Model.ClientGroupCollection m_ClientGroupCollection;
        private bool m_SVHCOVIDANubmers;

        public ClientBillingReportDialog()
        {
            this.m_SVHCOVIDANubmers = false;

            this.m_PostDateStart =DateTime.Today;
            this.m_PostDateEnd = DateTime.Today;            
            this.m_ClientGroupCollection = Business.Gateway.PhysicianClientGateway.GetClientGroupCollection();            

            InitializeComponent();

            this.DataContext = this;
        }

        public bool SVHCOVIDANubmers
        {
            get { return this.m_SVHCOVIDANubmers; }
            set { this.m_SVHCOVIDANubmers = value; }
        }

        public Nullable<DateTime> PostDateStart
        {
            get { return this.m_PostDateStart; }
            set { this.m_PostDateStart = value; }
        }

        public Nullable<DateTime> PostDateEnd
        {
            get { return this.m_PostDateEnd; }
            set { this.m_PostDateEnd = value; }
        }        

        public YellowstonePathology.Business.Client.Model.ClientGroup ClientGroup
        {
            get { return this.m_ClientGroup; }
            set { this.m_ClientGroup = value; }
        }        
       
        public YellowstonePathology.Business.Client.Model.ClientGroupCollection ClientGroupCollection
        {
            get { return this.m_ClientGroupCollection; }
        }
       
        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ButtonShowReport_Click(object sender, RoutedEventArgs e)
        {
            Business.XPSDocument.Result.ClientBillingDetailReportResult.ClientBillingDetailReportData clientBillingDetailReportData = null;
            if(this.m_SVHCOVIDANubmers == false)
            {
                clientBillingDetailReportData = Business.Gateway.XmlGateway.GetClientBillingDetailReport(this.m_PostDateStart.Value, this.m_PostDateEnd.Value, this.m_ClientGroup.ClientGroupId);
            }
            else
            {
                clientBillingDetailReportData = Business.Gateway.XmlGateway.GetClientBillingDetailReportSVHANumbers(this.m_PostDateStart.Value, this.m_PostDateEnd.Value, this.m_ClientGroup.ClientGroupId);
            }
            
            YellowstonePathology.Document.ClientBillingDetailReportV2 clientBillingDetailReport = new Document.ClientBillingDetailReportV2(clientBillingDetailReportData, this.m_PostDateStart.Value, this.m_PostDateEnd.Value);

            XpsDocumentViewer viewer = new XpsDocumentViewer();
            viewer.LoadDocument(clientBillingDetailReport.FixedDocument);
            viewer.ShowDialog();
        }
    }
}
