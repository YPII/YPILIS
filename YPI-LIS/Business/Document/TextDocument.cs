using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.IO;

namespace YellowstonePathology.Business.Document
{
	public class TextDocument : CaseDocument
	{
		public TextDocument()
		{
		}

		public override void Close()
		{
			File.Delete(this.FullFileName);
		}

		public override void Show(System.Windows.Controls.ContentControl contentControl, object writer)
		{
			try
			{
				ScrollViewer scrollViewer = new ScrollViewer();
				TextBlock textBlock = new TextBlock();
				textBlock.TextWrapping = System.Windows.TextWrapping.Wrap;
				textBlock.FontSize = 12;
				textBlock.Text = File.ReadAllText(this.FullFileName);
				//StackPanel stackPanel = new StackPanel();
				//stackPanel.Orientation = Orientation.Vertical;
				//scrollViewer.Content = stackPanel;
				scrollViewer.Content = textBlock;
				contentControl.Content = scrollViewer;
			}
			catch
			{
			}
		}
	}
}
