using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Runtime.Serialization;

namespace YellowstonePathology.Business.Domain
{
    [DataContract]
    public class SearchResult
    {
		string m_MasterAccessionNo;
        string m_ReportNo;
        string m_PatientName;
        string m_PSex;
        string m_PCAN;
        string m_PSSN;
        Nullable<DateTime> m_PBirthdate;
        Nullable<DateTime> m_CollectionDate;
        DateTime m_AccessionDate;
        Nullable<DateTime> m_FinalTime;
        int m_ClientId;
		int m_PhysicianId;
        string m_ClientName;
        string m_PhysicianName;
        string m_PanelSetName;

        public SearchResult()
        {

        }

        [DataMember]
		public string MasterAccessionNo
        {
            get { return this.m_MasterAccessionNo; }
            set { this.m_MasterAccessionNo = value; }
        }

        [DataMember]
        public string ReportNo
        {
            get { return this.m_ReportNo; }
            set { this.m_ReportNo = value; }
        }

        [DataMember]
        public string PatientName
        {
            get { return this.m_PatientName; }
            set { this.m_PatientName = value; }
        }

        [DataMember]
        public string PCAN
        {
            get { return this.m_PCAN; }
            set { this.m_PCAN = value; }
        }

        [DataMember]
        public string PSSN
        {
            get { return this.m_PSSN; }
            set { this.m_PSSN = value; }
        }

        [DataMember]
        public string PSex
        {
            get { return this.m_PSex; }
            set { this.m_PSex = value; }
        }

        [DataMember]
        public Nullable<DateTime> PBirthdate
        {
            get { return this.m_PBirthdate; }
            set { this.m_PBirthdate = value; }
        }

        [DataMember]
        public Nullable<DateTime> CollectionDate
        {
            get { return this.m_CollectionDate; }
            set { this.m_CollectionDate = value; }
        }

        [DataMember]
        public DateTime AccessionDate
        {
            get { return this.m_AccessionDate; }
            set { this.m_AccessionDate = value; }
        }

        [DataMember]
        public int ClientId
        {
            get { return this.m_ClientId; }
            set { this.m_ClientId = value; }
        }

        [DataMember]
		public int PhysicianId
        {
            get { return this.m_PhysicianId; }
            set { this.m_PhysicianId = value; }
        }

        [DataMember]
        public string ClientName
        {
            get { return this.m_ClientName; }
            set { this.m_ClientName = value; }
        }

        [DataMember]
        public string PhysicianName
        {
            get { return this.m_PhysicianName; }
            set { this.m_PhysicianName = value; }
        }

        [DataMember]
        public Nullable<DateTime> FinalTime
        {
            get { return this.m_FinalTime; }
            set { this.m_FinalTime = value; }
        }

        [DataMember]
        public string PanelSetName
        {
            get { return this.m_PanelSetName; }
            set { this.m_PanelSetName = value; }
        }
    }
}
