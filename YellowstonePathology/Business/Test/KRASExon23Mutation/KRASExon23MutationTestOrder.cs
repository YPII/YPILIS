using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Test.KRASExon23Mutation
{
	[PersistentClass("tblKRASExon23MutationTestOrder", "tblPanelSetOrder", "YPIDATA")]
	public class KRASExon23MutationTestOrder : YellowstonePathology.Business.Test.PanelSetOrder
	{
		private string m_Result;
		private string m_Interpretation;
		private string m_Mutations;
		private string m_Method;
		private string m_ReportDisclaimer;
		
		public KRASExon23MutationTestOrder()
        {
            
        }

		public KRASExon23MutationTestOrder(string masterAccessionNo, string reportNo, string objectId,
			YellowstonePathology.Business.PanelSet.Model.PanelSet panelSet,
            YellowstonePathology.Business.Interface.IOrderTarget orderTarget,
			bool distribute)
			: base(masterAccessionNo, reportNo, objectId, panelSet, orderTarget, distribute)
		{
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "50", "null", "varchar")]
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
		[PersistentDataColumnProperty(true, "1000", "null", "varchar")]
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
		[PersistentDataColumnProperty(true, "50", "null", "varchar")]
		public string Mutations
		{
			get { return this.m_Mutations; }
			set
			{
				if (this.m_Mutations != value)
				{
					this.m_Mutations = value;
					this.NotifyPropertyChanged("Mutations");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "1000", "null", "varchar")]
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
		[PersistentDataColumnProperty(true, "500", "null", "varchar")]
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

		public override string ToResultString(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
		{
			StringBuilder result = new StringBuilder();
			result.Append("Result: ");
			result.AppendLine(this.m_Result);
			result.Append("Interpretation: ");
			result.AppendLine(this.m_Interpretation);
			result.Append("Mutations: ");
			result.AppendLine(this.m_Mutations);
			return result.ToString();
		}
	}
}
