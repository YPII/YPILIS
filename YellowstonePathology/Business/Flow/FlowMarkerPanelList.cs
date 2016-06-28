using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Data;

namespace YellowstonePathology.Business.Flow
{
    public class FlowMarkerPanelList : ObservableCollection<FlowMarkerPanelListItem>
    {
        private SqlCommand m_Cmd;

        public FlowMarkerPanelList()
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
                        FlowMarkerPanelListItem item = new FlowMarkerPanelListItem();
                        item.Fill(dr);
                        this.Add(item);
                    }
                }
            }
        }

        public void SetFillCommandByPanelId(int panelId)
        {
            this.m_Cmd.Parameters.Clear();
            string sql = "Select * from tblFlowMarkerPanel where PanelId = @PanelId";
            this.m_Cmd.Parameters.Add("@PanelId", SqlDbType.Int).Value = panelId;            
            this.m_Cmd.CommandText = sql;
            this.m_Cmd.CommandType = CommandType.Text;
        }
    }

    public class FlowMarkerPanelListItem : ListItem
    {
        int m_PanelId;
        string m_MarkerName;
        string m_Reference;

        public FlowMarkerPanelListItem()
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

        [YellowstonePathology.Business.CustomAttributes.SqlListItemFieldAttribute("MarkerName", SqlDbType.VarChar)]
        public string MarkerName
        {
            get { return this.m_MarkerName; }
            set
            {
                if (value != this.m_MarkerName)
                {
                    this.m_MarkerName = value;
                    this.NotifyPropertyChanged("MarkerName");
                }
            }
        }

        [YellowstonePathology.Business.CustomAttributes.SqlListItemFieldAttribute("Reference", SqlDbType.VarChar)]
        public string Reference
        {
            get { return this.m_Reference; }
            set
            {
                if (value != this.m_Reference)
                {
                    this.m_Reference = value;
                    this.NotifyPropertyChanged("Reference");
                }
            }
        }

        public override void Fill(SqlDataReader dr)
        {
            this.m_PanelId = BaseData.GetIntValue("PanelId", dr);
            this.m_MarkerName = BaseData.GetStringValue("MarkerName", dr);
            this.m_Reference = BaseData.GetStringValue("Reference", dr);
        }
    }
}
