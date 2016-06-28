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

		public void SetSummaryResult(LSEResult lSEResult)
		{
			IHCResult result = IHCResult.CreateResultFromResultCode(this.m_ResultCode);
			if (result != null)
			{
				lSEResult.MLH1Result = result.MLH1Result.LSEResult;
				lSEResult.MSH2Result = result.MSH2Result.LSEResult;
				lSEResult.MSH6Result = result.MSH6Result.LSEResult;
				lSEResult.PMS2Result = result.PMS2Result.LSEResult;
			}
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
