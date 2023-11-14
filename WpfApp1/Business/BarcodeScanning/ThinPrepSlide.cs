using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.BarcodeScanning
{
    public class ThinPrepSlide
    {
        public string m_AliquotOrderId;
        private BarcodeVersion2 m_Barcode;        
        private string m_PatientLastName;
        private string m_PatientFirstName;        

        public ThinPrepSlide()
        {
            
        }

        public ThinPrepSlide(string aliquotOrderId, string patientLastName, string patientFirstName, string testName)
        {
            this.m_AliquotOrderId = aliquotOrderId;
            this.m_PatientLastName = patientLastName;
            this.m_PatientFirstName = patientFirstName;
            this.m_Barcode = new BarcodeVersion2(BarcodePrefixEnum.PSLD, this.m_AliquotOrderId);            
        }

        public BarcodeVersion2 Barcode
        {
            get { return this.m_Barcode; }
            set { this.m_Barcode = value; }
        }

        public string AliquotOrderId
        {
            get { return this.m_AliquotOrderId; }
            set { this.m_AliquotOrderId = value; }
        }

        public string PatientLastName
        {
            get { return this.m_PatientLastName; }
            set { this.m_PatientLastName = value; }
        }

        public string PatientFirstName
        {
            get { return this.m_PatientFirstName; }
            set { this.m_PatientFirstName = value; }
        }                       
    }
}
