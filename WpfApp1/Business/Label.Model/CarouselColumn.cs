using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Label.Model
{
    public class CarouselColumn
    {
        private string m_CassetteColor;
        private int m_ColumnNumber;
        private int m_PrinterCode;
        private string m_ColorCode;
        private string m_PrinterPath;        

        public CarouselColumn(string cassetteColor, int columnNumber, int printerCode, string colorCode, string printerPath)
        {
            this.m_CassetteColor = cassetteColor;
            this.m_ColumnNumber = columnNumber;
            this.m_PrinterCode = printerCode;
            this.m_ColorCode = colorCode;
            this.m_PrinterPath = printerPath;
        }        

        public string CassetteColor
        {
            get { return this.m_CassetteColor; }
        }

        public int ColumnNumber
        {
            get { return this.m_ColumnNumber; }
        }

        public int PrinterCode
        {
            get { return this.m_PrinterCode; }
        }

        public string PrinterPath
        {
            get { return this.m_PrinterPath; }
        }

        public string ColorCode
        {
            get { return this.m_ColorCode; }
        }
    }
}
