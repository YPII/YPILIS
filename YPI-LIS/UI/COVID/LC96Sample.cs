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
    public class LC96Sample: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string m_Color;
        private string m_Position;
        private string m_SampleName;
        private string m_GeneName;
        private string m_ConditionName;
        private string m_SampleType;
        private string m_Concentration;
        private string m_Dye;        
        private string m_ReplicateGroup;
        private string m_Notes;        
        private string m_SamplePrepNotes;
        private string m_Number;

        private LC96Sample m_Control;

        public LC96Sample()
        {

        }   

        public static List<PropertyInfo> GetLC96PropertyList()
        { 
            List<PropertyInfo> result = new List<PropertyInfo>();
            Type type = typeof(LC96Sample);
            result.Add(type.GetProperty("Color"));
            result.Add(type.GetProperty("Position"));
            result.Add(type.GetProperty("SampleName"));
            result.Add(type.GetProperty("GeneName"));
            result.Add(type.GetProperty("ConditionName"));
            result.Add(type.GetProperty("SampleType"));
            result.Add(type.GetProperty("Concentration"));
            result.Add(type.GetProperty("Dye"));
            result.Add(type.GetProperty("ReplicateGroup"));
            result.Add(type.GetProperty("Notes"));
            result.Add(type.GetProperty("SamplePrepNotes"));
            result.Add(type.GetProperty("Number"));
            return result;
        }
        
        public LC96Sample Control
        {
            get { return this.m_Control; }
            set 
            { 
                if(this.m_Control != value)
                {
                    this.m_Control = value;
                    this.NotifyPropertyChanged("Control");
                }                
            }
        }

        [MappingAttribute("Color")]
        public string Color
        {
            get { return this.m_Color; }
            set { this.m_Color = value; }
        }

        [MappingAttribute("Position")]
        public string Position
        {
            get { return this.m_Position; }
            set { this.m_Position = value; }
        }

        [MappingAttribute("Sample Name")]
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

        [MappingAttribute("Gene Name")]
        public string GeneName
        {
            get { return this.m_GeneName; }
            set { this.m_GeneName = value; }
        }        

        [MappingAttribute("Condition Name")]
        public string ConditionName
        {
            get { return this.m_ConditionName; }
            set 
            {
                if(this.m_ConditionName != value)
                {
                    this.m_ConditionName = value;
                    this.NotifyPropertyChanged(string.Empty);
                }
            }
        }        
        
        [MappingAttribute("Sample Type")]
        public string SampleType
        {
            get { return this.m_SampleType; }
            set { this.m_SampleType = value; }
        }

        [MappingAttribute("Concentration")]
        public string Concentration
        {
            get { return this.m_Concentration; }
            set { this.m_Concentration = value; }
        }

        [MappingAttribute("Dye")]
        public string Dye
        {
            get { return this.m_Dye; }
            set { this.m_Dye = value; }
        }

        [MappingAttribute("Replicate Group")]
        public string ReplicateGroup
        {
            get { return this.m_ReplicateGroup; }
            set { this.m_ReplicateGroup = value; }
        }              

        [MappingAttribute("Notes")]
        public string Notes
        {
            get { return this.m_Notes; }
            set { this.m_Notes = value; }
        }

        [MappingAttribute("Sample Prep Notes")]
        public string SamplePrepNotes
        {
            get { return this.m_SamplePrepNotes; }
            set { this.m_SamplePrepNotes = value; }
        }

        [MappingAttribute("Number")]
        public string Number
        {
            get { return this.m_Number; }
            set { this.m_Number = value; }
        }

        public string GetLC96CSV()
        {
            StringBuilder result = new StringBuilder();
            List<PropertyInfo> propertyList = LC96Sample.GetLC96PropertyList();
            foreach (PropertyInfo property in propertyList)
            {   
                if(property.GetValue(this) != null)
                {
                    if(property.Name == "ConditionName")
                    {
                        result.Append($"\"{property.GetValue(this).ToString()}\",");
                    }
                    else
                    {
                        result.Append($"{property.GetValue(this).ToString()},");
                    }
                }
                else
                {
                    result.Append($",");
                }                
            }
            return result.ToString().Remove(result.Length - 1, 1);
        }

        public string GetControlCSV()
        {
            StringBuilder result = new StringBuilder();
            List<PropertyInfo> propertyList = LC96Sample.GetLC96PropertyList();
            foreach (PropertyInfo property in propertyList)
            {
                if (property.GetValue(this.Control) != null)
                {
                    if (property.Name == "ConditionName")
                    {
                        result.Append($"\"{property.GetValue(this.Control).ToString()}\",");
                    }
                    else
                    {
                        result.Append($"{property.GetValue(this.Control).ToString()},");
                    }
                }
                else
                {
                    result.Append($",");
                }

            }
            return result.ToString().Remove(result.Length - 1, 1);
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
