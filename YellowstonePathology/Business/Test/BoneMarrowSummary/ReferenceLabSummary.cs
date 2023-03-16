using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Test.BoneMarrowSummary
{
    public class ReferenceLabSummary : YellowstonePathology.Business.Test.PanelSetOrder
	{
        private string m_ReferenceReportNo;
        private string m_PanelSetId;
        private string m_PanelSetName;
        private string m_ResultSummary;
        private string m_SummaryReportNo;

        public ReferenceLabSummary()
        {

        }

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "500", "null", "varchar")]
		public string ReferenceReportNo
		{
			get { return this.m_ReferenceReportNo; }
			set
			{
				if (this.m_ReferenceReportNo != value)
				{
					this.m_ReferenceReportNo = value;
					this.NotifyPropertyChanged("ReferenceReportNo");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "500", "null", "varchar")]
		public string PanelSetId
		{
			get { return this.m_PanelSetId; }
			set
			{
				if (this.m_PanelSetId != value)
				{
					this.m_PanelSetId = value;
					this.NotifyPropertyChanged("PanelSetId");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "500", "null", "varchar")]
		public string PanelSetName
		{
			get { return this.m_PanelSetName; }
			set
			{
				if (this.m_PanelSetName != value)
				{
					this.m_PanelSetName = value;
					this.NotifyPropertyChanged("PanelSetName");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "500", "null", "varchar")]
		public string ResultSummary
		{
			get { return this.m_ResultSummary; }
			set
			{
				if (this.m_ResultSummary != value)
				{
					this.m_ResultSummary = value;
					this.NotifyPropertyChanged("ResultSummary");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "500", "null", "varchar")]
		public string SummaryReportNo
		{
			get { return this.m_SummaryReportNo; }
			set
			{
				if (this.m_SummaryReportNo != value)
				{
					this.m_SummaryReportNo = value;
					this.NotifyPropertyChanged("SummaryReportNo");
				}
			}
		}
	}
}
