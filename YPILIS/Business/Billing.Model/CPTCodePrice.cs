using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Billing.Model
{
    public class CPTCodePrice
    {
        protected CptCode m_CptCode;
        protected string m_BillBy;
        protected string m_BillTo;
        protected double m_Price;

        public CPTCodePrice(CptCode cptCode, string billBy, string billTo, double price)
        {
            this.m_CptCode = cptCode;
            this.m_BillBy = billBy;
            this.m_BillTo = billTo;
            this.m_Price = price;
        }

        public CptCode CptCode
        {
            get { return this.m_CptCode; }
            set { this.m_CptCode = value; }
        }

        public string BillBy
        {
            get { return this.m_BillBy; }
            set { this.m_BillBy = value; }
        }

        public string BillTo
        {
            get { return this.m_BillTo; }
            set { this.m_BillTo = value; }
        }

        public double Price
        {
            get { return this.m_Price; }
            set { this.m_Price = value; }
        }
    }
}
