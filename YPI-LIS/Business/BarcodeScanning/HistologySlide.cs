using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.BarcodeScanning
{
    public class HistologySlide
    {
        public string m_SlideOrderId;
        private BarcodeVersion2 m_Barcode;

        private string m_ReportNo;
        private string m_SlideNumber;
        private string m_LastName;
        private string m_TestName;
        private string m_FacilityLocationAbbreviation;

        public HistologySlide(string slideOrderId, string reportNo, string slideNumber, string lastName, string testName, string facilityLocationAbbreviation)
        {            
            this.m_SlideOrderId = slideOrderId;
            this.m_ReportNo = reportNo;
            this.m_SlideNumber = slideNumber;
            this.m_LastName = lastName;
            this.m_TestName = testName;
            this.m_FacilityLocationAbbreviation = facilityLocationAbbreviation;
            this.m_Barcode = new BarcodeVersion2(BarcodePrefixEnum.HSLD, this.m_SlideOrderId);            
        }                    

        public BarcodeVersion2 Barcode
        {
            get { return this.m_Barcode; }
            set { this.m_Barcode = value; }
        }

        public string SlideOrderId
        {
            get { return this.m_SlideOrderId; }
            set { this.m_SlideOrderId = value;}
        }

        public string ReportNo
        {
            get { return this.m_ReportNo; }
            set { this.m_ReportNo = value; }
        }

        public string SlideNumber
        {
            get { return this.m_SlideNumber; }
            set { this.m_SlideNumber = value; }
        }

        public string LastName
        {
            get { return this.m_LastName; }
            set { this.m_LastName = value; }
        }

        public string TestName
        {
            get { return this.m_TestName; }
            set { this.m_TestName = value; }
        }

        public string FacilityLocationAbbreviation
        {
            get { return this.m_FacilityLocationAbbreviation; }
            set { this.m_FacilityLocationAbbreviation = value; }
        }
         
    }
}
