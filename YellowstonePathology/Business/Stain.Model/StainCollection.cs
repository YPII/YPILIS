using System;
using System.Text;
using System.Collections.ObjectModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Linq;
using System.Data;
using MySql.Data.MySqlClient;

namespace YellowstonePathology.Business.Stain.Model
{
    public class StainCollection : ObservableCollection<Stain>
    {
        private static StainCollection instance;

        public StainCollection() { }

        public static StainCollection Instance
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

        public bool Exists(string stainId)
        {
            Stain stain = this.FirstOrDefault(s => s.StainId == stainId);
            return stain != null ? true : false;
        }

        public Stain GetStain(string stainId)
        {
            Stain result = this.FirstOrDefault(s => s.StainId == stainId);
            return result;
        }

        public bool ExistsByTestid(string testId)
        {
            Stain stain = this.FirstOrDefault(s => s.TestId == testId);
            return stain != null ? true : false;
        }

        public Stain GetStainByTestId(string testId)
        {
            Stain result = this.FirstOrDefault(s => s.TestId == testId);
            return result;
        }

        public string ToJSON()
        {
            var camelCaseFormatter = new JsonSerializerSettings();
            camelCaseFormatter.ContractResolver = new CamelCasePropertyNamesContractResolver();
            string result = JsonConvert.SerializeObject(this, Formatting.Indented, camelCaseFormatter);
            return result;
        }

        private static StainCollection Load()
        {
            StainCollection result = new StainCollection();
            MySqlCommand cmd = new MySqlCommand("Select JSONValue from tblStain;");
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
                        Stain stain = JsonStainFactory.FromJson(jString);
                        result.Add(stain);
                    }
                }
            }
            return (result);
        }

        public static string GetQuotedTestIds()
        {
            StringBuilder result = new StringBuilder();
            foreach(Stain stain in Instance)
            {
                result.Append("'" + stain.TestId + "',");
            }
            result.Remove(result.Length - 1, 1);
            return result.ToString();
        }

        public static void DeleteStain(Stain stain)
        {
            StainCollection.Instance.Remove(stain);

            MySqlCommand cmd = new MySqlCommand("Delete from tblStain where StainId = @StainId;");
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@StainId", stain.StainId);
            using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                cmd.ExecuteNonQuery();
            }

            Test.Model.TestCollectionInstance.Reload();
        }

        public Business.Test.Model.TestCollection GetTestCollection()
        {
            Business.Test.Model.TestCollection result = new Test.Model.TestCollection();
            foreach(Stain stain in this)
            {
                Business.Test.Model.Test test = Business.Test.Model.TestCollectionInstance.GetClone(stain.TestId);
                result.Add(test);
            }
            return result;
        }
        
        public static void Refresh()
        {
            instance = Load();
            YellowstonePathology.Business.Test.Model.TestCollectionInstance.Reload();
        }
    }
}

