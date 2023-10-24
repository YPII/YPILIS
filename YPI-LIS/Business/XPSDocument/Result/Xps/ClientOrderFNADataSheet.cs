using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Xml.Linq;
using System.Globalization;

namespace YellowstonePathology.Document.Result.Xps
{
	public class ClientOrderFNADataSheet
	{
		private readonly YellowstonePathology.Document.Xps.ReportDocument m_ReportDocument;
		private readonly XElement m_AccessionOrderData;
		private readonly XElement m_ClientOrderData;
		private readonly XElement m_ClientOrderFNAData;
		private XElement m_ClientOrderDetailFNAData;

		private const string m_PageTitle = "Fine Needle Aspirate Data Sheet";
		private const int m_FontSize = 12;
		private const int m_MarginSize = 3;
		private const int m_PageBorderThickness = 1;
		//private const string m_Bullet = "\x2022";
		private readonly double[] m_ClientOrderGridColWidth = new double[] { 3.45, 3.45 };
		private const int m_ClientOrderGridRowsCount = 8;
		private const int m_FNAPropertyGridRowsCount = 6;
		private const int m_FNAPropertyGridColCount = 1;
		private const int m_FNADetailPropertyGridRowsCount = 1;
		private const int m_FNADetailPropertyGridColCount = 5;
		private StackPanel m_BodyRoot;
		private TextBlock m_DocTitle;
		private Grid m_ClientOrderGrid;
		private Grid m_FNAPropertyGrid;
		private Grid m_FNADetailPropertyGrid;

		public ClientOrderFNADataSheet(XElement reportData)
		{
			m_AccessionOrderData = reportData;
			if (m_AccessionOrderData == null) throw new ApplicationException("XML data file missing \"AccessionOrder\" node");
			m_ClientOrderData = reportData.Descendants("ClientOrder").FirstOrDefault();
			if (m_ClientOrderData == null) throw new ApplicationException("XML data file missing \"ClientOrder\" node");
			m_ClientOrderFNAData = reportData.Descendants("ClientOrderFNAProperty").FirstOrDefault();
			if (m_ClientOrderFNAData == null) throw new ApplicationException("XML data file missing \"ClientOrderFNAProperty\" node");

			YellowstonePathology.Document.Xps.NoHeader header = new Document.Xps.NoHeader();
			YellowstonePathology.Document.Xps.NoFooter footer = new Document.Xps.NoFooter();
			m_ReportDocument = new Document.Xps.ReportDocument(header, footer);

			SetupDocumentLayout();
			WriteClientOrderSection();
			WriteFNAPropertySection();
		}

		public FixedDocument FixedDocument
		{
			get { return m_ReportDocument.FixedDocument; }
		}

		private void SetupDocumentLayout()
		{
			m_BodyRoot = new StackPanel();

			m_DocTitle = Document.Xps.XPSHelper.GetTextBlock(m_PageTitle, HorizontalAlignment.Center, VerticalAlignment.Top, new Thickness(m_MarginSize * 3, m_MarginSize * 3, m_MarginSize * 3, 0), m_FontSize * 1.4, null, FontWeights.Bold);
			m_BodyRoot.Children.Add(m_DocTitle);

			m_ClientOrderGrid = Document.Xps.XPSHelper.GetGrid(m_ClientOrderGridColWidth, m_ClientOrderGridRowsCount, 0.0, new Thickness(m_MarginSize * 3, 0, m_MarginSize * 3, m_MarginSize * 3));
			m_BodyRoot.Children.Add(m_ClientOrderGrid);

			m_FNAPropertyGrid = Document.Xps.XPSHelper.GetGrid(m_FNAPropertyGridRowsCount, m_FNAPropertyGridColCount, 0.0, new Thickness(m_MarginSize * 3, 0, m_MarginSize * 3, m_MarginSize * 3));
			m_BodyRoot.Children.Add(m_FNAPropertyGrid);

			m_ReportDocument.WriteRowContent(m_BodyRoot);
		}
		/// <summary>method write "Demographic History" section to document grid
		/// </summary>
		private void WriteClientOrderSection()
		{
			int row = 0;
			WriteSectionTitle(m_ClientOrderGrid, "Client Order", row++);
			string patientName = string.Format("Patient Name: {0}, {1}, {2}", GetClientOrderData("PLastName"), GetClientOrderData("PFirstName"), GetClientOrderData("PMiddleInitial"));
			WriteLabelAndTextBlock(m_ClientOrderGrid, "Patient Name:", patientName, row, 0);
			WriteLabelAndTextBlock(m_ClientOrderGrid, "Report No:", GetAccessionOrderData("ReportNo"), row++, 1);

			WriteLabelAndTextBlock(m_ClientOrderGrid, "SSN:", GetClientOrderData("PSSN"), row, 0);
			WriteLabelAndTextBlock(m_ClientOrderGrid, "Master Accession No:", GetAccessionOrderData("MasterAccessionNo"), row++, 1);

			WriteLabelAndTextBlock(m_ClientOrderGrid, "Client Name:", GetClientOrderData("ClientName"), row, 0);
			WriteLabelAndTextBlock(m_ClientOrderGrid, "Order Date:", DateTimeString(GetAccessionOrderData("AccessionDate")), row++, 1);

			WriteLabelAndTextBlock(m_ClientOrderGrid, "Provider Name:", GetClientOrderData("ProviderName"), row, 0);
			WriteLabelAndTextBlock(m_ClientOrderGrid, "Medical Record:", GetAccessionOrderData("SvhMedicalRecord"), row++, 1);

			WriteLabelAndTextBlock(m_ClientOrderGrid, "Clinical History:", GetClientOrderData("ClinicalHistory"), row, 0);
			WriteLabelAndTextBlock(m_ClientOrderGrid, "Account No:", GetAccessionOrderData("SvhAccount"), row++, 1);

			WriteLabelAndTextBlock(m_ClientOrderGrid, "PreOp Diagnosis:", GetClientOrderData("PreOpDiagnosis"), row++, 0);
			
			WriteLabelAndTextBlock(m_ClientOrderGrid, string.Empty, string.Empty, row++, 0);
			//WriteExam1Group(row++);
			//WriteExam2Group(row);
		}

		private void WriteFNAPropertySection()
		{
			int row = 0;
			WriteSectionTitle(m_FNAPropertyGrid, "FNA Properties", row++);

			WriteLabelAndTextBlock(m_FNAPropertyGrid, "Specimen Source:", GetFNAPropertyData("SpecimenSource"), row++, 0);
			WriteLabelAndTextBlock(m_FNAPropertyGrid, "Gross Description:", GetFNAPropertyData("GrossDescription"), row++, 0);
			WriteLabelAndTextBlock(m_FNAPropertyGrid, "Start Time:", DateTimeString(GetFNAPropertyData("StartTime")), row++, 0);
			WriteLabelAndTextBlock(m_FNAPropertyGrid, "End Time:", DateTimeString(GetFNAPropertyData("EndTime")), row++, 0);

			WriteLabelAndTextBlock(m_ClientOrderGrid, string.Empty, string.Empty, row++, 0);

			List<XElement> detailElements = this.m_ClientOrderFNAData.Element("ClientOrderDetailFNAPropertyCollection").Elements("ClientOrderDetailFNAProperty").ToList();
			WriteFNADetailPropertyTitles();
			foreach (XElement detailElement in detailElements)
			{
				this.m_ClientOrderDetailFNAData = detailElement;
				m_FNADetailPropertyGrid = Document.Xps.XPSHelper.GetGrid(m_FNADetailPropertyGridRowsCount, m_FNADetailPropertyGridColCount, 0.0, new Thickness(m_MarginSize * 3, 0, m_MarginSize * 3, m_MarginSize * 2 / 3));
				WriteFNADetailPropertySection();

				m_BodyRoot.Children.Add(m_FNADetailPropertyGrid);
			}
		}

		private void WriteFNADetailPropertyTitles()
		{
			m_FNADetailPropertyGrid = Document.Xps.XPSHelper.GetGrid(m_FNADetailPropertyGridRowsCount, m_FNADetailPropertyGridColCount, 0.0, new Thickness(m_MarginSize * 3, 0, m_MarginSize * 3, m_MarginSize * 2 / 3));
			WriteColumnTitle(m_FNADetailPropertyGrid, "Pass Number", 0, 0);
			WriteColumnTitle(m_FNADetailPropertyGrid, "Diff-Quik Slide Count", 0, 1);
			WriteColumnTitle(m_FNADetailPropertyGrid, "Non Gyn Slide Count", 0, 2);
			WriteColumnTitle(m_FNADetailPropertyGrid, "Time Received", 0, 3);
			WriteColumnTitle(m_FNADetailPropertyGrid, "Time Called To Provider", 0, 4);
			m_BodyRoot.Children.Add(m_FNADetailPropertyGrid);
		}

		private void WriteFNADetailPropertySection()
		{
			Document.Xps.XPSHelper.WriteTextBlockToGrid(GetFNADetailPropertyData("PassNo"), m_FNADetailPropertyGrid, 0, 0, HorizontalAlignment.Stretch, VerticalAlignment.Top, new Thickness(m_MarginSize, 1, m_MarginSize,1), m_FontSize);
			Document.Xps.XPSHelper.WriteTextBlockToGrid(GetFNADetailPropertyData("DiffQuikSlideCount"), m_FNADetailPropertyGrid, 0, 1, HorizontalAlignment.Stretch, VerticalAlignment.Top, new Thickness(m_MarginSize, 1, m_MarginSize, 1), m_FontSize);
			Document.Xps.XPSHelper.WriteTextBlockToGrid(GetFNADetailPropertyData("PapSlideCount"), m_FNADetailPropertyGrid, 0, 2, HorizontalAlignment.Stretch, VerticalAlignment.Top, new Thickness(m_MarginSize, 1, m_MarginSize, 1), m_FontSize);
			Document.Xps.XPSHelper.WriteTextBlockToGrid(DateTimeString(GetFNADetailPropertyData("TimeReceived")), m_FNADetailPropertyGrid, 0, 3, HorizontalAlignment.Stretch, VerticalAlignment.Top, new Thickness(m_MarginSize, 1, m_MarginSize, 1), m_FontSize);
			Document.Xps.XPSHelper.WriteTextBlockToGrid(DateTimeString(GetFNADetailPropertyData("TimeCalledToProvider")), m_FNADetailPropertyGrid, 0, 4, HorizontalAlignment.Stretch, VerticalAlignment.Top, new Thickness(m_MarginSize, 1, m_MarginSize, 1), m_FontSize);
		}

		private string DateTimeString(string value)
		{
			DateTime date;
			string dateString = string.Empty;
			string timeString = string.Empty;

			bool parsedOK = DateTime.TryParse(value, new CultureInfo("en-US").DateTimeFormat, DateTimeStyles.None, out date);
			if (parsedOK)
			{
				dateString = date.ToString("MM/dd/yyyy");
				if (date.Hour != 0 || date.Minute != 0)
				{
					timeString = date.ToString("HH:mm");
				}
			}

			return dateString + " " + timeString;
		}

		private string GetAccessionOrderData(string xmlElementName)
		{
			return m_AccessionOrderData.Element(xmlElementName).Value;
		}

		private string GetClientOrderData(string xmlElementName)
		{
			return m_ClientOrderData.Element(xmlElementName).Value;
		}

		private string GetFNAPropertyData(string xmlElementName)
		{
			return m_ClientOrderFNAData.Element(xmlElementName).Value;
		}

		private string GetFNADetailPropertyData(string xmlElementName)
		{
			return m_ClientOrderDetailFNAData.Element(xmlElementName).Value;
		}

		private static void WriteSectionTitle(Grid baseGrid, string title, int rowNo)
		{
			Document.Xps.XPSHelper.WriteTextBlockToGrid(title, baseGrid, rowNo, 0, HorizontalAlignment.Left, VerticalAlignment.Top, new Thickness(m_MarginSize, m_MarginSize, m_MarginSize, m_MarginSize), m_FontSize, null, FontWeights.Bold, true, true, 0, baseGrid.ColumnDefinitions.Count);
		}

		private static void WriteColumnTitle(Grid baseGrid, string title, int rowNo, int colNo)
		{
			Document.Xps.XPSHelper.WriteTextBlockToGrid(title, baseGrid, rowNo, colNo, HorizontalAlignment.Left, VerticalAlignment.Top, new Thickness(m_MarginSize, m_MarginSize, m_MarginSize, m_MarginSize), m_FontSize, null, FontWeights.Bold, false, true);
		}

		private static void WriteLabelAndTextBlock(Grid baseGrid, string labelText, string value, int rowNo, int colNo, bool addBullet = false, int colSpan = 0)
		{
			Grid grid = Document.Xps.XPSHelper.GetGrid(new GridLength[] { GridLength.Auto, new GridLength(1, GridUnitType.Star) }, 1);
			Document.Xps.XPSHelper.WriteItemToGrid(grid, baseGrid, rowNo, colNo, 0, colSpan);
			string fullLabelText = string.Format("{0}{1}", "", labelText);
			Document.Xps.XPSHelper.WriteTextBlockToGrid(fullLabelText, grid, 0, 0, HorizontalAlignment.Left, VerticalAlignment.Top, new Thickness(m_MarginSize, m_MarginSize + 1, m_MarginSize, m_MarginSize - 1), m_FontSize);
			Document.Xps.XPSHelper.WriteTextBlockToGrid(value, grid, 0, 1, HorizontalAlignment.Stretch, VerticalAlignment.Top, new Thickness(m_MarginSize, m_MarginSize, m_MarginSize, m_MarginSize), m_FontSize);
		}
	}
}
