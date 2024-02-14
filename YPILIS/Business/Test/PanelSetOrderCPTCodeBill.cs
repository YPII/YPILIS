using System;
using System.ComponentModel;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Test
{
	[PersistentClass("tblPanelSetOrderCPTCodeBill", "YPIDATA")]
	public class PanelSetOrderCPTCodeBill : INotifyPropertyChanged
	{			
        public event PropertyChangedEventHandler PropertyChanged;

		private string m_ObjectId;
		private string m_PanelSetOrderCPTCodeBillId;	
		private string m_ReportNo;
        private int m_ClientId;	
		private int m_Quantity;		
		private string m_CPTCode;
		private string m_Modifier;				
        private Nullable<DateTime> m_PostDate;
        private string m_BillTo;
        private string m_BillBy;
        private string m_CodeType;
        private string m_MedicalRecord;
        private string m_Account;
        private bool m_PostedToClient;
        private Nullable<DateTime> m_PostedToClientDate;

        public PanelSetOrderCPTCodeBill()
        {

        }

		public PanelSetOrderCPTCodeBill(string reportNo, string objectId, string panelSetOrderCPTCodeBillId)
		{
			this.m_ReportNo = reportNo;
			this.m_ObjectId = objectId;
			this.m_PanelSetOrderCPTCodeBillId = panelSetOrderCPTCodeBillId;
		}

        public void FromPanelSetOrderCPTCode(PanelSetOrderCPTCode panelSetOrderCPTCode)
        {
            this.m_ReportNo = panelSetOrderCPTCode.ReportNo;
            this.m_ClientId = panelSetOrderCPTCode.ClientId;
            this.m_Quantity = panelSetOrderCPTCode.Quantity;
            this.m_CPTCode = panelSetOrderCPTCode.CPTCode;
            this.m_CodeType = panelSetOrderCPTCode.CodeType;
            this.m_Modifier = panelSetOrderCPTCode.Modifier;
            this.m_MedicalRecord = panelSetOrderCPTCode.MedicalRecord;
            this.m_Account = panelSetOrderCPTCode.Account;
        }

		[PersistentDocumentIdProperty()]
		[PersistentDataColumnProperty(true, "50", "null", "varchar")]
		public string ObjectId
		{
			get { return this.m_ObjectId; }
			set
			{
				if (this.m_ObjectId != value)
				{
					this.m_ObjectId = value;
					this.NotifyPropertyChanged("ObjectId");
				}
			}
		}

        [PersistentPrimaryKeyProperty(false)]
        [PersistentDataColumnProperty(false, "50", "null", "varchar")]
        public string PanelSetOrderCPTCodeBillId
        {
            get { return this.m_PanelSetOrderCPTCodeBillId; }
            set
            {
                if (this.m_PanelSetOrderCPTCodeBillId != value)
                {
                    this.m_PanelSetOrderCPTCodeBillId = value;
                    this.NotifyPropertyChanged("PanelSetOrderCPTCodeBillId");
                }
            }
        }        

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "20", "null", "varchar")]
		public string ReportNo
		{
			get { return this.m_ReportNo; }
			set
			{
				if(this.m_ReportNo != value)
				{
					this.m_ReportNo = value;
					this.NotifyPropertyChanged("ReportNo");
				}
			}
		}

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "11", "null", "int")]
        public int ClientId
        {
            get { return this.m_ClientId; }
            set
            {
                if (this.m_ClientId != value)
                {
                    this.m_ClientId = value;
                    this.NotifyPropertyChanged("ClientId");
                }
            }
        }							

		[PersistentProperty()]
		[PersistentDataColumnProperty(false, "11", "null", "int")]
		public int Quantity
		{
			get { return this.m_Quantity; }
			set
			{
				if(this.m_Quantity != value)
				{
					this.m_Quantity = value;
					this.NotifyPropertyChanged("Quantity");
				}
			}
		}								

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "50", "null", "varchar")]
		public string CPTCode
		{
			get { return this.m_CPTCode; }
			set
			{
				if(this.m_CPTCode != value)
				{
					this.m_CPTCode = value;
					this.NotifyPropertyChanged("CPTCode");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "50", "null", "varchar")]
		public string Modifier
		{
			get { return this.m_Modifier; }
			set
			{
				if(this.m_Modifier != value)
				{
					this.m_Modifier = value;
					this.NotifyPropertyChanged("Modifier");
				}
			}
		}				

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "3", "null", "datetime")]
        public Nullable<DateTime> PostDate
        {
            get { return this.m_PostDate; }
            set
            {
                if (this.m_PostDate != value)
                {
                    this.m_PostDate = value;
                    this.NotifyPropertyChanged("PostDate");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "50", "null", "varchar")]
        public string BillTo
        {
            get { return this.m_BillTo; }
            set
            {
                if (this.m_BillTo != value)
                {
                    this.m_BillTo = value;
                    this.NotifyPropertyChanged("BillTo");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "50", "null", "varchar")]
        public string BillBy
        {
            get { return this.m_BillBy; }
            set
            {
                if (this.m_BillBy != value)
                {
                    this.m_BillBy = value;
                    this.NotifyPropertyChanged("BillBy");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "50", "null", "varchar")]
        public string CodeType
        {
            get { return this.m_CodeType; }
            set
            {
                if (this.m_CodeType != value)
                {
                    this.m_CodeType = value;
                    this.NotifyPropertyChanged("CodeType");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "50", "null", "varchar")]
        public string MedicalRecord
        {
            get { return this.m_MedicalRecord; }
            set
            {
                if (this.m_MedicalRecord != value)
                {
                    this.m_MedicalRecord = value;
                    this.NotifyPropertyChanged("MedicalRecord");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "50", "null", "varchar")]
        public string Account
        {
            get { return this.m_Account; }
            set
            {
                if (this.m_Account != value)
                {
                    this.m_Account = value;
                    this.NotifyPropertyChanged("Account");
                }
            }
        }

        [PersistentProperty()]        
        [PersistentDataColumnProperty(false, "1", "0", "tinyint")]
        public bool PostedToClient
        {
            get { return this.m_PostedToClient; }
            set
            {
                if (this.m_PostedToClient != value)
                {
                    this.m_PostedToClient = value;
                    this.NotifyPropertyChanged("PostedToClient");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "3", "null", "datetime")]
        public Nullable<DateTime> PostedToClientDate
        {
            get { return this.m_PostedToClientDate; }
            set
            {
                if (this.m_PostedToClientDate != value)
                {
                    this.m_PostedToClientDate = value;
                    this.NotifyPropertyChanged("PostedToClientDate");
                }
            }
        }

        public string GetBillToReverse()
        {
            string result = null;
            if (this.m_BillTo == "Patient") result = "Client";
            if (this.m_BillTo == "Client") result = "Patient";
            return result;
        }        
        
        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }			

        public void From(PanelSetOrderCPTCodeBill panelSetOrderCPTCodeBill)
        {
            this.m_ReportNo = panelSetOrderCPTCodeBill.ReportNo;
			this.m_ClientId = panelSetOrderCPTCodeBill.ClientId;
			this.m_CPTCode = panelSetOrderCPTCodeBill.CPTCode;
            this.m_CodeType = panelSetOrderCPTCodeBill.CodeType;
            this.m_Quantity = panelSetOrderCPTCodeBill.Quantity;
            this.m_Modifier = panelSetOrderCPTCodeBill.Modifier;            
            this.m_PostDate = panelSetOrderCPTCodeBill.PostDate;
            this.m_BillTo = panelSetOrderCPTCodeBill.BillTo;
            this.m_BillBy = panelSetOrderCPTCodeBill.BillBy;
            this.m_MedicalRecord = panelSetOrderCPTCodeBill.MedicalRecord;
            this.m_Account = panelSetOrderCPTCodeBill.Account;
            this.m_PostedToClient = panelSetOrderCPTCodeBill.m_PostedToClient;
            this.m_PostedToClientDate = panelSetOrderCPTCodeBill.m_PostedToClientDate;
        }
	}
}

















