using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.HL7View.NMH
{
    public enum NMHResultStatsusEnum
    {
        P, //Pending
        D, //Draft
        S, //Signed
        A, //addendum
        AS //Signed Addendum
    }
}
