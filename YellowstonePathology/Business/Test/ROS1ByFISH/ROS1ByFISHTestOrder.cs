using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Test.ROS1ByFISH
{
	[PersistentClass("tblROS1ByFISHTestOrder", "tblPanelSetOrder", "YPIDATA")]
    public class ROS1ByFISHTestOrder : YellowstonePathology.Business.Test.PanelSetOrder, YellowstonePathology.Business.Interface.ISolidTumorTesting
    {        
        private string m_Result;
        private string m_Interpretation;
        private string m_ReferenceRange;
        private string m_ProbeSetDetail;
        private string m_NucleiScored;
        private string m_Method;
        private string m_ReportDisclaimer;        
        private string m_TumorNucleiPercentage;

        public ROS1ByFISHTestOrder()
        {

        }

        public ROS1ByFISHTestOrder(string masterAccessionNo, string reportNo)
        {
            this.MasterAccessionNo = masterAccessionNo;
            this.ReportNo = reportNo;            
        }

        public ROS1ByFISHTestOrder(string masterAccessionNo, string reportNo, string objectId,
            YellowstonePathology.Business.PanelSet.Model.PanelSet panelSet,            
            YellowstonePathology.Business.Interface.IOrderTarget orderTarget,
			bool distribute)
			: base(masterAccessionNo, reportNo, objectId, panelSet, orderTarget, distribute)
		{
			
		}        

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "50", "null", "varchar")]
        public string Result
        {
            get { return this.m_Result; }
            set
            {
                if (this.m_Result != value)
                {
                    this.m_Result = value;
                    this.NotifyPropertyChanged("Result");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "5000", "null", "varchar")]
        public string Interpretation
        {
            get { return this.m_Interpretation; }
            set
            {
                if (this.m_Interpretation != value)
                {
                    this.m_Interpretation = value;
                    this.NotifyPropertyChanged("Interpretation");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "500", "null", "varchar")]
        public string ReferenceRange
        {
            get { return this.m_ReferenceRange; }
            set
            {
                if (this.m_ReferenceRange != value)
                {
                    this.m_ReferenceRange = value;
                    this.NotifyPropertyChanged("ReferenceRange");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "500", "null", "varchar")]
        public string ProbeSetDetail
        {
            get { return this.m_ProbeSetDetail; }
            set
            {
                if (this.m_ProbeSetDetail != value)
                {
                    this.m_ProbeSetDetail = value;
                    this.NotifyPropertyChanged("ProbeSetDetail");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "50", "null", "varchar")]
        public string NucleiScored
        {
            get { return this.m_NucleiScored; }
            set
            {
                if (this.m_NucleiScored != value)
                {
                    this.m_NucleiScored = value;
                    this.NotifyPropertyChanged("NucleiScored");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "1000", "null", "varchar")]
        public string Method
        {
            get { return this.m_Method; }
            set
            {
                if (this.m_Method != value)
                {
                    this.m_Method = value;
                    this.NotifyPropertyChanged("Method");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "5000", "null", "varchar")]
        public string ReportDisclaimer
        {
            get { return this.m_ReportDisclaimer; }
            set
            {
                if (this.m_ReportDisclaimer != value)
                {
                    this.m_ReportDisclaimer = value;
                    this.NotifyPropertyChanged("ReportDisclaimer");
                }
            }
        }        

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "50", "null", "varchar")]
        public string TumorNucleiPercentage
        {
            get { return this.m_TumorNucleiPercentage; }
            set
            {
                if (this.m_TumorNucleiPercentage != value)
                {
                    this.m_TumorNucleiPercentage = value;
                    this.NotifyPropertyChanged("TumorNucleiPercentage");
                }
            }
        }

		public override string ToResultString(Business.Test.AccessionOrder accessionOrder)
		{
            return null;
		}
    }
}
