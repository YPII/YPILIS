using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Newtonsoft.Json;

namespace YellowstonePathology.Business.Billing.Model
{
    public class MonitoredObject
    {
        private string m_ReportNo;
        private Object m_Object;
        private object m_Clone;
                
        public MonitoredObject(object o, string reportNo)
        {
            this.m_Object = o;
            this.m_ReportNo = reportNo;
            Business.Persistence.ObjectCloner objectCloner = new Persistence.ObjectCloner();
            this.m_Clone = objectCloner.Clone(o);                        
        } 
        
        public void WriteChanges()
        {
            List<PropertyChanges> propertyChangeList = new List<PropertyChanges>();
            PropertyInfo[] properties = this.m_Object.GetType().GetProperties().
                Where(prop => Attribute.IsDefined(prop, typeof(Business.Persistence.MonitorProperty))).ToArray();            
            foreach (PropertyInfo property in properties)
            {
                if (Business.Persistence.PersistenceHelper.ArePropertiesEqual(property, this.m_Object, this.m_Clone) == false)
                {
                    
                    PropertyChanges propertyChanges = new PropertyChanges(this.m_ReportNo, property.Name, property.GetValue(this.m_Object).ToString(), property.GetValue(this.m_Clone).ToString());
                    propertyChangeList.Add(propertyChanges);
                }
            }

            if(propertyChangeList.Count > 0)
            {
                string json = JsonConvert.SerializeObject(propertyChangeList, Newtonsoft.Json.Formatting.Indented);

                string fileName = Environment.ExpandEnvironmentVariables(YellowstonePathology.Properties.Settings.Default.MonitoredPropertyFolder) + "\\" + this.m_ReportNo + "-" + DateTime.Now.ToString("hhMMss") + ".json";
                using (System.IO.StreamWriter sw = new System.IO.StreamWriter(fileName, false))
                {
                    sw.Write(json);
                }
            }
        }             
    }
}
