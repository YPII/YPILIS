using System;
using System.Collections.ObjectModel;
using MySql.Data.MySqlClient;

namespace YellowstonePathology.Business.Billing.Model
{
    public class  ICDCodeList : ObservableCollection<ICDCode>
    {
        private MySqlCommand m_SqlCommand;  

        public ICDCodeList()
        {
            this.m_SqlCommand = new MySqlCommand();
        }

        public void SetFillCommandByFlowCodes()
        {
            this.m_SqlCommand.Parameters.Clear();
            this.m_SqlCommand.CommandText = "Select * from tblICD9Code where Category = 'Flow' order by Description";            
        }

        public void Fill()
        {
            this.Clear();
            using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                this.m_SqlCommand.Connection = cn;
                using (MySqlDataReader dr = this.m_SqlCommand.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        ICDCode item = new ICDCode();
                        item.Fill(dr);
                        this.Add(item);
                    }
                }
            }
        }
    }

    public class ICDCode : ListItem
    {
        int m_ICD9CodeId;
        string m_ICD9Code = string.Empty;
        string m_ICD10Code = string.Empty;
        string m_Category = string.Empty;
        string m_Description = string.Empty;

        public ICDCode()
        {

        }

        public int ICD9CodeId
        {
            get { return this.m_ICD9CodeId; }
            set { this.m_ICD9CodeId = value; }
        }

        public string ICD9Code
        {
            get { return this.m_ICD9Code; }
            set { this.m_ICD9Code = value; }
        }

        public string ICD10Code
        {
            get { return this.m_ICD10Code; }
            set { this.m_ICD10Code = value; }
        }

        public string Category
        {
            get { return this.m_Category; }
            set { this.m_Category = value; }
        }

        public string Description
        {
            get { return this.m_Description; }
            set { this.m_Description = value; }
        }

		public override void Fill(MySqlDataReader dr)
        {
            this.m_ICD9CodeId = BaseData.GetIntValue("ICD9CodeId", dr);
            this.m_ICD9Code = BaseData.GetStringValue("ICD9Code", dr);
            this.m_Category = BaseData.GetStringValue("Category", dr);
            this.m_Description = BaseData.GetStringValue("Description", dr);
        }
    }
}
