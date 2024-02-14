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
using System.Xml.Linq;

namespace YellowstonePathology.UI.Test
{
	public partial class MissingInformationResultPage : ResultControl, INotifyPropertyChanged 
	{
		public event PropertyChangedEventHandler PropertyChanged;        

        public delegate void NextEventHandler(object sender, EventArgs e);
        public event NextEventHandler Next;

        public event EventHandler ShowICDEntry;
        public event EventHandler ShowFaxPage;
        
		private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
        private string m_PageHeaderText;

        private YellowstonePathology.Business.Test.MissingInformation.MissingInformationTestOrder m_MissingInformationTestOrder;

        public MissingInformationResultPage(YellowstonePathology.Business.Test.MissingInformation.MissingInformationTestOrder missingInformationTestOrder,
			YellowstonePathology.Business.Test.AccessionOrder accessionOrder) : base(missingInformationTestOrder, accessionOrder)
		{            
			this.m_AccessionOrder = accessionOrder;						             

			this.m_MissingInformationTestOrder = missingInformationTestOrder;
            this.m_PageHeaderText = "Missing Information For: " + this.m_AccessionOrder.PatientDisplayName;

			InitializeComponent();            

			this.DataContext = this;

            this.m_ControlsNotDisabledOnFinal.Add(this.ButtonNext);
            this.m_ControlsNotDisabledOnFinal.Add(this.TextBlockUnfinalResults);
        }

        public YellowstonePathology.Business.Test.AccessionOrder AccessionOrder
        {
            get { return this.m_AccessionOrder; }
        }

		public YellowstonePathology.Business.Test.MissingInformation.MissingInformationTestOrder MissingInformtionTestOrder
        {
            get { return this.m_MissingInformationTestOrder; }
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
            if (this.Next != null) this.Next(this, new EventArgs());            
        }

		private void HyperLinkFinalize_Click(object sender, RoutedEventArgs e)
		{
			if (this.m_MissingInformationTestOrder.Final == false)
			{				
				this.m_MissingInformationTestOrder.Finish(this.m_AccessionOrder);				
			}
			else
			{
				MessageBox.Show("This case cannot be finalized because it is already final.");
			}
		}

		private void HyperLinkUnfinalResults_Click(object sender, RoutedEventArgs e)
		{
			if (this.m_MissingInformationTestOrder.Final == true)
			{
				this.m_MissingInformationTestOrder.Unfinalize();
			}
			else
			{
				MessageBox.Show("This case cannot be unfinalized because it is not final.");
			}
		}        

        private void HyperLinkFirstCall_Click(object sender, RoutedEventArgs e)
        {            
            if (this.m_MissingInformationTestOrder.FirstCall == false)
            {
                this.m_MissingInformationTestOrder.SetFirstCall();
            }
            else
            {
                MessageBox.Show("The first call has already been made.");
            }
        }

        private void HyperLinkSecondCall_Click(object sender, RoutedEventArgs e)
        {
            if (this.m_MissingInformationTestOrder.SecondCall == false)
            {
                this.m_MissingInformationTestOrder.SetSecondCall();
            }
            else
            {
                MessageBox.Show("The second call has already been made.");
            }
        }

        private void HyperLinkThirdCall_Click(object sender, RoutedEventArgs e)
        {
            if (this.m_MissingInformationTestOrder.ThirdCall == false)
            {
                this.m_MissingInformationTestOrder.SetThirdCall();
            }
            else
            {
                MessageBox.Show("The third call has already been made.");
            }
        }        

        private void HyperLinkFax_Click(object sender, RoutedEventArgs e)
        {
            if (this.m_MissingInformationTestOrder.Fax == false)
            {
                this.m_MissingInformationTestOrder.Fax = true;
                this.m_MissingInformationTestOrder.FaxSentBy = Business.User.SystemIdentity.Instance.User.DisplayName;
                this.m_MissingInformationTestOrder.TimeFaxSent = DateTime.Now;
                this.NotifyPropertyChanged("FaxDisplayString");
            }
            else
            {
                MessageBox.Show("A fax has already sent.");
            }
        }

        private void HyperLinkClientSystemLookup_Click(object sender, RoutedEventArgs e)
        {
            if (this.m_MissingInformationTestOrder.ClientSystemLookup == false)
            {
                this.m_MissingInformationTestOrder.ClientSystemLookup = true;
                this.m_MissingInformationTestOrder.ClientSystemLookupBy = Business.User.SystemIdentity.Instance.User.DisplayName;
                this.m_MissingInformationTestOrder.TimeOfClientSystemLookup = DateTime.Now;
                this.NotifyPropertyChanged("ClientSystemLookupDisplayString");
            }
            else
            {
                MessageBox.Show("The Client System Lookup is already set.");
            }
        }

        private void HyperLinkClearResults_Click(object sender, RoutedEventArgs e)
        {
            this.m_MissingInformationTestOrder.FirstCall = false;
            this.m_MissingInformationTestOrder.FirstCallMadeBy = null;
            this.m_MissingInformationTestOrder.TimeOfFirstCall = null;
            this.m_MissingInformationTestOrder.FirstCallComment = null;

            this.m_MissingInformationTestOrder.SecondCall = false;
            this.m_MissingInformationTestOrder.SecondCallMadeBy = null;
            this.m_MissingInformationTestOrder.TimeOfSecondCall = null;
            this.m_MissingInformationTestOrder.SecondCallComment = null;

            this.m_MissingInformationTestOrder.ThirdCall = false;
            this.m_MissingInformationTestOrder.ThirdCallMadeBy = null;
            this.m_MissingInformationTestOrder.TimeOfThirdCall = null;
            this.m_MissingInformationTestOrder.ThirdCallComment = null;

            this.m_MissingInformationTestOrder.Fax = false;
            this.m_MissingInformationTestOrder.FaxSentBy = null;
            this.m_MissingInformationTestOrder.TimeFaxSent = null;

            this.m_MissingInformationTestOrder.ClientSystemLookup = false;
            this.m_MissingInformationTestOrder.ClientSystemLookupBy = null;
            this.m_MissingInformationTestOrder.TimeOfClientSystemLookup = null;

            this.NotifyPropertyChanged("*");
        }

        private void HyperLinkShowICDEntryPage_Click(object sender, RoutedEventArgs e)
        {
            if(this.ShowICDEntry != null)
            {
                this.ShowICDEntry(this, new EventArgs());
            }
        }

        private void HyperLinkShowFaxPage_Click(object sender, RoutedEventArgs e)
        {
            if (this.ShowFaxPage != null) this.ShowFaxPage(this, new EventArgs());            
        }
    }
}
