using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using MySql.Data.MySqlClient;

namespace YellowstonePathology.Business.Flow
{
    public class MarkerCollection : ObservableCollection<MarkerItem>
    {
        protected MySqlCommand m_Cmd;
        protected List<MySqlParameter> m_ParameterList;

        public MarkerCollection()
        {
            this.m_Cmd = new MySqlCommand();
            this.m_ParameterList = new List<MySqlParameter>();
        }

        public void SetFillCommandAll()
        {
            string sql = "select * from tblMarkers order by OrderFlag, MarkerName;";
            this.m_ParameterList.Clear();
            this.m_Cmd.CommandText = sql;
            this.m_Cmd.CommandType = CommandType.Text;
        }

        public void SetFillCommandByMarkerId(int markerId)
        {
            string sql = "select * from tblMarkers where MarkerId = " + markerId + ";"; 
            this.m_ParameterList.Clear();
            this.m_Cmd.CommandText = sql;
            this.m_Cmd.CommandType = CommandType.Text;
        }

        public void Fill()
        {
            this.Clear();
            using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                this.m_Cmd.Connection = cn;
                using (MySqlDataReader dr = m_Cmd.ExecuteReader())
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
				
