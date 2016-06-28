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
            result.Add(new PQRSBreastMeasure());
			result.Add(new PQRSBarrettsEsophagusMeasure());
			result.Add(new PQRSColorectalMeasure());
			result.Add(new PQRSRadicalProstatectomyMeasure());
            result.Add(new PQRSMelanomaMeasure());
            result.Add(new PQRSMeasure395());
            result.Add(new PQRSMeasure396());
			return result;
        }
    }
}
