using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Data;
using System.Data.SqlClient;

namespace YellowstonePathology.Business.Flow
{
    public class MarkerCollection : ObservableCollection<MarkerItem>
    {
        protected SqlCommand m_Cmd;
        protected List<SqlParameter> m_ParameterList;

        public MarkerCollection()
        {
            this.m_Cmd = new SqlCommand();
            this.m_ParameterList = new List<SqlParameter>();
        }

        public void SetFillCommandAll()
        {
            string sql = "select * from tblMarkers order by OrderFlag, MarkerName ";
            this.m_ParameterList.Clear();
            this.m_Cmd.CommandText = sql;
            this.m_Cmd.CommandType = CommandType.Text;
        }

        public void SetFillCommandByMarkerId(int markerId)
        {
            string sql = "select * from tblMarkers where MarkerId = " + markerId; 
            this.m_ParameterList.Clear();
            this.m_Cmd.CommandText = sql;
            this.m_Cmd.CommandType = CommandType.Text;
        }

        public void Fill()
        {
            this.Clear();
            using (SqlConnection cn = new SqlConnection(BaseData.SqlConnectionString))
            {
                cn.Open();
                this.m_Cmd.Connection = cn;
                using (SqlDataReader dr = m_Cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        MarkerItem item = new MarkerItem();
                        item.Fill(dr);
                        this.Add(item);
                    }
                }
            }
        }
    }    
}
				
