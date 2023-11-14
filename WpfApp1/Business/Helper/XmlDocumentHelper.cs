using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Helper
{
	public static class XmlDocumentHelper
	{
        public static void AddElementIfNotEmpty(XElement parent, XElement child)
        {
            if (string.IsNullOrEmpty(child.Value) == false)
            {
                parent.Add(child);
            }
        }

        public static void AddElement(string name, string value, XElement xElement)
        {
            if (string.IsNullOrEmpty(value) == false)
            {
                XElement newElement = new XElement(name, value);
                xElement.Add(newElement);
            }
        }

        public static void InsertImage(this XmlDocument documentPart, string bookmarkName, XmlNamespaceManager namespaceManager)
        {
            List<XmlNode> bookmarkNodes = new List<XmlNode>();
            GetBookmarkTextNodes(documentPart, bookmarkNodes, bookmarkName, namespaceManager);

            foreach (XmlNode node in bookmarkNodes)
            {
                if (node.InnerText == bookmarkName)
                {
                    node.InnerXml = @"<w:p><w:r><w:pict><v:shape id=""myShape1"" type=""#_x0000_t75"" style=""width:400; height:240""><v:imagedata r:id=""rId1""/></v:shape></w:pict></w:r></w:p>";
                }
            }
        }

        public static void GetBookmarkTextNodes(this XmlDocument xmlDocument, List<XmlNode> nodeList, string bookmarkName, XmlNamespaceManager nameSpaceManager)
        {                                    
            XmlNode nodeBookmarkStart = xmlDocument.SelectSingleNode("//w:bookmarkStart[@w:name='" + bookmarkName + "']", nameSpaceManager);

            if (bookmarkName == "report_distribution")
            {
                Console.Write("sdf");
            }

            if (nodeBookmarkStart != null)
            {
                XmlNode nodeNextSibling = nodeBookmarkStart.NextSibling;
                int loop = 0;
                while (loop == 0)
                {
                    if (nodeNextSibling.Name != "w:bookmarkEnd")
                    {
                        nodeList.Add(nodeNextSibling);
                    }
                    else
                    {
                        break;
                    }
                    nodeNextSibling = nodeNextSibling.NextSibling;
                }
            }            
        }
      
        public static void ReplaceBookmarkText(this XmlDocument documentPart, string bookmarkName, string text, XmlNamespaceManager namespaceManager)
        {
            List<XmlNode> bookmarkNodes = new List<XmlNode>();
            GetBookmarkTextNodes(documentPart, bookmarkNodes, bookmarkName, namespaceManager);           

            foreach(XmlNode node in bookmarkNodes)
            {                
                node.InnerXml = node.InnerXml.Replace(bookmarkName, text);
            }
        }        
	}
}
