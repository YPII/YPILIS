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
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace YellowstonePathology.UI.Billing
{	
	public partial class ADTPage : UserControl, INotifyPropertyChanged 
	{
		public event PropertyChangedEventHandler PropertyChanged;

		public delegate void NextEventHandler(object sender, EventArgs e);
		public event NextEventHandler Next;

		public delegate void BackEventHandler(object sender, EventArgs e);
		public event BackEventHandler Back;

        private Business.Test.AccessionOrder m_AccessionOrder;
        private Business.HL7View.ADTMessages m_ADTMessages = new Business.HL7View.ADTMessages();
        private string m_HL7;

        public ADTPage(Business.Test.AccessionOrder accessionOrder)
		{
            this.m_AccessionOrder = accessionOrder;
            this.m_ADTMessages = Business.Gateway.AccessionOrderGateway.GetADTMessagesByPatientNameDOB(accessionOrder.PFirstName, accessionOrder.PLastName, accessionOrder.PBirthdate.Value);

			InitializeComponent();			
			DataContext = this;
		}   
        
        public Business.HL7View.ADTMessages ADTMessages
        {
            get { return this.m_ADTMessages; }
        }     

        public string HL7
        {
            get { return this.m_HL7; }
            set { this.m_HL7 = value; }
        }

		public void NotifyPropertyChanged(String info)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(info));
			}
		}		

		private void ButtonNext_Click(object sender, RoutedEventArgs e)
		{            
            if (this.Next != null) this.Next(this, new EventArgs());            
		}        

		private void ButtonBack_Click(object sender, RoutedEventArgs e)
		{
			if (this.Back != null) this.Back(this, new EventArgs());
		}

        private void ListViewADT_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(this.ListViewADTItems.SelectedItem != null)
            {
                Business.HL7View.ADTMessage adtMessage = (Business.HL7View.ADTMessage)this.ListViewADTItems.SelectedItem;
                this.m_HL7 = adtMessage.Message;
                this.NotifyPropertyChanged("HL7");
            }
        }

        private void ButtonInsuranceCard_Click(object sender, RoutedEventArgs e)
        {
            Business.OrderIdParser orderIdParser = new Business.OrderIdParser(this.m_AccessionOrder.MasterAccessionNo);
            YellowstonePathology.Business.Document.ADTInsuranceDocument adtInsuranceDocument = new Business.Document.ADTInsuranceDocument(this.m_ADTMessages);
            adtInsuranceDocument.SaveAsTIF(orderIdParser);
        }

        private void ButtonSet_Click(object sender, RoutedEventArgs e)
        {
            this.m_AccessionOrder.PrimaryInsurance = this.m_ADTMessages.GetPrimaryInsurance();
        }
    }
}
