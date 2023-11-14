using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Collections;

namespace YellowstonePathology.UI.Login
{
	public class CaseDocumentViewCollection : ObservableCollection<CaseDocumentView>
	{
        private YellowstonePathology.Business.Document.CaseDocumentCollection m_CaseDocumentCollection;
        private bool m_ShowUnverifiedDocuments;

		public CaseDocumentViewCollection(YellowstonePathology.Business.Document.CaseDocumentCollection caseDocumentCollection, bool showUnverifiedDocuments)
		{
            this.m_CaseDocumentCollection = caseDocumentCollection;
            this.m_ShowUnverifiedDocuments = showUnverifiedDocuments;
            Load();
		}

        private void Load()
        {
            foreach (YellowstonePathology.Business.Document.CaseDocument caseDocument in this.m_CaseDocumentCollection)
            {
                CaseDocumentView caseDocumentView = new CaseDocumentView(caseDocument);
                caseDocumentView.DocumentName = "Document " + (this.Count + 1).ToString();
                caseDocumentView.IsVisible = false;
                this.Add(caseDocumentView);

                if (this.m_ShowUnverifiedDocuments == false)
                {
                    if (caseDocument.Verified == false)
                    {
                        caseDocumentView.IsVisible = false;
                    }
                }                                    
            }
        }

		public void Refresh()
		{
			this.Clear();
            this.Load();
		}		

		public CaseDocumentView GetCaseDocumentViewByDocumentId(string documentId)
		{
			CaseDocumentView result = null;
			foreach (CaseDocumentView caseDocumentView in this)
			{
				if (caseDocumentView.CaseDocument.DocumentId == documentId)
				{
					result = caseDocumentView;
					break;
				}
			}
			return result;
		}

		public void ShowAll()
		{
			foreach (CaseDocumentView caseDocumentView in this)
			{
				caseDocumentView.IsVisible = true;
			}
		}

		public bool HasUnverified()
		{
			bool result = false;
			foreach (CaseDocumentView caseDocumentView in this)
			{
				if (!caseDocumentView.CaseDocument.Verified)
				{
					result = true;
					break;
				}
			}
			return result;
		}

		public void SetMasterAccessionNo(string masterAccessionNo)
		{
			foreach (CaseDocumentView caseDocumentView in this)
			{
				caseDocumentView.CaseDocument.MasterAccessionNo = masterAccessionNo;
			}
		}

		/*public void SaveCaseDocument()
		{
			YellowstonePathology.Business.Gateway.SimpleSubmitter<YellowstonePathology.Business.Document.CaseDocument> simpleSubmitter = new Business.Gateway.SimpleSubmitter<YellowstonePathology.Business.Document.CaseDocument>(this.m_CaseDocumentCollection);
			simpleSubmitter.SubmitChanges(this.m_AccessionOrderGateway);
		}*/
	}
}
