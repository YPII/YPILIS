using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Label.Model
{
	public class Cassette 
	{
        //C:\GDHC\cassette\formats\Normal.itl#102#$#H2#15-23277#FS1A#JP#YPII#ALQ15-23277.1A#15#23277

        private const string TemplateFileName = @"C:\Program Files\General Data Company\Cassette Printing\Normal.itl";
        private const string SomeNumber = "102";

        private YellowstonePathology.Business.Test.AliquotOrder m_AliquotOrder;
		protected string m_Prefix = "$";
        protected string m_LaserDelimeter = "|";
        protected string m_Delimeter = "#";
		protected string m_CassetteColumnDelimiter = "H";
		protected string m_CompanyId = "YPII";
		protected string m_ScanningPrefix = "ALQ";
		protected string m_CassetteColumn;
		protected string m_BlockTitle;
		protected string m_PatientInitials;		
		protected bool m_PrintRequested;
		protected bool m_Verified;
		protected string m_ReportNo;
		protected string m_MasterAccessionNo;
        protected string m_ClientAccessionNo;
        protected bool m_ClientAccessioned;
        protected string m_EmbeddingInstructions;

        public Cassette()
		{
            
		}

        public YellowstonePathology.Business.Test.AliquotOrder AliquotOrder
        {
            get { return this.m_AliquotOrder; }
        }

		public string ReportNo
		{
			get
			{
				return this.m_ReportNo;
			}
			set
			{
				if (value != this.m_ReportNo)
				{
					this.m_ReportNo = value;					
				}
			}
		}

		public string MasterAccessionNo
		{
			get
			{
				return this.m_MasterAccessionNo;
			}
			set
			{
				if (value != this.m_MasterAccessionNo)
				{
					this.m_MasterAccessionNo = value;					
				}
			}
		}

        public string BlockTitle
        {
			get { return this.m_BlockTitle; }
			set
			{
				if (value != this.m_BlockTitle)
				{
					this.m_BlockTitle = value;					
				}
			}
        }
		
        public string PatientInitials
        {
			get { return this.m_PatientInitials; }
			set
			{
				if (value != this.m_PatientInitials)
				{
					this.m_PatientInitials = value;					
				}
			}
        }

        public string CassetteColumn
        {
            get { return this.m_CassetteColumn; }
			set
			{
				if (value != this.m_CassetteColumn)
				{
					this.m_CassetteColumn = value;					
				}
			}
        }

		public bool PrintRequested
		{
			get { return this.m_PrintRequested; }
			set
			{
				if (value != this.m_PrintRequested)
				{
					this.m_PrintRequested = value;					
				}
			}
		}

		public bool Verified
		{
			get { return this.m_Verified; }
			set
			{
				if (value != this.m_Verified)
				{
					this.m_Verified = value;					
				}
			}
		}

		public string FormattedCassetteColumn
		{
			get { return this.m_CassetteColumnDelimiter + this.m_CassetteColumn.ToString(); }
		}

        public string CompanyId
        {
            get { return this.m_CompanyId; }
        }

        public string ClientAccessionNo
        {
            get { return this.m_ClientAccessionNo; }
        }

        public string ScanningId
        {
            get { return this.m_ScanningPrefix + this.m_AliquotOrder.AliquotOrderId; }            
        }

        public void FromAliquotOrder(YellowstonePathology.Business.Test.AliquotOrder aliquotOrder, YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {
            this.m_AliquotOrder = aliquotOrder;
			YellowstonePathology.Business.OrderIdParser orderIdParser = new YellowstonePathology.Business.OrderIdParser(accessionOrder.MasterAccessionNo);

            YellowstonePathology.Business.PatientName patientName = new PatientName(accessionOrder.PLastName, accessionOrder.PFirstName);
            this.m_MasterAccessionNo = orderIdParser.MasterAccessionNo;
            this.m_AliquotOrder = aliquotOrder;
            this.m_BlockTitle = aliquotOrder.PrintLabel;
            this.m_Verified = aliquotOrder.GrossVerified;           
            this.m_PatientInitials = patientName.GetInitials();
            this.m_EmbeddingInstructions = aliquotOrder.EmbeddingInstructions;

            if(accessionOrder.ClientAccessioned == true)
            {
                this.m_ClientAccessionNo = accessionOrder.ClientAccessionNo;
                this.m_ClientAccessioned = true;
            }
            else
            {
                this.m_ClientAccessionNo = null;
                this.m_ClientAccessioned = false;
            }

            if(YellowstonePathology.Business.User.UserPreferenceInstance.Instance.UserPreference.UseLaserCassettePrinter == false)
            {
                this.m_CassetteColumn = accessionOrder.PrintMateColumnNumber.ToString();
            }
            else
            {
                YellowstonePathology.Business.Common.PrintMateCarousel printMateCarousel = new Common.PrintMateCarousel();
                YellowstonePathology.Business.Common.PrintMateColumn printMateColumn = printMateCarousel.GetColumn(accessionOrder.PrintMateColumnNumber);
                this.m_CassetteColumn = printMateColumn.GeneralDataColor.ToString();
            }
        }

        public string ToLaserString()
        {
            //C:\Program Files\General Data Company\Cassette Printing\Normal.itl|102|15-28044|1A|JA|YPII|ALQ15-28044.1A|15|28044
            YellowstonePathology.Business.OrderIdParser orderIdParser = new YellowstonePathology.Business.OrderIdParser(this.m_MasterAccessionNo);
            StringBuilder result = new StringBuilder(TemplateFileName + this.m_LaserDelimeter);                                    

            result.Append(this.m_CassetteColumn.ToString() + this.m_LaserDelimeter);
            result.Append(orderIdParser.MasterAccessionNo + this.m_LaserDelimeter);
            result.Append(this.BlockTitle + this.m_LaserDelimeter);
            result.Append(this.PatientInitials + this.m_LaserDelimeter);

            if(this.m_ClientAccessioned == true)
            {
                result.Append(this.m_ClientAccessionNo + this.m_LaserDelimeter);
            }
            else
            {
                if(string.IsNullOrEmpty(this.m_EmbeddingInstructions) == false)
                {
                    result.Append(this.m_EmbeddingInstructions + this.m_LaserDelimeter);
                }
                else
                {
                    result.Append(this.m_CompanyId + this.m_LaserDelimeter);
                }                
            }
            
            result.Append(this.ScanningId + this.m_LaserDelimeter);
            result.Append(orderIdParser.MasterAccessionNoYear.Value.ToString() + this.m_LaserDelimeter);
            result.Append(orderIdParser.MasterAccessionNoNumber.Value.ToString());
            return result.ToString();
        }

        public override string ToString()
        {
            YellowstonePathology.Business.OrderIdParser orderIdParser = new YellowstonePathology.Business.OrderIdParser(this.m_MasterAccessionNo);

            StringBuilder result = new StringBuilder(this.m_Prefix + this.m_Delimeter);
            result.Append(this.FormattedCassetteColumn + this.m_Delimeter);
            result.Append(orderIdParser.MasterAccessionNo + this.m_Delimeter);
            result.Append(this.BlockTitle + this.m_Delimeter);
            result.Append(this.PatientInitials + this.m_Delimeter);
            result.Append(this.CompanyId + this.m_Delimeter);
            result.Append(this.ScanningId + this.m_Delimeter);
            result.Append(orderIdParser.MasterAccessionNoYear.Value.ToString() + this.m_Delimeter);
            result.Append(orderIdParser.MasterAccessionNoNumber.Value.ToString());
            return result.ToString();
        }        
	}
}
