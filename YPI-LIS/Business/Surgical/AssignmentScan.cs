using System;
using System.ComponentModel;

namespace YellowstonePathology.Business.Surgical
{
    public class AssignmentScan :INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string m_MasterAccessionNo;
        private string m_ScanId;
        private string m_AssignedTo;

        public AssignmentScan()
        {
        }

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        public string MasterAccessionNo
        {
            get { return this.m_MasterAccessionNo; }
            set
            {
                if(this.m_MasterAccessionNo != value)
                {
                    this.m_MasterAccessionNo = value;
                    this.NotifyPropertyChanged("MasterAccessionNo");
                }
            }
        }

        public string ScanId
        {
            get { return this.m_ScanId; }
            set
            {
                if (this.m_ScanId != value)
                {
                    this.m_ScanId = value;
                    this.NotifyPropertyChanged("ScanId");
                }
            }
        }

        public string AssignedTo
        {
            get { return this.m_AssignedTo; }
            set
            {
                if (this.m_AssignedTo != value)
                {
                    this.m_AssignedTo = value;
                    this.NotifyPropertyChanged("AssignedTo");
                }
            }
        }
    }
}
