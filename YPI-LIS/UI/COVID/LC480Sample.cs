using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YellowstonePathology.Business.Persistence;
using System.ComponentModel;
using System.Reflection;

namespace YellowstonePathology.UI.COVID
{
    public class LC480Sample: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        
        private string m_Position;
        private string m_SampleId;
        private string m_SampleName;
        private string m_FiltComb;                

        public LC480Sample()
        {

        }   

        public static List<PropertyInfo> GetPropertyList()
        { 
            List<PropertyInfo> result = new List<PropertyInfo>();
            Type type = typeof(LC480Sample);            
            result.Add(type.GetProperty("Position"));
            result.Add(type.GetProperty("SampleName"));
            result.Add(type.GetProperty("SampleId"));
            result.Add(type.GetProperty("FiltComb"));            
            return result;
        }                      

        [MappingAttribute("General:Pos")]
        public string Position
        {
            get { return this.m_Position; }
            set { this.m_Position = value; }
        }

        [MappingAttribute("General:Sample Name")]
        public string SampleName
        {
            get { return this.m_SampleName; }
            set 
            { 
                if(this.m_SampleName != value)
                {
                    this.m_SampleName = value;
                    this.NotifyPropertyChanged("SampleName");
                }                
            }
        }

        [MappingAttribute("General:Sample ID")]
        public string SampleId
        {
            get { return this.m_SampleId; }
            set 
            { 
                if(this.m_SampleId != value)
                {
                    this.m_SampleId = value;
                    this.NotifyPropertyChanged("SampleId");
                }                
            }
        }        

        [MappingAttribute("General:Filt. Comb.")]
        public string FiltComb
        {
            get { return this.m_FiltComb; }
            set 
            {
                if(this.m_FiltComb != value)
                {
                    this.m_FiltComb = value;
                    this.NotifyPropertyChanged(string.Empty);
                }
            }
        }                       

        public string GetCSV()
        {
            StringBuilder result = new StringBuilder();
            List<PropertyInfo> propertyList = LC480Sample.GetPropertyList();
            foreach (PropertyInfo property in propertyList)
            {   
                if(property.GetValue(this) != null)
                {
                    result.Append($"\"{property.GetValue(this).ToString()}\"\t");                    
                }
                else
                {
                    result.Append($"\t");
                }                
            }
            return result.ToString().Trim();
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
