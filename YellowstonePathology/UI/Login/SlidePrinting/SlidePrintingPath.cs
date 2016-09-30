using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI.Login.SlidePrinting
{
    public class SlidePrintingPath
    {
        public delegate void DoneEventHandler(object sender, EventArgs e);
        public event DoneEventHandler Done;

        private Login.Receiving.LoginPageWindow m_LoginPageWindow;

        public SlidePrintingPath()
        {                        
            
        }

        public void Start()
        {
			YellowstonePathology.Business.User.SystemIdentity systemIdentity = Business.User.SystemIdentity.Instance;
            this.m_LoginPageWindow = new Login.Receiving.LoginPageWindow();
            this.m_LoginPageWindow.Show();
            this.ShowScanContainerPage();
        }

        private void ShowScanContainerPage()
        {
            ScanContainerPage scanContainerPage = new ScanContainerPage();
            scanContainerPage.Back += new ScanContainerPage.BackEventHandler(ScanContainerPage_Back);
            scanContainerPage.ContainerScannedReceived += new ScanContainerPage.ContainScanReceivedEventHandler(ScanContainerPage_ContainerScannedReceived);
            this.m_LoginPageWindow.PageNavigator.Navigate(scanContainerPage);
        }

		private void ScanContainerPage_ContainerScannedReceived(object sender, Business.BarcodeScanning.ContainerBarcode containerBarcode)
        {
            string masterAccessionNo = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetMasterAccessionNoFromContainerId(containerBarcode.ToString());
            YellowstonePathology.Business.Test.AccessionOrder accessionOrder = YellowstonePathology.Business.Persistence.DocumentGateway.Instance.PullAccessionOrder(masterAccessionNo, this.m_LoginPageWindow);
            this.ShowPrintSlidesPage(containerBarcode.ToString(), accessionOrder);
        }

        private void ShowPrintSlidesPage(string containerId, YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {
            PrintSlidePage printSlidePage = new PrintSlidePage(containerId, accessionOrder);
            printSlidePage.Back += new PrintSlidePage.BackEventHandler(PrintSlidePage_Back);
            this.m_LoginPageWindow.PageNavigator.Navigate(printSlidePage);
        }

        private void PrintSlidePage_Back(object sender, EventArgs e)
        {
            this.ShowScanContainerPage();
        }

        private void ScanContainerPage_Back(object sender, EventArgs e)
        {
            this.m_LoginPageWindow.Close();
            if (this.Done != null) this.Done(this, new EventArgs());
        }
    }
}
