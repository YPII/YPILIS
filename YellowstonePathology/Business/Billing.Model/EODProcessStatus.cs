using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Billing.Model
{
    public class EODProcessStatus
    {
        private DateTime m_ProcessDate;
        private DateTime? m_MRNAcctUpdate;
        private DateTime? m_ADTMatch;
        private DateTime? m_ProcessSVHCDMFiles;
        private DateTime? m_TransferSVHFiles;
        private DateTime? m_SendSVHClinicEmail;
        private DateTime? m_ProcessPSAFiles;
        private DateTime? m_ProcessAPSFiles;
        private DateTime? m_TransferPSAFiles;
        private DateTime? m_TransferAPSFiles;
        private DateTime? m_FaxTheReport;

        public EODProcessStatus()
        { }

        [PersistentProperty()]
        public DateTime ProcessDate
        {
            get { return this.m_ProcessDate; }
            set { this.m_ProcessDate = value; }
        }

        [PersistentProperty()]
        public DateTime? MRNAcctUpdate
        {
            get { return this.m_MRNAcctUpdate; }
            set { this.m_MRNAcctUpdate = value; }
        }

        [PersistentProperty()]
        public DateTime? ADTMatch
        {
            get { return this.m_ADTMatch; }
            set { this.m_ADTMatch = value; }
        }

        [PersistentProperty()]
        public DateTime? ProcessSVHCDMFiles
        {
            get { return this.m_ProcessSVHCDMFiles; }
            set { this.m_ProcessSVHCDMFiles = value; }
        }

        [PersistentProperty()]
        public DateTime? TransferSVHFiles
        {
            get { return this.m_TransferSVHFiles; }
            set { this.m_TransferSVHFiles = value; }
        }

        [PersistentProperty()]
        public DateTime? SendSVHClinicEmail
        {
            get { return this.m_SendSVHClinicEmail; }
            set { this.m_SendSVHClinicEmail = value; }
        }

        [PersistentProperty()]
        public DateTime? ProcessPSAFiles
        {
            get { return this.m_ProcessPSAFiles; }
            set { this.m_ProcessPSAFiles = value; }
        }

        [PersistentProperty()]
        public DateTime? ProcessAPSFiles
        {
            get { return this.m_ProcessAPSFiles; }
            set { this.m_ProcessAPSFiles = value; }
        }

        [PersistentProperty()]
        public DateTime? TransferPSAFiles
        {
            get { return this.m_TransferPSAFiles; }
            set { this.m_TransferPSAFiles = value; }
        }

        [PersistentProperty()]
        public DateTime? TransferAPSFiles
        {
            get { return this.m_TransferAPSFiles; }
            set { this.m_TransferAPSFiles = value; }
        }


        [PersistentProperty()]
        public DateTime? FaxTheReport
        {
            get { return this.m_FaxTheReport; }
            set { this.m_FaxTheReport = value; }
        }

        public string ProcessDateDay
        {
            get { return this.m_ProcessDate.ToShortDateString() + " - " +  this.m_ProcessDate.DayOfWeek; }
        }

        public string MRNAcctUpdateTime
        {
            get
            {
                if (this.m_MRNAcctUpdate.HasValue)
                {
                    return this.m_MRNAcctUpdate.Value.ToShortTimeString();
                }
                return string.Empty;
            }
        }

        public string ADTMatchTime
        {
            get
            {
                if (this.m_ADTMatch.HasValue)
                {
                    return this.m_ADTMatch.Value.ToShortTimeString();
                }
                return string.Empty;
            }
        }

        public string ProcessSVHCDMFilesTime
        {
            get
            {
                if (this.m_ProcessSVHCDMFiles.HasValue)
                {
                    return this.m_ProcessSVHCDMFiles.Value.ToShortTimeString();
                }
                return string.Empty;
            }
        }

        public string TransferSVHFilesTime
        {
            get
            {
                if (this.m_TransferSVHFiles.HasValue)
                {
                    return this.m_TransferSVHFiles.Value.ToShortTimeString();
                }
                return string.Empty;
            }
        }

        public string SendSVHClinicEmailTime
        {
            get
            {
                if (this.m_SendSVHClinicEmail.HasValue)
                {
                    return this.m_SendSVHClinicEmail.Value.ToShortTimeString();
                }
                return string.Empty;
            }
        }

        public string ProcessPSAFilesTime
        {
            get
            {
                if (this.m_ProcessPSAFiles.HasValue)
                {
                    return this.m_ProcessPSAFiles.Value.ToShortTimeString();
                }
                return string.Empty;
            }
        }

        public string TransferPSAFilesTime
        {
            get
            {
                if (this.m_TransferPSAFiles.HasValue)
                {
                    return this.m_TransferPSAFiles.Value.ToShortTimeString();
                }
                return string.Empty;
            }
        }

        public string FaxTheReportTime
        {
            get
            {
                if (this.m_FaxTheReport.HasValue)
                {
                    return this.m_FaxTheReport.Value.ToShortTimeString();
                }
                return string.Empty;
            }
        }
    }
}
