using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Document
{
    public sealed class ReportDistributionTable
    {

        private ReportDistributionTable()
        {

        }

        /*
		public static void ToDocumentBody(YellowstonePathology.DocumentCreator.DocumentPart pageBody,
			YellowstonePathology.Business.ReportDistribution.Model.ReportDistributionCollection reportDistributionCollection)
        {
			YellowstonePathology.DocumentCreator.DocumentRow reportDistributionHeaderRow = new YellowstonePathology.DocumentCreator.DocumentRow();
            //reportDistributionHeaderRow.Height = 17;
			reportDistributionHeaderRow.Height = 20;
			YellowstonePathology.DocumentCreator.DocumentTextBlock reportDistributionHeaderTextBlock = new YellowstonePathology.DocumentCreator.DocumentTextBlock();
            reportDistributionHeaderTextBlock.Width = 500;
            reportDistributionHeaderTextBlock.FontWeight = System.Windows.FontWeights.Bold;
            reportDistributionHeaderTextBlock.TextDecorations = System.Windows.TextDecorations.Underline;
            reportDistributionHeaderTextBlock.Text = "Report Distribution";
            reportDistributionHeaderTextBlock.FontSize = 10;

			reportDistributionHeaderTextBlock.Margin = new System.Windows.Thickness(0,5,0,3);

			reportDistributionHeaderRow.DocumentTextBlockCollection.Add(reportDistributionHeaderTextBlock);
			pageBody.DocumentRowCollection.Add(reportDistributionHeaderRow);

            if (reportDistributionCollection.Count != 0)
            {
				foreach (YellowstonePathology.Business.ReportDistribution.Model.ReportDistribution item in reportDistributionCollection)
                {
					YellowstonePathology.DocumentCreator.DocumentRow documentRow = new YellowstonePathology.DocumentCreator.DocumentRow();
                    //documentRow.Height = 17;
					documentRow.Height = 12;
					YellowstonePathology.DocumentCreator.DocumentTextBlock documentTextBlock = new YellowstonePathology.DocumentCreator.DocumentTextBlock();
                    documentTextBlock.Width = 500;
                    documentTextBlock.Text = item.ClientName + " - " + item.PhysicianName;
                    documentTextBlock.FontSize = 10;
					documentRow.DocumentTextBlockCollection.Add(documentTextBlock);
					pageBody.DocumentRowCollection.Add(documentRow);
                }
            }
            else
            {
				YellowstonePathology.DocumentCreator.DocumentRow documentRow = new YellowstonePathology.DocumentCreator.DocumentRow();
				YellowstonePathology.DocumentCreator.DocumentTextBlock documentTextBlock = new YellowstonePathology.DocumentCreator.DocumentTextBlock();
                documentTextBlock.Width = 300;
                documentTextBlock.Text = "None.";
				pageBody.DocumentRowCollection.Add(documentRow);
            }
        }  
        */
    }
}
