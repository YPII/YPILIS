using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Visitor
{
    public class StainAcknowledgementTaskOrderVisitor : AccessionTreeVisitor
    {
        private YellowstonePathology.Business.User.SystemIdentity m_SystemIdentity;
        private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;        
        private YellowstonePathology.Business.Test.PanelSetOrder m_PanelSetOrder;
		private YellowstonePathology.Business.Task.Model.TaskOrderStainAcknowledgement m_TaskOrderStainAcknowlegedment;
        private YellowstonePathology.Business.Test.Model.TestOrderCollection m_AddedTestOrderCollection;
        private YellowstonePathology.Business.Test.Model.TestOrderCollection m_CancelledTestOrderCollection;
		private YellowstonePathology.Business.Task.Model.TaskOrderDetail m_TaskOrderDetailAddedTestOrders;
		private YellowstonePathology.Business.Task.Model.TaskOrderDetail m_TaskOrderDetailCancelledTestOrders;  
        private string m_TaskOrderDetailComment;

        public StainAcknowledgementTaskOrderVisitor(YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder) 
            : base(true, true)
        {
            this.m_PanelSetOrder = panelSetOrder;
            this.m_SystemIdentity = YellowstonePathology.Business.User.SystemIdentity.Instance;

            this.m_AddedTestOrderCollection = new YellowstonePathology.Business.Test.Model.TestOrderCollection();
            this.m_CancelledTestOrderCollection = new YellowstonePathology.Business.Test.Model.TestOrderCollection();
        }

        public override void Visit(Test.AccessionOrder accessionOrder)
        {
            this.m_AccessionOrder = accessionOrder;
            this.HandleAddedTestOrders();
            this.HandleRemovedTestOrders();            
        }

        public string TaskOrderDetailComment
        {
            get { return this.m_TaskOrderDetailComment; }
            set { this.m_TaskOrderDetailComment = value; }
        }

		public YellowstonePathology.Business.Task.Model.TaskOrderStainAcknowledgement TaskOrderStainAcknowlegedment
		{
			get { return this.m_TaskOrderStainAcknowlegedment; }
		}

        public void SetTaskOrderDetailCommentFirstLine(string comment)
        {            
            if (this.m_TaskOrderDetailAddedTestOrders != null)
            {
                this.m_TaskOrderDetailAddedTestOrders.Comment = comment + Environment.NewLine + this.m_TaskOrderDetailAddedTestOrders.Comment;
            }

            if (this.m_TaskOrderDetailCancelledTestOrders != null)
            {
                this.m_TaskOrderDetailCancelledTestOrders.Comment = comment + Environment.NewLine + this.m_TaskOrderDetailCancelledTestOrders.Comment;
            }
        }

        private void HandleAddedTestOrders()
        {
            if (this.m_AddedTestOrderCollection.Count != 0)
            {
                this.AddTaskOrderIfNull();
                this.AddTaskOrderDetailTestOrdersAddedIfNull();
                this.SetTestOrdersAddedComment();
            }            
        }

        private void HandleRemovedTestOrders()
        {
            if (this.m_CancelledTestOrderCollection.Count != 0)
            {
                this.AddTaskOrderIfNull();
                this.AddTaskOrderDetailTestOrdersCancelledIfNull();
                this.SetTestOrdersCancelledComment();
            }
            else if (this.m_TaskOrderStainAcknowlegedment != null)
            {
                if (this.m_TaskOrderStainAcknowlegedment.TaskOrderDetailCollection.Count == 0)
                {
                    this.m_AccessionOrder.TaskOrderCollection.Remove(this.m_TaskOrderStainAcknowlegedment);
                    this.m_TaskOrderStainAcknowlegedment = null;
                }
            }
        }

        private void AddTaskOrderIfNull()
        {
            if (this.m_TaskOrderStainAcknowlegedment == null)
            {
				string taskOrderId = YellowstonePathology.Business.OrderIdParser.GetNextTaskOrderId(this.m_AccessionOrder.TaskOrderCollection, this.m_AccessionOrder.MasterAccessionNo);
                string objectId = MongoDB.Bson.ObjectId.GenerateNewId().ToString();

				this.m_TaskOrderStainAcknowlegedment = new YellowstonePathology.Business.Task.Model.TaskOrderStainAcknowledgement(taskOrderId, objectId, this.m_AccessionOrder, this.m_PanelSetOrder, this.m_SystemIdentity);                
                this.m_AccessionOrder.TaskOrderCollection.Add(this.m_TaskOrderStainAcknowlegedment);
            }
        }

        private void AddTaskOrderDetailTestOrdersAddedIfNull()
        {
			YellowstonePathology.Business.Task.Model.TaskAcknowledgeStainOrder taskAcknowledgeStainOrder = new YellowstonePathology.Business.Task.Model.TaskAcknowledgeStainOrder();
            if (this.m_TaskOrderDetailAddedTestOrders == null)
            {
				string taskOrderDetailId = YellowstonePathology.Business.OrderIdParser.GetNextTaskOrderDetailId(this.m_TaskOrderStainAcknowlegedment.TaskOrderDetailCollection, this.m_TaskOrderStainAcknowlegedment.TaskOrderId);
                string taskOrderDetailObjectId = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
				this.m_TaskOrderDetailAddedTestOrders = new YellowstonePathology.Business.Task.Model.TaskOrderDetail(taskOrderDetailId, this.m_TaskOrderStainAcknowlegedment.TaskOrderId, taskOrderDetailObjectId, taskAcknowledgeStainOrder);                
                this.m_TaskOrderStainAcknowlegedment.TaskOrderDetailCollection.Add(this.m_TaskOrderDetailAddedTestOrders);
            }
        }

        private void AddTaskOrderDetailTestOrdersCancelledIfNull()
        {
			YellowstonePathology.Business.Task.Model.TaskAcknowledgeStainOrder taskAcknowledgeStainOrder = new YellowstonePathology.Business.Task.Model.TaskAcknowledgeStainOrder();
            if (this.m_TaskOrderDetailCancelledTestOrders == null)
            {
				string taskOrderDetailId = YellowstonePathology.Business.OrderIdParser.GetNextTaskOrderDetailId(this.m_TaskOrderStainAcknowlegedment.TaskOrderDetailCollection, this.m_TaskOrderStainAcknowlegedment.TaskOrderId);
                string taskOrderDetailObjectId = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
				this.m_TaskOrderDetailCancelledTestOrders = new YellowstonePathology.Business.Task.Model.TaskOrderDetail(taskOrderDetailId, this.m_TaskOrderStainAcknowlegedment.TaskOrderId, taskOrderDetailObjectId, taskAcknowledgeStainOrder);                
                this.m_TaskOrderStainAcknowlegedment.TaskOrderDetailCollection.Add(this.m_TaskOrderDetailCancelledTestOrders);
            }
        }

        public void AddTestOrder(YellowstonePathology.Business.Test.Model.TestOrder testOrder)
        {                       
            this.m_AddedTestOrderCollection.Add(testOrder);            
        }

        public void RemoveTestOrder(YellowstonePathology.Business.Test.Model.TestOrder testOrder)
        {            
            
            if (this.m_AddedTestOrderCollection.Exists(testOrder.TestOrderId) == true)
            {
                this.m_AddedTestOrderCollection.Remove(testOrder.TestOrderId);
            }
            else
            {                    
                this.m_CancelledTestOrderCollection.Add(testOrder);
            }

            if (this.m_AddedTestOrderCollection.Count == 0 && this.m_TaskOrderDetailAddedTestOrders != null)
            {
                this.m_TaskOrderStainAcknowlegedment.TaskOrderDetailCollection.Remove(this.m_TaskOrderDetailAddedTestOrders);
                this.m_TaskOrderDetailAddedTestOrders = null;
            }
        }

        public void SetTestOrdersAddedComment()
        {
            YellowstonePathology.Business.Test.Model.TestCollection allTests = YellowstonePathology.Business.Test.Model.TestCollection.GetAllTests();            
            StringBuilder taskOrderDetailDescription = new StringBuilder();
            taskOrderDetailDescription.AppendLine("The following stains have been ordered:");

            foreach (YellowstonePathology.Business.Test.Model.TestOrder testOrder in this.m_AddedTestOrderCollection)
            {
                YellowstonePathology.Business.Test.Model.Test test = allTests.GetTest(testOrder.TestId);
                if (test.NeedsAcknowledgement == true)
                {
                    taskOrderDetailDescription.AppendLine(testOrder.DisplayString);
                }
            }

            this.m_TaskOrderDetailAddedTestOrders.Description = taskOrderDetailDescription.ToString().Trim();
            this.m_TaskOrderDetailAddedTestOrders.Comment = this.m_TaskOrderDetailComment;
        }

        public void SetTestOrdersCancelledComment()
        {
            YellowstonePathology.Business.Test.Model.TestCollection allTests = YellowstonePathology.Business.Test.Model.TestCollection.GetAllTests();
            StringBuilder taskOrderDetailDescription = new StringBuilder();            
            taskOrderDetailDescription.AppendLine("The following stains have been cancelled:");

            foreach (YellowstonePathology.Business.Test.Model.TestOrder testOrder in this.m_CancelledTestOrderCollection)
            {
                YellowstonePathology.Business.Test.Model.Test test = allTests.GetTest(testOrder.TestId);
                if (test.NeedsAcknowledgement == true)
                {
                    taskOrderDetailDescription.AppendLine(testOrder.DisplayString);
                }
            }
            this.m_TaskOrderDetailCancelledTestOrders.Description = taskOrderDetailDescription.ToString().Trim();
            this.m_TaskOrderDetailCancelledTestOrders.Comment = this.m_TaskOrderDetailComment;
        }
    }
}
