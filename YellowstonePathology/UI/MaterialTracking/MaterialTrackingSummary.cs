using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI.MaterialTracking
{
    public class MaterialTrackingSummary
    {
        private string m_MasterAccessionNo;        
        private List<MaterialTrackingSummaryColumn> m_ColumnList;
        private bool m_IsTotal;

        public MaterialTrackingSummary(string masterAccessionNo, bool isTotal)
        {
            this.m_MasterAccessionNo = masterAccessionNo;            
            this.m_IsTotal = isTotal;

            this.m_ColumnList = new List<MaterialTrackingSummaryColumn>();            
        }

        public List<MaterialTrackingSummaryColumn> ColumnList
        {
            get { return this.m_ColumnList; }
        }

        public string MasterAccessionNo
        {
            get { return this.m_MasterAccessionNo; }
        }        

        public bool IsTotal
        {
            get { return this.m_IsTotal; }
        }
    }
}
