using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI.Test
{
    public class ExcelWorksheetCell
    {
        private int m_RowIndex;
        private int m_ColumnIndex;

        public ExcelWorksheetCell(int rowIndex, int columnIndex)
        {
            this.m_RowIndex = rowIndex;
            this.m_ColumnIndex = columnIndex;
        }

        public int RowIndex
        {
            get { return this.m_RowIndex; }
        }

        public int ColumnIndex
        {
            get { return this.m_ColumnIndex; }
        }
    }
}
