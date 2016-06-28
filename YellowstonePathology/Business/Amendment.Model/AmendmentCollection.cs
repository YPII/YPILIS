using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Xml.Serialization;

namespace YellowstonePathology.Business.Amendment.Model
{
	public class AmendmentCollection : ObservableCollection<Amendment>
	{
		public const string PREFIXID = "AM";

		public AmendmentCollection()
        {
        }

		public Amendment GetNextItem(string reportNo, string objectId, string amendmentId)
		{
			Amendment amendment = new Amendment(reportNo, objectId, amendmentId);
			amendment.AmendmentId = amendmentId;
			return amendment;
		}
		
        public Amendment GetAmendment(string amendmentId)
        {
            Amendment amendment = null;
            foreach (Amendment item in this)
            {
                if (item.AmendmentId == amendmentId)
                {
                    amendment = item;
                    break;
                }
            }
            return amendment;
        }

        public bool HasFinalRevisedDiagnosis()
        {
            bool result = false;
            foreach (Amendment item in this)
            {
                if (item.Final == true)
                {
                    if (item.RevisedDiagnosis == true)
                    {
                        result = true;
                        break;
                    }
                }
            }
            return result;
        }

        public bool Exists(string amendmentId)
        {
            bool result = false;
            foreach (Amendment item in this)
            {
                if (item.AmendmentId == amendmentId)
                {
                    result = true;
                    break;                    
                }
            }
            return result;
        }

        public Amendment GetMostRecentAmendment()
        {
            Amendment amendment = null;
            DateTime workingDate = DateTime.Parse("1/1/1900");
            foreach (Amendment item in this)
            {
                if (item.Final == true)
                {
                    if (item.FinalTime >= workingDate)
                    {
                        workingDate = item.FinalTime.Value;
                        amendment = item;                        
                    }
                }
                else
                {
                    if (item.AmendmentTime.Value >= workingDate)
                    {
                        workingDate = item.AmendmentTime.Value;
                        amendment = item;
                    }
                }
            }
            return amendment;
        }

        public Amendment GetMostRecentFinalRevisedDiagnosis()
        {
            Amendment amendment = null;
            DateTime workingDate = DateTime.Parse("1/1/1900");
            foreach (Amendment item in this)
            {
                if (item.Final == true)
                {
                    if (item.RevisedDiagnosis == true)
                    {
                        if (item.FinalTime >= workingDate)
                        {
                            workingDate = item.FinalTime.Value;
                            amendment = item;
                        }
                    }
                }                
            }
            return amendment;
        }

        public bool HasOpenAmendment()
        {
            bool answer = false;
            if (this.Count == 0) return false;

            foreach (Amendment item in this)
            {
                if (item.Final == false)
                {
                    answer = true;
                    break;
                }
            }
            return answer;
        }

		public bool HasAmendmentForReport(string reportNo)
		{
			bool result = false;
			string searchString = "(see YPI report #" + reportNo + ")";
			foreach (Amendment amendment in this)
			{
				if (amendment.Text.Contains(searchString) == true)
				{
					result = true;
					break;
				}
			}
			return result;
		}

        public bool HasAmendmentForReferenceReportNo(string reportNo)
        {
            bool result = false;
            foreach (Amendment amendment in this)
            {
                if (amendment.ReferenceReportNo == reportNo)
                {
                    result = true;
                    break;
                }
            }
            return result;
        }

        public Amendment GetAmendmentForReferenceReportNo(string reportNo)
        {
            Amendment result = null;
            foreach (Amendment amendment in this)
            {
                if (amendment.ReferenceReportNo == reportNo)
                {
                    result = amendment;
                    break;
                }
            }
            return result;
        }
    }
}
