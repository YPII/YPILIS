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

        private YellowstonePathology.Business.Test.MissingInformation.MissingInformtionTestOrder m_MissingInformtionTestOrder;

        public MissingInformationResultPage(YellowstonePathology.Business.Test.MissingInformation.MissingInformtionTestOrder missingInformationTestOrder,
			YellowstonePathology.Business.Test.AccessionOrder accessionOrder) : base(missingInformationTestOrder, accessionOrder)
		{            
			this.m_AccessionOrder = accessionOrder;						             

			this.m_MissingInformtionTestOrder = missingInformationTestOrder;
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

		public YellowstonePathology.Business.Test.MissingInformation.MissingInformtionTestOrder MissingInformtionTestOrder
        {
            get { return this.m_MissingInformtionTestOrder; }
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
			if (this.m_MissingInformtionTestOrder.Final == false)
			{				
				this.m_MissingInformtionTestOrder.Finish(this.m_AccessionOrder);				
			}
			else
			{
				MessageBox.Show("This case cannot be finalized because it is already final.");
			}
		}

		private void HyperLinkUnfinalResults_Click(object sender, RoutedEventArgs e)
		{
			if (this.m_MissingInformtionTestOrder.Final == true)
			{
				this.m_MissingInformtionTestOrder.Unfinalize();
			}
			else
			{
				MessageBox.Show("This case cannot be unfinalized because it is not final.");
			}
		}        

        private void HyperLinkFirstCall_Click(object sender, RoutedEventArgs e)
        {            
            if (this.m_MissingInformtionTestOrder.FirstCall == false)
            {
                this.m_MissingInformtionTestOrder.SetFirstCall();
            }
            else
            {
                MessageBox.Show("The first call has already been made.");
            }
        }

        private void HyperLinkSecondCall_Click(object sender, RoutedEventArgs e)
        {
            if (this.m_MissingInformtionTestOrder.SecondCall == false)
            {
                this.m_MissingInformtionTestOrder.SetSecondCall();
            }
            else
            {
                MessageBox.Show("The second call has already been made.");
            }
        }

        private void HyperLinkThirdCall_Click(object sender, RoutedEventArgs e)
        {
            if (this.m_MissingInformtionTestOrder.ThirdCall == false)
            {
                this.m_MissingInformtionTestOrder.SetThirdCall();
            }
            else
            {
                MessageBox.Show("The third call has already been made.");
            }
        }        

        private void HyperLinkFax_Click(object sender, RoutedEventArgs e)
        {
            if (this.m_MissingInformtionTestOrder.Fax == false)
            {
                this.m_MissingInformtionTestOrder.Fax = true;
                this.m_MissingInformtionTestOrder.FaxSentBy = Business.User.SystemIdentity.Instance.User.DisplayName;
                this.m_MissingInformtionTestOrder.TimeFaxSent = DateTime.Now;
                this.NotifyPropertyChanged("FaxDisplayString");
            }
            else
            {
                MessageBox.Show("A fax has already sent.");
            }
        }

        private void HyperLinkClientSystemLookup_Click(object sender, RoutedEventArgs e)
        {
            if (this.m_MissingInformtionTestOrder.ClientSystemLookup == false)
            {
                this.m_MissingInformtionTestOrder.ClientSystemLookup = true;
                this.m_MissingInformtionTestOrder.ClientSystemLookupBy = Business.User.SystemIdentity.Instance.User.DisplayName;
                this.m_MissingInformtionTestOrder.TimeOfClientSystemLookup = DateTime.Now;
                this.NotifyPropertyChanged("ClientSystemLookupDisplayString");
            }
            else
            {
                MessageBox.Show("The Client System Lookup is already set.");
            }
        }

        private void HyperLinkClearResults_Click(object sender, RoutedEventArgs e)
        {
            this.m_MissingInformtionTestOrder.FirstCall = false;
            this.m_MissingInformtionTestOrder.FirstCallMadeBy = null;
            this.m_MissingInformtionTestOrder.TimeOfFirstCall = null;
            this.m_MissingInformtionTestOrder.FirstCallComment = null;

            this.m_MissingInformtionTestOrder.SecondCall = false;
            this.m_MissingInformtionTestOrder.SecondCallMadeBy = null;
            this.m_MissingInformtionTestOrder.TimeOfSecondCall = null;
            this.m_MissingInformtionTestOrder.SecondCallComment = null;

            this.m_MissingInformtionTestOrder.ThirdCall = false;
            this.m_MissingInformtionTestOrder.ThirdCallMadeBy = null;
            this.m_MissingInformtionTestOrder.TimeOfThirdCall = null;
            this.m_MissingInformtionTestOrder.ThirdCallComment = null;

            this.m_MissingInformtionTestOrder.Fax = false;
            this.m_MissingInformtionTestOrder.FaxSentBy = null;
            this.m_MissingInformtionTestOrder.TimeFaxSent = null;

            this.m_MissingInformtionTestOrder.ClientSystemLookup = false;
            this.m_MissingInformtionTestOrder.ClientSystemLookupBy = null;
            this.m_MissingInformtionTestOrder.TimeOfClientSystemLookup = null;

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
