using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Her2AmplificationByFish
{
	public class Her2AmplificationByFishPositiveResult : Her2AmplificationByFishResult
	{
		public Her2AmplificationByFishPositiveResult()
		{
			this.m_Result = "Positive";
			this.m_Interpretation = "Average HER2 signals/nucleus: " + "$AVGHER2SIGNALS$\r\n" +
				"Average CEN 17 signals/nucleus: " + "$AVGCEN17SIGNALS$\r\n" +
				"HER2/CEN 17 signal ratio: " + "$HER2CEN17SIGNALRATIO$\r\n\r\n" +
				"Along with fluorescence in situ hybridization (FISH), an H&E stained slide was reviewed by a pathologist to identify the target area " +
				"containing invasive tumor.  FISH analysis of 40 interphase nuclei was performed within the marked target area using a dual-probe FISH " +
				"assay.  Controls performed appropriately.  Results show HER2 amplification and a HER2/CEN17 ratio of >/=2.0 with average HER2 copy " +
				"number >/=4.0 signals per cell.  This is a POSITIVE result.\r\n\r\n" +
				"Note: This HER2 FISH assay was scored on an automated image analysis platform.  Two or more independent areas of tumor were " +
				"analyzed and the technical results underwent a manual technologist review for quality control purposes.";
			this.m_Comment = "Fixation time in 10% neutral buffered formalin was at least 6 hours and less than 72 hours and the cold ischemia time was less than 1 hour, " +
				"which meets 2013 ASCO/CAP guidelines for HER2 testing in breast cancer.\r\n\r\n" +
				"The ASCO/CAP guidelines recommend confirmation of HER2 results for the following situations: (1) when there is discordance between the " +
				"HER2 results and the histopathologic findings (e.g. positive HER2 result in certain histologic grade 1 carcinomas), (2) limited invasive tumor " +
				"in a core biopsy with negative HER2 result, (3) resection specimen contains high grade carcinoma that is morphologically distinct from that in " +
				"the core biopsy, (4) core biopsy results are equivocal by both IHC and ISH, and (5) specimen handling of core biopsy is suspected not to " +
				"meet fixation guidelines.";
			this.m_Reference = "Wolff AC, Hammond ME, Hicks DG, et al; American Society of Clinical Oncology; College of American Pathologists. " +
				"Recommendations for human epidermal growth factor receptor 2 testing in breast cancer: American Society of Clinical Oncology/College of " +
				"American Pathologists clinical practice guideline update. Arch Pathol Lab Med. 2014;138(2):241-56.";
		}
	}
}
