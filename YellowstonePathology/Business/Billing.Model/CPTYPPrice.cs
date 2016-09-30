using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Billing.Model
{
    public class CPTCodeYPPrice
    {
        List<CPTCodePrice> m_CPTCodePriceList;

        public CPTCodeYPPrice()
        {
            this.m_CPTCodePriceList = new List<CPTCodePrice>();            
            this.m_CPTCodePriceList.Add(new CPTCodePrice(new CptCodeDefinition.CPT81210(), "YPI", "YP", 72.51));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(new CptCodeDefinition.CPT81220(), "YPI", "YP", 883.90));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(new CptCodeDefinition.CPT81240(), "YPI", "YP", 52.00));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(new CptCodeDefinition.CPT81241(), "YPI", "YP", 52.00));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(new CptCodeDefinition.CPT81261(), "YPI", "YP", 101.00));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(new CptCodeDefinition.CPT81270(), "YPI", "YP", 38.44));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(new CptCodeDefinition.CPT81275(), "YPI", "YP", 469.98));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(new CptCodeDefinition.CPT83890(), "YPI", "YP", 5.35));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(new CptCodeDefinition.CPT83891(), "YPI", "YP", 5.35));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(new CptCodeDefinition.CPT83892(), "YPI", "YP", 5.35));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(new CptCodeDefinition.CPT83896(), "YPI", "YP", 5.35));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(new CptCodeDefinition.CPT83898(), "YPI", "YP", 22.39));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(new CptCodeDefinition.CPT83900(), "YPI", "YP", 44.77));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(new CptCodeDefinition.CPT83901(), "YPI", "YP", 22.39));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(new CptCodeDefinition.CPT83903(), "YPI", "YP", 21.98));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(new CptCodeDefinition.CPT83907(), "YPI", "YP", 17.84));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(new CptCodeDefinition.CPT83909(), "YPI", "YP", 22.39));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(new CptCodeDefinition.CPT83914(), "YPI", "YP", 22.39));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(new CptCodeDefinition.CPT85055(), "YPI", "YP", 35.76));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(new CptCodeDefinition.CPT86023(), "YPI", "YP", 16.63));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(new CptCodeDefinition.CPT86356(), "YPI", "YP", 35.76));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(new CptCodeDefinition.CPT86367(), "YPI", "YP", 50.37));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(new CptCodeDefinition.CPT87491(), "YPI", "YP", 46.87));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(new CptCodeDefinition.CPT87491(), "YPI", "Client", 95.00));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(new CptCodeDefinition.CPT87591(), "YPI", "YP", 46.87));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(new CptCodeDefinition.CPT87591(), "YPI", "Client", 95.00));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(new CptCodeDefinition.CPT87621(), "YPI", "YP", 46.87));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(new CptCodeDefinition.CPT87621(), "YPI", "Client", 66.00));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(new CptCodeDefinition.CPT87798(), "YPI", "YP", 36.85));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(new CptCodeDefinition.CPT88104(), "YPI", "YP", 29.45));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(new CptCodeDefinition.CPT88112(), "YPI", "YP", 37.71));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(new CptCodeDefinition.CPT88112(), "YPI", "Client", 242.00));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(new CptCodeDefinition.CPT88141(), "YPI", "Client", 38.00));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(new CptCodeDefinition.CPT88142(), "YPI", "YP", 40.00));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(new CptCodeDefinition.CPT88160(), "YPI", "YP", 23.10));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(new CptCodeDefinition.CPT88161(), "YPI", "YP", 24.69));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(new CptCodeDefinition.CPT88164(), "YPI", "YP", 14.11));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(new CptCodeDefinition.CPT88172(), "YPI", "YP", 19.61));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(new CptCodeDefinition.CPT88173(), "YPI", "YP", 55.18));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(new CptCodeDefinition.CPT88175(), "YPI", "YP", 35.37));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(new CptCodeDefinition.CPT88175(), "YPI", "Client", 40.00));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(new CptCodeDefinition.CPT88182(), "YPI", "YP", 56.05));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(new CptCodeDefinition.CPT88184(), "YPI", "YP", 67.89));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(new CptCodeDefinition.CPT88185(), "YPI", "YP", 40.58));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(new CptCodeDefinition.CPT88300(), "YPI", "YP", 16.74));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(new CptCodeDefinition.CPT88302(), "YPI", "YP", 36.44));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(new CptCodeDefinition.CPT88304(), "YPI", "YP", 43.75));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(new CptCodeDefinition.CPT88304(), "YPI", "Client", 79.00));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(new CptCodeDefinition.CPT88305(), "YPI", "YP", 57.09));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(new CptCodeDefinition.CPT88305(), "YPI", "Client", 110.00));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(new CptCodeDefinition.CPT88307(), "YPI", "YP", 113.95));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(new CptCodeDefinition.CPT88309(), "YPI", "YP", 157.06));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(new CptCodeDefinition.CPT88311(), "YPI", "YP", 5.31));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(new CptCodeDefinition.CPT88312(), "YPI", "YP", 63.13));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(new CptCodeDefinition.CPT88312(), "YPI", "Client", 165.00));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(new CptCodeDefinition.CPT88313(), "YPI", "YP", 52.64));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(new CptCodeDefinition.CPT88313(), "YPI", "Client", 124));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(new CptCodeDefinition.CPT88323(), "YPI", "YP", 48.19));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(new CptCodeDefinition.CPT88331(), "YPI", "YP", 24.01));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(new CptCodeDefinition.CPT88332(), "YPI", "YP", 8.81));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(new CptCodeDefinition.CPT88333(), "YPI", "YP", 27.23));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(new CptCodeDefinition.CPT88334(), "YPI", "YP", 16.74));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(new CptCodeDefinition.CPT88342(), "YPI", "YP", 50.10));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(new CptCodeDefinition.CPT88342(), "YPI", "Client", 105.00));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(new CptCodeDefinition.CPT88360(), "YPI", "YP", 57.09));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(new CptCodeDefinition.CPT88360(), "YPI", "Client", 125.00));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(new CptCodeDefinition.CPT88368(), "YPI", "YP", 125.07));
        }
    }
}
