using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Data;
using System.Data;
using System.Data.SqlClient;

namespace YellowstonePathology.Business.Flow
{
    public class FlowPanelList : ObservableCollection<FlowPanelListItem>
    {
        private SqlCommand m_Cmd; 

        public FlowPanelList()
        {
            this.m_Cmd = new SqlCommand();
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
                        FlowPanelListItem item = new FlowPanelListItem();
                        item.Fill(dr);
                        this.Add(item);
                    }
                }
            }
        }

        public void SetFillCommandByAll()
        {
            this.m_Cmd.Parameters.Clear();
            string sql = "select * from tblFlowPanel Where Active = 1 order by PanelName";
            this.m_Cmd.CommandText = sql;
            this.m_Cmd.CommandType = CommandType.Text;
        }        
    }

    public class FlowPanelListItem : ListItem
    {
        int m_PanelId;
        string m_PanelName;

        public FlowPanelListItem()
        {

        }

        [YellowstonePathology.Business.CustomAttributes.SqlListItemFieldAttribute("PanelId", SqlDbType.Int)]
        public int PanelId
        {
            get { return this.m_PanelId; }
            set
            {
                if (value != this.m_PanelId)
                {
                    this.m_PanelId = value;
                    this.NotifyPropertyChanged("PanelId");
                }
            }
        }

        [YellowstonePathology.Business.CustomAttributes.SqlListItemFieldAttribute("PanelName", SqlDbType.VarChar)]
        public string PanelName
        {
            get { return this.m_PanelName; }
            set
            {
                if (value != this.m_PanelName)
                {
                    this.m_PanelName = value;
                    this.NotifyPropertyChanged("PanelName");
                }
            }
        }

        public override void Fill(SqlDataReader dr)
        {
            this.m_PanelId = BaseData.GetIntValue("PanelId", dr);
            this.m_PanelName = BaseData.GetStringValue("PanelName", dr);
        }
    }
}
