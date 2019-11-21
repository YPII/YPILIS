using System;
using System.ComponentModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Data;
using MySql.Data.MySqlClient;

namespace YellowstonePathology.Business.Stain.Model
{
    public class Stain : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string m_StainId;
        private string m_StainType;
        private string m_OrderComment;
        private string m_TestId;
        private string m_StainName;
        private string m_StainAbbreviation;
        private string m_AliquotType;
        private string m_DefaultResult;
        private string m_HistologyDisplayString;
        private string m_StainerType;
        private string m_VentanaBenchMarkProtocolName;
        private string m_CPTCode;
        private string m_SubsequentCPTCode;
        private string m_GCode;
        private string m_SubsequentGCode;
        private string m_VentanaBenchMarkWetProtocolName;
        private int? m_VentanaBenchMarkId;
        private int? m_VentanaBenchMarkWetId;
        private int m_StainResultGroupId;
        private bool m_IsBillable;
        private bool m_HasGCode;
        private bool m_IsDualOrder;
        private bool m_HasCptCodeLevels;
        private bool m_Active;
        private bool m_NeedsAcknowledgement;
        private bool m_UseWetProtocol;
        private bool m_PerformedByHand;
        private bool m_RequestForAdditionalReport;
        private bool m_HasWetProtocol;

        protected Stain m_FirstStain;
        protected Stain m_SecondStain;
        protected string m_DepricatedFirstStainId;
        protected string m_DepricatedSecondStainId;

        public Stain() { }

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        public string ToJSON()
        {
            var camelCaseFormatter = new JsonSerializerSettings();
            camelCaseFormatter.ContractResolver = new CamelCasePropertyNamesContractResolver();
            string result = JsonConvert.SerializeObject(this, Formatting.Indented, camelCaseFormatter);
            return result;
        }

        public void Save()
        {
            string jString = this.ToJSON();
            MySqlCommand cmd = new MySqlCommand("Insert tblStain(StainId, JSONValue) values(@StainId, @JSONValue) ON DUPLICATE KEY UPDATE StainId = @StainId, JSONValue = @JSONValue;");
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@JSONValue", jString);
            cmd.Parameters.AddWithValue("@StainId", this.m_StainId);

            using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                cmd.ExecuteNonQuery();
            }
            Test.Model.TestCollectionInstance.Reload();
        }

        public string StainId
        {
            get { return this.m_StainId; }
            set
            {
                if (this.m_StainId != value)
                {
                    this.m_StainId = value;
                    this.NotifyPropertyChanged("StainId");
                }
            }
        }

        public string StainType
        {
            get { return this.m_StainType; }
            set
            {
                if (this.m_StainType != value)
                {
                    this.m_StainType = value;
                    this.NotifyPropertyChanged("StainType");
                }
            }
        }

        public string OrderComment
        {
            get { return this.m_OrderComment; }
            set
            {
                if (this.m_OrderComment != value)
                {
                    this.m_OrderComment = value;
                    this.NotifyPropertyChanged("OrderComment");
                }
            }
        }

        public string TestId
        {
            get { return this.m_TestId; }
            set
            {
                if (this.m_TestId != value)
                {
                    this.m_TestId = value;
                    this.NotifyPropertyChanged("TestId");
                }
            }
        }

        public string StainName
        {
            get { return this.m_StainName; }
            set
            {
                if (this.m_StainName != value)
                {
                    this.m_StainName = value;
                    this.NotifyPropertyChanged("StainName");
                }
            }
        }

        public string StainAbbreviation
        {
            get { return this.m_StainAbbreviation; }
            set
            {
                if (this.m_StainAbbreviation != value)
                {
                    this.m_StainAbbreviation = value;
                    this.NotifyPropertyChanged("StainAbbreviation");
                }
            }
        }

        public string AliquotType
        {
            get { return this.m_AliquotType; }
            set
            {
                if (this.m_AliquotType != value)
                {
                    this.m_AliquotType = value;
                    this.NotifyPropertyChanged("AliquotType");
                }
            }
        }

        public string DefaultResult
        {
            get { return this.m_DefaultResult; }
            set
            {
                if (this.m_DefaultResult != value)
                {
                    this.m_DefaultResult = value;
                    this.NotifyPropertyChanged("DefaultResult");
                }
            }
        }

        public string HistologyDisplayString
        {
            get
            {
                string result = this.m_StainAbbreviation;
                if (this.m_UseWetProtocol == true) result = result + "(W)";
                return result;
            }           
        }

        public string StainerType
        {
            get { return this.m_StainerType; }
            set
            {
                if (this.m_StainerType != value)
                {
                    this.m_StainerType = value;
                    this.NotifyPropertyChanged("StainerType");
                }
            }
        }

        public string VentanaBenchMarkProtocolName
        {
            get { return this.m_VentanaBenchMarkProtocolName; }
            set
            {
                if (this.m_VentanaBenchMarkProtocolName != value)
                {
                    this.m_VentanaBenchMarkProtocolName = value;
                    this.NotifyPropertyChanged("VentanaBenchMarkProtocolName");
                }
            }
        }

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

        public string SubsequentCPTCode
        {
            get { return this.m_SubsequentCPTCode; }
            set
            {
                if (this.m_SubsequentCPTCode != value)
                {
                    this.m_SubsequentCPTCode = value;
                    this.NotifyPropertyChanged("SubsequentCPTCode");
                }
            }
        }

        public string GCode
        {
            get { return this.m_GCode; }
            set
            {
                if (this.m_GCode != value)
                {
                    this.m_GCode = value;
                    this.NotifyPropertyChanged("GCode");
                }
            }
        }

        public string SubsequentGCode
        {
            get { return this.m_SubsequentGCode; }
            set
            {
                if (this.m_SubsequentGCode != value)
                {
                    this.m_SubsequentGCode = value;
                    this.NotifyPropertyChanged("SubsequentGCode");
                }
            }
        }

        public string VentanaBenchMarkWetProtocolName
        {
            get { return this.m_VentanaBenchMarkWetProtocolName; }
            set
            {
                if (this.m_VentanaBenchMarkWetProtocolName != value)
                {
                    this.m_VentanaBenchMarkWetProtocolName = value;
                    this.NotifyPropertyChanged("VentanaBenchMarkWetProtocolName");
                }
            }
        }

        public int? VentanaBenchMarkId
        {
            get { return this.m_VentanaBenchMarkId; }
            set
            {
                if (this.m_VentanaBenchMarkId != value)
                {
                    this.m_VentanaBenchMarkId = value;
                    this.NotifyPropertyChanged("VentanaBenchMarkId");
                }
            }
        }

        public int? VentanaBenchMarkWetId
        {
            get { return this.m_VentanaBenchMarkWetId; }
            set
            {
                if (this.m_VentanaBenchMarkWetId != value)
                {
                    this.m_VentanaBenchMarkWetId = value;
                    this.NotifyPropertyChanged("VentanaBenchMarkWetId");
                }
            }
        }

        public int StainResultGroupId
        {
            get { return this.m_StainResultGroupId; }
            set
            {
                if (this.m_StainResultGroupId != value)
                {
                    this.m_StainResultGroupId = value;
                    this.NotifyPropertyChanged("StainResultGroupId");
                }
            }
        }

        public bool IsBillable
        {
            get { return this.m_IsBillable; }
            set
            {
                if (this.m_IsBillable != value)
                {
                    this.m_IsBillable = value;
                    this.NotifyPropertyChanged("IsBillable");
                }
            }
        }

        public bool HasGCode
        {
            get { return this.m_HasGCode; }
            set
            {
                if (this.m_HasGCode != value)
                {
                    this.m_HasGCode = value;
                    this.NotifyPropertyChanged("HasGCode");
                }
            }
        }

        public bool IsDualOrder
        {
            get { return this.m_IsDualOrder; }
            set
            {
                if (this.m_IsDualOrder != value)
                {
                    this.m_IsDualOrder = value;
                    this.NotifyPropertyChanged("IsDualOrder");
                }
            }
        }

        public bool HasCptCodeLevels
        {
            get { return this.m_HasCptCodeLevels; }
            set
            {
                if (this.m_HasCptCodeLevels != value)
                {
                    this.m_HasCptCodeLevels = value;
                    this.NotifyPropertyChanged("HasCptCodeLevels");
                }
            }
        }

        public bool Active
        {
            get { return this.m_Active; }
            set
            {
                if (this.m_Active != value)
                {
                    this.m_Active = value;
                    this.NotifyPropertyChanged("Active");
                }
            }
        }

        public bool NeedsAcknowledgement
        {
            get { return this.m_NeedsAcknowledgement; }
            set
            {
                if (this.m_NeedsAcknowledgement != value)
                {
                    this.m_NeedsAcknowledgement = value;
                    this.NotifyPropertyChanged("NeedsAcknowledgement");
                }
            }
        }

        public bool UseWetProtocol
        {
            get { return this.m_UseWetProtocol; }
            set
            {
                if (this.m_UseWetProtocol != value)
                {
                    this.m_UseWetProtocol = value;
                    this.NotifyPropertyChanged("UseWetProtocol");
                }
            }
        }

        public bool PerformedByHand
        {
            get { return this.m_PerformedByHand; }
            set
            {
                if (this.m_PerformedByHand != value)
                {
                    this.m_PerformedByHand = value;
                    this.NotifyPropertyChanged("PerformedByHand");
                }
            }
        }

        public bool RequestForAdditionalReport
        {
            get { return this.m_RequestForAdditionalReport; }
            set
            {
                if (this.m_RequestForAdditionalReport != value)
                {
                    this.m_RequestForAdditionalReport = value;
                    this.NotifyPropertyChanged("RequestForAdditionalReport");
                }
            }
        }

        public bool HasWetProtocol
        {
            get { return this.m_HasWetProtocol; }
            set
            {
                if (this.m_HasWetProtocol != value)
                {
                    this.m_HasWetProtocol = value;
                    this.NotifyPropertyChanged("HasWetProtocol");
                }
            }
        }




        public Stain FirstStain
        {
            get { return this.m_FirstStain; }
            set
            {
                if (this.m_FirstStain != value)
                {
                    this.m_FirstStain = value;
                    this.NotifyPropertyChanged("FirstStain");
                }
            }
        }

        public Stain SecondStain
        {
            get { return this.m_SecondStain; }
            set
            {
                if (this.m_SecondStain != value)
                {
                    this.m_SecondStain = value;
                    this.NotifyPropertyChanged("SecondStain");
                }
            }
        }

        public string DepricatedFirstStainId
        {
            get { return this.m_DepricatedFirstStainId; }
            set
            {
                if (this.m_DepricatedFirstStainId != value)
                {
                    this.m_DepricatedFirstStainId = value;
                    this.NotifyPropertyChanged("DepricatedFirstStainId");
                }
            }
        }

        public string DepricatedSecondStainId
        {
            get { return this.m_DepricatedSecondStainId; }
            set
            {
                if (this.m_DepricatedSecondStainId != value)
                {
                    this.m_DepricatedSecondStainId = value;
                    this.NotifyPropertyChanged("DepricatedSecondStainId");
                }
            }
        }

    }
}
