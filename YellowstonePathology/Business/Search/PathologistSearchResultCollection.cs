using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Search
{
    public class PathologistSearchResultCollection : ObservableCollection<PathologistSearchResult>
    {
        public PathologistSearchResultCollection()
        {

        }

        public bool Exists(int panelSetId)
        {
            bool result = false;
            foreach (PathologistSearchResult psr in this)
            {
                if (psr.PanelSetId == panelSetId)
                {
                    result = true;
                    break;
                }
            }
            return result;
        }

        public PathologistSearchResult Get(int panelSetId)
        {
            PathologistSearchResult result = null;
            foreach (PathologistSearchResult psr in this)
            {
                if (psr.PanelSetId == panelSetId)
                {
                    result = psr;
                    break;
                }
            }
            return result;
        }

        public bool ReportNoExists(string reportNo)
        {
            bool result = false;
            foreach (PathologistSearchResult psr in this)
            {
                if (psr.ReportNo == reportNo)
                {
                    result = true;
                    break;
                }
            }
            return result;
        }

        public bool ReportNoExists(PathologistSearchResult pathologistSearchResult)
        {
            bool result = false;
            foreach (PathologistSearchResult psr in this)
            {
                if (pathologistSearchResult.ReportNo == psr.ReportNo)
                {
                    result = true;
                    break;
                }
            }
            return result;
        }

        public PathologistSearchResult GetPrimaryResult()
        {
            PathologistSearchResult result = null;
            
            bool surgicalFound = false;
            foreach (PathologistSearchResult psr in this)
            {                
                if (psr.PanelSetId == 13)
                {
                    surgicalFound = true;
                    result = psr;
                    break;
                }                
            }
            if (surgicalFound == false)
            {
                if (this.Count != 0) result = this[0];
            }
            
            return result;
        }
    }
}
