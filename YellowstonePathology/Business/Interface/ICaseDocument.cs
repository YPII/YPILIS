using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Interface
{
	public interface ICaseDocument
	{
		YellowstonePathology.Business.Rules.MethodResult DeleteCaseFiles(YellowstonePathology.Business.OrderIdParser orderIdParser);
		void Render();
        void Publish();
        YellowstonePathology.Business.Document.NativeDocumentFormatEnum NativeDocumentFormat { get; set; }
	}
}
