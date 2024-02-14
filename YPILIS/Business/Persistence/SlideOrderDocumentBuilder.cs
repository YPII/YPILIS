using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;

namespace YellowstonePathology.Business.Persistence
{
    public class SlideOrderDocumentBuilder : DocumentBuilder
    {
        MySqlCommand m_SQLCommand;

        public SlideOrderDocumentBuilder(string slideOrderId)
        {
            this.m_SQLCommand = new MySqlCommand("select * from tblSlideOrder where SlideOrderId = @SlideOrderId;");
            this.m_SQLCommand.CommandType = CommandType.Text;
            this.m_SQLCommand.Parameters.AddWithValue("@SlideOrderId", slideOrderId);
        }

        public override object BuildNew()
        {
            YellowstonePathology.Business.Slide.Model.SlideOrder slideOrder = new Slide.Model.SlideOrder();
            this.Build(slideOrder);
            return slideOrder;
        }

        private void Build(YellowstonePathology.Business.Slide.Model.SlideOrder slideOrder)
        {
            using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                this.m_SQLCommand.Connection = cn;
                using (MySqlDataReader dr = this.m_SQLCommand.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(slideOrder, dr);
                        sqlDataReaderPropertyWriter.WriteProperties();
                    }
                }
            }
        }
    }
}
