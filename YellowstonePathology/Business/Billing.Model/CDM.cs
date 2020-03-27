using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using YellowstonePathology.Business.Persistence;
using System.Data;
using MySql.Data.MySqlClient;

namespace YellowstonePathology.Business.Billing.Model
{
    public class CDM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string m_CDMId;
        private string m_CDMCode;
        private string m_CPTCode;
        private string m_ProcedureName;
        private string m_CDMClient;

        public CDM()
        { }

        [PersistentPrimaryKeyProperty(false)]
        public string CDMId
        {
            get { return this.m_CDMId; }
            set
            {
                if (this.m_CDMId != value)
                {
                    this.m_CDMId = value;
                    this.NotifyPropertyChanged("CDMId");
                }
            }
        }

        [PersistentProperty(false)]
        public string CDMCode
        {
            get { return this.m_CDMCode; }
            set
            {
                if (this.m_CDMCode != value)
                {
                    this.m_CDMCode = value;
                    this.NotifyPropertyChanged("CDMCode");
                }
            }
        }

        [PersistentProperty()]
        public string CPTCode
        {
            get { return this.m_CPTCode; }
            set
            {
                if (this.m_CPTCode != value)
                {
                    this.m_CPTCode = value;
                    this.NotifyPropertyChanged("CPTCode");
                }
            }
        }

        [PersistentProperty()]
        public string ProcedureName
        {
            get { return this.m_ProcedureName; }
            set
            {
                if (this.m_ProcedureName != value)
                {
                    this.m_ProcedureName = value;
                    this.NotifyPropertyChanged("ProcedureName");
                }
            }
        }

        [PersistentProperty()]
        public string CDMClient
        {
            get { return this.m_CDMClient; }
            set
            {
                if (this.m_CDMClient != value)
                {
                    this.m_CDMClient = value;
                    this.NotifyPropertyChanged("CDMClient");
                }
            }
        }

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        public void Save()
        {
            MySqlCommand cmd = new MySqlCommand("UPDATE tblCDM set CDMCode = @CDMCode, CPTCode = @CPTCode, ProcedureName = @ProcedureName, CDMClient = @CDMClient where CDMCode = @CDMCode;");
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@CDMId", this.m_CDMId);
            cmd.Parameters.AddWithValue("@CDMCode", this.m_CDMCode);
            cmd.Parameters.AddWithValue("@CPTCode", this.m_CPTCode);
            cmd.Parameters.AddWithValue("@ProcedureName", this.m_ProcedureName);
            cmd.Parameters.AddWithValue("@CDMClient", this.m_CDMClient);

            using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                cmd.ExecuteNonQuery();
            }
        }
    }
}
