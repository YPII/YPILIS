using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Test.MPNExtendedReflex
{
	[PersistentClass("tblPanelSetOrderMPNExtendedReflex", "tblPanelSetOrder", "YPIDATA")]
	public class PanelSetOrderMPNExtendedReflex : YellowstonePathology.Business.Test.ReflexTesting.ReflexTestingPlan
	{
		private string m_Comment;
		private string m_Interpretation;
		private string m_Method;

		public PanelSetOrderMPNExtendedReflex()
		{

		}

		public PanelSetOrderMPNExtendedReflex(string masterAccessionNo, string reportNo, string objectId,
			YellowstonePathology.Business.PanelSet.Model.PanelSet panelSet,
			YellowstonePathology.Business.Interface.IOrderTarget orderTarget,
			bool distribute)
			: base(masterAccessionNo, reportNo, objectId, panelSet, orderTarget, distribute)
		{

		}

		public override void OrderInitialTests(AccessionOrder accessionOrder, YellowstonePathology.Business.Interface.IOrderTarget orderTarget)
		{
			YellowstonePathology.Business.Test.JAK2V617F.JAK2V617FTest panelSetJAK2V617F = new YellowstonePathology.Business.Test.JAK2V617F.JAK2V617FTest();
            if (accessionOrder.PanelSetOrderCollection.Exists(panelSetJAK2V617F.PanelSetId) == false)
			{
                YellowstonePathology.Business.Test.TestOrderInfo testOrderInfo = new TestOrderInfo(panelSetJAK2V617F, orderTarget, false);                
                YellowstonePathology.Business.Visitor.OrderTestOrderVisitor orderTestOrderVisitor = new Visitor.OrderTestOrderVisitor(testOrderInfo);
                accessionOrder.TakeATrip(orderTestOrderVisitor);
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

		public override string ToResultString(AccessionOrder accessionOrder)
		{
			StringBuilder result = new StringBuilder();
			result.AppendLine("Interpretation:");
			result.AppendLine(this.m_Interpretation);
			result.AppendLine();

			result.AppendLine("Comment:");
			result.AppendLine(this.m_Comment);
			result.AppendLine();

			YellowstonePathology.Business.Test.JAK2V617F.JAK2V617FTest jak2V617FTest = new YellowstonePathology.Business.Test.JAK2V617F.JAK2V617FTest();
			YellowstonePathology.Business.Test.CalreticulinMutationAnalysis.CalreticulinMutationAnalysisTest calreticulinMutationAnalysisTest = new YellowstonePathology.Business.Test.CalreticulinMutationAnalysis.CalreticulinMutationAnalysisTest();
			YellowstonePathology.Business.Test.MPL.MPLTest mplTest = new YellowstonePathology.Business.Test.MPL.MPLTest();
			if(accessionOrder.PanelSetOrderCollection.Exists(jak2V617FTest.PanelSetId) == true)
			{
				result.AppendLine("JAK2 V617F");
				YellowstonePathology.Business.Test.JAK2V617F.JAK2V617FTestOrder jak2V617FTestOrder = (YellowstonePathology.Business.Test.JAK2V617F.JAK2V617FTestOrder)accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(jak2V617FTest.PanelSetId);
				result.AppendLine(jak2V617FTestOrder.ToResultString(accessionOrder));
				result.AppendLine();
			}

			if (accessionOrder.PanelSetOrderCollection.Exists(calreticulinMutationAnalysisTest.PanelSetId) == true)
			{
				result.AppendLine("Calreticulin Mutation Analysis");
				YellowstonePathology.Business.Test.CalreticulinMutationAnalysis.CalreticulinMutationAnalysisTestOrder calreticulinMutationAnalysisTestOrder = (YellowstonePathology.Business.Test.CalreticulinMutationAnalysis.CalreticulinMutationAnalysisTestOrder)accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(calreticulinMutationAnalysisTest.PanelSetId);
				result.AppendLine(calreticulinMutationAnalysisTestOrder.ToResultString(accessionOrder));
				result.AppendLine();
			}

			if (accessionOrder.PanelSetOrderCollection.Exists(mplTest.PanelSetId) == true)
			{
				result.AppendLine("MPL");
				YellowstonePathology.Business.Test.MPL.PanelSetOrderMPL mplTestOrder = (YellowstonePathology.Business.Test.MPL.PanelSetOrderMPL)accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(mplTest.PanelSetId);
				result.AppendLine(mplTestOrder.ToResultString(accessionOrder));
				result.AppendLine();
			}

			return result.ToString();
		}
	}
}
