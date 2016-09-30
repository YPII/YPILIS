using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Billing.Model
{
    public class CPTCodeGFPrice
    {
        List<CPTCodePrice> m_CPTCodePriceList;

        public CPTCodeGFPrice()
        {
            this.m_CPTCodePriceList = new List<CPTCodePrice>();
            this.m_CPTCodePriceList.Add(new CPTCodePrice(new CptCodeDefinition.CPT85055(), "YPI", "Client", 42.88));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(new CptCodeDefinition.CPT86023(), "YPI", "Client", 19.94));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(new CptCodeDefinition.CPT86360(), "YPI", "Client", 75.21));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(new CptCodeDefinition.CPT87491(), "YPI", "Client", 97.66));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(new CptCodeDefinition.CPT87591(), "YPI", "Client", 97.66));
        }
    }
}
