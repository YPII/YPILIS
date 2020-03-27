using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Data;
using MySql.Data.MySqlClient;


namespace YellowstonePathology.Business.Billing.Model
{
    public class CDMCollection : ObservableCollection<CDM>
    {
        private static CDMCollection instance;

        public CDMCollection()
        { }

        public static CDMCollection Instance
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

        public void UpdateCDMId()
        {
            foreach(CDM cdm in this)
            {
                string bsonId = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
                cdm.CDMId = bsonId;
                cdm.Save();
            }
        }

        private static CDMCollection Load()
        {
            CDMCollection result = new CDMCollection();
            MySqlCommand cmd = new MySqlCommand("Select CDMCode, CPTCode, ProcedureName, CDMClient from tblCDM order by CDMCode;");
            cmd.CommandType = CommandType.Text;

            using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        CDM cdms = new Model.CDM();
                        YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(cdms, dr);
                        sqlDataReaderPropertyWriter.WriteProperties();
                        result.Add(cdms);
                    }
                }
            }
            return result;
        }

        public static void Refresh()
        {
            instance = null;
            CDMCollection tmp = CDMCollection.Instance;
        }

        public bool Exists(string cdmCode)
        {
            CDM result = this.FirstOrDefault(c => c.CDMCode == cdmCode);
            return result == null ? false : true;
        }

        public bool Exists(string cptCode, string cdmClient)
        {
            CDM result = this.FirstOrDefault(c => c.CPTCode == cptCode && c.CDMClient == cdmClient);
            return result == null ? false : true;
        }

        public CDM GetCDMS(string cdmCode)
        {
            CDM result = this.FirstOrDefault(c => c.CDMCode == cdmCode);
            return result;
        }

        public CDM GetCDMS(string cptCode, string cdmClient)
        {
            CDM result = this.FirstOrDefault(c => c.CPTCode == cptCode && c.CDMClient == cdmClient);
            return result;
        }
    }
}
