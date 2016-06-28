using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Documents;
using System.Windows;
using System.Windows.Controls;

namespace YellowstonePathology.Document.Xps
{
	/// <summary> report document class
	/// </summary>
    public class ReportDocument
    {
		/// <summary>fixed document object
		/// </summary>
		private readonly FixedDocument m_FixedDocument;
		/// <summary>page header object for first page 
		/// </summary>
		private readonly HeaderFooterBase m_ReportFirstPageHeader;
		/// <summary>page header object for second and next pages
		/// </summary>
		private readonly HeaderFooterBase m_ReportNextPageHeader;
		/// <summary>report footer object
		/// </summary>
		private readonly HeaderFooterBase m_ReportFooter;

		/// <summary>page object for report page is populated currently
		/// </summary>
		private ReportPage m_CurrentReportPage;

		/// <summary>constructor
		/// </summary>
		/// <param name="reportFirstPageHeader"></param>
		/// <param name="reportFooter">page header for first page</param>
		/// <param name="reportNextPageHeader">page header for second and next pages, if null then page header for first page is used</param>
		public ReportDocument(HeaderFooterBase reportFirstPageHeader, HeaderFooterBase reportFooter, HeaderFooterBase reportNextPageHeader = null)
		{
			m_ReportFirstPageHeader = reportFirstPageHeader;
			m_ReportNextPageHeader = (reportNextPageHeader ?? m_ReportFirstPageHeader);
			m_ReportFooter = reportFooter;
			m_FixedDocument = new FixedDocument();

			m_FixedDocument.DocumentPaginator.PageSize = new Size(ReportPage.DisplayResolution * ReportPage.PageWidth, ReportPage.DisplayResolution * ReportPage.PageHeight);
			m_CurrentReportPage = new ReportPage(m_ReportFirstPageHeader, m_ReportFooter);
			m_FixedDocument.Pages.Add(m_CurrentReportPage.PageContent);

			m_CurrentReportPage.UpdatePageLayout();
		}

		/// <summary>property return fixed document object
		/// </summary>
		public FixedDocument FixedDocument
		{
			get { return m_FixedDocument; }
		}

		/// <summary>method write control to new row that will add to report body grid
		/// </summary>
		/// <param name="element">writing control</param>
		/// <returns>index of new row</returns>
		public int WriteRowContent(UIElement element)
		{
			int rowIndex = WriteElement(element);
			if (m_CurrentReportPage.BodyGrid.ActualHeight >= m_CurrentReportPage.MaxBodyHeight)
			{
				RemoveElement(element);
				AddNewPage();
				rowIndex = WriteElement(element);
			}
			return rowIndex;
		}

		/// <summary>method add border to report body grid's first cell
		/// </summary>
		/// <param name="border">adding border</param>
		public void WriteBorder(Border border)
		{
			m_CurrentReportPage.BodyGrid.Children.Add(border);
			m_CurrentReportPage.UpdatePageLayout();
		}

		/// <summary>method write control to new row that will add to report body grid
		/// </summary>
		/// <param name="element">writing control</param>
		/// <returns>index of new row</returns>
		private int WriteElement(UIElement element)
		{
			m_CurrentReportPage.BodyGrid.RowDefinitions.Add(new RowDefinition());
			int rowIndex = m_CurrentReportPage.BodyGrid.RowDefinitions.Count - 1;
			XPSHelper.WriteItemToGrid(element, m_CurrentReportPage.BodyGrid, rowIndex, 0);
			m_CurrentReportPage.UpdatePageLayout();
			return rowIndex;
		}

		private void RemoveElement(UIElement element)
		{
			m_CurrentReportPage.BodyGrid.Children.Remove(element);
			m_CurrentReportPage.UpdatePageLayout();
		}

		private void AddNewPage()
		{
			m_CurrentReportPage = new ReportPage(m_ReportNextPageHeader, m_ReportFooter);
			m_FixedDocument.Pages.Add(m_CurrentReportPage.PageContent);
		}
	}
}
