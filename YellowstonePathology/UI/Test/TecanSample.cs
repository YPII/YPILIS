using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI.Test
{
    public class TecanSample
    {        
        private ExcelWorksheetCell m_WellCell;
        private ExcelWorksheetCell m_ResultIdCell;

        public TecanSample(ExcelWorksheetCell wellCell, ExcelWorksheetCell resultIdCell)
        {
            this.m_WellCell = wellCell;
            this.m_ResultIdCell = resultIdCell;
        }

        public static string GetWellCellValue(string reportNo, string patientLastName)
        {
            return "[" + reportNo + "] " + patientLastName;
        }

        public static string GetReportNo(string resultIdCell)
        {
            string result = resultIdCell;
            if (resultIdCell != "Enter Sample ID")
            {
                int positionOfFirstBracket = resultIdCell.IndexOf('[');
                int positionOfSecondBracket = resultIdCell.IndexOf(']');                
                result = resultIdCell.Substring(positionOfFirstBracket + 1, positionOfSecondBracket - positionOfFirstBracket - 1);                
            }
            return result;
        }        

        public ExcelWorksheetCell WellCell
        {
            get { return this.m_WellCell; }
        }

        public ExcelWorksheetCell ResultIdCell
        {
            get { return this.m_ResultIdCell; }
        }
    }
}
