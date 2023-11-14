using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Test.LynchSyndrome
{
	[PersistentClass("tblPanelSetOrderLynchSyndromeIHC", "tblPanelSetOrder", "YPIDATA")]
    public class PanelSetOrderLynchSyndromeIHC : PanelSetOrder
    {        
        private string m_MLH1Result;
        private string m_MSH2Result;
        private string m_MSH6Result;
        private string m_PMS2Result;
        private string m_Comment;      

        public PanelSetOrderLynchSyndromeIHC()
        {

        }

		public PanelSetOrderLynchSyndromeIHC(string masterAccessionNo, string reportNo, string objectId,
			YellowstonePathology.Business.PanelSet.Model.PanelSet panelSet,
            YellowstonePathology.Business.Interface.IOrderTarget orderTarget,
			bool distribute)
			: base(masterAccessionNo, reportNo, objectId, panelSet, orderTarget, distribute)
		{
            
		}        

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "50", "null", "varchar")]
        public string MLH1Result
        {
            get { return this.m_MLH1Result; }
            set
            {
                if (this.m_MLH1Result != value)
                {
                    this.m_MLH1Result = value;
                    this.NotifyPropertyChanged("MLH1Result");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "50", "null", "varchar")]
        public string MSH2Result
        {
            get { return this.m_MSH2Result; }
            set
            {
                if (this.m_MSH2Result != value)
                {
                    this.m_MSH2Result = value;
                    this.NotifyPropertyChanged("MSH2Result");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "50", "null", "varchar")]
        public string MSH6Result
        {
            get { return this.m_MSH6Result; }
            set
            {
                if (this.m_MSH6Result != value)
                {
                    this.m_MSH6Result = value;
                    this.NotifyPropertyChanged("MSH6Result");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "50", "null", "varchar")]
        public string PMS2Result
        {
            get { return this.m_PMS2Result; }
            set
            {
                if (this.m_PMS2Result != value)
                {
                    this.m_PMS2Result = value;
                    this.NotifyPropertyChanged("PMS2Result");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "5000", "null", "varchar")]
        public string Comment
        {
            get { return this.m_Comment; }
            set
            {
                if (this.m_Comment != value)
                {
                    this.m_Comment = value;
                    this.NotifyPropertyChanged("Comment");
                }
            }
        }

        public IHCResult GetSummaryResult()
        {
            IHCResult result = IHCResult.CreateResultFromResultCode(this.m_ResultCode);
            return result;
        }

		public override string ToResultString(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
		{
			StringBuilder result = new StringBuilder();
			result.AppendLine("MLH1 Result: " + this.m_MLH1Result);
			result.AppendLine("MSH2 Result: " + this.m_MSH2Result);
			result.AppendLine("MSH6 Result: " + this.m_MSH6Result);
			result.AppendLine("PMS2 Result: " + this.m_PMS2Result);
			return result.ToString();
		}
	}
}
