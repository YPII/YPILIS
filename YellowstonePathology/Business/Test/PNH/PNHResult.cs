using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.PNH
{
	public class PNHResult : YellowstonePathology.Business.Test.TestResult
	{
		protected string m_Comment;
		protected string m_Method;
		protected string m_References;
		protected string m_ASRComment;

		enum PNHCellLineEnum { RBC, MONO, GRAN };

		protected List<PNHTestOrder> m_PNHTestOrders;
		protected List<PNHCellLine> m_cellLines;

		public PNHResult()
		{
			m_PNHTestOrders = new List<PNHTestOrder>();
			m_cellLines = new List<PNHCellLine>();
			m_cellLines.Add(new PNHCellLine("RedBloodCells", 0.04m, 0.1m, 1.0m));
			m_cellLines.Add(new PNHCellLine("MonoCyteCells", 0.03m, 0.1m, 1.0m));
			m_cellLines.Add(new PNHCellLine("GranulocyteCells", 0.02m, 0.1m, 1.0m));

			this.m_Method = "Quantitative Flow Cytometry.  High sensitivity immunophenotypic analysis was performed using CD45/side scatter log gating, with further " +
				"gating refinement using CD15 and CD64. GPI-linked proteins were analyzed using CD14, CD24, CD59, and fluorescent Aerolysin(FLAER). Sensitivity " +
				"for granulocytes: 0.01%. Sensitivity for RBC: 0.03%. Sensitivity for Monocytes: 0.02%.";
			this.m_References = "Diagnosis and Management of PNH.  Parker C, et al.  Blood. 2005 Dec 1; 106(12):3699-709." + Environment.NewLine + 
				"Guidelines for the diagnosis and monitoring of paroxysmal nocturnal hemoglobinuria and related disorders by flow cytometry. Borowitz MJ, Craig FE, " +
				"DiGiuseppe JA, Illingworth AJ, Rosse W, Sutherland DR, Wittwer CT, Richards SJ. Cytometry B (Clin Cytometry) 2010 Jul; 78B(4): 211-30.";
			this.m_ASRComment = "Tests utilizing Analytic Specific Regents (ASRs) were developed and performance characteristics determined " +
                "by Yellowstone Pathology Institute, Inc.  They have not been cleared or approved by the U.S. Food and Drug Administration.  " +
                "The FDA has determined that such clearance or approval is not necessary.  ASRs may be used for clinical purposes and should " +
                "not be regarded as investigational or for research.  This laboratory is certified under the Clinical Laboratory Improvement " +
                "Amendments of 1988 (CLIA-88) as qualified to perform high complexity clinical laboratory testing.";
		}

		public decimal RedBloodCellsTypeIIResult
		{
			get { return this.m_cellLines[(int)PNHCellLineEnum.RBC].Type2; }
		}

		public decimal RedBloodCellsTypeIIIResult
		{
			get { return this.m_cellLines[(int)PNHCellLineEnum.RBC].Type3; }
		}

		public decimal RedBloodTotal
		{
			get { return this.m_cellLines[(int)PNHCellLineEnum.RBC].CellLineValue; }
		}

		public decimal MonocytesTypeIIResult
		{
			get { return this.m_cellLines[(int)PNHCellLineEnum.MONO].Type2; }
		}

		public decimal MonocytesTypeIIIResult
		{
			get { return this.m_cellLines[(int)PNHCellLineEnum.MONO].Type3; }
		}

		public decimal MonocytesTotal
		{
			get { return this.m_cellLines[(int)PNHCellLineEnum.MONO].CellLineValue; }
		}

		public decimal GranulocytesTypeIIResult
		{
			get { return this.m_cellLines[(int)PNHCellLineEnum.GRAN].Type2; }
		}

		public decimal GranulocytesTypeIIIResult
		{
			get { return this.m_cellLines[(int)PNHCellLineEnum.GRAN].Type3; }
		}

		public decimal GranulocytesTotal
		{
			get { return this.m_cellLines[(int)PNHCellLineEnum.GRAN].CellLineValue; }
		}

		public void SetTotals(PNHTestOrder pnhTestOrder)
		{
			this.m_cellLines[(int)PNHCellLineEnum.RBC].Type1 = Convert.ToDecimal(pnhTestOrder.TypeIRedBloodCells);
			this.m_cellLines[(int)PNHCellLineEnum.RBC].Type2 = Convert.ToDecimal(pnhTestOrder.TypeIIRedBloodCells);
			this.m_cellLines[(int)PNHCellLineEnum.RBC].Type3 = Convert.ToDecimal(pnhTestOrder.TypeIIIRedBloodCells);

			this.m_cellLines[(int)PNHCellLineEnum.GRAN].Type2 = Convert.ToDecimal(pnhTestOrder.TypeIIGranulocytes);
			this.m_cellLines[(int)PNHCellLineEnum.GRAN].Type3 = Convert.ToDecimal(pnhTestOrder.TypeIIIGranulocytes);

			this.m_cellLines[(int)PNHCellLineEnum.MONO].Type2 = Convert.ToDecimal(pnhTestOrder.TypeIIMonocytes);
			this.m_cellLines[(int)PNHCellLineEnum.MONO].Type3 = Convert.ToDecimal(pnhTestOrder.TypeIIIMonocytes);
		}

		public bool IsSignificantPositiveResult
		{
			get{ return (this.CountAboveThreshold > 1 && this.CountAboveSmallLimit > 0); }
		}

		public bool IsSmallPositiveResult
		{
			get { return this.CountBetweenRareAndSmall > 1; }
		}

        public bool IsRareResult
        {
            get { return this.CountBetweenThresholdAndRare > 1; }
        }

        public bool IsGpiDeficientResult
		{
			get
			{
				bool rbcBetweenThresholdAndLargeLimit = (this.m_cellLines[(int)PNHCellLineEnum.RBC].IsAboveThreshold && !this.m_cellLines[(int)PNHCellLineEnum.RBC].IsAboveSmall);
				return (this.CountBetweenThresholdAndRare == 1 && !rbcBetweenThresholdAndLargeLimit);
			}
		}

		public bool IsPersistentResult(List<YellowstonePathology.Business.Test.AccessionOrder> accessionOrders, string masterAccessionNo, DateTime compareDate)
		{
			return ((this.IsSmallPositiveResult || this.IsSignificantPositiveResult) && this.HasPreviousPositiveResults(accessionOrders, masterAccessionNo, compareDate));
		}

		public bool IsNegativeWithPreviousPositiveResult(List<YellowstonePathology.Business.Test.AccessionOrder> accessionOrders, string masterAccessionNo, DateTime compareDate)
		{
			return (this.IsNegativeResult && this.HasPreviousPositiveResults(accessionOrders, masterAccessionNo, compareDate));
		}

		public bool IsNegativeResult
		{
			get { return !(this.IsSignificantPositiveResult || this.IsRareResult || this.IsSmallPositiveResult || this.IsGpiDeficientResult); }
		}        

        public List<PNHTestOrder> GetPreviousPanelSetOrders(List<YellowstonePathology.Business.Test.AccessionOrder> accessionOrders, string masterAccessionNo, DateTime compareDate)
		{
			m_PNHTestOrders = (from ao in accessionOrders
							  where ao.MasterAccessionNo != masterAccessionNo
							  from pso in ao.PanelSetOrderCollection
							  where pso.PanelSetId == 19 && pso.Final == true && pso.FinalDate.Value < compareDate
							  orderby pso.FinalDate.Value descending
							  select (PNHTestOrder)pso).ToList<PNHTestOrder>();
			return m_PNHTestOrders;
		}

		public List<YellowstonePathology.Business.Test.AccessionOrder> GetPreviousAccessions(string patientId)
		{
			List<YellowstonePathology.Business.Test.AccessionOrder> accessionOrders = new List<YellowstonePathology.Business.Test.AccessionOrder>();
			YellowstonePathology.Business.Test.SearchEngine searchEngine = new YellowstonePathology.Business.Test.SearchEngine();
			searchEngine.SetFillByPatientId(patientId);
			searchEngine.FillSearchList();
			foreach (YellowstonePathology.Business.Search.ReportSearchItem item in searchEngine.ReportSearchList)
			{
                accessionOrders.Add(YellowstonePathology.Business.Persistence.DocumentGateway.Instance.GetAccessionOrderByMasterAccessionNo(item.MasterAccessionNo));
			}

			return accessionOrders;
		}

		private bool HasPreviousPositiveResults(List<YellowstonePathology.Business.Test.AccessionOrder> accessionOrders, string masterAccessionNo, DateTime compareDate)
		{
			bool returnValue = false;
			this.GetPreviousPanelSetOrders(accessionOrders, masterAccessionNo, compareDate);
			int positiveCount = (from pso in m_PNHTestOrders
								 where pso.Result.Contains("Positive") || pso.Result.Contains("Persistent")
								 select pso).Count<PNHTestOrder>();
			if (positiveCount > 0)
			{				
				returnValue = true;
			}
			return returnValue;
		}

		private int CountAboveThreshold
		{
			get
			{
				int count = (from cl in this.m_cellLines
							 where cl.IsAboveThreshold
							 select cl).Count<PNHCellLine>();
				return count;
			}
		}
		private int CountAboveSmallLimit
		{
			get
			{
				int count = (from cl in this.m_cellLines
							 where cl.IsAboveSmall
							 select cl).Count<PNHCellLine>();
				return count;
			}
		}

		private int CountBetweenThresholdAndRare
		{
			get
			{
				int count = (from cl in this.m_cellLines
							 where cl.IsAboveThreshold && !cl.IsAboveRare
							 select cl).Count<PNHCellLine>();
				return count;
			}
		}

        private int CountBetweenRareAndSmall
        {
            get
            {
                int count = (from cl in this.m_cellLines
                             where cl.IsAboveRare && !cl.IsAboveSmall
                             select cl).Count<PNHCellLine>();
                return count;
            }
        }

        private int CountGreaterThanSmall
        {
            get
            {
                int count = (from cl in this.m_cellLines
                             where cl.IsAboveSmall
                             select cl).Count<PNHCellLine>();
                return count;
            }
        }

        public virtual void SetResults(PNHTestOrder testOrder)
		{
			testOrder.Result = this.m_Result;
			testOrder.ResultCode = this.m_ResultCode;
			testOrder.Comment = this.m_Comment;
			testOrder.Method = this.m_Method;
			testOrder.References = this.m_References;
			testOrder.ASRComment = this.m_ASRComment;
		}
	}
}
