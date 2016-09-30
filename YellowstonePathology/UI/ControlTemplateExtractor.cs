using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Xml;
using System.IO;

namespace YellowstonePathology.UI
{
    public class ControlTemplateExtractor
    {
        public static void WriteToFile(System.Windows.Controls.Control control)
        {
            ControlTemplate template = control.Template;

            XmlWriterSettings xmlSettings = new XmlWriterSettings();
            xmlSettings.Indent = true;

            StringBuilder sb = new StringBuilder();
            XmlWriter writer = XmlWriter.Create(sb, xmlSettings);
            System.Windows.Markup.XamlWriter.Save(template, writer);
            TextWriter tw = new StreamWriter(@"c:\testing\ControlTemplate.xml");
            tw.WriteLine(sb.ToString());
            tw.Close();            
        }
    }
}
