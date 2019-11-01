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
        private int m_BozemanBlocks;
        private int m_BlocksToSend;

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
        public int BozemanBlocks
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
        public int BlocksToSend
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

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
    }
}
