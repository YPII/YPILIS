using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Test.CLLByFish
{
	[PersistentClass("tblCLLByFishTestOrder", "tblPanelSetOrder", "YPIDATA")]
	public class CLLByFishTestOrder : YellowstonePathology.Business.Test.PanelSetOrder
	{
		private string m_Result;
		private string m_ResultDescription;
		private string m_Interpretation;
		private string m_ProbeSetDetail;
		private string m_NucleiScored;
		private string m_References;

		public CLLByFishTestOrder()
		{
		}

		public CLLByFishTestOrder(string masterAccessionNo, string reportNo, string objectId,
			YellowstonePathology.Business.PanelSet.Model.PanelSet panelSet,
			YellowstonePathology.Business.Interface.IOrderTarget orderTarget,
			bool distribute)
			: base(masterAccessionNo, reportNo, objectId, panelSet, orderTarget, distribute)
		{
            this.m_References = "1. Dohner H, et al. N Engl J Med 2000; 343:1910-6." + Environment.NewLine +
                "2.Hamblin TJ.Best Practice &Research Clinical Haematology. 2007; 20(3):455 - 68." + Environment.NewLine +
                "3.Nowakowski GS, et al. Br J Hematol. 2005; 130:36 - 42." + Environment.NewLine +
                "4.Atlas of Genetics and Cytogenetics in Oncology and Hematology http://atlasgeneticsoncology.org/";
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
		public string ResultDescription
		{
			get { return this.m_ResultDescription; }
			set
			{
				if (this.m_ResultDescription != value)
				{
					this.m_ResultDescription = value;
					this.NotifyPropertyChanged("ResultDescription");
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
		public string ProbeSetDetail
		{
			get { return this.m_ProbeSetDetail; }
			set
			{
				if (this.m_ProbeSetDetail != value)
				{
					this.m_ProbeSetDetail = value;
					this.NotifyPropertyChanged("ProbeSetDetail");
				}
			}
		}

		[PersistentProperty()]
		public string NucleiScored
		{
			get { return this.m_NucleiScored; }
			set
			{
				if (this.m_NucleiScored != value)
				{
					this.m_NucleiScored = value;
					this.NotifyPropertyChanged("NucleiScored");
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

            result.Append("Result: ");
			string resultDescription = this.m_Result;
			if (string.IsNullOrEmpty(this.m_ResultDescription) == false) resultDescription = this.m_ResultDescription;
			result.AppendLine(resultDescription);
            result.AppendLine();            

            result.AppendLine("Interpretation: " + this.m_Interpretation);
            result.AppendLine();

            return result.ToString();
        }
	}
}
