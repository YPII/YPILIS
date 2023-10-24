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
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Store.AppDataStore.Instance.CPTCodeCollection.GetClone("81210", null), "YPI", "Client", 262.00));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Store.AppDataStore.Instance.CPTCodeCollection.GetClone("81261", null), "YPI", "Client", 525.00));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Store.AppDataStore.Instance.CPTCodeCollection.GetClone("81270", null), "YPI", "Client", 261.00));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Store.AppDataStore.Instance.CPTCodeCollection.GetClone("81275", null), "YPI", "Client", 645.00));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Store.AppDataStore.Instance.CPTCodeCollection.GetClone("88184", null), "YPI", "Client", 98.00));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Store.AppDataStore.Instance.CPTCodeCollection.GetClone("88185", null), "YPI", "Client", 59.00));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Store.AppDataStore.Instance.CPTCodeCollection.GetClone("88312", null), "YPI", "Client", 79.00));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Store.AppDataStore.Instance.CPTCodeCollection.GetClone("88368", null), "YPI", "Client", 224.00));
        }
    }
}
