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
        private string m_ASR;
        private string m_ProbeComment;

        public CLLByFishTestOrder()
		{
		}

		public CLLByFishTestOrder(string masterAccessionNo, string reportNo, string objectId,
			YellowstonePathology.Business.PanelSet.Model.PanelSet panelSet,
			YellowstonePathology.Business.Interface.IOrderTarget orderTarget,
			bool distribute)
			: base(masterAccessionNo, reportNo, objectId, panelSet, orderTarget, distribute)
		{
            this.m_ReportReferences = "1. Dohner H, et al. N Engl J Med 2000; 343:1910-6." + Environment.NewLine +
                "2.Hamblin TJ.Best Practice &Research Clinical Haematology. 2007; 20(3):455 - 68." + Environment.NewLine +
                "3.Nowakowski GS, et al. Br J Hematol. 2005; 130:36 - 42." + Environment.NewLine +
                "4.Atlas of Genetics and Cytogenetics in Oncology and Hematology http://atlasgeneticsoncology.org/";
            this.m_ASR = "The performance characteristics of this test have been determined by NeoGenomics Laboratories. This test has not been approved by the FDA. The FDA has determined such clearance or approval is not necessary. This laboratory is CLIA certified to perform high complexity clinical testing.";
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
		[PersistentDataColumnProperty(true, "500", "null", "varchar")]
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
		[PersistentDataColumnProperty(true, "100", "null", "varchar")]
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
        [PersistentDataColumnProperty(true, "5000", "null", "varchar")]
        public string ASR
        {
            get { return this.m_ASR; }
            set
            {
                if (this.m_ASR != value)
                {
                    this.m_ASR = value;
                    this.NotifyPropertyChanged("ASR");
                }
            }
        }

        [PersistentProperty()]
        public string ProbeComment
        {
            get { return this.m_ProbeComment; }
            set
            {
                if (this.m_ProbeComment != value)
                {
                    this.m_ProbeComment = value;
                    this.NotifyPropertyChanged("ProbeComment");
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
