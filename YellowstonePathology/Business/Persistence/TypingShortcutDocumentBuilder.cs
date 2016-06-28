using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Xml;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Persistence
{
    class TypingShortcutDocumentBuilder : DocumentBuilder
    {
        SqlCommand m_SQLCommand;

        public TypingShortcutDocumentBuilder(string objectId)
        {
            this.m_SQLCommand = new SqlCommand();
            this.m_SQLCommand.CommandText = "select * From tblTypingShortcut where ObjectId = @ObjectId";
            this.m_SQLCommand.CommandType = CommandType.Text;
            this.m_SQLCommand.Parameters.Add("@ObjectId", SqlDbType.VarChar).Value = objectId;
        }

        public override object BuildNew()
        {
            YellowstonePathology.Business.Typing.TypingShortcut typingShortcut = new Typing.TypingShortcut();
            this.BuildTypingShortcut(typingShortcut);
            return typingShortcut;
        }

        private void BuildTypingShortcut(YellowstonePathology.Business.Typing.TypingShortcut typingShortcut)
        {
            using (SqlConnection cn = new SqlConnection(Properties.Settings.Default.ProductionConnectionString))
            {
                cn.Open();
                m_SQLCommand.Connection = cn;

                using (SqlDataReader dr = m_SQLCommand.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(typingShortcut, dr);
                        sqlDataReaderPropertyWriter.WriteProperties();
                    }
                }
            }
        }
    }
}

