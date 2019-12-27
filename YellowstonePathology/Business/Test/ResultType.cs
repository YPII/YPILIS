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
        public const string REFLAB = "REFLAB";

        public static bool IsDistributionTypeImplemented(int panelSetId, string distributionType)
        {
            bool result = true;
            Business.PanelSet.Model.PanelSet panelSet = Business.PanelSet.Model.PanelSetCollection.GetAll().GetPanelSet(panelSetId);

            if (distributionType == ReportDistribution.Model.DistributionType.EPIC)
            {
                if (panelSet.ImplementedResultTypes.Contains(YellowstonePathology.Business.Test.ResultType.EPIC) == false)
                {
                    result = false;
                }
            }
            else if (distributionType == ReportDistribution.Model.DistributionType.EPICANDFAX)
            {
                if (panelSet.ImplementedResultTypes.Contains(YellowstonePathology.Business.Test.ResultType.EPIC) == false)
                {
                    result = false;
                }
            }
            else if (distributionType == ReportDistribution.Model.DistributionType.MEDITECH)
            {
                if (panelSet.ImplementedResultTypes.Contains(YellowstonePathology.Business.Test.ResultType.WPH) == false)
                {
                    result = false;
                }
            }
            else if (distributionType == ReportDistribution.Model.DistributionType.ATHENA)
            {
                if (panelSet.ImplementedResultTypes.Contains(YellowstonePathology.Business.Test.ResultType.CMMC) == false)
                {
                    result = false;
                }
            }
            else if (distributionType == ReportDistribution.Model.DistributionType.ECW)
            {
                if (panelSet.ImplementedResultTypes.Contains(YellowstonePathology.Business.Test.ResultType.ECW) == false)
                {
                    result = false;
                }
            }
            else if (distributionType == ReportDistribution.Model.DistributionType.MTDOH)
            {
                if (panelSet.ImplementedResultTypes.Contains(YellowstonePathology.Business.Test.ResultType.MDOH) == false)
                {
                    result = false;
                }
            }
            else if (distributionType == ReportDistribution.Model.DistributionType.WYDOH)
            {
                if (panelSet.ImplementedResultTypes.Contains(YellowstonePathology.Business.Test.ResultType.WDOH) == false)
                {
                    result = false;
                }
            }
            else if (distributionType == ReportDistribution.Model.DistributionType.FAX)
            {
                if (panelSet.ImplementedResultTypes.Contains(YellowstonePathology.Business.Test.ResultType.WORD) == false &&
                    panelSet.ImplementedResultTypes.Contains(YellowstonePathology.Business.Test.ResultType.REFLAB) == false)
                {
                    result = false;
                }
            }
            else if (distributionType == ReportDistribution.Model.DistributionType.MAIL)
            {
                result = true;
            }
            else if (distributionType == ReportDistribution.Model.DistributionType.PRINT)
            {
                result = true;
            }
            else if (distributionType == ReportDistribution.Model.DistributionType.WEBSERVICE)
            {
                result = true;
            }
            else if (distributionType == ReportDistribution.Model.DistributionType.WEBSERVICEANDFAX)
            {
                result = true;
            }
            else if (distributionType == ReportDistribution.Model.DistributionType.DONOTDISTRIBUTE)
            {
                result = true;
            }

            return result;
        }
    }
}
