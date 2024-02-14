using System;
using System.ComponentModel;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.UI.Billing
{
	public class SimulationListItem : INotifyPropertyChanged
    {
		public event PropertyChangedEventHandler PropertyChanged;

        private string m_MasterAccessionNo;
        private string m_ReportNo;
		private Nullable<DateTime> m_PostDate;
        private Nullable<DateTime> m_FinalDate;
		private int m_ClientId;
		private string m_ClientName;        
		private string m_PanelSetName;
        private string m_PatientType;
        private string m_PatientTypeSim;
        private string m_PatientClass;
        private string m_AssignedPatientLocation;
        private string m_PrimaryInsuranceManual;
        private string m_PrimaryInsuranceADT;
        private string m_PrimaryInsuranceSim;
        private string m_MedicalRecord;
        private string m_BackgroundColor;        

        public SimulationListItem()
		{

		}

		public void NotifyPropertyChanged(String info)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(info));
			}
		}        

        [PersistentProperty()]
        public string MasterAccessionNo
		{
			get { return this.m_MasterAccessionNo; }
			set
			{
				if (value != this.m_MasterAccessionNo)
				{
					this.m_MasterAccessionNo = value;
					this.NotifyPropertyChanged("MasterAccessionNo");
				}
			}
		}

        [PersistentProperty()]
        public string ReportNo
        {
            get { return this.m_ReportNo; }
            set
            {
                if (value != this.m_ReportNo)
                {
                    this.m_ReportNo = value;
                    this.NotifyPropertyChanged("ReportNo");
                }
            }
        }

        [PersistentProperty()]
        public Nullable<DateTime> PostDate
		{
			get { return this.m_PostDate; }
			set
			{
				if (value != this.m_PostDate)
				{
					this.m_PostDate = value;
					this.NotifyPropertyChanged("PostDate");
				}
			}
		}

        [PersistentProperty()]
        public Nullable<DateTime> FinalDate
        {
            get { return this.m_FinalDate; }
            set
            {
                if (value != this.m_FinalDate)
                {
                    this.m_FinalDate = value;
                    this.NotifyPropertyChanged("FinaltDate");
                }
            }
        }

        [PersistentProperty()]
        public int ClientId
		{
			get { return this.m_ClientId; }
			set
			{
				if (value != this.m_ClientId)
				{
					this.m_ClientId = value;
					this.NotifyPropertyChanged("ClientId");
				}
			}
		}        

        [PersistentProperty()]
        public string ClientName
		{
			get { return this.m_ClientName; }
			set
			{
				if (value != this.m_ClientName)
				{
					this.m_ClientName = value;
					this.NotifyPropertyChanged("ClientName");
				}
			}
		}
        
        [PersistentProperty()]
        public string PanelSetName
		{
			get { return this.m_PanelSetName; }
			set
			{
				if (value != this.m_PanelSetName)
				{
					this.m_PanelSetName = value;
					this.NotifyPropertyChanged("PanelSetName");
				}
			}
		}

        [PersistentProperty()]
        public string PatientType
        {
            get { return this.m_PatientType; }
            set
            {
                if (value != this.m_PatientType)
                {
                    this.m_PatientType = value;
                    this.NotifyPropertyChanged("PatientType");
                }
            }
        }

        [PersistentProperty()]
        public string PatientClass
        {
            get { return this.m_PatientClass; }
            set
            {
                if (value != this.m_PatientClass)
                {
                    this.m_PatientClass = value;
                    this.NotifyPropertyChanged("PatientClass");
                }
            }
        }

        [PersistentProperty()]
        public string AssignedPatientLocation
        {
            get { return this.m_AssignedPatientLocation; }
            set
            {
                if (value != this.m_AssignedPatientLocation)
                {
                    this.m_AssignedPatientLocation = value;
                    this.NotifyPropertyChanged("AssignedPatientLocation");
                }
            }
        }

        [PersistentProperty()]
        public string PatientTypeSim
        {
            get { return this.m_PatientTypeSim; }
            set
            {
                if (value != this.m_PatientTypeSim)
                {
                    this.m_PatientTypeSim = value;
                    this.NotifyPropertyChanged("PatientTypeSim");
                }
            }
        }

        [PersistentProperty()]
        public string PrimaryInsuranceManual
        {
            get { return this.m_PrimaryInsuranceManual; }
            set
            {
                if (value != this.m_PrimaryInsuranceManual)
                {
                    this.m_PrimaryInsuranceManual = value;
                    this.NotifyPropertyChanged("PrimaryInsuranceManual");
                }
            }
        }

        [PersistentProperty()]
        public string PrimaryInsuranceADT
        {
            get { return this.m_PrimaryInsuranceADT; }
            set
            {
                if (value != this.m_PrimaryInsuranceADT)
                {
                    this.m_PrimaryInsuranceADT = value;
                    this.NotifyPropertyChanged("PrimaryInsuranceADT");
                }
            }
        }

        [PersistentProperty()]
        public string PrimaryInsuranceSim
        {
            get { return this.m_PrimaryInsuranceSim; }
            set
            {
                if (value != this.m_PrimaryInsuranceSim)
                {
                    this.m_PrimaryInsuranceSim = value;
                    this.NotifyPropertyChanged("PrimaryInsuranceSim");
                }
            }
        }

        [PersistentProperty()]
        public string MedicalRecord
        {
            get { return this.m_MedicalRecord; }
            set
            {
                if (value != this.m_MedicalRecord)
                {
                    this.m_MedicalRecord = value;
                    this.NotifyPropertyChanged("MedicalRecord");
                }
            }
        }

        public string BackgroundColor
        {
            get { return this.m_BackgroundColor; }
            set
            {
                if (value != this.m_BackgroundColor)
                {
                    this.m_BackgroundColor = value;
                    this.NotifyPropertyChanged("BackgroundColor");
                }
            }
        }

        public void SetInsuranceBackgroundColor()
        {
            this.m_BackgroundColor = "Warning";
            if(string.IsNullOrEmpty(this.m_PrimaryInsuranceManual) == false)
            {
                if(this.m_PrimaryInsuranceManual != "Not Selected")
                {                    
                    if(this.m_PrimaryInsuranceManual == this.m_PrimaryInsuranceSim)
                    {
                        this.m_BackgroundColor = "Normal";
                    }                    
                    else
                    {
                        this.m_BackgroundColor = "Critical";
                    }                    
                }
                else
                {
                    if (this.m_PrimaryInsuranceSim == "Commercial")
                    {
                        this.m_BackgroundColor = "Normal";
                    }
                }
            }
            this.NotifyPropertyChanged("BackgroundColor");
        }

        public string GetAdjustedPrimaryInsurance()
        {
            string result = this.m_PrimaryInsuranceSim;
            if (this.m_PrimaryInsuranceSim == "Commercial")
                result = "Not Selected";
            return result;
        }
    }
}
