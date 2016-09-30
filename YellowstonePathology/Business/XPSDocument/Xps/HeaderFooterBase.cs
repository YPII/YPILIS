using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media;

namespace YellowstonePathology.Document.Xps
{
	/// <summary>base class for report header/footer
	/// </summary>
	public class HeaderFooterBase
	{
		/// <summary>report standard brushes
		/// </summary>
		protected readonly SolidColorBrush m_RedishBrush;
		protected readonly SolidColorBrush m_GreenishBrush;

		/// <summary>horizontal (left/right) margin 
		/// </summary>
		protected readonly double m_HorMargin;
		/// <summary>base font size
		/// </summary>
		protected readonly double m_FontSize;

		/// <summary>default constructor
		/// </summary>
		public HeaderFooterBase()
		{
			m_RedishBrush = new SolidColorBrush(Color.FromRgb(0xBC, 0x6D, 0x50));
			m_GreenishBrush = new SolidColorBrush(Color.FromRgb(0x98, 0x94, 0x82));
			m_HorMargin = 7;
			m_FontSize = 10.0;
		}
		/// <summary>overridable method for report header/footer content writing
		/// </summary>
		/// <param name="grid">root grid of report document</param>
		public virtual void Write(Grid grid)
		{

		}


	}
}
