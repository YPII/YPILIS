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
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Store.AppDataStore.Instance.CPTCodeCollection.GetClone("88104", null), "YPI", "Client", 38.51));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Store.AppDataStore.Instance.CPTCodeCollection.GetClone("88112", null), "YPI", "Client", 44.01));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Store.AppDataStore.Instance.CPTCodeCollection.GetClone("88160", null), "YPI", "Client", 30.11));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Store.AppDataStore.Instance.CPTCodeCollection.GetClone("88161", null), "YPI", "Client", 31.72));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Store.AppDataStore.Instance.CPTCodeCollection.GetClone("88173", null), "YPI", "Client", 67.61));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Store.AppDataStore.Instance.CPTCodeCollection.GetClone("88175", null), "YPI", "Client", 37.52));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Store.AppDataStore.Instance.CPTCodeCollection.GetClone("88300", null), "YPI", "Client", 22.67));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Store.AppDataStore.Instance.CPTCodeCollection.GetClone("88302", null), "YPI", "Client", 46.92));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Store.AppDataStore.Instance.CPTCodeCollection.GetClone("88304", null), "YPI", "Client", 48.54));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Store.AppDataStore.Instance.CPTCodeCollection.GetClone("88305", null), "YPI", "Client", 66.32));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Store.AppDataStore.Instance.CPTCodeCollection.GetClone("88307", null), "YPI", "Client", 147.81));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Store.AppDataStore.Instance.CPTCodeCollection.GetClone("88309", null), "YPI", "Client", 205.75));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Store.AppDataStore.Instance.CPTCodeCollection.GetClone("88311", null), "YPI", "Client", 6.82));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Store.AppDataStore.Instance.CPTCodeCollection.GetClone("88312", null), "YPI", "Client", 64.38));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Store.AppDataStore.Instance.CPTCodeCollection.GetClone("88313", null), "YPI", "Client", 51.44));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Store.AppDataStore.Instance.CPTCodeCollection.GetClone("88331", null), "YPI", "Client", 32.04));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Store.AppDataStore.Instance.CPTCodeCollection.GetClone("88332", null), "YPI", "Client", 11.03));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Store.AppDataStore.Instance.CPTCodeCollection.GetClone("88333", null), "YPI", "Client", 35.28));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Store.AppDataStore.Instance.CPTCodeCollection.GetClone("88334", null), "YPI", "Client", 22.34));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Store.AppDataStore.Instance.CPTCodeCollection.GetClone("88342", null), "YPI", "Client", 61.47));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Store.AppDataStore.Instance.CPTCodeCollection.GetClone("88360", null), "YPI", "Client", 66.00));
        }
    }
}
