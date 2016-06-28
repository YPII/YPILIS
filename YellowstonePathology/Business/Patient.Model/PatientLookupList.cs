using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace YellowstonePathology.Business.Patient.Model
{
    public class PatientLookupList : ObservableCollection<PatientLookupListItem>
    {
        SqlCommand m_Cmd;

        public PatientLookupList()
        {
            this.m_Cmd = new SqlCommand();
        }

        public void SetFillCommandByPatientName(string lastName, string firstName)
        {
            this.m_Cmd.Parameters.Clear();
            this.m_Cmd.CommandText = "prcGetPatientLookup";
            this.m_Cmd.CommandType = CommandType.StoredProcedure;
            this.m_Cmd.Parameters.Add("@Lastname", SqlDbType.VarChar, 100).Value = lastName;
            this.m_Cmd.Parameters.Add("@Firstname", SqlDbType.VarChar, 100).Value = firstName;            
        }

        public void Fill()
        {
            this.Clear();
            using (SqlConnection cn = new SqlConnection(BaseData.SqlConnectionString))
            {
                cn.Open();
                this.m_Cmd.Connection = cn;
                using (SqlDataReader dr = this.m_Cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        PatientLookupListItem item = new PatientLookupListItem();
                        item.Fill(dr);
                        this.Add(item);
                    }
                }
            }
        }
    }

    public class PatientLookupListItem : ListItem
    {
        string m_LastName;
        string m_FirstName;
        string m_MiddleInitial;
        Nullable<DateTime> m_Birthdate;
        string m_SSN;

        public PatientLookupListItem()
        {

        }

        [YellowstonePathology.Business.CustomAttributes.SqlListItemFieldAttribute("LastName", SqlDbType.VarChar)]
        public string LastName
        {
            get { return this.m_LastName; }
            set
            {
                if (value != this.m_LastName)
                {
                    this.m_LastName = value;
                    this.NotifyPropertyChanged("LastName");
                }
            }
        }

        [YellowstonePathology.Business.CustomAttributes.SqlListItemFieldAttribute("FirstName", SqlDbType.VarChar)]
        public string FirstName
        {
            get { return this.m_FirstName; }
            set
            {
                if (value != this.m_FirstName)
                {
                    this.m_FirstName = value;
                    this.NotifyPropertyChanged("FirstName");
                }
            }
        }

        [YellowstonePathology.Business.CustomAttributes.SqlListItemFieldAttribute("MiddleInitial", SqlDbType.VarChar)]
        public string MiddleInitial
        {
            get { return this.m_MiddleInitial; }
            set
            {
                if (value != this.m_MiddleInitial)
                {
                    this.m_MiddleInitial = value;
                    this.NotifyPropertyChanged("MiddleInitial");
                }
            }
        }


        [YellowstonePathology.Business.CustomAttributes.SqlListItemFieldAttribute("Birthdate", SqlDbType.DateTime)]
        public Nullable<DateTime> Birthdate
        {
            get { return this.m_Birthdate; }
            set
            {
                if (value != this.m_Birthdate)
                {
                    this.m_Birthdate = value;
                    this.NotifyPropertyChanged("Birthdate");
                }
            }
        }


        [YellowstonePathology.Business.CustomAttributes.SqlListItemFieldAttribute("SSN", SqlDbType.VarChar)]
        public string SSN
        {
            get { return this.m_SSN; }
            set
            {
                if (value != this.m_SSN)
                {
                    this.m_SSN = value;
                    this.NotifyPropertyChanged("SSN");
                }
            }
        }

        public override void Fill(SqlDataReader dr)
        {
            this.LastName = BaseData.GetStringValue("LastName", dr);
            this.FirstName = BaseData.GetStringValue("FirstName", dr);
            this.MiddleInitial = BaseData.GetStringValue("MiddleInitial", dr);
            this.Birthdate = BaseData.GetDateTimeValue("Birthdate", dr);
            this.SSN = BaseData.GetStringValue("SSN", dr);
        }		
    }
}
