using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI.MaterialTracking
{
    public class MaterialTrackingSummary
    {
        private string m_MasterAccessionNo;
        private string m_ClientAccessionNo; 
        private List<MaterialTrackingSummaryColumn> m_ColumnList;
        private bool m_IsTotal;

        public MaterialTrackingSummary(string masterAccessionNo, string clientAccessionNo, bool isTotal)
        {
            this.m_MasterAccessionNo = masterAccessionNo;
            this.m_ClientAccessionNo = clientAccessionNo;
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

        public string ClientAccessionNo
        {
            get { return this.m_ClientAccessionNo; }
        }

        public bool IsTotal
        {
            get { return this.m_IsTotal; }
        }
    }
}
