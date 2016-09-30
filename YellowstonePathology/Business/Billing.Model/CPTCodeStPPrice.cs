using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Billing.Model
{
    public class CPTCodeStPPrice
    {
        List<CPTCodePrice> m_CPTCodePriceList;

        public CPTCodeStPPrice()
        {
            this.m_CPTCodePriceList = new List<CPTCodePrice>();            
            this.m_CPTCodePriceList.Add(new CPTCodePrice(new CptCodeDefinition.CPT81210(), "YPI", "Client", 262.00));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(new CptCodeDefinition.CPT81261(), "YPI", "Client", 525.00));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(new CptCodeDefinition.CPT81270(), "YPI", "Client", 261.00));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(new CptCodeDefinition.CPT81275(), "YPI", "Client", 645.00));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(new CptCodeDefinition.CPT88184(), "YPI", "Client", 98.00));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(new CptCodeDefinition.CPT88185(), "YPI", "Client", 59.00));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(new CptCodeDefinition.CPT88312(), "YPI", "Client", 79.00));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(new CptCodeDefinition.CPT88368(), "YPI", "Client", 224.00));
        }
    }
}
