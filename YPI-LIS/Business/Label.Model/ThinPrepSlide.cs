using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace YellowstonePathology.Business.Label.Model
{
    public class ThinPrepSlide : Label
    {                
        private string m_PatientFirstName;
        private string m_PatientLastName;
		private YellowstonePathology.Business.BarcodeScanning.BarcodeVersion2 m_Barcode;
		private YellowstonePathology.Business.BarcodeScanning.CytycBarcode m_CytycBarcode;

        public ThinPrepSlide(string patientFirstName, string patientLastName, YellowstonePathology.Business.BarcodeScanning.BarcodeVersion2 barcode,
			YellowstonePathology.Business.BarcodeScanning.CytycBarcode cytycBarcode)
        {                        
            this.m_PatientFirstName = patientFirstName;
            this.m_PatientLastName = patientLastName;
            this.m_Barcode = barcode;
            this.m_CytycBarcode = cytycBarcode;            
        }

        public override void DrawLabel(int x, int y, System.Drawing.Printing.PrintPageEventArgs e)
        {           
            int xMargin = 13;
            int yMargin = 13;            

            using (Font hologicFont = new Font("OCRAMCE", 10, FontStyle.Regular, GraphicsUnit.Point))
            {
                e.Graphics.DrawString(this.m_CytycBarcode.LineOne, hologicFont, Brushes.Black, new Point(x + xMargin + 0, y + yMargin + 0));
                e.Graphics.DrawString(this.m_CytycBarcode.LineTwo, hologicFont, Brushes.Black, new Point(x + xMargin + 0, y + yMargin + 17));
            }
            
            string patientNameText = this.TruncateString(this.m_PatientLastName, 8)  +  ", " + this.TruncateString(this.m_PatientFirstName, 1);            
            using (Font patientNamefont = new Font("Verdana", 7, FontStyle.Regular, GraphicsUnit.Point))
            {
                e.Graphics.DrawString(patientNameText, patientNamefont, Brushes.Black, new Point(x + xMargin + 0, y + yMargin + 37));
            }

            
            string locationText = "YPI Blgs";            
            using (Font locationfont = new Font("Verdana", 5, FontStyle.Regular, GraphicsUnit.Point))
            {
                e.Graphics.DrawString(locationText, locationfont, Brushes.Black, new Point(x + xMargin + 0, y + yMargin + 68));
            }

            DataMatrix.DmtxImageEncoder encoder = new DataMatrix.DmtxImageEncoder();
            DataMatrix.DmtxImageEncoderOptions options = new DataMatrix.DmtxImageEncoderOptions();
            options.ModuleSize = 1;
            options.MarginSize = 2;
            options.BackColor = System.Drawing.Color.White;
            options.ForeColor = System.Drawing.Color.Black;
            Bitmap bitmap = encoder.EncodeImage(this.m_Barcode.ToString(), options);
            e.Graphics.DrawImage(bitmap, new PointF(x + xMargin + 55, y + yMargin + 55));            
        }        
    }
}
