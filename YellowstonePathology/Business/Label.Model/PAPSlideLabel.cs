using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace YellowstonePathology.Business.Label.Model
{
    public class PAPSlideLabel : Label
    {                
        private string m_PatientFirstName;
        private string m_PatientLastName;
		private YellowstonePathology.Business.BarcodeScanning.BarcodeVersion2 m_Barcode;
		private YellowstonePathology.Business.BarcodeScanning.CytycBarcode m_CytycBarcode;

		public PAPSlideLabel(string patientFirstName, string patientLastName, YellowstonePathology.Business.BarcodeScanning.BarcodeVersion2 barcode,
			YellowstonePathology.Business.BarcodeScanning.CytycBarcode cytycBarcode)
        {                        
            this.m_PatientFirstName = patientFirstName;
            this.m_PatientLastName = patientLastName;
            this.m_Barcode = barcode;
            this.m_CytycBarcode = cytycBarcode;            
        }

        public override void DrawLabel(int x, int y, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Rectangle hologicRectangle = new Rectangle(x + 10, y + 12, 80, 32);
            string hologicString = this.m_CytycBarcode.LineOne + Environment.NewLine + this.m_CytycBarcode.LineTwo;

            StringFormat hologicStringFormat = new StringFormat();
            hologicStringFormat.Alignment = StringAlignment.Near;
            hologicStringFormat.LineAlignment = StringAlignment.Center;

            using (Font hologicFont = new Font("OCRAMCE", 10, FontStyle.Regular, GraphicsUnit.Point))
            {
                e.Graphics.DrawString(hologicString, hologicFont, Brushes.Black, hologicRectangle, hologicStringFormat);
            }

            string patientNameText = this.TruncateString(this.m_PatientLastName, 8)  +  ", " + this.TruncateString(this.m_PatientFirstName, 1);

            StringFormat patientNameStringtFormat = new StringFormat();
            patientNameStringtFormat.Alignment = StringAlignment.Near;
            patientNameStringtFormat.LineAlignment = StringAlignment.Center;

            Rectangle patientNameRectangle = new Rectangle(x + 10, y + 38, 80, 30);

            using (Font patientNamefont = new Font("Verdana", 7, FontStyle.Regular, GraphicsUnit.Point))
            {
                e.Graphics.DrawString(patientNameText, patientNamefont, Brushes.Black, patientNameRectangle, patientNameStringtFormat);
            }

            string locationText = "YPI Blgs";

            StringFormat locationStringtFormat = new StringFormat();
            locationStringtFormat.Alignment = StringAlignment.Near;
            locationStringtFormat.LineAlignment = StringAlignment.Center;

            Rectangle locationRectangle = new Rectangle(x + 10, y + 72, 80, 15);

            using (Font locationfont = new Font("Verdana", 5, FontStyle.Regular, GraphicsUnit.Point))
            {
                e.Graphics.DrawString(locationText, locationfont, Brushes.Black, locationRectangle, locationStringtFormat);
            }

            DataMatrix.DmtxImageEncoder encoder = new DataMatrix.DmtxImageEncoder();
            DataMatrix.DmtxImageEncoderOptions options = new DataMatrix.DmtxImageEncoderOptions();
            options.ModuleSize = 1;
            options.MarginSize = 2;
            options.BackColor = System.Drawing.Color.White;
            options.ForeColor = System.Drawing.Color.Black;
            Bitmap bitmap = encoder.EncodeImage(this.m_Barcode.ToString(), options);
            e.Graphics.DrawImage(bitmap, new PointF(x + 62, y + 62));
        }        
    }
}
