using System;
using System.Collections.ObjectModel;
using System.Data;
using MySql.Data.MySqlClient;

namespace YellowstonePathology.Business
{
    public class SearchResultList : ObservableCollection<SearchResultListItem>
    {
        MySqlCommand m_Cmd;

        public SearchResultList()
        {
            this.m_Cmd = new MySqlCommand();
        }

		public void SetFillByAccessionNo(string reportNo)
        {
            this.m_Cmd.Parameters.Clear();
            this.m_Cmd.CommandText = "prcGetSearchResultsByAccessionNo";
            this.m_Cmd.CommandType = CommandType.StoredProcedure;
			this.m_Cmd.Parameters.Add("@ReportNo", SqlDbType.VarChar).Value = reportNo;
        }

        public void Fill()
        {
            this.Clear();
            using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                this.m_Cmd.Connection = cn;
                using (MySqlDataReader dr = this.m_Cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        SearchResultListItem item = new SearchResultListItem();
                        item.Fill(dr);
                        this.Add(item);
                    }
                }
            }
        }
    }

    public class SearchResultListItem : BaseListItem
    {
        string m_Result;
        string m_ReportNo;

        public SearchResultListItem()
        {

        }
        
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

        public string Result
        {
            get { return this.m_Result; }
            set 
            {
                if (this.m_Result != value)
                {
                    this.m_Result = value;
                    this.NotifyPropertyChanged("Result");
                }
            }
        }

        public void Fill(MySqlDataReader dr)
        {
            this.ReportNo = BaseData.GetStringValue("ReportNo", dr);
            this.Result = BaseData.GetStringValue("Result", dr);
        }
    }
}
