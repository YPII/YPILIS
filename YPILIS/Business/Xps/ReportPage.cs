using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Documents;
using System.Windows;
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows.Markup;

namespace YellowstonePathology.Document.Xps
{
	/// <summary>report page class
	/// </summary>
    public class ReportPage
    {
		/// <summary>display resolution in pixels per inch
		/// </summary>
		private readonly static double m_DisplayResolution = 96.0;
		/// <summary>page width in inches
		/// </summary>
		private readonly static double m_PageWidth = 8.5;
		/// <summary>page height in inches
		/// </summary>
		private readonly static double m_PageHeight = 11.0;
		/// <summary>page left margin in inches
		/// </summary>
		private readonly static double m_PageLeftMargin = 0.5;
		/// <summary>page top margin in inches
		/// </summary>
		private readonly static double m_PageTopMargin = 0.5;
		/// <summary>page bottom margin in inches
		/// </summary>
		private readonly static double m_PageBottomMargin = 0.5;
		/// <summary>gap between header and body in inches
		/// </summary>
		private readonly static double m_HeaderToBodyGap = 0.7;

		/// <summary>grid with report header
		/// </summary>
		private readonly Grid m_HeaderGrid;
		/// <summary>grid with report footer
		/// </summary>
		private readonly Grid m_FooterGrid;

		/// <summary>default constructor
		/// </summary>
		/// <param name="header">page header object</param>
		/// <param name="footer">page footer object</param>
		public ReportPage(HeaderFooterBase header, HeaderFooterBase footer)
        {
            PageContent = new PageContent();
			FixedPage = new FixedPage() { Background = Brushes.White, Width = m_DisplayResolution * m_PageWidth, Height = m_DisplayResolution * m_PageHeight };
            ((IAddChild)PageContent).AddChild(FixedPage);

			m_HeaderGrid = new Grid();
			m_FooterGrid = new Grid();
			BodyGrid = XPSHelper.GetGrid(0, 1);

			header.Write(m_HeaderGrid);
			footer.Write(m_FooterGrid);

			WriteItemToPage(m_HeaderGrid, m_PageTopMargin, m_PageTopMargin);
			WriteItemToPage(BodyGrid, m_PageLeftMargin, m_HeaderGrid.ActualHeight / m_DisplayResolution + m_HeaderToBodyGap);
			WriteItemToPage(m_FooterGrid, m_PageLeftMargin, double.MinValue, double.MinValue, m_PageBottomMargin);
		}

		/// <summary>property return display resolution in pixels per inch
		/// </summary>
		public static double DisplayResolution { get { return m_DisplayResolution; } }
		/// <summary>property return page width in inches
		/// </summary>
		public static double PageWidth { get { return m_PageWidth; } }
		/// <summary>property return page height in inches
		/// </summary>
		public static double PageHeight { get { return m_PageHeight; } }
		/// <summary>property return page left margin in inches
		/// </summary>
		public static double PageLeftMargin { get { return m_PageLeftMargin; } }
		/// <summary>property return page bottom margin in inches
		/// </summary>
		public static double PageTopMargin { get { return m_PageTopMargin; } }
		/// <summary>property return page bottom margin in inches
		/// </summary>
		public static double PageBottomMargin { get { return m_PageBottomMargin; } }
		/// <summary>property return report width in inches (without left/right margins)
		/// </summary>
		public static double ReportWidth { get { return m_PageWidth - 2.0 * m_PageLeftMargin; } }
		/// <summary>property return report height in inches (without top/bottom margins)
		/// </summary>
		public static double ReportHeight { get { return m_PageHeight - (m_PageTopMargin + m_PageBottomMargin); } }
		/// <summary>property return page size in device independent pixels
		/// </summary>
		public static Size PageSize
		{
			get { return new Size(m_DisplayResolution * m_PageWidth, m_DisplayResolution * m_PageHeight); }
		}

		/// <summary>propery return page content object
		/// </summary>
		public PageContent PageContent { get; private set; }
		/// <summary>propery return page content object
		/// </summary>
		public FixedPage FixedPage { get; private set; }
		/// <summary>propery return grid with report body
		/// </summary>
		public Grid BodyGrid { get; private set; }
		/// <summary>property return max body height in device independent pixels
		/// </summary>
		public int MaxBodyHeight
		{
			get { return (int)(ReportHeight * m_DisplayResolution - m_HeaderGrid.ActualHeight - m_FooterGrid.ActualHeight - (m_HeaderToBodyGap * m_DisplayResolution)); }
		}

		/// <summary>method update page layout after item adding
		/// </summary>
		public void UpdatePageLayout()
		{
			FixedPage.Measure(PageSize);
			FixedPage.Arrange(new Rect(new Point(), PageSize));
			FixedPage.UpdateLayout();
		}
	
		/// <summary>method write control to page in absolute position
		/// </summary>
		/// <param name="item">adding control</param>
		/// <param name="left">control left displacement, default is not set</param>
		/// <param name="top">control top displacement, default is not set</param>
		/// <param name="right">control right displacement, default is not set</param>
		/// <param name="bottom">control bottom displacement, default is not set</param>
		private void WriteItemToPage(UIElement item, double left = double.MinValue, double top = double.MinValue, double right = double.MinValue, double bottom = double.MinValue)
		{
			if (left > double.MinValue) FixedPage.SetLeft(item, m_DisplayResolution * left);
			if (top > double.MinValue) FixedPage.SetTop(item, m_DisplayResolution * top);
			if (right > double.MinValue) FixedPage.SetRight(item, m_DisplayResolution * right);
			if (bottom > double.MinValue) FixedPage.SetBottom(item, m_DisplayResolution * bottom);
			FixedPage.Children.Add(item);
			UpdatePageLayout();
		}
	}
}
