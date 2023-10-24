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
    public class MultiPageDocument
    {		
		private readonly FixedDocument m_FixedDocument;	
		private readonly HeaderFooterBase m_ReportFirstPageHeader;		
		private readonly HeaderFooterBase m_ReportNextPageHeader;						
		private ReportPage m_CurrentReportPage;
		
        public MultiPageDocument(HeaderFooterBase reportFirstPageHeader, HeaderFooterBase reportNextPageHeader = null)
		{
			this.m_ReportFirstPageHeader = reportFirstPageHeader;
            this.m_ReportNextPageHeader = (reportNextPageHeader ?? m_ReportFirstPageHeader);
            this.m_FixedDocument = new FixedDocument();

            this.m_FixedDocument.DocumentPaginator.PageSize = new Size(ReportPage.DisplayResolution * ReportPage.PageWidth, ReportPage.DisplayResolution * ReportPage.PageHeight);
            PageNumberFooter pageNumberFooter = new PageNumberFooter(1);
            this.m_CurrentReportPage = new ReportPage(m_ReportFirstPageHeader, pageNumberFooter);
            this.m_FixedDocument.Pages.Add(m_CurrentReportPage.PageContent);

            this.m_CurrentReportPage.UpdatePageLayout();
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
                int currentPageCount = m_FixedDocument.Pages.Count + 1;
				RemoveElement(element);
				AddNewPage(currentPageCount);
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

		private void AddNewPage(int pageNumber)
		{
            PageNumberFooter pageNumberFooter = new PageNumberFooter(pageNumber);
			m_CurrentReportPage = new ReportPage(m_ReportNextPageHeader, pageNumberFooter);
			m_FixedDocument.Pages.Add(m_CurrentReportPage.PageContent);
		}
	}
}
