using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.IO;

namespace YellowstonePathology.Business.Document
{
	public class JpgDocument : CaseDocument
	{
		public JpgDocument()
		{

		}

		public override void Show(System.Windows.Controls.ContentControl contentControl, object writer)
		{
			FileStream imageStreamSource = new FileStream(this.FullFileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
			Image image = new Image();
			JpegBitmapDecoder jpgDecoder = new JpegBitmapDecoder(imageStreamSource, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.Default);
			for (int i = 0; i < jpgDecoder.Frames.Count; i++)
			{
				BitmapSource bitMapSource = jpgDecoder.Frames[i];
				image.Source = bitMapSource;
			}
            contentControl.Content = image;			
		}
	}
}
