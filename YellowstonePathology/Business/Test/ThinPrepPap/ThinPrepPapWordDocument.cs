using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Xml;
using System.Data;
using System.Collections;
using System.Text.RegularExpressions;

namespace YellowstonePathology.Business.Test.ThinPrepPap
{
    public class ThinPrepPapWordDocument : YellowstonePathology.Business.Interface.ICaseDocument
    {
        public const string HPVHasBeenOrderedComment = "High Risk HPV testing has been ordered and will be reported separately.";
        public const string HPV1618HasBeenOrderedComment = "HPV Genotypes 16 and 18 testing has been ordered and will be reported separately.";
        public const string NoAdditionalTestingOrderedComment = "No additional testing has been ordered.";
        public const string HPVHasBeenOrderedAndHasBeenResulted = "High Risk HPV testing (YPI report #*REPORTNO*): *RESULT*";

        const string m_ThinPrepTemplateName = @"\\CFileServer\Documents\ReportTemplates\XmlTemplates\CytologyThinPrep.6.xml";
        const string m_RegularTemplateName = @"\\CFileServer\Documents\ReportTemplates\XmlTemplates\CytologyRegular.5.xml";

        protected string m_TemplateName;
        protected XmlDocument m_ReportXml;
        protected XmlNamespaceManager m_NameSpaceManager;
        protected string m_SaveFileName;
        protected YellowstonePathology.Business.Document.ReportSaveModeEnum m_ReportSaveEnum;
        
        protected YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
		protected YellowstonePathology.Business.Test.ThinPrepPap.PanelSetOrderCytology m_PanelSetOrderCytology;

        protected YellowstonePathology.Business.Document.NativeDocumentFormatEnum m_NativeDocumentFormat;		

        public ThinPrepPapWordDocument(Business.Test.AccessionOrder accessionOrder, Business.Test.PanelSetOrder panelSetOrder, YellowstonePathology.Business.Document.ReportSaveModeEnum reportSaveMode)             
        {
            this.m_AccessionOrder = accessionOrder;
            this.m_PanelSetOrderCytology = (YellowstonePathology.Business.Test.ThinPrepPap.PanelSetOrderCytology)panelSetOrder;
            this.m_ReportSaveEnum = reportSaveMode;

            this.m_NativeDocumentFormat = YellowstonePathology.Business.Document.NativeDocumentFormatEnum.Word;
            this.m_ReportXml = new XmlDocument();
            this.m_NameSpaceManager = new XmlNamespaceManager(m_ReportXml.NameTable);
            this.m_NameSpaceManager.AddNamespace("w", "http://schemas.microsoft.com/office/word/2003/wordml");            
        }

        public YellowstonePathology.Business.Document.NativeDocumentFormatEnum NativeDocumentFormat
        {
            get { return this.m_NativeDocumentFormat; }
            set { this.m_NativeDocumentFormat = value; }
        }

		public YellowstonePathology.Business.Rules.MethodResult DeleteCaseFiles(YellowstonePathology.Business.OrderIdParser orderIdParser)
        {
            return YellowstonePathology.Business.Document.CaseDocument.DeleteCaseFiles(orderIdParser);
        }

        public void Render()
        {            			
            this.m_TemplateName = m_ThinPrepTemplateName;
            this.OpenTemplate();

            this.SetDemographics();
            
            this.SetReportDistribution(this.m_PanelSetOrderCytology.TestOrderReportDistributionCollection);

            this.SetCaseHistory();

            bool hpvHasBeenOrdered = this.m_AccessionOrder.PanelSetOrderCollection.Exists(14);            

            string additionalTestingComment = string.Empty;
            if (hpvHasBeenOrdered == true)
            {                
                additionalTestingComment = HPVHasBeenOrderedComment;                
            }            
            else
            {
                additionalTestingComment = NoAdditionalTestingOrderedComment;
            }

            this.ReplaceText("additional_testing", additionalTestingComment);            

            string sceeningImpression = m_PanelSetOrderCytology.ScreeningImpression;
            if (string.IsNullOrEmpty(sceeningImpression) == false)
            {
                this.SetXmlNodeData("screening_impression", sceeningImpression);
            }
            else
            {
                this.DeleteRow("Epithelial Cell Description");
                this.DeleteRow("screening_impression");
            }

            string specimenAdequacy = m_PanelSetOrderCytology.SpecimenAdequacy;
            this.SetXmlNodeData("specimen_adequacy", specimenAdequacy);

            string otherConditions = m_PanelSetOrderCytology.OtherConditions;
            if (string.IsNullOrEmpty(otherConditions) == false)
            {
                this.SetXmlNodeData("other_conditions", otherConditions);
            }
            else
            {
                this.DeleteRow("Other Conditions");
                this.DeleteRow("other_conditions");
            }

            string reportComment = m_PanelSetOrderCytology.ReportComment;
            if (string.IsNullOrEmpty(reportComment) == false)
            {
                this.SetXMLNodeParagraphData("report_comment", reportComment);
            }
            else
            {
                this.DeleteRow("Report Comment");
                this.DeleteRow("report_comment");
            }

            YellowstonePathology.Business.Test.ThinPrepPap.PanelOrderCytology screeningPanelOrder = null;
            YellowstonePathology.Business.Test.ThinPrepPap.PanelOrderCytology reviewPanelOrder = null;

            foreach (YellowstonePathology.Business.Interface.IPanelOrder panelOrder in this.m_PanelSetOrderCytology.PanelOrders)
            {
                Type objectType = panelOrder.GetType();
                if (typeof(YellowstonePathology.Business.Test.ThinPrepPap.PanelOrderCytology).IsAssignableFrom(objectType) == true)
                {
                    YellowstonePathology.Business.Test.ThinPrepPap.PanelOrderCytology cytologyPanelOrder = (YellowstonePathology.Business.Test.ThinPrepPap.PanelOrderCytology)panelOrder;
                    if (cytologyPanelOrder.PanelId == 38)
                    {
                        if (cytologyPanelOrder.ScreeningType == "Primary Screening")
                        {
                            screeningPanelOrder = cytologyPanelOrder;
                        }                        
                        else if (cytologyPanelOrder.ScreeningType == "Pathologist Review")
                        {
                            reviewPanelOrder = cytologyPanelOrder;
                        }
                        else if (cytologyPanelOrder.ScreeningType == "Cytotech Review")
                        {
                            if (reviewPanelOrder == null || reviewPanelOrder.ScreeningType != "Pathologist Review")
                            {
                                reviewPanelOrder = cytologyPanelOrder;
                            }                            
                        }
                    }
                }
            }

            YellowstonePathology.Business.User.SystemUser systemUser = YellowstonePathology.Business.User.SystemUserCollectionInstance.Instance.SystemUserCollection.GetSystemUserById(screeningPanelOrder.ScreenedById);
            string screenedBy = string.Empty;
            if (string.IsNullOrEmpty(systemUser.Signature) == false)
            {
                screenedBy = systemUser.Signature;
            }
            this.SetXmlNodeData("screened_by", screenedBy);

            string finalDate = YellowstonePathology.Business.Helper.DateTimeExtensions.DateStringFromNullable(this.m_PanelSetOrderCytology.FinalDate);
            string cytoTechFinal = YellowstonePathology.Business.Helper.DateTimeExtensions.DateStringFromNullable(screeningPanelOrder.AcceptedDate);
            this.SetXmlNodeData("cytotech_final", cytoTechFinal);

            if (reviewPanelOrder != null)
            {
                string reviewedBy = YellowstonePathology.Business.User.SystemUserCollectionInstance.Instance.SystemUserCollection.GetSystemUserById(reviewPanelOrder.ScreenedById).Signature;
                string reviewedByFinal = YellowstonePathology.Business.Helper.DateTimeExtensions.DateStringFromNullable(reviewPanelOrder.AcceptedDate);

                if (reviewedBy.IndexOf("MD") >= 0)
                {
                    this.SetXmlNodeData("Reviewed By:", "Interpreted By:");
                }
                this.SetXmlNodeData("reviewed_by", reviewedBy);
                this.SetXmlNodeData("case_final", reviewedByFinal);
            }
            else
            {
                this.SetXmlNodeData("Reviewed By:", "");
                this.SetXmlNodeData("reviewed_by", "");
                this.SetXmlNodeData("case_final", "");
            }
            this.SetXmlNodeData("final_date", finalDate);
            
            SetAmendments(this.m_PanelSetOrderCytology.AmendmentCollection);

			YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrder(this.m_PanelSetOrderCytology.OrderedOn, this.m_PanelSetOrderCytology.OrderedOnId);
            this.SetXmlNodeData("specimen_description", specimenOrder.GetSpecimenDescriptionString());
            this.SetXmlNodeData("specimen_source", specimenOrder.SpecimenSource);

            string clinicalHistory = this.m_AccessionOrder.ClinicalHistory;
            this.SetXmlNodeData("clinical_history", clinicalHistory);

            this.SetXmlNodeData("screening_method", this.m_PanelSetOrderCytology.Method);
            this.SetXmlNodeData("report_references", this.m_PanelSetOrderCytology.References);

            string collectionDateTimeString = this.m_AccessionOrder.SpecimenOrderCollection[0].GetCollectionDateTimeString();
            this.SetXmlNodeData("date_time_collected", collectionDateTimeString);

            this.SetXmlNodeData("footer_final_date", "Final Date: " + finalDate);
            this.SetXmlNodeData("location_performed", this.m_PanelSetOrderCytology.GetLocationPerformedComment());
            this.SaveReport();
        }

        public void Publish()
        {
			YellowstonePathology.Business.OrderIdParser orderIdParser = new YellowstonePathology.Business.OrderIdParser(this.m_PanelSetOrderCytology.ReportNo);
            YellowstonePathology.Business.Document.CaseDocument.SaveXMLAsPDF(orderIdParser);
            YellowstonePathology.Business.Helper.FileConversionHelper.SaveXpsReportToTiff(this.m_PanelSetOrderCytology.ReportNo);
        }

        public void OpenTemplate()
        {
			YellowstonePathology.Business.OrderIdParser orderIdParser = new YellowstonePathology.Business.OrderIdParser(this.m_PanelSetOrderCytology.ReportNo);
            this.m_ReportXml.Load(this.m_TemplateName);
            switch (this.m_ReportSaveEnum)
            {
                case YellowstonePathology.Business.Document.ReportSaveModeEnum.Draft:
					this.m_SaveFileName = YellowstonePathology.Document.CaseDocumentPath.GetPath(orderIdParser) + this.m_PanelSetOrderCytology.ReportNo + ".draft.xml";
                    break;
                case YellowstonePathology.Business.Document.ReportSaveModeEnum.Normal:
					this.m_SaveFileName = YellowstonePathology.Document.CaseDocumentPath.GetPath(orderIdParser) + this.m_PanelSetOrderCytology.ReportNo + ".xml";
                    break;
                case YellowstonePathology.Business.Document.ReportSaveModeEnum.Test:
                    this.m_SaveFileName = @"c:\test.xml";
                    break;
            }
        }

        public void SetDemographics()
        {         
            this.ReplaceText("report_number", this.m_PanelSetOrderCytology.ReportNo);
            this.ReplaceText("accession_no", this.m_AccessionOrder.MasterAccessionNo.ToString());
            this.ReplaceText("patient_name", this.m_AccessionOrder.PatientDisplayName);
            this.ReplaceText("patient_age", this.m_AccessionOrder.PatientAccessionAge + ", " + this.m_AccessionOrder.PSex);

            if (string.IsNullOrEmpty(this.m_AccessionOrder.PCAN) == false)
            {
                this.ReplaceText("client_mrn_no", this.m_AccessionOrder.PCAN);
                this.ReplaceText("client_account_no", string.Empty);
            }
            else
            {
                this.ReplaceText("client_mrn_no", this.m_AccessionOrder.SvhMedicalRecord);
                this.ReplaceText("client_account_no", this.m_AccessionOrder.SvhAccount);
            }     

            if (this.m_AccessionOrder.CollectionDate.HasValue == true)
            {
                this.ReplaceText("date_of_service", this.m_AccessionOrder.CollectionDate.Value.ToShortDateString());
            }
            if (this.m_PanelSetOrderCytology.OrderDate.HasValue == true)
            {
                this.ReplaceText("received_date", this.m_PanelSetOrderCytology.OrderDate.Value.ToShortDateString());
                this.ReplaceText("accession_date", this.m_AccessionOrder.AccessionTime.Value.ToString("MM/dd/yyyy HH:mm"));
            }
            if (this.m_PanelSetOrderCytology.FinalDate.HasValue == true)
            {
                this.ReplaceText("final_date", this.m_PanelSetOrderCytology.FinalTime.Value.ToString("MM/dd/yyyy HH:mm"));
                this.ReplaceText("report_date", this.m_PanelSetOrderCytology.FinalDate.Value.ToShortDateString());
            }
            if (this.m_PanelSetOrderCytology.FinalTime.HasValue == true)
            {
                this.ReplaceText("report_time", this.m_PanelSetOrderCytology.FinalTime.Value.ToShortTimeString());
                this.ReplaceText("final_time", this.m_PanelSetOrderCytology.FinalTime.Value.ToShortTimeString());
            }
            if (this.m_AccessionOrder.CollectionDate.HasValue == true)
            {
                this.ReplaceText("collection_date", this.ReportDateTimeFormat(this.m_AccessionOrder.CollectionDate.Value));
            }
            if (this.m_AccessionOrder.PBirthdate.HasValue == true) this.ReplaceText("patient_birthdate", this.m_AccessionOrder.PBirthdate.Value.ToShortDateString());

            this.ReplaceText("physician_name", this.m_AccessionOrder.PhysicianName);
            this.ReplaceText("client_name", this.m_AccessionOrder.ClientName);
            this.ReplaceText("page2_header", this.m_AccessionOrder.PatientName + ", " + this.m_PanelSetOrderCytology.ReportNo);
        }

        public void SetReportDistribution(YellowstonePathology.Business.ReportDistribution.Model.TestOrderReportDistributionCollection testOrderReportDistributionCollection)
        {
            XmlNode nodeP = m_ReportXml.SelectSingleNode("descendant::w:p[w:r/w:t='report_distribution']", this.m_NameSpaceManager);
            XmlNode nodeTc = m_ReportXml.SelectSingleNode("descendant::w:tc[w:p/w:r/w:t='report_distribution']", this.m_NameSpaceManager);

            if (testOrderReportDistributionCollection.Count != 0)
            {
                foreach (YellowstonePathology.Business.ReportDistribution.Model.TestOrderReportDistribution testOrderReportDistribution in testOrderReportDistributionCollection)
                {
                    string reportDistribution = testOrderReportDistribution.DistributionType + ": " + testOrderReportDistribution.ClientName + " - " + testOrderReportDistribution.PhysicianName;
                    XmlNode nodeAppend = nodeP.Clone();
                    nodeAppend.SelectSingleNode("//w:r/w:t", this.m_NameSpaceManager).InnerText = reportDistribution;
                    nodeTc.AppendChild(nodeAppend);
                }
                nodeTc.RemoveChild(nodeP);
            }
            else
            {
                XmlNode nodeAppend = nodeP.Clone();
                nodeAppend.SelectSingleNode("//w:r/w:t", this.m_NameSpaceManager).InnerText = "None.";
                nodeTc.AppendChild(nodeAppend);
                nodeTc.RemoveChild(nodeP);
            }
        }

        public void SetCaseHistory()
        {
            XmlNode nodeP = m_ReportXml.SelectSingleNode("descendant::w:p[w:r/w:t='other_ypii_cases']", this.m_NameSpaceManager);
            XmlNode nodeTc = m_ReportXml.SelectSingleNode("descendant::w:tc[w:p/w:r/w:t='other_ypii_cases']", this.m_NameSpaceManager);

            YellowstonePathology.Business.Patient.Model.PatientHistoryList patientHistoryList = new Patient.Model.PatientHistoryList();
            patientHistoryList.SetFillCommandByAccessionNo(this.m_PanelSetOrderCytology.ReportNo);
            patientHistoryList.Fill();

            bool hashistory = false;
            if (patientHistoryList.Count > 1)
            {
                string caseHistory = "";
                foreach (Business.Patient.Model.PatientHistoryListItem item in patientHistoryList)
                {
                    if (item.ReportNo != this.m_PanelSetOrderCytology.ReportNo)
                    {
                        caseHistory += item.ReportNo + ", ";
                    }
                }
                if (caseHistory.Length > 2)
                {
                    hashistory = true;
                    caseHistory = caseHistory.Substring(0, caseHistory.Length - 2);
                    XmlNode nodeAppend = nodeP.Clone();
                    nodeAppend.SelectSingleNode("//w:r/w:t", this.m_NameSpaceManager).InnerText = caseHistory;
                    nodeTc.AppendChild(nodeAppend);
                    nodeTc.RemoveChild(nodeP);
                }
            }

            if (!hashistory)
            {
                XmlNode nodeAppend = nodeP.Clone();
                nodeAppend.SelectSingleNode("//w:r/w:t", this.m_NameSpaceManager).InnerText = "None.";
                nodeTc.AppendChild(nodeAppend);
                nodeTc.RemoveChild(nodeP);
            }
        }

        public void ReplaceText(string searchString, string replaceString)
        {
            XmlNodeList nodeList = this.m_ReportXml.SelectNodes("descendant::w:t", this.m_NameSpaceManager);
            foreach (XmlNode node in nodeList)
            {
                string text = node.InnerXml;
                if (text.Contains(searchString) == true)
                {
                    XmlNode parentNode = node.ParentNode;
                    List<XmlNode> nodesToAdd = this.GetNodesToInsert(node, searchString, replaceString);
                    XmlNode nodeToAddAfter = node;
                    foreach (XmlNode addNode in nodesToAdd)
                    {
                        parentNode.InsertAfter(addNode, nodeToAddAfter);
                        nodeToAddAfter = addNode;
                    }
                    parentNode.RemoveChild(node);
                }
            }
        }

        public List<XmlNode> GetNodesToInsert(XmlNode replaceNode, string searchString, string replaceString)
        {
            List<XmlNode> nodeList = new List<XmlNode>();

            if (string.IsNullOrEmpty(replaceString) == true) return nodeList;

            replaceString = replaceString.Replace("&", "&amp;");
            string[] lines = Regex.Split(replaceString, "\r\n");

            if (lines.Length == 1)
            {
                XmlNode tNode = this.m_ReportXml.CreateElement("w", "t", "http://schemas.microsoft.com/office/word/2003/wordml");
                tNode.InnerXml = replaceNode.InnerXml.Replace(searchString, replaceString);
                nodeList.Add(tNode);
            }
            else
            {
                string[] preservedText = Regex.Split(replaceNode.InnerXml, searchString);
                if (preservedText.Length >= 1)
                {
                    XmlNode startNode = this.m_ReportXml.CreateElement("w", "t", "http://schemas.microsoft.com/office/word/2003/wordml");
                    startNode.InnerXml = preservedText[0];
                    nodeList.Add(startNode);
                }

                for (int i = 0; i < lines.Length; i++)
                {
                    XmlNode lineNode = this.m_ReportXml.CreateElement("w", "t", "http://schemas.microsoft.com/office/word/2003/wordml");
                    lineNode.InnerXml = lines[i];
                    nodeList.Add(lineNode);

                    if (i != lines.Length - 1)
                    {
                        XmlNode breakNode = this.m_ReportXml.CreateElement("w", "br", "http://schemas.microsoft.com/office/word/2003/wordml");
                        nodeList.Add(breakNode);
                    }
                }

                if (preservedText.Length == 2)
                {
                    XmlNode endNode = this.m_ReportXml.CreateElement("w", "t", "http://schemas.microsoft.com/office/word/2003/wordml");
                    endNode.InnerXml = preservedText[1];
                    nodeList.Add(endNode);
                }
            }
            return nodeList;
        }

        public void SetXmlNodeData(string field, string data)
        {
            try
            {
                XmlNode node = m_ReportXml.SelectSingleNode("//w:t[.='" + field + "']", this.m_NameSpaceManager);
                node.InnerText = data;
            }
            catch (NullReferenceException)
            {
                //System.Windows.MessageBox.Show(field + " " + e.Message);
            }
        }

        public void SetXMLNodeParagraphData(string field, string data)
        {
            try
            {
                XmlNode parentNode = m_ReportXml.SelectSingleNode("descendant::w:tc[w:p/w:r/w:t='" + field + "']", this.m_NameSpaceManager);
                XmlNode childNode = parentNode.SelectSingleNode("descendant::w:p[w:r/w:t='" + field + "']", this.m_NameSpaceManager);

                Regex regex = new Regex("(\n)");
                string[] lineSplit = regex.Split(data);

                for (int i = 0; i < lineSplit.Length; i++)
                {
                    if (lineSplit[i] != "\n")
                    {
                        XmlNode childNodeClone = childNode.Clone();
                        XmlNode node = childNodeClone.SelectSingleNode("descendant::w:r[w:t='" + field + "']/w:t", this.m_NameSpaceManager);
                        node.InnerText = lineSplit[i];
                        parentNode.AppendChild(childNodeClone);
                    }
                }
                parentNode.RemoveChild(childNode);
            }
            catch (Exception)
            {
                //System.Windows.MessageBox.Show("Hello");
            }
        }

        public void SetXMLNodeParagraphDataNode(XmlNode inputNode, string field, string data)
        {
            XmlNode parentNode = inputNode.SelectSingleNode("descendant::w:tc[w:p/w:r/w:t='" + field + "']", this.m_NameSpaceManager); ;
            XmlNode childNode = parentNode.SelectSingleNode("descendant::w:p[w:r/w:t='" + field + "']", this.m_NameSpaceManager);

            string paragraphs = data;
            string[] lineSplit = paragraphs.Split('\n');

            for (int i = 0; i < lineSplit.Length; i++)
            {
                XmlNode childNodeClone = childNode.Clone();
                XmlNode node = childNodeClone.SelectSingleNode("descendant::w:r[w:t='" + field + "']/w:t", this.m_NameSpaceManager);
                node.InnerText = lineSplit[i];
                parentNode.AppendChild(childNodeClone);
            }
            parentNode.RemoveChild(childNode);
        }

        public void DeleteRow(string field)
        {
            XmlNode parentNode = m_ReportXml.SelectSingleNode("descendant::w:tbl[w:tr/w:tc/w:p/w:r/w:t='" + field + "']", this.m_NameSpaceManager);
            XmlNode childNode = parentNode.SelectSingleNode("descendant::w:tr[w:tc/w:p/w:r/w:t='" + field + "']", this.m_NameSpaceManager);
            parentNode.RemoveChild(childNode);
        }

        public void SaveReport()
        {
            switch (this.m_ReportSaveEnum)
            {
                case YellowstonePathology.Business.Document.ReportSaveModeEnum.Draft:
                    this.m_ReportXml.Save(this.m_SaveFileName);
                    break;
                case YellowstonePathology.Business.Document.ReportSaveModeEnum.Normal:
                    this.m_ReportXml.Save(this.m_SaveFileName);
					YellowstonePathology.Business.OrderIdParser orderIdParser = new YellowstonePathology.Business.OrderIdParser(this.m_PanelSetOrderCytology.ReportNo);
					YellowstonePathology.Business.Document.CaseDocument.SaveXMLAsDoc(orderIdParser);
					YellowstonePathology.Business.Document.CaseDocument.SaveDocAsXPS(orderIdParser);
                    break;
                case YellowstonePathology.Business.Document.ReportSaveModeEnum.Test:
                    this.m_ReportXml.Save(@"c:\Testing\Test.xml");
                    break;
            }
        }

        public void SaveReport(bool isLinq)
        {
            switch (this.m_ReportSaveEnum)
            {
                case YellowstonePathology.Business.Document.ReportSaveModeEnum.Draft:
                    this.m_ReportXml.Save(this.m_SaveFileName);
                    break;
                case YellowstonePathology.Business.Document.ReportSaveModeEnum.Normal:
					YellowstonePathology.Business.OrderIdParser orderIdParser = new YellowstonePathology.Business.OrderIdParser(this.m_PanelSetOrderCytology.ReportNo);
                    this.m_ReportXml.Save(this.m_SaveFileName);
					YellowstonePathology.Business.Document.CaseDocument.SaveXMLAsDoc(orderIdParser);
                    break;
                case YellowstonePathology.Business.Document.ReportSaveModeEnum.Test:
                    this.m_ReportXml.Save(@"c:\Testing\Test.xml");
                    break;
            }
        }

        public void SetAmendments(YellowstonePathology.Business.Amendment.Model.AmendmentCollection amendments)
        {
            XmlNode tableNode = m_ReportXml.SelectSingleNode("descendant::w:tbl[w:tr/w:tc/w:p/w:r/w:t='amendment_text']", this.m_NameSpaceManager);
            XmlNode AddendumDateTimeNode = tableNode.SelectSingleNode("descendant::w:tr[w:tc/w:p/w:r/w:t='amendment_date_time']", this.m_NameSpaceManager);
            XmlNode AddendumTextNode = tableNode.SelectSingleNode("descendant::w:tr[w:tc/w:p/w:r/w:t='amendment_text']", this.m_NameSpaceManager);
            XmlNode AddendumSignatureNode = tableNode.SelectSingleNode("descendant::w:tr[w:tc/w:p/w:r/w:t='amendment_signature']", this.m_NameSpaceManager);
            XmlNode ElectronicSignatureNode = tableNode.SelectSingleNode("descendant::w:tr[w:tc/w:p/w:r/w:t='signature_title']", this.m_NameSpaceManager);

            XmlNode nodeToInsertAfter = AddendumDateTimeNode;

            string amendmentTitle = string.Empty;
            if (amendments.Count != 0)
            {
                amendmentTitle = "Amendment";
            }
            m_ReportXml.SelectSingleNode("descendant::w:r[w:t='amendment_title']/w:t", this.m_NameSpaceManager).InnerText = amendmentTitle;

            foreach (YellowstonePathology.Business.Amendment.Model.Amendment dr in amendments)
            {

                if (dr.Final == true)
                {
                    string addendumText = dr.Text;
                    string addendumDateTime = dr.FinalDate.Value.ToShortDateString() + " - " + dr.FinalTime.Value.ToString("HH:mm");
                    string signature = dr.PathologistSignature;

                    XmlNode nodeLine1 = AddendumDateTimeNode.Clone();
                    XmlNode nodeLine2 = AddendumTextNode.Clone();
                    XmlNode nodeLine3 = AddendumSignatureNode.Clone();
                    XmlNode nodeLine4 = ElectronicSignatureNode.Clone();

                    nodeLine1.SelectSingleNode("descendant::w:r[w:t='amendment_date_time']/w:t", this.m_NameSpaceManager).InnerText = addendumDateTime;
                    this.SetXMLNodeParagraphDataNew(nodeLine2, "amendment_text", addendumText, this.m_NameSpaceManager);
                    nodeLine3.SelectSingleNode("descendant::w:r[w:t='amendment_signature']/w:t", this.m_NameSpaceManager).InnerText = signature;
                    nodeLine4.SelectSingleNode("descendant::w:r[w:t='signature_title']/w:t", this.m_NameSpaceManager).InnerText = "*** Electronic Signature ***";

                    tableNode.InsertAfter(nodeLine1, nodeToInsertAfter);
                    tableNode.InsertAfter(nodeLine2, nodeLine1);
                    tableNode.InsertAfter(nodeLine3, nodeLine2);
                    tableNode.InsertAfter(nodeLine4, nodeLine3);

                    nodeToInsertAfter = nodeLine4;
                }
            }

            tableNode.RemoveChild(AddendumDateTimeNode);
            tableNode.RemoveChild(ElectronicSignatureNode);
            tableNode.RemoveChild(AddendumSignatureNode);
            tableNode.RemoveChild(AddendumTextNode);
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

        private string ReportDateTimeFormat(DateTime date)
        {
            string dateString = date.ToShortDateString();
            string timeString = date.ToShortTimeString();
            if (timeString != "12:00 AM")
            {
                dateString += " " + timeString;
            }
            return dateString;
        }

        private string ReportDateTimeFormat(Nullable<DateTime> date)
        {
            if (date.HasValue)
            {
                return ReportDateTimeFormat(date.Value);
            }
            return string.Empty;
        }
    }
}
