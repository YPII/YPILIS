using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.ComponentModel;
using System.Data;
using MySql.Data.MySqlClient;

namespace YellowstonePathology.Business.Slide.Model
{
    public class VantageSlide : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        string m_VantageSlideId;
        string m_MasterAccessionNo;
        string m_CurrentLocation;
        VantageSlideScanCollection m_SlideScans;

        public VantageSlide()
        {
            this.m_SlideScans = new VantageSlideScanCollection();
        }

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        public VantageSlideScanCollection SlideScans
        {
            get { return this.m_SlideScans; }
        }

        public string VantageSlideId
        {
            get { return this.m_VantageSlideId; }
            set { this.m_VantageSlideId = value; }
        }

        public string MasterAccessionNo
        {
            get { return this.m_MasterAccessionNo; }
            set { this.m_MasterAccessionNo = value; }
        }

        public string CurrentLocation
        {
            get { return this.m_CurrentLocation; }
            set
            {
                this.m_CurrentLocation = value;
                this.NotifyPropertyChanged("CurrentLocation");
            }
        }

        public string GetKey()
        {
            return this.m_MasterAccessionNo + ":" + this.m_VantageSlideId;
        }

        public string ToJSON()
        {            
            return JsonConvert.SerializeObject(this);            
        }

        public static VantageSlide FromJson(string json)
        {
            return JsonConvert.DeserializeObject<VantageSlide>(json);
        }

        public void Save()
        {
            string key = this.GetKey();
            string jString = this.ToJSON();
            MySqlCommand cmd = new MySqlCommand("Insert tblVantageSlide (VantageSlideId, JSONValue) values (@VantageSlideId, @JSONValue) ON DUPLICATE KEY UPDATE VantageSlideId = @VantageSlideId, JSONValue = @JSONValue;");
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@JSONValue", jString);
            cmd.Parameters.AddWithValue("@VantageSlideId", key);

            using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                cmd.ExecuteNonQuery();
            }
        }

        public static List<string> GetBySlideId(string slideId)
        {
            return GetByKey("'%:" + slideId + "'");
        }

        public static List<string> GetByMasterAccessionNo(string masterAccessionNo)
        {
            return GetByKey("'" + masterAccessionNo + "%'");
        }

        private static List<string> GetByKey(string idString)
        {
            List<string> result = new List<string>();
            MySqlCommand cmd = new MySqlCommand("Select JSONValue from tblVantageSlide where VantageSlideId Like " + idString + ";");
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
                        result.Add(jString);
                    }
                }
            }
            return result;
        }

    }
}
