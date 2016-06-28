using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Test.ChromosomeAnalysis
{
	[PersistentClass("tblChromosomeAnalysisTestOrder", "tblPanelSetOrder", "YPIDATA")]
	public class ChromosomeAnalysisTestOrder : YellowstonePathology.Business.Test.PanelSetOrder
	{
		private string m_Result;
		private string m_Karyotype;
		private string m_Interpretation;
		private string m_Comment;
		private string m_MetaphasesCounted;
		private string m_MetaphasesAnalyzed;
		private string m_MetaphasesKaryotyped;
		private string m_CultureType;
		private string m_BandingTechnique;
		private string m_BandingResolution;
		
		public ChromosomeAnalysisTestOrder()
        {
        }

		public ChromosomeAnalysisTestOrder(string masterAccessionNo, string reportNo, string objectId,
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
		public string Karyotype
		{
			get { return this.m_Karyotype; }
			set
			{
				if (this.m_Karyotype != value)
				{
					this.m_Karyotype = value;
					this.NotifyPropertyChanged("Karyotype");
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
		public string MetaphasesCounted
		{
			get { return this.m_MetaphasesCounted; }
			set
			{
				if (this.m_MetaphasesCounted != value)
				{
					this.m_MetaphasesCounted = value;
					this.NotifyPropertyChanged("MetaphasesCounted");
				}
			}
		}

		[PersistentProperty()]
		public string MetaphasesAnalyzed
		{
			get { return this.m_MetaphasesAnalyzed; }
			set
			{
				if (this.m_MetaphasesAnalyzed != value)
				{
					this.m_MetaphasesAnalyzed = value;
					this.NotifyPropertyChanged("MetaphasesAnalyzed");
				}
			}
		}

		[PersistentProperty()]
		public string MetaphasesKaryotyped
		{
			get { return this.m_MetaphasesKaryotyped; }
			set
			{
				if (this.m_MetaphasesKaryotyped != value)
				{
					this.m_MetaphasesKaryotyped = value;
					this.NotifyPropertyChanged("MetaphasesKaryotyped");
				}
			}
		}

		[PersistentProperty()]
		public string CultureType
		{
			get { return this.m_CultureType; }
			set
			{
				if (this.m_CultureType != value)
				{
					this.m_CultureType = value;
					this.NotifyPropertyChanged("CultureType");
				}
			}
		}

		[PersistentProperty()]
		public string BandingTechnique
		{
			get { return this.m_BandingTechnique; }
			set
			{
				if (this.m_BandingTechnique != value)
				{
					this.m_BandingTechnique = value;
					this.NotifyPropertyChanged("BandingTechnique");
				}
			}
		}

		[PersistentProperty()]
		public string BandingResolution
		{
			get { return this.m_BandingResolution; }
			set
			{
				if (this.m_BandingResolution != value)
				{
					this.m_BandingResolution = value;
					this.NotifyPropertyChanged("BandingResolution");
				}
			}
		}

		public override string ToResultString(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {
            StringBuilder result = new StringBuilder();

            result.AppendLine("Result: " + this.m_Result);
            result.AppendLine();

            result.AppendLine("Karyotype: " + this.m_Karyotype);
            result.AppendLine();

            result.AppendLine("Interpretation: " + this.m_Interpretation);
            result.AppendLine();

            result.AppendLine("Comment: " + this.m_Comment);
            result.AppendLine();            
            
            return result.ToString();
        }
	}
}
