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

        public static bool IsDistributionTypeImplemented(int panelSetId, string distributionType)
        {
            bool result = false;
            string resultType = Business.Helper.DistributionTypeMap.ResultTypeFromDistributionType(distributionType);
            if(resultType == string.Empty)
            {
                result = true;
            }
            else
            {
                Business.PanelSet.Model.PanelSet panelSet = Business.PanelSet.Model.PanelSetCollection.GetAll().GetPanelSet(panelSetId);
                if (panelSet.ImplementedResultTypes.Contains(resultType) == true)
                {
                    result = true;
                }
            }

            return result;
        }
    }
}
