using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace YellowstonePathology.Business.Persistence
{
    public class TaskOrderDocumentBuilder : DocumentBuilder
    {
        MySqlCommand m_SQLCommand;

        public TaskOrderDocumentBuilder(string taskOrderId)
        {
            this.m_SQLCommand = new MySqlCommand("select * from tblTaskOrder where TaskOrderId = @TaskOrderId; " +
                "select * from tblTaskOrderDetail where TaskOrderId = @TaskOrderId;");
            this.m_SQLCommand.CommandType = CommandType.Text;
            this.m_SQLCommand.Parameters.AddWithValue("@TaskOrderId", taskOrderId);
        }

        public override object BuildNew()
        {
            YellowstonePathology.Business.Task.Model.TaskOrder taskOrder = new Task.Model.TaskOrder();
            this.Build(taskOrder);
            return taskOrder;
        }


        private void Build(YellowstonePathology.Business.Task.Model.TaskOrder taskOrder)
        {
            using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                this.m_SQLCommand.Connection = cn;
                using (MySqlDataReader dr = this.m_SQLCommand.ExecuteReader(CommandBehavior.KeyInfo))
                {
                    while (dr.Read())
                    {
                        Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(taskOrder, dr);
                        sqlDataReaderPropertyWriter.WriteProperties();
                    }

                    dr.NextResult();
                    while (dr.Read())
                    {
                        YellowstonePathology.Business.Task.Model.TaskOrderDetail taskOrderDetail = new Task.Model.TaskOrderDetail();
                        Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(taskOrderDetail, dr);
                        sqlDataReaderPropertyWriter.WriteProperties();
                        taskOrder.TaskOrderDetailCollection.Add(taskOrderDetail);
                    }
                }
            }
        }
    }
}
