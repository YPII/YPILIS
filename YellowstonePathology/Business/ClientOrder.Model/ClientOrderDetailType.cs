using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.ClientOrder.Model
{
    public class ClientOrderDetailType
    {
        public string m_Code;
        public string m_Description;

        public ClientOrderDetailType()
        {

        }

        public string Code
        {
            get { return this.m_Code; }
            set { this.m_Code = value; }
        }

        public string Description
        {
            get { return this.m_Description; }
            set { this.m_Description = value; }
        }
    }
}
