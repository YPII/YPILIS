using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace YellowstonePathology.UI.Scanning
{
    public class LocalScannedFileCollection : ScannedFileCollection
    {        
        public LocalScannedFileCollection() 
        {
            
        }        

        public void WriteXml()
        {
            XElement xElement = new XElement("FileNameCollection");
            foreach (ScannedFile fileName in this)
            {
                xElement.Add("FileName", fileName.ToXml());
            }
            //xElement.Save(this.m_LocalFileName);
        }

        public void ReadXml()
        {
            //XElement xElement = XElement.Load(this.m_LocalFileName);
            //foreach (XElement fileNameElement in xElement.Elements("FileName"))
            //{
            //   ScannedFile newFileName = new ScannedFile();
            //    newFileName.FromXml(fileNameElement);
            //    this.Add(newFileName);
            //}
        }
    }
}
