using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Client.Model
{
    public class HPVCompoundStandingOrderRule5 : StandingOrder
    {
        public HPVCompoundStandingOrderRule5()
        {
            this.m_StandingOrderCode = "STNDHPVCMPRL5";
            this.m_Description = "Combines Rule 15 and Rule 16";
            this.m_ReflexOrder = new ReflexOrder();
			this.m_ReflexOrder.PanelSet = new YellowstonePathology.Business.Test.HPV.HPVTest();
        }

        public override bool IsRequired(Business.Test.AccessionOrder accessionOrder)
        {
            bool result = false;
            HPVReflexOrderRule15 hpvReflexOrderRule15 = new HPVReflexOrderRule15();
            if (hpvReflexOrderRule15.IsRequired(accessionOrder) == true)
            {
                result = true;
            }

            HPVReflexOrderRule16 hpvReflexOrderRule16 = new HPVReflexOrderRule16();
            if (hpvReflexOrderRule16.IsRequired(accessionOrder) == true)
            {
                result = true;
            }
            return result;
        }

        public override string ToString()
        {
            StringBuilder result = new StringBuilder();

            result.AppendLine("Compound Rule #5");

            HPVReflexOrderRule15 hpvReflexOrderRule15 = new HPVReflexOrderRule15();
            result.AppendLine("15.) " + hpvReflexOrderRule15.Description);

            HPVReflexOrderRule16 hpvReflexOrderRule16 = new HPVReflexOrderRule16();
            result.AppendLine("16.) " + hpvReflexOrderRule16.Description);

            return result.ToString().TrimEnd();
        }
    }
}
