﻿using System;
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
	public class OncologyRequisition
	{
        private System.Drawing.Printing.PrintDocument m_PrintDocument;
        private int m_ClientId;
		private YellowstonePathology.Business.View.ClientPhysicianView m_ClientPhysicianView;
        private int m_CopyCount;
        private int m_CopiesPrinted;
		private YellowstonePathology.Business.BarcodeScanning.BarcodeVersion1 m_ClientBarcode;

        public OncologyRequisition(int clientId)
		{
            this.m_PrintDocument = new System.Drawing.Printing.PrintDocument();
            this.m_ClientId = clientId;
			this.m_ClientPhysicianView = Business.Gateway.PhysicianClientGateway.GetClientPhysicianViewByClientIdV2(this.m_ClientId);
			this.m_ClientBarcode = new Business.BarcodeScanning.BarcodeVersion1(Business.BarcodeScanning.BarcodePrefixEnum.CLNT, this.m_ClientId.ToString());            
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
            int x = 45;
            int y = 150;

			YellowstonePathology.Business.DataMatrix.DmtxImageEncoder encoder = new YellowstonePathology.Business.DataMatrix.DmtxImageEncoder();
			YellowstonePathology.Business.DataMatrix.DmtxImageEncoderOptions options = new YellowstonePathology.Business.DataMatrix.DmtxImageEncoderOptions();
            options.ModuleSize = 3;
            options.MarginSize = 3;
            options.BackColor = System.Drawing.Color.White;
            options.ForeColor = System.Drawing.Color.Black;

			YellowstonePathology.Business.BarcodeScanning.BarcodeVersion1 barcodeVersion1 = new Business.BarcodeScanning.BarcodeVersion1(Business.BarcodeScanning.BarcodePrefixEnum.CLNT, this.m_ClientId.ToString());
            e.Graphics.DrawImage(encoder.EncodeImage(barcodeVersion1.ToString(), options), new PointF(x, y));            
            e.Graphics.DrawString(this.m_ClientPhysicianView.ClientName, new System.Drawing.Font("Tahoma", 10), System.Drawing.Brushes.Black, new System.Drawing.PointF(x + 50, y));
            e.Graphics.DrawString(this.m_ClientPhysicianView.Address, new System.Drawing.Font("Tahoma", 10), System.Drawing.Brushes.Black, new System.Drawing.PointF(x + 50, y + 15));

            StringBuilder cityStateZip = new StringBuilder();
            if (!string.IsNullOrEmpty(this.m_ClientPhysicianView.City))
            {
                cityStateZip.Append(this.m_ClientPhysicianView.City + ", ");
            }
            if (!string.IsNullOrEmpty(this.m_ClientPhysicianView.State))
            {
                cityStateZip.Append(this.m_ClientPhysicianView.State + " ");
            }
            if (string.IsNullOrEmpty(this.m_ClientPhysicianView.ZipCode) == false)
            {
                cityStateZip.Append(this.m_ClientPhysicianView.ZipCode);
            }
                
            e.Graphics.DrawString(cityStateZip.ToString(), new System.Drawing.Font("Tahoma", 10), System.Drawing.Brushes.Black, new System.Drawing.PointF(x + 50, y + 30));
            string formattedTelephone = Business.Helper.PhoneNumberHelper.FormatWithDashes(this.m_ClientPhysicianView.Telephone);
            e.Graphics.DrawString(formattedTelephone, new System.Drawing.Font("Tahoma", 10), System.Drawing.Brushes.Black, new System.Drawing.PointF(x + 50, y + 45));                
            
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
