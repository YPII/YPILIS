using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Text;

namespace YellowstonePathology.Document.Result.Data
{
	/// <summary>Cystic Fibrosis Carrier Screening report data class
	/// </summary>
	public class CysticFibrosisReportData : YpReportDataBase
	{

		#region Private constants
		/// <summary>test name in result section
		/// </summary>
		private const string m_TestName = "CF Carrier Screening:";
		/// <summary>"Interpretation" section's text over reference table
		/// </summary>
		private const string m_InterpretationRefTableTextData =
			"\nThe table below provides data to be used in the risk assessment of this individual.  Note that the detection " +
			"rate and the CF carrier risk are not available for individuals from other ethnic populations.";
		/// <summary>"Interpretation" section's reference table headers
		/// </summary>
		private readonly string[] m_InterpretationRefTableHeaderData = new string[] {"*Ethnic Group", "Detection Rate", "Before Test", "After Negative Test" };
		/// <summary>"Interpretation" section's reference table rows
		/// </summary>
		private readonly string[,] m_InterpretationRefTableData = new string[,] 
		{
			{ "Ashkenazi Jewish", "94%", "1/24", "1/400" },
			{ "European Caucasian",	"88%", "1/25", "1/208" },
			{ "African American", "65%", "1/65", "1/186" },
			{ "Hispanic American", "72%", "1/46", "1/164" },
			{ "Asian American", "49%", "1/94", "1/184" }
		};
		/// <summary>"Method" section's body text
		/// </summary>
		private const string m_MethodData =
			"DNA was extracted from the patient's specimen.  Multiplex PCR was performed to amplify the target genes.  " +
			"Enzymatic digestion was performed to generate single stranded DNA.  Sample DNA was hybridized to capture " +
			"probes with signal detection by alternating current voltammetry.";
		/// <summary>"References" section's body text
		/// </summary>
		private const string m_ReferencesData = "*Obstectrics and Gynecology (2005) 106:1465";
		#endregion Private constants

		#region Private data
		/// <summary>"Interpretation" section's reference table rows count
		/// </summary>
		private int m_InterpretationRefTableDataRowsCount;
		/// <summary>"Interpretation" section's reference table columns count
		/// </summary>
		private int m_InterpretationRefTableDataColsCount;
		#endregion Private data

		#region Public constants
		/// <summary>name of XML element with "Mutations Tested" list
		/// </summary>
		private static readonly string MutationsTested = "MutationsTested";
		/// <summary>name of XML element with "Interpretation" section's reference table paragraph
		/// </summary>
		private static readonly string InterpretationRefTable = "InterpretationRefTable";
		/// <summary>name of XML element with "Interpretation" section's text over reference table
		/// </summary>
		private static readonly string InterpretationRefHeaderTableText = "HeaderText";
		/// <summary>name of XML element with "Interpretation" section's reference table headers collection
		/// </summary>
		private static readonly string InterpretationRefTableHeadersCollection = "HeadersCollection";
		/// <summary>name of XML element with "Interpretation" section's reference table header
		/// </summary>
		private static readonly string InterpretationRefTableHeader = "Header";
		/// <summary>name of XML element with "Interpretation" section's reference table rows collection
		/// </summary>
		private static readonly string InterpretationRefTableRowsCollection = "RowsCollection";
		/// <summary>name of XML element with "Interpretation" section's reference table row
		/// </summary>
		private static readonly string InterpretationRefTableRow = "Row";
		/// <summary>names of XML elements with "Interpretation" section's reference table columns
		/// </summary>
		private static readonly string[] InterpretationRefTableColumns = new string[] { "EthnicGroup", "DetectionRate", "BeforeTest", "AfterNegativeTest" };
		#endregion Public constants

		#region Constructors
		/// <summary>constructor with report number and XML data block
		/// </summary>
		/// <param name="reportNo">report number</param>
		/// <param name="inDocument">XML element with input data</param>
		public CysticFibrosisReportData(string reportNo, XElement xmlDataDocument)
			: base(reportNo, xmlDataDocument, "CysticFibrosisReport")
        {
		}
		#endregion Constructors

		#region Public properties
		/// <summary>property return true, Interpretation Reference table section should be shown
		/// </summary>
		public bool IsInterpretationRefTableVisible
		{
			get { return (!this.GetStringValue(YpReportDataBase.TestResult).Contains("mutation was detected")); }
		}
		/// <summary>property return header text for "Interpretation reference table" section
		/// </summary>
		public string InterpretationRefTableHeaderText
		{
			get { return (Element(InterpretationRefTable).GetStringValue(InterpretationRefHeaderTableText)); }
		}
		/// <summary>property return list of column headers for "Interpretation reference table" section
		/// </summary>
		public IEnumerable<XElement> InterpretationRefTableColHeaders
		{
			get { return (Element(InterpretationRefTable).Element(InterpretationRefTableHeadersCollection).Elements(InterpretationRefTableHeader)); }
		}
		/// <summary>property return rows count for "Interpretation reference table" section
		/// </summary>
		public int InterpretationRefTableRowsCount
		{
			get { return m_InterpretationRefTableDataRowsCount; }
		}
		/// <summary>property return columns count for "Interpretation reference table" section
		/// </summary>
		public int InterpretationRefTableColsCount
		{
			get { return m_InterpretationRefTableDataColsCount; }
		}
		/// <summary>property return body text for "Mutations Tested" section
		/// </summary>
		public string MutationsTestedText
		{
			get { return (this.GetStringValue(MutationsTested).Replace("Delta", "∆")); }
		}
		/// <summary>property return xml raw value for "Mutations Tested" section
		/// </summary>
		public string MutationsTestedXml
		{
			get { return (this.Element(MutationsTested).Value); }
		}
		#endregion Public properties

		#region Public methods
		/// <summary>method return cell text of interpretation reference table
		/// 
		/// </summary>
		/// <param name="row">cell row index</param>
		/// <param name="column">cell column index</param>
		/// <returns></returns>
		public string GetInterpretationRefTableCellText(int row, int column)
		{
			return m_InterpretationRefTableData[row, column];
		}
		#endregion Public methods

		#region Protected methods
		/// <summary>method add custom report data
		/// </summary>
		/// <param name="xmlDataDocument">XML document with report data</param>
		protected override void AddCustomData(string reportNo, XElement xmlDataDocument)
		{
			AddTestName(m_TestName);
			AddTestResult(reportNo, xmlDataDocument);
			AddResultComment(reportNo, xmlDataDocument, "Report Comment");
			AddPathologistSignature(reportNo, xmlDataDocument);
			AddSpecimen(xmlDataDocument);
			AddInterpretation(reportNo, xmlDataDocument);
			AddInterpretationRefTableData();
			AddMethod(m_MethodData);
			AddReferences(m_ReferencesData);
			AddMutationsTestedData(xmlDataDocument);
		}
		#endregion Protected methods

		#region Private methods
		/// <summary>method add child element as string representation of array to this element from source element descendands array or empty child element, 
		/// if source child element is not found 
		/// </summary>
		/// <param name="e">XElement instance</param>
		private void AddMutationsTestedData(XElement srcParentElement)
		{
			XElement srcElement = srcParentElement.Descendants("TestOrderItemList").FirstOrDefault();
			if (srcElement != null)
			{
				IEnumerable<XElement> reportLst = srcElement.Elements("TestOrderListItem");
				StringBuilder mutaionsBuffer = new StringBuilder();
				for (int i = 0; i < reportLst.Count(); i++)
				{
					if (i > 0) mutaionsBuffer.Append(", ");
					mutaionsBuffer.Append(reportLst.ElementAt(i).GetStringValue("TestName"));
				}
				string mutaions = mutaionsBuffer.ToString();
				this.AddChildElement(MutationsTested, mutaions);
			}
			else
				this.AddChildElement(MutationsTested);
		}
		/// <summary>method add child element with Interpretation reference table content 
		/// </summary>
		private void AddInterpretationRefTableData()
		{
			int i, j;

			XElement root = this.AddChildElement(InterpretationRefTable);
			root.AddChildElement(InterpretationRefHeaderTableText, m_InterpretationRefTableTextData);
			XElement headersCollection = root.AddChildElement(InterpretationRefTableHeadersCollection);
			for (i = 0; i < m_InterpretationRefTableHeaderData.GetLength(0); i++)
			{
				headersCollection.AddChildElement(InterpretationRefTableHeader, m_InterpretationRefTableHeaderData[i]);
			}
			XElement rowsCollection = root.AddChildElement(InterpretationRefTableRowsCollection);
			m_InterpretationRefTableDataRowsCount = m_InterpretationRefTableData.GetLength(0);
			for (i = 0; i < m_InterpretationRefTableDataRowsCount; i++)
			{
				XElement row = rowsCollection.AddChildElement(InterpretationRefTableRow);
				m_InterpretationRefTableDataColsCount = m_InterpretationRefTableData.GetLength(1);
				for (j = 0; j < m_InterpretationRefTableDataColsCount; j++)
				{
					row.AddChildElement(InterpretationRefTableColumns[j], m_InterpretationRefTableData[i, j]);
				}
			}
		}
		#endregion Private methods

	}
}
