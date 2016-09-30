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

namespace YellowstonePathology.UI
{
	/// <summary>
	/// Interaction logic for TypingTemplageDialog.xaml
	/// </summary>
	public partial class TypingTemplateDialog : Window
	{
		private YellowstonePathology.Business.Typing.ParagraphTemplateCollection m_ParagraphTemplateCollection;

        public TypingTemplateDialog()
		{
			this.m_ParagraphTemplateCollection = new YellowstonePathology.Business.Typing.ParagraphTemplateCollection();

			InitializeComponent();
			DataContext = this;
		}

		public YellowstonePathology.Business.Typing.ParagraphTemplateCollection ParagraphTemplateCollection
		{
			get { return this.m_ParagraphTemplateCollection; }
			set { this.m_ParagraphTemplateCollection = value; }
		}

		private void ButtonGenerateText_Click(object sender, RoutedEventArgs e)
		{
			if (this.ListBoxParagraphTemplates.SelectedItems.Count != 0)
			{
				YellowstonePathology.Business.Typing.ParagraphTemplate paragraphTemplate = (YellowstonePathology.Business.Typing.ParagraphTemplate)this.ListBoxParagraphTemplates.SelectedItem;
				this.TextBoxTemplateParagraphText.Text = paragraphTemplate.GenerateParagraph(this.TextBoxTemplateTypedWords.Text);
			}
		}

		private void ListBoxParagraphTemplates_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (this.ListBoxParagraphTemplates.SelectedItems.Count != 0)
			{
				YellowstonePathology.Business.Typing.ParagraphTemplate paragraphTemplate = (YellowstonePathology.Business.Typing.ParagraphTemplate)this.ListBoxParagraphTemplates.SelectedItem;
				//this.TextBoxTemplateParagraphText.Text = paragraphTemplate.Text;
                this.TextBoxTemplateTypedWords.Focus();
			}
		}

		private void TextBoxTemplateParagraphText_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Tab)
			{
				this.TextBoxTemplateParagraphText.Select(5, 10);
			}
		}        

		private void ButtonOk_Click(object sender, RoutedEventArgs e)
		{
			this.DialogResult = true;
			Close();
		}

		private void ButtonClose_Click(object sender, RoutedEventArgs e)
		{
			Close();
		}

        private void ButtonAddText_Click(object sender, RoutedEventArgs e)
        {
            if (this.ListBoxParagraphTemplates.SelectedItems.Count != 0)
            {
                YellowstonePathology.Business.Typing.ParagraphTemplate paragraphTemplate = (YellowstonePathology.Business.Typing.ParagraphTemplate)this.ListBoxParagraphTemplates.SelectedItem;
                this.TextBoxTemplateParagraphText.Text += paragraphTemplate.GenerateParagraph(this.TextBoxTemplateTypedWords.Text);
                this.TextBoxTemplateTypedWords.Text = string.Empty;
                this.TextBoxTemplateTypedWords.Focus();
            }
        }        
	}
}
