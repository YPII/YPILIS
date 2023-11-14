using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace YellowstonePathology.Document.Result.Data
{
    /// <summary>Surgical pathology report report data class
    /// </summary>
    public class SurgicalPathologyReportData : YpReportDataBase
    {

		#region Private constants
		/// <summary>name of XML element with results collection
		/// </summary>
		protected static readonly string DiagnosisCollectionName = "Diagnosis";
		/// <summary>name of XML element with single result
		/// </summary>
		protected static readonly string DiagnosisName = "DiagnosisItem";

		/// <summary>name of XML element with "Diagnosis Comment" section body text
        /// </summary>
		private static readonly string DiagnosisComment = "DiagnosisComment";
		/// <summary>name of XML element with "Microscopic Description" section body text
        /// </summary>
		private static readonly string MicroscopicDescription = "MicroscopicX";
        /// <summary>name of XML element with "Ancillary Studies" collection
        /// </summary>
		private static readonly string AncillaryStudies = "AncillaryStudies";
        /// <summary>name of XML element with "Ancillary Studies" collection item
        /// </summary>
		private static readonly string AncillaryStudy = "AncillaryStudy";
		/// <summary>name of XML element with "Ancillary Studies" collection item
		/// </summary>
		private static readonly string[] AncillaryStudyCellNodeName = new string[] { "ProcedureName", "Label", "Result" };
        /// <summary>name of XML element with Ancillary Studies header text
        /// </summary>
		private static readonly string AncillaryStudiesHeader = "AncillaryStudiesHeader";
		/// <summary>name of XML element with Clinical Info body text
		/// </summary>
		private static readonly string ClinicalInfo = "ClinicalInfo";
		/// <summary>name of XML element with Gross Description body text
		/// </summary>
		private static readonly string GrossDescription = "GrossX";
		/// <summary>name of XML element with items collection for "Previous Diagnosis" section
		/// </summary>
		private static readonly string PrevDiagnosisCollectionName = "PreviousDiagnosis";
		/// <summary>name of XML element with "Previous Diagnosis" section's single item
		/// </summary>
		protected static readonly string PrevDiagnosisName = "PrevDiagnosisItem";
		/// <summary>name of XML element with "Previous Diagnosis Comment" section body text
		/// </summary>
		private static readonly string PrevDiagnosisComment = "PrevDiagnosisComment";
		
		/// <summary>static text of "Ancillary Studies" section
        /// </summary>
        private const string m_AncillaryStudiesHeader = "Cytochemical and immunohistochemical studies were performed, with appropriately reacting controls. Results are listed below.";
		#endregion Private constants

		#region Constructors
		/// <summary>constructor with report number and XML data block
		/// </summary>
		/// <param name="reportNo">report number</param>
		/// <param name="inDocument">XML element with input data</param>
		public SurgicalPathologyReportData(string reportNo, XElement xmlDataDocument)
            : base(reportNo, xmlDataDocument, "SurgicalPathologyReport")
        {
		}
		#endregion Constructors

		#region Public properties
		/// <summary>property return true, if diagnosis for this report is revised, otherwise property return false
		/// </summary>
		public bool IsRevised
		{
			get
			{
				int amendmentsCount = Amendments.Count();
				if (amendmentsCount > 0)
					return Amendments.ElementAt(0).GetBoolValue(YpReportDataBase.RevisedDiagnosis);
				else
					return false;
			}
		}

		/// <summary>property return date/time string for first amendment or empty string, if report has no any amendments
		/// </summary>
		public string FirstAmendmentDateText
		{
			get 
			{ 
				int amendmentsCount = Amendments.Count();
				if (amendmentsCount > 0)
					return Amendments.ElementAt(0).GetDateTime(YpReportDataBase.AmendmentTime, "MM/dd/yyyy - HH:mm");
				else
					return string.Empty;
			}
		}

		/// <summary>property return text for main box's "Diagnosis comment" section
		/// </summary>
		public string DiagnosisCommentText
		{
			get { return this.GetStringValue(DiagnosisComment); }
		}
		/// <summary>property return diagnosis rows count
		/// </summary>
		public int DiagnosisRowsCount
        {
            get
            {
				IEnumerable<XElement> results = Element(DiagnosisCollectionName).Elements(DiagnosisName);
                return results.Count();
            }
        }
        /// <summary>property return body text for "Microscopic Description" section
        /// </summary>
        public string MicroscopicDescriptionText
        {
            get { return this.GetStringValue(MicroscopicDescription); }
        }
		/// <summary>property return table header text for "Ancillary Studies" section
		/// </summary>
		public string AncillaryStudiesHeaderText
		{
			get { return this.GetStringValue(AncillaryStudiesHeader, AncillaryStudies); }
		}
		/// <summary>property return row count for "Ancillary Studies" section table
		/// </summary>
		public int AncillaryStudiesCount
        {
            get
            {
				if (Element(AncillaryStudies) == null)
					return 0;
				else
				{
					IEnumerable<XElement> ancillaryStudies = Element(AncillaryStudies).Elements(AncillaryStudy);
					return ancillaryStudies.Count();
				}
            }
        }
		/// <summary>property return body text for "Clinical Information" section
		/// </summary>
		public string ClinicalInfoText
		{
			get { return this.GetStringValue(ClinicalInfo); }
		}
		/// <summary>property return body text for "Gross Description" section
		/// </summary>
		public string GrossDescriptionText
		{
			get { return this.GetStringValue(GrossDescription); }
		}
		/// <summary>property return previous diagnosis rows count
		/// </summary>
		public int PrevDiagnosisRowsCount
		{
			get
			{
				IEnumerable<XElement> results = Element(PrevDiagnosisCollectionName).Elements(PrevDiagnosisName);
				return results.Count();
			}
		}
		/// <summary>property return text for "Previous Diagnosis comment" section
		/// </summary>
		public string PrevDiagnosisCommentText
		{
			get { return this.GetStringValue(PrevDiagnosisComment); }
		}
		/// <summary>main box's diagnosis label
		/// </summary>
		public string DiagnosisLabel
		{
			get
			{
				if (IsRevised)
					return string.Format("Revised Diagnosis: {0}", FirstAmendmentDateText);
				else
					return "Diagnosis:";
			}
		}
		/// <summary>propery return "Previous diagnosis" section's title text
		/// </summary>
		public string PrevDiagnosisLabel
		{
			get
			{
				return string.Format("Previous diagnosis on {0}", PageHeader.FinalTimeString);
			}
		}
		#endregion Public properties

		#region Public methods
		/// <summary>method return diagnosis row's title text
		/// </summary>
		/// <param name="rowIndex">diagnosis row index</param>
		/// <returns>diagnosis row title text</returns>
		public string GetDiagnosisRowTitle(int rowIndex)
        {
			IEnumerable<XElement> results = Element(DiagnosisCollectionName).Elements(DiagnosisName);
            XElement result = results.ElementAt(rowIndex);
            return string.Format("{0}.     {1}", result.GetStringValue(TestNumber), result.GetStringValue(TestName));
        }
		/// <summary>method return diagnosis row's body text
		/// </summary>
		/// <param name="rowIndex">diagnosis row index</param>
		/// <returns>diagnosis body text</returns>
		public string GetDiagnosisRowBody(int rowIndex)
        {
			IEnumerable<XElement> results = Element(DiagnosisCollectionName).Elements(DiagnosisName);
            XElement result = results.ElementAt(rowIndex);
            return result.GetStringValue(TestResult);
        }
		/// <summary>method return "Ancillary Study" table row's title text
		/// </summary>
		/// <param name="rowIndex">Ancillary Study" table row index</param>
		/// <returns>"Ancillary Study" table row's title text</returns>
        public string GetAncillaryStudyTitle(int rowIndex)
        {
            XElement result = GetAncillaryStudy(rowIndex);
            return string.Format("{0}. {1}", result.GetStringValue(TestNumber), result.GetStringValue(TestName));
        }
		/// <summary>method return "Ancillary Study" table's cell text
		/// </summary>
		/// <param name="rowIndex">Ancillary Study" table row index</param>
		/// <param name="colIndex">Ancillary Study" table column index</param>
		/// <returns>"Ancillary Study" table's cell text</returns>
		public string GetAncillaryStudyCellValue(int rowIndex, int colIndex)
		{
			XElement result = GetAncillaryStudy(rowIndex);
			return result.GetStringValue(AncillaryStudyCellNodeName[colIndex]);
		}
		/// <summary>method return previous diagnosis row's title text
		/// </summary>
		/// <param name="rowIndex">previous diagnosis row index</param>
		/// <returns>previous diagnosis row title text</returns>
		public string GetPrevDiagnosisRowTitle(int rowIndex)
		{
			IEnumerable<XElement> results = Element(PrevDiagnosisCollectionName).Elements(PrevDiagnosisName);
			XElement result = results.ElementAt(rowIndex);
			return string.Format("{0}.     {1}", result.GetStringValue(TestNumber), result.GetStringValue(TestName));
		}
		/// <summary>method return previous diagnosis row's body text
		/// </summary>
		/// <param name="rowIndex">previous diagnosis row index</param>
		/// <returns>previous diagnosis body text</returns>
		public string GetPrevDiagnosisRowBody(int rowIndex)
		{
			IEnumerable<XElement> results = Element(PrevDiagnosisCollectionName).Elements(PrevDiagnosisName);
			XElement result = results.ElementAt(rowIndex);
			return result.GetStringValue(TestResult);
		}
		#endregion Public methods

		#region Protected methods
		/// <summary>method add custom report data
		/// </summary>
		/// <param name="reportNo">report number</param>
		/// <param name="xmlDataDocument">XML document with report data</param>
		protected override void AddCustomData(string reportNo, XElement xmlDataDocument)
        {
			AddDiagnosisData(reportNo, xmlDataDocument);
			AddDiagnosisComment(reportNo, xmlDataDocument);
			AddPathologistSignature(reportNo, xmlDataDocument);
			AddSurgicalResultChildItemData(reportNo, xmlDataDocument, MicroscopicDescription);
            AddAncillaryStudiesData(reportNo, xmlDataDocument);
			AddSurgicalResultChildItemData(reportNo, xmlDataDocument, ClinicalInfo);
			AddSurgicalResultChildItemData(reportNo, xmlDataDocument, GrossDescription);
			AddPrevDiagnosisData(reportNo, xmlDataDocument);
			AddPrevDiagnosisComment(reportNo, xmlDataDocument);
		}
		#endregion Protected methods

		#region Private methods
		/// <summary>method return specimen and surgical lists
		/// </summary>
		/// <param name="reportNo">report number</param>
		/// <param name="xmlDataDocument">XML element with input data</param>
		/// <param name="specimenOrderCollection">output specimen list</param>
		/// <param name="surgicalResultItemCollection">output surgical list</param>
		/// <returns>true, if output lists are successfully initialized, otherwise false</returns>
		private bool GetSurgicalAndSpecimenLists(string reportNo, XElement xmlDataDocument, string surgicalResultItemCollectionName, out XElement specimenOrderCollection, out XElement surgicalResultItemCollection)
        {
            surgicalResultItemCollection = null;
            specimenOrderCollection = xmlDataDocument.Descendants("SpecimenOrderCollection").FirstOrDefault();
            if (specimenOrderCollection == null)
            {
                Add(new XElement(ResultsCollectionName));
                return false;
            }
            XElement surgicalResultItem = GetSurgicalResultItem(reportNo, xmlDataDocument);
            if (surgicalResultItem == null)
            {
                Add(new XElement(ResultsCollectionName));
                return false;
            }
			surgicalResultItemCollection = surgicalResultItem.Descendants(surgicalResultItemCollectionName).FirstOrDefault();
            if (surgicalResultItemCollection == null)
            {
                Add(new XElement(ResultsCollectionName));
                return false;
            }
            return true;
        }
		/// <summary>method add diagnosis data
		/// </summary>
		/// <param name="reportNo">report number</param>
		/// <param name="xmlDataDocument">XML document with report data</param>
		private void AddDiagnosisData(string reportNo, XElement xmlDataDocument)
        {
            XElement specimenOrderCollection, surgicalSpecimenResultItemCollection;
			if (!GetSurgicalAndSpecimenLists(reportNo, xmlDataDocument, "SurgicalSpecimenResultItemCollection", out specimenOrderCollection, out surgicalSpecimenResultItemCollection))
			{
				Add(new XElement(DiagnosisCollectionName));
				return;
			}
			var results =
                from specimenItem in specimenOrderCollection.Elements("SpecimenOrder")
                join surgItem in surgicalSpecimenResultItemCollection.Elements("SurgicalSpecimenResultItem")
                on specimenItem.Element("SpecimenOrderId").Value equals surgItem.Element("SpecimenOrderId").Value
				select new XElement(DiagnosisName, new XElement(TestNumber, specimenItem.Element("SpecimenNumber").Value), new XElement(TestName, specimenItem.Element("Description").Value), new XElement(TestResult, surgItem.Element("Diagnosis").Value));
			Add(new XElement(DiagnosisCollectionName, results));
        }
		/// <summary>method add diagnosis comment data
		/// </summary>
		/// <param name="reportNo">report number</param>
		/// <param name="xmlDataDocument">XML element with input data</param>
		private void AddDiagnosisComment(string reportNo, XElement xmlDataDocument)
		{
			XElement surgicalResultItem = GetSurgicalResultItem(reportNo, xmlDataDocument);
			if (surgicalResultItem == null)
				Add(new XElement(DiagnosisComment));
			else
			{
				XElement diagnosisComment = surgicalResultItem.Element("Comment");
				if (diagnosisComment == null) 
					Add(new XElement(DiagnosisComment));
				else
					Add(new XElement(DiagnosisComment, diagnosisComment.Value));
			}
		}
		/// <summary>method return "SurgicalResultItem" node from input XML document or null, if this node is not found
		/// </summary>
		/// <param name="reportNo">report number</param>
		/// <param name="xmlDataDocument">XML document with report data</param>
		/// <returns>"SurgicalResultItem" node from input XML document or null, if this node is not found</returns>
        private static XElement GetSurgicalResultItem(string reportNo, XElement xmlDataDocument)
        {
			XElement result = xmlDataDocument.Descendants("PanelSetOrder").FirstOrDefault();
            return result;
        }
		/// <summary>method add data for "Ancillary Studies" section
		/// </summary>
		/// <param name="reportNo">report number</param>
		/// <param name="xmlDataDocument">XML document with report data</param>
        private void AddAncillaryStudiesData(string reportNo, XElement xmlDataDocument)
        {
            XElement specimenOrderCollection, surgicalSpecimenResultItemCollection;
			if (!GetSurgicalAndSpecimenLists(reportNo, xmlDataDocument, "SurgicalSpecimenResultItemCollection", out specimenOrderCollection, out surgicalSpecimenResultItemCollection))
			{
				Add(new XElement(AncillaryStudies));
				return;
			}
            var results =
                from surgItem in surgicalSpecimenResultItemCollection.Elements("SurgicalSpecimenResultItem").Where(i => i.Element("StainResultItemCollection") != null)
                join specimenItem in specimenOrderCollection.Elements("SpecimenOrder")
                on surgItem.Element("SpecimenOrderId").Value equals specimenItem.Element("SpecimenOrderId").Value
                select new XElement(AncillaryStudy, 
                    new XElement(TestNumber, specimenItem.Element("SpecimenNumber").Value), 
                    new XElement(TestName, specimenItem.Element("Description").Value),
					new XElement(AncillaryStudyCellNodeName[0], surgItem.Descendants("StainResultItem").FirstOrDefault() != null ? surgItem.Descendants("StainResultItem").FirstOrDefault().GetStringValue(AncillaryStudyCellNodeName[0]) : string.Empty),
					new XElement(AncillaryStudyCellNodeName[1], specimenItem.Descendants("AliquotOrder").FirstOrDefault() != null ? specimenItem.Descendants("AliquotOrder").FirstOrDefault().GetStringValue(AncillaryStudyCellNodeName[1]) : string.Empty),
					new XElement(AncillaryStudyCellNodeName[2], surgItem.Descendants("StainResultItem").FirstOrDefault() != null ? surgItem.Descendants("StainResultItem").FirstOrDefault().GetStringValue(AncillaryStudyCellNodeName[2]) : string.Empty));
            Add(new XElement(AncillaryStudies, new XElement(AncillaryStudiesHeader, m_AncillaryStudiesHeader), results));
        }
		/// <summary>method return row node for "Ancillary Study" section's table
		/// </summary>
		/// <param name="rowIndex">"Ancillary Study" section table's row index</param>
		/// <returns>row node for "Ancillary Study" section's table</returns>
        private XElement GetAncillaryStudy(int rowIndex)
        {
            IEnumerable<XElement> results = Element(AncillaryStudies).Elements(AncillaryStudy);
            XElement result = results.ElementAt(rowIndex);
            return result;
        }
		/// <summary>method add data for "Microscopic Description", "Clinical Information" or "GrossDescription" sections
		/// </summary>
		/// <param name="reportNo">report number</param>
		/// <param name="xmlDataDocument">XML document with report data</param>
		/// <param name="childNodeName">child node name for specific section</param>
		private void AddSurgicalResultChildItemData(string reportNo, XElement xmlDataDocument, string childNodeName)
		{
			XElement surgicalResultItem = GetSurgicalResultItem(reportNo, xmlDataDocument);
			if (surgicalResultItem == null)
			{
				Add(new XElement(childNodeName));
				return;
			}
			string childNodeValue = surgicalResultItem.Element(childNodeName).Value;
			if (childNodeValue == null)
			{
				Add(new XElement(childNodeName));
				return;
			}
			Add(new XElement(childNodeName, childNodeValue));
		}
		/// <summary>method add report's previous diagnosis data
		/// </summary>
		/// <param name="reportNo">report number</param>
		/// <param name="xmlDataDocument">XML document with report data</param>
		private void AddPrevDiagnosisData(string reportNo, XElement xmlDataDocument)
		{
			XElement specimenOrderCollection, surgicalSpecimenResultItemCollection;
			if (!GetSurgicalAndSpecimenLists(reportNo, xmlDataDocument, "SurgicalSpecimenResultAuditItemCollection", out specimenOrderCollection, out surgicalSpecimenResultItemCollection))
			{
				Add(new XElement(ResultsCollectionName));
				return;
			}
			//var results =
			//    from specimenItem in specimenOrderCollection.Elements("SpecimenOrder")
			//    join surgItem in surgicalSpecimenResultItemCollection.Elements("SurgicalSpecimenResultAuditItem")
			//    on specimenItem.Element("SpecimenOrderId").Value equals surgItem.Element("SpecimenOrderId").Value
			//    select new XElement(PrevDiagnosisName, new XElement(TestNumber, specimenItem.Element("SpecimenNumber").Value), new XElement(TestName, specimenItem.Element("Description").Value), new XElement(TestResult, surgItem.Element("Diagnosis").Value));
			var results1 =
				from specimenItem in specimenOrderCollection.Elements("SpecimenOrder")
				join surgItem in surgicalSpecimenResultItemCollection.Elements("SurgicalSpecimenResultAuditItem")
				on specimenItem.Element("SpecimenOrderId").Value equals surgItem.Element("SpecimenOrderId").Value
				/*join amendmentItem in xmlDataDocument.Descendants(AmendmentItemName)
				on surgItem.Element("AmendmentId") equals amendmentItem.Element("AmendmentId")
				orderby amendmentItem.Element("AmendmentTime").Value descending */
				select new XElement(PrevDiagnosisName, new XElement("AmendmentId", surgItem.Element("AmendmentId").Value), new XElement(TestNumber, specimenItem.Element("SpecimenNumber").Value), new XElement(TestName, specimenItem.Element("Description").Value), new XElement(TestResult, surgItem.Element("Diagnosis").Value));
			var result2 =
				from prevDignosisItem in results1
				join amendmentItem in xmlDataDocument.Descendants(AmendmentItemName)
				on prevDignosisItem.Element("AmendmentId") equals amendmentItem.Element("AmendmentId")
				//orderby amendmentItem.Element("AmendmentTime").Value descending 
				select new XElement(PrevDiagnosisName, new XElement("AmendmentId", prevDignosisItem.Element("AmendmentId").Value), new XElement(TestNumber, prevDignosisItem.Element("SpecimenNumber").Value), new XElement(TestName, prevDignosisItem.Element("Description").Value), new XElement(TestResult, prevDignosisItem.Element("Diagnosis").Value));
			Add(new XElement(PrevDiagnosisCollectionName, results1));
		}
		/// <summary>method add previous diagnosis comment data
		/// </summary>
		/// <param name="reportNo">report number</param>
		/// <param name="xmlDataDocument">XML element with input data</param>
		private void AddPrevDiagnosisComment(string reportNo, XElement xmlDataDocument)
		{
			XElement surgicalResultItem = GetSurgicalResultItem(reportNo, xmlDataDocument);
			if (surgicalResultItem == null)
				Add(new XElement(PrevDiagnosisComment));
			else
			{
				//XElement diagnosisComment = surgicalResultItem.Element("Comment");
				//if (diagnosisComment == null)
					Add(new XElement(PrevDiagnosisComment));
				//else
				//    Add(new XElement(PrevDiagnosisComment, diagnosisComment.Value));
			}
		}

		#endregion Private methods
	}
}
