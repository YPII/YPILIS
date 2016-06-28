using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
        }

        private void HandlePantherOrder()
        {
            if (this.m_PanelSet.SendOrderToPanther == true)
            {
                YellowstonePathology.Business.Test.AliquotOrder aliquotOrder = (YellowstonePathology.Business.Test.AliquotOrder)this.m_TestOrderInfo.OrderTarget;
                YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrderByAliquotOrderId(aliquotOrder.AliquotOrderId);

                YellowstonePathology.Business.HL7View.Panther.PantherAssay pantherAssay = null;  
                switch (this.m_PanelSetOrder.PanelSetId)
                {
                    case 14:
                        pantherAssay = new Business.HL7View.Panther.PantherAssayHPV();
                        break;
                    case 3:
                        pantherAssay = new Business.HL7View.Panther.PantherAssayNGCT();
                        break;
                    case 62:
                        pantherAssay = new Business.HL7View.Panther.PantherAssayHPV1618();
                        break;
                    case 61:
                        pantherAssay = new Business.HL7View.Panther.PantherAssayTrich();
                        break;
                    default:
                        throw new Exception(this.m_PanelSetOrder.PanelSetName +  " is mot implemented yet.");
                }

                this.m_PanelSetOrder.OrderedOnId = aliquotOrder.AliquotOrderId;
                this.m_PanelSetOrder.OrderedOn = YellowstonePathology.Business.Specimen.Model.OrderedOn.Aliquot;

                YellowstonePathology.Business.HL7View.Panther.PantherOrder pantherOrder = new Business.HL7View.Panther.PantherOrder(pantherAssay, specimenOrder, aliquotOrder, this.m_AccessionOrder, this.m_PanelSetOrder, YellowstonePathology.Business.HL7View.Panther.PantherActionCode.NewSample);
                pantherOrder.Send();                    
            }
        }

        private void HandleAddAliquotOnOrder()
        {
            if (this.m_PanelSet.AddAliquotOnOrder == true)
            {                
                YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = (YellowstonePathology.Business.Specimen.Model.SpecimenOrder)this.m_TestOrderInfo.OrderTarget;
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
            this.m_ReportNo = this.m_AccessionOrder.GetNextReportNo(this.m_PanelSet);
            string objectId = MongoDB.Bson.ObjectId.GenerateNewId().ToString();

            bool distribute = this.m_TestOrderInfo.Distribute;
            if (this.m_PanelSet.NeverDistribute == true)
            {
                distribute = false;
            }

            this.m_PanelSetOrder = null;
            if (this.m_PanelSet.HasNoOrderTarget == true)
            {
                this.m_PanelSetOrder = YellowstonePathology.Business.Test.PanelSetOrderFactory.CreatePanelSetOrder(this.m_AccessionOrder.MasterAccessionNo, this.m_ReportNo, objectId, this.m_PanelSet, distribute);
            }
            else
            {
                if (this.m_OrderTargetIsKnow == false)
                {
                    this.m_OrderTarget = this.m_AccessionOrder.SpecimenOrderCollection.GetOrderTarget(this.m_PanelSet.OrderTargetTypeCollectionRestrictions);
                }
                this.m_PanelSetOrder = YellowstonePathology.Business.Test.PanelSetOrderFactory.CreatePanelSetOrder(this.m_AccessionOrder.MasterAccessionNo, this.m_ReportNo, objectId, this.m_PanelSet, this.m_OrderTarget, distribute);
            }            

            this.m_AccessionOrder.PanelSetOrderCollection.Add(this.m_PanelSetOrder);
            this.m_AccessionOrder.UpdateCaseAssignment(this.m_PanelSetOrder);
			this.m_TestOrderInfo.PanelSetOrder = this.m_PanelSetOrder;
        }

        public virtual void HandlePanelOrders()
        {
            foreach (YellowstonePathology.Business.Panel.Model.Panel panel in this.m_PanelSet.PanelCollection)
            {
                string panelOrderId = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
                YellowstonePathology.Business.Test.PanelOrder panelOrder = YellowstonePathology.Business.Test.PanelOrderFactory.GetPanelOrder(this.m_ReportNo, panelOrderId, panelOrderId, panel, YellowstonePathology.Business.User.SystemIdentity.Instance.User.UserId);
                this.m_PanelSetOrder.PanelOrderCollection.Add(panelOrder);

                if (panel.AcknowledgeOnOrder == true)
                {
                    panelOrder.Acknowledged = true;
                    panelOrder.AcknowledgedById = YellowstonePathology.Business.User.SystemIdentity.Instance.User.UserId;
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
                    panelOrder.TestOrderCollection.Add(testOrder);
                }
            }
        }

        public virtual void HandlDistribution()
        {
            if (this.m_AccessionOrder.ClientId != 0 && this.m_AccessionOrder.PhysicianId != 0)
            {
                if (this.m_PanelSet.NeverDistribute == false)
                {
                    if (this.m_PanelSetOrder.Distribute == true)
                    {
                        YellowstonePathology.Business.Client.Model.PhysicianClientDistributionList physicianClientDistributionCollection = YellowstonePathology.Business.Gateway.ReportDistributionGateway.GetPhysicianClientDistributionCollection(this.m_AccessionOrder.PhysicianId, this.m_AccessionOrder.ClientId);
                        physicianClientDistributionCollection.SetDistribution(this.m_PanelSetOrder, this.m_AccessionOrder);
                    }
                }
            }
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
			foreach (YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder in this.m_AccessionOrder.SpecimenOrderCollection)
            {
                if (this.m_PanelSet.OrderTargetTypeCollectionExclusions.Exists(specimenOrder) == false)
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
