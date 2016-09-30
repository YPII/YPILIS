using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Xml.Serialization;

namespace YellowstonePathology.Business.Billing.Model
{
	public class CptBillingCodeItemCollection : ObservableCollection<CptBillingCodeItem>
	{
		public const string PREFIXID = "CPT";

		private string m_ReportNo = string.Empty;

		public CptBillingCodeItemCollection()
        {

		}

		public CptBillingCodeItem GetNextItem(string masterAccessionNo, string specimenOrderId, string reportNo, string surgicalSpecimenId, int panelSetId,
			string testOrderId, string testName, int clientId, string primaryInsurance, string secondaryInsurance,
			string patientType, string cptCode, int cptCodeQuantity, DateTime? dateOfService, string professionalComponentFacilityId, string technicalComponentFacilityid)
		{
			CptBillingCodeItem cptBillingCodeItem = new CptBillingCodeItem();
			cptBillingCodeItem.CptBillingId = this.GetNextId();
			cptBillingCodeItem.ObjectId = cptBillingCodeItem.CptBillingId;
			cptBillingCodeItem.SurgicalSpecimenId = string.IsNullOrEmpty(surgicalSpecimenId) ? "0" : surgicalSpecimenId;
			cptBillingCodeItem.MasterAccessionNo = masterAccessionNo;
			cptBillingCodeItem.SpecimenOrderId = specimenOrderId;
			cptBillingCodeItem.ReportNo = reportNo;
			cptBillingCodeItem.ClientId = clientId;
			cptBillingCodeItem.PrimaryInsurance = primaryInsurance;
			cptBillingCodeItem.SecondaryInsurance = secondaryInsurance;
			cptBillingCodeItem.PatientType = patientType;			
			cptBillingCodeItem.PanelSetId = panelSetId;
			cptBillingCodeItem.TestOrderId = string.IsNullOrEmpty(testOrderId) ? "0" : testOrderId;
			cptBillingCodeItem.Description = testName;
			cptBillingCodeItem.CptCode = cptCode;
			cptBillingCodeItem.Quantity = cptCodeQuantity;
			cptBillingCodeItem.DateOfService = dateOfService;
            cptBillingCodeItem.ProfessionalComponentFacilityId = professionalComponentFacilityId;
            cptBillingCodeItem.TechnicalComponentFacilityId = technicalComponentFacilityid;
			return cptBillingCodeItem;
		}

		public string GetNextId()
		{
			string result = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
			return result;			
		}

        public void ReplaceCode(string codeToReplace, string newCode)
        {
            foreach (CptBillingCodeItem cptBillingCode in this)
            {
                if (cptBillingCode.CptCode.ToUpper() == codeToReplace.ToUpper())
                {
                    cptBillingCode.CptCode = newCode;
                }
            }
        }

		public bool CodeExists(string cptCode)
        {
            bool result = false;
            foreach (CptBillingCodeItem cptBillingCodeItem in this)
            {
                if (cptBillingCodeItem.CptCode == cptCode)
                {
                    result = true;
                    break;
                }
            }
            return result;
        }

		public void SetCaseAsNoCharge(string reportNo)
		{
			if (m_ReportNo != reportNo)
			{
				throw new Exception("Accession Number mismatch. SetCaseAsNoCharge not available");
			}

			foreach (CptBillingCodeItem cptBillingCodeItem in this)
			{
				cptBillingCodeItem.NoCharge = !cptBillingCodeItem.NoCharge;
			}
		}

        public void LoadFromAccessionOrder(CptBillingCodeItemCollection cptBillingCodeCollection, string reportNo)
        {
            foreach (CptBillingCodeItem cptBillingCode in cptBillingCodeCollection)
            {
                if (cptBillingCode.ReportNo == reportNo)
                {
                    this.Add(cptBillingCode);
                }
            }
        }

		public void UpdateFromAccession(string patientType, string primaryInsurance, string secondaryInsurance, string professionalComponentFacilityId, string technicalComponentFacilityId, int clientId)
		{
			int cnt = this.Count;
			for (int idx = 0; idx < cnt; idx++) 
			{
				CptBillingCodeItem cptBillingCodeItem = this[idx];
				if (cptBillingCodeItem.MatchesAccession(patientType, primaryInsurance, secondaryInsurance, professionalComponentFacilityId, technicalComponentFacilityId, clientId) == false)
				{
					if (cptBillingCodeItem.IsOktoUpdate() == true)
					{
						cptBillingCodeItem.UpdateFromAccession(patientType, primaryInsurance, secondaryInsurance, professionalComponentFacilityId, technicalComponentFacilityId, clientId);
					}
					else
					{
                        System.Windows.MessageBoxResult result = System.Windows.MessageBox.Show("Would you like to reverse these changes.", "Reverse", System.Windows.MessageBoxButton.YesNo);
                        if (result == System.Windows.MessageBoxResult.Yes)
                        {
							string cloneId = this.GetNextId();
                            CptBillingCodeItem newCptBillingCodeItem = cptBillingCodeItem.CloneForReversal(cloneId);
                            newCptBillingCodeItem.Locked = false;
                            newCptBillingCodeItem.BillingType = null;
							newCptBillingCodeItem.UpdateFromAccession(patientType, primaryInsurance, secondaryInsurance, professionalComponentFacilityId, technicalComponentFacilityId, clientId);
                            this.Reverse(cptBillingCodeItem, newCptBillingCodeItem);
                            cptBillingCodeItem.Locked = true;
                        }
                        else
                        {
							cptBillingCodeItem.UpdateFromAccession(patientType, primaryInsurance, secondaryInsurance, professionalComponentFacilityId, technicalComponentFacilityId, clientId);
                        }
					}
				}
			}
		}        

		/*public void UpdateFromAccession(string patientType, string primaryInsurance, string secondaryInsurance, string professionalComponentFacilityId, string technicalComponentFacility, int clientId, YellowstonePathology.Business.Persistence.ObjectTracker objectTracker)
		{
			int cnt = this.Count;
			for (int idx = 0; idx < cnt; idx++) 
			{
				CptBillingCodeItem cptBillingCodeItem = this[idx];
				if (cptBillingCodeItem.MatchesAccession(patientType, primaryInsurance, secondaryInsurance, professionalComponentFacilityId, technicalComponentFacility, clientId) == false)
				{
					if (cptBillingCodeItem.IsOktoUpdate() == true)
					{
						cptBillingCodeItem.UpdateFromAccession(patientType, primaryInsurance, secondaryInsurance, professionalComponentFacilityId, technicalComponentFacility, clientId);
					}
					else
					{
                        System.Windows.MessageBoxResult result = System.Windows.MessageBox.Show("Would you like to reverse these changes.", "Reverse", System.Windows.MessageBoxButton.YesNo);
                        if (result == System.Windows.MessageBoxResult.Yes)
                        {
							string cloneId = this.GetNextId(cptBillingCodeItem.MasterAccessionNo);
                            CptBillingCodeItem newCptBillingCodeItem = cptBillingCodeItem.CloneForReversal(cloneId);
                            newCptBillingCodeItem.Locked = false;
                            newCptBillingCodeItem.BillingType = null;
							newCptBillingCodeItem.UpdateFromAccession(patientType, primaryInsurance, secondaryInsurance, professionalComponentFacilityId, technicalComponentFacility, clientId);
                            this.Reverse(cptBillingCodeItem, newCptBillingCodeItem, objectTracker);
                            cptBillingCodeItem.Locked = true;
                        }
                        else
                        {
							cptBillingCodeItem.UpdateFromAccession(patientType, primaryInsurance, secondaryInsurance, technicalComponentFacility, professionalComponentFacilityId, clientId);
                        }
					}
				}
			}
		}*/

		public void Reverse(CptBillingCodeItem cptBillingCodeToReverse)
		{
			string cloneId = this.GetNextId();
			CptBillingCodeItem reversedCptBillingCodeItem = cptBillingCodeToReverse.CloneForReversal(cloneId);
			reversedCptBillingCodeItem.Locked = true;
			reversedCptBillingCodeItem.Quantity = reversedCptBillingCodeItem.Quantity * -1;
			reversedCptBillingCodeItem.Reversed = true;
			cptBillingCodeToReverse.Reversed = true;
			this.Add(reversedCptBillingCodeItem);
		}

		public void Reverse(CptBillingCodeItem cptBillingCodeToReverse, CptBillingCodeItem newCptBillingCode)
		{
			this.Reverse(cptBillingCodeToReverse);
			this.Add(newCptBillingCode);
		}		

		public void SetPostDischarge()
		{
			foreach (CptBillingCodeItem cptBillingCodeItem in this)
			{
				cptBillingCodeItem.Ordered14DaysPostDischarge = !cptBillingCodeItem.Ordered14DaysPostDischarge;
			}
		}

        public bool DoesCollectionHaveCodes(List<string> cptCodeList)
        {
            bool result = false;
            foreach (CptBillingCodeItem item in this)
            {
                foreach (string cptCode in cptCodeList)
                {
                    if (item.CptCode == cptCode)
                    {
                        result = true;
                        break;
                    }
                }
            }
            return result;
        }

        public CptBillingCodeItemCollection GetCollectionBySurgicalSpecimenId(string surgicalSpecimenId)
        {
            CptBillingCodeItemCollection result = new CptBillingCodeItemCollection();
            foreach (CptBillingCodeItem item in this)
            {
                if (item.SurgicalSpecimenId == surgicalSpecimenId)
                {
                    result.Add(item);
                }
            }
            return result;
        }

        public CptBillingCodeItemCollection GetCollectionByTestOrderId(string testOrderId)
        {
            CptBillingCodeItemCollection result = new CptBillingCodeItemCollection();
            foreach (CptBillingCodeItem item in this)
            {
                if (item.TestOrderId == testOrderId)
                {
                    result.Add(item);
                }
            }
            return result;
        }

        public CptBillingCodeItemCollection GetCollectionByReportNo(string reportNo)
        {
            CptBillingCodeItemCollection result = new CptBillingCodeItemCollection();
            foreach (CptBillingCodeItem item in this)
            {
                if (item.ReportNo == reportNo)
                {
                    result.Add(item);
                }
            }
            return result;
        }

        public CptBillingCodeItem GetCptCodeItem(string cptBillingCodeId)
        {
            foreach (CptBillingCodeItem item in this)
            {
                if (item.CptBillingId == cptBillingCodeId)
                {
                    return item;
                }
            }
            return null;
        }

		public CptBillingCodeItem GetCurrent()
		{
			return this.Count > 0 ? this[0] : null;
		}

		public CptBillingCodeItem GetCurrent(string cptBillingCodeId)
		{
			foreach (CptBillingCodeItem item in this)
			{
				if (item.CptBillingId == cptBillingCodeId)
				{
					return item;
				}
			}
			return null;
		}

		public void Bill()
		{
			foreach (CptBillingCodeItem cptBillingCodeItem in this)
			{
				//if (cptBillingCodeItem.Audited == false)
				//{

					cptBillingCodeItem.Audited = true;
					if (cptBillingCodeItem.BillingDate.HasValue == false)
					{
						cptBillingCodeItem.BillingDate = DateTime.Today;
					}

				//}
			}
		}

		public void Add(int testId, CptBillingCodeItem cptBillingCodeItem)
		{
			YellowstonePathology.Business.Test.Model.StainTest stainTest = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetStainTestByTestId(testId);
			if (stainTest != null)
			{
                cptBillingCodeItem.CptCode = stainTest.CptCode;
                cptBillingCodeItem.Quantity = stainTest.CptCodeQuantity;
				this.Add(cptBillingCodeItem);
			}
		}

		public void Remove(object obj)
		{
			CptBillingCodeItem item = obj as CptBillingCodeItem;
			if (item != null)
			{
				base.Remove(item);
			}			

			YellowstonePathology.Business.Test.Model.TestOrder testOrder = obj as YellowstonePathology.Business.Test.Model.TestOrder;
			if (testOrder != null)
			{
				foreach (CptBillingCodeItem cptBillingCodeItem in this)
				{
					if (cptBillingCodeItem.TestOrderId == testOrder.TestOrderId)
					{
						base.Remove(cptBillingCodeItem);
						break;
					}
				}
			}
		}

        public bool Exists(string cptCode, string reportNo)
        {
            bool result = false;
            foreach (CptBillingCodeItem cptBillingCode in this)
            {
                if (cptBillingCode.CptCode == cptCode && cptBillingCode.ReportNo == reportNo)
                {
                    result = true;
                    break;
                }
            }
            return result;
        }

        public void ClearCodes(string reportNo)
        {
            for (int i = 0; i < this.Count; i++)
            {
                if (this[i].ReportNo == reportNo)
                {
                    this.Remove(this[i]);
                    i--;
                }
            }
        }

        public bool CodeExistsWithTestOrderId(string cptCode, string testOrderId)
        {
            bool result = false;
            foreach (CptBillingCodeItem cptBillingCode in this)
            {
                if (cptBillingCode.TestOrderId == testOrderId)
                {
                    result = true;
                    break;
                }
            }
            return result;
        }
        
		public void SetFlowCptCodes(Test.AccessionOrder accessionOrder, string reportNo)
		{
			Test.LLP.PanelSetOrderLeukemiaLymphoma panelSetOrder = (Test.LLP.PanelSetOrderLeukemiaLymphoma)accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(reportNo);

			string primaryInsurance = accessionOrder.PrimaryInsurance;
			string secondaryInsurance = accessionOrder.SecondaryInsurance;
			string patientType = accessionOrder.PatientType;
			int clientId = accessionOrder.ClientId;
			string masterAccessionNo = accessionOrder.MasterAccessionNo;
			string specimenOrderId = accessionOrder.SpecimenOrderCollection[0].SpecimenOrderId;
			int panelSetId = panelSetOrder.PanelSetId;
			DateTime? dateOfService = accessionOrder.CollectionDate;
			bool noCharge = panelSetOrder.NoCharge;
			string professionalComponentFacilityid = panelSetOrder.ProfessionalComponentFacilityId;
            string technicalComponentFacilityid = panelSetOrder.TechnicalComponentFacilityId;
						
			switch (panelSetOrder.PanelSetId)
			{
				case 1:
                    this.AddFlowCptCodeIfNotExist(reportNo, "88184", 1, primaryInsurance, secondaryInsurance, patientType,
                        clientId, masterAccessionNo, specimenOrderId, panelSetId, dateOfService, noCharge, professionalComponentFacilityid, technicalComponentFacilityid);                        

					int markerCount = panelSetOrder.FlowMarkerCollection.CountOfUsedMarkers();

                    this.AddFlowCptCodeIfNotExist(reportNo, "88185", markerCount - 1, primaryInsurance, secondaryInsurance, patientType,
                        clientId, masterAccessionNo, specimenOrderId, panelSetId, dateOfService, noCharge, professionalComponentFacilityid, technicalComponentFacilityid);                        

					if (markerCount >= 2 && markerCount <= 8)
					{
                        this.AddFlowCptCodeIfNotExist(reportNo, "88187", 1, primaryInsurance, secondaryInsurance, patientType,
                            clientId, masterAccessionNo, specimenOrderId, panelSetId, dateOfService, noCharge, professionalComponentFacilityid, technicalComponentFacilityid);                        
					}

					if (markerCount >= 9 && markerCount <= 15)
					{
                        this.AddFlowCptCodeIfNotExist(reportNo, "88188", 1, primaryInsurance, secondaryInsurance, patientType,
                            clientId, masterAccessionNo, specimenOrderId, panelSetId, dateOfService, noCharge, professionalComponentFacilityid, technicalComponentFacilityid);                        
					}

					if (markerCount >= 16)
					{
                        this.AddFlowCptCodeIfNotExist(reportNo, "88189", 1, primaryInsurance, secondaryInsurance, patientType,
                            clientId, masterAccessionNo, specimenOrderId, panelSetId, dateOfService, noCharge, professionalComponentFacilityid, technicalComponentFacilityid);                        
					}
					break;
				case 4:
                    this.AddFlowCptCodeIfNotExist(reportNo, "86023", 2, primaryInsurance, secondaryInsurance, patientType,
                        clientId, masterAccessionNo, specimenOrderId, panelSetId, dateOfService, noCharge, professionalComponentFacilityid, technicalComponentFacilityid);                        
					break;
				case 5:
                    this.AddFlowCptCodeIfNotExist(reportNo, "85055", 1, primaryInsurance, secondaryInsurance, patientType,
                        clientId, masterAccessionNo, specimenOrderId, panelSetId, dateOfService, noCharge, professionalComponentFacilityid, technicalComponentFacilityid);                        
					break;
				case 7:
                    this.AddFlowCptCodeIfNotExist(reportNo, "86356", 1, primaryInsurance, secondaryInsurance, patientType,
                        clientId, masterAccessionNo, specimenOrderId, panelSetId, dateOfService, noCharge, professionalComponentFacilityid, technicalComponentFacilityid);                        
					break;
				case 12:
                    this.AddFlowCptCodeIfNotExist(reportNo, "86367", 1, primaryInsurance, secondaryInsurance, patientType,
                        clientId, masterAccessionNo, specimenOrderId, panelSetId, dateOfService, noCharge, professionalComponentFacilityid, technicalComponentFacilityid);                        
					break;
				case 13:
                    this.AddFlowCptCodeIfNotExist(reportNo, "86023", 2, primaryInsurance, secondaryInsurance, patientType,
                        clientId, masterAccessionNo, specimenOrderId, panelSetId, dateOfService, noCharge, professionalComponentFacilityid, technicalComponentFacilityid);                        

                    this.AddFlowCptCodeIfNotExist(reportNo, "85055", 1, primaryInsurance, secondaryInsurance, patientType,
                        clientId, masterAccessionNo, specimenOrderId, panelSetId, dateOfService, noCharge, professionalComponentFacilityid, technicalComponentFacilityid);                        
					break;
				case 14:
                    this.AddFlowCptCodeIfNotExist(reportNo, "88182", 1, primaryInsurance, secondaryInsurance, patientType,
                        clientId, masterAccessionNo, specimenOrderId, panelSetId, dateOfService, noCharge, professionalComponentFacilityid, technicalComponentFacilityid);                        
					break;
				case 15:
                    this.AddFlowCptCodeIfNotExist(reportNo, "86361", 1, primaryInsurance, secondaryInsurance, patientType,
                        clientId, masterAccessionNo, specimenOrderId, panelSetId, dateOfService, noCharge, professionalComponentFacilityid, technicalComponentFacilityid);                        
					break;
				case 16:
                    this.AddFlowCptCodeIfNotExist(reportNo, "86812", 1, primaryInsurance, secondaryInsurance, patientType,
                        clientId, masterAccessionNo, specimenOrderId, panelSetId, dateOfService, noCharge, professionalComponentFacilityid, technicalComponentFacilityid);                        
					break;
			}			
		}
       
       
		private void AddFlowCptCodeIfNotExist(string reportNo, string cptCode, int quantity, string primaryInsurance, string secondaryInsurance,
			string patientType, int clientId, string masterAccessionNo, string specimenOrderId, int panelSetId, DateTime? dateOfService,
			bool noCharge, string professionalComponentFacilityId, string technicalComponentFacilityId)
		{
            if (this.Exists(cptCode, reportNo) == false)
            {
                CptBillingCodeItem cptBillingCodeItem = this.GetNextItem(masterAccessionNo, specimenOrderId, reportNo, string.Empty, panelSetId, string.Empty, string.Empty,
                    clientId, primaryInsurance, secondaryInsurance, patientType, cptCode, quantity, dateOfService, professionalComponentFacilityId, technicalComponentFacilityId);
                cptBillingCodeItem.NoCharge = noCharge;
                this.Add(cptBillingCodeItem);
            }
		}

		public bool DoesCodeExistForSpecimenOrder(string cptCode, string specimenOrderId)
		{
			bool result = false;
			foreach (CptBillingCodeItem cptBillingCode in this)
			{
				if (cptBillingCode.SpecimenOrderId == specimenOrderId)
				{
					if (cptBillingCode.CptCode == cptCode)
					{
						result = true;
						break;
					}
				}
			}
			return result;
		}

		public bool DoesCodeExistForSpecimenProcedure(List<string> testOrderIds)
		{
			bool result = false;
			foreach (string testOrderId in testOrderIds)
			{
				foreach (CptBillingCodeItem cptBillingCode in this)
				{
					if (cptBillingCode.TestOrderId == testOrderId)
					{
						result = true;
						break;
					}
				}
			}
			return result;
		}

		public DateTime? GetEarliestBillingDate(string reportNo)
		{
			DateTime? result = null;
			foreach (CptBillingCodeItem cptBillingCode in this)
			{
				if (cptBillingCode.ReportNo == reportNo && cptBillingCode.BillingDate.HasValue)
				{
					result = cptBillingCode.BillingDate.Value;
					break;
				}
			}

			if (result.HasValue)
			{
				foreach (CptBillingCodeItem cptBillingCode in this)
				{
                    if (cptBillingCode.ReportNo == reportNo && 
                        cptBillingCode.BillingDate.HasValue && 
                        cptBillingCode.BillingDate.Value.CompareTo(result.Value) < 0)
                    {
                        result = cptBillingCode.BillingDate.Value;
                    }
				}
			}

			return result;
		}

        public Nullable<DateTime> GetReversalDate(string reportNo)
        {
            Nullable<DateTime> result = null;
            foreach (CptBillingCodeItem cptBillingCode in this)
            {
                if (cptBillingCode.Reversed == true && cptBillingCode.Quantity < 0)
                {
                    result = cptBillingCode.BillingDate;
                    break;
                }
            }
            return result;
        }
	}
}
