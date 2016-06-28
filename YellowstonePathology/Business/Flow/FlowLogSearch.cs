using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Data;
using System.ComponentModel;

namespace YellowstonePathology.Business.Flow
{
    public class FlowLogSearch : INotifyPropertyChanged
    {
        public delegate void PropertyChangedNotificationHandler(String info);
        public event PropertyChangedEventHandler PropertyChanged;

        private FlowLogListSearchTypeEnum m_SearchType;
        private object m_Parameter;

        YellowstonePathology.Business.Flow.FlowLogList m_FlowLogList;        

        public FlowLogSearch()
        {
			this.m_FlowLogList = new FlowLogList();
        }

        public YellowstonePathology.Business.Flow.FlowLogList FlowLogList
        {
            get { return this.m_FlowLogList; }
        }        

        public void SetByLeukemiaNotFinal()
        {
            this.m_SearchType = FlowLogListSearchTypeEnum.GetByLeukemiaNotFinal;            
        }

        public void SetByTestType(int testId)
        {
            this.m_SearchType = FlowLogListSearchTypeEnum.GetByTestType;
            this.m_Parameter = testId;            
        }

        public void SetByReportNo(string reportNo)
        {
			this.m_SearchType = FlowLogListSearchTypeEnum.GetByReportNo;
            this.m_Parameter = reportNo;            
        }        

        public void SetByAccessionMonth(DateTime date)
        {
            this.m_SearchType = FlowLogListSearchTypeEnum.GetByAccessionMonth;
            this.m_Parameter = date;            
        }

        public void SetByPatientName(string patientName)
        {
            this.m_SearchType = FlowLogListSearchTypeEnum.GetByPatientName;
            this.m_Parameter = patientName;            
        }

        public void SetByPathologistId(int pathologistId)
        {
            this.m_SearchType = FlowLogListSearchTypeEnum.GetByPathologistId;
            this.m_Parameter = pathologistId;            
        }

        public void Search()
        {            
            switch (this.m_SearchType)
            {
                case FlowLogListSearchTypeEnum.GetByLeukemiaNotFinal:
                    this.m_FlowLogList = Gateway.FlowGateway.GetByLeukemiaNotFinal();
                    break;               
                case FlowLogListSearchTypeEnum.GetByTestType:
                    int testId = (int)this.m_Parameter;
                    this.m_FlowLogList = Gateway.FlowGateway.GetByTestType(testId);
                    break;
				case FlowLogListSearchTypeEnum.GetByReportNo:
                    string reportNo = (string)this.m_Parameter;
					this.m_FlowLogList = Gateway.FlowGateway.GetFlowLogListByReportNo(reportNo);
                    break;
                case FlowLogListSearchTypeEnum.GetByAccessionMonth:
                    DateTime accessionDate = (DateTime)this.m_Parameter;
                    this.m_FlowLogList = Gateway.FlowGateway.GetByAccessionMonth(accessionDate);
                    break;
                case FlowLogListSearchTypeEnum.GetByPatientName:
                    string patientName = (string)this.m_Parameter;
                    this.m_FlowLogList = Gateway.FlowGateway.GetByPatientName(patientName);
                    break;
                case FlowLogListSearchTypeEnum.GetByPathologistId:
                    int pathologistId = (int)this.m_Parameter;
                    this.m_FlowLogList = Gateway.FlowGateway.GetByPathologistId(pathologistId);
                    break;
            }
            if (this.m_FlowLogList == null)
            {
                this.m_FlowLogList = new FlowLogList();
            }
            this.NotifyPropertyChanged("");
        }

        public void Refresh()
        {
            this.Search();
        }

        protected void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
    }
}
