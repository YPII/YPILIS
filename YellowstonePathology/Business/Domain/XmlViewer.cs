using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Diagnostics;

namespace YellowstonePathology.Business.Domain
{
    public class XmlViewer
    {
        const string XmlFileName = @"C:\Program Files\Yellowstone Pathology Institute\ViewXml.xml";

        public XmlViewer()
        {

        }

        public void Show(XElement xElement)
        {
            xElement.Save(XmlFileName);
            Process p1 = new Process();
            p1.StartInfo = new ProcessStartInfo("IExplore.exe", XmlFileName);
            p1.Start();
            p1.WaitForExit();
            p1.Close();
        }
    }
}
