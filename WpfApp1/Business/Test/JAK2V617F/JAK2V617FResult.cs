using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.JAK2V617F
{
	public class JAK2V617FResult : YellowstonePathology.Business.Test.TestResult
	{
		public static string Method = "Total nucleic acid was extracted from patient’s plasma or PB/BM cells. The primer pair was designed to " +
            "encompass the JAK 2 V617F point mutation located in chromosome 9p24.  The PCR product was then purified and sequenced in both " +
            "forward and reverse directions using high-sensitivity Sanger sequencing which improves the lower detection limit to approximately " +
            "1% mutated DNA in a wild-type background.  The resulting sequence was compared to GenBank Acc# NM_004972 sequence. Various factors " +
            "including quantity and quality of nucleic acid, sample preparation and sample age can affect assay performance.";

        public static string References = "1.  James C, et al. A unique clonal JAK2 mutation leading to constitutive signalling causes " +
            "polycythaemia vera. Nature. 2005; 434:1144-8." + Environment.NewLine +
            "2.  Kralovics R, et al.A gain-of-function mutation of JAK2 in myeloproliferative disorders.N Engl J Med. 2005; 352:1779–90.";

		protected string m_Interpretation;
		protected string m_Comment;

		public JAK2V617FResult()
		{

		}

        public void SetResults(JAK2V617FTestOrder panelSetOrderJAK2V617F)
        {
            panelSetOrderJAK2V617F.Interpretation = this.m_Interpretation;            
            panelSetOrderJAK2V617F.Comment = this.m_Comment;
            panelSetOrderJAK2V617F.Reference = References;
            panelSetOrderJAK2V617F.Method = Method;            
        }
	}
}
