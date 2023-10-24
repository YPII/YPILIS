using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace YellowstonePathology.Business.Billing.Model
{
    public class ICDCodeFactory
    {
        public ICDCodeFactory() { }

        public static ICDCode FromJson(string jString)
        {
            ICDCode result = JsonConvert.DeserializeObject<ICDCode>(jString, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All,
                ObjectCreationHandling = ObjectCreationHandling.Replace,
            });

            return result;
        }
    }
}
