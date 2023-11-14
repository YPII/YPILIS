using System;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Task.Model
{
    [PersistentClass("tblTaskOrderDetailFax", "tblTaskOrderDetail", "YPIDATA")]
    public class TaskOrderDetailFax : TaskOrderDetail
    {
        private string m_FaxNumber;
        private string m_SendToName;
        private string m_DocumentName;

        public TaskOrderDetailFax()
        {
        }

        public TaskOrderDetailFax(string taskOrderDetailId, string taskOrderId, string objectId, TaskFax task, int clientId) 
            : base(taskOrderDetailId, taskOrderId, objectId, task, clientId)
        {
            TaskFax taskFax = (Model.TaskFax)task;
            this.TaskId = taskFax.TaskId;
            this.m_DocumentName = task.DocumentName;
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "50", "null", "varchar")]
        public string FaxNumber
        {
            get { return this.m_FaxNumber; }
            set
            {
                if (this.m_FaxNumber != value)
                {
                    this.m_FaxNumber = value;
                    this.NotifyPropertyChanged("FaxNumber");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "50", "null", "varchar")]
        public string SendToName
        {
            get { return this.m_SendToName; }
            set
            {
                if (this.m_SendToName != value)
                {
                    this.m_SendToName = value;
                    this.NotifyPropertyChanged("SendToName");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "50", "null", "varchar")]
        public string DocumentName
        {
            get { return this.m_DocumentName; }
            set
            {
                if (this.m_DocumentName != value)
                {
                    this.m_DocumentName = value;
                    this.NotifyPropertyChanged("DocumentName");
                }
            }
        }

        public string FaxNumberProxy
        {
            get { return YellowstonePathology.Business.Helper.PhoneNumberHelper.CorrectPhoneNumber(this.m_FaxNumber); }
            set
            {
                if (this.m_FaxNumber != value)
                {
                    this.m_FaxNumber = value;
                    this.NotifyPropertyChanged("FaxNumber");
                    this.NotifyPropertyChanged("FaxNumberProxy");
                }
            }
        }

        public bool PropertiesAreEnabled
        {
            get
            {
                bool result = false;
                if (string.IsNullOrEmpty(this.m_FaxNumber) == true ||
                    string.IsNullOrEmpty(this.m_SendToName) == true) result = true;
                return result;
            }
        }
    }
}
