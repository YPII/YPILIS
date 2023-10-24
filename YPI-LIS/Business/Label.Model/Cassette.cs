using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Label.Model
{
	public class Cassette 
	{
        //C:\GDHC\cassette\formats\Normal.itl#102#$#H2#15-23277#FS1A#JP#YPII#ALQ15-23277.1A#15#23277
        protected const string TemplateFileName = @"C:\Program Files\General Data Company\Cassette Printing\Normal.itl";
        protected const string SomeNumber = "102";

        protected YellowstonePathology.Business.Test.AliquotOrder m_AliquotOrder;		
                		
		protected string m_CompanyId = "YPII";
		protected string m_ScanningPrefix = "ALQ";
		protected string m_CassetteColor;
		protected string m_BlockTitle;
		protected string m_PatientInitials;		
		protected bool m_PrintRequested;
		protected bool m_Verified;
		protected string m_ReportNo;
		protected string m_MasterAccessionNo;
        protected string m_ClientAccessionNo;
        protected bool m_ClientAccessioned;
        protected string m_EmbeddingInstructions;
		protected string m_ProstateDescription;

        public Cassette()
		{
            
		}

		public Cassette(string cassetteColor, string prostateDescription)
		{
			this.m_CassetteColor = cassetteColor;
			this.m_ProstateDescription = prostateDescription;
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

        public string CassetteColor
        {
            get { return this.m_CassetteColor; }
			set
			{
				if (value != this.m_CassetteColor)
				{
					this.m_CassetteColor = value;					
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

		public string ProstateDescription
		{
			get { return this.m_ProstateDescription; }
			set
			{
				if (value != this.m_ProstateDescription)
				{
					this.m_ProstateDescription = value;
				}
			}
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

        public virtual string GetLine(int printerColorCode)
        {
            throw new Exception("The Get Print String is not implemented in the base.");
        }

        public virtual string GetFileExtension()
        {
            throw new Exception("Not implemented here.");
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
            this.m_CassetteColor = accessionOrder.CassetteColor;
        }        
	}
}
