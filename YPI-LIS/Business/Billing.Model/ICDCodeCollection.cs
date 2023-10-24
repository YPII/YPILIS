using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using StackExchange.Redis;
using System.Data;
using MySql.Data.MySqlClient;

namespace YellowstonePathology.Business.Billing.Model
{
    public class  ICDCodeCollection : ObservableCollection<ICDCode>
    {
        private static volatile ICDCodeCollection instance;
        private static object syncRoot = new Object();

        public static ICDCodeCollection Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = Load();
                    }
                }

                return instance;
            }
        }

        public ICDCodeCollection()
        {

        }

        public ICDCode GetICDCode(string code)
        {
            ICDCode result = null;
            foreach (ICDCode icdCode in ICDCodeCollection.Instance)
            {
                if (icdCode.Code.ToUpper() == code.ToUpper())
                {
                    result = icdCode;
                    break;
                }
            }
            return result;
        }

        public ICDCode GetICDCodeById(string icdCodeId)
        {
            ICDCode result = null;
            foreach (ICDCode icdCode in ICDCodeCollection.Instance)
            {
                if (icdCode.ICDCodeId.ToUpper() == icdCodeId.ToUpper())
                {
                    result = icdCode;
                    break;
                }
            }
            return result;
        }

        public ICDCode GetClone(string icdCodeId)
        {
            ICDCode result = ICDCode.Clone(this.GetICDCodeById(icdCodeId));
            return result;
        }

        public static ICDCodeCollection GetSortedByCode(ICDCodeCollection icdCodeList)
        {
            ICDCodeCollection result = new ICDCodeCollection();
            IOrderedEnumerable<ICDCode> orderedResult = icdCodeList.OrderBy(i => i.Code);
            foreach (ICDCode icdCode in orderedResult)
            {
                result.Add(icdCode);
            }
            return result;
        }

        public static ICDCodeCollection GetSortedByDescription(ICDCodeCollection icdCodeList)
        {
            ICDCodeCollection result = new ICDCodeCollection();
            IOrderedEnumerable<ICDCode> orderedResult = icdCodeList.OrderBy(i => i.Description);
            foreach (ICDCode icdCode in orderedResult)
            {
                result.Add(icdCode);
            }
            return result;
        }

        public static ICDCodeCollection GetByCategory(string category)
        {
            ICDCodeCollection result = new ICDCodeCollection();
            foreach (ICDCode icdCode in ICDCodeCollection.Instance)
            {
                if (icdCode.Category == category)
                {
                    result.Add(icdCode);
                }
            }
            return result;
        }

        public static ICDCodeCollection GetBillingCodeList()
        {
            ICDCodeCollection collection = ICDCodeCollection.GetByCategory("Cytology");
            List<ICDCode> list = collection.ToList<ICDCode>();
            collection = ICDCodeCollection.GetByCategory("NGCT");
            list.AddRange(collection);
            collection = ICDCodeCollection.GetByCategory("Routine HPV");
            list.AddRange(collection);
            collection = ICDCodeCollection.GetByCategory("Trichomonas/Cervx");
            list.AddRange(collection);
            collection = ICDCodeCollection.GetByCategory("Trichomonas");
            list.AddRange(collection);
            collection = ICDCodeCollection.GetByCategory(string.Empty);
            list.AddRange(collection);

            ICDCodeCollection result = new Model.ICDCodeCollection();
            foreach(ICDCode code in list)
            {
                result.Add(code);
            }

            result = ICDCodeCollection.GetSortedByCode(result);

            return result;
        }

        public static ICDCodeCollection GetFlowCodeList()
        {
            ICDCodeCollection result = ICDCodeCollection.GetSortedByDescription(ICDCodeCollection.GetByCategory("Flow"));
            return result;
        }

        private static ICDCodeCollection Load()
        {
            ICDCodeCollection result = new Model.ICDCodeCollection();
            MySqlCommand cmd = new MySqlCommand("Select JSONValue from tblICDCode;");
            cmd.CommandType = CommandType.Text;

            using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        YellowstonePathology.Business.Billing.Model.ICDCode icdCode = Business.Billing.Model.ICDCodeFactory.FromJson(dr[0].ToString());
                        result.Add(icdCode);
                    }
                }
            }
            return result;
        }

        public static void Refresh()
        {
            instance = null;
            ICDCodeCollection tmp = ICDCodeCollection.Instance;
        }
    }
}
