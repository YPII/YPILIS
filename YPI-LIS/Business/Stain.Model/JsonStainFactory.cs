using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace YellowstonePathology.Business.Stain.Model
{
    public class JsonStainFactory
    {
        public JsonStainFactory() { }

        public static Stain FromJson(string jsonString)
        {
            Stain result = JsonConvert.DeserializeObject<Stain>(jsonString, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All,
                ObjectCreationHandling = ObjectCreationHandling.Replace,
            });
            return result;
        }
    }
}
