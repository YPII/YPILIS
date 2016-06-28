using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI.CustomEventArgs
{
    public class ExcelSpreadsheetReturnEventArgs : System.EventArgs
    {
        private Microsoft.Office.Interop.Excel.Application m_ExcelApplication;
        private Microsoft.Office.Interop.Excel.Workbook m_WorkBook;

        public ExcelSpreadsheetReturnEventArgs(Microsoft.Office.Interop.Excel.Application excelApplication, Microsoft.Office.Interop.Excel.Workbook workBook)
        {
            this.m_ExcelApplication = excelApplication;
            this.m_WorkBook = workBook;
        }

        public Microsoft.Office.Interop.Excel.Application ExcelApplication
        {
            get { return this.m_ExcelApplication; }
        }

        public Microsoft.Office.Interop.Excel.Workbook WorkBook
        {
            get { return this.m_WorkBook; }
        }    
    }
}
