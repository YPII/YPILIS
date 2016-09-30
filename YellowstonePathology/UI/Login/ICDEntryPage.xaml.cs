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
using System.Xml;
using System.Xml.Linq;

namespace YellowstonePathology.UI.Login
{
	/// <summary>
	/// Interaction logic for HPV1618ResultPage.xaml
	/// </summary>
	public partial class ICDEntryPage : UserControl, INotifyPropertyChanged 
	{
		public event PropertyChangedEventHandler PropertyChanged;

		public delegate void NextEventHandler(object sender, EventArgs e);
		public event NextEventHandler Next;

        public delegate void BackEventHandler(object sender, EventArgs e);
        public event BackEventHandler Back;
		
		private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
		private string m_PageHeaderText;
		private string m_ReportNo;
		

        public ICDEntryPage(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo)
		{			
			this.m_AccessionOrder = accessionOrder;
			this.m_ReportNo = reportNo;

			this.m_PageHeaderText = "ICD Entry for: " + this.m_AccessionOrder.PatientDisplayName;
			
			InitializeComponent();

			DataContext = this;
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

		public YellowstonePathology.Business.Test.AccessionOrder AccessionOrder
		{
			get { return this.m_AccessionOrder; }
		}		

        private void ButtonNext_Click(object sender, RoutedEventArgs e)
        {
            if (this.IsOkToGoNext() == true)
            {
                if (this.Next != null)
                {
                    YellowstonePathology.Business.Persistence.DocumentGateway.Instance.Save();
                    this.Next(this, new EventArgs());
                }
            }
        }

        private bool IsOkToGoNext()
        {
            bool result = true;
			if (this.m_AccessionOrder.PanelSetOrderCollection.HasWomensHealthProfileOrder() == true)
            {
				YellowstonePathology.Business.Test.WomensHealthProfile.WomensHealthProfileTestOrder womensHealthProfileTestOrder = (YellowstonePathology.Business.Test.WomensHealthProfile.WomensHealthProfileTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(116);
				if (womensHealthProfileTestOrder.OrderHPV == true)
                {
                    if (this.m_AccessionOrder.ICD9BillingCodeCollection.CodeExists("Z11.51") == false)
                    {
                        MessageBoxResult messageBoxResult = MessageBox.Show("A routine HPV was requested but to Z11.51 was not found. Are you sure you want to continue.", "Routine HPV Requested", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
                        if (messageBoxResult == MessageBoxResult.No)
                        {
                            result = false;                            
                        }
                    }
                }
            }
            return result;
        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            if (this.Back != null) this.Back(this, new EventArgs());
        }

        private void ListBoxCodes_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
			ListBox listBox = (ListBox)sender;
			XmlElement element = (XmlElement)listBox.SelectedItem;
			string icd9Code = element.GetAttribute("ICD9");
            string icd10Code = element.GetAttribute("ICD10");

			int quantity = Convert.ToInt32(element.GetAttribute("Quantity"));
            this.AddICD9Code(icd9Code, icd10Code, quantity);
        }		

		private void AddICD9Code(string icd9Code, string icd10Code, int quantity)
		{
			string specimenOrderId = this.m_AccessionOrder.SpecimenOrderCollection[0].SpecimenOrderId;
			YellowstonePathology.Business.Billing.Model.ICD9BillingCode icd9BillingCode = this.m_AccessionOrder.ICD9BillingCodeCollection.GetNextItem(this.m_ReportNo,
                this.m_AccessionOrder.MasterAccessionNo, specimenOrderId, icd9Code, icd10Code, quantity);
			this.m_AccessionOrder.ICD9BillingCodeCollection.Add(icd9BillingCode);
		}		

        private void HyperlinkAddICDCode_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(this.TextBoxICD10Code.Text) == false)
            {
                this.AddICD9Code(null, this.TextBoxICD10Code.Text, 1);
            }            
        }

        private void MenuItemDeleteICDCode_Click(object sender, RoutedEventArgs e)
        {
            if (this.ListViewICD9BillingCodes.SelectedItem != null)
            {
                YellowstonePathology.Business.Billing.Model.ICD9BillingCode icd9BillingCode = (YellowstonePathology.Business.Billing.Model.ICD9BillingCode)this.ListViewICD9BillingCodes.SelectedItem;
                this.m_AccessionOrder.ICD9BillingCodeCollection.Remove(icd9BillingCode);
            }
        }          
	}
}
