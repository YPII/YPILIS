using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Billing.Model
{
    public class CPTCodeSVHPrice
    {
        List<CPTCodePrice> m_CPTCodePriceList;

        public CPTCodeSVHPrice()
        {
            this.m_CPTCodePriceList = new List<CPTCodePrice>();            
            this.m_CPTCodePriceList.Add(new CPTCodePrice(new CptCodeDefinition.CPT88104(), "YPI", "Client", 38.51));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(new CptCodeDefinition.CPT88112(), "YPI", "Client", 44.01));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(new CptCodeDefinition.CPT88160(), "YPI", "Client", 30.11));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(new CptCodeDefinition.CPT88161(), "YPI", "Client", 31.72));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(new CptCodeDefinition.CPT88173(), "YPI", "Client", 67.61));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(new CptCodeDefinition.CPT88175(), "YPI", "Client", 37.52));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(new CptCodeDefinition.CPT88300(), "YPI", "Client", 22.67));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(new CptCodeDefinition.CPT88302(), "YPI", "Client", 46.92));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(new CptCodeDefinition.CPT88304(), "YPI", "Client", 48.54));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(new CptCodeDefinition.CPT88305(), "YPI", "Client", 66.32));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(new CptCodeDefinition.CPT88307(), "YPI", "Client", 147.81));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(new CptCodeDefinition.CPT88309(), "YPI", "Client", 205.75));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(new CptCodeDefinition.CPT88311(), "YPI", "Client", 6.82));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(new CptCodeDefinition.CPT88312(), "YPI", "Client", 64.38));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(new CptCodeDefinition.CPT88313(), "YPI", "Client", 51.44));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(new CptCodeDefinition.CPT88331(), "YPI", "Client", 32.04));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(new CptCodeDefinition.CPT88332(), "YPI", "Client", 11.03));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(new CptCodeDefinition.CPT88333(), "YPI", "Client", 35.28));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(new CptCodeDefinition.CPT88334(), "YPI", "Client", 22.34));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(new CptCodeDefinition.CPT88342(), "YPI", "Client", 61.47));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(new CptCodeDefinition.CPT88360(), "YPI", "Client", 66.00));
        }
    }
}
