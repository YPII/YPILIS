using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Shapes;

namespace YellowstonePathology.UI
{
	public class SortDirectionAdorner : Adorner
	{
		#region SortDirectionAdorner - Types
		#endregion

		#region SortDirectionAdorner - Fields
		static private readonly LinearGradientBrush ArrowBrush;
		static private readonly Geometry AscendingArrow;
		static private readonly Geometry DecendingArrow;
		private Geometry sda_render_geometry;
		#endregion

		#region SortDirectionAdorner - Delegates and Events
		#endregion

		#region SortDirectionAdorner - Properties
		#endregion

		#region SortDirectionAdorner - Constructors
		static SortDirectionAdorner()
		{
			GradientStopCollection stops = new GradientStopCollection(2);

			stops.Insert(0, new GradientStop(SystemColors.ActiveCaptionColor, 0));
			stops.Insert(1, new GradientStop(SystemColors.ControlColor, 1));

			ArrowBrush = new LinearGradientBrush(stops,
				new Point(.5, 1.0), new Point(.5, 0.0));
			DecendingArrow = Geometry.Parse("M 5,5 L 9,13 L 13,5");
			AscendingArrow = Geometry.Parse("M 5,13 L 9,5 L 13,13");
		}		/* constructor SortDirectionAdorner */

		public SortDirectionAdorner(UIElement adornedElement, ListSortDirection sortDirection)
			: base(adornedElement)
		{
			sda_render_geometry = (sortDirection == ListSortDirection.Ascending) ? AscendingArrow :
				DecendingArrow;
		}		/* constructor SortDirectionAdorner */
		#endregion

		#region SortDirectionAdorner - Methods
		protected override void OnRender(DrawingContext drawingContext)
		{
			base.OnRender(drawingContext);
			drawingContext.PushTransform(new TranslateTransform(AdornedElement.RenderSize.Width - 14, (AdornedElement.RenderSize.Height - 18) / 2.0));
			drawingContext.DrawGeometry(ArrowBrush, null, sda_render_geometry);
			drawingContext.Pop();
		}		/* method SortDirectionAdorner OnRender */
		#endregion
	}
}
