using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Test.ALKForNSCLCByFISH
{
    [PersistentClass("tblALKForNSCLCByFISHTestOrder", "tblPanelSetOrder", "YPIDATA")]
	public class ALKForNSCLCByFISHTestOrder : YellowstonePathology.Business.Test.PanelSetOrder, YellowstonePathology.Business.Interface.ISolidTumorTesting
	{
		private string m_Result;
		private string m_Interpretation;
		private string m_ReferenceRange;
		private string m_ProbeSetDetail;
		private string m_NucleiScored;
		private string m_References;
		private string m_NucleiPercent;
		private string m_Fusions;
        private string m_Method;
		private string m_ReportDisclaimer;
        private string m_ThreeFPercentage;
        private bool m_ALKGeneAmplification;
        private string m_TumorNucleiPercentage;        

        public ALKForNSCLCByFISHTestOrder()
        {

        }

		public ALKForNSCLCByFISHTestOrder(string masterAccessionNo, string reportNo, string objectId,
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
		public string ReferenceRange
		{
			get { return this.m_ReferenceRange; }
			set
			{
				if (this.m_ReferenceRange != value)
				{
					this.m_ReferenceRange = value;
					this.NotifyPropertyChanged("ReferenceRange");
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

		[PersistentProperty()]
		public string NucleiPercent
		{
			get { return this.m_NucleiPercent; }
			set
			{
				if (this.m_NucleiPercent != value)
				{
					this.m_NucleiPercent = value;
					this.NotifyPropertyChanged("NucleiPercent");
				}
			}
		}

		[PersistentProperty()]
		public string Fusions
		{
			get { return this.m_Fusions; }
			set
			{
				if (this.m_Fusions != value)
				{
					this.m_Fusions = value;
					this.NotifyPropertyChanged("Fusions");
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
        public string ThreeFPercentage
        {
            get { return this.m_ThreeFPercentage; }
            set
            {
                if (this.m_ThreeFPercentage != value)
                {
                    this.m_ThreeFPercentage = value;
                    this.NotifyPropertyChanged("ThreeFPercentage");
                }
            }
        }

        [PersistentProperty()]
        public bool ALKGeneAmplification
        {
            get { return this.m_ALKGeneAmplification; }
            set
            {
                if (this.m_ALKGeneAmplification != value)
                {
                    this.m_ALKGeneAmplification = value;
                    this.NotifyPropertyChanged("ALKGeneAmplification");
                }
            }
        }

        [PersistentProperty()]
        public string TumorNucleiPercentage
        {
            get { return this.m_TumorNucleiPercentage; }
            set
            {
                if (this.m_TumorNucleiPercentage != value)
                {
                    this.m_TumorNucleiPercentage = value;
                    this.NotifyPropertyChanged("TumorNucleiPercentage");
                }
            }
        }

		public override string ToResultString(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
		{
			StringBuilder result = new StringBuilder();

			result.AppendLine("Result: " + this.m_Result);
			result.AppendLine();

			result.AppendLine("Nuclei Scored: " + this.m_NucleiScored);
			result.AppendLine();

			result.AppendLine("Reference Range: " + this.m_ReferenceRange);
			result.AppendLine();

			result.AppendLine("Nuclei Percent: " + this.m_NucleiPercent);
			result.AppendLine();

			result.AppendLine("Fusions: " + this.m_Fusions);
			result.AppendLine();

			result.AppendLine("Interpretation: " + this.m_Interpretation);
			result.AppendLine();

			result.AppendLine("Probeset Detail: " + this.m_ProbeSetDetail);
			result.AppendLine();

            result.AppendLine("Method: " + this.m_Method);
            result.AppendLine();

			return result.ToString();
		}
	}
}
