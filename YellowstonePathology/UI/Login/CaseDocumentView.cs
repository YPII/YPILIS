using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace YellowstonePathology.UI.Login
{
	public class CaseDocumentView : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		private string m_DocumentName;
		private YellowstonePathology.Business.Document.CaseDocument m_CaseDocument;
		private bool m_IsVisible;

		public CaseDocumentView(YellowstonePathology.Business.Document.CaseDocument caseDocument)
		{
			this.m_CaseDocument = caseDocument;
		}

		public YellowstonePathology.Business.Document.CaseDocument CaseDocument
		{
			get { return this.m_CaseDocument; }
			set { this.m_CaseDocument = value; }
		}

		public string DocumentName
		{
			get { return this.m_DocumentName; }
			set { this.m_DocumentName = value; }
		}

		public string DisplayStatus
		{
			get
			{
				string result = string.Empty;
				if (this.CaseDocument.Received)
				{
					result = "Received";
				}
				if (this.CaseDocument.Verified)
				{
					result = "Verified";
				}
				return result;
			}
		}

		public bool IsVisible
		{
			get { return this.m_IsVisible; }
			set
			{
				if (this.m_IsVisible != value)
				{
					this.m_IsVisible = value;
					this.NotifyPropertyChanged("IsVisible");
				}
			}
		}

		public void NotifyPropertyChanged(String info)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(info));
			}
		}
	}
}
