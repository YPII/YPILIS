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

        public static void GetImplementedDistributionType(CanSetDistributionResult result)
        {
            switch (result.DistributionTypeToCheck)
            {
                case ReportDistribution.Model.DistributionType.EPIC:
                    {
                        if (result.PanelSet.ImplementedResultTypes.Contains(ResultType.EPIC) == true)
                        {
                            result.DistributionTypeToSet = result.DistributionTypeToCheck;
                            result.CanSetProvidedDistributionType = true;
                        }
                        else
                        {
                            result.DistributionTypeToSet = ReportDistribution.Model.DistributionType.FAX;
                            result.CanSetProvidedDistributionType = false;
                        }
                        break;
                    }
                case ReportDistribution.Model.DistributionType.EPICANDFAX:
                    {
                        if (result.PanelSet.ImplementedResultTypes.Contains(ResultType.EPIC) == true)
                        {
                            result.DistributionTypeToSet = result.DistributionTypeToCheck;
                            result.CanSetProvidedDistributionType = true;
                        }
                        else
                        {
                            result.DistributionTypeToSet = ReportDistribution.Model.DistributionType.FAX;
                            result.CanSetProvidedDistributionType = false;
                        }
                        break;
                    }
                case ReportDistribution.Model.DistributionType.MEDITECH:
                    {
                        if (result.PanelSet.ImplementedResultTypes.Contains(ResultType.WPH) == true)
                        {
                            result.DistributionTypeToSet = result.DistributionTypeToCheck;
                            result.CanSetProvidedDistributionType = true;
                        }
                        else
                        {
                            result.DistributionTypeToSet = ReportDistribution.Model.DistributionType.FAX;
                            result.CanSetProvidedDistributionType = false;
                        }
                        break;
                    }
                case ReportDistribution.Model.DistributionType.ATHENA:
                    {
                        if (result.PanelSet.ImplementedResultTypes.Contains(ResultType.CMMC) == true)
                        {
                            result.DistributionTypeToSet = result.DistributionTypeToCheck;
                            result.CanSetProvidedDistributionType = true;
                        }
                        else
                        {
                            result.DistributionTypeToSet = ReportDistribution.Model.DistributionType.FAX;
                            result.CanSetProvidedDistributionType = false;
                        }
                        break;
                    }
                case ReportDistribution.Model.DistributionType.ECW:
                    {
                        if (result.PanelSet.ImplementedResultTypes.Contains(ResultType.ECW) == true)
                        {
                            result.DistributionTypeToSet = result.DistributionTypeToCheck;
                            result.CanSetProvidedDistributionType = true;
                        }
                        else
                        {
                            result.DistributionTypeToSet = ReportDistribution.Model.DistributionType.FAX;
                            result.CanSetProvidedDistributionType = false;
                        }
                        break;
                    }
                case ReportDistribution.Model.DistributionType.MTDOH:
                    {
                        result.DistributionTypeToSet = result.DistributionTypeToCheck;
                        if (result.PanelSet.ImplementedResultTypes.Contains(ResultType.MDOH) == true)
                        {
                            result.CanSetProvidedDistributionType = true;
                        }
                        else
                        {
                            result.CanSetProvidedDistributionType = true;
                        }
                        break;
                    }
                case ReportDistribution.Model.DistributionType.WYDOH:
                    {
                        result.DistributionTypeToSet = result.DistributionTypeToCheck;
                        if (result.PanelSet.ImplementedResultTypes.Contains(ResultType.WDOH) == true)
                        {
                            result.CanSetProvidedDistributionType = true;
                        }
                        else
                        {
                            result.CanSetProvidedDistributionType = true;
                        }
                        break;
                    }
                case ReportDistribution.Model.DistributionType.FAX:
                    {
                        result.DistributionTypeToSet = result.DistributionTypeToCheck;
                        result.CanSetProvidedDistributionType = true;
                        break;
                    }
                case ReportDistribution.Model.DistributionType.MAIL:
                    {
                        result.DistributionTypeToSet = result.DistributionTypeToCheck;
                        result.CanSetProvidedDistributionType = true;
                        break;
                    }
                case ReportDistribution.Model.DistributionType.PRINT:
                    {
                        result.DistributionTypeToSet = result.DistributionTypeToCheck;
                        result.CanSetProvidedDistributionType = true;
                        break;
                    }
                case ReportDistribution.Model.DistributionType.WEBSERVICE:
                    {
                        result.DistributionTypeToSet = result.DistributionTypeToCheck;
                        result.CanSetProvidedDistributionType = true;
                        break;
                    }
                case ReportDistribution.Model.DistributionType.WEBSERVICEANDFAX:
                    {
                        result.DistributionTypeToSet = result.DistributionTypeToCheck;
                        result.CanSetProvidedDistributionType = true;
                        break;
                    }
                case ReportDistribution.Model.DistributionType.DONOTDISTRIBUTE:
                    {
                        result.DistributionTypeToSet = result.DistributionTypeToCheck;
                        result.CanSetProvidedDistributionType = true;
                        break;
                    }
            }
        }
    }
}
