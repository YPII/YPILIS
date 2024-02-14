using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Task.Model
{
	[PersistentClass(true, "tblTaskOrder", "YPIDATA")]
	public class TaskOrderStainAcknowledgement : TaskOrder
	{        
		public TaskOrderStainAcknowledgement()
		{

		}

        public TaskOrderStainAcknowledgement(string taskOrderId, string objectId,
            YellowstonePathology.Business.Test.AccessionOrder accessionOrder,
            YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder,                        
            YellowstonePathology.Business.User.SystemIdentity systemIdentity)            
        {            
            this.m_TaskOrderId = taskOrderId;
            this.m_ObjectId = objectId;
            this.m_MasterAccessionNo = accessionOrder.MasterAccessionNo;
            this.m_PanelSetName = panelSetOrder.PanelSetName;
            this.m_ReportNo = panelSetOrder.ReportNo;
            this.m_TaskName = "Acknowledge Stain Order";
            this.m_OrderedById = systemIdentity.User.UserId;
            this.m_OrderedByInitials = systemIdentity.User.Initials;
            this.m_OrderDate = DateTime.Now;
			this.m_AcknowledgementType = TaskAcknowledgementType.Immediate;

            this.m_TaskOrderDetailCollection = new TaskOrderDetailCollection();            
        }

        public override void Acknowledge(Test.AccessionOrder accessionOrder, Business.User.SystemIdentity systemIdentity)
        {
            base.Acknowledge(accessionOrder, systemIdentity);
        }

        public override void Acknowledge(TaskOrderDetail taskOrderDetail, Test.AccessionOrder accessionOrder, Business.User.SystemIdentity systemIdentity)
        {
            base.Acknowledge(taskOrderDetail, accessionOrder, systemIdentity);
        }          
	}
}
