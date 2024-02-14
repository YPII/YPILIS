using System;
using System.Text;

namespace YellowstonePathology.Business.BarcodeScanning
{
    public class VantageBarcode
    {
        private static int idNo = 0;
        private string m_ScanData;
        private string m_ReportNo;
        private string m_Specimen;
        private string m_Block;
        private string m_Slide;

        public VantageBarcode()
        {

        }

        public VantageBarcode(string scanData)
        {
            this.m_ScanData = scanData;
            string [] splits = scanData.Split(new char[] { ';' });
            this.m_ReportNo = splits[0];
            this.m_Specimen = splits[1];
            this.m_Block = splits[2];
            this.m_Slide = splits[3];
        }

        public string ScanData
        {
            get { return this.m_ScanData; }
        }

        public string ReportNo
        {
            get { return this.m_ReportNo; }
        }

        public string Specimen
        {
            get { return this.m_Specimen; }
        }

        public string Block
        {
            get { return this.m_Block; }
        }

        public string Slide
        {
            get { return this.m_Slide; }
        }

        public static string SimulateScan()
        {
            string first = "17Z07235S;A;1;1";
            string second = "17Z07235S;B;2;2";
            string third = "17Z07235S;C;2;1";
            string result = string.Empty;

            idNo++;
            switch (idNo)
            {
                case 1:
                    result = first;
                    break;
                case 2:
                    result = second;
                    break;
                case 3:
                    result = third;
                    break;
                default:
                    idNo = 1;
                    result = first;
                    break;
            }

            return result;
        }
    }
}
