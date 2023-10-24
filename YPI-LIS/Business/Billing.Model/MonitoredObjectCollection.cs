using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Reflection;
using Newtonsoft.Json;

namespace YellowstonePathology.Business.Billing.Model
{
    public class MonitoredObjectCollection : ObservableCollection<MonitoredObject>
    {        
        public void Load(Type type, object o, string reportNo)
        {                        
            MonitoredObject monitoredObject = new MonitoredObject(o, reportNo);
            this.Add(monitoredObject);            
        }

        public void WriteChanges()
        {
            StringBuilder result = new StringBuilder();
            foreach (MonitoredObject monitoredObject in this)
            {
                try
                {
                    monitoredObject.WriteChanges();
                }
                catch(Exception e)
                {
                    Business.Logging.EmailExceptionHandler.HandleException("Monitor Object Collection:WriteChanges(): " + e.Message);
                }                
            }            
        }
    }
}
