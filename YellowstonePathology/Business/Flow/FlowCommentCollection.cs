using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using MySql.Data.MySqlClient;

namespace YellowstonePathology.Business.Flow
{
    public partial class FlowCommentCollection : ObservableCollection<FlowCommentItem>
    {
        protected MySqlCommand m_Cmd;
        protected List<MySqlParameter> m_ParameterList;

        public FlowCommentCollection()
        {
            this.m_Cmd = new MySqlCommand();
            this.m_ParameterList = new List<MySqlParameter>();
        }

        public void SetFillCommandAll()
        {
            string sql = "select * from tblFlowCommentV2";
            this.m_ParameterList.Clear();
            this.m_Cmd.CommandText = sql;
            this.m_Cmd.CommandType = CommandType.Text;
        }

        public void SetFillCommandByStainId(int commentId)
        {
            string sql = "select * from tblFlowCommentV2 where CommentId = " + commentId;            
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
                        FlowCommentItem item = new FlowCommentItem();
                        item.Fill(dr);
                        this.Add(item);
                    }
                }
            }
        }

        public void Save(bool releaseLock)
        {
            foreach (FlowCommentItem item in this)
            {
                item.Save(releaseLock);
            }
        }

        public void Insert()
        {
         
        }
        
        public void Delete()
        {

        }
    }    
}
				
