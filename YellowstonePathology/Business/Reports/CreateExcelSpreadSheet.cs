using System;
using System.Xml;
using System.Data;
using MySql.Data.MySqlClient;

namespace YellowstonePathology.Business.Reports
{
    public class CreateExcelSpreadSheet
    {

        XmlTextWriter m_Writer;

        public CreateExcelSpreadSheet()
        {
            
        }

        public void StartFile()
        {
            this.m_Writer = new XmlTextWriter("c:\\Test.xml", null);
            this.m_Writer.Formatting = Formatting.Indented;            
            this.m_Writer.WriteRaw("<?xml version=\"1.0\"?>");
            this.m_Writer.WriteRaw("<?mso-application progid=\"Excel.Sheet\"?>");
            this.m_Writer.WriteStartElement("Workbook");
            this.m_Writer.WriteAttributeString("xmlns", "urn:schemas-microsoft-com:office:spreadsheet");
            this.m_Writer.WriteAttributeString("xmlns:o", "urn:schemas-microsoft-com:office:office");
            this.m_Writer.WriteAttributeString("xmlns:x", "urn:schemas-microsoft-com:office:excel");
            this.m_Writer.WriteAttributeString("xmlns:ss", "urn:schemas-microsoft-com:office:spreadsheet");
            this.m_Writer.WriteAttributeString("xmlns:html", "http://www.w3.org/TR/REC-html40");                        
        }        

        public void SetSheet(DateTime startDate, DateTime endDate)
        {
            this.m_Writer.WriteStartElement("Worksheet");
            string sheetName = startDate.Month.ToString().PadLeft(2, '0') + startDate.Day.ToString().PadLeft(2, '0') + startDate.Year.ToString()
                + endDate.Month.ToString().PadLeft(2, '0') + endDate.Day.ToString().PadLeft(2, '0') + endDate.Year.ToString();
            this.m_Writer.WriteAttributeString("ss:Name", sheetName);                
            this.m_Writer.WriteStartElement("Table");                              

            using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "prcGetBillingClientReportData";
                cmd.Parameters.AddWithValue("@StartDate", startDate.ToShortDateString());
                cmd.Parameters.AddWithValue("@EndDate", endDate.ToShortDateString());

                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    //Write column headers
                    this.m_Writer.WriteStartElement("Row");
                    for (int i = 0; i < dr.FieldCount; i++)
                    {
                        this.m_Writer.WriteStartElement("Cell");
                            this.m_Writer.WriteStartElement("Data");
                                this.m_Writer.WriteAttributeString("ss:Type", "String");
                                this.m_Writer.WriteString(dr.GetName(i));
                            this.m_Writer.WriteEndElement();
                        this.m_Writer.WriteEndElement();
                    }
                    this.m_Writer.WriteEndElement();

                    //Write Data
                    while (dr.Read())
                    {
                        this.m_Writer.WriteStartElement("Row");
                        for (int i = 0; i < dr.FieldCount; i++)
                        {
                            this.m_Writer.WriteStartElement("Cell");
                                this.m_Writer.WriteStartElement("Data");
                                    this.m_Writer.WriteAttributeString("ss:Type", "String");
                                    this.m_Writer.WriteString(BaseData.GetStringValue(dr.GetName(i), dr));                                    
                                this.m_Writer.WriteEndElement();
                            this.m_Writer.WriteEndElement();
                        }
                        this.m_Writer.WriteEndElement();
                    }
                }
            }
            this.m_Writer.WriteEndElement();
            this.m_Writer.WriteEndElement();
        }        

        public void EndFile()
        {
            this.m_Writer.WriteEndElement();
            this.m_Writer.Close();             
        }        
    }
}
