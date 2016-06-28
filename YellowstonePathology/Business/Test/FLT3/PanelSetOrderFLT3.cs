using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Test.FLT3
{
	[PersistentClass("tblPanelSetOrderFLT3", "tblPanelSetOrder", "YPIDATA")]
	public class PanelSetOrderFLT3 : PanelSetOrder
	{
		private string m_Result;
		private string m_ITDMutation;
		private string m_ITDPercentage;
		private string m_TKDMutation;
		private string m_Interpretation;
		private string m_Method;
		private string m_References;

        public PanelSetOrderFLT3()
        {
        }

		public PanelSetOrderFLT3(string masterAccessionNo, string reportNo, string objectId,
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
		public string ITDMutation
		{
			get { return this.m_ITDMutation; }
			set
			{
				if (this.m_ITDMutation != value)
				{
					this.m_ITDMutation = value;
					this.NotifyPropertyChanged("ITDMutation");
				}
			}
		}

		[PersistentProperty()]
		public string ITDPercentage
		{
			get { return this.m_ITDPercentage; }
			set
			{
				if (this.m_ITDPercentage != value)
				{
					this.m_ITDPercentage = value;
					this.NotifyPropertyChanged("ITDPercentage");
				}
			}
		}

		[PersistentProperty()]
		public string TKDMutation
		{
			get { return this.m_TKDMutation; }
			set
			{
				if (this.m_TKDMutation != value)
				{
					this.m_TKDMutation = value;
					this.NotifyPropertyChanged("TKDMutation");
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

            result.AppendLine("ITD Mutation: " + this.m_ITDMutation);
            result.AppendLine();

            result.AppendLine("ITD Percentage: " + this.m_ITDPercentage);
            result.AppendLine();

            result.AppendLine("TDK Mutation: " + this.m_TKDMutation);
            result.AppendLine();

            result.AppendLine("Interpretation: " + this.m_Interpretation);
            result.AppendLine();

            return result.ToString();
        }
	}
}
