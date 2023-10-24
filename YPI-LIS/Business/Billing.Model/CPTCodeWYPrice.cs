using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Billing.Model
{
    public class CPTCodeWYPrice
    {
        List<CPTCodePrice> m_CPTCodePriceList;

        public CPTCodeWYPrice()
        {
            this.m_CPTCodePriceList = new List<CPTCodePrice>();            
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Store.AppDataStore.Instance.CPTCodeCollection.GetClone("88112", null), "YPI", "Client", 46.37));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Store.AppDataStore.Instance.CPTCodeCollection.GetClone("88184", null), "YPI", "Client", 82.79));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Store.AppDataStore.Instance.CPTCodeCollection.GetClone("88302", null), "YPI", "Client", 49.33));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Store.AppDataStore.Instance.CPTCodeCollection.GetClone("88304", null), "YPI", "Client", 51.14));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Store.AppDataStore.Instance.CPTCodeCollection.GetClone("88305", null), "YPI", "Client", 69.86));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Store.AppDataStore.Instance.CPTCodeCollection.GetClone("88307", null), "YPI", "Client", 155.63));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Store.AppDataStore.Instance.CPTCodeCollection.GetClone("88312", null), "YPI", "Client", 67.81));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Store.AppDataStore.Instance.CPTCodeCollection.GetClone("88313", null), "YPI", "Client", 54.20));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Store.AppDataStore.Instance.CPTCodeCollection.GetClone("88342", null), "YPI", "Client", 64.75));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Store.AppDataStore.Instance.CPTCodeCollection.GetClone("88360", null), "YPI", "Client", 69.52));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Store.AppDataStore.Instance.CPTCodeCollection.GetClone("88368", null), "YPI", "Client", 160.06));
        }
    }
}
