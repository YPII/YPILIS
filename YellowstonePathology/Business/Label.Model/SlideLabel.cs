using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Label.Model
{
    public class SlideLabel
    {
        protected YellowstonePathology.Business.Test.AliquotOrder m_AliquotOrder;        
        protected YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;

        protected string m_MasterAccessionNo;
        protected string m_TruncatedFirstName;
        protected string m_TruncatedLastName;
        protected string m_SlideId;
        protected string m_LocationDescription;

        public SlideLabel()
        {
            
        }

        public SlideLabel(YellowstonePathology.Business.Test.AliquotOrder aliquotOrder, YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {
            this.m_AliquotOrder = aliquotOrder;            
            this.m_AccessionOrder = accessionOrder;

            this.m_MasterAccessionNo = accessionOrder.MasterAccessionNo;
            YellowstonePathology.Business.Patient.Model.Patient patient = new YellowstonePathology.Business.Patient.Model.Patient(accessionOrder.PFirstName, accessionOrder.PLastName);
            this.m_TruncatedFirstName = patient.GetTruncatedFirstName(12);
            this.m_TruncatedLastName = patient.GetTruncatedLastName(12);
            this.m_SlideId = aliquotOrder.Label;

            YellowstonePathology.Business.Facility.Model.FacilityCollection facilityCollection = YellowstonePathology.Business.Facility.Model.FacilityCollection.GetAllFacilities();
            YellowstonePathology.Business.Facility.Model.Facility facility = facilityCollection.GetByFacilityId(accessionOrder.AccessioningFacilityId);

            this.m_LocationDescription = facility.LocationAbbreviation;
        }

        public YellowstonePathology.Business.Test.AliquotOrder AliquotOrder
        {
            get { return this.m_AliquotOrder; }
        }        

        public void DrawLabel(int x, int y, System.Drawing.Printing.PrintPageEventArgs e)
        {
            DataMatrix.DmtxImageEncoder encoder = new DataMatrix.DmtxImageEncoder();
            DataMatrix.DmtxImageEncoderOptions options = new DataMatrix.DmtxImageEncoderOptions();
            options.ModuleSize = 1;
            options.MarginSize = 3;
            options.BackColor = System.Drawing.Color.White;
            options.ForeColor = System.Drawing.Color.Black;

			string barcodeId = YellowstonePathology.Business.BarcodeScanning.BarcodePrefixEnum.HBLK + this.m_AliquotOrder.AliquotOrderId;
            System.Drawing.Bitmap barcodeBitmap = encoder.EncodeImage(barcodeId, options);

            int xOffset = 5;

            e.Graphics.DrawString(this.m_AccessionOrder.MasterAccessionNo, new System.Drawing.Font("Verdana", 9), System.Drawing.Brushes.Black, new System.Drawing.PointF(x + xOffset, y));
            e.Graphics.DrawImage(barcodeBitmap, new System.Drawing.Point(x + xOffset, y + 15));
            
            e.Graphics.DrawString(this.m_SlideId, new System.Drawing.Font("Verdana", 6), System.Drawing.Brushes.Black, new System.Drawing.PointF(x + xOffset + 27, y + 18));                        

            e.Graphics.DrawString(this.m_TruncatedFirstName, new System.Drawing.Font("Verdana", 6), System.Drawing.Brushes.Black, new System.Drawing.PointF(x + xOffset, y + 40));
            e.Graphics.DrawString(this.m_TruncatedLastName, new System.Drawing.Font("Verdana", 6), System.Drawing.Brushes.Black, new System.Drawing.PointF(x + xOffset, y + 50));

            e.Graphics.DrawString(this.m_LocationDescription, new System.Drawing.Font("Verdana", 6), System.Drawing.Brushes.Black, new System.Drawing.PointF(x + xOffset, y + 70));            
        }
    }
}
