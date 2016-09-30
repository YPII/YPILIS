using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Client.Model
{
    public class HPVCompoundStandingOrderRule2 : StandingOrder
    {
        public HPVCompoundStandingOrderRule2()
        {
            this.m_StandingOrderCode = "STNDHPVCMPRL2";
            this.m_Description = "Combines Rule 4 and Rule 13";
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

            result.AppendLine("Compound Rule #2");

            HPVReflexOrderRule4 hpvReflexOrderRule4 = new HPVReflexOrderRule4();
            result.AppendLine("4.) " + hpvReflexOrderRule4.Description);

            HPVReflexOrderRule13 hpvReflexOrderRule13 = new HPVReflexOrderRule13();
            result.AppendLine("13.) " + hpvReflexOrderRule13.Description);

            return result.ToString().TrimEnd();
        }
    }
}
