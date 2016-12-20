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
            //this.m_SQLCommand = new MySqlCommand("select tsk.*, ( select tskd.* from tblTaskOrderDetail tskd where tskd.TaskOrderId = tsk.TaskOrderId " +
            //    "for xml Path('TaskOrderDetail'), type) [TaskOrderDetailCollection] " +
            //    "from tblTaskOrder tsk where tsk.TaskOrderId = @TaskOrderId  for xml Path('TaskOrder')");

            this.m_SQLCommand = new MySqlCommand("select * from tblTaskOrder where TaskOrderId = @TaskOrderId " +
                "select * from tblTaskOrderDetail where TaskOrderId = @TaskOrderId");
            this.m_SQLCommand.CommandType = CommandType.Text;
            this.m_SQLCommand.Parameters.Add("@TaskOrderId", SqlDbType.VarChar).Value = taskOrderId;
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

        /*private void Build(YellowstonePathology.Business.Task.Model.TaskOrder taskOrder)
        {
            XElement documentElement = new XElement("Document");

            using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                this.m_SQLCommand.Connection = cn;
                using (XmlReader xmlReader = this.m_SQLCommand.ExecuteXmlReader())
                {
                    if (xmlReader.Read() == true)
                    {
                        documentElement = XElement.Load(xmlReader);
                    }
                }
            }

            YellowstonePathology.Business.Persistence.XmlPropertyWriter taskOrderWriter = new YellowstonePathology.Business.Persistence.XmlPropertyWriter(documentElement, taskOrder);
            taskOrderWriter.Write();

            List<XElement> taskOrderDetailElements = (from item in documentElement.Elements("TaskOrderDetailCollection") select item).ToList<XElement>();
            foreach (XElement taskOrderDetailElement in taskOrderDetailElements.Elements("TaskOrderDetail"))
            {
                YellowstonePathology.Business.Task.Model.TaskOrderDetail taskOrderDetail = new YellowstonePathology.Business.Task.Model.TaskOrderDetail();
                YellowstonePathology.Business.Persistence.XmlPropertyWriter taskOrderDetailWriter = new YellowstonePathology.Business.Persistence.XmlPropertyWriter(taskOrderDetailElement, taskOrderDetail);
                taskOrderDetailWriter.Write();
                taskOrder.TaskOrderDetailCollection.Add(taskOrderDetail);
            }
        }*/
    }
}
