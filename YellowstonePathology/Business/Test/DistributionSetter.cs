using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Test
{
    public class DistributionSetter
    {
        private YellowstonePathology.Business.Test.PanelSetOrder m_PanelSetOrder;
        private int m_PhysicianId;
        private string m_PhysicianName;
        private int m_ClientId;
        private string m_ClientName;
        private string m_DistributionTypeToCheck;
        private string m_FaxNumber;
        private string m_Account;
        private string m_MedicalRecord;
        private bool m_CanSetDistribution;

        public DistributionSetter(YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder, int physicianId, string physicianName, 
            int clientId, string clientName, string distributionType, string faxNumber, string accountNo, string medicalRecordNo)
        {
            this.m_PanelSetOrder = panelSetOrder;
            this.m_PhysicianId = physicianId;
            this.m_PhysicianName = physicianName;
            this.m_ClientId = clientId;
            this.m_ClientName = clientName;
            this.m_DistributionTypeToCheck = distributionType;
            this.m_FaxNumber = faxNumber;
            this.m_Account = accountNo;
            this.m_MedicalRecord = medicalRecordNo;
        }

        public List<ReportDistribution.Model.TestOrderReportDistribution> GetDistributionResult()
        {
            Business.PanelSet.Model.PanelSet panelSet = Business.PanelSet.Model.PanelSetCollection.GetAll().GetPanelSet(this.m_PanelSetOrder.PanelSetId);
            CanSetDistributionResult canSetDistributionResult = new CanSetDistributionResult(panelSet, this.m_DistributionTypeToCheck);
            ResultType.GetImplementedDistributionType(canSetDistributionResult);
            this.m_CanSetDistribution = canSetDistributionResult.CanSetProvidedDistributionType;
            List<ReportDistribution.Model.TestOrderReportDistribution> result = this.GetDistributionResult(canSetDistributionResult);
            return result;
        }

        public bool CanSetDistribution
        {
            get { return this.m_CanSetDistribution; }
        }

        private List<ReportDistribution.Model.TestOrderReportDistribution> GetDistributionResult(CanSetDistributionResult canSetDistributionResult)
        {
            List<ReportDistribution.Model.TestOrderReportDistribution> result = new List<ReportDistribution.Model.TestOrderReportDistribution>();

            switch (canSetDistributionResult.DistributionTypeToSet)
            {
                case YellowstonePathology.Business.ReportDistribution.Model.DistributionType.EPIC:
                    result.Add(this.HandleAddEPICDistribution());
                    break;
                case YellowstonePathology.Business.ReportDistribution.Model.DistributionType.EPICANDFAX:
                    result.Add(this.HandleAddEPICDistribution());
                    result.Add(this.HandleAddFaxDistribution());
                    break;
                case YellowstonePathology.Business.ReportDistribution.Model.DistributionType.FAX:
                    result.Add(this.HandleAddFaxDistribution());
                    break;
                case YellowstonePathology.Business.ReportDistribution.Model.DistributionType.WEBSERVICE:
                    result.Add(this.HandleAddWebServiceDistribution());
                    break;
                case YellowstonePathology.Business.ReportDistribution.Model.DistributionType.WEBSERVICEANDFAX:
                    result.Add(this.HandleAddWebServiceDistribution());
                    result.Add(this.HandleAddFaxDistribution());
                    break;
                case YellowstonePathology.Business.ReportDistribution.Model.DistributionType.PRINT:
                    result.Add(this.GetTestOrderReportDistribution(YellowstonePathology.Business.ReportDistribution.Model.DistributionType.PRINT));
                    break;
                case YellowstonePathology.Business.ReportDistribution.Model.DistributionType.MTDOH:
                    result.Add(this.HandleAddMTDOHDistribution());
                    break;
                case YellowstonePathology.Business.ReportDistribution.Model.DistributionType.WYDOH:
                    result.Add(this.HandleAddWYDOHDistribution());
                    break;
                case YellowstonePathology.Business.ReportDistribution.Model.DistributionType.ECW:
                    result.Add(this.HandleAddECWDistribution());
                    break;
                case YellowstonePathology.Business.ReportDistribution.Model.DistributionType.ATHENA:
                    result.Add(this.HandleAddAthenaDistribution());
                    break;
                case YellowstonePathology.Business.ReportDistribution.Model.DistributionType.MEDITECH:
                    result.Add(this.HandleAddMEDITECHDistribution());
                    break;
                default:
                    throw new Exception("Not implemented");
            }

            return result;
        }

        private ReportDistribution.Model.TestOrderReportDistribution GetTestOrderReportDistribution(string distributionType)
        {
            string testOrderReportDistributionId = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
            YellowstonePathology.Business.ReportDistribution.Model.TestOrderReportDistribution result =
                new YellowstonePathology.Business.ReportDistribution.Model.TestOrderReportDistribution(testOrderReportDistributionId, 
                testOrderReportDistributionId, this.m_PanelSetOrder.ReportNo, this.m_PhysicianId, this.m_PhysicianName,
                this.m_ClientId, this.m_ClientName, distributionType, this.m_FaxNumber);
            return result;
        }

        private ReportDistribution.Model.TestOrderReportDistribution HandleAddFaxDistribution()
        {
            ReportDistribution.Model.TestOrderReportDistribution result = null;
            if (string.IsNullOrEmpty(this.m_FaxNumber) == false)
            {
                if (this.m_PanelSetOrder.TestOrderReportDistributionCollection.Exists(this.m_PhysicianId, this.m_ClientId, YellowstonePathology.Business.ReportDistribution.Model.DistributionType.FAX) == false)
                {
                    result = this.GetTestOrderReportDistribution(YellowstonePathology.Business.ReportDistribution.Model.DistributionType.FAX);
                }
            }
            return result;
        }

        private ReportDistribution.Model.TestOrderReportDistribution HandleAddEPICDistribution()
        {
            ReportDistribution.Model.TestOrderReportDistribution result = null;
            if (this.m_PanelSetOrder.TestOrderReportDistributionCollection.DistributionTypeExists(YellowstonePathology.Business.ReportDistribution.Model.DistributionType.EPIC) == false)
            {
                result = this.GetTestOrderReportDistribution(ReportDistribution.Model.DistributionType.EPIC);
            }
            else
            {
                result = this.HandleAddFaxDistribution();
            }
            return result;
        }

        private ReportDistribution.Model.TestOrderReportDistribution HandleAddWebServiceDistribution()
        {
            ReportDistribution.Model.TestOrderReportDistribution result = null;
            if (this.m_PanelSetOrder.TestOrderReportDistributionCollection.Exists(this.m_PhysicianId, this.m_ClientId, YellowstonePathology.Business.ReportDistribution.Model.DistributionType.WEBSERVICE) == false)
            {
                result = this.GetTestOrderReportDistribution(YellowstonePathology.Business.ReportDistribution.Model.DistributionType.WEBSERVICE);
            }
            return result;
        }

        private ReportDistribution.Model.TestOrderReportDistribution HandleAddMTDOHDistribution()
        {
            ReportDistribution.Model.TestOrderReportDistribution result = null;
            if (this.m_PanelSetOrder.PanelSetId == 13)
            {
                if (this.m_PanelSetOrder.TestOrderReportDistributionCollection.Exists(this.m_PhysicianId, this.m_ClientId, YellowstonePathology.Business.ReportDistribution.Model.DistributionType.MTDOH) == false)
                {
                    result = this.GetTestOrderReportDistribution(YellowstonePathology.Business.ReportDistribution.Model.DistributionType.MTDOH);
                }
            }
            return result;
        }

        private ReportDistribution.Model.TestOrderReportDistribution HandleAddWYDOHDistribution()
        {
            ReportDistribution.Model.TestOrderReportDistribution result = null;
            if (this.m_PanelSetOrder.PanelSetId == 13)
            {
                if (this.m_PanelSetOrder.TestOrderReportDistributionCollection.Exists(this.m_PhysicianId, this.m_ClientId, YellowstonePathology.Business.ReportDistribution.Model.DistributionType.WYDOH) == false)
                {
                    result = this.GetTestOrderReportDistribution(YellowstonePathology.Business.ReportDistribution.Model.DistributionType.WYDOH);
                }
            }
            return result;
        }

        private ReportDistribution.Model.TestOrderReportDistribution HandleAddAthenaDistribution()
        {
            ReportDistribution.Model.TestOrderReportDistribution result = null;
            if (this.m_PanelSetOrder.TestOrderReportDistributionCollection.DistributionTypeExists(YellowstonePathology.Business.ReportDistribution.Model.DistributionType.ATHENA) == false)
            {
                result = this.GetTestOrderReportDistribution(YellowstonePathology.Business.ReportDistribution.Model.DistributionType.ATHENA);
            }
            else
            {
                result = this.HandleAddFaxDistribution();
            }
            return result;
        }

        private ReportDistribution.Model.TestOrderReportDistribution HandleAddECWDistribution()
        {
            ReportDistribution.Model.TestOrderReportDistribution result = null;
            if (this.m_PanelSetOrder.TestOrderReportDistributionCollection.DistributionTypeExists(YellowstonePathology.Business.ReportDistribution.Model.DistributionType.ECW) == false)
            {
                result = this.GetTestOrderReportDistribution(YellowstonePathology.Business.ReportDistribution.Model.DistributionType.ECW);
            }
            else
            {
                result = this.HandleAddFaxDistribution();
            }
            return result;
        }

        private ReportDistribution.Model.TestOrderReportDistribution HandleAddMEDITECHDistribution()
        {
            ReportDistribution.Model.TestOrderReportDistribution result = null;
            if (this.m_PanelSetOrder.TestOrderReportDistributionCollection.DistributionTypeExists(YellowstonePathology.Business.ReportDistribution.Model.DistributionType.MEDITECH) == false)
            {
                if (string.IsNullOrEmpty(this.m_Account) == true || string.IsNullOrEmpty(this.m_MedicalRecord) == true)
                {
                    result = this.HandleAddFaxDistribution();
                }
                else
                {
                    result = this.GetTestOrderReportDistribution(YellowstonePathology.Business.ReportDistribution.Model.DistributionType.MEDITECH);
                }
            }
            else
            {
                result = this.HandleAddFaxDistribution();
            }
            return result;
        }
    }
}
