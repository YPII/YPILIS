using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using MySql.Data.MySqlClient;

namespace YellowstonePathology.Business.Billing.Model
{
    public class InsuranceMapCollection : ObservableCollection<InsuranceMap>
    {
        public InsuranceMapCollection()
        {

        }

        public void UnMap(InsuranceMap insuranceMap)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "delete from tblInsuranceMap where name = @name";
            cmd.Parameters.AddWithValue("@name", insuranceMap.Name.ToUpper());            

            using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                cmd.ExecuteNonQuery();
            }
            this.Remove(insuranceMap);
        }

        public void TryMap(string name, string mapsTo)
        {
            if (Exists(name) == false)
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = "Insert tblInsuranceMap (name, mapsto) values (@name, @mapsto)";
                cmd.Parameters.AddWithValue("@name", name.ToUpper());
                cmd.Parameters.AddWithValue("@mapsto", mapsTo);

                using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
                {
                    cn.Open();
                    cmd.Connection = cn;
                    cmd.ExecuteNonQuery();
                }

                InsuranceMap item = new InsuranceMap();
                item.Name = name;
                item.MapsTo = mapsTo;
                this.Add(item);
            }
        }

        public bool Exists(string name)
        {
            bool result = false;
            if(string.IsNullOrEmpty(name) == false)
            {
                foreach (InsuranceMap item in this)
                {
                    if (item.Name.ToUpper() == name.ToUpper())
                    {
                        result = true;
                        break;
                    }
                }
            }            
            return result;
        }

        public InsuranceMap GetMap(string name)
        {
            InsuranceMap result = null;
            foreach (InsuranceMap item in this)
            {
                if (item.Name.ToUpper() == name.ToUpper())
                {
                    result = item;
                    break;
                }
            }
            return result;
        }

        public static InsuranceMapCollection GetCollection()
        {
            InsuranceMapCollection result = new InsuranceMapCollection();

            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "Select * from tblInsuranceMap";

            using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        InsuranceMap item = new InsuranceMap();
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
