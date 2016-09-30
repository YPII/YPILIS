using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Billing.Model
{
    public class CPTCodeNVPrice
    {
        List<CPTCodePrice> m_CPTCodePriceList;

        public CPTCodeNVPrice()
        {
            this.m_CPTCodePriceList = new List<CPTCodePrice>();            
            this.m_CPTCodePriceList.Add(new CPTCodePrice(new CptCodeDefinition.CPT88184(), "YPI", "Client", 97.72));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(new CptCodeDefinition.CPT88185(), "YPI", "Client", 59.54));
        }
    }
}
