using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.IO;
using System.Windows.Xps.Packaging;

namespace YellowstonePathology.Business.Document
{
    public class CaseDocumentCollection : ObservableCollection<CaseDocument>
    {
        string m_ReportNo;

		public CaseDocumentCollection()
        {

        }

		public CaseDocumentCollection(string reportNo)
        {
            this.m_ReportNo = reportNo;
			YellowstonePathology.Business.OrderIdParser orderIdParser = new YellowstonePathology.Business.OrderIdParser(reportNo);
			string filePath = YellowstonePathology.Document.CaseDocumentPath.GetPath(orderIdParser);			
			if (!Directory.Exists(filePath))
			{
				filePath = @"\\CFileServer\Documents";
			}

			string[] files = Directory.GetFiles(filePath);
			foreach (string file in files)
			{
				string[] slashSplit = file.Split('\\');
				string fileOnly = slashSplit[slashSplit.Length - 1];
				if (fileOnly.ToUpper() != "THUMBS.DB")
				{
					CaseDocument item = CaseDocumentFactory.GetCaseDocument(null, file);
					item.SetFileName(file);
					item.CaseDocumentType = "Physical Document";
					this.Add(item);
				}
			}
		}

        public string ReportNo
        {
            get { return this.m_ReportNo; }
        }

		public CaseDocumentCollection(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo) : this(reportNo)
		{
			AccessionOrderCaseDocument accessionOrderCaseDocument = new AccessionOrderCaseDocument(reportNo, accessionOrder.MasterAccessionNo);
			this.Add(accessionOrderCaseDocument);           

			YellowstonePathology.Business.Rules.Surgical.WordSearchList  wordSearchList = new Rules.Surgical.WordSearchList();
			YellowstonePathology.Business.Rules.Surgical.WordSearchListItem wordSearchListItem = new Rules.Surgical.WordSearchListItem("Placenta", true, "");
			wordSearchList.Add(wordSearchListItem);
			YellowstonePathology.Business.Rules.Surgical.WordSearchListItem wordSearchItem = new Rules.Surgical.WordSearchListItem("Placental", true, "");
			wordSearchList.Add(wordSearchItem);
			if (accessionOrder.SpecimenOrderCollection.FindWordsInDescription(wordSearchList) == true)
			{
				PlacentalPathologyCaseDocument placentalPathologyCaseDocument = new PlacentalPathologyCaseDocument(accessionOrder.ClientOrderId);
				this.Add(placentalPathologyCaseDocument);
			}
		}

		public void Accession(string masterAccessionNo)
		{
			foreach (YellowstonePathology.Business.Document.CaseDocument caseDocument in this)
			{
				caseDocument.MasterAccessionNo = masterAccessionNo;
			}
		}

		protected override void InsertItem(int index, CaseDocument item)
		{
			if (string.IsNullOrEmpty(item.DocumentId) == false)
			{
				foreach (CaseDocument caseDocument in this)
				{
					if (caseDocument.DocumentId == item.DocumentId)
					{
						return;
					}
				}
			}
			base.InsertItem(index, item);
		}       

        public CaseDocument GetAccessionOrderDataSheet()
        {
            CaseDocument result = null;
            foreach (CaseDocument document in this)
            {
                if (document.CaseDocumentType == "AccessionOrderDataSheet")
                {
                    result = document;
                }
            }
            return result;
        }

        public CaseDocument GetPatientFaceSheet()
        {
            CaseDocument result = null;
            foreach (CaseDocument item in this)
            {
                if (item.FileName.ToUpper().Contains("PATIENT.TIF") == true)
                {
                    result = item;
                    break;
                }
            }
            return result;
        }		

        public CaseDocument GetFirstRequisition()
        {
            CaseDocument result = null;

			result = GetRequisitionResult("REQ.1.TIF");
			if (result != null) return result;

			result = GetRequisitionResult("REQ.2.TIF");
			if (result != null) return result;

			result = GetRequisitionResult("REQ.3.TIF");
			if (result != null) return result;

			result = GetRequisitionResult("REQ.4.TIF");
			if (result != null) return result;

			result = GetRequisitionResult("1000.TIF");
			if (result != null) return result;			

            result = GetElectronicResult();
            if (result != null) return result;

            return result;
        }

		public bool HasRequisition()
		{
			bool result = false;
			result = ContainsFile("REQ.1.TIF");
			if (result == true) return result;

			result = ContainsFile("REQ.2.TIF");
			if (result == true) return result;

			result = ContainsFile("REQ.3.TIF");
			if (result == true) return result;

			result = ContainsFile("REQ.4.TIF");
			if (result == true) return result;

			result = ContainsFile("1000.TIF");
			if (result == true) return result;

			return result;

		}

		private bool ContainsFile(string fileName)
		{
			bool result = false;
			foreach (YellowstonePathology.Business.Document.CaseDocument caseDocument in this)
			{
				if (caseDocument.FileName.ToUpper().IndexOf(fileName) > 0)
				{
					result = true;
					break;
				}
			}
			return result;
		}

        public CaseDocument GetRequisition()
        {
            CaseDocument result = null;            
            result = GetRequisitionResult("REQ.1.TIF");            
            return result;
        }

		public CaseDocument GetCurrent(string fileName)
		{
			foreach (CaseDocument item in this)
			{
				if(item.FullFileName == fileName)
				{
					return item;
				}
			}
			return null;
		}

		public CaseDocumentCollection GetRequisitions()
		{
			CaseDocumentCollection caseDocumentCollection = new CaseDocumentCollection();
			foreach (CaseDocument item in this)
			{
				if (item.FileName.Contains("REQ") ||
					this.FileIs1000s(item.FileName))
				{
					caseDocumentCollection.Add(item);
				}
			}

			return caseDocumentCollection;
		}

		public CaseDocumentCollection GetPsaFiles(string reportNo, string masterAccessionNo)
		{
			CaseDocumentCollection caseDocumentCollection = new CaseDocumentCollection();
			foreach (CaseDocument item in this)
			{
				if (item.FileName.ToUpper().Contains("REQ") ||                    
					this.FileIs1000s(item.FileName) ||                    
                    item.FileName.ToUpper() == masterAccessionNo.ToUpper() + ".PATIENT.TIF" ||
                    item.FileName.ToUpper() == reportNo.ToUpper() + ".BILLINGDETAILS.XML" ||
					item.FileName.ToUpper() == reportNo.ToUpper() + ".TIF")
				{
					caseDocumentCollection.Add(item);
				}				
			}

			return caseDocumentCollection;
		}

        public CaseDocumentCollection GetBillingDetailFiles(string reportNo)
        {
            CaseDocumentCollection caseDocumentCollection = new CaseDocumentCollection();
            foreach (CaseDocument item in this)
            {
                if (item.FileName.ToUpper().Contains(".BILLINGDETAILS"))
                {
                    caseDocumentCollection.Add(item);
                }
            }

            return caseDocumentCollection;
        }

		private bool FileIs1000s(string fileName)
		{
			bool result = false;
			for (int idx = 0; idx < 10; idx++)
			{
				if(fileName.ToUpper().Contains("." + (1000 + idx).ToString() + ".TIF"))
				{
					result = true;
					break;
				}
			}
			return result;
		}

		private bool FileIsReportPdf(string fullFileName)
		{
			bool result = false;
			string fileName = Path.GetFileName(fullFileName);
			string path = Path.GetDirectoryName(fullFileName);
			int idx = path.LastIndexOf(@"\");
			string reportNo = path.Substring(idx + 1);
			if (fileName.ToUpper() == reportNo.ToUpper() + ".PDF")
			{
				result = true;
			}
			return result;
		}

		private CaseDocument GetRequisitionResult(string searchString)
		{
			CaseDocument result = null;
            foreach (YellowstonePathology.Business.Document.CaseDocument caseDocument in this)
            {
                if (caseDocument.FileName.ToUpper().IndexOf(searchString) > 0)
                {
                    result = caseDocument;
                    break;
                }                    
            }                
            return result;
		}

		private CaseDocument GetElectronicResult()
		{
			CaseDocument result = null;
			foreach (YellowstonePathology.Business.Document.CaseDocument caseDocument in this)
			{
				if (caseDocument.CaseDocumentType == "AccessionOrderDataSheet")
				{
					result = caseDocument;
					break;
				}
			}
			return result;
		}

		public void Update(YellowstonePathology.Business.ClientOrder.Model.ClientOrderMediaCollection clientOrderMediaCollection, string clientOrderId)
		{
			foreach (YellowstonePathology.Business.ClientOrder.Model.ClientOrderMedia clientOrderMedia in clientOrderMediaCollection)
			{
				if (clientOrderMedia.ClientOrderMediaEnum == YellowstonePathology.Business.ClientOrder.Model.ClientOrderMediaEnum.Requisition)
				{
					bool found = false;
					foreach (CaseDocument caseDocument in this)
					{
						if (caseDocument.DocumentId == clientOrderMedia.ContainerId)
						{
							found = true;
							break;
						}
					}
					if (!found)
					{
						CaseDocument caseDocumentToAdd = new CaseDocument();
						caseDocumentToAdd.ClientOrderId = clientOrderId;
						caseDocumentToAdd.DocumentId = clientOrderMedia.ContainerId;
						caseDocumentToAdd.Received = true;
						this.Add(caseDocumentToAdd);
					}
				}
			}
		}

        public int GetNextRequisitionNumber()
        {            
            int maxReqNumber = 0;
            foreach (CaseDocument caseDocument in this)
            {
                if (caseDocument.FileName.Contains("REQ") == true)
                {
                    int reqNumberStartPosition = caseDocument.FileName.IndexOf("REQ") + 4;
                    int thisReqNumber = Convert.ToInt32(caseDocument.FileName.Substring(reqNumberStartPosition, 1));
                    if (thisReqNumber >= maxReqNumber) maxReqNumber = thisReqNumber;
                }
            }            
            return maxReqNumber += 1;
        }

        public CaseDocument GetNextRequisition()
        {
			YellowstonePathology.Business.OrderIdParser orderIdParser = new YellowstonePathology.Business.OrderIdParser(this.m_ReportNo);
			CaseDocument nextRequisition = new CaseDocument();
            nextRequisition.CaseDocumentType = "Physical Document";
			nextRequisition.FullFileName = YellowstonePathology.Document.CaseDocumentPath.GetPath(orderIdParser) + "\\" + this.m_ReportNo + ".REQ." + this.GetNextRequisitionNumber().ToString() + ".TIF";
            return nextRequisition;
        }
	}
}
