using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Test
{
    public class ResultType
    {
        public const string WORD = "WORD";
        public const string EPIC = "EPIC";
        public const string CMMC = "CMMC";
        public const string WPH = "WPH";
        public const string ECW = "ECW";
        public const string MDOH = "MDOH";
        public const string WDOH = "WDOH";

<<<<<<< HEAD

=======
        public static bool IsDistributionTypeImplemented(int panelSetId, string distributionType)
        {
            bool result = true;
            Business.PanelSet.Model.PanelSet panelSet = Business.PanelSet.Model.PanelSetCollection.GetAll().GetPanelSet(panelSetId);

            switch (distributionType)
            {
                case YellowstonePathology.Business.ReportDistribution.Model.DistributionType.EPIC:
                    if (panelSet.ImplementedResultTypes.Contains(YellowstonePathology.Business.Test.ResultType.EPIC) == false)
                    {
                        result = false;
                    }
                    break;
                case YellowstonePathology.Business.ReportDistribution.Model.DistributionType.EPICANDFAX:
                    if (panelSet.ImplementedResultTypes.Contains(YellowstonePathology.Business.Test.ResultType.EPIC) == false)
                    {
                        result = false;
                    }
                    break;
                case YellowstonePathology.Business.ReportDistribution.Model.DistributionType.MEDITECH:
                    if (panelSet.ImplementedResultTypes.Contains(YellowstonePathology.Business.Test.ResultType.WPH) == false)
                    {
                        result = false;
                    }
                    break;
                case YellowstonePathology.Business.ReportDistribution.Model.DistributionType.ATHENA:
                    if (panelSet.ImplementedResultTypes.Contains(YellowstonePathology.Business.Test.ResultType.CMMC) == false)
                    {
                        result = false;
                    }
                    break;
                case YellowstonePathology.Business.ReportDistribution.Model.DistributionType.ECW:
                    if (panelSet.ImplementedResultTypes.Contains(YellowstonePathology.Business.Test.ResultType.ECW) == false)
                    {
                        result = false;
                    }
                    break;
                case YellowstonePathology.Business.ReportDistribution.Model.DistributionType.MTDOH:
                    if (panelSet.ImplementedResultTypes.Contains(YellowstonePathology.Business.Test.ResultType.MDOH) == false)
                    {
                        result = false;
                    }
                    break;
                case YellowstonePathology.Business.ReportDistribution.Model.DistributionType.WYDOH:
                    if (panelSet.ImplementedResultTypes.Contains(YellowstonePathology.Business.Test.ResultType.WDOH) == false)
                    {
                        result = false;
                    }
                    break;
            }

            return result;
        }
>>>>>>> 79380102601c0b28d16e94fe4d12fdb67540cf94
    }
}
