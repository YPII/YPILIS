/*
 * Created by SharpDevelop.
 * User: william.copland
 * Date: 1/5/2016
 * Time: 10:42 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Text;
using YellowstonePathology.Business.Persistence;
using YellowstonePathology.Business.Helper;

namespace YellowstonePathology.Business.Test.TCellSubsetAnalysis
{
	/// <summary>
	/// Description of TCellSubsetAnalysisTestOrder.
	/// </summary>
	[PersistentClass("tblTCellSubsetAnalysisTestOrder", "tblPanelSetOrder", "YPIDATA")]
	public class TCellSubsetAnalysisTestOrder : PanelSetOrder
	{
		private string m_Method;
		private string m_References;
		private string m_ASRComment;
		private double? m_CD3Percent;
		private double? m_CD4Percent;
		private double? m_CD8Percent;
		private double? m_CD4CD8Ratio;
        private string m_ReferenceRange;
        private string m_Disclosure;
        private string m_Interpretation;
		
		public TCellSubsetAnalysisTestOrder()
		{
		}

		public TCellSubsetAnalysisTestOrder(string masterAccessionNo, string reportNo, string objectId,
			YellowstonePathology.Business.PanelSet.Model.PanelSet panelSet,
			YellowstonePathology.Business.Interface.IOrderTarget orderTarget,
			bool distribute)
			: base(masterAccessionNo, reportNo, objectId, panelSet, orderTarget, distribute)
		{
			this.m_ASRComment = "Tests utilizing Analytic Specific Reagents (ASR's) were developed and performance characteristics determined by " +
				"Yellowstone Pathology Institute, Inc.  They have not been cleared or approved by the U.S. Food and Drug Administration.  The FDA has " +
				"determined that such clearance or approval is not necessary.  ASR's may be used for clinical purposes and should not be regarded as " +
				"investigational or for research.  This laboratory is certified under the Clinical Laboratory Improvement Amendments of 1988 (CLIA-88) " +
				"as qualified to perform high complexity clinical laboratory testing.";
			this.m_Method = "Quantitative Flow Cytometry.";
            this.m_References = "1. Meyer, K. C., Raghu, G., Baughman, R. P., Brown, K. K., Costabel, U., du Bois, R. M., Drent, M., Haslam, P. L., Soon Kim, D., Nagai, S., Rottoli, P., Saltini, C., Selman, M., Strange, C., Wood, B. An Official American Thoracic Society Clinical Practice Guideline: The Clinical Utility of Bronchoalveolar Lavage Cellular Analysis in Interstitial Lung Disease. American Journal of Respiratory Critical Care. May 2012. 185:9 (1004-1014)." + Environment.NewLine +
                "2. Drent, M., Mansour, K., Linssen, C.Bronchoalveolar Lavage in Sarcoidosis.Seminars in Respiratory and Critical Care Medicine. 2007. 28:5. (486 - 495).";
            this.m_ReferenceRange = "Normal non-smoker 0.9-2.5";
            this.m_Interpretation = "A CD4/CD8 ratio greater or equal to 3.5 is supportive of a diagnosis of Sarcoidosis.";
            this.m_Disclosure = "Please interpret results of BAL-T-cell lymphocyte subset analysis with caution.  If the specimen is contaminated " +
                "with peripheral blood, the analysis includes both peripheral blood lymphocytes and BAL lymphocytes.";
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

		[PersistentProperty()]
		public string ASRComment
		{
			get { return this.m_ASRComment; }
			set
			{
				if (this.m_ASRComment != value)
				{
					this.m_ASRComment = value;
					this.NotifyPropertyChanged("ASRComment");
				}
			}
		}

		[PersistentProperty()]
		public double? CD3Percent
		{
			get { return this.m_CD3Percent; }
			set
			{
				if (this.m_CD3Percent != value)
				{
					this.m_CD3Percent = value;
					this.NotifyPropertyChanged("CD3Percent");
				}
			}
		}

		[PersistentProperty()]
		public double? CD4Percent
		{
			get { return this.m_CD4Percent; }
			set
			{
				if (this.m_CD4Percent != value)
				{
					this.m_CD4Percent = value;
					this.NotifyPropertyChanged("CD4Percent");
					this.SetCD4CD8Ratio();
				}
			}
		}

		[PersistentProperty()]
		public double? CD8Percent
		{
			get { return this.m_CD8Percent; }
			set
			{
				if (this.m_CD8Percent != value)
				{
					this.m_CD8Percent = value;
					this.NotifyPropertyChanged("CD8Percent");
					this.SetCD4CD8Ratio();
				}
			}
		}

		[PersistentProperty()]
		public double? CD4CD8Ratio
		{
			get { return this.m_CD4CD8Ratio; }
			set
			{
				if (this.m_CD4CD8Ratio != value)
				{
					this.m_CD4CD8Ratio = value;
					this.NotifyPropertyChanged("CD4CD8Ratio");
				}
			}
		}

        [PersistentProperty()]
        public string ReferenceRange
        {
            get { return this.m_ReferenceRange; }
            set
            {
                if (this.m_ReferenceRange != value)
                {
                    this.m_ReferenceRange = value;
                    this.NotifyPropertyChanged("ReferenceRange");
                }
            }
        }

        [PersistentProperty()]
        public string Disclosure
        {
            get { return this.m_Disclosure; }
            set
            {
                if (this.m_Disclosure != value)
                {
                    this.m_Disclosure = value;
                    this.NotifyPropertyChanged("Disclosure");
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

        private void SetCD4CD8Ratio()
		{
			double? result = null;
			if(this.m_CD4Percent.HasValue && this.m_CD8Percent.HasValue)
			{
				result = Math.Round(this.m_CD4Percent.Value / this.m_CD8Percent.Value, 2);
			}
			this.CD4CD8Ratio = result;
		}

		public override string ToResultString(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
		{
			StringBuilder result = new StringBuilder();

			result.AppendLine("CD3 Percent: " + this.m_CD3Percent.ToString().StringAsPercent());
			result.AppendLine("CD4 Percent: " + this.m_CD4Percent.ToString().StringAsPercent());
			result.AppendLine("CD8 Percent: " + this.m_CD8Percent.ToString().StringAsPercent());
			string value =  string.Empty;
			if(this.m_CD4CD8Ratio.HasValue) value = Math.Round(this.m_CD4CD8Ratio.Value, 2).ToString();
			result.AppendLine("CD4/CD8 Ratio: " + value);
            result.AppendLine("Reference Range: " + this.m_ReferenceRange);
            result.AppendLine();

			return result.ToString();
		}
	}
}
