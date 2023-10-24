using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace YellowstonePathology.UI.Cytology
{
	/// <summary>
	/// Interaction logic for DotReviewReasonDialog.xaml
	/// </summary>
	public partial class DotReviewCommentDialog : Window
	{
		private string m_Comment;

		public DotReviewCommentDialog()
		{
			this.m_Comment = string.Empty;
			InitializeComponent();
		}

		public string Comment
		{
			get { return this.m_Comment; }
		}

		private void ButtonOK_Click(object sender, RoutedEventArgs e)
		{
			if (this.TextBoxComment.Text.Length == 0)
			{
				MessageBox.Show("A comment is needed for a Dot review.", "Comment Required", MessageBoxButton.OK, MessageBoxImage.Exclamation);
				return;
			}
			this.m_Comment = this.TextBoxComment.Text;
			this.DialogResult = true;
			Close();
		}

		private void ButtonCancel_Click(object sender, RoutedEventArgs e)
		{
			Close();
		}
	}
}
