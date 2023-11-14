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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;

namespace YellowstonePathology.UI.Login
{
	/// <summary>
	/// Interaction logic for ClientFaxPage.xaml
	/// </summary>
	public partial class ClientFaxPage : UserControl, INotifyPropertyChanged
	{
		public delegate void PropertyChangedNotificationHandler(String info);
		public event PropertyChangedEventHandler PropertyChanged;

		public delegate void BackEventHandler(object sender, EventArgs e);
		public event BackEventHandler Back;

		public delegate void NextEventHandler(object sender, EventArgs e);
		public event NextEventHandler Next;

		private StringBuilder m_AbnEventComment;
		private StringBuilder m_InfoEventComment;

        private Business.Test.AccessionOrder m_AccessionOrder;
        private Business.Test.MissingInformation.MissingInformationTestOrder m_MissingInformationTestOrder;
        private YellowstonePathology.Business.User.SystemIdentity m_SystemIdentity;

        private string m_PatientNameWithBirthDate;		        
		private string m_TestName;		
		
		
		private string m_PageHeaderText = "Create Client Fax";

		public ClientFaxPage(Business.Test.AccessionOrder accessionOrder, Business.Test.MissingInformation.MissingInformationTestOrder missingInformationTestOrder,
			YellowstonePathology.Business.User.SystemIdentity systemIdentity)
		{
			this.m_AccessionOrder = accessionOrder;
            this.m_MissingInformationTestOrder = missingInformationTestOrder;
			this.m_SystemIdentity = systemIdentity;
			
			InitializeComponent();
			this.DataContext = this;

			this.m_AbnEventComment = new StringBuilder();
			this.m_InfoEventComment = new StringBuilder();
		}

		public string PageHeaderText
		{
			get { return this.m_PageHeaderText; }
		}

        public Business.Test.MissingInformation.MissingInformationTestOrder MissingInformationTestOrder
        {
            get { return this.m_MissingInformationTestOrder; }
        }

		public YellowstonePathology.Business.Test.AccessionOrder AccessionOrder
        {
            get { return this.m_AccessionOrder; }
        }						

		private void ButtonBack_Click(object sender, RoutedEventArgs e)
		{
			this.Back(this, new EventArgs());
		}

		private void ButtonNext_Click(object sender, RoutedEventArgs e)
		{
			this.Next(this, new EventArgs());
		}

		private void CreateLetterBody()
		{			
			this.m_PatientNameWithBirthDate = this.m_AccessionOrder.PatientName;
			if (this.m_AccessionOrder.PBirthdate.HasValue) m_PatientNameWithBirthDate = m_PatientNameWithBirthDate + " (DOB:" + this.m_AccessionOrder.PBirthdate.Value.ToShortDateString() + ")";
			StringBuilder result = new StringBuilder();
			bool created = CreateMissingABN(result);
			if (!created) created = CreateMissingInfo(result);
			if (!created) created = CreateMissingSignature(result);
			this.m_MissingInformationTestOrder.LetterBody = result.ToString();
		}

		private void ButtonCreateLetterBody_Click(object sender, RoutedEventArgs e)
		{
			this.CreateLetterBody();
		}

		private void ButtonFaxLetter_Click(object sender, RoutedEventArgs e)
		{
			if (this.m_AccessionOrder.ClientId != 0)
			{
				YellowstonePathology.Business.Client.Model.Client client = Business.Gateway.PhysicianClientGateway.GetClientByClientId(this.m_AccessionOrder.ClientId);				

                Business.Test.MissingInformation.MissingInformationWordDocument missingInformationWordDocument = new Business.Test.MissingInformation.MissingInformationWordDocument(this.m_AccessionOrder, this.m_MissingInformationTestOrder, Business.Document.ReportSaveModeEnum.Normal);
                missingInformationWordDocument.Render();
                missingInformationWordDocument.Publish();

                Business.OrderIdParser orderIdParser = new Business.OrderIdParser(this.m_MissingInformationTestOrder.ReportNo);
                string filePath = Business.Document.CaseDocument.GetCaseFileNameTif(orderIdParser);
                YellowstonePathology.Business.ReportDistribution.Model.FaxSubmission.Submit(client.Fax, "Missing Information", filePath, $"Missing Info: {this.m_AccessionOrder.MasterAccessionNo}");
            }
			else
			{
				MessageBox.Show("Client must be selected before a fax can be generated.");
			}
		}

		private void AppendToEventDescription(StringBuilder stringBuilder, string msg)
		{
			if (stringBuilder.Length > 9)
			{
				stringBuilder.Append("; ");
			}
			stringBuilder.Append(msg);
		}

		private bool CreateMissingABN(StringBuilder result)
		{
			bool res = false;
			if (this.CheckBoxABNDate.IsChecked == true ||
				this.CheckBoxABNEstimatedCost.IsChecked == true ||
				this.CheckBoxABNIdentificationNumber.IsChecked == true ||
				this.CheckBoxABNLaboratory.IsChecked == true ||
				this.CheckBoxABNNotifier.IsChecked == true ||
				this.CheckBoxABNOptionBoxChecked.IsChecked == true ||
				this.CheckBoxABNPatientName.IsChecked == true)
			{
				MissingABNLetterBody missingABNLetterBody = new MissingABNLetterBody();
				missingABNLetterBody.GetLetterBody(result, this.m_PatientNameWithBirthDate,
					this.CheckBoxABNDate.IsChecked == true,
					this.CheckBoxABNEstimatedCost.IsChecked == true,
					this.CheckBoxABNIdentificationNumber.IsChecked == true,
					this.CheckBoxABNLaboratory.IsChecked == true,
					this.CheckBoxABNNotifier.IsChecked == true,
					this.CheckBoxABNOptionBoxChecked.IsChecked == true,
					this.CheckBoxABNPatientName.IsChecked == true);

				res = true;

				if (this.CheckBoxABNDate.IsChecked == true) this.AppendToEventDescription(m_AbnEventComment, "Date");
				if (this.CheckBoxABNEstimatedCost.IsChecked == true) this.AppendToEventDescription(m_AbnEventComment, "Estimate Cost ");
				if (this.CheckBoxABNIdentificationNumber.IsChecked == true) this.AppendToEventDescription(m_AbnEventComment, "Identification Number ");
				if (this.CheckBoxABNLaboratory.IsChecked == true) this.AppendToEventDescription(m_AbnEventComment, "Laboratory Tests ");
				if (this.CheckBoxABNNotifier.IsChecked == true) this.AppendToEventDescription(m_AbnEventComment, "Notifier ");
				if (this.CheckBoxABNOptionBoxChecked.IsChecked == true) this.AppendToEventDescription(m_AbnEventComment, "Option Box Checked ");
				if (this.CheckBoxABNPatientName.IsChecked == true) this.AppendToEventDescription(m_AbnEventComment, "Patient Name ");
			}
			return res;
		}

		private bool CreateMissingInfo(StringBuilder result)
		{
			bool res = false;
			if (this.CheckBoxABN.IsChecked == true ||
				this.CheckBoxCytologyDXCode.IsChecked == true ||
				this.CheckBoxPatientDemographics.IsChecked == true ||
				this.CheckBoxNGCTDXCode.IsChecked == true ||
				this.CheckBoxTestType.IsChecked == true ||
				this.CheckBoxOrderingPhysician.IsChecked == true ||
				this.CheckBoxCollectionDate.IsChecked == true)
			{
				MissingInfoLetterBody missingInfoLetterBody = new MissingInfoLetterBody();
				missingInfoLetterBody.GetLetterBody(result, this.m_PatientNameWithBirthDate,
					this.CheckBoxABN.IsChecked == true,
					this.CheckBoxCytologyDXCode.IsChecked == true,
					this.CheckBoxPatientDemographics.IsChecked == true,
					this.CheckBoxNGCTDXCode.IsChecked == true,
					this.CheckBoxTestType.IsChecked == true,
					this.CheckBoxOrderingPhysician.IsChecked == true,
					this.CheckBoxCollectionDate.IsChecked == true);

				res = true;

				if (this.CheckBoxABN.IsChecked == true) this.AppendToEventDescription(m_InfoEventComment, "ABN");
				if (this.CheckBoxCytologyDXCode.IsChecked == true) this.AppendToEventDescription(m_InfoEventComment, "Diagnosis Code");
				if (this.CheckBoxPatientDemographics.IsChecked == true) this.AppendToEventDescription(m_InfoEventComment, "Patient Demographics");
                if (this.CheckBoxNGCTDXCode.IsChecked == true) this.AppendToEventDescription(m_InfoEventComment,  "NG/CT Diagnosis Code");                
				if (this.CheckBoxTestType.IsChecked == true) this.AppendToEventDescription(m_InfoEventComment, "Test Type");
				if (this.CheckBoxOrderingPhysician.IsChecked == true) this.AppendToEventDescription(m_InfoEventComment, "Ordering Physician");
				if (this.CheckBoxCollectionDate.IsChecked == true) this.AppendToEventDescription(m_InfoEventComment, "Collection Date");
			}
			return res;
		}

		private bool CreateMissingSignature(StringBuilder result)
		{
			bool res = false;
			this.m_TestName = string.Empty;
			if (this.CheckBoxMissingSignatureSurgical.IsChecked == true)
			{
				this.m_TestName = this.CheckBoxMissingSignatureSurgical.Tag.ToString();
			}

			if (this.CheckBoxMissingSignatureCytology.IsChecked == true)
			{
				this.m_TestName = this.CheckBoxMissingSignatureCytology.Tag.ToString();
			}

			if (this.CheckBoxMissingSignatureFlow.IsChecked == true)
			{
				this.m_TestName = this.CheckBoxMissingSignatureFlow.Tag.ToString();
			}

			if (this.CheckBoxMissingSignatureMolecular.IsChecked == true)
			{
				this.m_TestName = this.CheckBoxMissingSignatureMolecular.Tag.ToString();
			}
			if (this.m_TestName.Length > 0)
			{
				MissingSignatureLetterBody missingSignatureLetterBody = new MissingSignatureLetterBody();
				missingSignatureLetterBody.GetLetterBody(result, this.m_TestName, this.m_PatientNameWithBirthDate, this.m_AccessionOrder.PhysicianName);				
			}
			return res;
		}

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }        
    }
}
