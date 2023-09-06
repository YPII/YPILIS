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
    [PersistentClass("tblHuddleDashboard", "YPIDATA")]
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
        private string m_PapCount;
        private string m_OtherFlowCount;
        private string m_SVHBMCount;
        private string m_FollowUpProblem1;
        private string m_FollowUpProblem2;
        private string m_FollowUpProblem3;
        private string m_AdditionalComment1;
        private string m_Fedx;

        private bool m_CourierSheridan;
        private bool m_CourierCodyPowell;
        private bool m_CourierBozemanButte;
        private bool m_CourierMCSidneyForsytheHardin;
        private bool m_CourierHavreMalta;

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

        [PersistentProperty()]
        public string PapCount
        {
            get { return this.m_PapCount; }
            set
            {
                if (this.m_PapCount != value)
                {
                    this.m_PapCount = value;
                    this.NotifyPropertyChanged("PapCount");
                }
            }
        }

        [PersistentProperty()]
        public string OtherFlowCount
        {
            get { return this.m_OtherFlowCount; }
            set
            {
                if (this.m_OtherFlowCount != value)
                {
                    this.m_OtherFlowCount = value;
                    this.NotifyPropertyChanged("OtherFlowCount");
                }
            }
        }

        [PersistentProperty()]
        public string SVHBMCount
        {
            get { return this.m_SVHBMCount; }
            set
            {
                if (this.m_SVHBMCount != value)
                {
                    this.m_SVHBMCount = value;
                    this.NotifyPropertyChanged("SVHBMCount");
                }
            }
        }

        [PersistentProperty()]
        public string FollowUpProblem1
        {
            get { return this.m_FollowUpProblem1; }
            set
            {
                if (this.m_FollowUpProblem1 != value)
                {
                    this.m_FollowUpProblem1 = value;
                    this.NotifyPropertyChanged("FollowUpProblem1");
                }
            }
        }

        [PersistentProperty()]
        public string FollowUpProblem2
        {
            get { return this.m_FollowUpProblem2; }
            set
            {
                if (this.m_FollowUpProblem2 != value)
                {
                    this.m_FollowUpProblem2 = value;
                    this.NotifyPropertyChanged("FollowUpProblem2");
                }
            }
        }

        [PersistentProperty()]
        public string FollowUpProblem3
        {
            get { return this.m_FollowUpProblem3; }
            set
            {
                if (this.m_FollowUpProblem3 != value)
                {
                    this.m_FollowUpProblem3 = value;
                    this.NotifyPropertyChanged("FollowUpProblem3");
                }
            }
        }

        [PersistentProperty()]
        public string AdditionalComment1
        {
            get { return this.m_AdditionalComment1; }
            set
            {
                if (this.m_AdditionalComment1 != value)
                {
                    this.m_AdditionalComment1 = value;
                    this.NotifyPropertyChanged("AdditionalComment1");
                }
            }
        }

        [PersistentProperty()]
        public bool CourierSheridan
        {
            get { return this.m_CourierSheridan; }
            set
            {
                if (this.m_CourierSheridan != value)
                {
                    this.m_CourierSheridan = value;
                    this.NotifyPropertyChanged("CourierSheridan");
                }
            }
        }

        [PersistentProperty()]
        public bool CourierCodyPowell
        {
            get { return this.m_CourierCodyPowell; }
            set
            {
                if (this.m_CourierCodyPowell != value)
                {
                    this.m_CourierCodyPowell = value;
                    this.NotifyPropertyChanged("CourierCodyPowell");
                }
            }
        }

        [PersistentProperty()]
        public bool CourierBozemanButte
        {
            get { return this.m_CourierBozemanButte; }
            set
            {
                if (this.m_CourierBozemanButte != value)
                {
                    this.m_CourierBozemanButte = value;
                    this.NotifyPropertyChanged("CourierBozemanButte");
                }
            }
        }

        [PersistentProperty()]
        public bool CourierMCSidneyForsytheHardin
        {
            get { return this.m_CourierMCSidneyForsytheHardin; }
            set
            {
                if (this.m_CourierMCSidneyForsytheHardin != value)
                {
                    this.m_CourierMCSidneyForsytheHardin = value;
                    this.NotifyPropertyChanged("CourierMCSidneyForsytheHardin");
                }
            }
        }

        [PersistentProperty()]
        public bool CourierHavreMalta
        {
            get { return this.m_CourierHavreMalta; }
            set
            {
                if (this.m_CourierHavreMalta != value)
                {
                    this.m_CourierHavreMalta = value;
                    this.NotifyPropertyChanged("CourierHavreMalta");
                }
            }
        }

        public void Save()
        {
            Type type = this.GetType();
            List<System.Reflection.PropertyInfo> propertyList = type.GetProperties().ToList<System.Reflection.PropertyInfo>();
            string sql = Business.Sql.SQLHelper.GetUpdateCommand(typeof(HuddleDashboard), this, propertyList);

            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = sql;  
            cmd.CommandType = System.Data.CommandType.Text;
            using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                cmd.ExecuteNonQuery();
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
