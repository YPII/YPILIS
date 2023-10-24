using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Surgical
{
    public class PQRSMeasureCollection : List<PQRSMeasure>
    {
        public static PQRSMeasureCollection GetAll()
        {
            PQRSMeasureCollection result = new PQRSMeasureCollection();            			            
            result.Add(new PQRSMelanomaMeasure());
            result.Add(new PQRSMeasure249());
            result.Add(new PQRSMeasure250());
            result.Add(new PQRSMeasure395());
            result.Add(new PQRSMeasure396());
            result.Add(new PQRSMeasure491());
            return result;
        }
    }
}
