using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Common
{
    public class PrintMateCarousel
    {
        public List<PrintMateColumn> m_Columns;

        public PrintMateCarousel()
        {
            this.m_Columns = new List<PrintMateColumn>();
            this.m_Columns.Add(new PrintMateColumnYellow());
            this.m_Columns.Add(new PrintMateColumnTeal());
            this.m_Columns.Add(new PrintMateColumnOrange1());
            this.m_Columns.Add(new PrintMateColumnPink());
            this.m_Columns.Add(new PrintMateColumnOrange2());            
            this.m_Columns.Add(new PrintMateColumnGreen());
            this.m_Columns.Add(new PrintMateColumnLilac());
        }

        public List<PrintMateColumn> Columns
        {
            get { return this.m_Columns; }
        }        

        public PrintMateColumn GetColumn(int columnNumber)
        {
            PrintMateColumn result = new PrintMateNullColumn();
            foreach (PrintMateColumn printMateColumn in this.m_Columns)
            {
                if (printMateColumn.ColumnNumber == columnNumber)
                {
                    result = printMateColumn;
                    break;
                }
            }
            return result;
        }
    }
}
