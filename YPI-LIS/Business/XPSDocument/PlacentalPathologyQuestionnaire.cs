using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Xml.Linq;
using System.Globalization;

using YellowstonePathology.Document.Xps;

namespace YellowstonePathology.Document
{
	/// <summary>Placental Pathology Questionnaire report class
	/// </summary>
	public class PlacentalPathologyQuestionnaire
	{
		#region Private data
		/// <summary>report document
		/// </summary>
		private readonly ReportDocument m_ReportDocument;
		/// <summary>data file's "PlacentalPathologyQuestionnaire" XML node
		/// </summary>
		private readonly Result.Data.PlacentalPathologyQuestionnaireData m_PlacentalPathologyData;
		/// <summary>data file's "ClientOrder" XML node
		/// </summary>
		//private readonly XElement m_ClientOrderData;
		/// <summary>document body container
		/// </summary>
		private StackPanel m_BodyRoot;
		/// <summary>document title
		/// </summary>
		private TextBlock m_DocTitle;
		/// <summary>patient name
		/// </summary>
		private TextBlock m_PatientName;
		/// <summary>"Demographic history" section grid 
		/// </summary>
		private Grid m_DemographicGrid;
		/// <summary>"Indications for Placental Pathology" section grid 
		/// </summary>
		private Grid m_IndicationsGrid;
		/// <summary>"Date/Time/Sign" section grid 
		/// </summary>
		private Grid m_DateTimeSignGrid;
		#endregion Private data

		#region Private constants
		/// <summary>page title
		/// </summary>
		private const string m_PageTitle = "PLACENTAL PATHOLOGY QUESTIONNAIRE";
		/// <summary>body text font size
		/// </summary>
		private const int m_FontSize = 12;
		/// <summary>body text margin size
		/// </summary>
		private const int m_MarginSize = 3;
		/// <summary>page border thickness
		/// </summary>
		private const int m_PageBorderThickness = 1;
		/// <summary>bullet symbol
		/// </summary>
		private const string m_Bullet = "\x2022";
		/// <summary>"Demographic history" grid columns width array, inches
		/// </summary>
		private readonly double[] m_DemographicGridColWidth = new double[] { 4.0, 2.25 };
		/// <summary>"Demographic history" grid rows count
		/// </summary>
		private const int m_DemographicGridRowsCount = 9;
		/// <summary>"Indications for Placental Pathology" grid rows count
		/// </summary>
		private const int m_IndicationsGridRowsCount = 20;
		/// <summary>"Indications for Placental Pathology" grid columns count
		/// </summary>
		private const int m_IndicationsGridColCount = 3;
		/// <summary>"Date"/"Time" columns width, inches
		/// </summary>
		private const double DateTimeColWidth = 1.2;
		#endregion Private constants

		#region Constructors
		/// <summary>default constructor
		/// </summary>
		/// <param name="reportData">report XML data</param>
		public PlacentalPathologyQuestionnaire(Result.Data.PlacentalPathologyQuestionnaireData reportData)
		{
			m_PlacentalPathologyData = reportData;
			if (m_PlacentalPathologyData == null) throw new ApplicationException("XML data file have not \"PlacentalPathologyQuestionnaire\" node");			

			NoHeader header = new NoHeader();
			NoFooter footer = new NoFooter();
			m_ReportDocument = new ReportDocument(header, footer);

			SetupDocumentLayout();
			WriteDemographicHistorySection();
			WriteIndicationsSection();
			WriteDateTimeSignSection();
		}
		#endregion Constructors

		#region Public properties
		/// <summary>property return report document
		/// </summary>
		public FixedDocument FixedDocument
		{
			get { return m_ReportDocument.FixedDocument; }
		}
		#endregion Public properties

		#region Private methods
		/// <summary>method initilize document grid
		/// </summary>
		private void SetupDocumentLayout()
		{
			Border bodyBorder = XPSHelper.GetBorder(HorizontalAlignment.Stretch, VerticalAlignment.Stretch, new Thickness(m_MarginSize * 3, m_MarginSize * 3, m_MarginSize * 3, m_MarginSize * 3), null, new Thickness(m_PageBorderThickness));
			m_BodyRoot = new StackPanel();
			bodyBorder.Child = m_BodyRoot;

			m_DocTitle = XPSHelper.GetTextBlock(m_PageTitle, HorizontalAlignment.Center, VerticalAlignment.Top,  new Thickness(m_MarginSize * 3, m_MarginSize * 3, m_MarginSize * 3, 0), m_FontSize * 1.4, null, FontWeights.Bold);
			m_BodyRoot.Children.Add(m_DocTitle);
			
			string patientName = string.Format("Patient Name: {0}, {1}", this.m_PlacentalPathologyData.PLastName, this.m_PlacentalPathologyData.PFirstName);
			m_PatientName = XPSHelper.GetTextBlock(patientName, HorizontalAlignment.Center, VerticalAlignment.Top, new Thickness(m_MarginSize * 3, m_MarginSize, m_MarginSize * 3, m_MarginSize * 3), m_FontSize * 1.4, null, FontWeights.Bold);
			m_BodyRoot.Children.Add(m_PatientName);
			
			m_DemographicGrid = XPSHelper.GetGrid(m_DemographicGridColWidth, m_DemographicGridRowsCount, 0.0, new Thickness(m_MarginSize * 3, 0, m_MarginSize * 3, m_MarginSize * 3));
			m_BodyRoot.Children.Add(m_DemographicGrid);
	
			m_IndicationsGrid = XPSHelper.GetGrid(m_IndicationsGridRowsCount, m_IndicationsGridColCount, 0.0, new Thickness(m_MarginSize * 3, 0, m_MarginSize * 3, m_MarginSize * 3));
			m_BodyRoot.Children.Add(m_IndicationsGrid);

			GridLength[] gridLegths = new GridLength[] { new GridLength(DateTimeColWidth * ReportPage.DisplayResolution), new GridLength(DateTimeColWidth * ReportPage.DisplayResolution), new GridLength(1, GridUnitType.Star), GridLength.Auto };
			m_DateTimeSignGrid = XPSHelper.GetGrid(gridLegths, 1, 0.0, new Thickness(m_MarginSize * 3, m_MarginSize * 3, m_MarginSize * 6, m_MarginSize * 3));
			m_BodyRoot.Children.Add(m_DateTimeSignGrid);

			m_ReportDocument.WriteBorder(bodyBorder);
		}
		/// <summary>method write "Demographic History" section to document grid
		/// </summary>
		private void WriteDemographicHistorySection()
		{
			int row = 0;
			WriteSectionTitle(m_DemographicGrid, "Demographic History", row++);
			WriteLabelAndTextBlock(m_DemographicGrid, "Date of Birth", this.m_PlacentalPathologyData.Birthdate, row, 0);
			WriteLabelAndTextBlock(m_DemographicGrid, "Time of Birth", this.m_PlacentalPathologyData.BirthTime, row++, 1);
			WriteLabelAndTextBlock(m_DemographicGrid, "Time of Placenta Delivery", this.m_PlacentalPathologyData.PlacentaDeliveryTime, row, 0);
			WriteLabelAndTextBlock(m_DemographicGrid, "Gestation Age", this.m_PlacentalPathologyData.GestationalAge, row++, 1);
			WriteGravidaParaGroup(row);
			WriteLabelAndTextBlock(m_DemographicGrid, "Abortion", this.m_PlacentalPathologyData.Abortion.ToString(), row++, 1);
			WriteApgarGroup(row);
			WriteLabelAndTextBlock(m_DemographicGrid, "Infant Weight", this.m_PlacentalPathologyData.InfantWeight, row++, 1);
			WriteLabelAndTextBlock(m_DemographicGrid, "Specific Questions:", this.m_PlacentalPathologyData.SpecificQuestions, row++, 0, true, 2);
			WriteLabelAndTextBlock(m_DemographicGrid, string.Empty, string.Empty, row++, 0, false, 2);
			WriteExam1Group(row++);
			WriteExam2Group(row);
		}
		/// <summary>method write "Indications for Placental Pathology" section to document grid
		/// </summary>
		private void WriteIndicationsSection()
		{
			int row = 0;
			WriteSectionTitle(m_IndicationsGrid, "Indications for Placental Pathology", row++);

			WriteColumnTitle(m_IndicationsGrid, "Maternal Conditions", row, 0);
			WriteColumnTitle(m_IndicationsGrid, "Fetal/Neonatal Conditions", row, 1);
			WriteColumnTitle(m_IndicationsGrid, "Umbilical Cord/Placental Conditions", row++, 2);

			WriteCheckBox(m_IndicationsGrid, "Diabetes Mellitus", this.m_PlacentalPathologyData.DiabetesMellitus, row, 0);
			WriteCheckBox(m_IndicationsGrid, "Suspected Infection", this.m_PlacentalPathologyData.SuspectedInfection, row, 1);
			WriteCheckBox(m_IndicationsGrid, "Infarcts", this.m_PlacentalPathologyData.Infarcts, row++, 2);

			WriteCheckBox(m_IndicationsGrid, "Pregnancy Induced Hypertension", this.m_PlacentalPathologyData.PregnancyInducedHypertension, row, 0);
			WriteCheckBox(m_IndicationsGrid, "Stillborn", this.m_PlacentalPathologyData.Stillborn, row, 1);
			WriteCheckBox(m_IndicationsGrid, "Abnormal Calcifications", this.m_PlacentalPathologyData.AbnormalCalcifications, row++, 2);

			WriteCheckBox(m_IndicationsGrid, "Unexplained Fever", this.m_PlacentalPathologyData.UnexplainedFever, row, 0);
			WriteCheckBox(m_IndicationsGrid, "Erythroblastosis Fetalis", this.m_PlacentalPathologyData.ErythroblastosisFetalis, row, 1);
			WriteCheckBox(m_IndicationsGrid, "Abruption", this.m_PlacentalPathologyData.Abruption, row++, 2);

			WriteCheckBox(m_IndicationsGrid, "Premature Rupture of Membranes", this.m_PlacentalPathologyData.PrematureRuptureOfMembranes, row, 0);
			WriteCheckBox(m_IndicationsGrid, "Neonatal Death", this.m_PlacentalPathologyData.NeonatalDeath, row, 1);
			WriteCheckBox(m_IndicationsGrid, "Placenta Previa", this.m_PlacentalPathologyData.PlacentaPrevia, row++, 2);

			WriteCheckBox(m_IndicationsGrid, "Poor or Limited Prenatal Care", this.m_PlacentalPathologyData.PoorOrLimitedPrenatalCare, row, 0);
			WriteCheckBox(m_IndicationsGrid, "Transfer to NICU", this.m_PlacentalPathologyData.TransferToNICU, row, 1);
			WriteCheckBox(m_IndicationsGrid, "Vasa Previa", this.m_PlacentalPathologyData.VasaPrevia, row++, 2);

			WriteCheckBox(m_IndicationsGrid, "Polyhydramnios", this.m_PlacentalPathologyData.Polyhydramnios, row, 0);
			WriteCheckBox(m_IndicationsGrid, "Viscid or Thick Meconium", this.m_PlacentalPathologyData.ViscidOrThickMeconium, row, 1);
			WriteCheckBox(m_IndicationsGrid, "Abnormal Cord Appearance", this.m_PlacentalPathologyData.AbnormalCordAppearance, row++, 2);

			WriteCheckBox(m_IndicationsGrid, "Oligohydramnios", this.m_PlacentalPathologyData.Oligohydramnios, row, 0);
			WriteCheckBox(m_IndicationsGrid, "Multiple Gestation", this.m_PlacentalPathologyData.MultipleGestation, row, 1);
			WriteCheckBox(m_IndicationsGrid, "Mass", this.m_PlacentalPathologyData.Mass, row++, 2);

			WriteCheckBox(m_IndicationsGrid, "Preterm Delivery (less than 36 weeks)", this.m_PlacentalPathologyData.PretermDeliveryLessThan36Weeks, row, 0);
			WriteCheckBox(m_IndicationsGrid, "Ominous FHR Tracing", this.m_PlacentalPathologyData.OminousFHRTracing, row, 1);
			WriteCheckBoxAndTextBlock(m_IndicationsGrid, "Other:", this.m_PlacentalPathologyData.Other3, row++, 2);

			WriteCheckBox(m_IndicationsGrid, "Post Term Delivery (more than 42 weeks)", this.m_PlacentalPathologyData.PostTermDeliveryMoreThan42Weeks, row, 0);
			WriteCheckBox(m_IndicationsGrid, "Prematurity", this.m_PlacentalPathologyData.Prematurity, row, 1);
			WriteCheckBoxAndTextBlock(m_IndicationsGrid, string.Empty, string.Empty, row++, 2);

			WriteCheckBox(m_IndicationsGrid, "Infection", this.m_PlacentalPathologyData.Infection, row, 0);
			WriteCheckBox(m_IndicationsGrid, "IUGR", this.m_PlacentalPathologyData.IUGR, row, 1);
			WriteCheckBoxAndTextBlock(m_IndicationsGrid, string.Empty, string.Empty, row++, 2);

			WriteCheckBox(m_IndicationsGrid, "Postpartum Hemorrhage", this.m_PlacentalPathologyData.PostpartumHemorrhage, row, 0);
			WriteCheckBox(m_IndicationsGrid, "ApgarLess than 7 at 5 Min", this.m_PlacentalPathologyData.ApgarLessThan7at5Min, row, 1);
			WriteCheckBoxAndTextBlock(m_IndicationsGrid, string.Empty, string.Empty, row++, 2);

			WriteCheckBox(m_IndicationsGrid, "Maternal History of Reproductive Failure", this.m_PlacentalPathologyData.MaternalHistoryOfReproductiveFailure, row, 0);
			WriteCheckBox(m_IndicationsGrid, "Cord pH less than 7 dot 10", this.m_PlacentalPathologyData.CordpHLessThan7dot10, row, 1);
			WriteCheckBoxAndTextBlock(m_IndicationsGrid, string.Empty, string.Empty, row++, 2);

			WriteCheckBox(m_IndicationsGrid, "Severe Preeclampsia", this.m_PlacentalPathologyData.SeverePreeclampsia, row, 0);
			WriteCheckBox(m_IndicationsGrid, "Congenital Anomalies", this.m_PlacentalPathologyData.CongenitalAnomalies, row++, 1);

			WriteCheckBox(m_IndicationsGrid, "Suspected Drug Use", this.m_PlacentalPathologyData.SuspectedDrugUse, row, 0);
			WriteCheckBox(m_IndicationsGrid, "Neonatal Seizures", this.m_PlacentalPathologyData.NeonatalSeizures, row++, 1);

			WriteCheckBoxAndTextBlock(m_IndicationsGrid, "Other:", this.m_PlacentalPathologyData.Other1, row, 0);
			WriteCheckBoxAndTextBlock(m_IndicationsGrid, "Other:", this.m_PlacentalPathologyData.Other2, row++, 1);

			WriteCheckBoxAndTextBlock(m_IndicationsGrid, string.Empty, string.Empty, row, 0);
			WriteCheckBoxAndTextBlock(m_IndicationsGrid, string.Empty, string.Empty, row++, 1);

		}
		/// <summary>method write "Date/Time/Sign" section to document grid
		/// </summary>
		private void WriteDateTimeSignSection()
		{
			DateTime date;
			string dateString = string.Empty;
			string timeString = string.Empty;

			bool parsedOK = DateTime.TryParse(this.m_PlacentalPathologyData.DateSubmitted, new CultureInfo("en-US").DateTimeFormat, DateTimeStyles.None, out date);
			if (parsedOK)
			{
				dateString = date.ToString("MM/dd/yyyy");
				timeString = date.ToString("HH:mm:ss");
			}

			WriteLabelAndTextBlock(m_DateTimeSignGrid, "Date", dateString, 0, 0, false);
			WriteLabelAndTextBlock(m_DateTimeSignGrid, "Time", timeString, 0, 1, false);
			WriteLabelAndTextBlock(m_DateTimeSignGrid, "Provider's signature", this.m_PlacentalPathologyData.SubmittedBy, 0, 2, false);
		}

		/// <summary>method write section title to document grid
		/// </summary>
		/// <param name="baseGrid">base grid</param>
		/// <param name="title">section title text</param>
		/// <param name="rowNo">base grid row number of section title</param>
		private static void WriteSectionTitle(Grid baseGrid, string title, int rowNo)
		{
			XPSHelper.WriteTextBlockToGrid(title, baseGrid, rowNo, 0, HorizontalAlignment.Left, VerticalAlignment.Top, new Thickness(m_MarginSize, m_MarginSize, m_MarginSize, m_MarginSize), m_FontSize, null, FontWeights.Bold, true, true, 0, baseGrid.ColumnDefinitions.Count);
		}
		/// <summary>method write column title to document grid
		/// </summary>
		/// <param name="baseGrid">parent grid</param>
		/// <param name="title">section title text</param>
		/// <param name="rowNo">parent grid row number of section title</param>
		/// <param name="colNo">parent grid column number of section title</param> 
		private static void WriteColumnTitle(Grid baseGrid, string title, int rowNo, int colNo)
		{
			XPSHelper.WriteTextBlockToGrid(title, baseGrid, rowNo, colNo, HorizontalAlignment.Left, VerticalAlignment.Top, new Thickness(m_MarginSize, m_MarginSize, m_MarginSize, m_MarginSize), m_FontSize, null, FontWeights.Bold, false, true);
		}
		/// <summary>method write block with label and value to document grid
		/// </summary>
		/// <param name="baseGrid">block's parent grid</param>
		/// <param name="labelText">string value in label part of block</param>
		/// <param name="value">string value in text part of block</param>
		/// <param name="rowNo">parent grid row number of block</param>
		/// <param name="colNo">parent grid column number of block</param>
		/// <param name="addBullet">if true, then bullet added before label part of block</param>
		/// <param name="colSpan">ColSpan parameter of parent grid cell</param>
		private static void WriteLabelAndTextBlock(Grid baseGrid, string labelText, string value, int rowNo, int colNo, bool addBullet = true, int colSpan = 0)
		{
			Grid grid = XPSHelper.GetGrid(new GridLength[] { GridLength.Auto, new GridLength(1, GridUnitType.Star) }, 1);
			XPSHelper.WriteItemToGrid(grid, baseGrid, rowNo, colNo, 0, colSpan);
			string fullLabelText = string.Format("{0}{1}", (addBullet ? string.Format("{0} ", m_Bullet) : ""), labelText);
			XPSHelper.WriteTextBlockToGrid(fullLabelText, grid, 0, 0, HorizontalAlignment.Left, VerticalAlignment.Top, new Thickness(m_MarginSize, m_MarginSize + 1, m_MarginSize, m_MarginSize - 1), m_FontSize);
			XPSHelper.WriteTextBlockToGrid(value, grid, 0, 1, HorizontalAlignment.Stretch, VerticalAlignment.Top, new Thickness(m_MarginSize, m_MarginSize, m_MarginSize, m_MarginSize), m_FontSize);
			XPSHelper.WriteUnderliningToGridCell(grid, 0, 1);
		}
		/// <summary>method write check box to document grid
		/// </summary>
		/// <param name="baseGrid">block's parent grid</param>
		/// <param name="labelText">label text</param>
		/// <param name="stringXMLElementName">XML element name with check box value</param>
		/// <param name="rowNo">parent grid row number of block</param>
		/// <param name="colNo">parent grid column number of block</param>
		private void WriteCheckBox(Grid baseGrid, string labelText, bool isChecked, int rowNo, int colNo)
		{
			//bool isChecked = (stringXMLElementName != string.Empty ? bool.Parse(m_PlacentalPathologyData.Element(stringXMLElementName).Value) : false);
			XPSHelper.WriteCheckBox(labelText, isChecked, baseGrid, rowNo, colNo, HorizontalAlignment.Left, VerticalAlignment.Top, new Thickness(m_MarginSize, m_MarginSize * 2, m_MarginSize * 4, m_MarginSize), m_FontSize, null, FontWeights.Normal);
		}
		/// <summary>method write block with check box and text block to document grid
		/// </summary>
		/// <param name="baseGrid">block's parent grid</param>
		/// <param name="labelText">label of check box</param>
		/// <param name="stringXMLElementName">XML element name with text block value</param>
		/// <param name="rowNo">parent grid row number of block</param>
		/// <param name="colNo">parent grid column number of block</param>
		private void WriteCheckBoxAndTextBlock(Grid baseGrid, string labelText, string textValue, int rowNo, int colNo)
		{
			//string textValue = (stringXMLElementName != string.Empty ? m_PlacentalPathologyData.Element(stringXMLElementName).Value : string.Empty);
			Grid grid = XPSHelper.GetGrid(new GridLength[] { GridLength.Auto, new GridLength(1, GridUnitType.Star) }, 1, 0.0, new Thickness(0, 0, m_MarginSize * 4, 0));
			XPSHelper.WriteItemToGrid(grid, baseGrid, rowNo, colNo);
			XPSHelper.WriteCheckBox(labelText, (textValue != string.Empty), grid, 0, 0, HorizontalAlignment.Left, VerticalAlignment.Top, new Thickness(m_MarginSize, m_MarginSize * 2, m_MarginSize, m_MarginSize), m_FontSize, null, FontWeights.Normal);
			XPSHelper.WriteTextBlockToGrid(textValue, grid, 0, 1, HorizontalAlignment.Stretch, VerticalAlignment.Top, new Thickness(m_MarginSize, m_MarginSize * 2, m_MarginSize, 0), m_FontSize);
			XPSHelper.WriteUnderliningToGridCell(grid, 0, 1);
		}
		/// <summary>method write "Gravida/Para" group to "Democratic History" section
		/// </summary>
		/// <param name="rowNo">base grid row number of group</param>
		private void WriteGravidaParaGroup(int rowNo)
		{
			double col1Width = m_DemographicGridColWidth[0] * 2 / 3;
			double col2Width = m_DemographicGridColWidth[0] - col1Width;
			Grid grid = XPSHelper.GetGrid(new double[] { col1Width, col2Width }, 1);
			XPSHelper.WriteItemToGrid(grid, m_DemographicGrid, rowNo, 0);
			WriteLabelAndTextBlock(grid, "Gravida", this.m_PlacentalPathologyData.Gravida, 0, 0);
			WriteLabelAndTextBlock(grid, "Para", this.m_PlacentalPathologyData.Para, 0, 1);
		}
		/// <summary>method write "Apgar" group to "Democratic History" section
		/// </summary>
		/// <param name="rowNo">base grid row number of group</param>
		private void WriteApgarGroup(int rowNo)
		{
			Grid grid = XPSHelper.GetGrid(new double[] { 1.3, 0.9, 1.0 }, 1);
			XPSHelper.WriteItemToGrid(grid, m_DemographicGrid, rowNo, 0);
			WriteLabelAndTextBlock(grid, "Apgar 1 min", this.m_PlacentalPathologyData.Apgar1Min, 0, 0);
			WriteLabelAndTextBlock(grid, "5 min", this.m_PlacentalPathologyData.Apgar5Min, 0, 1, false);
			WriteLabelAndTextBlock(grid, "10 min", this.m_PlacentalPathologyData.Apgar10Min, 0, 2, false);
		}
		/// <summary>method write "Gross/Complete Exam" group to "Democratic History" section
		/// </summary>
		/// <param name="rowNo">base grid row number of group</param>
		private void WriteExam1Group(int rowNo)
		{
			Grid grid = XPSHelper.GetGrid(new GridLength[] { GridLength.Auto, new GridLength(1, GridUnitType.Star) }, 1);
			XPSHelper.WriteItemToGrid(grid, m_DemographicGrid, rowNo, 0, 0, 2);
			WriteCheckBox(grid, "Gross Examination Only", this.m_PlacentalPathologyData.GrossExam, 0, 0);
			WriteCheckBox(grid, "Complete Examination (Includes Gross and Microscopic)", this.m_PlacentalPathologyData.CompleteExam, 0, 1);
		}
		/// <summary>method write "Cytogenetics/Other Exam" group to "Democratic History" section
		/// </summary>
		/// <param name="rowNo">base grid row number of group</param>
		private void WriteExam2Group(int rowNo)
		{
			Grid grid = XPSHelper.GetGrid(new GridLength[] { GridLength.Auto, new GridLength(1, GridUnitType.Star) }, 1);
			XPSHelper.WriteItemToGrid(grid, m_DemographicGrid, rowNo, 0, 0, 2);
			WriteCheckBox(grid, "Cytogenetics", this.m_PlacentalPathologyData.Cytogenetics, 0, 0);
			WriteCheckBoxAndTextBlock(grid, "Other", this.m_PlacentalPathologyData.OtherExam, 0, 1);
		}
		#endregion Private methods
	}
}
