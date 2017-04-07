﻿using System;
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
        private string m_OrderedOnDescription;        

        public RetrospectiveReviewResultPage(YellowstonePathology.Business.Test.RetrospectiveReview.RetrospectiveReviewTestOrder retrospectiveReviewTestOrder,
            YellowstonePathology.Business.Test.AccessionOrder accessionOrder,
			YellowstonePathology.Business.User.SystemIdentity systemIdentity) : base(retrospectiveReviewTestOrder, accessionOrder)
		{
            this.m_PanelSetOrder = retrospectiveReviewTestOrder;
			this.m_AccessionOrder = accessionOrder;			
			this.m_SystemIdentity = systemIdentity;
            
            this.m_PageHeaderText = "Retrospective Review Results For: " + this.m_AccessionOrder.PatientDisplayName;

			YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrderByOrderTarget(this.m_PanelSetOrder.OrderedOnId);
            YellowstonePathology.Business.Test.AliquotOrder aliquotOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetAliquotOrder(this.m_PanelSetOrder.OrderedOnId);
            this.m_OrderedOnDescription = specimenOrder.Description;
            if(aliquotOrder != null) this.m_OrderedOnDescription += ": " + aliquotOrder.Label;

			InitializeComponent();

			DataContext = this;						
        }                

        public string OrderedOnDescription
        {
            get { return this.m_OrderedOnDescription; }
        }

        public YellowstonePathology.Business.Test.RetrospectiveReview.RetrospectiveReviewTestOrder PanelSetOrder
        {
            get { return this.m_PanelSetOrder; }
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
            YellowstonePathology.Business.Test.ROS1ByFISH.ROS1ByFISHWordDocument report = new Business.Test.ROS1ByFISH.ROS1ByFISHWordDocument(this.m_AccessionOrder, this.m_PanelSetOrder, Business.Document.ReportSaveModeEnum.Draft);
            report.Render();

			YellowstonePathology.Business.OrderIdParser orderIdParser = new Business.OrderIdParser(this.m_PanelSetOrder.ReportNo);
            string fileName = YellowstonePathology.Business.Document.CaseDocument.GetDraftDocumentFilePath(orderIdParser);
            YellowstonePathology.Business.Document.CaseDocument.OpenWordDocumentWithWordViewer(fileName);         
		}

        private void HyperLinkFinalizeResults_Click(object sender, RoutedEventArgs e)
        {
            
        }

		private void HyperLinkUnfinalResults_Click(object sender, RoutedEventArgs e)
		{            			
			         
		}

		private void HyperLinkAcceptResults_Click(object sender, RoutedEventArgs e)
		{
            
		}

		private void HyperLinkUnacceptResults_Click(object sender, RoutedEventArgs e)
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

        private void HyperLinkSetResults_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void ComboBoxResult_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }
	}
}