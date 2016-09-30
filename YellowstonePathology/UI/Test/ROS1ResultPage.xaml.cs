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
	public partial class ROS1ResultPage : ResultControl, INotifyPropertyChanged 
	{
		public event PropertyChangedEventHandler PropertyChanged;

		public delegate void NextEventHandler(object sender, EventArgs e);
		public event NextEventHandler Next;

		private YellowstonePathology.Business.User.SystemIdentity m_SystemIdentity;
		private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
        private string m_PageHeaderText;

        private YellowstonePathology.Business.Test.ROS1ByFISH.ROS1ByFISHTestOrder m_ROS1ByFISHTestOrder;
        private string m_OrderedOnDescription;

        private YellowstonePathology.Business.Test.ROS1ByFISH.ROS1ByFISHResultCollection m_ResultCollection;

        public ROS1ResultPage(YellowstonePathology.Business.Test.ROS1ByFISH.ROS1ByFISHTestOrder ros1ByFISHTestOrder,
            YellowstonePathology.Business.Test.AccessionOrder accessionOrder,
			YellowstonePathology.Business.User.SystemIdentity systemIdentity) : base(ros1ByFISHTestOrder, accessionOrder)
		{
            this.m_ROS1ByFISHTestOrder = ros1ByFISHTestOrder;
			this.m_AccessionOrder = accessionOrder;			
			this.m_SystemIdentity = systemIdentity;

            this.m_ResultCollection = new Business.Test.ROS1ByFISH.ROS1ByFISHResultCollection();
            this.m_PageHeaderText = "ROS1 Results For: " + this.m_AccessionOrder.PatientDisplayName;

			YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrderByOrderTarget(this.m_ROS1ByFISHTestOrder.OrderedOnId);
            YellowstonePathology.Business.Test.AliquotOrder aliquotOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetAliquotOrder(this.m_ROS1ByFISHTestOrder.OrderedOnId);
            this.m_OrderedOnDescription = specimenOrder.Description;
            if(aliquotOrder != null) this.m_OrderedOnDescription += ": " + aliquotOrder.Label;

			InitializeComponent();

			DataContext = this;				
			
			Loaded += ROS1ResultPage_Loaded;

            this.m_ControlsNotDisabledOnFinal.Add(this.ButtonNext);
            this.m_ControlsNotDisabledOnFinal.Add(this.TextBlockShowDocument);
            this.m_ControlsNotDisabledOnFinal.Add(this.TextBlockUnfinalResults);
        }

        public void ROS1ResultPage_Loaded(object sender, RoutedEventArgs e)
        {
        	this.ComboBoxResult.SelectionChanged += ComboBoxResult_SelectionChanged;             
        }

        public YellowstonePathology.Business.Test.ROS1ByFISH.ROS1ByFISHResultCollection ResultCollection
        {
            get { return this.m_ResultCollection; }
        }

        public string OrderedOnDescription
        {
            get { return this.m_OrderedOnDescription; }
        }

        public YellowstonePathology.Business.Test.ROS1ByFISH.ROS1ByFISHTestOrder ROS1ByFISHTestOrder
        {
            get { return this.m_ROS1ByFISHTestOrder; }
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

		private void HyperLinkShowDocument_Click(object sender, RoutedEventArgs e)
		{            
            YellowstonePathology.Business.Test.ROS1ByFISH.ROS1ByFISHWordDocument report = new Business.Test.ROS1ByFISH.ROS1ByFISHWordDocument(this.m_AccessionOrder, this.m_ROS1ByFISHTestOrder, Business.Document.ReportSaveModeEnum.Draft);
            report.Render();

			YellowstonePathology.Business.OrderIdParser orderIdParser = new Business.OrderIdParser(this.m_ROS1ByFISHTestOrder.ReportNo);
            string fileName = YellowstonePathology.Business.Document.CaseDocument.GetDraftDocumentFilePath(orderIdParser);
            YellowstonePathology.Business.Document.CaseDocument.OpenWordDocumentWithWordViewer(fileName);         
		}

        private void HyperLinkFinalizeResults_Click(object sender, RoutedEventArgs e)
        {
            if (this.m_ROS1ByFISHTestOrder.Accepted == true)
            {
                if (this.m_ROS1ByFISHTestOrder.Final == false)
                {
                    this.m_ROS1ByFISHTestOrder.Finish(this.m_AccessionOrder);
                }
                else
                {
                    MessageBox.Show("This case cannot be finaled because it is already final.");
                }
            }
            else
            {
                MessageBox.Show("This case cannot be finaled because it is not accepted.");
            }
        }

		private void HyperLinkUnfinalResults_Click(object sender, RoutedEventArgs e)
		{            			
			if (this.m_ROS1ByFISHTestOrder.Final == true)
            {
                this.m_ROS1ByFISHTestOrder.Unfinalize();
            }
            else
            {
                MessageBox.Show("This case cannot be unfinaled because it is not final.");
            }             
		}

		private void HyperLinkAcceptResults_Click(object sender, RoutedEventArgs e)
		{
            if (this.m_ROS1ByFISHTestOrder.Final == false)
            {
                if (this.m_ROS1ByFISHTestOrder.Accepted == false)
                {
                    this.m_ROS1ByFISHTestOrder.Accept();
                }
                else
                {
                    MessageBox.Show("This case cannot be accepted because it is already accepted.");
                }
            }
            else
            {
                MessageBox.Show("This case cannot be accepted because it is final.");
            }
		}

		private void HyperLinkUnacceptResults_Click(object sender, RoutedEventArgs e)
		{            			
			if (this.m_ROS1ByFISHTestOrder.Accepted == true)
			{
				this.m_ROS1ByFISHTestOrder.Unaccept();
			}
			else
			{
				MessageBox.Show("This case cannot be unaccepted because it is not accepted.");
			}            
		}

        private void HyperLinkCloseWindow_Click(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this).Close();
        }

        private void ButtonNext_Click(object sender, RoutedEventArgs e)
        {       
            if (this.Next != null) this.Next(this, new EventArgs());
        }

        private void HyperLinkSetResults_Click(object sender, RoutedEventArgs e)
        {
            if (this.m_ROS1ByFISHTestOrder.Accepted == false)
            {
                if (this.ComboBoxResult.SelectedItem != null)
                {
                    YellowstonePathology.Business.Test.ROS1ByFISH.ROS1ByFISHResult result = (YellowstonePathology.Business.Test.ROS1ByFISH.ROS1ByFISHResult)this.ComboBoxResult.SelectedItem;
                    result.SetResult(this.m_ROS1ByFISHTestOrder);
                }
            }
            else
            {
                MessageBox.Show("You cannot set the result because the test has been accepted.");
            }
        }

        private void ComboBoxResult_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.ComboBoxResult.SelectedItem != null)
            {
                YellowstonePathology.Business.Test.ROS1ByFISH.ROS1ByFISHResult result = (YellowstonePathology.Business.Test.ROS1ByFISH.ROS1ByFISHResult)this.ComboBoxResult.SelectedItem;
                this.m_ROS1ByFISHTestOrder.ResultCode = result.ResultCode;
            }
        }
	}
}
