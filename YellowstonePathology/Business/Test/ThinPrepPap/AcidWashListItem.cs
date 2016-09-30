using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YellowstonePathology.Business.Persistence;
using System.ComponentModel;

namespace YellowstonePathology.Business.Test.ThinPrepPap
{
    public class AcidWashListItem: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string m_MasterAccessionNo;
        private string m_ReportNo;
        private DateTime m_OrderTime;
        private string m_PLastName;
        private string m_PFirstName;
        private string m_PMiddleInitial;
        private bool m_Accepted;
        private string m_Comment;
        private Business.Monitor.Model.MonitorStateEnum m_State;

        public AcidWashListItem()
        {

        }

        public string PatientName
        {
            get
            {
                return Helper.PatientHelper.GetPatientDisplayName(this.m_PLastName, this.m_PFirstName, this.m_PMiddleInitial);
            }
        }

        [PersistentProperty()]
        public string MasterAccessionNo
        {
            get { return this.m_MasterAccessionNo; }
            set { this.m_MasterAccessionNo = value; }
        }

        [PersistentProperty()]
        public string ReportNo
        {
            get { return this.m_ReportNo; }
            set { this.m_ReportNo = value; }
        }

        [PersistentProperty()]
        public string PLastName
        {
            set { this.m_PLastName = value; }
        }

        [PersistentProperty()]
        public string PFirstName
        {
            set { this.m_PFirstName = value; }
        }

        [PersistentProperty()]
        public string PMiddleInitial
        {
            set { this.m_PMiddleInitial = value; }
        }

        [PersistentProperty()]
        public DateTime OrderTime
        {
            get { return this.m_OrderTime; }
            set { this.m_OrderTime = value; }
        }

        [PersistentProperty()]
        public bool Accepted
        {
            get { return this.m_Accepted; }
            set { this.m_Accepted = value; }
        }

        [PersistentProperty()]
        public string Comment
        {
            get { return this.m_Comment; }
            set { this.m_Comment = value; }
        }

        public Business.Monitor.Model.MonitorStateEnum State
        {
            get { return this.m_State; }
            set
            {
                if (this.m_State != value)
                {
                    this.m_State = value;
                    this.NotifyPropertyChanged("State");
                }
            }
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
