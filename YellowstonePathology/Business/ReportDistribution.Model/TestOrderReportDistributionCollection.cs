using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Xml.Linq;

namespace YellowstonePathology.Business.ReportDistribution.Model
{
	public class TestOrderReportDistributionCollection : ObservableCollection<TestOrderReportDistribution>
	{        
        public TestOrderReportDistributionCollection()
        {
            
		}

        public void RemoveDeleted(IEnumerable<XElement> elements)
        {
            for (int i = this.Count - 1; i > -1; i--)
            {
                bool found = false;
                foreach (XElement element in elements)
                {
                    string testOrderReportDistributionId = element.Element("TestOrderReportDistributionId").Value;
                    if (this[i].TestOrderReportDistributionId == testOrderReportDistributionId)
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

        public void ScheduleDistribution(DateTime timeToSchedule)
        {
            foreach (TestOrderReportDistribution testOrderReportDistribution in this)
            {
                testOrderReportDistribution.Distributed = false;
                testOrderReportDistribution.ScheduledDistributionTime = timeToSchedule;
            }
        }

        public void UnscheduleDistributions()
        {
            foreach (TestOrderReportDistribution testOrderReportDistribution in this)
            {
                testOrderReportDistribution.Distributed = false;
                testOrderReportDistribution.ScheduledDistributionTime = null;
            }
        }

		public void AddNext(string testOrderReportDistributionId, string objectId, string reportNo, int physicianId, string physicianName, int clientId, string clientName, string distributionType)
        {
            this.AddNext(testOrderReportDistributionId, objectId, reportNo, physicianId, physicianName, clientId, clientName, distributionType, null, false);
        }

		public void AddNext(string testOrderReportDistributionId, string objectId, string reportNo, int physicianId, string physicianName, int clientId, string clientName, string distributionType, string faxNumber, bool longDistance)
        {            
            YellowstonePathology.Business.ReportDistribution.Model.TestOrderReportDistribution testOrderReportDistribution = new TestOrderReportDistribution(testOrderReportDistributionId, objectId, reportNo,
                physicianId, physicianName, clientId, clientName, distributionType, faxNumber, longDistance);
            this.Add(testOrderReportDistribution);
        }

		public bool Exists(int physicianId, int clientId, string distributionType)
        {
            bool result = false;
            foreach (TestOrderReportDistribution testOrderReportDistribution in this)
            {
                if (testOrderReportDistribution.PhysicianId == physicianId && 
                    testOrderReportDistribution.ClientId == clientId && 
                    testOrderReportDistribution.DistributionType == distributionType)
                {
                    result = true;
                    break;
                }
            }
            return result;
        }

        public TestOrderReportDistribution Get(string testOrderReportDistributionId)
        {
            TestOrderReportDistribution result = null;
            foreach (TestOrderReportDistribution testOrderReportDistribution in this)
            {
                if (testOrderReportDistribution.TestOrderReportDistributionId == testOrderReportDistributionId)
                {
                    result = testOrderReportDistribution;
                    break;
                }
            }
            return result;
        }

        public bool DistributionTypeExists(string distributionType)
        {
            bool result = false;
            foreach (TestOrderReportDistribution testOrderReportDistribution in this)
            {
                if (testOrderReportDistribution.DistributionType == distributionType)
                {
                    result = true;
                    break;
                }
            }
            return result;
        }

        public bool Exists(string testOrderReportDistributionId)
        {
            bool result = false;
            foreach (TestOrderReportDistribution testOrderReportDistribution in this)
            {
                if (testOrderReportDistribution.TestOrderReportDistributionId == testOrderReportDistributionId)
                {
                    result = true;
                    break;
                }
            }
            return result;
        }

        public bool HasDistribution(string distributionType)
        {
            bool result = false;
            foreach (TestOrderReportDistribution testOrderReportDistribution in this)
            {
                if (testOrderReportDistribution.DistributionType == distributionType)
                {
                    result = true;
                    break;
                }
            }
            return result;
        }

        public bool HasDistributionAfter(DateTime dateTime)
        {
            bool result = false;
            foreach (TestOrderReportDistribution testOrderReportDistribution in this)
            {
                if (testOrderReportDistribution.TimeOfLastDistribution > dateTime)
                {
                    result = true;
                    break;
                }
            }
            return result;
        }

        public bool HasDistributedItems()
        {
            bool result = false;
            foreach (TestOrderReportDistribution testOrderReportDistribution in this)
            {
                if (testOrderReportDistribution.Distributed == true)
                {
                    result = true;
                    break;
                }
            }
            return result;
        }

        public bool HasDistributedItemsAfter(DateTime afterDate)
        {
            bool result = false;
            foreach (TestOrderReportDistribution testOrderReportDistribution in this)
            {
                if (testOrderReportDistribution.Distributed == true)
                {
                    if (testOrderReportDistribution.TimeOfLastDistribution >= afterDate)
                    {
                        result = true;
                        break;
                    }
                }
            }
            return result;
        }

        public bool HasUndistributedItems()
        {
            bool result = false;
            foreach (TestOrderReportDistribution testOrderReportDistribution in this)
            {
                if (testOrderReportDistribution.Distributed == false)
                {
                    result = true;
                    break;
                }
            }
            return result;
        }

        public void MarkAllAsNotDistributed()
        {            
            foreach (TestOrderReportDistribution testOrderReportDistribution in this)
            {
                testOrderReportDistribution.Distributed = false;
            }         
        }

        public void UnscheduleAll()
        {
            foreach (TestOrderReportDistribution testOrderReportDistribution in this)
            {
                testOrderReportDistribution.ScheduledDistributionTime = null;
            }         
        }

        public void Sync(DataTable dataTable, string reportNo)
        {
            this.RemoveDeleted(dataTable);
            DataTableReader dataTableReader = new DataTableReader(dataTable);
            while (dataTableReader.Read())
            {
                string testOrderReportDistributionId = dataTableReader["TestOrderReportDistributionId"].ToString();
                string distributionReportNo = dataTableReader["ReportNo"].ToString();

                TestOrderReportDistribution testOrderReportDistribution = null;

                if (this.Exists(testOrderReportDistributionId) == true)
                {
                    testOrderReportDistribution = this.Get(testOrderReportDistributionId);
                }
                else if (reportNo == distributionReportNo)
                {
                    testOrderReportDistribution = new TestOrderReportDistribution();
                    this.Add(testOrderReportDistribution);
                }

                if (testOrderReportDistribution != null)
                {
                    YellowstonePathology.Business.Persistence.SqlDataTableReaderPropertyWriter sqlDataTableReaderPropertyWriter = new Persistence.SqlDataTableReaderPropertyWriter(testOrderReportDistribution, dataTableReader);
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
                    string testOrderReportDistributionId = dataTable.Rows[idx]["TestOrderReportDistributionId"].ToString();
                    if (this[i].TestOrderReportDistributionId == testOrderReportDistributionId)
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
