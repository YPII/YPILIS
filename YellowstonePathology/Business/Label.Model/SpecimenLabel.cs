using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Label.Model
{
    public class SpecimenLabel
    {        
        protected string m_AliquotOrderId;
        protected string m_MasterAccessionNo;             
        protected string m_PatientInitials;
        protected string m_AliquotLabel;                        
                
        public SpecimenLabel()
        {

        }        

        public void FromAliquotOrder(string aliquotOrderId, string aliquotLabel, string masterAccessionNo, string pLastName, string pFirstName)
        {
            this.m_AliquotOrderId = aliquotOrderId;
            this.m_MasterAccessionNo = masterAccessionNo;
            YellowstonePathology.Business.PatientName patientName = new PatientName(pLastName, pFirstName);
            this.m_PatientInitials = patientName.GetInitials();
            this.m_AliquotLabel = aliquotLabel;
        }

        public virtual void DrawLabel(int x, int y, System.Drawing.Printing.PrintPageEventArgs e)
        {
            DataMatrix.DmtxImageEncoder encoder = new DataMatrix.DmtxImageEncoder();
            DataMatrix.DmtxImageEncoderOptions options = new DataMatrix.DmtxImageEncoderOptions();
            options.ModuleSize = 2;
            options.MarginSize = 3;
            options.BackColor = System.Drawing.Color.White;
            options.ForeColor = System.Drawing.Color.Black;

            string barcodeId = YellowstonePathology.Business.BarcodeScanning.BarcodePrefixEnum.HBLK + this.m_AliquotOrderId;
            System.Drawing.Bitmap barcodeBitmap = encoder.EncodeImage(barcodeId, options);

            e.Graphics.DrawString(this.m_MasterAccessionNo, new System.Drawing.Font("Verdana", 9), System.Drawing.Brushes.Black, new System.Drawing.PointF(x + 2, y));                        
            e.Graphics.DrawImage(barcodeBitmap, new System.Drawing.Point(x + 2, y + 18));
            
            e.Graphics.DrawString(this.m_PatientInitials, new System.Drawing.Font("Verdana", 6), System.Drawing.Brushes.Black, new System.Drawing.PointF(x + 2, y + 60));
            //e.Graphics.DrawString(this.m_AliquotLabel, new System.Drawing.Font("Verdana", 6), System.Drawing.Brushes.Black, new System.Drawing.PointF(x + 50, y + 60));            
        }
    }
}
