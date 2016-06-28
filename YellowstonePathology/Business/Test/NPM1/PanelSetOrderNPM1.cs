using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Test.NPM1
{
	[PersistentClass("tblPanelSetOrderNPM1", "tblPanelSetOrder", "YPIDATA")]
	public class PanelSetOrderNPM1 : PanelSetOrder
	{
		private string m_Result;
		private string m_PercentageNPM1Mutation;
		private string m_Interpretation;
		private string m_Method;
		private string m_References;
		
		public PanelSetOrderNPM1()
        {
        }

		public PanelSetOrderNPM1(string masterAccessionNo, string reportNo, string objectId,
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
		public string PercentageNPM1Mutation
		{
			get { return this.m_PercentageNPM1Mutation; }
			set
			{
				if (this.m_PercentageNPM1Mutation != value)
				{
					this.m_PercentageNPM1Mutation = value;
					this.NotifyPropertyChanged("PercentageNPM1Mutation");
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

		public override string ToResultString(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {
            StringBuilder result = new StringBuilder();

            result.AppendLine("Result: " + this.m_Result);
            result.AppendLine();

            result.AppendLine("Mutation Percentage: " + this.m_PercentageNPM1Mutation);
            result.AppendLine();

            result.AppendLine("Interpretation: " + this.m_Interpretation);
            result.AppendLine();           

            return result.ToString();
        }
	}
}
