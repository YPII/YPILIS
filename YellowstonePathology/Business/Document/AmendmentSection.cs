using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Linq;
using System.Data.Linq;
using System.Xml;

namespace YellowstonePathology.Business.Document
{    
    public class AmendmentSection
    {
        /*public void SetAmendment(Amendment.AmendmentItemCollectionOld AmendmentItemCollection, XmlDocument mainDocument, XmlNamespaceManager nameSpaceManager, bool hasSignature)
        {
            XmlNode tableNode = mainDocument.SelectSingleNode("descendant::w:tbl[w:tr/w:tc/w:p/w:r/w:t='amendment_text']", nameSpaceManager);
            XmlNode AmendmentDateTimeNode = tableNode.SelectSingleNode("descendant::w:tr[w:tc/w:p/w:r/w:t='amendment_date_time']", nameSpaceManager);
            XmlNode AmendmentTextNode = tableNode.SelectSingleNode("descendant::w:tr[w:tc/w:p/w:r/w:t='amendment_text']", nameSpaceManager);
            XmlNode AmendmentSignatureNode = tableNode.SelectSingleNode("descendant::w:tr[w:tc/w:p/w:r/w:t='amendment_signature']", nameSpaceManager);
            XmlNode ElectronicSignatureNode = tableNode.SelectSingleNode("descendant::w:tr[w:tc/w:p/w:r/w:t='electronic_signature']", nameSpaceManager);

            XmlNode nodeToInsertAfter = AmendmentDateTimeNode;

            string amendmentTitle = string.Empty;
            if (AmendmentItemCollection.Count != 0)
            {
                amendmentTitle = "Amendment";
            }
            mainDocument.SelectSingleNode("descendant::w:r[w:t='amendment_title']/w:t", nameSpaceManager).InnerText = amendmentTitle;

            foreach (Amendment.AmendmentItemOld amendment in AmendmentItemCollection)
            {
                bool isFinal = amendment.Final;
                if (isFinal == true)
                {
                    string amendmentText = amendment.Amendment;
                    string amendmentDateTime = amendment.AmendmentType + ": " + BaseData.GetShortDateString(amendment.FinalDate) + " - " + BaseData.GetMillitaryTimeString(amendment.FinalTime);
                    string signature = amendment.PathologistSignature;

                    XmlNode nodeLine1 = AmendmentDateTimeNode.Clone();
                    XmlNode nodeLine2 = AmendmentTextNode.Clone();


                    nodeLine1.SelectSingleNode("descendant::w:r[w:t='amendment_date_time']/w:t", nameSpaceManager).InnerText = amendmentDateTime;
                    this.SetXMLNodeParagraphDataNew(nodeLine2, "amendment_text", amendmentText, nameSpaceManager);

                    tableNode.InsertAfter(nodeLine1, nodeToInsertAfter);
                    tableNode.InsertAfter(nodeLine2, nodeLine1);

                    if (hasSignature == true)
                    {
                        XmlNode nodeLine3 = AmendmentSignatureNode.Clone();
                        XmlNode nodeLine4 = ElectronicSignatureNode.Clone();


                        if (amendment.RequirePathologistSignature == true)
                        {
                            nodeLine3.SelectSingleNode("descendant::w:r[w:t='amendment_signature']/w:t", nameSpaceManager).InnerText = signature;
                            nodeLine4.SelectSingleNode("descendant::w:r[w:t='electronic_signature']/w:t", nameSpaceManager).InnerText = "*** Electronic Signature ***";

                            tableNode.InsertAfter(nodeLine3, nodeLine2);
                            tableNode.InsertAfter(nodeLine4, nodeLine3);
                        }
                        else
                        {
                            nodeLine4.SelectSingleNode("descendant::w:r[w:t='electronic_signature']/w:t", nameSpaceManager).InnerText = "";
                            tableNode.InsertAfter(nodeLine4, nodeLine2);
                        }
                        nodeToInsertAfter = nodeLine4;
                    }
                }
            }

            tableNode.RemoveChild(AmendmentDateTimeNode);
            if (hasSignature == true)
            {
                tableNode.RemoveChild(ElectronicSignatureNode);
                tableNode.RemoveChild(AmendmentSignatureNode);
            }
            tableNode.RemoveChild(AmendmentTextNode);
        }*/

        public void SetAmendment(YellowstonePathology.Business.Amendment.Model.AmendmentCollection amendmentCollection, XmlDocument mainDocument, XmlNamespaceManager nameSpaceManager, bool hasSignature)
        {                           
            XmlNode tableNode = mainDocument.SelectSingleNode("descendant::w:tbl[w:tr/w:tc/w:p/w:r/w:t='amendment_text']", nameSpaceManager);
            XmlNode AmendmentBlankRowNode = tableNode.SelectSingleNode("descendant::w:tr[w:tc/w:p/w:r/w:t='amendment_blank_row']", nameSpaceManager);
            XmlNode AmendmentDateTimeNode = tableNode.SelectSingleNode("descendant::w:tr[w:tc/w:p/w:r/w:t='amendment_date_time']", nameSpaceManager);
            XmlNode AmendmentTextNode = tableNode.SelectSingleNode("descendant::w:tr[w:tc/w:p/w:r/w:t='amendment_text']", nameSpaceManager);
            XmlNode AmendmentSignatureNode = tableNode.SelectSingleNode("descendant::w:tr[w:tc/w:p/w:r/w:t='amendment_signature']", nameSpaceManager);
            XmlNode ElectronicSignatureNode = tableNode.SelectSingleNode("descendant::w:tr[w:tc/w:p/w:r/w:t='signature_title']", nameSpaceManager);

            XmlNode nodeToInsertAfter = AmendmentDateTimeNode;

            string amendmentTitle = string.Empty;
            if (amendmentCollection.Count != 0)
            {
                amendmentTitle = "Amendment";                
            }            
            mainDocument.SelectSingleNode("descendant::w:r[w:t='amendment_title']/w:t", nameSpaceManager).InnerText = amendmentTitle;

            foreach (YellowstonePathology.Business.Amendment.Model.Amendment amendment in amendmentCollection)
            {                              
                bool isFinal = amendment.Final;
                if (isFinal == true)
                {
                    string amendmentText = amendment.Text;
                    string amendmentDateTime = amendment.AmendmentType + ": " + BaseData.GetShortDateString(amendment.FinalDate) + " - " + BaseData.GetMillitaryTimeString(amendment.FinalTime);
                    string signature = amendment.PathologistSignature;

                    XmlNode nodeLine1 = AmendmentDateTimeNode.Clone();
                    XmlNode nodeLine2 = AmendmentTextNode.Clone();                   
                    
                    
                    nodeLine1.SelectSingleNode("descendant::w:r[w:t='amendment_date_time']/w:t", nameSpaceManager).InnerText = amendmentDateTime;
                    this.SetXMLNodeParagraphDataNew(nodeLine2, "amendment_text", amendmentText, nameSpaceManager);

                    tableNode.InsertAfter(nodeLine1, nodeToInsertAfter);
                    tableNode.InsertAfter(nodeLine2, nodeLine1);

                    if (hasSignature == true)
                    {
                        XmlNode nodeLine3 = AmendmentSignatureNode.Clone();
                        XmlNode nodeLine4 = ElectronicSignatureNode.Clone();


                        if (amendment.RequirePathologistSignature == true)
                        {
                            nodeLine3.SelectSingleNode("descendant::w:r[w:t='amendment_signature']/w:t", nameSpaceManager).InnerText = signature;
                            string signatureTitle = "*** E-Signed " + amendment.FinalDate.Value.ToShortDateString() + " " + amendment.FinalTime.Value.ToShortTimeString() + " ***";
                            nodeLine4.SelectSingleNode("descendant::w:r[w:t='signature_title']/w:t", nameSpaceManager).InnerText = signatureTitle;

                            tableNode.InsertAfter(nodeLine3, nodeLine2);
                            tableNode.InsertAfter(nodeLine4, nodeLine3);
                        }
                        else
                        {
                            nodeLine4.SelectSingleNode("descendant::w:r[w:t='signature_title']/w:t", nameSpaceManager).InnerText = "";
                            tableNode.InsertAfter(nodeLine4, nodeLine2);
                        }
                        nodeToInsertAfter = nodeLine4;
                    }
                }
            }
            
            tableNode.RemoveChild(AmendmentDateTimeNode);
			if(AmendmentBlankRowNode != null) tableNode.RemoveChild(AmendmentBlankRowNode);

            if (hasSignature == true)
            {
                tableNode.RemoveChild(ElectronicSignatureNode);
                tableNode.RemoveChild(AmendmentSignatureNode);
            }
            tableNode.RemoveChild(AmendmentTextNode);            
        }

		/*public void SetAmendment(EntitySet<YellowstonePathology.Business.Domain.Core.Amendment> amendments, XmlDocument mainDocument, XmlNamespaceManager nameSpaceManager, bool hasSignature)
		{
			XmlNode tableNode = mainDocument.SelectSingleNode("descendant::w:tbl[w:tr/w:tc/w:p/w:r/w:t='amendment_text']", nameSpaceManager);
			XmlNode AmendmentBlankRowNode = tableNode.SelectSingleNode("descendant::w:tr[w:tc/w:p/w:r/w:t='amendment_blank_row']", nameSpaceManager);
			XmlNode AmendmentDateTimeNode = tableNode.SelectSingleNode("descendant::w:tr[w:tc/w:p/w:r/w:t='amendment_date_time']", nameSpaceManager);
			XmlNode AmendmentTextNode = tableNode.SelectSingleNode("descendant::w:tr[w:tc/w:p/w:r/w:t='amendment_text']", nameSpaceManager);
			XmlNode AmendmentSignatureNode = tableNode.SelectSingleNode("descendant::w:tr[w:tc/w:p/w:r/w:t='amendment_signature']", nameSpaceManager);
			XmlNode ElectronicSignatureNode = tableNode.SelectSingleNode("descendant::w:tr[w:tc/w:p/w:r/w:t='signature_title']", nameSpaceManager);

			XmlNode nodeToInsertAfter = AmendmentDateTimeNode;

			string amendmentTitle = string.Empty;
			if (amendments.Count != 0)
			{
				amendmentTitle = "Amendment";
			}
			mainDocument.SelectSingleNode("descendant::w:r[w:t='amendment_title']/w:t", nameSpaceManager).InnerText = amendmentTitle;

			foreach (YellowstonePathology.Business.Domain.Core.Amendment amendment in amendments)
			{
				bool isFinal = false;
				if (amendment.Final.HasValue && amendment.Final.Value == true)
				{
					isFinal = true;
				}

				if (isFinal == true)
				{
					string amendmentText = amendment.Text;
					string amendmentDateTime = amendment.AmendmentType + ": " + amendment.FinalDate.Value.ToShortDateString() + " - " + BaseData.GetMillitaryTimeString(amendment.FinalTime);
					string signature = amendment.PathologistSignature;

					XmlNode nodeLine1 = AmendmentDateTimeNode.Clone();
					XmlNode nodeLine2 = AmendmentTextNode.Clone();


					nodeLine1.SelectSingleNode("descendant::w:r[w:t='amendment_date_time']/w:t", nameSpaceManager).InnerText = amendmentDateTime;
					this.SetXMLNodeParagraphDataNew(nodeLine2, "amendment_text", amendmentText, nameSpaceManager);

					tableNode.InsertAfter(nodeLine1, nodeToInsertAfter);
					tableNode.InsertAfter(nodeLine2, nodeLine1);

					if (hasSignature == true)
					{
						XmlNode nodeLine3 = AmendmentSignatureNode.Clone();
						XmlNode nodeLine4 = ElectronicSignatureNode.Clone();


						if (amendment.RequirePathologistSignature == true)
						{
							nodeLine3.SelectSingleNode("descendant::w:r[w:t='amendment_signature']/w:t", nameSpaceManager).InnerText = signature;
							string signatureTitle = "*** E-Signed " + amendment.FinalDate.Value.ToShortDateString() + " " + amendment.FinalTime.Value.ToShortTimeString() + " ***";
							nodeLine4.SelectSingleNode("descendant::w:r[w:t='signature_title']/w:t", nameSpaceManager).InnerText = signatureTitle;

							tableNode.InsertAfter(nodeLine3, nodeLine2);
							tableNode.InsertAfter(nodeLine4, nodeLine3);
						}
						else
						{
							nodeLine4.SelectSingleNode("descendant::w:r[w:t='signature_title']/w:t", nameSpaceManager).InnerText = "";
							tableNode.InsertAfter(nodeLine4, nodeLine2);
						}
						nodeToInsertAfter = nodeLine4;
					}
				}
			}

			tableNode.RemoveChild(AmendmentDateTimeNode);
			tableNode.RemoveChild(AmendmentBlankRowNode);
			if (hasSignature == true)
			{
				tableNode.RemoveChild(ElectronicSignatureNode);
				tableNode.RemoveChild(AmendmentSignatureNode);
			}
			tableNode.RemoveChild(AmendmentTextNode);
		}*/               

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
