using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Data;

namespace YellowstonePathology.UI
{
	static public class ListViewSorter
	{
		#region ListViewSorter - Types
		#endregion

		#region ListViewSorter - Fields
		/// <summary>
		/// Attached Property - Indicates to automaticaly sort on column when header is set
		/// </summary>
		/// <remarks>
		/// Default is true if IsSortable is set on the associated ListView
		/// </remarks>
		static public readonly DependencyProperty AutoSortProperty = DependencyProperty.RegisterAttached("AutoSort",
																									typeof(bool), typeof(GridViewColumnHeader),
																									new PropertyMetadata(true));
		/// <summary>
		/// Attached Property - Indicates that the sort code is invoked on the ListView
		/// </summary>
		/// <remarks>
		/// Once set true at the ListView all columns will default to providing an auto sort.
		/// This acton may be overridden by AutoSort='false' on GridViewColumHeader
		/// </remarks>
		static public readonly DependencyProperty IsSortableProperty = DependencyProperty.RegisterAttached("IsSortableSorted",
																									typeof(bool), typeof(ListView),
																									new PropertyMetadata(OnIsSortableChanged));
		/// <summary>
		/// Attached Property - Specifies the property name to use for sorting the column
		/// </summary>
		/// <remarks>
		/// Defaults to null.  If null, the binding information will be used.
		/// </remarks>
		static public readonly DependencyProperty SortPropertyNameProperty = DependencyProperty.RegisterAttached("SortPropertyName",
																									typeof(string), typeof(GridViewColumnHeader),
																									new PropertyMetadata(null));

		static private readonly DependencyPropertyKey AdornerPropertyKey = DependencyProperty.RegisterAttachedReadOnly("Adorner",
																									typeof(Adorner), typeof(ListView),
																									new PropertyMetadata(null));
		static public readonly DependencyProperty AdornerProperty = AdornerPropertyKey.DependencyProperty;
		static private readonly DependencyPropertyKey LastColumnSortedPropertyKey = DependencyProperty.RegisterAttachedReadOnly("LastColumnSorted",
																									typeof(GridViewColumnHeader), typeof(ListView),
																									new PropertyMetadata(null));
		static public readonly DependencyProperty LastColumnSortedProperty = LastColumnSortedPropertyKey.DependencyProperty;
		static private readonly DependencyPropertyKey LastSortDirectionPropertyKey = DependencyProperty.RegisterAttachedReadOnly("LastSortDirection",
																									typeof(ListSortDirection), typeof(GridViewColumnHeader),
																									new PropertyMetadata(ListSortDirection.Descending));
		static public readonly DependencyProperty LastSortDirectionProperty = LastSortDirectionPropertyKey.DependencyProperty;
		#endregion

		#region ListViewSorter - Delegates and Events
		#endregion

		#region ListViewSorter - Properties
		#endregion

		#region ListViewSorter - Constructors
		static ListViewSorter()
		{
		}		/* constructor ListViewSorter */
		#endregion

		#region ListViewSorter - Methods
		/// <summary>
		/// Returns the ListView associated with the specified GridViewColumnHeader
		/// </summary>
		/// <param name="columnHeader"></param>
		/// <returns>
		/// The GridViewColumnHeader or null if the ListView cannot be found
		/// </returns>
		static private ListView GetListViewFromColumnHeader(GridViewColumnHeader columnHeader)
		{
			DependencyObject parent = columnHeader.Parent;

			while (parent != null)
			{
				if (parent is ListView)
					break;
				parent = VisualTreeHelper.GetParent(parent);
			}
			return (ListView)parent;
		}		/* method ListViewSorter GetListViewFromColumnHeader */

		/// <summary>
		/// Handles the Click event for a GridViewColumnHeader
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		static public void OnColumnHeaderClick(object sender, RoutedEventArgs e)
		{
			GridViewColumnHeader gvch = e.OriginalSource as GridViewColumnHeader;
			ListView lv;

			if (gvch != null && GetAutoSort(gvch))
			{
				/*
				 *			Make sure the ListView is still marked sortable
				 * */
				if ((lv = GetListViewFromColumnHeader(gvch)) == null)
					return;
				if (GetIsSortable(lv))
					SortByColumn(gvch, lv);
			}
		}		/* method ListViewSorter OnColumnHeaderClick */

		/// <summary>
		/// Handles changes of the ListView IsSortable attached property
		/// </summary>
		/// <param name="obj"></param>
		/// <param name="e"></param>
		/// <remarks>
		/// Sets or clears the attached ClickEvent handler
		/// </remarks>
		static private void OnIsSortableChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
		{
			ListView lv = obj as ListView;

			if (obj != null)
			{
				if (e.NewValue is bool)
					if ((bool)e.NewValue)
						lv.AddHandler(GridViewColumnHeader.ClickEvent, new RoutedEventHandler(OnColumnHeaderClick));
					else
						lv.RemoveHandler(GridViewColumnHeader.ClickEvent, new RoutedEventHandler(OnColumnHeaderClick));
			}
		}		/* method ListViewSorter OnIsSortableChanged */

		/// <summary>
		/// Sorts the ListView on the given column
		/// </summary>
		/// <param name="columnHeader"></param>
		/// <param name="listView"></param>
		/// <remarks>
		/// The data will be sorted by suppling a SortDescription.  The property name for the
		/// sort will come from one of three places in order
		/// 1) - Explicitly specfied name (SortPropertyName attached property)
		/// 2) - A XPath specification in the column binding
		/// 3) - The Path property from the binding Path
		/// </remarks>
		static public void SortByColumn(GridViewColumnHeader columnHeader, ListView listView)
		{
			GridViewColumnHeader last_sorted_header;
			ListSortDirection sort_direction;
			SortDirectionAdorner adorner;
			string sort_property_name;
			Binding binding;

			if (columnHeader.Column != null)					// user clicked a valid column header
			{
				/*
				 *			Determine the sort property name
				 *			If we cannot determine it, do nothing
				 * */
				if ((sort_property_name = GetSortPropertyName(columnHeader)) == null)
				{
					/*
					 *			Binding is not present if a CellTemplate is used
					 * */
					if ((binding = ((Binding)columnHeader.Column.DisplayMemberBinding)) == null)
						goto function_exit;
					sort_property_name = binding.XPath;
					if (string.IsNullOrEmpty(sort_property_name))
						sort_property_name = ((Binding)columnHeader.Column.DisplayMemberBinding).Path.Path;
				}
				last_sorted_header = GetLastColumnSorted(listView);
				if (last_sorted_header != null)
					AdornerLayer.GetAdornerLayer(last_sorted_header).Remove(GetAdorner(last_sorted_header));

				sort_direction = ((GetLastSortDirection(columnHeader) == ListSortDirection.Ascending) ?
					ListSortDirection.Descending : ListSortDirection.Ascending);
				SetLastSortDirection(columnHeader, sort_direction);
				SetLastColumnSorted(listView, columnHeader);
				listView.Items.SortDescriptions.Clear();
				listView.Items.SortDescriptions.Add(new SortDescription(sort_property_name, sort_direction));

				adorner = new SortDirectionAdorner(columnHeader, sort_direction);
				SetAdorner(columnHeader, adorner);
				AdornerLayer.GetAdornerLayer(columnHeader).Add(adorner);
			}
		function_exit:
			;
		}		/* method ListViewSorter SortByColumn */

		/// <summary>
		/// Sorts a ListView on a given column
		/// </summary>
		/// <param name="columnHeader"></param>
		static public void SortByColumn(GridViewColumnHeader columnHeader)
		{
			ListView lv;

			if ((lv = GetListViewFromColumnHeader(columnHeader)) == null)
				return;
			SortByColumn(columnHeader, lv);
		}		/* method ListViewSorter SortByColumn */
		#endregion

		#region ListViewSorter - Methods - Dependency Property Accessors
		static public Adorner GetAdorner(DependencyObject obj)
		{
			return (Adorner)obj.GetValue(AdornerProperty);
		}		/* method ListViewSorter GetAdorner */

		static private void SetAdorner(DependencyObject obj, Adorner value)
		{
			obj.SetValue(AdornerPropertyKey, value);
		}		/* method ListViewSorter SetAdorner */

		static public bool GetAutoSort(DependencyObject obj)
		{
			return (bool)obj.GetValue(AutoSortProperty);
		}		/* method ListViewSorter GetAutoSort */

		static public void SetAutoSort(DependencyObject obj, bool value)
		{
			obj.SetValue(AutoSortProperty, value);
		}		/* method ListViewSorter SetAutoSort */

		static public bool GetIsSortable(DependencyObject obj)
		{
			return (bool)obj.GetValue(IsSortableProperty);
		}		/* method ListViewSorter GetIsSortable */

		static public void SetIsSortable(DependencyObject obj, bool value)
		{
			obj.SetValue(IsSortableProperty, value);
		}		/* method ListViewSorter SetIsSortable */

		static public GridViewColumnHeader GetLastColumnSorted(DependencyObject obj)
		{
			return (GridViewColumnHeader)obj.GetValue(LastColumnSortedProperty);
		}		/* method ListViewSorter GetLastColumnSorted */

		static private void SetLastColumnSorted(DependencyObject obj, GridViewColumnHeader value)
		{
			obj.SetValue(LastColumnSortedPropertyKey, value);
		}		/* method ListViewSorter SetLastColumnSorted */

		static public ListSortDirection GetLastSortDirection(DependencyObject obj)
		{
			return (ListSortDirection)obj.GetValue(LastSortDirectionProperty);
		}		/* method ListViewSorter GetLastSortDirection */

		static private void SetLastSortDirection(DependencyObject obj, ListSortDirection value)
		{
			obj.SetValue(LastSortDirectionPropertyKey, value);
		}		/* method ListViewSorter SetLastSortDirection */

		static public string GetSortPropertyName(DependencyObject obj)
		{
			return (string)obj.GetValue(SortPropertyNameProperty);
		}		/* method ListViewSorter GetSortPropertyName */

		static public void SetSortPropertyName(DependencyObject obj, string value)
		{
			obj.SetValue(SortPropertyNameProperty, value);
		}		/* method ListViewSorter SetSortPropertyName */
		#endregion
	}
}
