using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Test.LynchSyndrome
{
	[PersistentClass("tblPanelSetOrderLynchSyndromeEvaluation", "tblPanelSetOrder", "YPIDATA")]
	public class PanelSetOrderLynchSyndromeEvaluation : YellowstonePathology.Business.Test.ReflexTesting.ReflexTestingPlan
	{
        private string m_LynchSyndromeEvaluationType;
		private string m_Interpretation;
		private string m_Comment;
        private string m_Method;
        private bool m_BRAFIsIndicated;

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
            YellowstonePathology.Business.Test.TestOrderInfo testOrderInfo = new TestOrderInfo(lynchSyndromeIHCPanelTest, orderTarget, false);            
            YellowstonePathology.Business.Visitor.OrderTestOrderVisitor orderTestOrderVisitor = new Visitor.OrderTestOrderVisitor(testOrderInfo);            
            accessionOrder.TakeATrip(orderTestOrderVisitor);            
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
        [PersistentDataColumnProperty(true, "1", "0", "bit")]
        public bool BRAFIsIndicated
        {
            get { return this.m_BRAFIsIndicated; }
            set
            {
                if (this.m_BRAFIsIndicated != value)
                {
                    this.m_BRAFIsIndicated = value;
                    this.NotifyPropertyChanged("BRAFIsIndicated");
                }
            }
        }

		public override string ToResultString(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
		{
			StringBuilder result = new StringBuilder();
			result.AppendLine("Interpretation: " + this.m_Interpretation);
			result.AppendLine();

			result.AppendLine("Comment: " + this.m_Comment);
			result.AppendLine();

			return result.ToString();
		}

        protected override void CheckResults(AccessionOrder accessionOrder, object clone)
        {
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
        }
    }
}
