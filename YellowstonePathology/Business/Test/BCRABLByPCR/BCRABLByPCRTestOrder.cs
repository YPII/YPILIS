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
		private string m_References;

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
		public string References
		{
			get { return this.m_References; }
			set
			{
				if (this.m_References != value)
				{
					this.m_References = value;
					this.NotifyPropertyChanged("References");
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
