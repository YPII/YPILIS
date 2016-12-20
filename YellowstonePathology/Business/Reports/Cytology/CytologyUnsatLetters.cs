using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using MySql.Data.MySqlClient;

namespace YellowstonePathology.Business.Reports.Cytology
{
    public class CytologyUnsatLetters : ObservableCollection<CytologyUnsatLetterItem>
    {
        MySqlCommand cmd;

        public CytologyUnsatLetters()
        {
            this.cmd = new MySqlCommand();
        }

        public void FillByPhysicianId(int physicianId, DateTime startDate, DateTime endDate)
        {            
            string sql = "prcReportGetCytologyUnsatLettersByPhysician";
            cmd.Parameters.AddWithValue("@PhysicianId", physicianId);
            cmd.Parameters.AddWithValue("@StartDate", startDate.ToShortDateString());
            cmd.Parameters.AddWithValue("@EndDate", endDate.ToShortDateString());
            cmd.CommandText = sql;
            cmd.CommandType = CommandType.StoredProcedure;
            this.FillData();
        }

        public void FillByClientId(int clientId, DateTime startDate, DateTime endDate)
        {
            string sql = "prcReportGetCytologyUnsatLettersByClient";
            cmd.Parameters.AddWithValue("@ClientId", clientId);
            cmd.Parameters.AddWithValue("@StartDate", startDate.ToShortDateString());
            cmd.Parameters.AddWithValue("@EndDate", endDate.ToShortDateString());
            cmd.CommandText = sql;
            cmd.CommandType = CommandType.StoredProcedure;
            this.FillData();
        }

        public void FillByDate(DateTime startDate, DateTime endDate)
        {            
            string sql = "prcReportGetCytologyUnsatLetters";
            cmd.Parameters.AddWithValue("@StartDate", startDate.ToShortDateString());
            cmd.Parameters.AddWithValue("@EndDate", endDate.ToShortDateString());
            cmd.CommandText = sql;
            cmd.CommandType = CommandType.StoredProcedure;
            this.FillData();
        }

        private void FillData()
        {
            using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;                

                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    //int currentPhysicianClientId = 0;
					string currentPhysicianClientId = "0";
                    CytologyUnsatLetterItem item = new CytologyUnsatLetterItem();
                    CytologyUnsatLetterDetailItem detailItem = new CytologyUnsatLetterDetailItem();

                    while (dr.Read())
                    {
                        //int thisPhysicianClientId = BaseData.GetIntValue("PhysicianClientId", dr);
						string thisPhysicianClientId = BaseData.GetStringValue("PhysicianClientId", dr);
						if (thisPhysicianClientId != currentPhysicianClientId)
                        {
                            item = new CytologyUnsatLetterItem();
                            item.Fill(dr);
                            this.Add(item);

                            detailItem = new CytologyUnsatLetterDetailItem();
                            detailItem.Fill(dr);
                            item.DetailItems.Add(detailItem);
                        }
                        else
                        {
                            detailItem = new CytologyUnsatLetterDetailItem();
                            detailItem.Fill(dr);
                            item.DetailItems.Add(detailItem);
                        }
                        currentPhysicianClientId = thisPhysicianClientId;
                    }
                }
            }            
        }
    }


    public class CytologyUnsatLetterItem
    {
		string m_PhysicianClientId;
		int m_ClientId;
        string m_ClientName;
        string m_PhysicianName;
        string m_Address;
        string m_CityStateZip;
        string m_FaxNumber;
        bool m_LongDistance;

        List<CytologyUnsatLetterDetailItem> m_DetailItems;

        public CytologyUnsatLetterItem()
        {
            this.m_DetailItems = new List<CytologyUnsatLetterDetailItem>();
        }

        public List<CytologyUnsatLetterDetailItem> DetailItems
        {
            get { return this.m_DetailItems; }
        }

        public string PhysicianClientId
        {
            get { return this.m_PhysicianClientId; }
            set { this.m_PhysicianClientId = value; }
        }

        public int ClientId
        {
            get { return this.m_ClientId; }
            set { this.m_ClientId = value; }
        }

        public string ClientName
        {
            get { return this.m_ClientName; }
            set { this.m_ClientName = value; }
        }


        public string PhysicianName
        {
            get { return this.m_PhysicianName; }
            set { this.m_PhysicianName = value; }
        }

        public string Address
        {
            get { return this.m_Address; }
            set { this.m_Address = value; }
        }


        public string CityStateZip
        {
            get { return this.m_CityStateZip; }
            set { this.m_CityStateZip = value; }
        }

        public string FaxNumber
        {
            get { return this.m_FaxNumber; }
            set { this.m_FaxNumber = value; }
        }

        public bool LongDistance
        {
            get { return this.m_LongDistance; }
            set { this.m_LongDistance = value; }
        }

        public void Fill(MySqlDataReader dr)
        {
            this.PhysicianClientId = BaseData.GetStringValue("PhysicianClientId", dr);
            this.ClientId = BaseData.GetIntValue("ClientId", dr);
            this.ClientName = BaseData.GetStringValue("ClientName", dr);
            this.PhysicianName = BaseData.GetStringValue("PhysicianName", dr);
            this.Address = BaseData.GetStringValue("Address", dr);
            this.CityStateZip = BaseData.GetStringValue("CityStateZip", dr);
            this.FaxNumber = BaseData.GetStringValue("Fax", dr);
            this.LongDistance = BaseData.GetBoolValue("LongDistance", dr);
        }
    }

    public class CytologyUnsatLetterDetailItem
    {
        string m_PhysicianClientId;
        int m_ClientId;
        string m_ReportNo;
        Nullable<DateTime> m_PBirthdate;
        string m_PatientName;
        Nullable<DateTime> m_CollectionDate;
        string m_ScreeningImpression;

        public CytologyUnsatLetterDetailItem()
        {

        }


        public string PhysicianClientId
        {
            get { return this.m_PhysicianClientId; }
            set { this.m_PhysicianClientId = value; }
        }

        public int ClientId
        {
            get { return this.m_ClientId; }
            set { this.m_ClientId = value; }
        }

        public string ReportNo
        {
            get { return this.m_ReportNo; }
            set { this.m_ReportNo = value; }
        }


        public Nullable<DateTime> PBirthdate
        {
            get { return this.m_PBirthdate; }
            set { this.m_PBirthdate = value; }
        }


        public string PatientName
        {
            get { return this.m_PatientName; }
            set { this.m_PatientName = value; }
        }


        public Nullable<DateTime> CollectionDate
        {
            get { return this.m_CollectionDate; }
            set { this.m_CollectionDate = value; }
        }


        public string ScreeningImpression
        {
            get { return this.m_ScreeningImpression; }
            set { this.m_ScreeningImpression = value; }
        }

        public void Fill(MySqlDataReader dr)
        {
            this.PhysicianClientId = BaseData.GetStringValue("PhysicianClientId", dr);
            this.ClientId = BaseData.GetIntValue("ClientId", dr);
            this.ReportNo = BaseData.GetStringValue("ReportNo", dr);
            this.PBirthdate = BaseData.GetDateTimeValue("PBirthdate", dr);
            this.PatientName = BaseData.GetStringValue("PatientName", dr);
            this.CollectionDate = BaseData.GetDateTimeValue("CollectionDate", dr);
            this.ScreeningImpression = BaseData.GetStringValue("ScreeningImpression", dr);
        }
    }
}
