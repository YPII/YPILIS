using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Test.BRAFV600EK
{
	[PersistentClass("tblBRAFV600EKTestOrder", "tblPanelSetOrder", "YPIDATA")]
    public class BRAFV600EKTestOrder : YellowstonePathology.Business.Test.PanelSetOrder, YellowstonePathology.Business.Interface.ISolidTumorTesting
	{
		private string m_Result;
		private string m_Interpretation;
		private string m_Comment;
		private string m_Indication;
        private string m_IndicationComment;
		private string m_TumorNucleiPercentage;
		private string m_Method;
		private string m_ReportDisclaimer;

        public BRAFV600EKTestOrder()
        {     
       
        }

		public BRAFV600EKTestOrder(string masterAccessionNo, string reportNo, string objectId, 
            YellowstonePathology.Business.PanelSet.Model.PanelSet panelSet,
            YellowstonePathology.Business.Interface.IOrderTarget orderTarget,
			bool distribute)
			: base(masterAccessionNo, reportNo, objectId, panelSet, orderTarget, distribute)
		{
            this.m_ReportDisclaimer = "This test was developed and its performance characteristics determined by Yellowstone Pathology Institute, Inc.  It has not been cleared or " +
                "approved by the U.S. Food and Drug Administration. The FDA has determined that such clearance or approval is not necessary.  This test is used for clinical purposes.  It should not" +
                "be regarded as investigational or for research.  This laboratory is certified under the Clinical Laboratory Improvement Amendments of 1988 (CLIA-88) as qualified to perform high " +
                "complexity clinical laboratory testing.";
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

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "50", "null", "varchar")]
		public string Indication
		{
			get { return this.m_Indication; }
			set
			{
				if (this.m_Indication != value)
				{
					this.m_Indication = value;
					this.NotifyPropertyChanged("Indication");
				}
			}
		}

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "500", "null", "varchar")]
        public string IndicationComment
        {
            get { return this.m_IndicationComment; }
            set
            {
                if (this.m_IndicationComment != value)
                {
                    this.m_IndicationComment = value;
                    this.NotifyPropertyChanged("IndicationComment");
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
		[PersistentDataColumnProperty(true, "1000", "null", "varchar")]
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

		public void SetSummaryResult(YellowstonePathology.Business.Test.LynchSyndrome.LSEResult lSEResult)
		{
			if (string.IsNullOrEmpty(this.Result) == false)
			{
                YellowstonePathology.Business.Test.BRAFV600EK.BRAFV600EKNotDetectedResult notDetectedResult = new BRAFV600EKNotDetectedResult();
                YellowstonePathology.Business.Test.BRAFV600EK.BRAFV600EKDetectedResult detectedResult = new BRAFV600EKDetectedResult();

                if (this.ResultCode == notDetectedResult.ResultCode)
                {
                    lSEResult.BrafResult = YellowstonePathology.Business.Test.LynchSyndrome.LSEResultEnum.Negative;
                }
                else if (this.ResultCode == detectedResult.ResultCode)
                {
                    lSEResult.BrafResult = YellowstonePathology.Business.Test.LynchSyndrome.LSEResultEnum.Positive;
                }
			}
		}

		public override string ToResultString(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
		{
			StringBuilder result = new StringBuilder();
			result.Append("Result: ");
			result.AppendLine(this.m_Result);
			result.AppendLine();
			result.Append("Result Comment: ");
			result.AppendLine(this.m_Comment);
			result.AppendLine();
			result.Append("Interpretation: ");
			result.AppendLine(this.m_Interpretation);
			result.AppendLine();
			result.Append("Indication: ");
			result.AppendLine(this.m_Indication);
			result.AppendLine();
			result.Append("Tumor Nuclei Percent: ");
			result.AppendLine(this.m_TumorNucleiPercentage);
			return result.ToString();
		}
	}
}
