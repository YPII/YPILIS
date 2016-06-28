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
	class ValidTextBox : TextBox
	{
		public static readonly DependencyProperty HasErrorProperty =
		  DependencyProperty.Register("HasError", typeof(Nullable<bool>), typeof(ValidTextBox));

		public ValidTextBox()
		{
			this.PreviewLostKeyboardFocus += new KeyboardFocusChangedEventHandler(ValidTextBox_PreviewLostKeyboardFocus);
		}

		public bool HasError
		{
			get
			{
				Nullable<bool> ok = base.GetValue(HasErrorProperty) as Nullable<bool>;
				if (!ok.HasValue)
				{
					return false;
				}
				return ok.Value;
			}
			set { base.SetValue(HasErrorProperty, value); }
		}

		protected override void OnPreviewLostKeyboardFocus(System.Windows.Input.KeyboardFocusChangedEventArgs e)
		{
			BindingExpression be = GetBindingExpression(TextBox.TextProperty);
			be.UpdateSource();
			base.OnPreviewLostKeyboardFocus(e);
		}

		private void ValidTextBox_PreviewLostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
		{
			if (HasError)
			{
				e.Handled = true;
			}
		}
	}
}
