using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YellowstonePathology.Business.Audit.Model;
using YellowstonePathology.Business.Persistence;
using YellowstonePathology.Business.Rules;

namespace YellowstonePathology.Business.Test.Her2AmplificationByIHC
{
	[PersistentClass("tblPanelSetOrderHer2AmplificationByIHC", "tblPanelSetOrder", "YPIDATA")]
    public class PanelSetOrderHer2AmplificationByIHC : PanelSetOrder
	{
		private string m_Result;
		private string m_Score;
		private string m_IntenseCompleteMembraneStainingPercent;
		private string m_BreastTestingFixative;
		private string m_Method;
		private string m_Interpretation;
		private string m_ReportDisclaimer;
		private string m_Reference;

        public PanelSetOrderHer2AmplificationByIHC()
        {

        }

		public PanelSetOrderHer2AmplificationByIHC(string masterAccessionNo, string reportNo, string objectId,
			YellowstonePathology.Business.PanelSet.Model.PanelSet panelSet,
			YellowstonePathology.Business.Interface.IOrderTarget orderTarget,
			bool distribute)
			: base(masterAccessionNo, reportNo, objectId, panelSet, orderTarget, distribute)
		{
            this.ReportDisclaimer = this.GetLocationPerformedComment() + Environment.NewLine + "The performance characteristics of this test " +
                "have been determined by NeoGenomics Laboratories.  This test has not been approved by the FDA.  The FDA has determined such " +
                "clearance or approval is not necessary.  This laboratory is CLIA certified to perform high complexity clinical testing.";

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
		[PersistentDataColumnProperty(true, "50", "null", "varchar")]
		public string Score
		{
			get { return this.m_Score; }
			set
			{
				if (this.m_Score != value)
				{
					this.m_Score = value;
					this.NotifyPropertyChanged("Score");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "50", "null", "varchar")]
		public string IntenseCompleteMembraneStainingPercent
		{
			get { return this.m_IntenseCompleteMembraneStainingPercent; }
			set
			{
				if (this.m_IntenseCompleteMembraneStainingPercent != value)
				{
					this.m_IntenseCompleteMembraneStainingPercent = value;
					this.NotifyPropertyChanged("IntenseCompleteMembraneStainingPercent");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "500", "null", "varchar")]
		public string BreastTestingFixative
		{
			get { return this.m_BreastTestingFixative; }
			set
			{
				if (this.m_BreastTestingFixative != value)
				{
					this.m_BreastTestingFixative = value;
					this.NotifyPropertyChanged("BreastTestingFixative");
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
		[PersistentDataColumnProperty(true, "500", "null", "varchar")]
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
		public string ReportDisclaimer
		{
			get { return this.m_ReportDisclaimer; }
			set
			{
				if (this.m_ReportDisclaimer != value)
				{
					this.m_ReportDisclaimer = value;
					this.NotifyPropertyChanged("ReportDisclaimer");
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

		public override string ToResultString(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
		{
			StringBuilder result = new StringBuilder();

			result.AppendLine("Result: " + this.m_Result);
			result.AppendLine();

			result.AppendLine("Score: " + this.m_Score);
			result.AppendLine();

			result.AppendLine("Intense Complete Membrane Staining Percent: " + this.m_IntenseCompleteMembraneStainingPercent);
			result.AppendLine();

			result.AppendLine("Interpretation: " + this.m_Interpretation);
			result.AppendLine();

			return result.ToString();
		}

        public override MethodResult IsOkToAccept()
        {
            MethodResult result = base.IsOkToAccept();
            if(result.Success == true)
            {
                if(string.IsNullOrEmpty(this.m_Result) == true)
                {
                    result.Success = false;
                    result.Message = "Unable to accept results as the result is not present." + Environment.NewLine;
                }

                if (string.IsNullOrEmpty(this.m_Score) == true)
                {
                    result.Success = false;
                    result.Message += "Unable to accept results as the score is not present." + Environment.NewLine;
                }
            }
            return result;
        }

        public override AuditResult IsOkToFinalize(AccessionOrder accessionOrder)
        {
            AuditResult result = base.IsOkToFinalize(accessionOrder);

            if (result.Status == AuditStatusEnum.OK)
            {
                HER2AmplificationByISH.HER2AmplificationByISHTest ishTest = new HER2AmplificationByISH.HER2AmplificationByISHTest();
                if(accessionOrder.PanelSetOrderCollection.Exists(ishTest.PanelSetId, this.m_OrderedOnId, true) == true)
                {
                    HER2AmplificationByISH.HER2AmplificationByISHTestOrder ishTestOrder = (HER2AmplificationByISH.HER2AmplificationByISHTestOrder)accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(ishTest.PanelSetId, this.m_OrderedOnId, true);
                    if(ishTestOrder.Final == true)
                    {
                        if (this.m_Score.Contains("2+") == true)
                        {
                            HER2AmplificationRecount.HER2AmplificationRecountTest test = new HER2AmplificationRecount.HER2AmplificationRecountTest();
                            if (accessionOrder.PanelSetOrderCollection.Exists(test.PanelSetId, this.m_OrderedOnId, true) == false)
                            {
                                result.Status = AuditStatusEnum.Failure;
                                result.Message = "Unable to finalize as a " + test.PanelSetName + " is required.";
                            }
                        }
                        else
                        {
                            HER2AmplificationSummary.HER2AmplificationSummaryTest summaryTest = new HER2AmplificationSummary.HER2AmplificationSummaryTest();
                            if (accessionOrder.PanelSetOrderCollection.Exists(summaryTest.PanelSetId, this.m_OrderedOnId, true) == false)
                            {
                                result.Status = AuditStatusEnum.Failure;
                                result.Message = "Unable to finalize as a " + summaryTest.PanelSetName + " is required.";
                            }
                        }
                    }
                }
            }

            return result;
        }

        public override FinalizeTestResult Finish(AccessionOrder accessionOrder)
        {
            if (this.m_Score.Contains("2+") == false)
            {
                HER2AmplificationSummary.HER2AmplificationSummaryTest test = new HER2AmplificationSummary.HER2AmplificationSummaryTest();
                if (accessionOrder.PanelSetOrderCollection.Exists(test.PanelSetId, this.m_OrderedOnId, true) == true)
                {
                    HER2AmplificationSummary.HER2AmplificationSummaryTestOrder testOrder = (HER2AmplificationSummary.HER2AmplificationSummaryTestOrder)accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(test.PanelSetId, this.m_OrderedOnId, true);
                    testOrder.SetValues(accessionOrder);
                }
            }
            return base.Finish(accessionOrder);
        }
    }
}
