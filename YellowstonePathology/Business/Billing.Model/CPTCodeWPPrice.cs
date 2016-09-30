using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Billing.Model
{
    public class CPTCodeWPPrice
    {
        List<CPTCodePrice> m_CPTCodePriceList;

        public CPTCodeWPPrice()
        {
            this.m_CPTCodePriceList = new List<CPTCodePrice>();            
            this.m_CPTCodePriceList.Add(new CPTCodePrice(new CptCodeDefinition.CPT85055(), "YPI", "Client", 25.64));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(new CptCodeDefinition.CPT86023(), "YPI", "Client", 17.64));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(new CptCodeDefinition.CPT86360(), "YPI", "Client", 66.55));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(new CptCodeDefinition.CPT86361(), "YPI", "Client", 25.64));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(new CptCodeDefinition.CPT87491(), "YPI", "Client", 95.00));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(new CptCodeDefinition.CPT87591(), "YPI", "Client", 95.00));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(new CptCodeDefinition.CPT88104(), "YPI", "Client", 40.58));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(new CptCodeDefinition.CPT88112(), "YPI", "Client", 46.37));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(new CptCodeDefinition.CPT88161(), "YPI", "Client", 33.34));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(new CptCodeDefinition.CPT88172(), "YPI", "Client", 19.14));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(new CptCodeDefinition.CPT88173(), "YPI", "Client", 71.22));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(new CptCodeDefinition.CPT88184(), "YPI", "Client", 82.79));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(new CptCodeDefinition.CPT88185(), "YPI", "Client", 50.11));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(new CptCodeDefinition.CPT88300(), "YPI", "Client", 23.91));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(new CptCodeDefinition.CPT88302(), "YPI", "Client", 49.43));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(new CptCodeDefinition.CPT88304(), "YPI", "Client", 51.14));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(new CptCodeDefinition.CPT88305(), "YPI", "Client", 69.86));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(new CptCodeDefinition.CPT88307(), "YPI", "Client", 155.63));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(new CptCodeDefinition.CPT88309(), "YPI", "Client", 216.72));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(new CptCodeDefinition.CPT88311(), "YPI", "Client", 7.23));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(new CptCodeDefinition.CPT88312(), "YPI", "Client", 67.81));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(new CptCodeDefinition.CPT88313(), "YPI", "Client", 54.20));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(new CptCodeDefinition.CPT88331(), "YPI", "Client", 33.78));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(new CptCodeDefinition.CPT88332(), "YPI", "Client", 11.65));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(new CptCodeDefinition.CPT88342(), "YPI", "Client", 64.75));
                //above code is for non-medicare. Medicare code G0461/G0462 is 64.75
            this.m_CPTCodePriceList.Add(new CPTCodePrice(new CptCodeDefinition.CPT88360(), "YPI", "Client", 69.52));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(new CptCodeDefinition.CPT88368(), "YPI", "Client", 214.00));
        }
    }
}
