using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Billing.Model
{
    public class InsuranceMap
    {
        private string m_Name;
        private string m_MapsTo;

        public InsuranceMap()
        {

        }

        [PersistentPrimaryKeyProperty(false)]
        [PersistentDataColumnProperty(true, "500", "null", "varchar")]
        public string Name
        {
            get { return this.m_Name; }
            set { this.m_Name = value; }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "100", "null", "varchar")]
        public string MapsTo
        {
            get { return this.m_MapsTo; }
            set { this.m_MapsTo = value; }
        }
    }
}
