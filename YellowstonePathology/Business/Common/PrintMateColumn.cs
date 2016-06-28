using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Common
{
    public class PrintMateColumn
    {
        private int m_ColumnNumber;
        private string m_Description;
        private string m_ColorCode;
        private string m_Color;
        private int m_GeneralDataId;     

        public PrintMateColumn(int columnNumber, string description, string colorCode, string color, int generalDataId)
        {
            this.m_ColumnNumber = columnNumber;
            this.m_Description = description;
            this.m_ColorCode = colorCode;
            this.m_Color = color;
            this.m_GeneralDataId = generalDataId;
        }

        public int ColumnNumber
        {
            get { return this.m_ColumnNumber; }
        }

        public string Description
        {
            get { return this.m_Description; }
        }

        public string ColorCode
        {
            get { return this.m_ColorCode; }
        }

        public string Color
        {
            get { return this.m_Color; }
        }

        public int GeneralDataColor
        {
            get { return this.m_GeneralDataId; }
        }
    }
}
