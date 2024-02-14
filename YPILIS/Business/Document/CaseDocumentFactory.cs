using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Document
{
	public class CaseDocumentFactory
	{
		public static CaseDocument GetCaseDocument(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string fileName)
		{
			CaseDocument caseDocument = null;
			if(!string.IsNullOrEmpty(fileName))
			{
				string[] slashSplit = fileName.Split('\\');
				string fileOnly = slashSplit[slashSplit.Length - 1];
				string[] dotSplit = fileOnly.Split('.');
				string extension = dotSplit[dotSplit.Length - 1];

				switch (extension.ToUpper())
				{
					case "TIF":
						caseDocument = new TifDocument();
						break;
					case "JPG":
					case "JPEG":
						caseDocument = new JpgDocument();
						break;
					case "DOC":
						caseDocument = new WordDocDocument();
						break;
					case "PDF":
						caseDocument = new PdfDocument();
						break;
					case "XPS":
						caseDocument = new XpsCaseDocument();
						break;
					default:
						caseDocument = new CaseDocument();
						break;
				}
			}

			return caseDocument;
		}
	}
}
