using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace YellowstonePathology.Business.Label.Model
{
    public class HistologySlideLabel : Label
    {
        private string m_SlideOrderId;
        private string m_ReportNo;
        private string m_SlideNumber;
        private string m_PatientLastName;
        private string m_TestAbbreviation;
        private string m_FacilityLocationAbbreviation;
        private string m_ClientAccessionNo;        
		private YellowstonePathology.Business.BarcodeScanning.BarcodeVersion2 m_Barcode;

        public HistologySlideLabel(string slideOrderId, string reportNo, string slideNumber, string patientLastName, string testAbbreviation, string facilityLocationAbbreviation, Business.Test.AccessionOrder accessionOrder)
        {
            this.m_SlideOrderId = slideOrderId;
            this.m_ReportNo = reportNo;
            this.m_SlideNumber = slideNumber;
            this.m_PatientLastName = patientLastName;
            this.m_TestAbbreviation = testAbbreviation;
            this.m_FacilityLocationAbbreviation = facilityLocationAbbreviation;
            this.m_ClientAccessionNo = accessionOrder.ClientAccessionNo;
			this.m_Barcode = new YellowstonePathology.Business.BarcodeScanning.BarcodeVersion2(Business.BarcodeScanning.BarcodePrefixEnum.HSLD, this.m_SlideOrderId);            
        }

        public override void DrawLabel(int x, int y, System.Drawing.Printing.PrintPageEventArgs e)
        {            
            e.Graphics.DrawString(this.m_ReportNo, new System.Drawing.Font("Verdana", 9), System.Drawing.Brushes.Black, new System.Drawing.PointF(x + 3, y + 0));            
            e.Graphics.DrawString(this.m_SlideNumber, new System.Drawing.Font("Verdana", 8, System.Drawing.FontStyle.Bold), System.Drawing.Brushes.Black, new System.Drawing.PointF(x + 26, y + 17));
            e.Graphics.DrawString(this.m_FacilityLocationAbbreviation, new System.Drawing.Font("Verdana", 4), System.Drawing.Brushes.Black, new System.Drawing.PointF(x + 29, y + 31));
            e.Graphics.DrawString(this.m_ClientAccessionNo, new System.Drawing.Font("Verdana", 6), System.Drawing.Brushes.Black, new System.Drawing.PointF(x + 3, y + 39));
            e.Graphics.DrawString(this.m_PatientLastName, new System.Drawing.Font("Verdana", 6), System.Drawing.Brushes.Black, new System.Drawing.PointF(x + 3, y + 47));
            
            DataMatrix.DmtxImageEncoder encoder = new DataMatrix.DmtxImageEncoder();
            DataMatrix.DmtxImageEncoderOptions options = new DataMatrix.DmtxImageEncoderOptions();
            options.ModuleSize = 1;
            options.MarginSize = 2;
            options.BackColor = System.Drawing.Color.White;
            options.ForeColor = System.Drawing.Color.Black;
            Bitmap bitmap = encoder.EncodeImage(this.m_Barcode.ToString(), options);
            e.Graphics.DrawImage(bitmap, new PointF(x + 3 , y + 17));
        }        
    }
}
