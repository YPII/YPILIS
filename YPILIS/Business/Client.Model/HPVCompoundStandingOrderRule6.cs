using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Client.Model
{
    public class HPVCompoundStandingOrderRule6 : StandingOrder
    {
        public HPVCompoundStandingOrderRule6()
        {
            this.m_StandingOrderCode = "STNDHPVCMPRL6";
            this.m_Description = "Combines Rule 4 and Rule 10";
            this.m_ReflexOrder = new ReflexOrder();
			this.m_ReflexOrder.PanelSet = new YellowstonePathology.Business.Test.HPV.HPVTest();
        }

        public override bool IsRequired(Business.Test.AccessionOrder accessionOrder)
        {
            bool result = false;
            HPVReflexOrderRule4 hpvReflexOrderRule4 = new HPVReflexOrderRule4();
            if (hpvReflexOrderRule4.IsRequired(accessionOrder) == true)
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

            result.AppendLine("Compound Rule #6");

            HPVReflexOrderRule4 hpvReflexOrderRule4 = new HPVReflexOrderRule4();
            result.AppendLine("1.) " + hpvReflexOrderRule4.Description);

            HPVReflexOrderRule10 hpvReflexOrderRule10 = new HPVReflexOrderRule10();
            result.AppendLine("10.) " + hpvReflexOrderRule10.Description);

            return result.ToString().TrimEnd();
        }
    }
}
