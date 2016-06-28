using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace YellowstonePathology.UI
{
    public class TaskUI : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
        private YellowstonePathology.Business.User.SystemUserCollection m_LogUsers;
        private YellowstonePathology.Business.Task.Model.TaskOrderCollection m_TaskOrderCollection;
        private YellowstonePathology.Business.Task.Model.TaskOrderCollection m_DailyTaskOrderCollection;

        private List<string> m_TaskAcknowledgementTypeList;

        private string m_SelectedItemCount;
        private object m_Writer;

        public TaskUI(object writer)
        {
            this.m_Writer = writer;
            this.m_LogUsers = YellowstonePathology.Business.User.SystemUserCollectionInstance.Instance.SystemUserCollection.GetUsersByRole(YellowstonePathology.Business.User.SystemUserRoleDescriptionEnum.Log, true);
            this.m_TaskAcknowledgementTypeList = YellowstonePathology.Business.Task.Model.TaskAcknowledgementType.GetAll();

            this.GetTaskOrderCollection();
            this.GetDailyTaskOrderCollection();

            YellowstonePathology.UI.TaskNotifier.Instance.Notifier.Alert += new TaskNotifier.AlertEventHandler(Notifier_Alert);
        }

        private void Notifier_Alert(object sender, CustomEventArgs.TaskOrderCollectionReturnEventArgs e)
        {
            this.m_TaskOrderCollection = e.TaskOrderCollection;
            this.NotifyPropertyChanged("TaskOrderCollection");
            YellowstonePathology.UI.TaskNotifier.Instance.Notifier.Alert -= Notifier_Alert;
        }

        public YellowstonePathology.Business.User.SystemIdentity SystemIdentity
        {
            get { return Business.User.SystemIdentity.Instance; }
        }

        public string SelectedItemCount
        {
            get { return this.m_SelectedItemCount; }
            set
            {
                if (this.m_SelectedItemCount != value)
                {
                    this.m_SelectedItemCount = value;
                    this.NotifyPropertyChanged("SelectedItemCount");
                }
            }
        }

        public YellowstonePathology.Business.Test.AccessionOrder AccessionOrder
        {
            get { return this.m_AccessionOrder; }
            set
            {
                this.m_AccessionOrder = value;
                this.NotifyPropertyChanged("AccessionOrder");
            }
        }

        public YellowstonePathology.Business.User.SystemUserCollection LogUsers
        {
            get { return this.m_LogUsers; }
        }

        public YellowstonePathology.Business.Task.Model.TaskOrderCollection TaskOrderCollection
        {
            get { return this.m_TaskOrderCollection; }
        }

        public YellowstonePathology.Business.Task.Model.TaskOrderCollection DailyTaskOrderCollection
        {
            get { return this.m_DailyTaskOrderCollection; }
        }

        public List<string> TaskAcknowledgementTypeList
        {
            get { return this.m_TaskAcknowledgementTypeList; }
        }

        public void GetTaskOrderCollection()
        {
            this.m_TaskOrderCollection = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetTaskOrderCollection(YellowstonePathology.Business.Task.Model.TaskAcknowledgementType.Immediate);
            this.NotifyPropertyChanged("TaskOrderCollection");
        }

        public void GetTasksNotAcknowledged()
        {
            string assignedTo = YellowstonePathology.Business.User.UserPreferenceInstance.Instance.UserPreference.AcknowledgeTasksFor;
            this.m_TaskOrderCollection = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetTasksNotAcknowledged(assignedTo, YellowstonePathology.Business.Task.Model.TaskAcknowledgementType.Immediate);
            this.NotifyPropertyChanged("TaskOrderCollection");
        }

        public void GetDailyTaskOrderCollection()
        {
            this.m_DailyTaskOrderCollection = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetDailyTaskOrderCollection();
            this.NotifyPropertyChanged("DailyTaskOrderCollection");
        }

        public void GetDailyTaskOrderHistoryCollection()
        {
            this.m_DailyTaskOrderCollection = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetDailyTaskOrderHistoryCollection(30);
            this.NotifyPropertyChanged("DailyTaskOrderCollection");
        }

        public void ViewLabOrderLog(DateTime orderDate)
        {
            string rptpath = @"\\CFileServer\documents\Reports\Lab\LabOrdersLog\YEAR\MONTH\LabOrdersLog.FILEDATE.v1.xml";

            string rptName = rptpath.Replace("YEAR", orderDate.ToString("yyyy"));
            rptName = rptName.Replace("MONTH", orderDate.ToString("MMMM"));
            rptName = rptName.Replace("FILEDATE", orderDate.ToString("MM.dd.yy"));

            YellowstonePathology.Business.Reports.LabOrdersLog labOrdersLog = new YellowstonePathology.Business.Reports.LabOrdersLog();
            labOrdersLog.CreateReport(orderDate);

            string holdRptName = string.Empty;
            do
            {
                holdRptName = rptName;
                int vLocation = rptName.IndexOf(".v");
                int originalVersion = Convert.ToInt32(rptName.Substring(vLocation + 2, 1));
                int newVersion = originalVersion + 1;
                rptName = rptName.Replace(".v" + originalVersion.ToString(), ".v" + newVersion.ToString());
            } while (System.IO.File.Exists(rptName));

            YellowstonePathology.Business.Document.CaseDocument.OpenWordDocumentWithWordViewer(holdRptName);
        }

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
    }
}
