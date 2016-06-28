using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace YellowstonePathology.Business
{
    public class SearchListItem : BaseListItem
    {
        string m_MasterAccessionNo;
        string m_ReportNo;
        Nullable<DateTime> m_AccessionDate;
        Nullable<DateTime> m_FinalDate;
        string m_PatientName;
        string m_SSN;
        Nullable<DateTime> m_Birthdate;
        string m_PatientId;        
        int m_PanelSetId;
        int m_ClientId;  
        
        public SearchListItem()
        {

        }

        [YellowstonePathology.Business.CustomAttributes.SqlField("MasterAccessionNo", 20, SqlDbType.VarChar)]
		public string MasterAccessionNo
        {
            get { return this.m_MasterAccessionNo; }
            set { this.m_MasterAccessionNo = value; }
        }

        [YellowstonePathology.Business.CustomAttributes.SqlField("ReportNo", 10, SqlDbType.VarChar)]
        public string ReportNo
        {
            get { return this.m_ReportNo; }
            set { this.m_ReportNo = value; }
        }

        [YellowstonePathology.Business.CustomAttributes.SqlField("AccessionDate", 20, SqlDbType.DateTime)]
        public Nullable<DateTime> AccessionDate
        {
            get { return this.m_AccessionDate; }
            set { this.m_AccessionDate = value; }
        }

        [YellowstonePathology.Business.CustomAttributes.SqlField("FinalDate", 20, SqlDbType.DateTime)]
        public Nullable<DateTime> FinalDate
        {
            get { return this.m_FinalDate; }
            set { this.m_FinalDate = value; }
        }

        [YellowstonePathology.Business.CustomAttributes.SqlField("PatientName", 200, SqlDbType.VarChar)]
        public string PatientName
        {
            get { return this.m_PatientName; }
            set { this.m_PatientName = value; }
        }

        [YellowstonePathology.Business.CustomAttributes.SqlField("PSSN", 20, SqlDbType.VarChar)]
        public string SSN
        {
            get { return this.m_SSN; }
            set { this.m_SSN = value; }
        }

        [YellowstonePathology.Business.CustomAttributes.SqlField("PBirthdate", 20, SqlDbType.DateTime)]
        public Nullable<DateTime> Birthdate
        {
            get { return this.m_Birthdate; }
            set { this.m_Birthdate = value; }
        }

        [YellowstonePathology.Business.CustomAttributes.SqlField("PatientId", 20, SqlDbType.VarChar)]
        public string PatientId
        {
            get { return this.m_PatientId; }
            set { this.m_PatientId = value; }
        }
        
        [YellowstonePathology.Business.CustomAttributes.SqlField("PanelSetId", 20, SqlDbType.Int)]
        public int PanelSetId
        {
            get { return this.m_PanelSetId; }
            set { this.m_PanelSetId = value; }
        }

        [YellowstonePathology.Business.CustomAttributes.SqlField("ClientId", 20, SqlDbType.Int)]
        public int ClientId
        {
            get { return this.m_ClientId; }
            set { this.m_ClientId = value; }
        }  
    }
}
