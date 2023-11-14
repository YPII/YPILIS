using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Billing.Model
{
    public class FeeSchedule
    {
        protected string m_Name;

        public FeeSchedule()
        {

        }

        public FeeSchedule(string name)
        {
            this.m_Name = name;
        }

        public string Name
        {
            get { return this.m_Name; }
        }
    }
}
