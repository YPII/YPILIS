using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Reflection;
using System.Data;
using System.Data.SqlClient;

namespace YellowstonePathology.Business
{
    public class SearchResultList : ObservableCollection<SearchResultListItem>
    {
        SqlCommand m_Cmd;

        public SearchResultList()
        {
            this.m_Cmd = new SqlCommand();
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
            using (SqlConnection cn = new SqlConnection(BaseData.SqlConnectionString))
            {
                cn.Open();
                this.m_Cmd.Connection = cn;
                using (SqlDataReader dr = this.m_Cmd.ExecuteReader())
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

        public void Fill(SqlDataReader dr)
        {
            this.ReportNo = BaseData.GetStringValue("ReportNo", dr);
            this.Result = BaseData.GetStringValue("Result", dr);
        }
    }
}
