using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business
{
    public class PreviousResult : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string m_Result;
        private string m_MasterAccessionNo;
        private string m_ReportNo;
        private DateTime m_AccessionTime;
        private DateTime m_AccessionDate;
        private DateTime m_FinalDate;
        private int m_PanelSetId;

        public PreviousResult() { }

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        [PersistentProperty()]
        public string Result
        {
            get { return this.m_Result; }
            set
            {
                if (this.m_Result != value)
                {
                    this.m_Result = value;
                    NotifyPropertyChanged("Result");
                }
            }
        }

        [PersistentProperty()]
        public string MasterAccessionNo
        {
            get { return this.m_MasterAccessionNo; }
            set
            {
                if (this.m_MasterAccessionNo != value)
                {
                    this.m_MasterAccessionNo = value;
                    NotifyPropertyChanged("MasterAccessionNo");
                }
            }
        }

        [PersistentProperty()]
        public string ReportNo
        {
            get { return this.m_ReportNo; }
            set
            {
                if (this.m_ReportNo != value)
                {
                    this.m_ReportNo = value;
                    NotifyPropertyChanged("ReportNo");
                }
            }
        }

        [PersistentProperty()]
        public DateTime AccessionTime
        {
            get { return this.m_AccessionTime; }
            set
            {
                if (this.m_AccessionTime != value)
                {
                    this.m_AccessionTime = value;
                    NotifyPropertyChanged("AccessionTime");
                }
            }
        }

        [PersistentProperty()]
        public DateTime AccessionDate
        {
            get { return this.m_AccessionDate; }
            set
            {
                if (this.m_AccessionDate != value)
                {
                    this.m_AccessionDate = value;
                    NotifyPropertyChanged("AccessionDate");
                }
            }
        }

        [PersistentProperty()]
        public DateTime FinalDate
        {
            get { return this.m_FinalDate; }
            set
            {
                if (this.m_FinalDate != value)
                {
                    this.m_FinalDate = value;
                    NotifyPropertyChanged("FinalDate");
                }
            }
        }

        [PersistentProperty()]
        public int PanelSetId
        {
            get { return this.m_PanelSetId; }
            set
            {
                if (this.m_PanelSetId != value)
                {
                    this.m_PanelSetId = value;
                    NotifyPropertyChanged("PanelSetId");
                }
            }
        }
    }
}
