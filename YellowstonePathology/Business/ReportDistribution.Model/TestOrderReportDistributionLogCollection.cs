using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Data;
using System.Xml.Serialization;
using System.Xml.Linq;

namespace YellowstonePathology.Business.ReportDistribution.Model
{
	public class TestOrderReportDistributionLogCollection : ObservableCollection<TestOrderReportDistributionLog>
	{     
        public const string PREFIXID = "TORD";

        public TestOrderReportDistributionLogCollection()
        {

		}

        public void RemoveDeleted(IEnumerable<XElement> elements)
        {
            for (int i = this.Count - 1; i > -1; i--)
            {
                bool found = false;
                foreach (XElement element in elements)
                {
                    string testOrderReportDistributionLogId = element.Element("TestOrderReportDistributionLogId").Value;
                    if (this[i].TestOrderReportDistributionLogId == testOrderReportDistributionLogId)
                    {
                        found = true;
                        break;
                    }
                }
                if (found == false)
                {
                    this.RemoveItem(i);
                }
            }
        }

        public bool Exists(string testOrderReportDistributionLogId)
        {
            bool result = false;
            foreach (TestOrderReportDistributionLog item in this)
            {
                if(item.TestOrderReportDistributionLogId == testOrderReportDistributionLogId)
                {
                    result = true;
                    break;    
                }
            }
            return result;
        }

        public TestOrderReportDistributionLog Get(string testOrderReportDistributionLogId)
        {
            TestOrderReportDistributionLog result = null;
            foreach (TestOrderReportDistributionLog item in this)
            {
                if (item.TestOrderReportDistributionLogId == testOrderReportDistributionLogId)
                {
                    result = item;
                    break;
                }
            }
            return result;
        }

        public void Sync(DataTable dataTable, string reportNo)
        {
            this.RemoveDeleted(dataTable);
            DataTableReader dataTableReader = new DataTableReader(dataTable);
            while (dataTableReader.Read())
            {
                string testOrderReportDistributionLogId = dataTableReader["TestOrderReportDistributionLogId"].ToString();
                string distributionReportNo = dataTableReader["ReportNo"].ToString();

                TestOrderReportDistributionLog testOrderReportDistributionLog = null;

                if (this.Exists(testOrderReportDistributionLogId) == true)
                {
                    testOrderReportDistributionLog = this.Get(testOrderReportDistributionLogId);
                }
                else if (reportNo == distributionReportNo)
                {
                    testOrderReportDistributionLog = new TestOrderReportDistributionLog();
                    this.Add(testOrderReportDistributionLog);
                }

                if (testOrderReportDistributionLog != null)
                {
                    YellowstonePathology.Business.Persistence.SqlDataTableReaderPropertyWriter sqlDataTableReaderPropertyWriter = new Persistence.SqlDataTableReaderPropertyWriter(testOrderReportDistributionLog, dataTableReader);
                    sqlDataTableReaderPropertyWriter.WriteProperties();
                }
            }
        }

        public void RemoveDeleted(DataTable dataTable)
        {
            for (int i = this.Count - 1; i > -1; i--)
            {
                bool found = false;
                for (int idx = 0; idx < dataTable.Rows.Count; idx++)
                {
                    string testOrderReportDistributionLogId = dataTable.Rows[idx]["TestOrderReportDistributionLogId"].ToString();
                    if (this[i].TestOrderReportDistributionLogId == testOrderReportDistributionLogId)
                    {
                        found = true;
                        break;
                    }
                }
                if (found == false)
                {
                    this.RemoveItem(i);
                }
            }
        }
    }
}
