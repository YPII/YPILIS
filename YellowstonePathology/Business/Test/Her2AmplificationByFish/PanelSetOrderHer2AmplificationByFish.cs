using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Test.Her2AmplificationByFish
{
	[PersistentClass("tblPanelSetOrderHer2AmplificationByFish", "tblPanelSetOrder", "YPIDATA")]
	public class PanelSetOrderHer2AmplificationByFish : PanelSetOrder
	{
		private string m_Result;
		private string m_Indicator;
		private string m_ReferenceRanges;
		private string m_AverageHER2SignalsPerNucleus;
		private string m_AverageCEN17SignalsPerNucleus;
		private string m_HER2CEN17SignalRatio;
		private string m_Interpretation;
		private string m_Comment;
		private string m_NucleiScored;
		private string m_Reference;
        private bool m_NonBreast;

		public PanelSetOrderHer2AmplificationByFish()
        {

        }

		public PanelSetOrderHer2AmplificationByFish(string masterAccessionNo, string reportNo, string objectId,
             YellowstonePathology.Business.PanelSet.Model.PanelSet panelSet,
            YellowstonePathology.Business.Interface.IOrderTarget orderTarget,
			bool distribute)
			: base(masterAccessionNo, reportNo, objectId, panelSet, orderTarget, distribute)
		{
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "100", "null", "varchar")]
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
		[PersistentDataColumnProperty(true, "50", "null", "varchar")]
		public string Indicator
		{
			get { return this.m_Indicator; }
			set
			{
				if (this.m_Indicator != value)
				{
					this.m_Indicator = value;
					this.NotifyPropertyChanged("Indicator");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "500", "null", "varchar")]
		public string ReferenceRanges
		{
			get { return this.m_ReferenceRanges; }
			set
			{
				if (this.m_ReferenceRanges != value)
				{
					this.m_ReferenceRanges = value;
					this.NotifyPropertyChanged("ReferenceRanges");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "50", "null", "varchar")]
		public string AverageHER2SignalsPerNucleus
		{
			get { return this.m_AverageHER2SignalsPerNucleus; }
			set
			{
				if (this.m_AverageHER2SignalsPerNucleus != value)
				{
					this.m_AverageHER2SignalsPerNucleus = value;
					this.NotifyPropertyChanged("AverageHER2SignalsPerNucleus");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "50", "null", "varchar")]
		public string AverageCEN17SignalsPerNucleus
		{
			get { return this.m_AverageCEN17SignalsPerNucleus; }
			set
			{
				if (this.m_AverageCEN17SignalsPerNucleus != value)
				{
					this.m_AverageCEN17SignalsPerNucleus = value;
					this.NotifyPropertyChanged("AverageCEN17SignalsPerNucleus");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "50", "null", "varchar")]
		public string HER2CEN17SignalRatio
		{
			get { return this.m_HER2CEN17SignalRatio; }
			set
			{
				if (this.m_HER2CEN17SignalRatio != value)
				{
					this.m_HER2CEN17SignalRatio = value;
					this.NotifyPropertyChanged("HER2CEN17SignalRatio");
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
		[PersistentDataColumnProperty(true, "1000", "null", "varchar")]
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
		public string Reference
		{
			get { return this.m_Reference; }
			set
			{
				if (this.m_Reference != value)
				{
					this.m_Reference = value;
					this.NotifyPropertyChanged("Reference");
				}
			}
		}

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "1", "0", "bit")]
        public bool NonBreast
        {
            get { return this.m_NonBreast; }
            set
            {
                if (this.m_NonBreast != value)
                {
                    this.m_NonBreast = value;
                    this.NotifyPropertyChanged("NonBreast");
                }
            }
        }

		public override string ToResultString(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
		{
			StringBuilder result = new StringBuilder();

			result.AppendLine("Result: " + this.m_Result);
			result.AppendLine();

			result.AppendLine("Average HER2 Signals/Nucleus: " + this.m_AverageHER2SignalsPerNucleus);
			result.AppendLine();

			result.AppendLine("Average CEN17 Signals/Nucleus: " + this.m_AverageCEN17SignalsPerNucleus);
			result.AppendLine();

			result.AppendLine("HER2/CEN17 Signal Ratio: " + this.HER2CEN17SignalRatio);
			result.AppendLine();

			result.AppendLine("Interpretation: " + this.m_Interpretation);
			result.AppendLine();

			result.AppendLine("Comment: " + this.m_Comment);
			result.AppendLine();

			result.AppendLine("Nuclei Scored: " + this.m_NucleiScored);
			result.AppendLine();

			return result.ToString();
		}
	}
}
