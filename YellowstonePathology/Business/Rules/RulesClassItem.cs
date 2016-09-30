using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Rules
{
    public class RulesClassItem : INotifyPropertyChanged
    {
        public delegate void PropertyChangedNotificationHandler(String info);
        public event PropertyChangedEventHandler PropertyChanged;

        Type m_ClassType;

        public RulesClassItem()
        {

        }

        public string TypeString
        {
            get { return this.ClassType.Name; }
        }

        public Type ClassType
        {
            get { return this.m_ClassType; }
            set
            {
                this.m_ClassType = value;
                this.NotifyPropertyChanged("TypeString");
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
