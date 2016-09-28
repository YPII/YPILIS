using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackExchange.Redis;
using System.ComponentModel;

namespace YellowstonePathology.Business.BarcodeScanning
{
    public class EmbeddingScan : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string m_AliquotOrderId;
        private string m_ProcessorRunId;
        private string m_ProcessorRun;
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
            this.m_ProcessorRunId = hashEntries[1].Value;
            this.m_ProcessorRun = hashEntries[2].Value;
            this.m_DateScanned = DateTime.Parse(hashEntries[3].Value);
            this.m_ScannedById = Convert.ToInt32(hashEntries[4].Value);
            this.m_ScannedBy = hashEntries[5].Value;
            this.m_Updated = Convert.ToBoolean(hashEntries[6].Value);
        }

        public EmbeddingScan(string aliquotOrderId, string processorRunId, string processorRun)
        {
            this.m_AliquotOrderId = aliquotOrderId;
            this.m_ProcessorRunId = processorRunId;
            this.m_ProcessorRun = processorRun;
            this.m_DateScanned = DateTime.Now;
            this.m_ScannedBy = YellowstonePathology.Business.User.SystemIdentity.Instance.User.UserName;
            this.m_ScannedById = YellowstonePathology.Business.User.SystemIdentity.Instance.User.UserId;
            this.m_Updated = false;
        }

        public string AliquotOrderId
        {
            get { return this.m_AliquotOrderId; }
        }

        public EmbeddingScan(string processorId)
        {

        }

        public string ProcessorRunId
        {
            get { return this.m_ProcessorRunId; }
        }

        public string ProcessorRun
        {
            get { return this.m_ProcessorRun; }
        }

        public string ScannedBy
        {
            get { return this.m_ScannedBy; }
        }

        public int ScannedById
        {
            get { return this.m_ScannedById; }
        }

        public DateTime DateScanned
        {
            get { return this.m_DateScanned; }
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

        public string HashKey
        {
            get { return "EmbeddingScan:" + this.m_AliquotOrderId; }
        }

        public HashEntry[] GetHasEntries()
        {
            HashEntry[] hashEntries = new HashEntry[7];
            hashEntries[0] = new HashEntry("AliquotOrderId", this.m_AliquotOrderId);
            hashEntries[1] = new HashEntry("ProcessorRunId", this.m_ProcessorRunId);
            hashEntries[2] = new HashEntry("ProcessorRun", this.m_ProcessorRun);
            hashEntries[3] = new HashEntry("DateScanned", DateTime.Now.ToString());
            hashEntries[4] = new HashEntry("ScannedById", this.m_ScannedById);
            hashEntries[5] = new HashEntry("ScannedBy", this.m_ScannedBy);
            hashEntries[6] = new HashEntry("Updated", this.m_Updated.ToString());
            return hashEntries;
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