using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Test.SARSCoV2
{
	[PersistentClass("tblSARSCoV2TestOrder", "tblPanelSetOrder", "YPIDATA")]
	public class SARSCoV2TestOrder : PanelSetOrder
	{                
        private string m_Result;        
        private string m_Method;		
        private string m_Interpretation;
        private string m_Comment;
        private string m_ASRComment;

        public SARSCoV2TestOrder()
		{

		}

		public SARSCoV2TestOrder(string masterAccessionNo, string reportNo, string objectId,
			YellowstonePathology.Business.PanelSet.Model.PanelSet panelSet,
			YellowstonePathology.Business.Interface.IOrderTarget orderTarget,
			bool distribute)
			: base(masterAccessionNo, reportNo, objectId, panelSet, orderTarget, distribute)
		{            
            this.m_Method = m_Method = "The sample was mixed with proteinase K and then heat inactivated. Dualplex RT-qPCR was performed targeting the N1 gene specific for SARS-CoV-2. The test is highly specific for SARS-CoV-2 and has a limit of detection of 30 copies/uL.";
            this.m_ReportReferences = "Chantal Vogels, Doug E. Brackney, Chaney C Kalinich, Isabel M Ott, Nathan Grubaugh, Anne Wyllie (09/01/2020). SalivaDirect: RNA extraction-free SARS-CoV-2 diagnostics. ";
            this.m_ASRComment = "This test was performed using a US FDA approved DNA probe kit.  The FDA procedure was performed using a modified DNA extraction method for test optimization, and the modified procedure was validated by Yellowstone Pathology Institute (YPI).  YPI assumes the responsibility for test performance. Laboratory Improvement Amendments of 1988 (CLIA-88) as qualified to perform high complexity clinical laboratory testing.";
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
