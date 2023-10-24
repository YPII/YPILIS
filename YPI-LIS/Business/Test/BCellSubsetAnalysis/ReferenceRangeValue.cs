using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Test.BCellSubsetAnalysis
{
    public class ReferenceRangeValue
    {
        public int m_CellTypeId;
        public string m_CellType;
        public double m_LowerBound;
        public double m_UpperBound;

        public ReferenceRangeValue(int cellTypeId, string cellType, double lowerbound, double upperBound)
        {
            this.m_CellTypeId = cellTypeId;
            this.m_CellType = cellType;
            this.m_LowerBound = lowerbound;
            this.m_UpperBound = upperBound;
        }

        public string GetResultString(string result)
        {
            string resultString = result.ToString();

            double tryParse = 0;
            bool success = double.TryParse(result, out tryParse);

            Nullable<double> dresult = null;
            if (success)
            {
                dresult = tryParse;
                if (dresult > this.m_UpperBound)
                {
                    resultString = result + " H";
                }
                else if (dresult < this.m_LowerBound)
                {
                    resultString = result + " L";
                }
            }                            
            
            return resultString;
        }
    }
}
