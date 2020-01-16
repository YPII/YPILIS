using System;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace YellowstonePathology.Business.Billing.Model
{
    public class CptCodeFactory
    {
        public CptCodeFactory() { }

        public static CptCode FromJson(string jString)
        {
            CptCode result = null;
            JObject jObject = JsonConvert.DeserializeObject<JObject>(jString);
            if (jObject["codeType"].ToString() == "PQRS")
            {
                PQRSCode pqrsCode = JsonConvert.DeserializeObject<Business.Billing.Model.PQRSCode>(jString, new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.All,
                    ObjectCreationHandling = ObjectCreationHandling.Replace
                });

                pqrsCode.ReportingDefinition = pqrsCode.Description;                
                result = pqrsCode;
            }
            else
            {
                result = JsonConvert.DeserializeObject<Business.Billing.Model.CptCode>(jString, new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.All,
                    ObjectCreationHandling = ObjectCreationHandling.Replace,
                });
            }

            return result;
        }
    }
}
