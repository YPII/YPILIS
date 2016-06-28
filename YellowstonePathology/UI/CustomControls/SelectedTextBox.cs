using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace YellowstonePathology.UI.CustomControls
{
	class SelectedTextBox : TextBox
	{
		private bool _allSelectedOccurred;

		protected override void OnLostFocus(System.Windows.RoutedEventArgs e)
		{
			_allSelectedOccurred = false;
			base.OnLostFocus(e);
		}
		protected override void OnLostKeyboardFocus(System.Windows.Input.KeyboardFocusChangedEventArgs e)
		{
			_allSelectedOccurred = false;
			base.OnLostKeyboardFocus(e);
		}

		protected override void OnMouseUp(System.Windows.Input.MouseButtonEventArgs e)
		{
			base.OnMouseUp(e);
			if (!this._allSelectedOccurred)
			{
				this.SelectAll();
				this._allSelectedOccurred = true;
			}
		}
	}
}
