using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.IO;

namespace YellowstonePathology.Business.Document
{
	public class TifDocument : CaseDocument
	{
        FileStream m_ImageStreamSource;

		public TifDocument()
		{

		}

        public override void Close()
        {
            this.m_ImageStreamSource.Close();
        }

		public override void Show(System.Windows.Controls.ContentControl contentControl, object writer)
		{
            ScrollViewer scrollViewer = new ScrollViewer();            
            StackPanel stackPanel = new StackPanel();
            stackPanel.Orientation = Orientation.Vertical;
            scrollViewer.Content = stackPanel;

			this.m_ImageStreamSource = new FileStream(this.FullFileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
			TiffBitmapDecoder tiffDecoder = new TiffBitmapDecoder(this.m_ImageStreamSource, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.Default);
			try
			{
				for (int i = 0; i < tiffDecoder.Frames.Count; i++)
				{
					BitmapSource bitMapSource = tiffDecoder.Frames[i];
					Image image = new Image();
					image.Source = bitMapSource;
					stackPanel.Children.Add(image);
				}
				contentControl.Content = scrollViewer;
			}
			catch
			{

			}
		}
	}
}
