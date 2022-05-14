using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Linq;
using System.Data;
using MySql.Data.MySqlClient;

namespace YellowstonePathology.UI.Gross
{
    public class DictationTemplateCollection : ObservableCollection<DictationTemplate>
    {
        private static DictationTemplateCollection instance;

        public DictationTemplateCollection() { }

        public static DictationTemplateCollection Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = Load();
                }
                return instance;
            }
        }

        public bool Exists(string dictationTemplateId)
        {
            DictationTemplate template = this.FirstOrDefault(dt => dt.TemplateId == dictationTemplateId);
            return template == null ? false : true;
        }

        public DictationTemplate GetCloneByTemplateId(string dictationTemplateId)
        {
            DictationTemplate foundTemplate = this.FirstOrDefault(dt => dt.TemplateId == dictationTemplateId);
            DictationTemplate result = null;

            if(foundTemplate != null)
            {
                YellowstonePathology.Business.Persistence.ObjectCloner objectCloner = new YellowstonePathology.Business.Persistence.ObjectCloner();
                result = (DictationTemplate)objectCloner.Clone(foundTemplate);
            }

            return result;
        }

        public DictationTemplate GetClone(Business.Specimen.Model.SpecimenOrder specimenOrder)
        {
            DictationTemplate notFound = DictationTemplateCollection.Instance.FirstOrDefault(t => t.TemplateName == "Template Not Found.");
            YellowstonePathology.Business.Persistence.ObjectCloner objectCloner = new YellowstonePathology.Business.Persistence.ObjectCloner();
            DictationTemplate result = (DictationTemplate)objectCloner.Clone(notFound);
            
            if (specimenOrder.HasCytospin() == true)
            {
                result = this.GetCloneByTemplateId("fldcytspn");
            }
            else
            {
                foreach (DictationTemplate dictationTemplate in this)
                {
                    if (dictationTemplate.SpecimenCollection.Exists(specimenOrder.SpecimenId) == true)
                    {
                        result = (DictationTemplate)objectCloner.Clone(dictationTemplate);
                        break;
                    }
                }
            }            

            return result;
        }

        public string ToJSON()
        {
            var camelCaseFormatter = new JsonSerializerSettings();
            camelCaseFormatter.ContractResolver = new CamelCasePropertyNamesContractResolver();
            string result = JsonConvert.SerializeObject(this, Formatting.Indented, camelCaseFormatter);
            return result;
        }

        private static DictationTemplateCollection Load()
        {
            DictationTemplateCollection result = new DictationTemplateCollection();
            MySqlCommand cmd = new MySqlCommand("Select JSONValue from tblDictationTemplate;");
            cmd.CommandType = CommandType.Text;

            using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        string jString = dr[0].ToString();
                        DictationTemplate dictationTemplate = JsonConvert.DeserializeObject<DictationTemplate>(jString, new JsonSerializerSettings
                        {
                            TypeNameHandling = TypeNameHandling.All,
                            ObjectCreationHandling = ObjectCreationHandling.Replace,
                        });

                        JObject jObject = JsonConvert.DeserializeObject<JObject>(jString);
                        foreach (string id in jObject["specimenIds"])
                        {
                            Business.Specimen.Model.Specimen specimen = Business.Specimen.Model.SpecimenCollection.Instance.GetSpecimen(id);
                            dictationTemplate.SpecimenCollection.Add(specimen);
                        }

                        result.Add(dictationTemplate);
                    }
                }
            }

            return result;
        }

        public static DictationTemplateCollection Refresh()
        {
            instance = null;
            return Instance;
        }
    }
}
