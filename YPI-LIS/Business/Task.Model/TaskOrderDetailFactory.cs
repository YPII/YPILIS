using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Task.Model
{
    public class TaskOrderDetailFactory
    {
        public static TaskOrderDetail GetTaskOrderDetail(string taskId)
        {
            TaskOrderDetail result = null;
            switch(taskId)
            {
                case "FDXSHPMNT":
                    result = new TaskOrderDetailFedexShipment();
                    break;
                case "TSKFAX":
                    result = new TaskOrderDetailFax();
                    break;
                default:
                    result = new TaskOrderDetail();
                    break;
            }
            return result;
        }

        public static TaskOrderDetail GetTaskOrderDetail(string taskOrderDetailId, string taskOrderId, string objectId, Task task, int clientId)
        {
            TaskOrderDetail result = null;
            switch (task.TaskId)
            {
                case "FDXSHPMNT":
                    result = new TaskOrderDetailFedexShipment(taskOrderDetailId, taskOrderId, objectId, task, clientId);
                    break;
                case "TSKFAX":
                    result = new TaskOrderDetailFax(taskOrderDetailId, taskOrderId, objectId, task as TaskFax, clientId);
                    break;
                default:
                    result = new TaskOrderDetail(taskOrderDetailId, taskOrderId, objectId, task, clientId);
                    break;
            }
            return result;
        }
    }
}
