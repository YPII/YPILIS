using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace YellowstonePathology.Business.Billing.Model
{
    public class SimulationSerializer
    {        
        public static void Serialize(Business.Test.AccessionOrder ao, string reportNo, string fileName)
        {
            JsonSerializer serializer = new JsonSerializer();
            serializer.Converters.Add(new Newtonsoft.Json.Converters.JavaScriptDateTimeConverter());
            serializer.NullValueHandling = NullValueHandling.Ignore;

            using (System.IO.StreamWriter sw = new System.IO.StreamWriter(fileName))
            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                Business.Test.PanelSetOrder pso = ao.PanelSetOrderCollection.GetPanelSetOrder(reportNo);
                serializer.Serialize(writer, pso.PanelSetOrderCPTCodeBillCollection);                
            }
        }
    }
}
