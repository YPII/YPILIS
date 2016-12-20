using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace YellowstonePathology.Business.Persistence
{
    class TypingShortcutDocumentBuilder : DocumentBuilder
    {
        MySqlCommand m_SQLCommand;

        public TypingShortcutDocumentBuilder(string objectId)
        {
            this.m_SQLCommand = new MySqlCommand();
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
            using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                m_SQLCommand.Connection = cn;

                using (MySqlDataReader dr = m_SQLCommand.ExecuteReader())
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

