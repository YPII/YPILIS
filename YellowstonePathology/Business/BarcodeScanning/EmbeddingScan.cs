using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackExchange.Redis;
using System.ComponentModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace YellowstonePathology.Business.BarcodeScanning
{
    public class EmbeddingScan : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string m_AliquotOrderId;
        private Nullable<DateTime> m_ProcessorStartTime;
        private Nullable<TimeSpan> m_ProcessorFixationDuration;
        private DateTime m_DateScanned;
        private string m_ScannedBy;
        private int m_ScannedById;
        private bool m_Updated;        

        public EmbeddingScan()
        {

        }

        public EmbeddingScan(HashEntry[] hashEntries)
        {
            this.m_AliquotOrderId = hashEntries[0].Value;
            this.m_ProcessorStartTime = Business.Helper.DateTimeExtensions.NullableDateTimeFromString(hashEntries[1].Value);
            this.m_ProcessorFixationDuration = Business.Helper.DateTimeExtensions.NullableTimeSpanFromString(hashEntries[2].Value);
            this.m_DateScanned = DateTime.Parse(hashEntries[3].Value);
            this.m_ScannedById = Convert.ToInt32(hashEntries[4].Value);
            this.m_ScannedBy = hashEntries[5].Value;
            this.m_Updated = Convert.ToBoolean(hashEntries[6].Value);           
        }

        public EmbeddingScan(string aliquotOrderId, DateTime processorStartTime, TimeSpan processorFixationDuration)
        {
            this.m_AliquotOrderId = aliquotOrderId;
            this.m_ProcessorStartTime = processorStartTime;
            this.m_ProcessorFixationDuration = processorFixationDuration;
            this.m_DateScanned = DateTime.Now;
            this.m_ScannedBy = Business.User.SystemIdentity.Instance.User.UserName;
            this.m_ScannedById = Business.User.SystemIdentity.Instance.User.UserId;
            this.m_Updated = false;
        }

        public string AliquotOrderId
        {
            get { return this.m_AliquotOrderId; }
            set { this.m_AliquotOrderId = value; }
        }

        public EmbeddingScan(string processorId)
        {

        }        

        public string ScannedBy
        {
            get { return this.m_ScannedBy; }
            set { this.m_ScannedBy = value; }
        }

        public int ScannedById
        {
            get { return this.m_ScannedById; }
            set { this.m_ScannedById = value; }
        }

        public DateTime DateScanned
        {
            get { return this.m_DateScanned; }
            set { this.m_DateScanned = value; }
        }

        public Nullable<DateTime> ProcessorStartTime
        {
            get { return this.m_ProcessorStartTime; }
            set { this.m_ProcessorStartTime = value; }
        }

        public Nullable<TimeSpan> ProcessorFixationDuration
        {
            get { return this.m_ProcessorFixationDuration; }
            set { this.m_ProcessorFixationDuration = value; }
        }

        public bool Updated
        {
            get { return this.m_Updated; }
            set
            {
                if (this.m_Updated != value)
                {
                    this.m_Updated = true;
                    this.NotifyPropertyChanged("Updated");
                }
            }
        }       

        public HashEntry[] GetHasEntries()
        {
            HashEntry[] hashEntries = new HashEntry[7];
            hashEntries[0] = new HashEntry("AliquotOrderId", this.m_AliquotOrderId);                                                
            hashEntries[1] = new HashEntry("ProcessorStartTime", this.GetProcessorStartTimeHashString());
            hashEntries[2] = new HashEntry("ProcessorFixationDuration", this.GetProcessorFixationDurationHashString());
            hashEntries[3] = new HashEntry("DateScanned", DateTime.Now.ToString());
            hashEntries[4] = new HashEntry("ScannedById", this.m_ScannedById);
            hashEntries[5] = new HashEntry("ScannedBy", this.m_ScannedBy);
            hashEntries[6] = new HashEntry("Updated", this.m_Updated.ToString());
            return hashEntries;
        }        

        private string GetProcessorStartTimeHashString()
        {
            if(this.m_ProcessorStartTime.HasValue == true)
            {
                return this.m_ProcessorStartTime.ToString();
            }
            else
            {
                return "null";
            }
        }

        private string GetProcessorFixationDurationHashString()
        {
            if (this.m_ProcessorFixationDuration.HasValue == true)
            {
                return this.m_ProcessorFixationDuration.Value.Ticks.ToString();
            }
            else
            {
                return "null";
            }
        }        

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        public string ToJson()
        {
            StringBuilder result = new StringBuilder();
            result.Append("{");
            result.Append("\"Updated\": ");
            result.Append("\"" + this.m_Updated.ToString() + "\"");
            result.Append(",\"ScannedBy\": ");
            result.Append("\"" + this.m_ScannedBy + "\"");
            result.Append(",\"DateScanned\": ");
            result.Append("\"" + this.m_DateScanned.ToString() + "\"");
            result.Append(",\"ScannedById\": ");
            result.Append("\"" + this.m_ScannedById.ToString() + "\"");
            result.Append(",\"AliquotOrderId\": ");
            result.Append("\"" + this.m_AliquotOrderId + "\"");
            result.Append(",\"ProcessorStartTime\": ");
            result.Append("\"" + this.GetProcessorStartTimeHashString() + "\"");
            result.Append(",\"ProcessorFixationDuration\": ");
            result.Append("\"" + GetProcessorFixationDurationHashString() + "\"");
            result.Append("}");

            return result.ToString();
        }

        public static EmbeddingScan FromJson(string json)
        {
            JObject jObject = JsonConvert.DeserializeObject<JObject>(json);
            EmbeddingScan result = new BarcodeScanning.EmbeddingScan();

            result.m_AliquotOrderId = jObject["AliquotOrderId"].ToString();
            result.m_ProcessorStartTime = Business.Helper.DateTimeExtensions.NullableDateTimeFromString(jObject["ProcessorStartTime"].ToString());
            result.m_ProcessorFixationDuration = Business.Helper.DateTimeExtensions.NullableTimeSpanFromString(jObject["ProcessorFixationDuration"].ToString());
            result.m_DateScanned = DateTime.Parse(jObject["DateScanned"].ToString());
            result.m_ScannedById = Convert.ToInt32(jObject["ScannedById"].ToString());
            result.m_ScannedBy = jObject["ScannedBy"].ToString();
            result.m_Updated = Convert.ToBoolean(jObject["Updated"].ToString());

            return result;
        }

        public void UpdateFromExistingScan(EmbeddingScan scan)
        {
            this.m_Updated = scan.Updated;
        }
    }
}