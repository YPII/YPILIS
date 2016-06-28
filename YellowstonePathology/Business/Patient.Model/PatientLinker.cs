using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Patient.Model
{
    public class PatientLinker
    {
		private PatientLinkingListItem m_ItemToLink;
		private ObservableCollection<PatientLinkingListItem> m_LinkingList;
		private YellowstonePathology.Business.Validation.ValidationResult m_IsOkToLinkValidationResult;

		public PatientLinker(string masterAccessionNo, string reportNo, string pFirstName, string pLastName, string pMiddleInitial, string pSSN, string patientId, DateTime? pBirthdate)
		{
			this.m_ItemToLink = new PatientLinkingListItem();

			this.m_ItemToLink.MasterAccessionNo = masterAccessionNo;
			this.m_ItemToLink.ReportNo = reportNo;
			this.m_ItemToLink.PFirstName = pFirstName;
			this.m_ItemToLink.PLastName = pLastName;
            this.m_ItemToLink.PMiddleInitial = pMiddleInitial;
			this.m_ItemToLink.PSSN = pSSN;
			this.m_ItemToLink.PatientId = patientId;
			this.m_ItemToLink.PBirthdate = pBirthdate;
			this.m_IsOkToLinkValidationResult = this.m_ItemToLink.IsOkToLink();
		}

		public void GetLinkingList()
		{
			this.m_LinkingList = YellowstonePathology.Business.Gateway.PatientLinkingGateway.GetPatientLinkingList(this.m_ItemToLink);
			this.m_LinkingList.Insert(0, this.m_ItemToLink);
		}

		public ObservableCollection<PatientLinkingListItem> LinkingList
		{
			get { return this.m_LinkingList; }
		}

		public YellowstonePathology.Business.Validation.ValidationResult IsOkToLink
		{
			get { return m_IsOkToLinkValidationResult; }
		}
        
        public void SetMatches()
        {
			foreach (YellowstonePathology.Business.Patient.Model.PatientLinkingListItem item in this.m_LinkingList)
            {
				YellowstonePathology.Business.Patient.Model.BirthdateLastFirstRule birthdateLastFirstRule = new BirthdateLastFirstRule(this.m_ItemToLink, item);
                YellowstonePathology.Business.Patient.Model.LinkingRuleMatchCollection birthdateLastFirstRuleMatchCollection = birthdateLastFirstRule.Match();

                if (birthdateLastFirstRuleMatchCollection.IsMatch() == true)
                {
                    item.IsSelected = true;
                } 
            }
        }

        public PatientLinkingListItem ItemToLink
        {
            get { return this.m_ItemToLink; }
        }

        public void Refresh()
        {
			this.m_LinkingList = YellowstonePathology.Business.Gateway.PatientLinkingGateway.GetPatientLinkingList(this.m_ItemToLink);
			this.m_LinkingList.Insert(0, this.m_ItemToLink);
		}

		public void SetItemsToUnSelected()
		{
			foreach (YellowstonePathology.Business.Patient.Model.PatientLinkingListItem item in this.m_LinkingList)
			{
				item.IsSelected = false;
			}
		}

		public string Link()
		{
			string patientId = this.DetermineId();
			foreach (PatientLinkingListItem item in this.m_LinkingList)
			{
				if (item.IsSelected == true && item.PatientId != patientId)
				{
					item.PatientId = patientId;
					if (item.MasterAccessionNo != this.ItemToLink.MasterAccessionNo)
					{
						YellowstonePathology.Business.Gateway.AccessionOrderGateway.SetPatientId(item.MasterAccessionNo, patientId);
					}
				}
			}
			return patientId;
		}

		private string DetermineId()
		{
			bool needNewId = false;
			string patientId = string.Empty;
			foreach (PatientLinkingListItem item in this.m_LinkingList)
			{
				if (item.IsSelected == true)
				{
					patientId = item.PatientId;
					break;
				}
			}

			if (patientId == null || patientId.Length == 0 || patientId == "0" || Char.IsLetter(patientId[0]))
			{
				needNewId = true;
			}
			else
			{
				foreach (YellowstonePathology.Business.Patient.Model.PatientLinkingListItem item in this.m_LinkingList)
				{
					if (item.IsSelected == true && item.PatientId != null && item.PatientId != patientId && item.PatientId != "0")
					{
						needNewId = true;
						break;
					}
				}
			}

			if (!needNewId)
			{
				foreach (YellowstonePathology.Business.Patient.Model.PatientLinkingListItem item in this.m_LinkingList)
				{
					if (item.IsSelected == false && item.PatientId == patientId)
					{
						needNewId = true;
						break;
					}
				}
			}

			if (needNewId)
			{
				patientId = YellowstonePathology.Business.Gateway.PatientLinkingGateway.GetNewPatientId();
			}

			return patientId;
		}


		public XElement toXml()
		{
			XNamespace xsi = "http://www.w3.org/2001/XMLSchema-instance";

			XElement patientHistoryElement = new XElement("Document",
				new XAttribute(XNamespace.Xmlns + "xsi", xsi),
				new XElement("PatientHistory"));
			foreach (PatientLinkingListItem item in this.m_LinkingList)
			{
				if (item.IsSelected)
				{
					patientHistoryElement.Element("PatientHistory").Add(item.ToXml());
				}
			}
			return patientHistoryElement;
		}

	}    
}
