using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Visitor
{
    public class OrderTestVisitor : AccessionTreeVisitor
    {        
        private string m_ReportNo;
        private bool m_OrderedAsDual;
        private bool m_AcknowledgeOnOrder;
        private bool m_OrderAsSlide;
        private string m_TestOrderComment;
        private string m_PanelOrderComment;

        private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
        private YellowstonePathology.Business.User.SystemIdentity m_SystemIdentity;
        private YellowstonePathology.Business.Test.Model.Test m_Test;
        private YellowstonePathology.Business.Test.AliquotOrder m_AliquotOrder;        
        private YellowstonePathology.Business.Test.Model.TestOrder m_TestOrder;
        private YellowstonePathology.Business.Test.PanelOrder m_PanelOrder;
        private YellowstonePathology.Business.Test.PanelSetOrder m_PanelSetOrder;
        private YellowstonePathology.Business.Test.Surgical.SurgicalSpecimen m_SurgicalSpecimen;
		private YellowstonePathology.Business.Task.Model.TaskOrderCollection m_TaskOrderCollection;

        public OrderTestVisitor(string reportNo, YellowstonePathology.Business.Test.Model.Test test, string testOrderComment, string panelOrderComment, bool orderedAsDual,
			YellowstonePathology.Business.Test.AliquotOrder aliquotOrder, bool acknowledgeOnOrder, bool orderAsSlide,
            YellowstonePathology.Business.Task.Model.TaskOrderCollection taskOrderCollection) 
            : base(true, true)
        {
            this.m_ReportNo = reportNo;
            this.m_Test = test;            
            this.m_OrderedAsDual = orderedAsDual;
            this.m_TestOrderComment = testOrderComment;
            this.m_PanelOrderComment = panelOrderComment;
            this.m_AliquotOrder = aliquotOrder;
            this.m_SystemIdentity = YellowstonePathology.Business.User.SystemIdentity.Instance;
            this.m_AcknowledgeOnOrder = acknowledgeOnOrder;
            this.m_OrderAsSlide = orderAsSlide;
			this.m_TaskOrderCollection = taskOrderCollection;
        }

        public override void Visit(Test.AccessionOrder accessionOrder)
        {
            this.m_AccessionOrder = accessionOrder;

            YellowstonePathology.Business.ReportDistribution.Model.MultiTestDistributionHandler multiTestDistributionHandler = YellowstonePathology.Business.ReportDistribution.Model.MultiTestDistributionHandlerFactory.GetHandler(this.m_AccessionOrder);
            multiTestDistributionHandler.Set();
        }

        public override void Visit(YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder)
        {
            if (panelSetOrder.ReportNo == this.m_ReportNo)
            {
                this.m_PanelSetOrder = panelSetOrder;
                this.HandlePanelOrder();
                this.HandleTestOrder();                
                this.HandleSlideOrder();
            }
        }

        public override void Visit(YellowstonePathology.Business.Test.Surgical.SurgicalSpecimen surgicalSpecimen)
        {            
            if (surgicalSpecimen.SpecimenOrderId == this.m_AliquotOrder.SpecimenOrderId)
            {
                this.m_SurgicalSpecimen = surgicalSpecimen;
                this.HandleIC();
                this.HandleStainResult();
            }
        }

        private void HandleIC()
        {            
            if (this.m_SurgicalSpecimen.IntraoperativeConsultationResultCollection.Count == 0) // don't add one if one already exists.
            {
                if (this.m_TestOrder.TestId == 45 || this.m_TestOrder.TestId == 194)
                {
                    YellowstonePathology.Business.Test.Surgical.IntraoperativeConsultationResult intraoperativeConsultationResult = this.m_SurgicalSpecimen.IntraoperativeConsultationResultCollection.GetNextItem(this.m_SurgicalSpecimen.SurgicalSpecimenId);
                    intraoperativeConsultationResult.TestOrderId = this.m_TestOrder.TestOrderId;
                    this.m_SurgicalSpecimen.IntraoperativeConsultationResultCollection.Add(intraoperativeConsultationResult);
                }
            }            
        }

        private void HandleStainResult()
        {            
            YellowstonePathology.Business.Test.Model.StainTest stainTest = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetStainTestByTestId(this.m_TestOrder.TestId);
            if (stainTest != null && !string.IsNullOrEmpty(stainTest.CptCode))
            {
                YellowstonePathology.Business.SpecialStain.StainResultItem stainResultItem = this.m_SurgicalSpecimen.StainResultItemCollection.GetNextItem(this.m_SurgicalSpecimen.SurgicalSpecimenId);
                stainResultItem.TestOrderId = this.m_TestOrder.TestOrderId;
                stainResultItem.ProcedureName = this.m_TestOrder.TestName;
                stainResultItem.CptCode = stainTest.CptCode;
                stainResultItem.CptCodeQuantity = stainTest.CptCodeQuantity;
                stainResultItem.ControlComment = stainTest.ControlComment;
                stainResultItem.StainType = stainTest.StainType;

                stainResultItem.Billable = true;
                stainResultItem.Reportable = true;

                if (stainTest.ImmunoCommentId > 0)
                {
                    YellowstonePathology.Business.Domain.ImmunoComment immunoComment = Business.Gateway.AccessionOrderGateway.GetImmunoCommentByImmunocommentId(stainTest.ImmunoCommentId);
                    stainResultItem.ImmunoComment = immunoComment.Comment;
                }
                this.m_SurgicalSpecimen.StainResultItemCollection.Add(stainResultItem);
            }             
        }

        private void HandlePanelOrder()
        {                        
            if (this.m_Test.NeedsAcknowledgement == true && this.m_AcknowledgeOnOrder == false)
            {                
                YellowstonePathology.Business.Panel.Model.SpecialStainPanel specialStainPanel = new YellowstonePathology.Business.Panel.Model.SpecialStainPanel();                    
                string objectId = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
                this.m_PanelOrder = new Test.PanelOrder(this.m_ReportNo, objectId, objectId, specialStainPanel, this.m_SystemIdentity.User.UserId);
                this.m_PanelOrder.Comment = this.m_PanelOrderComment;
                this.m_PanelSetOrder.PanelOrderCollection.Add(this.m_PanelOrder);                         
            }
            else
            {
                if (this.m_PanelSetOrder.PanelOrderCollection.HasInitialPanel() == true)
                {
                    this.m_PanelOrder = this.m_PanelSetOrder.PanelOrderCollection.GetInitialPanel();
                }
                else
                {
                    YellowstonePathology.Business.Panel.Model.InitialPanel initialPanel = new YellowstonePathology.Business.Panel.Model.InitialPanel();
                    string objectId = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
                    this.m_PanelOrder = new Test.PanelOrder(this.m_ReportNo, objectId, objectId, initialPanel, this.m_SystemIdentity.User.UserId);                    
                    this.m_PanelSetOrder.PanelOrderCollection.Add(this.m_PanelOrder);         
                }                
            }            
        }

        private void HandleTestOrder()
        {
            string objectId = MongoDB.Bson.ObjectId.GenerateNewId().ToString();            
            YellowstonePathology.Business.Test.Model.TestOrder testOrder = this.m_PanelOrder.TestOrderCollection.GetNextItem(this.m_PanelOrder.PanelOrderId, objectId, this.m_AliquotOrder.AliquotOrderId, this.m_Test, this.m_TestOrderComment);
            testOrder.OrderedAsDual = this.m_OrderedAsDual;
            testOrder.AliquotOrder = this.m_AliquotOrder;

            this.m_PanelOrder.TestOrderCollection.Add(testOrder);
            this.m_TestOrder = testOrder;

			this.m_AliquotOrder.TestOrderCollection.Add(this.m_TestOrder);
			this.m_AliquotOrder.SetLabelPrefix(testOrder, true);
		}        

        private void HandleSlideOrder()
        {
            if (this.m_OrderAsSlide == true)
            {
                string slideOrderId = YellowstonePathology.Business.OrderIdParser.GetNextSlideOrderId(this.m_AliquotOrder.SlideOrderCollection, this.m_AliquotOrder.AliquotOrderId);
                string slideOrderObjectId = MongoDB.Bson.ObjectId.GenerateNewId().ToString();

                YellowstonePathology.Business.Visitor.AddSlideOrderVisitor addSlideOrderVisitor = new AddSlideOrderVisitor(this.m_AliquotOrder, this.m_TestOrder);
                this.m_AccessionOrder.TakeATrip(addSlideOrderVisitor);                
            }
        }

        public string ReportNo
        {
            get { return this.m_ReportNo; }
        }

        public YellowstonePathology.Business.Test.Model.Test Test
        {
            get { return this.m_Test; }
        }        

        public YellowstonePathology.Business.User.SystemIdentity SystemIdentity
        {
            get { return this.m_SystemIdentity; }
        }

        public YellowstonePathology.Business.Test.AliquotOrder AliquotOrder
        {
            get { return this.m_AliquotOrder; }
        }

        public bool OrderedAsDual
        {
            get { return this.m_OrderedAsDual; }
        }

        public YellowstonePathology.Business.Test.Model.TestOrder TestOrder
        {
            get { return this.m_TestOrder; }
            set { this.m_TestOrder = value; }
        }
    }
}
