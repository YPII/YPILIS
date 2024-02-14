using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Drawing;
using System.Drawing.Imaging;

namespace YellowstonePathology.UI.Login
{
    public class BarcodeLabelPanel : StackPanel
    {
		public BarcodeLabelPanel(YellowstonePathology.Business.BarcodeScanning.BarcodePrefixEnum barcodePrefixEnum)
        {
            /*
			this.Margin = new Thickness(15, 10, 31, 0);

			YellowstonePathology.Business.BarcodeScanning.IScanable scanable = null;
			switch(barcodePrefixEnum)
			{
				case Business.BarcodeScanning.BarcodePrefixEnum.CTNR:
					scanable = new YellowstonePathology.Business.BarcodeScanning.Container();
					((YellowstonePathology.Business.BarcodeScanning.Container)scanable).FromGuid();
					break;
				case Business.BarcodeScanning.BarcodePrefixEnum.DCMT:
					scanable = new YellowstonePathology.Business.BarcodeScanning.Document();
					((YellowstonePathology.Business.BarcodeScanning.Document)scanable).FromGuid();
					break;
			}
            
            TextBlock textBlockCompany = new TextBlock();
            textBlockCompany.Padding = new Thickness(2,2,2,2);
            textBlockCompany.Text = "YPI";
            textBlockCompany.FontSize = 16;
            textBlockCompany.FontWeight = System.Windows.FontWeights.Bold;            
            textBlockCompany.TextAlignment = TextAlignment.Center;
            this.Children.Add(textBlockCompany);

            DmtxImageEncoder encoder = new DmtxImageEncoder();
            DmtxImageEncoderOptions options = new DmtxImageEncoderOptions();
            options.ModuleSize = 1;
            options.MarginSize = 2;
            options.BackColor = System.Drawing.Color.White;
            options.ForeColor = System.Drawing.Color.Black;

			Bitmap bitmap = encoder.EncodeImage(scanable.Barcode.ToString(), options);

            System.Windows.Media.Imaging.BitmapSource bitmapSource = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
                    bitmap.GetHbitmap(),
                    IntPtr.Zero,
                    Int32Rect.Empty,
                    System.Windows.Media.Imaging.BitmapSizeOptions.FromEmptyOptions());

            System.Windows.Controls.Image image = new System.Windows.Controls.Image();
            image.Source = bitmapSource;                        
            this.Children.Add(image);            

            TextBlock textBlockCodeFirstLine = new TextBlock();
			textBlockCodeFirstLine.Text = scanable.Barcode.GetFirstLine();
            textBlockCodeFirstLine.FontSize = 6;
            textBlockCodeFirstLine.TextAlignment = TextAlignment.Center;      
            this.Children.Add(textBlockCodeFirstLine);

            TextBlock textBlockCodeSecondLine = new TextBlock();
			textBlockCodeSecondLine.Text = scanable.Barcode.GetSecondLine();
            textBlockCodeSecondLine.FontSize = 6;
            textBlockCodeSecondLine.TextAlignment = TextAlignment.Center;
            this.Children.Add(textBlockCodeSecondLine);

            TextBlock textBlockCodeThirdLine = new TextBlock();
			textBlockCodeThirdLine.Text = scanable.Barcode.GetThirdLine();
            textBlockCodeThirdLine.FontSize = 8;
            textBlockCodeThirdLine.TextAlignment = TextAlignment.Center;
            this.Children.Add(textBlockCodeThirdLine);   
            */
        }
    }
}
