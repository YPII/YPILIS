﻿using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Markup;

namespace YellowstonePathology.UI
{
    /// <summary>
	/// Interaction logic for TifDocumentViewer.xaml
    /// </summary>
    public partial class TifDocumentViewerPage : UserControl
    {
        List<BitmapSource> m_BitmapSourceList;

        public TifDocumentViewerPage()
        {
            this.m_BitmapSourceList = new List<BitmapSource>();
            InitializeComponent();         
        }

        public void Load(string fileName)
        {
            System.IO.MemoryStream memoryStream = new System.IO.MemoryStream();
            System.IO.FileStream fileStream = System.IO.File.OpenRead(fileName);
            CopyStream(fileStream, memoryStream);
            fileStream.Close();

            TiffBitmapDecoder decoder = new TiffBitmapDecoder(memoryStream, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.Default);
            foreach (BitmapFrame bitmapFrame in decoder.Frames)
            {
                BitmapSource bitmapSource = bitmapFrame;
                this.m_BitmapSourceList.Add(bitmapSource);

                Image image = new Image();
                image.Source = bitmapSource;
                image.Margin = new Thickness(2);
                this.StackPanelImages.Children.Add(image);
            }            
        }                      

        public static void CopyStream(System.IO.Stream fromStream, System.IO.Stream toStream)
        {
            int Length = 65536;
            Byte[] buffer = new Byte[Length];
            int bytesRead = fromStream.Read(buffer, 0, Length);
            while (bytesRead > 0)
            {
                toStream.Write(buffer, 0, bytesRead);
                bytesRead = fromStream.Read(buffer, 0, Length);
            }
        }        
    }
}
