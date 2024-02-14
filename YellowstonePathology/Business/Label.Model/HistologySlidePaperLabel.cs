using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace YellowstonePathology.Business.Label.Model
{
    public class HistologySlidePaperLabel : Label
    {
        private string m_SlideOrderId;
        private string m_ReportNo;
        private string m_SlideNumber;
        private string m_FirstName;
        private string m_LastName;
        private string m_TestAbbreviation;
        private string m_FacilityLocationAbbreviation;
        private YellowstonePathology.Business.BarcodeScanning.BarcodeVersion2 m_Barcode;

        public HistologySlidePaperLabel(string slideOrderId, string reportNo, string slideNumber, string firstName, string lastName, string testAbbreviation, string facilityLocationAbbreviation)
        {
            this.m_SlideOrderId = slideOrderId;
            this.m_ReportNo = reportNo;
            this.m_SlideNumber = slideNumber;
            this.m_LastName = lastName;
            this.m_FirstName = firstName;
            this.m_TestAbbreviation = testAbbreviation;
            this.m_FacilityLocationAbbreviation = facilityLocationAbbreviation;
            this.m_Barcode = new YellowstonePathology.Business.BarcodeScanning.BarcodeVersion2(Business.BarcodeScanning.BarcodePrefixEnum.HSLD, this.m_SlideOrderId);
        }

        public string GetZPL()
        {
            Business.Label.Model.HistologySlidePaperZPLLabelV3 label = new Business.Label.Model.HistologySlidePaperZPLLabelV3(this.m_SlideOrderId, this.m_ReportNo,
                this.m_FirstName, this.m_LastName, this.m_TestAbbreviation, this.m_SlideNumber, "YPI Blgs, Mt", false, false);
            string zpl = label.GetCommandWithOffset(0);
            string result = $"^XA{zpl}^XZ";
            return result;
        }

        public override void DrawLabel(int x, int y, System.Drawing.Printing.PrintPageEventArgs e)
        {
            int xOffset = 9;
            e.Graphics.DrawString(this.m_ReportNo, new System.Drawing.Font("Verdana", 9), System.Drawing.Brushes.Black, new System.Drawing.PointF(x + xOffset, y + 6));
            e.Graphics.DrawString(this.m_SlideNumber, new System.Drawing.Font("Verdana", 8, System.Drawing.FontStyle.Bold), System.Drawing.Brushes.Black, new System.Drawing.PointF(x + xOffset + 30, y + 34));
            e.Graphics.DrawString(this.m_LastName, new System.Drawing.Font("Verdana", 6), System.Drawing.Brushes.Black, new System.Drawing.PointF(x + xOffset, y + 56));
            e.Graphics.DrawString(this.m_TestAbbreviation, new System.Drawing.Font("Verdana", 6), System.Drawing.Brushes.Black, new System.Drawing.PointF(x + xOffset, y + 66));
            e.Graphics.DrawString(this.m_FacilityLocationAbbreviation, new System.Drawing.Font("Verdana", 4), System.Drawing.Brushes.Black, new System.Drawing.PointF(x + xOffset + 24, y + 83));

            DataMatrix.DmtxImageEncoder encoder = new DataMatrix.DmtxImageEncoder();
            DataMatrix.DmtxImageEncoderOptions options = new DataMatrix.DmtxImageEncoderOptions();
            options.ModuleSize = 1;
            options.MarginSize = 2;
            options.BackColor = System.Drawing.Color.White;
            options.ForeColor = System.Drawing.Color.Black;
            Bitmap bitmap = encoder.EncodeImage(this.m_Barcode.ToString(), options);
            e.Graphics.DrawImage(bitmap, new PointF(x + xOffset, y + 26));
        }
    }
}