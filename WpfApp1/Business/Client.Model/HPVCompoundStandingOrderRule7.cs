using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Client.Model
{
    public class HPVCompoundStandingOrderRule7 : StandingOrder
    {
        public HPVCompoundStandingOrderRule7()
        {
            this.m_StandingOrderCode = "STNDHPVCMPRL7";
            this.m_Description = "Combines Rule 3 and Rule 10";
            this.m_ReflexOrder = new ReflexOrder();
			this.m_ReflexOrder.PanelSet = new YellowstonePathology.Business.Test.HPV.HPVTest();
        }

        public override bool IsRequired(Business.Test.AccessionOrder accessionOrder)
        {
            bool result = false;
            HPVReflexOrderRule3 hpvReflexOrderRule3 = new HPVReflexOrderRule3();
            if (hpvReflexOrderRule3.IsRequired(accessionOrder) == true)
            {
                result = true;
            }

            HPVReflexOrderRule10 hpvReflexOrderRule10 = new HPVReflexOrderRule10();
            if (hpvReflexOrderRule10.IsRequired(accessionOrder) == true)
            {
                result = true;
            }
            return result;
        }

        public override string ToString()
        {
            StringBuilder result = new StringBuilder();

            result.AppendLine("Compound Rule #7");

            HPVReflexOrderRule3 hpvReflexOrderRule3 = new HPVReflexOrderRule3();
            result.AppendLine("3.) " + hpvReflexOrderRule3.Description);

            HPVReflexOrderRule10 hpvReflexOrderRule10 = new HPVReflexOrderRule10();
            result.AppendLine("10.) " + hpvReflexOrderRule10.Description);

            return result.ToString().TrimEnd();
        }
    }
}
