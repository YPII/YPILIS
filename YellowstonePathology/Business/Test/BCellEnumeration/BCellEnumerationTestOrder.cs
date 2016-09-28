/*
 * Created by SharpDevelop.
 * User: william.copland
 * Date: 1/5/2016
 * Time: 9:47 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Text;
using YellowstonePathology.Business.Persistence;
using YellowstonePathology.Business.Helper;

namespace YellowstonePathology.Business.Test.BCellEnumeration
{
	/// <summary>
	/// Description of BCellEnumerationTestOrder.
	/// </summary>
	/// 
	[PersistentClass("tblBCellEnumerationTestOrder", "tblPanelSetOrder", "YPIDATA")]
	public class BCellEnumerationTestOrder : PanelSetOrder
	{		
		private string m_Method;
		private string m_ASRComment;
		private double? m_WBC;
		private double? m_LymphocytePercentage;		
		private double? m_CD19BCellPositivePercent;		
		private double? m_CD20BCellPositivePercent;
		private double? m_CD19AbsoluteCount;
		private double? m_CD20AbsoluteCount;
		
		public BCellEnumerationTestOrder()
		{
		}

		public BCellEnumerationTestOrder(string masterAccessionNo, string reportNo, string objectId,
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
            this.m_ReportReferences = "1. Fried AJ, Bonilla FA.  Pathogenesis, Diagnosis, and Management of Primary Antibody Deficiencies and Infections.  Clinical Microbiology Review.  2009 Jul; 22:  396 - 414. " + Environment.NewLine +
                "2. Kotylo PK, Fineberg NS, et al.Reference ranges for lymphocyte subsets in pediactric patients.American Journal of Clinical Pathology. 1993; 100: 111 - 115. " + Environment.NewLine +
                "3. Prescovitz  MD.Rituximab, and Anti-CD20 Monoclonal Antibody:  History and Mechanism of Action.American Journal of Transplantation. 2006; 6: 859 - 866. " + Environment.NewLine +
                "4. Stewart CC, Stewart SJ. Immunological monitoring utilizing novel probes.Annual of the New York Annal of New York Academy of Science.  1993; 95: 816 - 823.";
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "50", "null", "varchar")]
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
		[PersistentDataColumnProperty(true, "", "null", "float")]
		public double? WBC
		{
			get { return this.m_WBC; }
			set
			{
				if (this.m_WBC != value)
				{
					this.m_WBC = value;
					this.NotifyPropertyChanged("WBC");
					this.SetCD19AbsoluteCount();
					this.SetCD20AbsoluteCount();
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "", "null", "float")]
		public double? LymphocytePercentage
		{
			get { return this.m_LymphocytePercentage; }
			set
			{
				if (this.m_LymphocytePercentage != value)
				{
					this.m_LymphocytePercentage = value;
					this.NotifyPropertyChanged("LymphocytePercentage");
					this.SetCD19AbsoluteCount();
					this.SetCD20AbsoluteCount();
				}
			}
		}		

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "", "null", "float")]
		public double? CD19BCellPositivePercent
		{
			get { return this.m_CD19BCellPositivePercent; }
			set
			{
				if (this.m_CD19BCellPositivePercent != value)
				{
					this.m_CD19BCellPositivePercent = value;
					this.NotifyPropertyChanged("CD19BCellPositivePercent");
					this.SetCD19AbsoluteCount();
				}
			}
		}		

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "", "null", "float")]
		public double? CD20BCellPositivePercent
		{
			get { return this.m_CD20BCellPositivePercent; }
			set
			{
				if (this.m_CD20BCellPositivePercent != value)
				{
					this.m_CD20BCellPositivePercent = value;
					this.NotifyPropertyChanged("CD20BCellPositivePercent");
					this.SetCD20AbsoluteCount();
				}
			}
		}
		
		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "", "null", "float")]
		public double? CD19AbsoluteCount
		{
			get { return this.m_CD19AbsoluteCount; }
			set
			{
				if (this.m_CD19AbsoluteCount != value)
				{
					this.m_CD19AbsoluteCount = value;
					this.NotifyPropertyChanged("CD19AbsoluteCount");
				}
			}
		}
		
		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "", "null", "float")]
		public double? CD20AbsoluteCount
		{
			get { return this.m_CD20AbsoluteCount; }
			set
			{
				if (this.m_CD20AbsoluteCount != value)
				{
					this.m_CD20AbsoluteCount = value;
					this.NotifyPropertyChanged("CD20AbsoluteCount");
				}
			}
		}
		
		private void SetCD19AbsoluteCount()
		{
            double? result = null;
			if(this.m_WBC.HasValue && this.m_LymphocytePercentage.HasValue && this.m_CD19BCellPositivePercent.HasValue)
			{				
				result = Math.Round((this.m_WBC.Value * this.m_LymphocytePercentage.Value * this.m_CD19BCellPositivePercent.Value/10000), 2);
			}
			this.CD19AbsoluteCount = result;
		}
		
		public void SetCD20AbsoluteCount()
		{
            double? result = null;
			if(this.m_WBC.HasValue && this.m_LymphocytePercentage.HasValue && this.m_CD20BCellPositivePercent.HasValue)
			{                
				result = Math.Round((this.m_WBC.Value * this.m_LymphocytePercentage.Value * this.m_CD20BCellPositivePercent.Value / 10000), 2);
			}
			this.CD20AbsoluteCount = result;
		}

		public override string ToResultString(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
		{
			StringBuilder result = new StringBuilder();
			
			result.AppendLine("WBC: " + this.m_WBC.ToString());			
			result.AppendLine("Lymphocyte Percentage: " + this.m_LymphocytePercentage.ToString().StringAsPercent());								
			result.AppendLine("CD19 B-Cell Positive Percent: " + this.m_CD19BCellPositivePercent.ToString().StringAsPercent());							
			result.AppendLine("CD20 B-Cell Positive Percent: " + this.m_CD20BCellPositivePercent.ToString().StringAsPercent());			
			result.AppendLine("CD19 Absolute Count: " + this.m_CD19AbsoluteCount.ToString());			
			result.AppendLine("CD20 Absolute Count: " + this.m_CD20AbsoluteCount.ToString());

			return result.ToString();
		}
	}
}
