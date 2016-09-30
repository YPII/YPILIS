using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Rules.Surgical
{
	public class PatientRecentAccessions
	{        
        private string m_PFirstName;
        private string m_PLastName;

		private View.RecentAccessionViewCollection m_RecentAccessionViewCollection;
		private PatientRecentAccessionSelectionEnum m_PatientRecentAccessionSelectionEnum;
		private string m_SelectedMasterAccessionNo;		

		public void GetPatientRecentAccessionsByLastNameFirstName(string pLastName, string pFirstName)
		{
			this.m_PLastName = pLastName;
			this.m_PFirstName = pFirstName;
			GetPatientNameRecentAccessions();
		}

		public bool Exist
		{
			get 
            {
                bool result = false;
                if (this.m_RecentAccessionViewCollection != null)
                {
                    result = this.m_RecentAccessionViewCollection.Count > 0; 
                }
                return result;
            }
		}

		private void GetPatientNameRecentAccessions()
		{
			if (!string.IsNullOrEmpty(this.m_PLastName))
			{
				this.m_RecentAccessionViewCollection = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetRecentAccessionOrders(this.m_PLastName, this.m_PFirstName);
			}
		}

		public View.RecentAccessionViewCollection RecentAccessions
		{
			get { return this.m_RecentAccessionViewCollection; }
		}

		public PatientRecentAccessionSelectionEnum PatientRecentAccessionSelection
		{
			get { return this.m_PatientRecentAccessionSelectionEnum; }
			set { this.m_PatientRecentAccessionSelectionEnum = value; }
		}

		public string SelectedMasterAccessionNo
		{
			get { return this.m_SelectedMasterAccessionNo; }
			set { this.m_SelectedMasterAccessionNo = value; }
		}
	}
}
