using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YellowstonePathology.Business.Audit.Model;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Test.LynchSyndrome
{
	[PersistentClass("tblPanelSetOrderLynchSyndromeEvaluation", "tblPanelSetOrder", "YPIDATA")]
	public class PanelSetOrderLynchSyndromeEvaluation : YellowstonePathology.Business.Test.ReflexTesting.ReflexTestingPlan
	{
        private string m_LynchSyndromeEvaluationType;
        private string m_Result;
        private string m_Interpretation;		
        private string m_Method;
        private bool m_ReflexToBRAFMETH;

		public PanelSetOrderLynchSyndromeEvaluation()
		{

		}

		public PanelSetOrderLynchSyndromeEvaluation(string masterAccessionNo, string reportNo, string objectId,
			YellowstonePathology.Business.PanelSet.Model.PanelSet panelSet,
			YellowstonePathology.Business.Interface.IOrderTarget orderTarget,
			bool distribute)
			: base(masterAccessionNo, reportNo, objectId, panelSet, orderTarget, distribute)
		{
            this.m_LynchSyndromeEvaluationType = YellowstonePathology.Business.Test.LynchSyndrome.LSEType.NOTSET;
		}

		public override void OrderInitialTests(AccessionOrder accessionOrder, YellowstonePathology.Business.Interface.IOrderTarget orderTarget)
		{						
            YellowstonePathology.Business.Test.LynchSyndrome.LynchSyndromeIHCPanelTest lynchSyndromeIHCPanelTest = new YellowstonePathology.Business.Test.LynchSyndrome.LynchSyndromeIHCPanelTest();
            if (accessionOrder.PanelSetOrderCollection.Exists(lynchSyndromeIHCPanelTest.PanelSetId, this.m_OrderedOnId, true) == false)
            {
                YellowstonePathology.Business.Test.TestOrderInfo testOrderInfo = new TestOrderInfo(lynchSyndromeIHCPanelTest, orderTarget, false);
                YellowstonePathology.Business.Visitor.OrderTestOrderVisitor orderTestOrderVisitor = new Visitor.OrderTestOrderVisitor(testOrderInfo);
                accessionOrder.TakeATrip(orderTestOrderVisitor);
            }
		}		

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "50", "null", "varchar")]
        public string LynchSyndromeEvaluationType
        {
            get { return this.m_LynchSyndromeEvaluationType; }
            set
            {
                if (this.m_LynchSyndromeEvaluationType != value)
                {
                    this.m_LynchSyndromeEvaluationType = value;
                    this.NotifyPropertyChanged("LynchSyndromeEvaluationType");
                }
            }
        }

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "500", "null", "varchar")]
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
        [PersistentDataColumnProperty(true, "5000", "null", "varchar")]
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
        [PersistentDataColumnProperty(true, "1", "0", "tinyint")]
        public bool ReflexToBRAFMeth
        {
            get { return this.m_ReflexToBRAFMETH; }
            set
            {
                if (this.m_ReflexToBRAFMETH != value)
                {
                    this.m_ReflexToBRAFMETH = value;
                    this.NotifyPropertyChanged("ReflexToBRAFMETH");
                }
            }
        }

		public override string ToResultString(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
		{
            StringBuilder result = new StringBuilder();
            result.AppendLine("Result: " + this.m_Result);
            result.AppendLine();
            
			result.AppendLine("Interpretation: " + this.m_Interpretation);
			result.AppendLine();			

			return result.ToString();
		}

        protected override void CheckResults(AccessionOrder accessionOrder, object clone)
        {
            throw new Exception("needs workd");
            /*
            PanelSetOrderLynchSyndromeEvaluation panelSetToCheck = (PanelSetOrderLynchSyndromeEvaluation)clone;
            YellowstonePathology.Business.Test.LynchSyndrome.LSEResult cloneLSEResult = null;
            YellowstonePathology.Business.Test.LynchSyndrome.LSEResult lseResult = YellowstonePathology.Business.Test.LynchSyndrome.LSEResult.GetResult(accessionOrder, panelSetToCheck);
            YellowstonePathology.Business.Test.LynchSyndrome.LSEResult accessionLSEResult = YellowstonePathology.Business.Test.LynchSyndrome.LSEResultCollection.GetResult(lseResult, panelSetToCheck.LynchSyndromeEvaluationType);

            if (accessionLSEResult == null)
            {
                cloneLSEResult = lseResult;
            }
            else
            {
                cloneLSEResult = accessionLSEResult;
            }

            cloneLSEResult.SetResults(accessionOrder, panelSetToCheck);
            */
        }

        public Audit.Model.AuditResult IsOkToSetResults()
        {
            Audit.Model.AuditResult result = new Audit.Model.AuditResult();
            result.Status = Audit.Model.AuditStatusEnum.OK;
            if(this.LynchSyndromeEvaluationType == LSEType.NOTSET)
            {
                result.Status = Audit.Model.AuditStatusEnum.Failure;
                result.Message = "Results may not be set as the Indication is not selected.";
            }
            else if (this.Accepted == true)
            {
                result.Status = Audit.Model.AuditStatusEnum.Failure;
                result.Message = "Results may not be set as the results have been accepted.";
            }
            return result;
        }

        public override AuditResult IsOkToAccept(AccessionOrder accessionOrder)
        {
            AuditResult result = base.IsOkToAccept(accessionOrder);
            if(result.Status == AuditStatusEnum.OK)
            {
                if(string.IsNullOrEmpty(this.m_Result) == true)
                {
                    result.Status = AuditStatusEnum.Failure;
                    result.Message = "Unable to accept results as the result has not been set.";
                }
            }
            return result;
        }
    }
}
