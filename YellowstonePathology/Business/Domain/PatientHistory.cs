using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Collections.ObjectModel;

namespace YellowstonePathology.Business.Domain
{
    public class PatientHistory : ObservableCollection<PatientHistoryResult>
    {
        public PatientHistory()
        {

        }

        public bool PanelSetIdExists(int panelSetId)
        {
            bool result = false;
            foreach(PatientHistoryResult patientHistoryResult in this)
            {
                if(patientHistoryResult.PanelSetId == panelSetId)
                {
                    result = true;
                    break;
                }
            }
            return result;
        }

        public PatientHistoryResult GetByPanelSetId(int panelSetId)
        {
            PatientHistoryResult result = null;
            foreach (PatientHistoryResult patientHistoryResult in this)
            {
                if (patientHistoryResult.PanelSetId == panelSetId)
                {
                    result = patientHistoryResult;
                    break;
                }
            }
            return result;
        }

        public Nullable<DateTime> GetDateOfPreviousHpv(DateTime thisAccessionDate)
        {
            Nullable<DateTime> result = null;
            foreach (PatientHistoryResult patientHistoryResult in this)
            {
                if (patientHistoryResult.AccessionDate != thisAccessionDate)
                {
                    if (patientHistoryResult.PanelSetId == 14 || patientHistoryResult.PanelSetId == 10)
                    {
                        if (result.HasValue == true)
                        {
                            if (patientHistoryResult.FinalDate > result.Value)
                            {
                                result = patientHistoryResult.FinalDate;
                            }
                        }
                        else
                        {
                            result = patientHistoryResult.FinalDate;
                        }
                    }
                }
            }
            return result;
        }

		public PatientHistory GetPriorPapRelatedHistory(string masterAccessionNo, DateTime cutoffDate)
		{
			PatientHistory result = new PatientHistory();
			YellowstonePathology.Business.Test.ThinPrepPap.ThinPrepPapTest panelSetThinPrepPap = new YellowstonePathology.Business.Test.ThinPrepPap.ThinPrepPapTest();
			YellowstonePathology.Business.Test.HPV.HPVTest panelSetHPV = new YellowstonePathology.Business.Test.HPV.HPVTest();
			YellowstonePathology.Business.Test.HPV1618.HPV1618Test panelSetHPV1618 = new YellowstonePathology.Business.Test.HPV1618.HPV1618Test();
            YellowstonePathology.Business.Test.NGCT.NGCTTest panelSetNGCT = new YellowstonePathology.Business.Test.NGCT.NGCTTest();
			YellowstonePathology.Business.Test.Trichomonas.TrichomonasTest panelSetTrichomonas = new YellowstonePathology.Business.Test.Trichomonas.TrichomonasTest();

			foreach (YellowstonePathology.Business.Domain.PatientHistoryResult patientHistoryResult in this)
			{
				if (patientHistoryResult.MasterAccessionNo != masterAccessionNo)
				{
					if (DateTime.Compare(patientHistoryResult.AccessionDate, cutoffDate) >= 0)
					{
						if (patientHistoryResult.PanelSetId == panelSetThinPrepPap.PanelSetId ||
							patientHistoryResult.PanelSetId == panelSetHPV.PanelSetId ||
							patientHistoryResult.PanelSetId == panelSetHPV1618.PanelSetId ||
							patientHistoryResult.PanelSetId == panelSetNGCT.PanelSetId ||
							patientHistoryResult.PanelSetId == panelSetTrichomonas.PanelSetId)
						{
							if (result.ContainsMasterAccessionNo(patientHistoryResult.MasterAccessionNo) == false)
							{
								result.Add(patientHistoryResult);
							}
						}
					}
				}
			}
			return result;
		}

		public bool ContainsMasterAccessionNo(string masterAccessionNo)
		{
			bool result = false;
			foreach (YellowstonePathology.Business.Domain.PatientHistoryResult patientHistoryResult in this)
			{
				if (patientHistoryResult.MasterAccessionNo == masterAccessionNo)
				{
					result = true;
					break;
				}
			}
			return result;
		}
    }
}
