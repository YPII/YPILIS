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
    }
}
