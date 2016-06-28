using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Test.Retired
{
	[PersistentClass("tblPanelSetOrderHer2ByFishRetired", "tblPanelSetOrder", "YPIDATA")]
	public class PanelSetOrderHer2AmplificationByFishRetired2 : PanelSetOrder
	{
		private string m_Result;
		private int m_CellsCounted;
		private int m_TotalChr17SignalsCounted;
		private int m_TotalHer2SignalsCounted;
		private int m_Her2byIHCOrder;
		private int m_NumberOfObservers;
		private bool m_IncludeImmunoRecommendedComment;
		private bool m_IncludeResultComment;
		private bool m_IncludePolysomyComment;
		private string m_PolysomyPercent;
		private string m_Chr17SignalRangeLow;
		private string m_Chr17SignalRangeHigh;
		private string m_Her2SignalRangeLow;
		private string m_Her2SignalRangeHigh;
		private string m_CommentLabel;
		private string m_SampleAdequacy;
		private string m_ProbeSignalIntensity;
		private string m_TechComment;
		private string m_ResultComment;
		private string m_InterpretiveComment;
		private string m_ResultDescription;
		private string m_SourceBlock;
		private string m_GeneticHeterogeneity;
		private string m_Her2Chr17ClusterRatio;
		private string m_ReportReference;
		private string m_Indicator;
		private string m_Method;

		public PanelSetOrderHer2AmplificationByFishRetired2()
		{

		}

		public PanelSetOrderHer2AmplificationByFishRetired2(string masterAccessionNo, string reportNo, string objectId,
			YellowstonePathology.Business.PanelSet.Model.PanelSet panelSet,
            YellowstonePathology.Business.Test.AliquotOrder block,
			bool distribute)
			: base(masterAccessionNo, reportNo, objectId, panelSet, distribute)
		{
		}

		public Nullable<double> Her2Chr17Ratio
		{
			get
			{
				Nullable<double> ratio = null;
				if (TotalHer2SignalsCounted > 0 && TotalChr17SignalsCounted > 0)
				{
					double dratio = (double)TotalHer2SignalsCounted / (double)TotalChr17SignalsCounted;
					ratio = Convert.ToDouble(Math.Round((dratio), 2));
				}
				return ratio;
			}
			set { }
		}

		public Nullable<double> AverageHer2NeuSignal
		{
			get
			{
				Nullable<double> result = null;
				if (TotalHer2SignalsCounted > 0 && CellsCounted > 0)
				{
					double dratio = (double)TotalHer2SignalsCounted / (double)CellsCounted;
					result = Convert.ToDouble(Math.Round((dratio), 2));
				}
				return result;
			}
			set { }
		}

		public string AverageChr17Signal
		{
			get
			{
				string ratio = "Unable to calculate";
				if (TotalChr17SignalsCounted > 0 && CellsCounted > 0)
				{
					double dratio = (double)TotalChr17SignalsCounted / (double)CellsCounted;
					ratio = Convert.ToString(Math.Round((dratio), 2));
				}
				return ratio;
			}
			set { }
		}

		public string AverageHer2Chr17Signal
		{
			get
			{
				string ratio = "Unable to calculate";
				Nullable<double> dratio = this.AverageHer2Chr17SignalAsDouble;
				if (dratio.HasValue)
				{
					ratio = Convert.ToString(Math.Round((dratio.Value), 2));
				}
				return ratio;
			}
			set { }
		}

		private Nullable<double> AverageHer2Chr17SignalAsDouble
		{
			get
			{
				Nullable<double> dratio = null;
				if (TotalChr17SignalsCounted > 0 && TotalHer2SignalsCounted > 0 && CellsCounted > 0)
				{
					dratio = ((double)TotalHer2SignalsCounted / (double)CellsCounted) / ((double)TotalChr17SignalsCounted / (double)CellsCounted);
				}
				return dratio;
			}
		}

		public int TotalChr17SignalsCountedUI
		{
			get { return this.m_TotalChr17SignalsCounted; }
			set
			{
				if (this.m_TotalChr17SignalsCounted != value)
				{
					this.TotalChr17SignalsCounted = value;
					NotifyPropertyChanged("Her2Chr17Ratio");
					NotifyPropertyChanged("TotalChr17SignalsCountedUI");
				}
			}
		}

		public int TotalHer2SignalsCountedUI
		{
			get { return this.m_TotalHer2SignalsCounted; }
			set
			{
				if (this.m_TotalHer2SignalsCounted != value)
				{
					this.TotalHer2SignalsCounted = value;
					NotifyPropertyChanged("Her2Chr17Ratio");
					NotifyPropertyChanged("TotalHer2SignalsCountedUI");
				}
			}
		}

		#region Persistent Properties
		[PersistentProperty()]
		public bool IncludeImmunoRecommendedComment
		{
			get { return this.m_IncludeImmunoRecommendedComment; }
			set
			{
				if (this.m_IncludeImmunoRecommendedComment != value)
				{
					this.m_IncludeImmunoRecommendedComment = value;
					this.NotifyPropertyChanged("IncludeImmunoRecommendedComment");
				}
			}
		}

		[PersistentProperty()]
		public bool IncludeResultComment
		{
			get { return this.m_IncludeResultComment; }
			set
			{
				if (this.m_IncludeResultComment != value)
				{
					this.m_IncludeResultComment = value;
					this.NotifyPropertyChanged("IncludeResultComment");
				}
			}
		}

		[PersistentProperty()]
		public bool IncludePolysomyComment
		{
			get { return this.m_IncludePolysomyComment; }
			set
			{
				if (this.m_IncludePolysomyComment != value)
				{
					this.m_IncludePolysomyComment = value;
					this.NotifyPropertyChanged("IncludePolysomyComment");
				}
			}
		}

		[PersistentProperty()]
		public int CellsCounted
		{
			get { return this.m_CellsCounted; }
			set
			{
				if (this.m_CellsCounted != value)
				{
					this.m_CellsCounted = value;
					this.NotifyPropertyChanged("CellsCounted");
				}
			}
		}

		[PersistentProperty()]
		public int TotalChr17SignalsCounted
		{
			get { return this.m_TotalChr17SignalsCounted; }
			set
			{
				if (this.m_TotalChr17SignalsCounted != value)
				{
					this.m_TotalChr17SignalsCounted = value;
					this.NotifyPropertyChanged("TotalChr17SignalsCounted");
				}
			}
		}

		[PersistentProperty()]
		public int TotalHer2SignalsCounted
		{
			get { return this.m_TotalHer2SignalsCounted; }
			set
			{
				if (this.m_TotalHer2SignalsCounted != value)
				{
					this.m_TotalHer2SignalsCounted = value;
					this.NotifyPropertyChanged("TotalHer2SignalsCounted");
				}
			}
		}

		[PersistentProperty()]
		public int Her2byIHCOrder
		{
			get { return this.m_Her2byIHCOrder; }
			set
			{
				if (this.m_Her2byIHCOrder != value)
				{
					this.m_Her2byIHCOrder = value;
					this.NotifyPropertyChanged("Her2byIHCOrder");
				}
			}
		}

		[PersistentProperty()]
		public int NumberOfObservers
		{
			get { return this.m_NumberOfObservers; }
			set
			{
				if (this.m_NumberOfObservers != value)
				{
					this.m_NumberOfObservers = value;
					this.NotifyPropertyChanged("NumberOfObservers");
				}
			}
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
		public string PolysomyPercent
		{
			get { return this.m_PolysomyPercent; }
			set
			{
				if (this.m_PolysomyPercent != value)
				{
					this.m_PolysomyPercent = value;
					this.NotifyPropertyChanged("PolysomyPercent");
				}
			}
		}

		[PersistentProperty()]
		public string Chr17SignalRangeLow
		{
			get { return this.m_Chr17SignalRangeLow; }
			set
			{
				if (this.m_Chr17SignalRangeLow != value)
				{
					this.m_Chr17SignalRangeLow = value;
					this.NotifyPropertyChanged("Chr17SignalRangeLow");
				}
			}
		}

		[PersistentProperty()]
		public string Chr17SignalRangeHigh
		{
			get { return this.m_Chr17SignalRangeHigh; }
			set
			{
				if (this.m_Chr17SignalRangeHigh != value)
				{
					this.m_Chr17SignalRangeHigh = value;
					this.NotifyPropertyChanged("Chr17SignalRangeHigh");
				}
			}
		}

		[PersistentProperty()]
		public string Her2SignalRangeLow
		{
			get { return this.m_Her2SignalRangeLow; }
			set
			{
				if (this.m_Her2SignalRangeLow != value)
				{
					this.m_Her2SignalRangeLow = value;
					this.NotifyPropertyChanged("Her2SignalRangeLow");
				}
			}
		}

		[PersistentProperty()]
		public string Her2SignalRangeHigh
		{
			get { return this.m_Her2SignalRangeHigh; }
			set
			{
				if (this.m_Her2SignalRangeHigh != value)
				{
					this.m_Her2SignalRangeHigh = value;
					this.NotifyPropertyChanged("Her2SignalRangeHigh");
				}
			}
		}

		[PersistentProperty()]
		public string CommentLabel
		{
			get { return this.m_CommentLabel; }
			set
			{
				if (this.m_CommentLabel != value)
				{
					this.m_CommentLabel = value;
					this.NotifyPropertyChanged("CommentLabel");
				}
			}
		}

		[PersistentProperty()]
		public string ResultDescription
		{
			get { return this.m_ResultDescription; }
			set
			{
				if (this.m_ResultDescription != value)
				{
					this.m_ResultDescription = value;
					this.NotifyPropertyChanged("ResultDescription");
				}
			}
		}

		[PersistentProperty()]
		public string SampleAdequacy
		{
			get { return this.m_SampleAdequacy; }
			set
			{
				if (this.m_SampleAdequacy != value)
				{
					this.m_SampleAdequacy = value;
					this.NotifyPropertyChanged("SampleAdequacy");
				}
			}
		}

		[PersistentProperty()]
		public string ProbeSignalIntensity
		{
			get { return this.m_ProbeSignalIntensity; }
			set
			{
				if (this.m_ProbeSignalIntensity != value)
				{
					this.m_ProbeSignalIntensity = value;
					this.NotifyPropertyChanged("ProbeSignalIntensity");
				}
			}
		}

		[PersistentProperty()]
		public string TechComment
		{
			get { return this.m_TechComment; }
			set
			{
				if (this.m_TechComment != value)
				{
					this.m_TechComment = value;
					this.NotifyPropertyChanged("TechComment");
				}
			}
		}

		[PersistentProperty()]
		public string ResultComment
		{
			get { return this.m_ResultComment; }
			set
			{
				if (this.m_ResultComment != value)
				{
					this.m_ResultComment = value;
					this.NotifyPropertyChanged("ResultComment");
				}
			}
		}

		[PersistentProperty()]
		public string InterpretiveComment
		{
			get { return this.m_InterpretiveComment; }
			set
			{
				if (this.m_InterpretiveComment != value)
				{
					this.m_InterpretiveComment = value;
					this.NotifyPropertyChanged("InterpretiveComment");
				}
			}
		}

		[PersistentProperty()]
		public string GeneticHeterogeneity
		{
			get { return this.m_GeneticHeterogeneity; }
			set
			{
				if (this.m_GeneticHeterogeneity != value)
				{
					this.m_GeneticHeterogeneity = value;
					this.NotifyPropertyChanged("GeneticHeterogeneity");
				}
			}
		}

		[PersistentProperty()]
		public string Her2Chr17ClusterRatio
		{
			get { return this.m_Her2Chr17ClusterRatio; }
			set
			{
				if (this.m_Her2Chr17ClusterRatio != value)
				{
					this.m_Her2Chr17ClusterRatio = value;
					this.NotifyPropertyChanged("Her2Chr17ClusterRatio");
				}
			}
		}

		[PersistentProperty()]
		public string ReportReference
		{
			get { return this.m_ReportReference; }
			set
			{
				if (this.m_ReportReference != value)
				{
					this.m_ReportReference = value;
					this.NotifyPropertyChanged("ReportReference");
				}
			}
		}

		[PersistentProperty()]
		public string SourceBlock
		{
			get { return this.m_SourceBlock; }
			set
			{
				if (this.m_SourceBlock != value)
				{
					this.m_SourceBlock = value;
					this.NotifyPropertyChanged("SourceBlock");
				}
			}
		}

		[PersistentProperty()]
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
		# endregion
	}
}
