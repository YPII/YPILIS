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
using System.IO;
using System.ComponentModel;

namespace YellowstonePathology.UI.Test
{
    /// <summary>
    /// Interaction logic for AuthorizationForVerbalTestRequestResultPage.xaml
    /// </summary>
    public partial class AuthorizationForVerbalTestRequestResultPage : ResultControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public delegate void NextEventHandler(object sender, EventArgs e);
        public event NextEventHandler Next;

        private YellowstonePathology.Business.User.SystemIdentity m_SystemIdentity;
        private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
        private string m_PageHeaderText;
        private YellowstonePathology.Business.Test.AuthorizationForVerbalTestRequest.AuthorizationForVerbalTestRequestTestOrder m_PanelSetOrder;

        public AuthorizationForVerbalTestRequestResultPage(YellowstonePathology.Business.Test.AuthorizationForVerbalTestRequest.AuthorizationForVerbalTestRequestTestOrder testOrder,
            YellowstonePathology.Business.Test.AccessionOrder accessionOrder,
            YellowstonePathology.Business.User.SystemIdentity systemIdentity) : base(testOrder, accessionOrder)
        {
            this.m_AccessionOrder = accessionOrder;
            this.m_SystemIdentity = systemIdentity;

            this.m_PanelSetOrder = testOrder;

            this.m_PageHeaderText = "Authorization For Verbal Test Request For: " + this.m_AccessionOrder.PatientDisplayName;


            InitializeComponent();

            DataContext = this;

            this.m_ControlsNotDisabledOnFinal.Add(this.ButtonNext);
            this.m_ControlsNotDisabledOnFinal.Add(this.TextBlockUnfinalResults);

            Loaded += AuthorizationForVerbalTestRequestResultPage_Loaded;
        }

        private void AuthorizationForVerbalTestRequestResultPage_Loaded(object sender, RoutedEventArgs e)
        {
            string message = string.Empty;
            YellowstonePathology.Business.Client.Model.Client client = YellowstonePathology.Business.Gateway.PhysicianClientGateway.GetClientByClientId(this.m_AccessionOrder.ClientId);
            if(string.IsNullOrEmpty(this.m_PanelSetOrder.ContactName) == true)
            {
                this.m_PanelSetOrder.ContactName = client.ContactName;
                if (string.IsNullOrEmpty(client.ContactName) == true)
                {
                    message = "The client Contact Name is not set.";
                }
            }

            if (string.IsNullOrEmpty(this.m_PanelSetOrder.Fax) == true)
            {
                this.m_PanelSetOrder.Fax = client.AdditionalTestingNotificationFax;
                if (string.IsNullOrEmpty(client.AdditionalTestingNotificationFax) == true)
                {
                    this.m_PanelSetOrder.Fax = client.AdditionalTestingNotificationFax;
                    if (string.IsNullOrEmpty(client.AdditionalTestingNotificationFax) == true)
                    {
                        if (string.IsNullOrEmpty(message) == false)
                        {
                            message += Environment.NewLine;
                        }
                        message += "The notification fax number for this client is not set.";
                    }
                }
            }

            if(string.IsNullOrEmpty(message) == false)
            {
                MessageBox.Show(message);
            }
        }

        public YellowstonePathology.Business.Test.AuthorizationForVerbalTestRequest.AuthorizationForVerbalTestRequestTestOrder PanelSetOrder
        {
            get { return this.m_PanelSetOrder; }
        }

        public YellowstonePathology.Business.Test.AccessionOrder AccessionOrder
        {
            get { return this.m_AccessionOrder; }
        }

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        public string PageHeaderText
        {
            get { return this.m_PageHeaderText; }
        }

        private void ButtonNext_Click(object sender, RoutedEventArgs e)
        {
            this.Next(this, new EventArgs());
        }

        private void HyperLinkFinalize_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Audit.Model.AuditResult auditResult = this.m_PanelSetOrder.IsOkToFinalize(this.m_AccessionOrder);
            if (auditResult.Status == Business.Audit.Model.AuditStatusEnum.OK)
            {
                this.m_PanelSetOrder.Finish(this.m_AccessionOrder);
            }
            else
            {
                MessageBox.Show(auditResult.Message);
            }
        }

        private void HyperLinkUnfinalResults_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Rules.MethodResult methodResult = this.m_PanelSetOrder.IsOkToUnfinalize();
            if (methodResult.Success == true)
            {
                this.m_PanelSetOrder.Unfinalize();
            }
            else
            {
                MessageBox.Show(methodResult.Message);
            }
        }

        private void HyperLinkUnacceptResults_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Rules.MethodResult methodResult = this.m_PanelSetOrder.IsOkToUnaccept();
            if (methodResult.Success == true)
            {
                this.m_PanelSetOrder.Unaccept();
            }
            else
            {
                MessageBox.Show(methodResult.Message);
            }
        }

        private void HyperLinkAcceptResults_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Rules.MethodResult methodResult = this.m_PanelSetOrder.IsOkToAccept();
            if (methodResult.Success == true)
            {
                this.m_PanelSetOrder.Accept();
            }
            else
            {
                MessageBox.Show(methodResult.Message);
            }
        }

        private void HyperLinkShowDocument_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Rules.MethodResult result = this.ValuesArePresent();
            if (result.Success == true)
            {
                YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder = (Business.Test.PanelSetOrder)this.ComboBoxTestNeedsAuthorization.SelectedItem;
                YellowstonePathology.Business.Test.AuthorizationForVerbalTestRequest.AuthorizationForVerbalTestRequestWordDocument report = new Business.Test.AuthorizationForVerbalTestRequest.AuthorizationForVerbalTestRequestWordDocument(this.m_AccessionOrder, this.m_PanelSetOrder, Business.Document.ReportSaveModeEnum.Draft, panelSetOrder);
                report.Render();
                YellowstonePathology.Business.Document.CaseDocument.OpenWordDocumentWithWord(report.SaveFileName);
            }
            else
            {
                MessageBox.Show(result.Message);
            }
        }

        private void HyperLinkPublish_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Rules.MethodResult result = this.ValuesArePresent();
            if (result.Success == true)
            {
                YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder = (Business.Test.PanelSetOrder)this.ComboBoxTestNeedsAuthorization.SelectedItem;
                YellowstonePathology.Business.Test.AuthorizationForVerbalTestRequest.AuthorizationForVerbalTestRequestWordDocument report = new Business.Test.AuthorizationForVerbalTestRequest.AuthorizationForVerbalTestRequestWordDocument(this.m_AccessionOrder,this.m_PanelSetOrder, Business.Document.ReportSaveModeEnum.Normal, panelSetOrder);
                report.Render();
                report.Publish();
                MessageBox.Show("The request has been published.");
            }
            else
            {
                MessageBox.Show(result.Message);
            }
        }

        private void HyperLinkSendFax_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Rules.MethodResult result = this.ValuesArePresent();
            if (result.Success == true)
            {
                Business.OrderIdParser orderIdParser = new Business.OrderIdParser(this.m_PanelSetOrder.ReportNo);
                string tifPath = YellowstonePathology.Document.CaseDocumentPath.GetPath(orderIdParser) + orderIdParser.ReportNo + ".auth.tif";
                if (File.Exists(tifPath) == true)
                {
                    YellowstonePathology.Business.ReportDistribution.Model.DistributionResult distributionResult = Business.ReportDistribution.Model.FaxSubmission.Submit(this.m_PanelSetOrder.Fax, "Authorization For Verbal Test Request", tifPath);
                    if (distributionResult.IsComplete == false)
                    {
                        MessageBox.Show(distributionResult.Message);
                    }
                    else
                    {
                        MessageBox.Show("The request has been faxed.");
                    }
                }
                else
                {
                    MessageBox.Show("Publish beore faxing.");
                }
            }
            else
            {
                MessageBox.Show(result.Message);
            }
        }

        private YellowstonePathology.Business.Rules.MethodResult ValuesArePresent()
        {
            YellowstonePathology.Business.Rules.MethodResult result = new Business.Rules.MethodResult();

            if (string.IsNullOrEmpty(this.PanelSetOrder.AuthorizationTestName) == true)
            {
                result.Success = false;
                result.Message = "Select the test that requires authorization." + Environment.NewLine;
            }

            if(string.IsNullOrEmpty(this.PanelSetOrder.ContactName) == true)
            {
                result.Success = false;
                result.Message += "The Contact Name is not set." + Environment.NewLine;
            }

            if (string.IsNullOrEmpty(this.PanelSetOrder.Fax) == true)
            {
                result.Success = false;
                result.Message += "The Fax number is not set." + Environment.NewLine;
            }
            return result;
        }

        private bool MaskNumberIsValid(Xceed.Wpf.Toolkit.MaskedTextBox maskedTextBox)
        {
            bool result = false;
            if (maskedTextBox.IsMaskFull == true && maskedTextBox.HasValidationError == false && maskedTextBox.HasParsingError == false)
            {
                result = true;
            }
            else if (maskedTextBox.IsMaskCompleted == false && maskedTextBox.HasValidationError == false && maskedTextBox.HasParsingError == false)
            {
                result = true;
            }

            if (result == false) MessageBox.Show("The Fax (or phone) number must be 10 digits or empty.");
            return result;
        }

        private void HyperlinkReceiveCompletedRequest_Click(object sender, RoutedEventArgs e)
        {
        }
    }
}
