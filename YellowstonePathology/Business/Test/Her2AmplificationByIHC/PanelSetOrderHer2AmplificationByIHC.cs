using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YellowstonePathology.Business.Persistence;

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
	}
}
