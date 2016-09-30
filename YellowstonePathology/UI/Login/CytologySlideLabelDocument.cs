using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace YellowstonePathology.UI.Login
{
    public class CytologySlideLabelDocument : FixedDocument
    {
        public CytologySlideLabelDocument(YellowstonePathology.Business.Slide.Model.SlideLabelCollection slideLabels, bool acidWash)
        {
            foreach (YellowstonePathology.Business.Slide.Model.SlideLabel slideLabel in slideLabels)
            {
                this.AddPage(slideLabel.ReportNo, slideLabel.PLastName, acidWash);
            }
        }

        public CytologySlideLabelDocument(string reportNo, string lastName, bool acidWash)
        {
            this.AddPage(reportNo, lastName, acidWash);
        }

        private void AddPage(string reportNo, string lastName, bool acidWash)
        {
            PageContent pageContent = new PageContent();
            FixedPage fixedPage = new FixedPage();

            StackPanel rowStackPanel = new StackPanel();
            rowStackPanel.Orientation = Orientation.Horizontal;
            rowStackPanel.Margin = new Thickness(9,5,5,5);

			YellowstonePathology.Business.OrderIdParser orderIdParser = new Business.OrderIdParser(reportNo);
			string labelLine1 = string.Empty;
			string labelLine2 = string.Empty;
			
			string masterAccessionNo = orderIdParser.MasterAccessionNo;
            string crc = YellowstonePathology.Business.BarcodeScanning.CytycCRC32.ComputeCrc(masterAccessionNo);
			labelLine1 = YellowstonePathology.Business.BarcodeScanning.CrcLabel.LabelLine1(masterAccessionNo);
			labelLine2 = YellowstonePathology.Business.BarcodeScanning.CrcLabel.LabelLine2(masterAccessionNo, crc);			

            CytologySlideLabelPanel label1 = new CytologySlideLabelPanel(labelLine1, labelLine2, lastName, acidWash);
            CytologySlideLabelPanel label2 = new CytologySlideLabelPanel(labelLine1, labelLine2, lastName, acidWash);
            CytologySlideLabelPanel label3 = new CytologySlideLabelPanel(labelLine1, labelLine2, lastName, acidWash);

            rowStackPanel.Children.Add(label1);
            rowStackPanel.Children.Add(label2);
            rowStackPanel.Children.Add(label3);

            fixedPage.Children.Add(rowStackPanel);
            ((System.Windows.Markup.IAddChild)pageContent).AddChild(fixedPage);
            this.Pages.Add(pageContent);
        }
    }
}
