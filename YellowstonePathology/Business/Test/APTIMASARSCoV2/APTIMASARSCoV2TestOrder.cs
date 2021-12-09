using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Test.APTIMASARSCoV2
{
	[PersistentClass("tblAPTIMASARSCoV2TestOrder", "tblPanelSetOrder", "YPIDATA")]
	public class APTIMASARSCoV2TestOrder : PanelSetOrder
	{                
        private string m_Result;        
        private string m_Method;		
        private string m_Interpretation;
        private string m_Comment;
        private string m_ASRComment;

        public APTIMASARSCoV2TestOrder()
		{

		}

		public APTIMASARSCoV2TestOrder(string masterAccessionNo, string reportNo, string objectId,
			YellowstonePathology.Business.PanelSet.Model.PanelSet panelSet,
			YellowstonePathology.Business.Interface.IOrderTarget orderTarget,
			bool distribute)
			: base(masterAccessionNo, reportNo, objectId, panelSet, orderTarget, distribute)
		{

            StringBuilder references = new StringBuilder();
            references.AppendLine("Centers for Disease Control and Prevention.Coronavirus Disease 2019 - (COVID - 19) Situation Summary.Updated March 9, 2020.");
            references.AppendLine("Centers for Disease Control and Prevention Web site https://www.cdc.gov/coronavirus/2019-ncov/summary.html. Accessed March 10, 2020");
            references.Append("Aptima SARS-CoV-2 Assay (Panther System) Package Insert. AW-21492-001 Rev. 008");
            
            this.m_Method = "The Aptima SARS-CoV-2 assay is a nucleic acid amplification in vitro diagnostic test intended for the qualitative detection of RNA from SARS-CoV-2 isolated and purified from nasopharyngeal (NP)and oropharyngeal(OP) swab specimens, nasopharyngeal washes/aspirates or nasal aspirates.The Aptima SARS-CoV-2 assay combines the technologies of target capture, Transcription Mediated Amplification(TMA), and Dual Kinetic Assay(DKA).";

            this.Comment = "Questions about your test result or clinical condition should be directed to your personal physician or healthcare provider.  For local quarantine and return-to-work guidelines, refer to https://covid.riverstonehealth.org/.";

            this.m_ReportReferences = references.ToString();

            StringBuilder asr = new StringBuilder();
            asr.AppendLine("Testing performed on the Panther instrument. This assay is for in vitro diagnostic use under FDA Emergency Use Authorization only.");
            asr.AppendLine("False-negative results may arise from degradation of viral RNA during shipping/storage.");
            asr.AppendLine("Results should be interpreted by a trained professional in conjunction with the patient's history and clinical signs and symptoms, and epidemiological risk factors.");
            asr.AppendLine("Negative results do not preclude infection with the SARS - CoV - 2 virus and should not be the sole basis of patient treatment / management or public health decision.Follow up testing should be performed according to the current CDC recommendations.");
            asr.AppendLine("The Aptima SARS-CoV-2 assay is for use only under Emergency Use Authorization (EUA) in laboratories certified under the Clinical Laboratory Improvement Amendments of 1988 (CLIA), 42 U.S.C. 263a, that meet requirements to perform high complexity tests. YPI assumes the responsibility for test performance.  This laboratory is certified under the Clinical Laboratory Improvement Amendments of 1988 (CLIA-88) as qualified to perform high complexity clinical laboratory testing");

            this.m_ASRComment = asr.ToString();
            this.m_TechnicalComponentInstrumentId = Instrument.HOLOGICPANTHERID;            
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "100", "null", "varchar")]
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
        [PersistentDataColumnProperty(true, "1000", "null", "varchar")]
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
        [PersistentDataColumnProperty(true, "1000", "null", "varchar")]
        public string ASRComment
        {
            get { return this.m_ASRComment; }
            set
            {
                if (this.m_ASRComment != value)
                {
                    this.m_ASRComment = value;
                    this.NotifyPropertyChanged("ASRComment");
                }
            }
        }

        public override string ToResultString(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
		{
			StringBuilder result = new StringBuilder();
			result.Append("SARSCoV2: ");
			result.AppendLine(this.m_Result);			
            return result.ToString();
		}                		
    }
}
