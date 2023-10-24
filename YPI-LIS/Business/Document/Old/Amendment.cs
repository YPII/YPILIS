using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Windows.Forms;
using System.Xml;

namespace YellowstonePathology.Business.Document.Old
{    
    class Amendment
    {
        public void SetAmendment(DataTable table, XmlDocument mainDocument, XmlNamespaceManager nameSpaceManager)
        {     
            XmlNode tableNode = mainDocument.SelectSingleNode("descendant::w:tbl[w:tr/w:tc/w:p/w:r/w:t='amendment_text']", nameSpaceManager);            
            XmlNode AmendmentDateTimeNode = tableNode.SelectSingleNode("descendant::w:tr[w:tc/w:p/w:r/w:t='amendment_date_time']", nameSpaceManager);
            XmlNode AmendmentBlankRowNode = tableNode.SelectSingleNode("descendant::w:tr[w:tc/w:p/w:r/w:t='amendment_blank_row']", nameSpaceManager);
            XmlNode AmendmentTextNode = tableNode.SelectSingleNode("descendant::w:tr[w:tc/w:p/w:r/w:t='amendment_text']", nameSpaceManager);
            XmlNode AmendmentSignatureNode = tableNode.SelectSingleNode("descendant::w:tr[w:tc/w:p/w:r/w:t='amendment_signature']", nameSpaceManager);
            XmlNode ElectronicSignatureNode = tableNode.SelectSingleNode("descendant::w:tr[w:tc/w:p/w:r/w:t='electronic_signature']", nameSpaceManager);

            XmlNode nodeToInsertAfter = AmendmentDateTimeNode;

            foreach (DataRow dr in table.Rows)
            {                               
                bool isFinal = Convert.ToBoolean(dr["final"].ToString());
                if (isFinal == true)
                {
                    string amendmentText = dr["text"].ToString();
                    string amendmentDateTime = dr["amendmenttype"].ToString() + ": " + DateTime.Parse(dr["finaldate"].ToString()).ToShortDateString() + " - " + DateTime.Parse(dr["finaltime"].ToString()).ToString("HH:mm");
                    string signature = dr["pathologistsignature"].ToString();

                    XmlNode nodeLine1 = AmendmentDateTimeNode.Clone();
                    XmlNode nodeLine2 = AmendmentTextNode.Clone();
                    XmlNode nodeLine3 = AmendmentBlankRowNode.Clone();
                    XmlNode nodeLine4 = AmendmentSignatureNode.Clone();
                    XmlNode nodeLine5 = ElectronicSignatureNode.Clone();
                    
                    nodeLine1.SelectSingleNode("descendant::w:r[w:t='amendment_date_time']/w:t", nameSpaceManager).InnerText = amendmentDateTime;
                    this.SetXMLNodeParagraphDataNew(nodeLine2, "amendment_text", amendmentText, nameSpaceManager);

                    tableNode.InsertAfter(nodeLine1, nodeToInsertAfter);
                    tableNode.InsertAfter(nodeLine2, nodeLine1);

                    if (dr["RequirePathologistSignature"].ToString() == "True")
                    {
                        nodeLine3.SelectSingleNode("descendant::w:r[w:t='amendment_blank_row']/w:t", nameSpaceManager).InnerText = "";
                        nodeLine4.SelectSingleNode("descendant::w:r[w:t='amendment_signature']/w:t", nameSpaceManager).InnerText = signature;
                        nodeLine5.SelectSingleNode("descendant::w:r[w:t='electronic_signature']/w:t", nameSpaceManager).InnerText = "*** Electronic Signature ***";

                        tableNode.InsertAfter(nodeLine3, nodeLine2);
                        tableNode.InsertAfter(nodeLine4, nodeLine3);
                        tableNode.InsertAfter(nodeLine5, nodeLine4);
                    }
                    else
                    {                        
                        nodeLine5.SelectSingleNode("descendant::w:r[w:t='electronic_signature']/w:t", nameSpaceManager).InnerText = "";
                        tableNode.InsertAfter(nodeLine5, nodeLine2);
                    }
                    nodeToInsertAfter = nodeLine5;
                }
            }
            
            tableNode.RemoveChild(AmendmentDateTimeNode);
            tableNode.RemoveChild(ElectronicSignatureNode);
            tableNode.RemoveChild(AmendmentBlankRowNode);
            tableNode.RemoveChild(AmendmentSignatureNode);
            tableNode.RemoveChild(AmendmentTextNode);
        }

        public void SetXMLNodeParagraphDataNew(XmlNode inputNode, string field, string data, XmlNamespaceManager nameSpaceManager)
        {
            XmlNode parentNode = inputNode.SelectSingleNode("descendant::w:tc[w:p/w:r/w:t='" + field + "']", nameSpaceManager);
            XmlNode childNode = parentNode.SelectSingleNode("descendant::w:p[w:r/w:t='" + field + "']", nameSpaceManager);
            
            string paragraphs = data;
            string[] lineSplit = paragraphs.Split('\n');
            
            for (int i = 0; i < lineSplit.Length; i++)
            {
                XmlNode childNodeClone = childNode.Clone();
                XmlNode node = childNodeClone.SelectSingleNode("descendant::w:r[w:t='" + field + "']/w:t", nameSpaceManager);
                node.InnerText = lineSplit[i];
                parentNode.AppendChild(childNodeClone);
            }
            parentNode.RemoveChild(childNode);            
        }
    }
}
