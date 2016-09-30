using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace YellowstonePathology.Business.Facility.Model
{
    public partial class NullFacility : Facility
    {
        public NullFacility()
        {
            this.m_CliaLicense = new CLIALicense(this, null);
        }

    }
}
