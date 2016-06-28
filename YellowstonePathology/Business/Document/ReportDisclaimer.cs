using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Document
{
	public class ReportDisclaimer
	{
		private ReportDisclaimer() { }

        /*
		public static void ToDocumentBody(YellowstonePathology.DocumentCreator.DocumentPart pageBody, string disclaimer)
		{
			YellowstonePathology.DocumentCreator.DocumentRow reportBlankRow = new YellowstonePathology.DocumentCreator.DocumentRow();
			reportBlankRow.Height = 10;
			pageBody.DocumentRowCollection.Add(reportBlankRow);

			YellowstonePathology.DocumentCreator.DocumentTextBlock disclaimerTextBlock = new YellowstonePathology.DocumentCreator.DocumentTextBlock();
			disclaimerTextBlock.Width = 860;
			disclaimerTextBlock.IsMultiLine = true;
			disclaimerTextBlock.TextAlignment = System.Windows.TextAlignment.Left;
			disclaimerTextBlock.FontSize = 8;
			disclaimerTextBlock.VerticalAlignment = System.Windows.VerticalAlignment.Bottom;

			YellowstonePathology.DocumentCreator.TextLineCollection textLines = new DocumentCreator.TextLineCollection();
			textLines.ProcessText(disclaimer, disclaimerTextBlock.FontFamily, disclaimerTextBlock.FontSize, disclaimerTextBlock.Width);

			foreach (string line in textLines)
			{
				YellowstonePathology.DocumentCreator.DocumentRow reportDisclaimerRow = new YellowstonePathology.DocumentCreator.DocumentRow();
				reportDisclaimerRow.Height = 10;

				YellowstonePathology.DocumentCreator.DocumentTextBlock reportDisclaimerTextBlock = new YellowstonePathology.DocumentCreator.DocumentTextBlock();
				reportDisclaimerTextBlock.Width = 860;
				reportDisclaimerTextBlock.IsMultiLine = true;
				reportDisclaimerTextBlock.TextAlignment = System.Windows.TextAlignment.Left;
				reportDisclaimerTextBlock.FontSize = 8;
				reportDisclaimerTextBlock.VerticalAlignment = System.Windows.VerticalAlignment.Bottom;
				reportDisclaimerTextBlock.Text = line;
				reportDisclaimerRow.DocumentTextBlockCollection.Add(reportDisclaimerTextBlock);
				pageBody.DocumentRowCollection.Add(reportDisclaimerRow);
			}
		}
        */
	}
}
