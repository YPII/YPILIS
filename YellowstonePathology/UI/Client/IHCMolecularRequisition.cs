using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Drawing;
using System.Drawing.Imaging;

namespace YellowstonePathology.UI.Client
{
	public class IHCMolecularRequisition
	{
        private System.Drawing.Printing.PrintDocument m_PrintDocument;
        private int m_ClientId;
		private YellowstonePathology.Business.View.ClientPhysicianView m_ClientPhysicianView;
        private int m_CopyCount;
        private int m_CopiesPrinted;
        private YellowstonePathology.Business.BarcodeScanning.BarcodeVersion1 m_Barcode;

        public IHCMolecularRequisition(int clientId)
		{
            this.m_PrintDocument = new System.Drawing.Printing.PrintDocument();
            this.m_ClientId = clientId;
			this.m_ClientPhysicianView = YellowstonePathology.Business.Gateway.PhysicianClientGateway.GetClientPhysicianViewByClientIdV2(this.m_ClientId);
			this.m_Barcode = new YellowstonePathology.Business.BarcodeScanning.BarcodeVersion1(YellowstonePathology.Business.BarcodeScanning.BarcodePrefixEnum.CLNT, this.m_ClientId.ToString());            
		}

        public void Print(int copyCount, System.Printing.PrintQueue printQueue)
		{
            this.m_CopyCount = copyCount;
            this.m_CopiesPrinted = 0;
            this.m_PrintDocument.PrinterSettings.PrinterName = printQueue.FullName;
            this.m_PrintDocument.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(PrintDocument_PrintPage);
            this.m_PrintDocument.Print();            
		}

        private void PrintDocument_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {            
            int x = 85;
            int y = 150;

			YellowstonePathology.Business.DataMatrix.DmtxImageEncoder encoder = new YellowstonePathology.Business.DataMatrix.DmtxImageEncoder();
			YellowstonePathology.Business.DataMatrix.DmtxImageEncoderOptions options = new YellowstonePathology.Business.DataMatrix.DmtxImageEncoderOptions();
            options.ModuleSize = 3;
            options.MarginSize = 3;
            options.BackColor = System.Drawing.Color.White;
            options.ForeColor = System.Drawing.Color.Black;

			YellowstonePathology.Business.BarcodeScanning.BarcodeVersion1 barcodeVersion1 = new YellowstonePathology.Business.BarcodeScanning.BarcodeVersion1(YellowstonePathology.Business.BarcodeScanning.BarcodePrefixEnum.CLNT, this.m_ClientId.ToString());
            e.Graphics.DrawImage(encoder.EncodeImage(barcodeVersion1.ToString(), options), new PointF(35, 150));            
            e.Graphics.DrawString(this.m_ClientPhysicianView.ClientName, new System.Drawing.Font("Tahoma", 10), System.Drawing.Brushes.Black, new System.Drawing.PointF(x, y));
            e.Graphics.DrawString(this.m_ClientPhysicianView.Address, new System.Drawing.Font("Tahoma", 10), System.Drawing.Brushes.Black, new System.Drawing.PointF(x, y + 15));

            StringBuilder cityStateZip = new StringBuilder();
            if (!string.IsNullOrEmpty(this.m_ClientPhysicianView.City))
            {
                cityStateZip.Append(this.m_ClientPhysicianView.City + ", ");
            }
            if (!string.IsNullOrEmpty(this.m_ClientPhysicianView.State))
            {
                cityStateZip.Append(this.m_ClientPhysicianView.State + " ");
            }
            if (string.IsNullOrEmpty(this.m_ClientPhysicianView.ZipCode) == false )
            {
                cityStateZip.Append(this.m_ClientPhysicianView.ZipCode.ToString());
            }
                
            e.Graphics.DrawString(cityStateZip.ToString(), new System.Drawing.Font("Tahoma", 10), System.Drawing.Brushes.Black, new System.Drawing.PointF(x, y + 30));
            string formattedTelephone = YellowstonePathology.Business.Helper.PhoneNumberHelper.FormatWithDashes(this.m_ClientPhysicianView.Telephone);
            e.Graphics.DrawString(formattedTelephone, new System.Drawing.Font("Tahoma", 10), System.Drawing.Brushes.Black, new System.Drawing.PointF(x, y + 45));

            x = 30;
            y = 480;
            
            YellowstonePathology.Business.Test.Model.DisplayGroupEpithelial displayGroupEpithelial = new YellowstonePathology.Business.Test.Model.DisplayGroupEpithelial();
            e.Graphics.DrawString(displayGroupEpithelial.GroupName, new System.Drawing.Font("Tahoma", 8), System.Drawing.Brushes.Black, new System.Drawing.PointF(x, y));
            y += 12;

            foreach (YellowstonePathology.Business.Test.Model.ImmunoHistochemistryTest immunoHistochemistryTest in displayGroupEpithelial.List)
            {
                e.Graphics.DrawString("[ ] " + immunoHistochemistryTest.TestName, new System.Drawing.Font("Tahoma", 8), System.Drawing.Brushes.Black, new System.Drawing.PointF(x, y));
                y += 12;
            }
            
            y += 20;

            YellowstonePathology.Business.Test.Model.DisplayGroupSiteSpecificTumorMarkers displayGroupSiteSpecificTumorMarkers = new YellowstonePathology.Business.Test.Model.DisplayGroupSiteSpecificTumorMarkers();
            e.Graphics.DrawString(displayGroupSiteSpecificTumorMarkers.GroupName, new System.Drawing.Font("Tahoma", 8), System.Drawing.Brushes.Black, new System.Drawing.PointF(x, y));
            y += 12;

            foreach (YellowstonePathology.Business.Test.Model.ImmunoHistochemistryTest immunoHistochemistryTest in displayGroupSiteSpecificTumorMarkers.List)
            {
                e.Graphics.DrawString("[ ] " + immunoHistochemistryTest.TestName, new System.Drawing.Font("Tahoma", 8), System.Drawing.Brushes.Black, new System.Drawing.PointF(x, y));
                y += 12;
            }
            
            x = 225;
            y = 480;

            YellowstonePathology.Business.Test.Model.DisplayGroupBreast displayGroupBreast = new YellowstonePathology.Business.Test.Model.DisplayGroupBreast();
            e.Graphics.DrawString(displayGroupBreast.GroupName, new System.Drawing.Font("Tahoma", 8), System.Drawing.Brushes.Black, new System.Drawing.PointF(x, y));
            y += 12;

            e.Graphics.DrawString("[ ] Invasive Breast Panel", new System.Drawing.Font("Tahoma", 8), System.Drawing.Brushes.Black, new System.Drawing.PointF(x, y));
            y += 12;

            e.Graphics.DrawString("[ ] DCIS Breast Panel", new System.Drawing.Font("Tahoma", 8), System.Drawing.Brushes.Black, new System.Drawing.PointF(x, y));
            y += 12;

            foreach (YellowstonePathology.Business.Test.Model.ImmunoHistochemistryTest immunoHistochemistryTest in displayGroupBreast.List)
            {
                e.Graphics.DrawString("[ ] " + immunoHistochemistryTest.TestName, new System.Drawing.Font("Tahoma", 8), System.Drawing.Brushes.Black, new System.Drawing.PointF(x, y));
                y += 12;
            }

            y += 20;

            YellowstonePathology.Business.Test.Model.DisplayGroupColon displayGroupColon = new YellowstonePathology.Business.Test.Model.DisplayGroupColon();
            e.Graphics.DrawString(displayGroupColon.GroupName, new System.Drawing.Font("Tahoma", 8), System.Drawing.Brushes.Black, new System.Drawing.PointF(x, y));
            y += 12;

            foreach (YellowstonePathology.Business.Test.Model.ImmunoHistochemistryTest immunoHistochemistryTest in displayGroupColon.List)
            {
                e.Graphics.DrawString("[ ] " + immunoHistochemistryTest.TestName, new System.Drawing.Font("Tahoma", 8), System.Drawing.Brushes.Black, new System.Drawing.PointF(x, y));
                y += 12;
            }

            y += 20;

            YellowstonePathology.Business.Test.Model.DisplayGroupInfectiousDiseases displayGroupInfectiousDiseases = new YellowstonePathology.Business.Test.Model.DisplayGroupInfectiousDiseases();
            e.Graphics.DrawString(displayGroupInfectiousDiseases.GroupName, new System.Drawing.Font("Tahoma", 8), System.Drawing.Brushes.Black, new System.Drawing.PointF(x, y));
            y += 12;

            foreach (YellowstonePathology.Business.Test.Model.ImmunoHistochemistryTest immunoHistochemistryTest in displayGroupInfectiousDiseases.List)
            {
                e.Graphics.DrawString("[ ] " + immunoHistochemistryTest.TestName, new System.Drawing.Font("Tahoma", 8), System.Drawing.Brushes.Black, new System.Drawing.PointF(x, y));
                y += 12;
            }

            y += 12;

            YellowstonePathology.Business.Test.Model.DisplayGroupProliferationCellCycleMarkers displayGroupProliferationCellCycleMarkers = new YellowstonePathology.Business.Test.Model.DisplayGroupProliferationCellCycleMarkers();
            e.Graphics.DrawString(displayGroupProliferationCellCycleMarkers.GroupName, new System.Drawing.Font("Tahoma", 8), System.Drawing.Brushes.Black, new System.Drawing.PointF(x, y));
            y += 12;

            foreach (YellowstonePathology.Business.Test.Model.ImmunoHistochemistryTest immunoHistochemistryTest in displayGroupProliferationCellCycleMarkers.List)
            {
                e.Graphics.DrawString("[ ] " + immunoHistochemistryTest.TestName, new System.Drawing.Font("Tahoma", 8), System.Drawing.Brushes.Black, new System.Drawing.PointF(x, y));
                y += 12;
            }

            y += 12;

            YellowstonePathology.Business.Test.Model.DisplayGroupMiscellaneous displayGroupMiscellaneous = new YellowstonePathology.Business.Test.Model.DisplayGroupMiscellaneous();
            e.Graphics.DrawString(displayGroupMiscellaneous.GroupName, new System.Drawing.Font("Tahoma", 8), System.Drawing.Brushes.Black, new System.Drawing.PointF(x, y));
            y += 12;

            foreach (YellowstonePathology.Business.Test.Model.ImmunoHistochemistryTest immunoHistochemistryTest in displayGroupMiscellaneous.List)
            {
                e.Graphics.DrawString("[ ] " + immunoHistochemistryTest.TestName, new System.Drawing.Font("Tahoma", 8), System.Drawing.Brushes.Black, new System.Drawing.PointF(x, y));
                y += 12;
            }

            x = 385;
            y = 480;

            YellowstonePathology.Business.Test.Model.DisplayGroupHematopoietic displayGroupHematopoietic = new YellowstonePathology.Business.Test.Model.DisplayGroupHematopoietic();
            e.Graphics.DrawString(displayGroupHematopoietic.GroupName, new System.Drawing.Font("Tahoma", 8), System.Drawing.Brushes.Black, new System.Drawing.PointF(x, y));
            y += 12;

            foreach (YellowstonePathology.Business.Test.Model.ImmunoHistochemistryTest immunoHistochemistryTest in displayGroupHematopoietic.List)
            {
                e.Graphics.DrawString("[ ] " + immunoHistochemistryTest.TestName, new System.Drawing.Font("Tahoma", 8), System.Drawing.Brushes.Black, new System.Drawing.PointF(x, y));
                y += 12;
            }             

            x = 505;
            y = 480;

            YellowstonePathology.Business.Test.Model.DisplayGroupMelanoma displayGroupMelanoma = new YellowstonePathology.Business.Test.Model.DisplayGroupMelanoma();
            e.Graphics.DrawString(displayGroupMelanoma.GroupName, new System.Drawing.Font("Tahoma", 8), System.Drawing.Brushes.Black, new System.Drawing.PointF(x, y));
            y += 12;

            foreach (YellowstonePathology.Business.Test.Model.ImmunoHistochemistryTest immunoHistochemistryTest in displayGroupMelanoma.List)
            {
                e.Graphics.DrawString("[ ] " + immunoHistochemistryTest.TestName, new System.Drawing.Font("Tahoma", 8), System.Drawing.Brushes.Black, new System.Drawing.PointF(x, y));
                y += 12;
            }

            y += 12;

            YellowstonePathology.Business.Test.Model.DisplayGroupNeural displayGroupNeural = new YellowstonePathology.Business.Test.Model.DisplayGroupNeural();
            e.Graphics.DrawString(displayGroupNeural.GroupName, new System.Drawing.Font("Tahoma", 8), System.Drawing.Brushes.Black, new System.Drawing.PointF(x, y));
            y += 12;

            foreach (YellowstonePathology.Business.Test.Model.ImmunoHistochemistryTest immunoHistochemistryTest in displayGroupNeural.List)
            {
                e.Graphics.DrawString("[ ] " + immunoHistochemistryTest.TestName, new System.Drawing.Font("Tahoma", 8), System.Drawing.Brushes.Black, new System.Drawing.PointF(x, y));
                y += 12;
            }

            y += 12;

            YellowstonePathology.Business.Test.Model.DisplaySoftTissueMesenchymal displaySoftTissueMesenchymal = new YellowstonePathology.Business.Test.Model.DisplaySoftTissueMesenchymal();
            e.Graphics.DrawString(displaySoftTissueMesenchymal.GroupName, new System.Drawing.Font("Tahoma", 8), System.Drawing.Brushes.Black, new System.Drawing.PointF(x, y));
            y += 12;

            foreach (YellowstonePathology.Business.Test.Model.ImmunoHistochemistryTest immunoHistochemistryTest in displaySoftTissueMesenchymal.List)
            {
                e.Graphics.DrawString("[ ] " + immunoHistochemistryTest.TestName, new System.Drawing.Font("Tahoma", 8), System.Drawing.Brushes.Black, new System.Drawing.PointF(x, y));
                y += 12;
            }
            
            y += 12;

            YellowstonePathology.Business.Test.Model.DisplayGroupProstate displayGroupProstate = new YellowstonePathology.Business.Test.Model.DisplayGroupProstate();
            e.Graphics.DrawString(displayGroupProstate.GroupName, new System.Drawing.Font("Tahoma", 8), System.Drawing.Brushes.Black, new System.Drawing.PointF(x, y));
            y += 12;

            foreach (YellowstonePathology.Business.Test.Model.ImmunoHistochemistryTest immunoHistochemistryTest in displayGroupProstate.List)
            {
                e.Graphics.DrawString("[ ] " + immunoHistochemistryTest.TestName, new System.Drawing.Font("Tahoma", 8), System.Drawing.Brushes.Black, new System.Drawing.PointF(x, y));
                y += 12;
            }           

            x = 655;
            y = 480;

            YellowstonePathology.Business.Test.Model.DisplayGroupCytochemical cisplayGroupCytochemical = new YellowstonePathology.Business.Test.Model.DisplayGroupCytochemical();
            e.Graphics.DrawString(cisplayGroupCytochemical.GroupName, new System.Drawing.Font("Tahoma", 8), System.Drawing.Brushes.Black, new System.Drawing.PointF(x, y));
            y += 12;

            foreach (YellowstonePathology.Business.Test.Model.Test test in cisplayGroupCytochemical.List)
            {
                e.Graphics.DrawString("[ ] " + test.TestName, new System.Drawing.Font("Tahoma", 8), System.Drawing.Brushes.Black, new System.Drawing.PointF(x, y));
                y += 12;
            }

            y += 12;

            YellowstonePathology.Business.Test.Model.DisplayGroupLiverPanel displayGroupLiverPanel = new YellowstonePathology.Business.Test.Model.DisplayGroupLiverPanel();
            e.Graphics.DrawString(displayGroupLiverPanel.GroupName, new System.Drawing.Font("Tahoma", 8), System.Drawing.Brushes.Black, new System.Drawing.PointF(x, y));
            y += 12;

            foreach (YellowstonePathology.Business.Test.Model.Test test in displayGroupLiverPanel.List)
            {
                e.Graphics.DrawString("[ ] " + test.TestName, new System.Drawing.Font("Tahoma", 8), System.Drawing.Brushes.Black, new System.Drawing.PointF(x, y));
                y += 12;
            }


            this.m_CopiesPrinted += 1;
            if (this.m_CopiesPrinted < this.m_CopyCount)
            {
                e.HasMorePages = true;
            }
            else
            {
                e.HasMorePages = false;
            }                                                  
        }				        
	}
}
