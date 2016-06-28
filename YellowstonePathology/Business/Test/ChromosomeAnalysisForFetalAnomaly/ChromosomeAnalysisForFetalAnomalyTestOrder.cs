using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Test.ChromosomeAnalysisForFetalAnomaly
{
	[PersistentClass("tblChromosomeAnalysisForFetalAnomalyTestOrder", "tblPanelSetOrder", "YPIDATA")]
	public class ChromosomeAnalysisForFetalAnomalyTestOrder : YellowstonePathology.Business.Test.PanelSetOrder
	{
		private string m_Result;
		private string m_Karyotype;
		private string m_Interpretation;
		private string m_Scored;
		private string m_NumberOfColonies;
		private string m_NumberOfCultures;
		private string m_Karyotyped;
		private string m_Analyzed;
		private string m_Counted;
		private string m_GTGBandLevel;
        private string m_TestDetails;
		
		public ChromosomeAnalysisForFetalAnomalyTestOrder()
        {

        }

		public ChromosomeAnalysisForFetalAnomalyTestOrder(string masterAccessionNo, string reportNo, string objectId,
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
		public string Scored
		{
			get { return this.m_Scored; }
			set
			{
				if (this.m_Scored != value)
				{
					this.m_Scored = value;
					this.NotifyPropertyChanged("Scored");
				}
			}
		}

		[PersistentProperty()]
		public string NumberOfColonies
		{
			get { return this.m_NumberOfColonies; }
			set
			{
				if (this.m_NumberOfColonies != value)
				{
					this.m_NumberOfColonies = value;
					this.NotifyPropertyChanged("NumberOfColonies");
				}
			}
		}

		[PersistentProperty()]
		public string NumberOfCultures
		{
			get { return this.m_NumberOfCultures; }
			set
			{
				if (this.m_NumberOfCultures != value)
				{
					this.m_NumberOfCultures = value;
					this.NotifyPropertyChanged("NumberOfCultures");
				}
			}
		}

		[PersistentProperty()]
		public string Karyotyped
		{
			get { return this.m_Karyotyped; }
			set
			{
				if (this.m_Karyotyped != value)
				{
					this.m_Karyotyped = value;
					this.NotifyPropertyChanged("MetaphasesKaryotyped");
				}
			}
		}

		[PersistentProperty()]
		public string Analyzed
		{
			get { return this.m_Analyzed; }
			set
			{
				if (this.m_Analyzed != value)
				{
					this.m_Analyzed = value;
					this.NotifyPropertyChanged("Analyzed");
				}
			}
		}

		[PersistentProperty()]
		public string Counted
		{
			get { return this.m_Counted; }
			set
			{
				if (this.m_Counted != value)
				{
					this.m_Counted = value;
					this.NotifyPropertyChanged("Counted");
				}
			}
		}

		[PersistentProperty()]
		public string GTGBandLevel
		{
			get { return this.m_GTGBandLevel; }
			set
			{
				if (this.m_GTGBandLevel != value)
				{
					this.m_GTGBandLevel = value;
					this.NotifyPropertyChanged("GTGBandLevel");
				}
			}
		}

        [PersistentProperty()]
        public string TestDetails
        {
            get { return this.m_TestDetails; }
            set
            {
                if (this.m_TestDetails != value)
                {
                    this.m_TestDetails = value;
                    this.NotifyPropertyChanged("TestDetails");
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

			result.AppendLine("Scored: " + this.m_Scored);
			result.AppendLine();

			result.AppendLine("Number Of Colonies: " + this.m_NumberOfColonies);
            result.AppendLine();

			result.AppendLine("Number Of Cultures: " + this.m_NumberOfCultures);
			result.AppendLine();            

			result.AppendLine("Karyotyped: " + this.m_Karyotyped);
			result.AppendLine();

			result.AppendLine("Analyzed: " + this.m_Analyzed);
			result.AppendLine();

			result.AppendLine("Counted: " + this.m_Counted);
			result.AppendLine();

			result.AppendLine("GTG Band Level: " + this.m_GTGBandLevel);
			result.AppendLine();
            
            return result.ToString();
        }

		public override YellowstonePathology.Business.Rules.MethodResult IsOkToAccept()
		{
			YellowstonePathology.Business.Rules.MethodResult result = base.IsOkToAccept();
			if (result.Success == true)
			{
				if (string.IsNullOrEmpty(this.m_ResultCode) == true)
				{
					result.Success = false;
					result.Message = "The result must be set before the results can be accepted.";
				}
			}
			return result;
		}
	}
}
