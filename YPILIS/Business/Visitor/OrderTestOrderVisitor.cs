using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YellowstonePathology.Business.Test;

namespace YellowstonePathology.Business.Visitor
{
    public class OrderTestOrderVisitor : AccessionTreeVisitor
    {
        protected YellowstonePathology.Business.Test.PanelSetOrder m_PanelSetOrder;
        protected YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
        protected YellowstonePathology.Business.PanelSet.Model.PanelSet m_PanelSet;
        protected YellowstonePathology.Business.Interface.IOrderTarget m_OrderTarget;        
        
        protected string m_ReportNo;
        protected bool m_OrderTargetIsKnow;
		protected YellowstonePathology.Business.Test.TestOrderInfo m_TestOrderInfo;

        public OrderTestOrderVisitor(YellowstonePathology.Business.Test.TestOrderInfo testOrderInfo)
            : base(true, true)
        {
            this.m_OrderTargetIsKnow = testOrderInfo.OrderTargetIsKnown;
            this.m_PanelSet = testOrderInfo.PanelSet;
            this.m_OrderTarget = testOrderInfo.OrderTarget;            
			this.m_TestOrderInfo = testOrderInfo;
        }        

        public YellowstonePathology.Business.Test.PanelSetOrder PanelSetOrder
        {
            get { return this.m_PanelSetOrder; }
        }

        public override void Visit(Test.AccessionOrder accessionOrder)
        {
            this.m_AccessionOrder = accessionOrder;
            this.HandleAddAliquotOnOrder();
            this.HandleClientAccessioned();
            this.HandlePanelSetOrder();                                    
            this.HandlePanelOrders();
            this.HandlDistribution();
            this.HandlReflexTestingPlan();
            this.HandlePantherOrder();
            this.HandleRetrospectiveReviews();
            this.HandleSurgicalAmendments();
        }  
        
        private void HandleSurgicalAmendments()
        {
            if(this.m_PanelSet.AddSurgicalAmendment == true && this.m_AccessionOrder.PanelSetOrderCollection.HasSurgical() == true)
            {
                PanelSetOrder panelSetOrder = this.m_AccessionOrder.PanelSetOrderCollection.GetSurgical();
                Amendment.Model.Amendment amendment = new Amendment.Model.Amendment();
                amendment.AmendmentId = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
                amendment.MasterAccessionNo = this.m_AccessionOrder.MasterAccessionNo;
                amendment.ReportNo = panelSetOrder.ReportNo;
                amendment.AmendmentDate = DateTime.Today;
                amendment.AmendmentTime = DateTime.Now;
                amendment.AmendmentType = "Addendum";
                amendment.DistributeOnFinal = true;
                amendment.SystemGenerated = true;
                amendment.UserId = this.m_AccessionOrder.CaseOwnerId;

                if(this.m_PanelSet.SurgicalAmendmentRequired == true)
                {
                    amendment.Text = this.m_PanelSet.SurgicalAmendmentTemplate;
                    amendment.Text = amendment.Text.Replace("[PATIENTNAME]", this.m_AccessionOrder.PatientDisplayName);
                    amendment.Text = amendment.Text.Replace("[ORDERDATE]", DateTime.Today.ToString("MM/dd/yyyy"));
                    amendment.Text = amendment.Text.Replace("[PROVIDERLASTNAME]", this.m_AccessionOrder.PhysicianName);                    
                }
                
                this.m_AccessionOrder.AmendmentCollection.Add(amendment);

                YellowstonePathology.Business.Test.PanelSetOrderCPTCode panelSetOrderCPTCode = this.m_PanelSetOrder.PanelSetOrderCPTCodeCollection.GetNextItem(panelSetOrder.ReportNo);
                panelSetOrderCPTCode.Quantity = 1;
                panelSetOrderCPTCode.CPTCode = "88363";
                panelSetOrderCPTCode.Modifier = null;
                //panelSetOrderCPTCode.CodeableDescription = "Specimen " + specimenOrder.SpecimenNumber + ": " + this.m_PanelSetOrder.PanelSetName;
                //panelSetOrderCPTCode.CodeableType = "BillableTest";
                panelSetOrderCPTCode.EntryType = Business.Billing.Model.PanelSetOrderCPTCodeEntryType.SystemGenerated;
                //panelSetOrderCPTCode.SpecimenOrderId = specimenOrder.SpecimenOrderId;
                panelSetOrderCPTCode.ClientId = this.m_AccessionOrder.ClientId;
                panelSetOrderCPTCode.MedicalRecord = this.m_AccessionOrder.SvhMedicalRecord;
                panelSetOrderCPTCode.Account = this.m_AccessionOrder.SvhAccount;
                panelSetOrder.PanelSetOrderCPTCodeCollection.Add(panelSetOrderCPTCode);                
            }
        }

        private void HandleRetrospectiveReviews()
        {
            if(this.m_PanelSetOrder is Business.Test.RetrospectiveReview.RetrospectiveReviewTestOrder)
            {
                Business.Test.RetrospectiveReview.RetrospectiveReviewTestOrder rrto = (Business.Test.RetrospectiveReview.RetrospectiveReviewTestOrder)this.m_PanelSetOrder;
                foreach(Business.Specimen.Model.SpecimenOrder specimenOrder in this.m_AccessionOrder.SpecimenOrderCollection)
                {
                    Business.Test.Surgical.SurgicalTestOrder surgicalTestOrder = this.m_AccessionOrder.PanelSetOrderCollection.GetSurgical();
                    Business.Test.Surgical.SurgicalSpecimen surgicalSpecimen = surgicalTestOrder.SurgicalSpecimenCollection.GetBySpecimenOrderId(specimenOrder.SpecimenOrderId);
                    if (surgicalSpecimen != null)
                    {
                        string objectId = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
                        Business.Test.RetrospectiveReview.RetrospectiveReviewTestOrderDetail rrtod = new Test.RetrospectiveReview.RetrospectiveReviewTestOrderDetail(objectId, rrto.ReportNo);
                        rrtod.SpecimenDescription = specimenOrder.Description;
                        rrtod.SpecimenNumber = specimenOrder.SpecimenNumber;
                        rrtod.Diagnosis = surgicalSpecimen.Diagnosis;

                        rrto.RetrospectiveReviewTestOrderDetailCollection.Add(rrtod);
                    }
                }
            }
        }    

        private void HandlePantherOrder()
        {
            if (this.m_PanelSet.SendOrderToPanther == true)
            {                
                Business.HL7View.Panther.PantherAssay pantherAssay = Business.HL7View.Panther.PantherAssayFactory.GetPantherAssay(this.m_PanelSetOrder);                  
                Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrderByOrderTarget(this.m_PanelSetOrder.OrderedOnId);                                
                this.m_PanelSetOrder.InstrumentOrderDate = DateTime.Now;                
                YellowstonePathology.Business.HL7View.Panther.PantherOrder pantherOrder = new Business.HL7View.Panther.PantherOrder(pantherAssay, specimenOrder, this.m_AccessionOrder, this.m_PanelSetOrder, YellowstonePathology.Business.HL7View.Panther.PantherActionCode.NewSample);
                pantherOrder.Send();                    
            }
        }

        private void HandleAddAliquotOnOrder()
        {
            if (this.m_PanelSet.AddAliquotOnOrder == true)
            {
                YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = null;
                if (this.m_TestOrderInfo.OrderTarget is Business.Specimen.Model.SpecimenOrder)
                {
                    specimenOrder = (YellowstonePathology.Business.Specimen.Model.SpecimenOrder)this.m_OrderTarget;
                }
                else
                {
                    YellowstonePathology.Business.Test.AliquotOrder aliquotOrderOrderedOn = (YellowstonePathology.Business.Test.AliquotOrder)this.m_TestOrderInfo.OrderTarget;
                    specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrderByAliquotOrderId(aliquotOrderOrderedOn.AliquotOrderId);
                }                
                 
                YellowstonePathology.Business.Test.AliquotOrder aliquotOrder = null;

                if (specimenOrder.AliquotOrderCollection.Exists(this.m_PanelSet.AliquotToAddOnOrder) == false)
                {
                    aliquotOrder = specimenOrder.AliquotOrderCollection.AddAliquot(this.m_PanelSet.AliquotToAddOnOrder, specimenOrder, DateTime.Now);
                    this.m_TestOrderInfo.OrderTarget = aliquotOrder;
                }
                else
                {
                    aliquotOrder = specimenOrder.AliquotOrderCollection.Get(this.m_PanelSet.AliquotToAddOnOrder);
                    this.m_TestOrderInfo.OrderTarget = aliquotOrder;
                }
            }
        }

        private void HandleClientAccessioned()
        {
            if (this.m_PanelSet.IsClientAccessioned == true)
            {
                this.m_AccessionOrder.ClientAccessioned = true;
                foreach (YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder in this.m_AccessionOrder.SpecimenOrderCollection)
                {
                    specimenOrder.ClientAccessioned = true;
                    foreach (YellowstonePathology.Business.Test.AliquotOrder aliquotOrder in specimenOrder.AliquotOrderCollection)
                    {
                        aliquotOrder.ClientAccessioned = true;
                    }
                }
            }
        }

        private void HandlePanelSetOrder()
        {
            ClientOrder.Model.ExternalOrderIdsCollection externalOrderIdsCollection = ClientOrder.Model.ExternalOrderIdsCollection.FromFormattedValue(this.m_AccessionOrder.ExternalOrderId);
            string externalOrderId = externalOrderIdsCollection.GetExternalOrderId(this.m_PanelSet.PanelSetId);            

            this.m_ReportNo = this.m_AccessionOrder.GetNextReportNo(this.m_PanelSet);
            string objectId = MongoDB.Bson.ObjectId.GenerateNewId().ToString();

            bool distribute = true;
            if(this.m_TestOrderInfo.Distribute.HasValue == true)
            {
                distribute = this.m_TestOrderInfo.Distribute.Value;
            }
            else if (this.m_PanelSet.NeverDistribute == true)
            {
                distribute = false;
            }

            this.m_PanelSetOrder = null;
            if (this.m_PanelSet.HasNoOrderTarget == true)
            {
                this.m_PanelSetOrder = Business.Test.PanelSetOrderFactory.CreatePanelSetOrder(this.m_AccessionOrder.MasterAccessionNo, this.m_ReportNo, objectId, this.m_PanelSet, distribute);
            }
            else
            {
                if (this.m_OrderTargetIsKnow == false)
                {
                    this.m_OrderTarget = this.m_AccessionOrder.SpecimenOrderCollection.GetOrderTarget(this.m_PanelSet.OrderTargetTypeCollectionRestrictions);
                }
                this.m_PanelSetOrder = Business.Test.PanelSetOrderFactory.CreatePanelSetOrder(this.m_AccessionOrder.MasterAccessionNo, this.m_ReportNo, objectId, this.m_PanelSet, this.m_OrderTarget, distribute);
            }
            
            this.m_PanelSetOrder.ExternalOrderId = externalOrderId;
            
            string universalServiceId = externalOrderIdsCollection.GetUniversalServiceId(this.m_PanelSet.PanelSetId);
            if (this.m_AccessionOrder.UniversalServiceId == "SMP" && this.m_PanelSet.PanelSetId == 13) universalServiceId = "SMP"; //this per SVH
            if(string.IsNullOrEmpty(universalServiceId) == false)
            {
                this.m_PanelSetOrder.UniversalServiceId = universalServiceId;
            }
            else
            {
                if(this.m_AccessionOrder.ClientId == 136)
                {
                    Business.ClientOrder.Model.UniversalServiceCollectionRiverstone uscr = new ClientOrder.Model.UniversalServiceCollectionRiverstone();
                    Business.ClientOrder.Model.UniversalService us = uscr.GetByPanelSetId(this.m_PanelSet.PanelSetId);
                    if(us != null)
                    {
                        this.m_PanelSetOrder.UniversalServiceId = us.UniversalServiceId;
                    }
                }
            }           

            if (this.m_PanelSet.OrderInitialTestsOnly == false)
            {
                this.m_AccessionOrder.PanelSetOrderCollection.Add(this.m_PanelSetOrder);
                this.m_AccessionOrder.PanelSetOrderCollection.UpdateWHPExpectedFinalTimeOnOrder(this.m_PanelSetOrder);
                this.m_AccessionOrder.UpdateCaseAssignment(this.m_PanelSetOrder);
			    this.m_TestOrderInfo.PanelSetOrder = this.m_PanelSetOrder;            
            }

            this.PanelSetOrder.HandlePostCreationTasks(this.m_AccessionOrder);
        }

        public virtual void HandlePanelOrders()
        {
            foreach (YellowstonePathology.Business.Panel.Model.Panel panel in this.m_PanelSet.PanelCollection)
            {
                string panelOrderId = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
                YellowstonePathology.Business.Test.PanelOrder panelOrder = Business.Test.PanelOrderFactory.GetPanelOrder(this.m_ReportNo, panelOrderId, panelOrderId, panel, YellowstonePathology.Business.User.SystemIdentity.Instance.User.UserId, YellowstonePathology.Business.User.SystemIdentity.Instance.User.Initials);
                this.m_PanelSetOrder.PanelOrderCollection.Add(panelOrder);

                if (panel.AcknowledgeOnOrder == true)
                {
                    panelOrder.Acknowledged = true;
                    panelOrder.AcknowledgedById = Business.User.SystemIdentity.Instance.User.UserId;
                    panelOrder.AcknowledgedDate = DateTime.Today;
                    panelOrder.AcknowledgedTime = DateTime.Now;
                }

                this.HandleTestOrders(panel, panelOrder);
            }
        }

        public virtual void HandleTestOrders(YellowstonePathology.Business.Panel.Model.Panel panel, YellowstonePathology.Business.Test.PanelOrder panelOrder)
        {
            if (this.m_OrderTarget is YellowstonePathology.Business.Test.AliquotOrder)
            {
                YellowstonePathology.Business.Test.AliquotOrder aliquotOrder = (YellowstonePathology.Business.Test.AliquotOrder)this.m_OrderTarget;
                foreach (YellowstonePathology.Business.Test.Model.Test test in panel.TestCollection)
                {
                    string testOrderObjectId = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
                    YellowstonePathology.Business.Test.Model.TestOrder testOrder = panelOrder.TestOrderCollection.Add(panelOrder.PanelOrderId, testOrderObjectId, aliquotOrder.AliquotOrderId, test, test.OrderComment);                    
                    
                    aliquotOrder.TestOrderCollection.Add(testOrder);
                    aliquotOrder.SetLabelPrefix(testOrder, true);
                }
            }
            else
            {
                foreach (YellowstonePathology.Business.Test.Model.Test test in panel.TestCollection)
                {
                    string testOrderObjectId = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
                    YellowstonePathology.Business.Test.Model.TestOrder testOrder = panelOrder.TestOrderCollection.Add(panelOrder.PanelOrderId, testOrderObjectId, null, test, test.OrderComment);                    
                }
            }
        }

        public virtual void HandlDistribution()
        {
            this.m_AccessionOrder.SetDistribution();
            //YellowstonePathology.Business.Client.Model.PhysicianClientDistributionList physicianClientDistributionCollection = Business.Gateway.ReportDistributionGateway.GetPhysicianClientDistributionCollection(this.m_AccessionOrder.PhysicianId, this.m_AccessionOrder.ClientId);
            //physicianClientDistributionCollection.SetDistribution(this.m_PanelSetOrder, this.m_AccessionOrder);
        }

        public virtual void HandlReflexTestingPlan()
        {
            if (this.m_PanelSetOrder is YellowstonePathology.Business.Test.ReflexTesting.ReflexTestingPlan)
            {
                YellowstonePathology.Business.Test.ReflexTesting.ReflexTestingPlan reflexTestingPlan = (YellowstonePathology.Business.Test.ReflexTesting.ReflexTestingPlan)this.m_PanelSetOrder;
                reflexTestingPlan.OrderInitialTests(this.m_AccessionOrder, this.m_OrderTarget);
            }
        }

        public override void Visit(YellowstonePathology.Business.Test.Surgical.SurgicalTestOrder surgicalTestOrder)
        {
            if(this.m_TestOrderInfo.PanelSet.PanelSetId == 13)
            {
                Test.Surgical.SurgicalTest surgicalTest = new Test.Surgical.SurgicalTest();
                foreach (YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder in this.m_AccessionOrder.SpecimenOrderCollection)
                {
                    if (surgicalTest.OrderTargetTypeCollectionExclusions.Exists(specimenOrder) == false)
                    {
                        if (surgicalTestOrder.SurgicalSpecimenCollection.SpecimenOrderExists(specimenOrder.SpecimenOrderId) == false)
                        {
                            YellowstonePathology.Business.Test.Surgical.SurgicalSpecimen surgicalSpecimen = surgicalTestOrder.SurgicalSpecimenCollection.Add(this.m_ReportNo);
                            surgicalSpecimen.FromSpecimenOrder(specimenOrder);
                        }
                    }
                }
            }            
        }
    }
}
