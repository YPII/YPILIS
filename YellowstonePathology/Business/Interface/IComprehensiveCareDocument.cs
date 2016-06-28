using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Interface
{
	public interface IComprehensiveCareDocument
	{
		void Publish(int accessionSummaryId, YellowstonePathology.Business.Document.ReportSaveModeEnum reportSaveModeEnum);
		YellowstonePathology.Business.Document.NativeDocumentFormatEnum NativeDocumentFormat { get; set; }
		string FileName { get; }
	}
}
