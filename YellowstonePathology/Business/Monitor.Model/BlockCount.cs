using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Monitor.Model
{
    [PersistentClass("tblBlockCount", "YPIDATA")]
    public class BlockCount : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private DateTime m_BlockCountDate;        
        private int m_YPIBlocks;
        private int m_YPIPaths;    
        private int? m_BozemanBlocks;
        private int m_BozemanPaths;
        private int? m_BlocksToSend;
        private int m_BlocksPerPath;
        private int m_BlocksPerPathBozeman;

        public BlockCount()
        {

        }

        [PersistentPrimaryKeyProperty(false)]
        public DateTime BlockCountDate
        {
            get { return this.m_BlockCountDate; }
            set
            {
                if (this.m_BlockCountDate != value)
                {
                    this.m_BlockCountDate = value;
                    this.NotifyPropertyChanged("BlockCountDate");
                }
            }
        }    
        
        public string DateDisplayString
        {
            get { return BlockCountDate.DayOfWeek.ToString() + "-" + BlockCountDate.Month + "/" + BlockCountDate.Day;  }
        }

        [PersistentProperty()]
        public int YPIBlocks
        {
            get { return this.m_YPIBlocks; }
            set
            {
                if(this.m_YPIBlocks != value)
                {
                    this.m_YPIBlocks = value;
                    this.NotifyPropertyChanged("YPIBlocks");
                }                
            }
        }

        [PersistentProperty()]
        public int YPIPaths
        {
            get { return this.m_YPIPaths; }
            set
            {
                if (this.m_YPIPaths != value)
                {
                    this.m_YPIPaths = value;
                    this.NotifyPropertyChanged("YPIPaths");
                }
            }
        }

        [PersistentProperty()]
        public int? BozemanBlocks
        {
            get { return this.m_BozemanBlocks; }
            set
            {
                if (this.m_BozemanBlocks != value)
                {
                    this.m_BozemanBlocks = value;
                    this.NotifyPropertyChanged("BozemanBlocks");
                }
            }
        }

        [PersistentProperty()]
        public int BozemanPaths
        {
            get { return this.m_BozemanPaths; }
            set
            {
                if (this.m_BozemanPaths != value)
                {
                    this.m_BozemanPaths = value;
                    this.NotifyPropertyChanged("BozemanPaths");
                }
            }
        }

        [PersistentProperty()]
        public int BlocksPerPath
        {
            get { return this.m_BlocksPerPath; }
            set
            {
                if (this.m_BlocksPerPath != value)
                {
                    this.m_BlocksPerPath = value;
                    this.NotifyPropertyChanged("BlocksPerPath");
                }
            }
        }

        [PersistentProperty()]
        public int BlocksPerPathBozeman
        {
            get { return this.m_BlocksPerPathBozeman; }
            set
            {
                if (this.m_BlocksPerPathBozeman != value)
                {
                    this.m_BlocksPerPathBozeman = value;
                    this.NotifyPropertyChanged("BlocksPerPathBozeman");
                }
            }
        }

        [PersistentProperty()]
        public int? BlocksToSend
        {
            get { return this.m_BlocksToSend; }
            set
            {
                if (this.m_BlocksToSend != value)
                {
                    this.m_BlocksToSend = value;
                    this.NotifyPropertyChanged("BlocksToSend");
                }
            }
        }

        public int GetTotalBlockCount()
        {
            return this.m_YPIBlocks + (this.m_BozemanBlocks.HasValue ? this.m_BozemanBlocks.Value : 0);
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
