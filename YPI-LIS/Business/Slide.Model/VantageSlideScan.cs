using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Slide.Model
{
    public class VantageSlideScan
    {
        string m_SlideId;
        DateTime m_ScanDate;
        string m_Location;
        string m_ScannedBy;

        public VantageSlideScan()
        {

        }

        public string SlideId
        {
            get { return this.m_SlideId; }
            set { this.m_SlideId = value; }
        }

        public DateTime ScanDate
        {
            get { return this.m_ScanDate; }
            set { this.m_ScanDate = value; }
        }

        public string Location
        {
            get { return this.m_Location; }
            set { this.m_Location = value; }
        }

        public string ScannedBy
        {
            get { return this.m_ScannedBy; }
            set { this.m_ScannedBy = value; }
        }
    }
}
