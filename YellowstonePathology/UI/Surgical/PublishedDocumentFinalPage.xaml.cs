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

namespace YellowstonePathology.UI.Surgical
{	
	public partial class PublishedDocumentFinalPage : Test.ResultControl, INotifyPropertyChanged 
	{
		public event PropertyChangedEventHandler PropertyChanged;

		public delegate void CloseEventHandler(object sender, EventArgs e);
        public event CloseEventHandler Close;

		private YellowstonePathology.Business.User.SystemIdentity m_SystemIdentity;
		private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
		private string m_PageHeaderText;

		private YellowstonePathology.Business.Test.PanelSetOrder m_PanelSetOrder;		

        public PublishedDocumentFinalPage(YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder,
			YellowstonePathology.Business.Test.AccessionOrder accessionOrder,
			YellowstonePathology.Business.User.SystemIdentity systemIdentity) : base(panelSetOrder, accessionOrder)
		{
            this.m_PanelSetOrder = panelSetOrder;
			this.m_AccessionOrder = accessionOrder;
			this.m_SystemIdentity = systemIdentity;

			this.m_PageHeaderText = "Published Document Final Page: " + this.m_AccessionOrder.PatientDisplayName;			

			InitializeComponent();

			DataContext = this;

            Loaded += PublishedDocumentFinalPage_Loaded;
            Unloaded += PublishedDocumentFinalPage_Unloaded;

            this.m_ControlsNotDisabledOnFinal.Add(this.ButtonClose);
            this.m_ControlsNotDisabledOnFinal.Add(this.TextBlockUnfinalResults);
		}

        private void PublishedDocumentFinalPage_Loaded(object sender, RoutedEventArgs e)
        {
             
        }

        private void PublishedDocumentFinalPage_Unloaded(object sender, RoutedEventArgs e)
        {
             
        }

        public YellowstonePathology.Business.Test.PanelSetOrder PanelSetOrder
		{
			get { return this.m_PanelSetOrder; }
		}		

		public string PageHeaderText
		{
			get { return this.m_PageHeaderText; }
		}

		public bool OkToSaveOnNavigation(Type pageNavigatingTo)
		{
			return true;
		}

		public bool OkToSaveOnClose()
		{
			return true;
		}

		public void Save(bool releaseLock)
		{
            
        }

        public void UpdateBindingSources()
		{

		}		

        private void HyperLinkFinalizeResults_Click(object sender, RoutedEventArgs e)
        {
			if (this.m_PanelSetOrder.Final == false)
			{
				this.m_PanelSetOrder.Finish(this.m_AccessionOrder);
			}
			else
			{
				MessageBox.Show("This case cannot be finalized because it is already final.");
			}
		}

		private void HyperLinkUnfinalResults_Click(object sender, RoutedEventArgs e)
		{
			if (this.m_PanelSetOrder.Final == true)
			{
				this.m_PanelSetOrder.Unfinalize();
			}
			else
			{
				MessageBox.Show("This case cannot be unfinalized because it is not final.");
			}
		}

		private void HyperLinkAcceptResults_Click(object sender, RoutedEventArgs e)
		{			
			YellowstonePathology.Business.Rules.MethodResult result = this.m_PanelSetOrder.IsOkToAccept();
			if (result.Success == true)
			{
				this.m_PanelSetOrder.Accept();
			}
			else
			{
				MessageBox.Show(result.Message);
			}		
		}

		private void HyperLinkUnacceptResults_Click(object sender, RoutedEventArgs e)
		{
			YellowstonePathology.Business.Rules.MethodResult result = this.m_PanelSetOrder.IsOkToUnaccept();
			if (result.Success == true)
			{
				this.m_PanelSetOrder.Unaccept();
			}
			else
			{
				MessageBox.Show(result.Message);
			}
		}

		private void ButtonClose_Click(object sender, RoutedEventArgs e)
		{
            if (this.Close != null) this.Close(this, new EventArgs());
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
