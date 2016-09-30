using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Data;
using System.Data.SqlClient;

namespace YellowstonePathology.Business.Flow
{
    public partial class FlowCommentCollection : ObservableCollection<FlowCommentItem>
    {
        protected SqlCommand m_Cmd;
        protected List<SqlParameter> m_ParameterList;

        public FlowCommentCollection()
        {
            this.m_Cmd = new SqlCommand();
            this.m_ParameterList = new List<SqlParameter>();
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
            using (SqlConnection cn = new SqlConnection(BaseData.SqlConnectionString))
            {
                cn.Open();
                this.m_Cmd.Connection = cn;
                using (SqlDataReader dr = m_Cmd.ExecuteReader())
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
				
