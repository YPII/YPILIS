﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Billing.Model
{
    public class CPTCodeWlandPrice
    {
        List<CPTCodePrice> m_CPTCodePriceList;

        public CPTCodeWlandPrice()
        {
            this.m_CPTCodePriceList = new List<CPTCodePrice>();            
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Business.Billing.Model.CptCodeCollection.Instance.GetClone("88175", null), "YPI", "Client", 42.43));
        }
    }
}
