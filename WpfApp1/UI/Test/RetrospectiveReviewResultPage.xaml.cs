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
	public partial class RetrospectiveReviewResultPage : ResultControl, INotifyPropertyChanged 
	{
		public event PropertyChangedEventHandler PropertyChanged;

		public delegate void NextEventHandler(object sender, EventArgs e);
		public event NextEventHandler Next;

		private YellowstonePathology.Business.User.SystemIdentity m_SystemIdentity;
		private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
        private string m_PageHeaderText;
        private YellowstonePathology.Business.Test.RetrospectiveReview.RetrospectiveReviewTestOrder m_PanelSetOrder;        

        public RetrospectiveReviewResultPage(YellowstonePathology.Business.Test.RetrospectiveReview.RetrospectiveReviewTestOrder retrospectiveReviewTestOrder,
            YellowstonePathology.Business.Test.AccessionOrder accessionOrder,
			YellowstonePathology.Business.User.SystemIdentity systemIdentity) : base(retrospectiveReviewTestOrder, accessionOrder)
		{
            this.m_PanelSetOrder = retrospectiveReviewTestOrder;
			this.m_AccessionOrder = accessionOrder;			
			this.m_SystemIdentity = systemIdentity;
            
            this.m_PageHeaderText = "Retrospective Review Results For: " + this.m_AccessionOrder.PatientDisplayName;			

			InitializeComponent();

            this.m_ControlsNotDisabledOnFinal.Add(this.ButtonNext);
            this.m_ControlsNotDisabledOnFinal.Add(this.TextBlockUnfinalize);

            DataContext = this;						
        }                        

        public YellowstonePathology.Business.Test.RetrospectiveReview.RetrospectiveReviewTestOrder PanelSetOrder
        {
            get { return this.m_PanelSetOrder; }
        }		

		public string PageHeaderText
		{
			get { return this.m_PageHeaderText; }
		}				        		

        private void HyperLinkFinalizeResults_Click(object sender, RoutedEventArgs e)
        {
            
        }		

        private void HyperLinkCloseWindow_Click(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this).Close();
        }

        private void ButtonNext_Click(object sender, RoutedEventArgs e)
        {       
            if (this.Next != null) this.Next(this, new EventArgs());
        }                

        private void HyperLinkDisagreements_Click(object sender, RoutedEventArgs e)
        {

        }

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        private void HyperLinkAgreeAll_Click(object sender, RoutedEventArgs e)
        {
            foreach(Business.Test.RetrospectiveReview.RetrospectiveReviewTestOrderDetail rrtod in this.m_PanelSetOrder.RetrospectiveReviewTestOrderDetailCollection)
            {
                rrtod.Result = "Agree";
                rrtod.Impact = null;
                rrtod.Level = null;
                rrtod.Type = null;
                this.NotifyPropertyChanged(string.Empty);
            }
        }

        private void ComboboxResult_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cmbBox = (ComboBox)e.Source;            
            Business.Test.RetrospectiveReview.RetrospectiveReviewTestOrderDetail rrtod = (Business.Test.RetrospectiveReview.RetrospectiveReviewTestOrderDetail)cmbBox.Tag;
            if(rrtod.Result == "Agree")
            {
                rrtod.Impact = null;
                rrtod.Level = null;
                rrtod.Type = null;
            }
        }

        private void HyperLinkFinalize_Click(object sender, RoutedEventArgs e)
        {
            this.m_PanelSetOrder.Accept();
            this.m_PanelSetOrder.Finish(this.m_AccessionOrder);
        }

        private void HyperLinkUnfinalize_Click(object sender, RoutedEventArgs e)
        {
            this.m_PanelSetOrder.Unfinalize();
            this.m_PanelSetOrder.Unaccept();
        }
    }
}
