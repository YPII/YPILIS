using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Linq;
using YellowstonePathology.Business.Persistence;
using MySql.Data.MySqlClient;

namespace YellowstonePathology.UI.Monitor
{
    [PersistentClass("tblHuddleBoard", "YPIDATA")]
    public class HuddleDashboard : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string m_HuddleDashboardId;
        private string m_Staffing;
        private string m_Quality;
        private string m_Compliance;
        private string m_Pathologist;
        private string m_Safety;
        private string m_Supplies;
        private string m_Equipment;
        private string m_Clients;
        private string m_BlockCount;
        private string m_Fedx;

        public HuddleDashboard()
        {

        }

        public static HuddleDashboard Load()
        {
            HuddleDashboard result = new HuddleDashboard();            

            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "Select * from tblHuddleDashboard limit 1;";
            cmd.CommandType = System.Data.CommandType.Text;

            using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new SqlDataReaderPropertyWriter(result, dr);
                        sqlDataReaderPropertyWriter.WriteProperties();                     
                    }
                }
            }
            return result;
        }

        [PersistentPrimaryKeyProperty(false)]
        [PersistentProperty()]
        public string HuddleDashboardId
        {
            get { return this.m_HuddleDashboardId; }
            set
            {
                if (this.m_HuddleDashboardId != value)
                {
                    this.m_HuddleDashboardId = value;
                    this.NotifyPropertyChanged("HuddleDashboardId");
                }
            }
        }

        [PersistentProperty()]
        public string Staffing
        {
            get { return this.m_Staffing; }
            set
            {
                if (this.m_Staffing != value)
                {
                    this.m_Staffing = value;
                    this.NotifyPropertyChanged("Staffing");
                }
            }
        }

        [PersistentProperty()]
        public string Quality
        {
            get { return this.m_Quality; }
            set
            {
                if (this.m_Quality != value)
                {
                    this.m_Quality = value;
                    this.NotifyPropertyChanged("Quality");
                }
            }
        }

        [PersistentProperty()]
        public string Compliance
        {
            get { return this.m_Compliance; }
            set
            {
                if (this.m_Compliance != value)
                {
                    this.m_Compliance = value;
                    this.NotifyPropertyChanged("Compliance");
                }
            }
        }

        [PersistentProperty()]
        public string Pathologist
        {
            get { return this.m_Pathologist; }
            set
            {
                if (this.m_Pathologist != value)
                {
                    this.m_Pathologist = value;
                    this.NotifyPropertyChanged("Pathologist");
                }
            }
        }

        [PersistentProperty()]
        public string Safety
        {
            get { return this.m_Safety; }
            set
            {
                if (this.m_Safety != value)
                {
                    this.m_Safety = value;
                    this.NotifyPropertyChanged("Safety");
                }
            }
        }

        [PersistentProperty()]
        public string Supplies
        {
            get { return this.m_Supplies; }
            set
            {
                if (this.m_Supplies != value)
                {
                    this.m_Supplies = value;
                    this.NotifyPropertyChanged("Supplies");
                }
            }
        }

        [PersistentProperty()]
        public string Equipment
        {
            get { return this.m_Equipment; }
            set
            {
                if (this.m_Equipment != value)
                {
                    this.m_Equipment = value;
                    this.NotifyPropertyChanged("Equipment");
                }
            }
        }

        [PersistentProperty()]
        public string Clients
        {
            get { return this.m_Clients; }
            set
            {
                if (this.m_Clients != value)
                {
                    this.m_Clients = value;
                    this.NotifyPropertyChanged("Clients");
                }
            }
        }

        [PersistentProperty()]
        public string Fedx
        {
            get { return this.m_Fedx; }
            set
            {
                if (this.m_Fedx != value)
                {
                    this.m_Fedx = value;
                    this.NotifyPropertyChanged("Fedx");
                }
            }
        }

        [PersistentProperty()]
        public string BlockCount
        {
            get { return this.m_BlockCount; }
            set
            {
                if (this.m_BlockCount != value)
                {
                    this.m_BlockCount = value;
                    this.NotifyPropertyChanged("BlockCount");
                }
            }
        }

        public void Save()
        {
            string staffing = this.m_Staffing != null ? "'" + Escape(this.m_Staffing) + "'" : "null";
            string quality = this.m_Quality != null ? "'" + Escape(this.m_Quality) + "'" : "null";
            string compliance = this.m_Compliance != null ? "'" + Escape(this.m_Compliance) + "'" : "null";
            string pathologist = this.m_Pathologist != null ? "'" + Escape(this.m_Pathologist) + "'" : "null";
            string safety = this.m_Safety != null ? "'" + Escape(this.m_Safety) + "'" : "null";
            string supplies = this.m_Supplies != null ? "'" + Escape(this.m_Supplies) + "'" : "null";
            string equipment = this.m_Equipment != null ? "'" + Escape(this.m_Equipment) + "'" : "null";
            string clients = this.m_Clients != null ? "'" + Escape(this.m_Clients) + "'" : "null";
            string fedx = this.m_Fedx != null ? "'" + Escape(this.m_Fedx) + "'" : "null";
            string blockCount = this.m_BlockCount != null ? "'" + Escape(this.m_BlockCount) + "'" : "null";

            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = $"Update tblHuddleDashboard set Staffing = {staffing}, " +
                $"Quality = {quality}, " +
                $"Compliance = {compliance}, " +
                $"Pathologist = {pathologist}, " +
                $"Safety = {safety}, " +
                $"Supplies = {supplies}, " +
                $"Equipment = {equipment}, " +
                $"Clients = {clients}, " +
                $"BlockCount = {blockCount}, " +
                $"FedX = {fedx} " +
                $"where HuddleDashboardId = '{this.m_HuddleDashboardId}'";
            cmd.CommandType = System.Data.CommandType.Text;
            using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                cmd.ExecuteNonQuery();
            }
        }

        public string Escape(string value)
        {
            if(string.IsNullOrEmpty(value) == true)
            {
                return null;
            }
            else
            {
                return value.Replace("'", "''");
            }
        }

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
    }
}
