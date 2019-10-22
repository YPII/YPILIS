using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.ComponentModel;

namespace YellowstonePathology.Business
{
    public class PathologyCalendar : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private DateTime m_Date;
        private string m_Bibbey;

        public PathologyCalendar()
        {

        }

        public string Bibbey
        {
            get { return this.m_Bibbey; }
            set
            {
                if (this.m_Bibbey != value)
                {
                    this.m_Bibbey = value;
                    this.NotifyPropertyChanged("Bibbey");
                }
            }
        }

        public string ToJSON()
        {            
            return JsonConvert.SerializeObject(this, Formatting.Indented);
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
