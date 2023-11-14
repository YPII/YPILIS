using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using StackExchange.Redis;
using System.Data;
using MySql.Data.MySqlClient;

namespace YellowstonePathology.Business.Billing.Model
{
    public class CptCodeCollection : ObservableCollection<CptCode>
    {        

        public CptCodeCollection()
        {

        }        

        public bool Exists(string code)
        {
            bool result = false;
            foreach(CptCode cptCode in this)
            {
                if(cptCode.Code == code)
                {
                    result = true;
                    break;
                }
            }
            return result;
        }

        public bool IsMedicareCode(string cptCode)
        {
            bool result = false;
            return result;
        }

        public void AddCloneWithModifier(string code, string modifier)
        {
            foreach (CptCode cptCode in this)
            {
                if (cptCode.Code == code)
                {
                    CptCode result = cptCode.Clone(cptCode);
                    result.Modifier = modifier;
                    this.Add(result);
                    break;
                }
            }
        }

        public CptCode GetClone(string code, string modifier)
        {
            CptCode result = null;
            foreach(CptCode cptCode in this)
            {
                if(cptCode.Code == code)
                {
                    result = cptCode.Clone(cptCode);
                    result.Modifier = modifier;
                    break;
                }
            }
            return result;
        }

        public CptCodeCollection Clone()
        {
            CptCodeCollection result = new CptCodeCollection();
            foreach(CptCode cptCode in this)
            {
                result.Add(cptCode.Clone(cptCode));
            }
            return result;
        }

        public CptCodeCollection GetCptCodeCollection(FeeScheduleEnum feeSchedule)
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

        public CptCode GetCPTCode(string cptCode)
        {
            CptCode result = null;
            foreach(CptCode cpt in this)
            {
                if(cpt.Code == cptCode)
                {
                    result = cpt;
                    break;
                }
            }
            return result;
        }

        public CptCodeCollection GetFISHCPTCodeCollection()
        {
            CptCodeCollection result = new CptCodeCollection();
            result.Add(this.GetCPTCode("88374"));
            result.Add(this.GetCPTCode("88377"));
            result.Add(this.GetCPTCode("88368"));
            result.Add(this.GetCPTCode("88369"));
            result.Add(this.GetCPTCode("88367"));
            result.Add(this.GetCPTCode("88373"));            
            return result;
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

        public void Load()
        {
            this.ClearItems();
            MySqlCommand cmd = new MySqlCommand("Select JSONValue from tblCPTCode;");
            cmd.CommandType = CommandType.Text;

            using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        YellowstonePathology.Business.Billing.Model.CptCode cptCode = Business.Billing.Model.CptCodeFactory.FromJson(dr[0].ToString());
                        this.Add(cptCode);
                    }
                }
            }
        }
    }
}
