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
        public static void SerializePanelSetOrderCPTCodeBill(Business.Test.AccessionOrder ao, string reportNo, string fileName)
        {
            JsonSerializer serializer = new JsonSerializer();            
            
            serializer.Converters.Add(new Newtonsoft.Json.Converters.JavaScriptDateTimeConverter());
            serializer.NullValueHandling = NullValueHandling.Include;
            serializer.DateFormatHandling = DateFormatHandling.MicrosoftDateFormat;

            using (System.IO.StreamWriter sw = new System.IO.StreamWriter(fileName))
            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                Business.Test.PanelSetOrder pso = ao.PanelSetOrderCollection.GetPanelSetOrder(reportNo);
                serializer.Serialize(writer, pso.PanelSetOrderCPTCodeBillCollection);                
            }            
        }

        public static void SerializePanelSetOrderCPTCode(Business.Test.AccessionOrder ao, string reportNo, string fileName)
        {
            JsonSerializer serializer = new JsonSerializer();

            serializer.Converters.Add(new Newtonsoft.Json.Converters.JavaScriptDateTimeConverter());
            serializer.NullValueHandling = NullValueHandling.Include;
            serializer.DateFormatHandling = DateFormatHandling.MicrosoftDateFormat;

            using (System.IO.StreamWriter sw = new System.IO.StreamWriter(fileName))
            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                Business.Test.PanelSetOrder pso = ao.PanelSetOrderCollection.GetPanelSetOrder(reportNo);
                serializer.Serialize(writer, pso.PanelSetOrderCPTCodeCollection);
            }
        }

        public static Business.Test.PanelSetOrderCPTCodeBillCollection Deserialize()
        {
            string json = System.IO.File.ReadAllText(@"D:\BillingSimulation\02182019\19-4342.S.json");
            JArray jArray = JsonConvert.DeserializeObject<JArray>(json);
            Business.Test.PanelSetOrderCPTCodeBillCollection result = new Test.PanelSetOrderCPTCodeBillCollection();
            foreach(JObject jObject in jArray)
            {
                Business.Test.PanelSetOrderCPTCodeBill item = jObject.ToObject<Business.Test.PanelSetOrderCPTCodeBill>();
                result.Add(item);
            }
            return result;
        }
    }
}
