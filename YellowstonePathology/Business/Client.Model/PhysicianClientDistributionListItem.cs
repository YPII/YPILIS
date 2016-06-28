using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YellowstonePathology.Business.Persistence;
using System.Text.RegularExpressions;

namespace YellowstonePathology.Business.Client.Model
{
    public class PhysicianClientDistributionListItem
    {
		protected int m_PhysicianId;
        protected string m_PhysicianName;
        protected int m_ClientId;
        protected string m_ClientName;
        protected string m_DistributionType;
        protected string m_FaxNumber;
        protected bool m_LongDistance;        

        public PhysicianClientDistributionListItem()
        {

        }

        public virtual void From(PhysicianClientDistributionListItem physicianClientDistribution)
        {
            this.m_ClientId = physicianClientDistribution.m_ClientId;
            this.m_ClientName = physicianClientDistribution.m_ClientName;
            this.m_PhysicianId = physicianClientDistribution.m_PhysicianId;
            this.m_PhysicianName = physicianClientDistribution.m_PhysicianName;
            this.m_LongDistance = physicianClientDistribution.LongDistance;
            this.m_FaxNumber = physicianClientDistribution.FaxNumber;
            this.m_DistributionType = physicianClientDistribution.DistributionType;
        }

        [PersistentProperty]
		public int PhysicianId
        {
            get { return this.m_PhysicianId; }
            set { this.m_PhysicianId = value; }
        }

        [PersistentProperty]
        public string PhysicianName
        {
            get { return this.m_PhysicianName; }
            set { this.m_PhysicianName = value; }
        }

        [PersistentProperty]
        public int ClientId
        {
            get { return this.m_ClientId; }
            set { this.m_ClientId = value; }
        }

        [PersistentProperty]
        public string ClientName
        {
            get { return this.m_ClientName; }
            set { this.m_ClientName = value; }
        }

        [PersistentProperty]
        public string DistributionType
        {
            get { return this.m_DistributionType; }
            set { this.m_DistributionType = value; }
        }

        [PersistentProperty]
        public string FaxNumber
        {
            get { return this.m_FaxNumber; }
            set { this.m_FaxNumber = value; }
        }

        [PersistentProperty]
        public bool LongDistance
        {
            get { return this.m_LongDistance; }
            set { this.m_LongDistance = value; }
        }

        public string FormattedFaxNumber
        {
            get
            {
                if (this.m_FaxNumber == null)
                    return string.Empty;

                switch (this.m_FaxNumber.Length)
                {
                    case 7:
                        return Regex.Replace(this.m_FaxNumber, @"(\d{3})(\d{4})", "$1-$2");
                    case 10:
                        return Regex.Replace(this.m_FaxNumber, @"(\d{3})(\d{3})(\d{4})", "($1) $2-$3");
                    case 11:
                        return Regex.Replace(this.m_FaxNumber, @"(\d{1})(\d{3})(\d{3})(\d{4})", "$1-$2-$3-$4");
                    default:
                        return this.m_FaxNumber;
                }

            }
        }

        public bool SetDistribution(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {
            bool result = true;
            foreach(YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder in accessionOrder.PanelSetOrderCollection)
            {
                result = SetDistribution(panelSetOrder, accessionOrder);
            }
            return result;
        }

        public bool SetDistribution(YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder, YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {
            bool result = true;

            switch (this.DistributionType)
            {
                case YellowstonePathology.Business.ReportDistribution.Model.DistributionType.WEBSERVICEANDFAX:
                    this.HandleAddWebServiceDistribution(panelSetOrder);
                    this.HandleAddFaxDistribution(panelSetOrder);
                    break;
                case YellowstonePathology.Business.ReportDistribution.Model.DistributionType.EPICANDFAX:
                    this.HandleAddEPICDistribution(panelSetOrder, accessionOrder);
                    this.HandleAddFaxDistribution(panelSetOrder);
                    break;
                case YellowstonePathology.Business.ReportDistribution.Model.DistributionType.EPIC:
                    this.HandleAddEPICDistribution(panelSetOrder, accessionOrder);
                    break;
                case YellowstonePathology.Business.ReportDistribution.Model.DistributionType.FAX:
                    this.HandleAddFaxDistribution(panelSetOrder);
                    break;
                case YellowstonePathology.Business.ReportDistribution.Model.DistributionType.WEBSERVICE:
                    this.HandleAddWebServiceDistribution(panelSetOrder);
                    break;
                case YellowstonePathology.Business.ReportDistribution.Model.DistributionType.PRINT:
                    this.AddTestOrderReportDistribution(panelSetOrder, this.m_PhysicianId, this.m_PhysicianName, this.m_ClientId, this.m_ClientName, YellowstonePathology.Business.ReportDistribution.Model.DistributionType.PRINT, this.FaxNumber, this.LongDistance);
                    break;
                case YellowstonePathology.Business.ReportDistribution.Model.DistributionType.MTDOH:
                    this.HandleAddMTDOHDistribution(panelSetOrder);
                    break;
                case YellowstonePathology.Business.ReportDistribution.Model.DistributionType.WYDOH:
                    this.HandleAddWYDOHDistribution(panelSetOrder);
                    break;
                case YellowstonePathology.Business.ReportDistribution.Model.DistributionType.ECW:
                    this.HandleAddECWDistribution(panelSetOrder, accessionOrder);
                    break;
                case YellowstonePathology.Business.ReportDistribution.Model.DistributionType.ATHENA:
                    this.HandleAddAthenaDistribution(panelSetOrder, accessionOrder);
                    break;
                case YellowstonePathology.Business.ReportDistribution.Model.DistributionType.MEDITECH:
                    this.HandleAddMEDITECHDistribution(panelSetOrder, accessionOrder);
                    break;
                default:
                    throw new Exception("Not implemented");
            }

            return result;
        }

        private void AddTestOrderReportDistribution(YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder, int physicianId, string physicianName, int clientId, string clientName, string distributionType, string faxNumber, bool longDistance)
        {

            string testOrderReportDistributionId = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
            YellowstonePathology.Business.ReportDistribution.Model.TestOrderReportDistribution testOrderReportDistribution =
                new YellowstonePathology.Business.ReportDistribution.Model.TestOrderReportDistribution(testOrderReportDistributionId, testOrderReportDistributionId, panelSetOrder.ReportNo, physicianId, physicianName,
                    clientId, clientName, distributionType, faxNumber, longDistance);
            panelSetOrder.TestOrderReportDistributionCollection.Add(testOrderReportDistribution);
        }        

        private bool HandleAddFaxDistribution(YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder)
        {
            bool result = true;
            if (string.IsNullOrEmpty(this.m_FaxNumber) == false)
            {
                if (panelSetOrder.TestOrderReportDistributionCollection.Exists(this.m_PhysicianId, this.m_ClientId, YellowstonePathology.Business.ReportDistribution.Model.DistributionType.FAX) == false)
                {
                    this.AddTestOrderReportDistribution(panelSetOrder, this.m_PhysicianId, this.m_PhysicianName, this.m_ClientId, this.m_ClientName, YellowstonePathology.Business.ReportDistribution.Model.DistributionType.FAX, this.FaxNumber, this.LongDistance);
                }
            }
            else
            {
                //MessageBox.Show("Unable to add a fax distribution because the fax number is blank.");
                result = false;
            }
            return result;
        }

        private bool HandleAddEPICDistribution(YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder, YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {
            bool result = true;
            if (panelSetOrder.TestOrderReportDistributionCollection.DistributionTypeExists(YellowstonePathology.Business.ReportDistribution.Model.DistributionType.EPIC) == false)
            {
                List<int> clientGroupIds = new List<int>();
                clientGroupIds.Add(1);
                clientGroupIds.Add(2);

                YellowstonePathology.Business.Client.Model.ClientGroupClientCollection stVincentAndHRHGroup = YellowstonePathology.Business.Gateway.PhysicianClientGateway.GetClientGroupClientCollectionByClientGroupId(clientGroupIds);
                if (stVincentAndHRHGroup.ClientIdExists(accessionOrder.ClientId) == true)
                {
                    if (string.IsNullOrEmpty(accessionOrder.SvhAccount) == true || string.IsNullOrEmpty(accessionOrder.SvhMedicalRecord) == true)
                    {
                        this.HandleAddFaxDistribution(panelSetOrder);
                    }
                    else
                    {
                        YellowstonePathology.Business.PanelSet.Model.PanelSetCollection panelSetCollection = YellowstonePathology.Business.PanelSet.Model.PanelSetCollection.GetAll();
                        YellowstonePathology.Business.PanelSet.Model.PanelSet panelSet = panelSetCollection.GetPanelSet(panelSetOrder.PanelSetId);
                        if (panelSet.ResultDocumentSource == YellowstonePathology.Business.PanelSet.Model.ResultDocumentSourceEnum.YPIDatabase)
                        {
                            this.AddTestOrderReportDistribution(panelSetOrder, accessionOrder.PhysicianId, accessionOrder.PhysicianName, accessionOrder.ClientId, accessionOrder.ClientName, YellowstonePathology.Business.ReportDistribution.Model.DistributionType.EPIC, this.FaxNumber, this.LongDistance);
                        }
                        else
                        {
                            this.HandleAddFaxDistribution(panelSetOrder);
                        }
                    }
                }                
                else
                {
                    this.HandleAddFaxDistribution(panelSetOrder);
                }
            }
            return result;
        }

        private bool HandleAddWebServiceDistribution(YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder)
        {
            bool result = true;
            if (panelSetOrder.TestOrderReportDistributionCollection.Exists(this.m_PhysicianId, this.m_ClientId, this.m_DistributionType) == false)
            {
                this.AddTestOrderReportDistribution(panelSetOrder, this.m_PhysicianId, this.m_PhysicianName, this.m_ClientId, this.m_ClientName, YellowstonePathology.Business.ReportDistribution.Model.DistributionType.WEBSERVICE, this.FaxNumber, this.LongDistance);
            }
            return result;
        }

        private bool HandleAddMTDOHDistribution(YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder)
        {
            bool result = true;
            if (panelSetOrder.PanelSetId == 13)
            {
                if (panelSetOrder.TestOrderReportDistributionCollection.Exists(this.m_PhysicianId, this.m_ClientId, this.m_DistributionType) == false)
                {
                    this.AddTestOrderReportDistribution(panelSetOrder, this.m_PhysicianId, this.m_PhysicianName, this.m_ClientId, this.m_ClientName, YellowstonePathology.Business.ReportDistribution.Model.DistributionType.MTDOH, this.FaxNumber, this.LongDistance);
                }
            }
            return result;
        }

        private bool HandleAddWYDOHDistribution(YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder)
        {
            bool result = true;
            if (panelSetOrder.PanelSetId == 13)
            {
                if (panelSetOrder.TestOrderReportDistributionCollection.Exists(this.m_PhysicianId, this.m_ClientId, this.m_DistributionType) == false)
                {
                    this.AddTestOrderReportDistribution(panelSetOrder, this.m_PhysicianId, this.m_PhysicianName, this.m_ClientId, this.m_ClientName, YellowstonePathology.Business.ReportDistribution.Model.DistributionType.WYDOH, this.FaxNumber, this.LongDistance);
                }
            }
            return result;
        }

        private bool HandleAddAthenaDistribution(YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder, YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {
            bool result = true;
            if (panelSetOrder.TestOrderReportDistributionCollection.DistributionTypeExists(YellowstonePathology.Business.ReportDistribution.Model.DistributionType.ATHENA) == false)
            {
                YellowstonePathology.Business.PanelSet.Model.PanelSetCollection panelSetCollection = YellowstonePathology.Business.PanelSet.Model.PanelSetCollection.GetAll();
                YellowstonePathology.Business.PanelSet.Model.PanelSet panelSet = panelSetCollection.GetPanelSet(panelSetOrder.PanelSetId);
                if (panelSet.ResultDocumentSource == YellowstonePathology.Business.PanelSet.Model.ResultDocumentSourceEnum.YPIDatabase)
                {
                    YellowstonePathology.Business.Client.Model.ClientGroupClientCollection cmmcGroup = YellowstonePathology.Business.Gateway.PhysicianClientGateway.GetClientGroupClientCollectionByClientGroupId(3);
                    if (cmmcGroup.ClientIdExists(this.ClientId) == true)
                    {
                        this.AddTestOrderReportDistribution(panelSetOrder, this.m_PhysicianId, this.m_PhysicianName, this.m_ClientId, this.m_ClientName, YellowstonePathology.Business.ReportDistribution.Model.DistributionType.ATHENA, this.FaxNumber, this.LongDistance);
                    }
                }
            }            
            return result;
        }

        private bool HandleAddECWDistribution(YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder, YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {
            bool result = true;
            if (panelSetOrder.TestOrderReportDistributionCollection.DistributionTypeExists(YellowstonePathology.Business.ReportDistribution.Model.DistributionType.ECW) == false)
            {
                if (accessionOrder.ClientId == 1203)
                {
                    this.AddTestOrderReportDistribution(panelSetOrder, accessionOrder.PhysicianId, accessionOrder.PhysicianName, accessionOrder.ClientId, accessionOrder.ClientName, YellowstonePathology.Business.ReportDistribution.Model.DistributionType.ECW, this.FaxNumber, this.LongDistance);
                }
            }
            return result;
        }

        private bool HandleAddMEDITECHDistribution(YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder, YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {
            bool result = true;
            if (panelSetOrder.TestOrderReportDistributionCollection.DistributionTypeExists(YellowstonePathology.Business.ReportDistribution.Model.DistributionType.MEDITECH) == false)
            {
                YellowstonePathology.Business.Client.Model.ClientGroupClientCollection westParkHospitalGroup = YellowstonePathology.Business.Gateway.PhysicianClientGateway.GetClientGroupClientCollectionByClientGroupId(36);
                if (westParkHospitalGroup.ClientIdExists(accessionOrder.ClientId) == true)
                {
                    this.AddTestOrderReportDistribution(panelSetOrder, accessionOrder.PhysicianId, accessionOrder.PhysicianName, accessionOrder.ClientId, accessionOrder.ClientName, YellowstonePathology.Business.ReportDistribution.Model.DistributionType.MEDITECH, this.FaxNumber, this.LongDistance);
                }
            }
            return result;
        }
    }
}
