using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Billing.Model
{
    public class CPTCodeHRPrice
    {
        List<CPTCodePrice> m_CPTCodePriceList;

        public CPTCodeHRPrice()
        {
            this.m_CPTCodePriceList = new List<CPTCodePrice>();            
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Store.AppDataStore.Instance.CPTCodeCollection.GetClone("88104", null), "YPI", "Client", 19.8));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Store.AppDataStore.Instance.CPTCodeCollection.GetClone("88112", null), "YPI", "Client", 43.33));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Store.AppDataStore.Instance.CPTCodeCollection.GetClone("88160", null), "YPI", "Client", 20.11));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Store.AppDataStore.Instance.CPTCodeCollection.GetClone("88161", null), "YPI", "Client", 23.55));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Store.AppDataStore.Instance.CPTCodeCollection.GetClone("88172", null), "YPI", "Client", 15.41));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Store.AppDataStore.Instance.CPTCodeCollection.GetClone("88173", null), "YPI", "Client", 49.25));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Store.AppDataStore.Instance.CPTCodeCollection.GetClone("88300", null), "YPI", "Client", 13.50));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Store.AppDataStore.Instance.CPTCodeCollection.GetClone("88302", null), "YPI", "Client", 31.08));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Store.AppDataStore.Instance.CPTCodeCollection.GetClone("88304", null), "YPI", "Client", 39.23));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Store.AppDataStore.Instance.CPTCodeCollection.GetClone("88305", null), "YPI", "Client", 50.87));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Store.AppDataStore.Instance.CPTCodeCollection.GetClone("88307", null), "YPI", "Client", 79.75));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Store.AppDataStore.Instance.CPTCodeCollection.GetClone("88309", null), "YPI", "Client", 216.72));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Store.AppDataStore.Instance.CPTCodeCollection.GetClone("83111", null), "YPI", "Client", 4.41));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Store.AppDataStore.Instance.CPTCodeCollection.GetClone("88312", null), "YPI", "Client", 40.77));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Store.AppDataStore.Instance.CPTCodeCollection.GetClone("88313", null), "YPI", "Client", 36.39));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Store.AppDataStore.Instance.CPTCodeCollection.GetClone("88331", null), "YPI", "Client", 19.84));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Store.AppDataStore.Instance.CPTCodeCollection.GetClone("88332", null), "YPI", "Client", 7.25));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Store.AppDataStore.Instance.CPTCodeCollection.GetClone("88342", null), "YPI", "Client", 39.12));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Store.AppDataStore.Instance.CPTCodeCollection.GetClone("88360", null), "YPI", "Client", 42.00));
        }
    }
}
