using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Calendar
{
    public class PathologistsByLocation
    {
        private int m_BillingsCount;
        private int m_BozemanCount;

        public PathologistsByLocation()
        { }        

        public int TotalCount
        {
            get { return this.m_BillingsCount + this.m_BozemanCount; }
        }

        public int BillingsCount
        {
            get { return this.m_BillingsCount; }
            set { this.m_BillingsCount = value; }
        }

        public int BozemanCount
        {
            get { return this.m_BozemanCount; }
            set { this.m_BozemanCount = value; }
        }
    }
}
