using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.XPSDocument.Result.ClientBillingDetailReportResult
{
    public class ClientBillingDetailReportDataAccessionOrder
    {
        List<ClientBillingDetailReportDataReport> m_ClientBillingDetailReportDataReports;

        private string m_MasterAccessionNo;
        private DateTime? m_DateOfService;
        private string m_PFirstName;
        private string m_PLastName;
        private string m_PMiddleInitial;
        private Nullable<DateTime> m_PBirthdate;
        private string m_ClientName;
        private string m_PhysicianName;
        private string m_SvhAccount;
        private string m_SvhMedicalRecord;
        private string m_PatientType;
        private string m_PrimaryInsurance;
        private string m_SecondaryInsurance;

        public ClientBillingDetailReportDataAccessionOrder()
        {
            this.m_ClientBillingDetailReportDataReports = new List<ClientBillingDetailReportDataReport>();
        }

        public List<ClientBillingDetailReportDataReport> ClientBillingDetailReportDataReports
        {
            get { return this.m_ClientBillingDetailReportDataReports; }
        }

        [PersistentProperty()]
        public string MasterAccessionNo
        {
            get { return this.m_MasterAccessionNo; }
            set { this.m_MasterAccessionNo = value; }
        }

        [PersistentProperty()]
        public DateTime? DateOfService
        {
            get { return this.m_DateOfService; }
            set { this.m_DateOfService = value; }
        }

        [PersistentProperty()]
        public string PFirstName
        {
            get { return this.m_PFirstName; }
            set { this.m_PFirstName = value; }
        }

        [PersistentProperty()]
        public string PLastName
        {
            get { return this.m_PLastName; }
            set { this.m_PLastName = value; }
        }

        [PersistentProperty()]
        public string PMiddleInitial
        {
            get { return this.m_PMiddleInitial; }
            set { this.m_PMiddleInitial = value; }
        }

        [PersistentProperty()]
        public Nullable<DateTime> PBirthdate
        {
            get { return this.m_PBirthdate; }
            set { this.m_PBirthdate = value; }
        }

        [PersistentProperty()]
        public string ClientName
        {
            get { return this.m_ClientName; }
            set { this.m_ClientName = value; }
        }

        [PersistentProperty()]
        public string PhysicianName
        {
            get { return this.m_PhysicianName; }
            set { this.m_PhysicianName = value; }
        }

        [PersistentProperty()]
        public string SvhAccount
        {
            get { return this.m_SvhAccount; }
            set { this.m_SvhAccount = value; }
        }

        [PersistentProperty()]
        public string SvhMedicalRecord
        {
            get { return this.m_SvhMedicalRecord; }
            set { this.m_SvhMedicalRecord = value; }
        }

        [PersistentProperty()]
        public string PatientType
        {
            get { return this.m_PatientType; }
            set { this.m_PatientType = value; }
        }

        [PersistentProperty()]
        public string PrimaryInsurance
        {
            get { return this.m_PrimaryInsurance; }
            set { this.m_PrimaryInsurance = value; }
        }

        [PersistentProperty()]
        public string SecondaryInsurance
        {
            get { return this.m_SecondaryInsurance; }
            set { this.m_SecondaryInsurance = value; }
        }
    }
}
