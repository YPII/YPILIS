using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Test.BCRABLByPCR
{
	[PersistentClass("tblBCRABLByPCRTestOrder", "tblPanelSetOrder", "YPIDATA")]
	public class BCRABLByPCRTestOrder : YellowstonePathology.Business.Test.PanelSetOrder
	{
		private string m_Result;
		private string m_DetectedLogReduction;
		private string m_FusionTranscriptType;
        private string m_PercentBCRABL;
		private string m_Interpretation;
		private string m_Method;

        public BCRABLByPCRTestOrder()
        {
        }

		public BCRABLByPCRTestOrder(string masterAccessionNo, string reportNo, string objectId,
            YellowstonePathology.Business.PanelSet.Model.PanelSet panelSet,
            YellowstonePathology.Business.Interface.IOrderTarget orderTarget,
			bool distribute)
			: base(masterAccessionNo, reportNo, objectId, panelSet, orderTarget, distribute)
		{
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "500", "null", "varchar")]
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
		[PersistentDataColumnProperty(true, "500", "null", "varchar")]
		public string DetectedLogReduction
		{
			get { return this.m_DetectedLogReduction; }
			set
			{
				if (this.m_DetectedLogReduction != value)
				{
					this.m_DetectedLogReduction = value;
					this.NotifyPropertyChanged("DetectedLogReduction");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "500", "null", "varchar")]
		public string FusionTranscriptType
		{
			get { return this.m_FusionTranscriptType; }
			set
			{
				if (this.m_FusionTranscriptType != value)
				{
					this.m_FusionTranscriptType = value;
					this.NotifyPropertyChanged("FusionTranscriptType");
				}
			}
		}

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "50", "null", "varchar")]
        public string PercentBCRABL
        {
            get { return this.m_PercentBCRABL; }
            set
            {
                if (this.m_PercentBCRABL != value)
                {
                    this.m_PercentBCRABL = value;
                    this.NotifyPropertyChanged("PercentBCRABL");
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

		public override string ToResultString(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {
            StringBuilder result = new StringBuilder();

            result.Append("Result: " + this.m_Result);
			if(string.IsNullOrEmpty(this.m_DetectedLogReduction) == false)
			{
				result.Append(" " + this.m_DetectedLogReduction);
			}
			result.AppendLine();

			result.AppendLine("Fusion Transcript Type: " + this.m_FusionTranscriptType);
            result.AppendLine();
            result.AppendLine();

            result.AppendLine("Interpretation: " + this.m_Interpretation);
            result.AppendLine();

            return result.ToString();
        }
	}
}
