using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Xml.Serialization;

namespace YellowstonePathology.Business.Search
{
	[XmlType("ReportSearchList")]
	public class ReportSearchList : ObservableCollection<ReportSearchItem>
    {
        private ReportSearchItem m_CurrentReportSearchItem;
        private int m_CurrentIndex;
        private bool m_EndOfList;
        private bool m_BeginningOfList;

		public ReportSearchList()
		{

		}

        public ReportSearchItem CurrentReportSearchItem
        {
            get { return this.m_CurrentReportSearchItem; }            
        }

        public void SetLockIsAquiredByMe(Business.Test.AccessionOrder accessionOrder)
        {
            foreach(ReportSearchItem item in this)
            {                
                if (item.MasterAccessionNo == accessionOrder.MasterAccessionNo)
                {
                    item.LockAquired = accessionOrder.AccessionLock.IsLockAquired;
                    item.IsLockAquiredByMe = accessionOrder.AccessionLock.IsLockAquiredByMe;                    
                }
                else
                {
                    item.IsLockAquiredByMe = false;
                    item.LockAquired = false;
                }
            }
        }

        public List<string> GetMasterAccessionNoList()
        {
            List<string> resultList = new List<string>();
            foreach(ReportSearchItem rsi in this)
            {
                resultList.Add(rsi.MasterAccessionNo);
            }
            return resultList;
        }        

        public void SetCurrentReportSearchItem(string reportNo)
        {
            int index = 0;
            foreach (ReportSearchItem item in this)
            {
                if (item.ReportNo == reportNo)
                {
                    this.m_CurrentReportSearchItem = item;
                    this.m_BeginningOfList = false;
                    this.m_EndOfList = false;
                    this.m_CurrentIndex = index;
                    break;
                }
                else
                {
                    index += 1;
                }
            }            
        }

        public bool EndOfList
        {
            get { return this.m_EndOfList; }
        }

        public bool BeginningOfList
        {
            get { return this.m_BeginningOfList; }
        }

        public void MoveNext()
        {
            this.m_CurrentIndex += 1;
            if (this.m_CurrentIndex == this.Count)
            {
                this.m_EndOfList = true;
                this.m_CurrentIndex -= 1;
            }
            else
            {
                this.m_EndOfList = false;
                this.m_CurrentReportSearchItem = this[this.m_CurrentIndex];
            }
        }

        public void MoveBack()
        {
            this.m_CurrentIndex -= 1;
            if (this.m_CurrentIndex == -1)
            {
                this.m_BeginningOfList = true;
                this.m_CurrentIndex += 1;
            }
            else
            {
                this.m_BeginningOfList = false;
                this.m_CurrentReportSearchItem = this[this.m_CurrentIndex];
            }
        }
	}
}
