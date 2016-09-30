using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Client.Model
{
    public class HPVCompoundStandingOrderRule3 : StandingOrder
    {
        public HPVCompoundStandingOrderRule3()
        {
            this.m_StandingOrderCode = "STNDHPVCMPRL3";
            this.m_Description = "Combines Rule 5 and Rule 13";
            this.m_ReflexOrder = new ReflexOrder();
			this.m_ReflexOrder.PanelSet = new YellowstonePathology.Business.Test.HPV.HPVTest();
        }

        public override bool IsRequired(Business.Test.AccessionOrder accessionOrder)
        {
            bool result = false;
            HPVReflexOrderRule5 hpvReflexOrderRule5 = new HPVReflexOrderRule5();
            if (hpvReflexOrderRule5.IsRequired(accessionOrder) == true)
            {
                result = true;
            }

            HPVReflexOrderRule13 hpvReflexOrderRule13 = new HPVReflexOrderRule13();
            if (hpvReflexOrderRule13.IsRequired(accessionOrder) == true)
            {
                result = true;
            }
            return result;
        }

        public override string ToString()
        {
            StringBuilder result = new StringBuilder();

            result.AppendLine("Compound Rule #3");

            HPVReflexOrderRule5 hpvReflexOrderRule5 = new HPVReflexOrderRule5();
            result.AppendLine("5.) " + hpvReflexOrderRule5.Description);

            HPVReflexOrderRule13 hpvReflexOrderRule13 = new HPVReflexOrderRule13();
            result.AppendLine("13.) " + hpvReflexOrderRule13.Description);

            return result.ToString().TrimEnd();
        }
    }
}
