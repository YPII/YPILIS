using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.HER2AmplificationByISH
{
	public class HER2AmplificationByISHResult : YellowstonePathology.Business.Test.TestResult
	{
		protected string m_InterpretiveCommentP4IHCOrder = "HER2 immunohistochemistry will be performed and results will be issued in an addendum to the original surgical pathology report.";
		protected string m_InterpretiveComment;
		protected string m_ResultComment;
		protected string m_ResultDescription;
		protected string m_ReportReference;
		protected string PositiveResult = "POSITIVE (amplified)";
		protected string NegativeResult = "NEGATIVE (not amplified)";
		protected string EquivocalResult = "EQUIVOCAL";
		protected string IndeterminateResult = "INDETERMINATE";
		protected string PositiveResultCode = "HRMPLFCTNBISHPSTV";
		protected string NegativeResultCode = "HRMPLFCTNBISHNGTV";
		protected string EquivocalResultCode = "HRMPLFCTNBISHQVCL";
		protected string IndeterminateResultCode = "HRMPLFCTNBISHNDTRMNT";
		protected string m_Method = "This test was performed using a molecular method, In Situ Hybridization (ISH) with the US FDA approved Inform HER2 DNA probe kit, modified to report results according to ASCO/CAP guidelines. The test was performed on paraffin embedded tissue in compliance with ASCO/CAP guidelines.  Probes used include a locus specific probe for HER2 and an internal hybridization control probe for the centromeric region of chromosome 17 (Chr17).";
		protected string m_ASRComment = "This test was performed using a US FDA approved DNA probe kit, modified to report results according to ASCO/CAP guidelines, and the modified procedure was validated by Yellowstone Pathology Institute (YPI).  YPI assumes the responsibility for test performance";

		public HER2AmplificationByISHResult()
		{
			this.m_ResultComment = null;
			this.m_InterpretiveComment = null;
			this.m_ResultDescription = null;
			this.m_ReportReference = null;
		}

		public virtual void SetResults(HER2AmplificationByISHTestOrder testOrder)
		{
			if (testOrder.GeneticHeterogeneity == HER2AmplificationByISHGeneticHeterogeneityCollection.GeneticHeterogeneityPresentInCells)
			{
				this.m_InterpretiveComment += Environment.NewLine + Environment.NewLine +
					"However, this tumor exhibits genetic heterogeneity in HER2 gene amplification in scattered individual cells.  The " +
					"clinical significance and potential clinical benefit of trastuzumab is uncertain when " +
					testOrder.Indicator.ToLower() +
					"carcinoma demonstrates genetic heterogeneity." + Environment.NewLine + Environment.NewLine;
				this.m_ResultComment = "This tumor exhibits genetic heterogeneity in HER2 gene amplification in scattered individual cells.  The clinical " +
					"significance and potential clinical benefit of trastuzumab is uncertain when " +
					testOrder.Indicator.ToLower() + 
					" carcinoma demonstrates genetic heterogeneity";
			}
			else if (testOrder.GeneticHeterogeneity == HER2AmplificationByISHGeneticHeterogeneityCollection.GeneticHeterogeneityPresentInClusters)
			{
				this.m_InterpretiveComment += Environment.NewLine + Environment.NewLine + 
					"However, this tumor exhibits genetic heterogeneity in HER2 gene amplification in small cell clusters. The HER2/Chr17 " +
					"ratio in the clusters is " +
					testOrder.Her2Chr17ClusterRatio +
					".  The clinical significance and potential clinical benefit of trastuzumab is uncertain when " +
					testOrder.Indicator.ToLower() + 
					" carcinoma demonstrates genetic heterogeneity." + Environment.NewLine + Environment.NewLine;
				this.m_ResultComment = "This tumor exhibits genetic heterogeneity in HER2 gene amplification in small cell clusters.  The clinical significance " +
					"and potential clinical benefit of trastuzumab is uncertain when " +
					testOrder.Indicator.ToLower() + 
					" carcinoma demonstrates genetic heterogeneity.";
			}

			testOrder.Result = this.m_Result;
			testOrder.ResultCode = this.m_ResultCode;
			testOrder.ResultComment = this.m_ResultComment;
			testOrder.InterpretiveComment = this.m_InterpretiveComment.TrimEnd();
			testOrder.ResultDescription = this.m_ResultDescription;
			testOrder.CommentLabel = null;
			testOrder.Method = this.m_Method;
			testOrder.ReportReference = this.m_ReportReference;
			testOrder.ASRComment = this.m_ASRComment;
            testOrder.NoCharge = false;
		}

		public static void AcceptResults(HER2AmplificationByISHTestOrder testOrder, YellowstonePathology.Business.User.SystemIdentity systemIdentity)
		{
			testOrder.Accept();
			if (testOrder.PanelOrderCollection.GetUnacceptedPanelCount() > 0)
			{
				YellowstonePathology.Business.Test.PanelOrder panelOrder = testOrder.PanelOrderCollection.GetUnacceptedPanelOrder();
				panelOrder.AcceptResults();
			}
		}

		public static void UnacceptResults(HER2AmplificationByISHTestOrder testOrder)
		{
			testOrder.Unaccept();
			if (testOrder.PanelOrderCollection.GetAcceptedPanelCount() > 0)
			{
				YellowstonePathology.Business.Test.PanelOrder panelOrder = testOrder.PanelOrderCollection.GetLastAcceptedPanelOrder();
				panelOrder.UnacceptResults();
			}
		}
	}
}
