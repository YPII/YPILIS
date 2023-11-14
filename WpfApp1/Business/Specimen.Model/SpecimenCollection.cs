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

namespace YellowstonePathology.Business.Specimen.Model
{
    public class SpecimenCollection : ObservableCollection<Specimen>
    {
        private static SpecimenCollection instance;

        public SpecimenCollection() { }

        public static SpecimenCollection Instance
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

        public bool Exists(string specimenId)
        {
            bool result = false;
            foreach (Specimen specimen in this)
            {
                if (specimen.SpecimenId == specimenId)
                {
                    result = true;
                    break;
                }
            }
            return result;
        }

        public Specimen GetSpecimen(string specimenId)
        {
            Specimen result = null;
            foreach (Specimen specimen in this)
            {
                if (specimen.SpecimenId == specimenId)
                {
                    result = specimen;
                    break;
                }
            }
            return result;
        }

        private static SpecimenCollection Sort(SpecimenCollection specimenCollection)
        {
            SpecimenCollection result = new SpecimenCollection();
            IOrderedEnumerable<Specimen> orderedResult = specimenCollection.OrderBy(i => i.SpecimenName);
            foreach (Specimen specimen in orderedResult)
            {
                result.Add(specimen);
            }
            return result;
        }

        public SpecimenCollection GetActive()
        {
            SpecimenCollection result = new SpecimenCollection();
            foreach(Specimen specimen in this)
            {
                if (specimen.Active == true) result.Add(specimen);
            }
            return Sort(result);
        }

        private static SpecimenCollection Load()
        {
            SpecimenCollection result = new SpecimenCollection();
            MySqlCommand cmd = new MySqlCommand("Select JSONValue from tblSpecimen;");
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
                        Specimen specimen = FromJSON(jString);
                        result.Add(specimen);
                    }
                }
            }
            result.Add(new Specimen());
            return Sort(result);
        }

        public static Specimen FromJSON(string jString)
        {
            Specimen specimen = JsonConvert.DeserializeObject<Specimen>(jString, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All,
                ObjectCreationHandling = ObjectCreationHandling.Replace,
            });

            JObject jObject = JsonConvert.DeserializeObject<JObject>(jString);
            string code = jObject["cptCodeId"].ToString();
            if (string.IsNullOrEmpty(code) == false)
            {
                specimen.CPTCode = Store.AppDataStore.Instance.CPTCodeCollection.GetClone(code, null);
            }

            return specimen;
        }

        public static void Refresh()
        {
            instance = null;
            SpecimenCollection tmp = SpecimenCollection.Instance;
        }
    }
}
