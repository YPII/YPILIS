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
            this.AddNext(testOrderReportDistributionId, objectId, reportNo, physicianId, physicianName, clientId, clientName, distributionType, null);
        }

		public void AddNext(string testOrderReportDistributionId, string objectId, string reportNo, int physicianId, string physicianName, int clientId, string clientName, string distributionType, string faxNumber)
        {            
            YellowstonePathology.Business.ReportDistribution.Model.TestOrderReportDistribution testOrderReportDistribution = new TestOrderReportDistribution(testOrderReportDistributionId, objectId, reportNo,
                physicianId, physicianName, clientId, clientName, distributionType, faxNumber);
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

        public TestOrderReportDistribution GetFirstEclinical()
        {
            TestOrderReportDistribution result = null;
            foreach (TestOrderReportDistribution testOrderReportDistribution in this)
            {
                if (testOrderReportDistribution.DistributionType == "Eclinical Works")
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

        /*public YellowstonePathology.Business.Rules.MethodResult AreDistributionTypesHandled(YellowstonePathology.Business.PanelSet.Model.PanelSet panelSet)
        {
            YellowstonePathology.Business.Rules.MethodResult result = new Rules.MethodResult();
            result.Success = true;

            foreach (TestOrderReportDistribution testOrderReportDistribution in this)
            {
                switch (testOrderReportDistribution.DistributionType)
                {
                    case YellowstonePathology.Business.ReportDistribution.Model.DistributionType.EPIC:
                        if (panelSet.ImplementedResultTypes.Contains(YellowstonePathology.Business.Test.ResultType.EPIC) == false)
                        {
                            result.Success = false;
                        }
                        break;
                    case YellowstonePathology.Business.ReportDistribution.Model.DistributionType.MEDITECH:
                        if (panelSet.ImplementedResultTypes.Contains(YellowstonePathology.Business.Test.ResultType.WPH) == false)
                        {
                            result.Success = false;
                        }
                        break;
                    case YellowstonePathology.Business.ReportDistribution.Model.DistributionType.ATHENA:
                        if (panelSet.ImplementedResultTypes.Contains(YellowstonePathology.Business.Test.ResultType.CMMC) == false)
                        {
                            result.Success = false;
                        }
                        break;
                    case YellowstonePathology.Business.ReportDistribution.Model.DistributionType.ECW:
                        if (panelSet.ImplementedResultTypes.Contains(YellowstonePathology.Business.Test.ResultType.ECW) == false)
                        {
                            result.Success = false;
                        }
                        break;
                }

                if(result.Success == false)
                {
                    result.Message = "Report No " + testOrderReportDistribution.ReportNo + " Distribution Type " + 
                        testOrderReportDistribution.DistributionType + " not implemented for " + panelSet.PanelSetName + 
                        " - id " + panelSet.PanelSetId.ToString();
                    break;
                }
            }

            return result;
        }*/

        public bool IsDistributionTypeImplemented(YellowstonePathology.Business.PanelSet.Model.PanelSet panelSet, string distributionType)
        {
            bool result = true;

            switch (distributionType)
            {
                case YellowstonePathology.Business.ReportDistribution.Model.DistributionType.EPIC:
                    if (panelSet.ImplementedResultTypes.Contains(YellowstonePathology.Business.Test.ResultType.EPIC) == false)
                    {
                        result = false;
                    }
                    break;
                case YellowstonePathology.Business.ReportDistribution.Model.DistributionType.EPICANDFAX:
                    if (panelSet.ImplementedResultTypes.Contains(YellowstonePathology.Business.Test.ResultType.EPIC) == false)
                    {
                        result = false;
                    }
                    break;
                case YellowstonePathology.Business.ReportDistribution.Model.DistributionType.MEDITECH:
                    if (panelSet.ImplementedResultTypes.Contains(YellowstonePathology.Business.Test.ResultType.WPH) == false)
                    {
                        result = false;
                    }
                    break;
                case YellowstonePathology.Business.ReportDistribution.Model.DistributionType.ATHENA:
                    if (panelSet.ImplementedResultTypes.Contains(YellowstonePathology.Business.Test.ResultType.CMMC) == false)
                    {
                        result = false;
                    }
                    break;
                case YellowstonePathology.Business.ReportDistribution.Model.DistributionType.ECW:
                    if (panelSet.ImplementedResultTypes.Contains(YellowstonePathology.Business.Test.ResultType.ECW) == false)
                    {
                        result = false;
                    }
                    break;
                case YellowstonePathology.Business.ReportDistribution.Model.DistributionType.MTDOH:
                    if (panelSet.ImplementedResultTypes.Contains(YellowstonePathology.Business.Test.ResultType.MDOH) == false)
                    {
                        result = false;
                    }
                    break;
                case YellowstonePathology.Business.ReportDistribution.Model.DistributionType.WYDOH:
                    if (panelSet.ImplementedResultTypes.Contains(YellowstonePathology.Business.Test.ResultType.WDOH) == false)
                    {
                        result = false;
                    }
                    break;
            }

            return result;
        }

        public bool IsDuplicate(TestOrderReportDistribution testOrderReportDistribution)
        {
            bool result = false;
            foreach (TestOrderReportDistribution compareTestOrderReportDistribution in this)
            {
                if (testOrderReportDistribution.ClientId == compareTestOrderReportDistribution.ClientId &&
                    testOrderReportDistribution.ClientName == compareTestOrderReportDistribution.ClientName &&
                    testOrderReportDistribution.DistributionType == compareTestOrderReportDistribution.DistributionType &&
                    testOrderReportDistribution.PhysicianId == compareTestOrderReportDistribution.PhysicianId &&
                    testOrderReportDistribution.PhysicianName == compareTestOrderReportDistribution.PhysicianName)
                {
                    result = true;
                    break;
                }
            }

            return result;
        }

        public void SetDistributionFromUnique(Test.PanelSetOrder panelSetOrder, TestOrderReportDistributionCollection uniqueDistributions)
        {
            Business.PanelSet.Model.PanelSet panelSet = Business.PanelSet.Model.PanelSetCollection.GetAll().GetPanelSet(panelSetOrder.PanelSetId);
            foreach (YellowstonePathology.Business.ReportDistribution.Model.TestOrderReportDistribution testOrderReportDistribution in uniqueDistributions)
            {
                if (this.IsDuplicate(testOrderReportDistribution) == false)
                {
                    if (this.IsDistributionTypeImplemented(panelSet, testOrderReportDistribution.DistributionType) == true)
                    {
                        string testOrderReportDistributionId = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
                        this.AddNext(testOrderReportDistributionId, testOrderReportDistributionId, panelSetOrder.ReportNo, testOrderReportDistribution.PhysicianId, testOrderReportDistribution.PhysicianName,
                                testOrderReportDistribution.ClientId, testOrderReportDistribution.ClientName, testOrderReportDistribution.DistributionType, testOrderReportDistribution.FaxNumber);
                    }
                }
            }
        }
    }
}
