using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace YellowstonePathology.Business.Label.Model
{
    public class PantherLabel : Label
    {        
        private string m_AliquotOrderId;
        private string m_PatientDisplayName;        
        private string m_SpecimenDescription;
        private DateTime m_Birthdate;

        public PantherLabel(string aliquotOrderId, string patientDisplayName, DateTime birthdate, string specimenDescription)
        {            
            this.m_AliquotOrderId = aliquotOrderId;            
            this.m_PatientDisplayName = patientDisplayName;            
            this.m_SpecimenDescription = specimenDescription;
            this.m_Birthdate = birthdate;
        }

        public override void DrawLabel(int x, int y, System.Drawing.Printing.PrintPageEventArgs e)
        {            
            /*
            BarcodeLib.Barcode barcode = new BarcodeLib.Barcode();
            Image barcodeImage = barcode.Encode(BarcodeLib.TYPE.CODE128, this.m_AliquotOrderId, Color.Black, Color.White, 190, 48);
            e.Graphics.DrawImage(barcodeImage, new Point(0, 8));

            string largeRectangleString = this.TruncateString(this.m_AliquotOrderId, 150) + ": " + this.m_Birthdate.ToShortDateString() + Environment.NewLine +
                this.TruncateString(this.m_PatientDisplayName, 150) + Environment.NewLine +
                this.m_SpecimenDescription;         

            StringFormat stringFormatLargeRectangle = new StringFormat();
            stringFormatLargeRectangle.Alignment = StringAlignment.Near;
            stringFormatLargeRectangle.LineAlignment = StringAlignment.Near;

            Rectangle rectangle = new Rectangle(10, 65, 200, 75);                        

            using (Font font = new Font("Verdana", 6, FontStyle.Regular, GraphicsUnit.Point))
            {
                e.Graphics.DrawString(largeRectangleString, font, Brushes.Black, rectangle, stringFormatLargeRectangle);                
            }            

            */
        }        
    }
}
