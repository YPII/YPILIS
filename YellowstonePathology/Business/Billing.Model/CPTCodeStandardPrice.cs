using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Billing.Model
{
    public class CPTCodeStandardPrice
    {
        List<CPTCodePrice> m_CPTCodePriceList;

        public CPTCodeStandardPrice()
        {
            this.m_CPTCodePriceList = new List<CPTCodePrice>();            
            this.m_CPTCodePriceList.Add(new CPTCodePrice(new CptCodeDefinition.CPT81210(), "YPI", "Patient", 238.00));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(new CptCodeDefinition.CPT81220(), "YPI", "Patient", 829.00));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(new CptCodeDefinition.CPT81235(), "YPI", "Patient", 720.00));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(new CptCodeDefinition.CPT81240(), "YPI", "Patient", 191.00));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(new CptCodeDefinition.CPT81241(), "YPI", "Patient", 191.00));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(new CptCodeDefinition.CPT81261(), "YPI", "Patient", 477.00));
            //above price is for paraffin. same code but for non-paraffin specimens is 467.00
            this.m_CPTCodePriceList.Add(new CPTCodePrice(new CptCodeDefinition.CPT81270(), "YPI", "Patient", 237.00));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(new CptCodeDefinition.CPT81275(), "YPI", "Patient", 586.00));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(new CptCodeDefinition.CPT81479(), "YPI", "Patient", 135.00));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(new CptCodeDefinition.CPT83890(), "YPI", "Patient", 5.68));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(new CptCodeDefinition.CPT83891(), "YPI", "Patient", 5.68));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(new CptCodeDefinition.CPT83892(), "YPI", "Patient", 5.68));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(new CptCodeDefinition.CPT83896(), "YPI", "Patient", 5.68));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(new CptCodeDefinition.CPT83898(), "YPI", "Patient", 23.74));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(new CptCodeDefinition.CPT83900(), "YPI", "Patient", 47.48));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(new CptCodeDefinition.CPT83901(), "YPI", "Patient", 23.74));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(new CptCodeDefinition.CPT83902(), "YPI", "Patient", 23.74));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(new CptCodeDefinition.CPT83907(), "YPI", "Patient", 18.92));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(new CptCodeDefinition.CPT83909(), "YPI", "Patient", 23.74));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(new CptCodeDefinition.CPT83914(), "YPI", "Patient", 23.74));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(new CptCodeDefinition.CPT85055(), "YPI", "Patient", 37.92));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(new CptCodeDefinition.CPT86023(), "YPI", "Patient", 17.64));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(new CptCodeDefinition.CPT86356(), "YPI", "Patient", 37.92));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(new CptCodeDefinition.CPT86360(), "YPI", "Patient", 73.21));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(new CptCodeDefinition.CPT86367(), "YPI", "Patient", 53.43));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(new CptCodeDefinition.CPT87169(), "YPI", "Patient", 5.87));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(new CptCodeDefinition.CPT87491(), "YPI", "Patient", 95.00));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(new CptCodeDefinition.CPT87591(), "YPI", "Patient", 95.00));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(new CptCodeDefinition.CPT87621(), "YPI", "Patient", 49.71));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(new CptCodeDefinition.CPT87798(), "YPI", "Patient", 95.00));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(new CptCodeDefinition.CPT88104(), "YPI", "Patient", 40.54));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(new CptCodeDefinition.CPT88112(), "YPI", "Patient", 46.33));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(new CptCodeDefinition.CPT88142(), "YPI", "Patient", 28.70));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(new CptCodeDefinition.CPT88160(), "YPI", "Patient", 31.69));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(new CptCodeDefinition.CPT88161(), "YPI", "Patient", 33.39));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(new CptCodeDefinition.CPT88164(), "YPI", "Patient", 14.97));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(new CptCodeDefinition.CPT88172(), "YPI", "Patient", 19.10));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(new CptCodeDefinition.CPT88173(), "YPI", "Patient", 71.17));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(new CptCodeDefinition.CPT88175(), "YPI", "Patient", 37.52));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(new CptCodeDefinition.CPT88177(), "YPI", "Patient", 6.84));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(new CptCodeDefinition.CPT88182(), "YPI", "Patient", 68.18));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(new CptCodeDefinition.CPT88184(), "YPI", "Patient", 88.84));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(new CptCodeDefinition.CPT88185(), "YPI", "Patient", 54.13));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(new CptCodeDefinition.CPT88300(), "YPI", "Patient", 23.86));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(new CptCodeDefinition.CPT88302(), "YPI", "Patient", 49.39));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(new CptCodeDefinition.CPT88304(), "YPI", "Patient", 56.20));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(new CptCodeDefinition.CPT88305(), "YPI", "Patient", 70.00));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(new CptCodeDefinition.CPT88307(), "YPI", "Patient", 155.59));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(new CptCodeDefinition.CPT88309(), "YPI", "Patient", 216.58));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(new CptCodeDefinition.CPT88311(), "YPI", "Patient", 7.18));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(new CptCodeDefinition.CPT88312(), "YPI", "Patient", 67.77));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(new CptCodeDefinition.CPT88313(), "YPI", "Patient", 55.83));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(new CptCodeDefinition.CPT88323(), "YPI", "Patient", 55.86));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(new CptCodeDefinition.CPT88331(), "YPI", "Patient", 33.73));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(new CptCodeDefinition.CPT88332(), "YPI", "Patient", 11.61));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(new CptCodeDefinition.CPT88333(), "YPI", "Patient", 37.14));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(new CptCodeDefinition.CPT88334(), "YPI", "Patient", 23.52));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(new CptCodeDefinition.CPT88342(), "YPI", "Patient", 64.71));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(new CptCodeDefinition.CPT88360(), "YPI", "Patient", 69.47));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(new CptCodeDefinition.CPT88365(), "YPI", "Patient", 325.00));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(new CptCodeDefinition.CPT88368(), "YPI", "Patient", 170.49));
        }
    }
}
