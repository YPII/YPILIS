using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Data;
using MySql.Data.MySqlClient;

namespace YellowstonePathology.UI.MaterialStorage
{
    internal class MaterialStorageScanLog : ObservableCollection<MaterialStorageScanLogItem>
    {
        public MaterialStorageScanLog()
        {

        }

        public static MaterialStorageScanLog GetAll()
        {
            MaterialStorageScanLog result = new MaterialStorageScanLog();
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "Select * from tblMaterialStorageScanLog order by ScanDate desc;";
            cmd.CommandType = CommandType.Text;

            using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        MaterialStorageScanLogItem item = new MaterialStorageScanLogItem();
                        YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Business.Persistence.SqlDataReaderPropertyWriter(item, dr);
                        sqlDataReaderPropertyWriter.WriteProperties();
                        result.Add(item);
                    }
                }
            }

            return result;
        }
    }
}
