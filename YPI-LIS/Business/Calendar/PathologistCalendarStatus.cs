using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace YellowstonePathology.Business.Calendar
{
    public class PathologistCalendarStatus : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string m_PathologistName;
        private string m_Status;

        public PathologistCalendarStatus()
        { }

        public PathologistCalendarStatus(string pathologistName, string status)
        {
            this.m_PathologistName = pathologistName;
            this.m_Status = status;
        }

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        public string PathologistName
        {
            get { return this.m_PathologistName; }
            set
            {
                if (this.m_PathologistName != value)
                {
                    this.m_PathologistName = value;
                    this.NotifyPropertyChanged("PathologistName");
                }
            }
        }

        public string Status
        {
            get { return this.m_Status; }
            set
            {
                if (this.m_Status != value)
                {
                    this.m_Status = value;
                    this.NotifyPropertyChanged("Status");
                }
            }
        }
    }
}
