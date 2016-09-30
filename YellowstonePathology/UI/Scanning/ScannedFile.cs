using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace YellowstonePathology.UI.Scanning
{
    public class ScannedFile
    {
        private string m_Name;
        private string m_ReportNo;
        private int m_FileSize;
        private DateTime m_FileDate;

        public ScannedFile()
        {

        }

        public string DisplayName
        {
            get { return System.IO.Path.GetFileName(Name); }
        }

        public string Extension
        {
            get { return System.IO.Path.GetExtension(this.Name); }
        }

        public string Name
        {
            get { return this.m_Name; }
            set { this.m_Name = value; }
        }

        public string ReportNo
        {
            get { return this.m_ReportNo; }
            set { this.m_ReportNo = value; }
        }

        public int FileSize
        {
            get { return this.m_FileSize; }
            set { this.m_FileSize = value; }
        }

        public DateTime FileDate
        {
            get { return this.m_FileDate; }
            set { this.m_FileDate = value; }
        }

        public XElement ToXml()
        {
            XElement xElement = new XElement("FileName",
                new XElement("Name", this.m_Name),
                new XElement("ReportNo", this.m_ReportNo),
                new XElement("FileSize", this.m_FileSize),
                new XElement("FileDate", this.m_FileDate.ToString()));                            
            return xElement;
        }

        public void FromXml(XElement xElement)
        {
            this.m_Name = xElement.Element("Name").Value;
            this.m_ReportNo = xElement.Element("ReportNo").Value;            
            this.m_FileSize = Convert.ToInt32(xElement.Element("FileSize").Value);            
            this.m_FileDate = DateTime.Parse(xElement.Element("FileDate").Value);            
        }
    }
}
