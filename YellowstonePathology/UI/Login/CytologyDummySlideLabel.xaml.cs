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
using System.Windows.Shapes;

namespace YellowstonePathology.UI.Login
{
    /// <summary>
    /// Interaction logic for ReprintSlideLabel.xaml
    /// </summary>
    public partial class CytologyDummySlideLabel : Window
    {
        YellowstonePathology.Business.Slide.Model.SlideLabelCollection m_SlideLabels;

        public CytologyDummySlideLabel()
        {
			m_SlideLabels = new YellowstonePathology.Business.Slide.Model.SlideLabelCollection();
            InitializeComponent();
        }

        public YellowstonePathology.Business.Slide.Model.SlideLabelCollection SlideLabels
        {
            get { return this.m_SlideLabels; }
        }

        private void ButtonOk_Click(object sender, RoutedEventArgs e)
        {
            this.TextBoxRealReportNumber.Text = this.TextBoxRealReportNumber.Text.ToUpper();
            this.TextBoxDummyReportNumber.Text = this.TextBoxDummyReportNumber.Text.ToUpper();

            if (this.TextBoxRealReportNumber.Text.Length < 4)
            {
                MessageBox.Show("The real report number does not appear to be a valid number.\n\nPlease check it and try again.", "Invalid report number", MessageBoxButton.OK);
                return;
            }
            string lastName = this.GetPatientLastName();

            if (lastName.Length == 0)
            {
                MessageBox.Show("The report number does not appear to be a valid number.\n\nPlease check it and try again.", "Case not found", MessageBoxButton.OK);
                return;
            }

            YellowstonePathology.UI.Login.CytologySlideLabelDocument cyologySlideLabelDocument = new CytologySlideLabelDocument(this.TextBoxDummyReportNumber.Text, lastName, false);
            System.Windows.Controls.PrintDialog printDialog = new System.Windows.Controls.PrintDialog();

            System.Printing.PrintQueue printQueue = YellowstonePathology.UI.PrintQueueFactory.GetSlideLabelPrintQueue(YellowstonePathology.Properties.Settings.Default.CytologySlideLabelPrinterName);
            printDialog.PrintQueue = printQueue;
            printDialog.PrintDocument(cyologySlideLabelDocument.DocumentPaginator, "Slide Labels");   
            
            Close();
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void FillSlideLabel( string lastName)
        {
            YellowstonePathology.Business.Slide.Model.SlideLabel slideLabel = new Business.Slide.Model.SlideLabel();
            slideLabel.Quantity = 1;
            slideLabel.ReportNo = this.TextBoxDummyReportNumber.Text;
            slideLabel.PLastName = lastName;
            this.m_SlideLabels.Add(slideLabel);            
        }

        private string GetPatientLastName()
        {
            MessageBox.Show("Sid Needs to work on this.  Please tell me you saw this.");

            /*
            string lastName = string.Empty;
			YellowstonePathology.Business.Test.AccessionOrder accessionOrder = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetAccessionOrderByReportNo(this.TextBoxRealReportNumber.Text);
			if (accessionOrder != null)
            {
                lastName = accessionOrder.PLastName;
            }
            return lastName;

            */

            throw new Exception("Not finished.");
        }
    }
}
