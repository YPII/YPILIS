using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using MySql.Data.MySqlClient;

namespace YellowstonePathology.Business.Gateway
{
    public class AccessionOrderBuilderV2
    {
        private Test.AccessionOrder m_AccessionOrder;
        private PanelSet.Model.PanelSetCollection m_PanelSetCollection;
        private List<string> m_PanelSetOrderReportNumbers;
        private List<string> m_PanelOrderIds;
        private DataTable m_TestOrderDataTable;
        private DataTable m_AliquotOrderDataTable;
        private DataTable m_SlideOrderDataTable;
        private DataTable m_TestOrderUnsortedDataTable;

        public AccessionOrderBuilderV2()
        {
            this.m_PanelSetCollection = PanelSet.Model.PanelSetCollection.GetAll();
        }

        public void Build(MySqlCommand cmd, YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {
            this.m_PanelSetOrderReportNumbers = new List<string>();
            this.m_PanelOrderIds = new List<string>();
            this.m_AccessionOrder = accessionOrder;
                        
            using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (MySqlDataReader dr = cmd.ExecuteReader(CommandBehavior.KeyInfo))
                {
                    HandleDataSets(dr);
                }
            }

            this.m_AccessionOrder.AccessionLock.MasterAccessionNo = accessionOrder.MasterAccessionNo;
            this.m_AccessionOrder.PanelSetOrderCollection.RemoveDeleted(this.m_PanelSetOrderReportNumbers);
            this.m_AccessionOrder.PanelSetOrderCollection = Test.PanelSetOrderCollection.Sort(this.m_AccessionOrder.PanelSetOrderCollection);
            this.RemoveDeletedPanelOrders();

            if (this.m_TestOrderDataTable != null)
            {
                this.HandleSlideOrderTestOrder(this.m_TestOrderDataTable);
                this.HandleAliquotOrderTestOrder(this.m_TestOrderDataTable);
            }
            if (this.m_AliquotOrderDataTable != null) this.HandleTestOrderAliquotOrder(this.m_AliquotOrderDataTable);
            if (this.m_SlideOrderDataTable != null) this.HandleTestOrderSlideOrderCollection(this.m_SlideOrderDataTable);

            if (this.m_AccessionOrder.PanelSetOrderCollection.HasSurgical() == true)
            {
                Test.Surgical.SurgicalTestOrder surgicalTestOrder = this.m_AccessionOrder.PanelSetOrderCollection.GetSurgical();
                this.SetSurgicalAuditAmendment(surgicalTestOrder);
                this.SetSurgicalSpecimenSpecimenOrder(surgicalTestOrder);
                this.SetSurgicalSpecimenAuditSpecimenOrder(surgicalTestOrder);
                this.SetSurgicalSpecimenOrderItemCollection(surgicalTestOrder);
                this.SetTypingStainCollection(surgicalTestOrder);
            }

            foreach(Test.PanelSetOrder panelSetOrder in this.m_AccessionOrder.PanelSetOrderCollection)
            {
                SetOrderedOnDescription(panelSetOrder);
            }
        }

        private void HandleDataSets(MySqlDataReader dr)
        {
            DataSet dataSet = new DataSet();
            dataSet.EnforceConstraints = false;
            DataTable dataTable = new DataTable();
            dataSet.Tables.Add(dataTable);
            dataTable.Load(dr, LoadOption.OverwriteChanges);
            if (dataTable.Rows.Count > 0)
            {
                string tablename = dataTable.Rows[0][0].ToString();
                switch (tablename)
                {
                    case "tblAccessionOrder":
                        this.HandleAccessionOrder(dataTable);
                        break;
                    case "tblSpecimenOrder":
                        this.HandleSpecimenOrder(dataTable);
                        break;
                    case "tblAliquotOrder":
                        this.HandleAliquotOrder(dataTable);
                        break;
                    case "tblSlideOrder":
                        this.HandleSlideOrder(dataTable);
                        break;
                    case "tblPanelSetOrder":
                        this.HandlePanelSetOrder(dataTable);
                        break;
                    case "tblPanelOrder":
                        this.HandlePanelOrder(dataTable);
                        break;
                    case "tblTestOrder":
                        //this.HandleTestOrder(dataTable);
                        this.m_TestOrderDataTable = dataTable;
                        break;
                    case "tblTestOrderUnsorted":
                        this.m_TestOrderUnsortedDataTable = dataTable;
                        this.HandleTestOrder(dataTable);
                        break;
                    case "tblTaskOrder":
                        this.HandleTaskOrder(dataTable);
                        break;
                    case "tblTaskOrderDetail":
                        this.HandleTaskOrderDetail(dataTable);
                        break;
                    case "tblICD9BillingCode":
                        this.HandleICD9BillingCode(dataTable);
                        break;
                    case "tblAmendment":
                        this.HandleAmendment(dataTable);
                        break;
                    case "tblPanelSetOrderCPTCode":
                        this.HandlePanelSetOrderCPTCode(dataTable);
                        break;
                    case "tblPanelSetOrderCPTCodeBill":
                        this.HandlePanelSetOrderCPTCodeBill(dataTable);
                        break;
                    case "tblTestOrderReportDistribution":
                        this.HandleTestOrderReportDistribution(dataTable);
                        break;
                    case "tblTestOrderReportDistributionLog":
                        this.HandleTestOrderReportDistributionLog(dataTable);
                        break;
                    case "tblSurgicalSpecimen":
                        this.HandleSurgicalSpecimen(dataTable);
                        break;
                    case "tblIcd9Code":
                        this.HandleICD9Code(dataTable);
                        break;
                    case "tblIntraoperativeConsultationResult":
                        this.HandleIntraoperativeConsultationResult(dataTable);
                        break;
                    case "tblStainResult":
                        this.HandleStainResult(dataTable);
                        break;
                    case "tblSurgicalAudit":
                        this.HandleSurgicalAudit(dataTable);
                        break;
                    case "tblSurgicalSpecimenAudit":
                        this.HandleSurgicalSpecimenAudit(dataTable);
                        break;
                    case "tblFlowMarkers":
                        this.HandleFlowMarker(dataTable);
                        break;
                }
            }
            
            if (dr.IsClosed == false)
            {
                HandleDataSets(dr);
            }
        }

        public void BuildMySql(MySqlCommand cmd, YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {
            this.m_PanelSetOrderReportNumbers = new List<string>();
            this.m_PanelOrderIds = new List<string>();
            this.m_AccessionOrder = accessionOrder;

            using (MySqlConnection cn = new MySqlConnection("Server = 10.1.2.26; Uid = sqldude; Pwd = 123Whatsup; Database = lis;"))
            {
                cn.Open();
                cmd.Connection = cn;
                using (MySqlDataReader dr = cmd.ExecuteReader(CommandBehavior.KeyInfo))
                {
                    HandleMySqlDataSets(dr);
                }
            }

            this.m_AccessionOrder.AccessionLock.MasterAccessionNo = accessionOrder.MasterAccessionNo;
            this.m_AccessionOrder.PanelSetOrderCollection.RemoveDeleted(this.m_PanelSetOrderReportNumbers);
            this.RemoveDeletedPanelOrders();
            if (this.m_TestOrderDataTable != null)
            {
                this.HandleSlideOrderTestOrder(this.m_TestOrderDataTable);                
                this.HandleAliquotOrderTestOrder(this.m_TestOrderUnsortedDataTable);
            }
            if (this.m_AliquotOrderDataTable != null) this.HandleTestOrderAliquotOrder(this.m_AliquotOrderDataTable);
            if (this.m_SlideOrderDataTable != null) this.HandleTestOrderSlideOrderCollection(this.m_SlideOrderDataTable);

            if (this.m_AccessionOrder.PanelSetOrderCollection.HasSurgical() == true)
            {
                Test.Surgical.SurgicalTestOrder surgicalTestOrder = this.m_AccessionOrder.PanelSetOrderCollection.GetSurgical();
                this.SetSurgicalAuditAmendment(surgicalTestOrder);
                this.SetSurgicalSpecimenSpecimenOrder(surgicalTestOrder);
                this.SetSurgicalSpecimenAuditSpecimenOrder(surgicalTestOrder);
                this.SetSurgicalSpecimenOrderItemCollection(surgicalTestOrder);
            }
        }

        private void HandleMySqlDataSets(MySqlDataReader dr)
        {
            DataSet dataSet = new DataSet();
            dataSet.EnforceConstraints = false;
            DataTable dataTable = new DataTable();
            dataSet.Tables.Add(dataTable);
            dataTable.Load(dr, LoadOption.OverwriteChanges);
            if (dataTable.Rows.Count > 0)
            {
                string tablename = dataTable.Rows[0][0].ToString();
                switch (tablename)
                {
                    case "tblAccessionOrder":
                        this.HandleAccessionOrder(dataTable);
                        break;
                    case "tblSpecimenOrder":
                        this.HandleSpecimenOrder(dataTable);
                        break;
                    case "tblAliquotOrder":
                        this.HandleAliquotOrder(dataTable);
                        break;
                    case "tblSlideOrder":
                        this.HandleSlideOrder(dataTable);
                        break;
                    case "tblPanelSetOrder":
                        this.HandlePanelSetOrder(dataTable);
                        break;
                    case "tblPanelOrder":
                        this.HandlePanelOrder(dataTable);
                        break;
                    case "tblTestOrder":
                        this.HandleTestOrder(dataTable);
                        break;
                    case "tblTaskOrder":
                        this.HandleTaskOrder(dataTable);
                        break;
                    case "tblTaskOrderDetail":
                        this.HandleTaskOrderDetail(dataTable);
                        break;
                    case "tblICD9BillingCode":
                        this.HandleICD9BillingCode(dataTable);
                        break;
                    case "tblAmendment":
                        this.HandleAmendment(dataTable);
                        break;
                    case "tblPanelSetOrderCPTCode":
                        this.HandlePanelSetOrderCPTCode(dataTable);
                        break;
                    case "tblPanelSetOrderCPTCodeBill":
                        this.HandlePanelSetOrderCPTCodeBill(dataTable);
                        break;
                    case "tblTestOrderReportDistribution":
                        this.HandleTestOrderReportDistribution(dataTable);
                        break;
                    case "tblTestOrderReportDistributionLog":
                        this.HandleTestOrderReportDistributionLog(dataTable);
                        break;
                    case "tblSurgicalSpecimen":
                        this.HandleSurgicalSpecimen(dataTable);
                        break;
                    case "tblIcd9Code":
                        this.HandleICD9Code(dataTable);
                        break;
                    case "tblIntraoperativeConsultationResult":
                        this.HandleIntraoperativeConsultationResult(dataTable);
                        break;
                    case "tblStainResult":
                        this.HandleStainResult(dataTable);
                        break;
                    case "tblSurgicalAudit":
                        this.HandleSurgicalAudit(dataTable);
                        break;
                    case "tblSurgicalSpecimenAudit":
                        this.HandleSurgicalSpecimenAudit(dataTable);
                        break;
                    case "tblFlowMarkers":
                        this.HandleFlowMarker(dataTable);
                        break;
                }
            }

            if (dr.IsClosed == false)
            {
                HandleMySqlDataSets(dr);
            }
        }

        private void HandleAccessionOrder(DataTable dataTable)
        {
            this.m_AccessionOrder.Sync(dataTable);
        }

        private void HandleSpecimenOrder(DataTable dataTable)
        {
            this.m_AccessionOrder.SpecimenOrderCollection.Sync(dataTable);
        }

        private void HandleAliquotOrder(DataTable dataTable)
        {
            this.m_AliquotOrderDataTable = dataTable;
            foreach (Business.Specimen.Model.SpecimenOrder specimenOrder in this.m_AccessionOrder.SpecimenOrderCollection)
            {
                specimenOrder.AliquotOrderCollection.Sync(dataTable, specimenOrder.SpecimenOrderId);
            }
        }

        private void HandleSlideOrder(DataTable dataTable)
        {
            this.m_SlideOrderDataTable = dataTable;
            foreach (Business.Specimen.Model.SpecimenOrder specimenOrder in this.m_AccessionOrder.SpecimenOrderCollection)
            {
                foreach (Business.Test.AliquotOrder aliquotOrder in specimenOrder.AliquotOrderCollection)
                {
                    aliquotOrder.SlideOrderCollection.Sync(dataTable, aliquotOrder.AliquotOrderId);
                }
            }
        }

        private void HandlePanelSetOrder(DataTable dataTable)
        {
            DataTableReader dataTableReader = new DataTableReader(dataTable);
            while (dataTableReader.Read())
            {
                string reportNo = dataTableReader["ReportNo"].ToString();
                this.m_PanelSetOrderReportNumbers.Add(reportNo);
            }
            this.m_AccessionOrder.PanelSetOrderCollection.Sync(dataTable);
        }

        private void HandlePanelOrder(DataTable dataTable)
        {
            foreach (Test.PanelSetOrder panelSetOrder in this.m_AccessionOrder.PanelSetOrderCollection)
            {
                panelSetOrder.PanelOrderCollection.Sync(dataTable, panelSetOrder.ReportNo);
            }

            DataTableReader dataTableReader = new DataTableReader(dataTable);
            while (dataTableReader.Read())
            {
                string panelOrderId = dataTableReader["PanelOrderId"].ToString();
                this.m_PanelOrderIds.Add(panelOrderId);
            }
        }

        private void HandleTestOrder(DataTable dataTable)
        {
            //this.m_TestOrderDataTable = dataTable;
            foreach (Test.PanelSetOrder panelSetOrder in this.m_AccessionOrder.PanelSetOrderCollection)
            {
                foreach (Test.PanelOrder panelOrder in panelSetOrder.PanelOrderCollection)
                {
                    panelOrder.TestOrderCollection.Sync(dataTable, panelOrder.PanelOrderId);
                }
            }
        }

        private void HandleAliquotOrderTestOrder(DataTable dataTable)
        {
            foreach (Business.Specimen.Model.SpecimenOrder specimenOrder in this.m_AccessionOrder.SpecimenOrderCollection)
            {
                foreach(Business.Test.AliquotOrder aliquotOrder in specimenOrder.AliquotOrderCollection)
                {
                    aliquotOrder.TestOrderCollection.SyncAliquot(dataTable, aliquotOrder.AliquotOrderId);                    
                }
            }
        }

        private void HandleTestOrderSlideOrderCollection(DataTable dataTable)
        {
            foreach (Business.Test.PanelSetOrder panelSetOrder in this.m_AccessionOrder.PanelSetOrderCollection)
            {
                foreach (Business.Test.PanelOrder panelOrder in panelSetOrder.PanelOrderCollection)
                {
                    foreach (Business.Test.Model.TestOrder testOrder in panelOrder.TestOrderCollection)
                    {
                        testOrder.SlideOrderCollection.SyncForTestOrder(dataTable, testOrder.TestOrderId);
                    }
                }
            }
        }

        private void HandleSlideOrderTestOrder(DataTable dataTable)
        {
            DataTableReader dataTableReader = new DataTableReader(dataTable);
            while (dataTableReader.Read())
            {
                string testOrderId = dataTableReader["TestOrderId"].ToString();
                YellowstonePathology.Business.Test.Model.TestOrder_Base testOrder = null;
                foreach (Business.Specimen.Model.SpecimenOrder specimenOrder in this.m_AccessionOrder.SpecimenOrderCollection)
                {
                    foreach (Business.Test.AliquotOrder aliquotOrder in specimenOrder.AliquotOrderCollection)
                    {
                        foreach (Business.Slide.Model.SlideOrder slideOrder in aliquotOrder.SlideOrderCollection)
                        {
                            if (slideOrder.TestOrderId == testOrderId)
                            {
                                if (slideOrder.TestOrder == null)
                                {
                                    testOrder = new Test.Model.TestOrder();
                                    slideOrder.TestOrder = testOrder;
                                }
                                else
                                {
                                    testOrder = slideOrder.TestOrder;
                                }

                                YellowstonePathology.Business.Persistence.SqlDataTableReaderPropertyWriter sqlDataTableReaderPropertyWriter = new Persistence.SqlDataTableReaderPropertyWriter(testOrder, dataTableReader);
                                sqlDataTableReaderPropertyWriter.WriteProperties();
                            }
                        }
                    }
                }
            }            
        }

        private void HandleTestOrderAliquotOrder(DataTable dataTable)
        {
            DataTableReader dataTableReader = new DataTableReader(dataTable);
            while (dataTableReader.Read())
            {
                string aliquotOrderId = dataTableReader["AliquotOrderId"].ToString();
                YellowstonePathology.Business.Test.AliquotOrder_Base aliquotOrder = null;
                foreach(Business.Test.PanelSetOrder panelSetOrder in this.m_AccessionOrder.PanelSetOrderCollection)
                {
                    foreach(Business.Test.PanelOrder panelOrder in panelSetOrder.PanelOrderCollection)
                    {
                        foreach(Business.Test.Model.TestOrder testOrder in panelOrder.TestOrderCollection)
                        {
                            if (testOrder.AliquotOrderId == aliquotOrderId)
                            {
                                if (testOrder.AliquotOrder == null)
                                {
                                    aliquotOrder = new Test.AliquotOrder();
                                    testOrder.AliquotOrder = aliquotOrder;
                                }
                                else
                                {
                                    aliquotOrder = testOrder.AliquotOrder;
                                }

                                YellowstonePathology.Business.Persistence.SqlDataTableReaderPropertyWriter sqlDataTableReaderPropertyWriter = new Persistence.SqlDataTableReaderPropertyWriter(aliquotOrder, dataTableReader);
                                sqlDataTableReaderPropertyWriter.WriteProperties();
                            }
                        }
                    }
                }
                
            }
        }

        private void HandleTaskOrder(DataTable dataTable)
        {
            this.m_AccessionOrder.TaskOrderCollection.Sync(dataTable);
        }

        private void HandleTaskOrderDetail(DataTable dataTable)
        {
            foreach (Task.Model.TaskOrder taskOrder in this.m_AccessionOrder.TaskOrderCollection)
            {
                taskOrder.TaskOrderDetailCollection.Sync(dataTable, taskOrder.TaskOrderId);
            }
        }

        private void HandleICD9BillingCode(DataTable dataTable)
        {
            this.m_AccessionOrder.ICD9BillingCodeCollection.Sync(dataTable);
        }

        private void SetOrderedOnDescription(Test.PanelSetOrder panelSetOrder)
        {
            if (panelSetOrder.OrderedOn != null)
            {
                YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrder(panelSetOrder.OrderedOn, panelSetOrder.OrderedOnId);
                if (specimenOrder != null)
                {
                    switch (panelSetOrder.OrderedOn)
                    {
                        case YellowstonePathology.Business.OrderedOn.Specimen:
                        case YellowstonePathology.Business.OrderedOn.ThinPrepFluid:
                            panelSetOrder.OrderedOnDescription = specimenOrder.Description;
                            break;
                        case YellowstonePathology.Business.OrderedOn.Aliquot:
                            YellowstonePathology.Business.Test.AliquotOrder aliquotOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetAliquotOrder(panelSetOrder.OrderedOnId);
                            panelSetOrder.OrderedOnDescription = specimenOrder.Description + " - " + aliquotOrder.Label;
                            break;
                        default:
                            throw new Exception("Must be Specimen or Aliquot");
                    }
                }
            }
        }

        private void HandleAmendment(DataTable dataTable)
        {
            foreach (Test.PanelSetOrder panelSetOrder in this.m_AccessionOrder.PanelSetOrderCollection)
            {
                panelSetOrder.AmendmentCollection.Sync(dataTable, panelSetOrder.ReportNo);
            }
        }

        private void HandlePanelSetOrderCPTCode(DataTable dataTable)
        {
            foreach (Test.PanelSetOrder panelSetOrder in this.m_AccessionOrder.PanelSetOrderCollection)
            {
                panelSetOrder.PanelSetOrderCPTCodeCollection.Sync(dataTable, panelSetOrder.ReportNo);
            }
        }
        private void HandlePanelSetOrderCPTCodeBill(DataTable dataTable)
        {
            foreach (Test.PanelSetOrder panelSetOrder in this.m_AccessionOrder.PanelSetOrderCollection)
            {
                panelSetOrder.PanelSetOrderCPTCodeBillCollection.Sync(dataTable, panelSetOrder.ReportNo);
            }
        }
        private void HandleTestOrderReportDistribution(DataTable dataTable)
        {
            foreach (Test.PanelSetOrder panelSetOrder in this.m_AccessionOrder.PanelSetOrderCollection)
            {
                panelSetOrder.TestOrderReportDistributionCollection.Sync(dataTable, panelSetOrder.ReportNo);
            }
        }
        private void HandleTestOrderReportDistributionLog(DataTable dataTable)
        {
            foreach (Test.PanelSetOrder panelSetOrder in this.m_AccessionOrder.PanelSetOrderCollection)
            {
                panelSetOrder.TestOrderReportDistributionLogCollection.Sync(dataTable, panelSetOrder.ReportNo);
            }
        }

        private void HandleSurgicalSpecimen(DataTable dataTable)
        {
            Test.Surgical.SurgicalTestOrder surgicalTestOrder = (Test.Surgical.SurgicalTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetSurgical();
            surgicalTestOrder.SurgicalSpecimenCollection.Sync(dataTable, surgicalTestOrder.ReportNo);
        }

        private void HandleICD9Code(DataTable dataTable)
        {
            Test.Surgical.SurgicalTestOrder surgicalTestOrder = (Test.Surgical.SurgicalTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetSurgical();
            foreach (Test.Surgical.SurgicalSpecimen surgicalSpecimen in surgicalTestOrder.SurgicalSpecimenCollection)
            {
                surgicalSpecimen.ICD9BillingCodeCollection.Sync(dataTable, surgicalSpecimen.SurgicalSpecimenId);
            }
        }
        private void HandleIntraoperativeConsultationResult(DataTable dataTable)
        {
            Test.Surgical.SurgicalTestOrder surgicalTestOrder = (Test.Surgical.SurgicalTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetSurgical();
            foreach (Test.Surgical.SurgicalSpecimen surgicalSpecimen in surgicalTestOrder.SurgicalSpecimenCollection)
            {
                surgicalSpecimen.IntraoperativeConsultationResultCollection.Sync(dataTable, surgicalSpecimen.SurgicalSpecimenId);
            }
        }

        private void HandleStainResult(DataTable dataTable)
        {
            Test.Surgical.SurgicalTestOrder surgicalTestOrder = (Test.Surgical.SurgicalTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetSurgical();
            foreach (Test.Surgical.SurgicalSpecimen surgicalSpecimen in surgicalTestOrder.SurgicalSpecimenCollection)
            {
                surgicalSpecimen.StainResultItemCollection.Sync(dataTable, surgicalSpecimen.SurgicalSpecimenId);
            }
        }

        private void HandleSurgicalAudit(DataTable dataTable)
        {
            Test.Surgical.SurgicalTestOrder surgicalTestOrder = (Test.Surgical.SurgicalTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetSurgical();
            surgicalTestOrder.SurgicalAuditCollection.Sync(dataTable);
        }

        private void HandleSurgicalSpecimenAudit(DataTable dataTable)
        {
            Test.Surgical.SurgicalTestOrder surgicalTestOrder = (Test.Surgical.SurgicalTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetSurgical();
            foreach (Test.Surgical.SurgicalAudit surgicalAudit in surgicalTestOrder.SurgicalAuditCollection)
            {
                surgicalAudit.SurgicalSpecimenAuditCollection.Sync(dataTable, surgicalAudit.SurgicalAuditId);
            }
        }

        private void HandleFlowMarker(DataTable dataTable)
        {
            foreach (Test.PanelSetOrder panelSetOrder in this.m_AccessionOrder.PanelSetOrderCollection)
            {
                if (panelSetOrder is Test.LLP.PanelSetOrderLeukemiaLymphoma)
                {
                    Test.LLP.PanelSetOrderLeukemiaLymphoma llpPanelSetOrder = (Test.LLP.PanelSetOrderLeukemiaLymphoma)panelSetOrder;
                    llpPanelSetOrder.FlowMarkerCollection.Sync(dataTable, llpPanelSetOrder.ReportNo);
                    llpPanelSetOrder.FlowMarkerCollection.SetCellPopulationsOfInterest();
                    llpPanelSetOrder.FlowMarkerCollection.SetFirstMarkerPanelIfExists();
                }
            }
        }

        private void SetSurgicalAuditAmendment(YellowstonePathology.Business.Test.Surgical.SurgicalTestOrder surgicalTestOrder)
        {
            foreach (YellowstonePathology.Business.Amendment.Model.Amendment amendment in surgicalTestOrder.AmendmentCollection)
            {
                surgicalTestOrder.SurgicalAuditCollection.SetAmendmentReference(amendment);
            }
        }

        private void SetSurgicalSpecimenSpecimenOrder(YellowstonePathology.Business.Test.Surgical.SurgicalTestOrder surgicalTestOrder)
        {
            foreach (YellowstonePathology.Business.Test.Surgical.SurgicalSpecimen surgicalSpecimen in surgicalTestOrder.SurgicalSpecimenCollection)
            {
                foreach (Specimen.Model.SpecimenOrder specimenOrder in this.m_AccessionOrder.SpecimenOrderCollection)
                {
                    if (specimenOrder.SpecimenOrderId == surgicalSpecimen.SpecimenOrderId)
                    {
                        surgicalSpecimen.SpecimenOrder = specimenOrder;
                        break;
                    }
                }
            }
        }

        private void SetSurgicalSpecimenAuditSpecimenOrder(YellowstonePathology.Business.Test.Surgical.SurgicalTestOrder surgicalTestOrder)
        {
            foreach (YellowstonePathology.Business.Test.Surgical.SurgicalAudit surgicalAudit in surgicalTestOrder.SurgicalAuditCollection)
            {
                foreach (YellowstonePathology.Business.Test.Surgical.SurgicalSpecimenAudit surgicalSpecimenAudit in surgicalAudit.SurgicalSpecimenAuditCollection)
                {
                    Specimen.Model.SpecimenOrder specimenOrder = (from so in this.m_AccessionOrder.SpecimenOrderCollection
                                                                  where so.SpecimenOrderId == surgicalSpecimenAudit.SpecimenOrderId
                                                                  select so).Single<Specimen.Model.SpecimenOrder>();
                    surgicalSpecimenAudit.SpecimenOrder = specimenOrder;
                }
            }
        }

        private void SetSurgicalSpecimenOrderItemCollection(YellowstonePathology.Business.Test.Surgical.SurgicalTestOrder surgicalTestOrder)
        {

            foreach (YellowstonePathology.Business.Test.Surgical.SurgicalSpecimen surgicalSpecimen in surgicalTestOrder.SurgicalSpecimenCollection)
            {
                foreach (Specimen.Model.SpecimenOrder specimenOrder in this.m_AccessionOrder.SpecimenOrderCollection)
                {
                    if (specimenOrder.SpecimenOrderId == surgicalSpecimen.SpecimenOrderId)
                    {
                        surgicalTestOrder.SpecimenOrderCollection.IsLoading = true;
                        surgicalTestOrder.SpecimenOrderCollection.Add(specimenOrder);
                        surgicalTestOrder.SpecimenOrderCollection.IsLoading = false;
                    }
                }
            }
        }

        private void SetTypingStainCollection(YellowstonePathology.Business.Test.Surgical.SurgicalTestOrder surgicalTestOrder)
        {
            List<string> stainResultIdList = new List<string>();
            foreach (Business.Test.Surgical.SurgicalSpecimen surgicalSpecimen in surgicalTestOrder.SurgicalSpecimenCollection)
            {
                foreach (YellowstonePathology.Business.SpecialStain.StainResultItem stainResultItem in surgicalSpecimen.StainResultItemCollection)
                {
                    stainResultIdList.Add(stainResultItem.StainResultId);
                    if (surgicalTestOrder.TypingStainCollection.Exists(stainResultItem.StainResultId) == false)
                    {
                        surgicalTestOrder.TypingStainCollection.Add(stainResultItem);
                    }
                }
            }
            surgicalTestOrder.TypingStainCollection.RemoveDeleted(stainResultIdList);
        }

        private void RemoveDeletedPanelOrders()
        {
            foreach (Test.PanelSetOrder panelSetOrder in this.m_AccessionOrder.PanelSetOrderCollection)
            {
                panelSetOrder.PanelOrderCollection.RemoveDeleted(this.m_PanelOrderIds);
            }
        }
    }
}