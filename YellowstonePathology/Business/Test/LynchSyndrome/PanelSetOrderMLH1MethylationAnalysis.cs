using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Test.LynchSyndrome
{
	[PersistentClass("tblPanelSetOrderMLH1MethylationAnalysis", "tblPanelSetOrder", "YPIDATA")]
    public class PanelSetOrderMLH1MethylationAnalysis : PanelSetOrder
    {
        private string m_Result;
		private string m_Interpretation;
		private string m_Method;
		private string m_MLH1MethylationPercent;

        public PanelSetOrderMLH1MethylationAnalysis()
        {

        }

		public PanelSetOrderMLH1MethylationAnalysis(string masterAccessionNo, string reportNo, string objectId,
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
		[PersistentDataColumnProperty(true, "5000", "null", "varchar")]
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
		[PersistentDataColumnProperty(true, "20", "null", "varchar")]
		public string MLH1MethylationPercent
		{
			get { return this.m_MLH1MethylationPercent; }
			set
			{
				if (this.m_MLH1MethylationPercent != value)
				{
					this.m_MLH1MethylationPercent = value;
					this.NotifyPropertyChanged("MLH1MethylationPercent");
				}
			}
		}

		public void SetSummaryResult(LSEResult lSEResult)
		{
            if (string.IsNullOrEmpty(m_Result) == false)
            {
                MLH1MethylationAnalysisDetectedResult detected = new MLH1MethylationAnalysisDetectedResult();
                MLH1MethylationAnalysisNotDetectedResult notDetected = new MLH1MethylationAnalysisNotDetectedResult();

                if (this.ResultCode == detected.ResultCode)
                {
                    lSEResult.MethResult = LSEResultEnum.Positive;
                }
                else if (this.ResultCode == notDetected.ResultCode)
                {
                    lSEResult.MethResult = LSEResultEnum.Negative;
                }
            }
            else
            {
                lSEResult.MethResult = LSEResultEnum.Pending;
            }
		}

		public override string ToResultString(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
		{
			StringBuilder result = new StringBuilder();

			result.AppendLine("Result: " + this.m_Result);
			result.AppendLine();

			result.AppendLine("MLH1 Methylation Percent: " + this.m_MLH1MethylationPercent);
			result.AppendLine();

			result.AppendLine("Interpretation: " + this.m_Interpretation);
			result.AppendLine();

			return result.ToString();
		}
	}
}
