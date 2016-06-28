using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace YellowstonePathology.Business.Billing.Model
{
    public class CptCodeCollection : List<CptCode>
    {
        private static volatile CptCodeCollection instance;
        private static object syncRoot = new Object();

        public static CptCodeCollection Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = FromJSON();                            
                    }
                }

                return instance;
            }
        }

        public CptCodeCollection()
        {

        }

        public void WriteToRedis()
        {
            IDatabase db = Business.RedisConnection.Instance.GetDatabase();
            db.KeyDelete("cptcodes");

            foreach (CptCode cptCode in this)
            {
                db.KeyDelete("cptcode:" + cptCode.Code);

                string result = JsonConvert.SerializeObject(cptCode, Newtonsoft.Json.Formatting.Indented, new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.All
                });

                db.ListRightPush("cptcodes", "cptcode:" + cptCode.Code);
                db.StringSet("cptcode:" + cptCode.Code, result);
            }
        }

        public static CptCodeCollection BuildFromRedis()
        {
            CptCodeCollection result = new CptCodeCollection();
            IDatabase db = Business.RedisConnection.Instance.GetDatabase();
            RedisValue[] items = db.ListRange("cptcodes", 0, -1);

            for(int i=0; i<items.Length; i++)
            {
                RedisValue json = db.StringGet(items[i].ToString());
                YellowstonePathology.Business.Billing.Model.CptCode cptCode = JsonConvert.DeserializeObject<Business.Billing.Model.CptCode>(json, new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.All,
                    ObjectCreationHandling = ObjectCreationHandling.Replace
                });

                result.Add(cptCode);
            }

            return result;
        }

        public bool IsMedicareCode(string cptCode)
        {
            bool result = false;

            return result;
        }

        public CptCode GetCptCode(string code)
        {
            CptCode result = null;            
            foreach (CptCode cptCode in this)
            {
                if (cptCode.Code.ToUpper() == code.ToUpper())
                {
                    result = cptCode;
                }
            }
            return result;
        }        

        public CptCodeCollection GetCptCodes(FeeScheduleEnum feeSchedule)
        {
            CptCodeCollection result = new CptCodeCollection();
            foreach (CptCode cptCode in this)
            {
                if (cptCode.FeeSchedule == feeSchedule)
                {
                    result.Add(cptCode);
                }
            }
            return result;
        }        

        public static CptCodeCollection GetCptCodeCollection(FeeScheduleEnum feeSchedule)
        {
            CptCodeCollection result = new CptCodeCollection();
            CptCodeCollection allCodes = GetAll();
            foreach (CptCode cptCode in allCodes)
            {
                if (cptCode.FeeSchedule == feeSchedule)
                {
                    result.Add(cptCode);                    
                }
            }
            return result;
        }

        public CptCode GetNewInstance(string cptCode, string modifier)
        {
            CptCode result = CptCode.Clone(this.GetCptCode(cptCode));
            result.Modifier = modifier;
            return result;         
        }

        public static CptCodeCollection GetAll()
        {
            return Instance;
            /*CptCodeCollection result = new CptCodeCollection();                                    
            result.Add(new CptCodeDefinition.CPT81210());
            result.Add(new CptCodeDefinition.CPT8121026());
            result.Add(new CptCodeDefinition.CPT81220());
            result.Add(new CptCodeDefinition.CPT8122026());
            result.Add(new CptCodeDefinition.CPT81235());
            result.Add(new CptCodeDefinition.CPT81240());
            result.Add(new CptCodeDefinition.CPT8124026());
            result.Add(new CptCodeDefinition.CPT81241());
            result.Add(new CptCodeDefinition.CPT8124126());
            result.Add(new CptCodeDefinition.CPT81261());
            result.Add(new CptCodeDefinition.CPT8126126());
            result.Add(new CptCodeDefinition.CPT81270());
            result.Add(new CptCodeDefinition.CPT8127026());
            result.Add(new CptCodeDefinition.CPT81275());
            result.Add(new CptCodeDefinition.CPT8127526());            
            result.Add(new CptCodeDefinition.CPT8129126());
            result.Add(new CptCodeDefinition.CPT81342());
            result.Add(new CptCodeDefinition.CPT85055());
            result.Add(new CptCodeDefinition.CPT85060());
            result.Add(new CptCodeDefinition.CPT85097());
            result.Add(new CptCodeDefinition.CPT86023());
            result.Add(new CptCodeDefinition.CPT86356());            
            result.Add(new CptCodeDefinition.CPT86367());            
            result.Add(new CptCodeDefinition.CPT87491());
            result.Add(new CptCodeDefinition.CPT87591());
            result.Add(new CptCodeDefinition.CPT87621());
            result.Add(new CptCodeDefinition.CPT87798());
            result.Add(new CptCodeDefinition.CPT88104());
            result.Add(new CptCodeDefinition.CPT88108());
            result.Add(new CptCodeDefinition.CPT88112());
            result.Add(new CptCodeDefinition.CPT88141());
            result.Add(new CptCodeDefinition.CPT88142());
            result.Add(new CptCodeDefinition.CPT88155());
            result.Add(new CptCodeDefinition.CPT88160());
            result.Add(new CptCodeDefinition.CPT88161());            
            result.Add(new CptCodeDefinition.CPT88172());
            result.Add(new CptCodeDefinition.CPT88173());
            result.Add(new CptCodeDefinition.CPT88175());
            result.Add(new CptCodeDefinition.CPT88177());
            result.Add(new CptCodeDefinition.CPT88182());
            result.Add(new CptCodeDefinition.CPT88184());
            result.Add(new CptCodeDefinition.CPT88185());
            result.Add(new CptCodeDefinition.CPT88187());
            result.Add(new CptCodeDefinition.CPT88188());
            result.Add(new CptCodeDefinition.CPT88189());
			result.Add(new CptCodeDefinition.CPT88237());
			result.Add(new CptCodeDefinition.CPT88262());
            result.Add(new CptCodeDefinition.CPT88264());
			result.Add(new CptCodeDefinition.CPT88280());
            result.Add(new CptCodeDefinition.CPT81287());
            result.Add(new CptCodeDefinition.CPT88291());
			result.Add(new CptCodeDefinition.CPT88300());            
            result.Add(new CptCodeDefinition.CPT88302());
            result.Add(new CptCodeDefinition.CPT88304());
            result.Add(new CptCodeDefinition.CPT88305());
            result.Add(new CptCodeDefinition.CPT88307());
            result.Add(new CptCodeDefinition.CPT88309());
            result.Add(new CptCodeDefinition.CPT88311());
            result.Add(new CptCodeDefinition.CPT88312());
            result.Add(new CptCodeDefinition.CPT88312TC());
            result.Add(new CptCodeDefinition.CPT88313());
            result.Add(new CptCodeDefinition.CPT88313TC());
            result.Add(new CptCodeDefinition.CPT88314());
            result.Add(new CptCodeDefinition.CPT88319());
            result.Add(new CptCodeDefinition.CPT88321());
            result.Add(new CptCodeDefinition.CPT88323());
            result.Add(new CptCodeDefinition.CPT88325());
            result.Add(new CptCodeDefinition.CPT88329());
            result.Add(new CptCodeDefinition.CPT88331());
            result.Add(new CptCodeDefinition.CPT88332());
            result.Add(new CptCodeDefinition.CPT88333());
            result.Add(new CptCodeDefinition.CPT88334());
            result.Add(new CptCodeDefinition.CPT88342());
            result.Add(new CptCodeDefinition.CPT88342TC());
			result.Add(new CptCodeDefinition.CPT88343());
			result.Add(new CptCodeDefinition.CPT88343TC());
			result.Add(new CptCodeDefinition.CPT88360());
            result.Add(new CptCodeDefinition.CPT88360TC());
            result.Add(new CptCodeDefinition.CPT88363());
            result.Add(new CptCodeDefinition.CPT88365());
            result.Add(new CptCodeDefinition.CPT88367());            
            result.Add(new CptCodeDefinition.CPT88368());
            result.Add(new CptCodeDefinition.CPT89060());            
            result.Add(new CptCodeDefinition.CPT99000());
            result.Add(new CptCodeDefinition.CPT81406());
            result.Add(new CptCodeDefinition.CPT81403());
            result.Add(new CptCodeDefinition.CPT81402());
            result.Add(new CptCodeDefinition.CPT81401());
            result.Add(new CptCodeDefinition.CPT81479());
            result.Add(new CptCodeDefinition.CPT81245());
            result.Add(new CptCodeDefinition.CPT81310());
            result.Add(new CptCodeDefinition.CPT81301());
            result.Add(new CptCodeDefinition.CPT81404());
            result.Add(new CptCodeDefinition.CPT81206());
            result.Add(new CptCodeDefinition.CPT81207());
            result.Add(new CptCodeDefinition.CPT88261());
            result.Add(new CptCodeDefinition.CPT81315());
            result.Add(new CptCodeDefinition.CPT88285());
            result.Add(new CptCodeDefinition.CPT81321());
            result.Add(new CptCodeDefinition.CPT84179());
            result.Add(new CptCodeDefinition.CPT88327());
            result.Add(new CptCodeDefinition.CPT81407());
            result.Add(new CptCodeDefinition.CPT88361());
            result.Add(new CptCodeDefinition.CPT87624());
            result.Add(new CptCodeDefinition.CPT87625());
            result.Add(new CptCodeDefinition.CPT88341());
            result.Add(new CptCodeDefinition.CPT88346());
            result.Add(new CptCodeDefinition.CPT88347());
            result.Add(new CptCodeDefinition.CPT88348());
            result.Add(new CptCodeDefinition.CPT88377());
            result.Add(new CptCodeDefinition.CPT88374());
            result.Add(new CptCodeDefinition.CPT88373());
            result.Add(new CptCodeDefinition.CPT88369());
            result.Add(new CptCodeDefinition.CPT88120());
            result.Add(new CptCodeDefinition.CPT81288());
            result.Add(new CptCodeDefinition.CPT81263());
            result.Add(new CptCodeDefinition.CPT81445());
            result.Add(new CptCodeDefinition.CPT88233());
            result.Add(new CptCodeDefinition.CPT81450());
            result.Add(new CptCodeDefinition.CPT88239());
            result.Add(new CptCodeDefinition.CPT88230());
            result.Add(new CptCodeDefinition.CPT87661());
            result.Add(new CptCodeDefinition.CPT81170());
            result.Add(new CptCodeDefinition.CPT81219());
            result.Add(new CptCodeDefinition.CPT81218());
            result.Add(new CptCodeDefinition.CPT81276());
            result.Add(new CptCodeDefinition.CPT81311());
            result.Add(new CptCodeDefinition.CPT81314());
            result.Add(new CptCodeDefinition.CPT81272());
            result.Add(new CptCodeDefinition.CPT81264());
            result.Add(new CptCodeDefinition.CPT83540());
            result.Add(new CptCodeDefinition.CPT81246());
            result.Add(new CptCodeDefinition.CPT88364());

            result.Add(new GCodeDefinitions.CPTG0123());
            result.Add(new GCodeDefinitions.CPTG0124());
            result.Add(new GCodeDefinitions.CPTG0145());
            result.Add(new GCodeDefinitions.CPTG0461());
            result.Add(new GCodeDefinitions.CPTG0462());
            result.Add(new GCodeDefinitions.CPTG0416());            

            result.Add(new PQRSCodeDefinitions.PQRS3125F());
            result.Add(new PQRSCodeDefinitions.PQRS3126F());
            result.Add(new PQRSCodeDefinitions.PQRS3126F1P());
            result.Add(new PQRSCodeDefinitions.PQRS3126F8P());
            result.Add(new PQRSCodeDefinitions.PQRS3250F());
            result.Add(new PQRSCodeDefinitions.PQRS3260());
            result.Add(new PQRSCodeDefinitions.PQRS3260F());
            result.Add(new PQRSCodeDefinitions.PQRS3267F());
            result.Add(new PQRSCodeDefinitions.PQRSG8721());
            result.Add(new PQRSCodeDefinitions.PQRSG8722());
            result.Add(new PQRSCodeDefinitions.PQRSG8723());
            result.Add(new PQRSCodeDefinitions.PQRSG8797());            
            result.Add(new PQRSCodeDefinitions.PQRSG8798());
            result.Add(new PQRSCodeDefinitions.PQRS3394F());
            result.Add(new PQRSCodeDefinitions.PQRS3394F8P());
            result.Add(new PQRSCodeDefinitions.PQRS3395F());

            result.Add(new PQRSCodeDefinitions.PQRSG9418());
            result.Add(new PQRSCodeDefinitions.PQRSG9419());
            result.Add(new PQRSCodeDefinitions.PQRSG9420());
            result.Add(new PQRSCodeDefinitions.PQRSG9421());
            result.Add(new PQRSCodeDefinitions.PQRSG9422());
            result.Add(new PQRSCodeDefinitions.PQRSG9423());
            result.Add(new PQRSCodeDefinitions.PQRSG9424());
            result.Add(new PQRSCodeDefinitions.PQRSG9425());
            result.Add(new PQRSCodeDefinitions.PQRSG9428());
            result.Add(new PQRSCodeDefinitions.PQRSG9429());

            result.Add(new CptCodeDefinition.AutopsyBlock());

            return GetSorted(result);*/
        }

        public static CptCodeCollection GetSorted(CptCodeCollection cptCodeCollection)
        {
            CptCodeCollection result = new CptCodeCollection();
            IOrderedEnumerable<CptCode> orderedResult = cptCodeCollection.OrderBy(i => i.Code);
            foreach (CptCode cptCode in orderedResult)
            {
                result.Add(cptCode);
            }
            return result;
        }

        public string ToJSON()
        {
            string result = JsonConvert.SerializeObject(this, Newtonsoft.Json.Formatting.Indented, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All
            });

            return result;
        }

        public static CptCodeCollection FromJSON()
        {
            string jsonString = string.Empty;
            Assembly assembly = Assembly.GetExecutingAssembly();
            using (StreamReader sr = new StreamReader(assembly.GetManifestResourceStream("YellowstonePathology.Business.Billing.Model.JSONCPTCodes.txt")))
            {
                jsonString = sr.ReadToEnd();
            }

            YellowstonePathology.Business.Billing.Model.CptCodeCollection result = JsonConvert.DeserializeObject<Business.Billing.Model.CptCodeCollection>(jsonString, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All,
                ObjectCreationHandling = ObjectCreationHandling.Replace
            });

            return result;
        }
    }
}
