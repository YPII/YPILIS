using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.IO;
using ImageMagick;

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

		public static void ConvertToPdf(string tifFileName, string pdfFileName)
		{
			using (MagickImageCollection tiffImageCollection = new MagickImageCollection())
			{
				tiffImageCollection.AddRange(tifFileName);
				tiffImageCollection.Write(pdfFileName);
			}
		}

		/*
		public static List<Image> GetImageList(string fileName)
		{
			List<Image> result = new List<Image>();

			FileStream imageStreamSource = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
			TiffBitmapDecoder tiffDecoder = new TiffBitmapDecoder(imageStreamSource, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.Default);
			
			for (int i = 0; i < tiffDecoder.Frames.Count; i++)
			{
				BitmapSource bitMapSource = tiffDecoder.Frames[i];
				System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap()
				Image image = new Image();
				image.Source = bitMapSource;
				result.Add(image);
			}
			return result;
		}
		*/
	}
}
