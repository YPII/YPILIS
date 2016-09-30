using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI.ReportOrder
{
    public class ReportOrderDialogFactory
    {
        public static void ShowDialog(YellowstonePathology.Business.Test.PanelSetOrder reportOrder)
        {
            if (reportOrder != null)
            {
                switch (reportOrder.PanelSetId)
                {
                    case 51: //Fish Analysis
						YellowstonePathology.Business.Test.FishAnalysis.FishAnalysisTestOrder reportOrderFishAnalysis = (YellowstonePathology.Business.Test.FishAnalysis.FishAnalysisTestOrder)reportOrder;
                        YellowstonePathology.UI.ReportOrder.ReportOrderFishAnalysisDialog reportOrderFishDialog = new ReportOrderFishAnalysisDialog(reportOrderFishAnalysis);
                        reportOrderFishDialog.ShowDialog();
                        break;
                    case 52: //Molecular Analysis
						YellowstonePathology.Business.Test.MolecularAnalysis.MolecularAnalysisTestOrder reportOrderMolecularAnalysis = (YellowstonePathology.Business.Test.MolecularAnalysis.MolecularAnalysisTestOrder)reportOrder;
                        YellowstonePathology.UI.ReportOrder.ReportOrderMolecularAnalysisDialog reportOrderMolecularDialog = new ReportOrderMolecularAnalysisDialog(reportOrderMolecularAnalysis);
                        reportOrderMolecularDialog.ShowDialog();
                        break;
                    case 53: //Absolute CD4 Count
						YellowstonePathology.Business.Test.AbsoluteCD4Count.AbsoluteCD4CountTestOrder reportOrderAbsolutCD4Count = (YellowstonePathology.Business.Test.AbsoluteCD4Count.AbsoluteCD4CountTestOrder)reportOrder;
                        YellowstonePathology.UI.ReportOrder.ReportOrderAbsoluteCD4CountDialog reportOrderAbsolutCD4CountDialog = new ReportOrderAbsoluteCD4CountDialog(reportOrderAbsolutCD4Count);
                        reportOrderAbsolutCD4CountDialog.ShowDialog();
                        break;
                }
            }
        }
    }
}
