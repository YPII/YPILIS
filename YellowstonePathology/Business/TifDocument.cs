using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.IO;
using System.Windows.Media.Imaging;
using System.Windows.Controls;

namespace YellowstonePathology.Business
{
	public class TifDocument : INotifyPropertyChanged
	{
        public delegate void PropertyChangedNotificationHandler(String info);
        public event PropertyChangedEventHandler PropertyChanged;

        protected string m_FileName;
        protected bool m_FileExists;
        protected List<TifDocumentPage> m_Pages;
        protected ObservableCollection<System.Windows.Controls.Image> m_Thumbnails;        

        public TifDocument()
        {
            this.m_Pages = new List<TifDocumentPage>();
            this.m_Thumbnails = new ObservableCollection<System.Windows.Controls.Image>();
        }

        public string FileName
        {
            get { return this.m_FileName; }
            set { this.m_FileName = value; }
        }

        public void SetupDocument(string fileName)
        {
            this.m_FileName = fileName;
            this.m_FileExists = System.IO.File.Exists(fileName);

            if (this.m_FileExists == true)
            {
                this.SetPages();
                this.SetThumbnails();
            }
        }

        public void Print(System.Windows.Controls.Image image)
        {
            System.Printing.LocalPrintServer localPrintServer = new System.Printing.LocalPrintServer();            
            PrintDialog printDialog = new PrintDialog();

            System.Printing.PrintCapabilities capabilities = printDialog.PrintQueue.GetPrintCapabilities(printDialog.PrintTicket);
            double scale = Math.Min(capabilities.PageImageableArea.ExtentWidth / image.ActualWidth, capabilities.PageImageableArea.ExtentHeight/image.ActualHeight);
            image.LayoutTransform = new System.Windows.Media.ScaleTransform(scale, scale);
            System.Windows.Size sz = new System.Windows.Size(capabilities.PageImageableArea.ExtentWidth, capabilities.PageImageableArea.ExtentHeight);
            image.Measure(sz);
            image.Arrange(new System.Windows.Rect(new System.Windows.Point(capabilities.PageImageableArea.OriginWidth, capabilities.PageImageableArea.OriginHeight), sz));

            printDialog.PrintVisual(image, "Print Image");            
        }

        private void SetPages()
        {
            System.IO.FileStream fileStream = new FileStream(this.m_FileName, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
            TiffBitmapDecoder tiffBitmapDecoder = new TiffBitmapDecoder(fileStream, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.Default);
            for (int i = 0; i < tiffBitmapDecoder.Frames.Count; i++)
            {
                BitmapSource bitmapSource = tiffBitmapDecoder.Frames[i];
                YellowstonePathology.Business.TifDocumentPage tifDocumentPage = new TifDocumentPage();
                tifDocumentPage.BitmapImage = this.BitmapImageFromBitmapSource(bitmapSource);
                this.Pages.Add(tifDocumentPage);
            }
            fileStream.Close();
        }

        private void SetThumbnails()
        {            
            this.m_Thumbnails.Clear();
            foreach (TifDocumentPage tifDocumentPage in this.m_Pages)
            {
                System.Windows.Controls.Image thumbNail = new System.Windows.Controls.Image();
                thumbNail.Source = tifDocumentPage.BitmapImage;
                thumbNail.Width = 100;
                thumbNail.Height = 100;
                thumbNail.Tag = tifDocumentPage.PageId;
                this.m_Thumbnails.Add(thumbNail);
            }
            this.NotifyPropertyChanged("Thumbnails");
        }        

        public bool FileExists
        {
            get { return this.m_FileExists; }
        }

        public List<TifDocumentPage> Pages
        {
            get { return this.m_Pages; }
        }

        public ObservableCollection<System.Windows.Controls.Image> Thumbnails
        {
            get { return this.m_Thumbnails; }
        }

        public void Save(bool releaseLock)
        {
            if (this.m_Pages.Count > 0)
            {
                List<Bitmap> bitmaps = new List<Bitmap>();
                foreach (TifDocumentPage tifDocumentPage in this.m_Pages)
                {
                    bitmaps.Add(this.BitmapFromBitmapSource(tifDocumentPage.BitmapImage));
                }
                this.Create(bitmaps);
            }
            else
            {
                this.DeleteDocument();
            }
        }

        public void DeleteDocument()
        {
            if (System.IO.File.Exists(this.m_FileName) == true)
            {
                System.IO.File.Delete(this.m_FileName);
            }
        }        

        public void AddScannedPages(List<Bitmap> bitmaps)
        {
            foreach (Bitmap bitmap in bitmaps)
            {                
                BitmapImage page = BitmapImageFromBitmap(bitmap);
                YellowstonePathology.Business.TifDocumentPage tifDocumentPage = new TifDocumentPage();
                tifDocumentPage.BitmapImage = page;
                this.m_Pages.Add(tifDocumentPage);
            }            
        }

        public void DeletePage(string pageId)
        {
            foreach (TifDocumentPage tifDocumentPage in this.m_Pages)
            {
                if (tifDocumentPage.PageId.ToString() == pageId)
                {
                    this.m_Pages.Remove(tifDocumentPage);
                    break;
                }
            }            
        }

        private void SaveSinglePage(Bitmap bitMap)
        {
            ImageCodecInfo codecInfo = getCodecForstring("TIFF");
            System.Drawing.Imaging.EncoderParameters iparams = new System.Drawing.Imaging.EncoderParameters(1);
            System.Drawing.Imaging.Encoder iparam = System.Drawing.Imaging.Encoder.Compression;
            System.Drawing.Imaging.EncoderParameter iparamPara = new System.Drawing.Imaging.EncoderParameter(iparam, (long)(System.Drawing.Imaging.EncoderValue.CompressionCCITT4));
            iparams.Param[0] = iparamPara;
            bitMap.Save(this.m_FileName, codecInfo, iparams);
        }

        private BitmapImage BitmapImageFromBitmap(Bitmap bitmap)
        {
            MemoryStream memoryStream = new MemoryStream();
            bitmap.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Bmp);
            BitmapImage bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.StreamSource = memoryStream;
            bitmapImage.EndInit();
            return bitmapImage;
        }

        private BitmapImage BitmapImageFromBitmapSource(BitmapSource bitmapSource)
        {
            BitmapImage bitmapImage = new BitmapImage();
            MemoryStream memoryStream = new MemoryStream();
            BitmapEncoder bitmapEncoder = new BmpBitmapEncoder();
            bitmapEncoder.Frames.Add(BitmapFrame.Create(bitmapSource));
            bitmapEncoder.Save(memoryStream);
            memoryStream.Seek(0, SeekOrigin.Begin);
            bitmapImage.BeginInit();
            bitmapImage.StreamSource = memoryStream;
            bitmapImage.EndInit();
            return bitmapImage;
        }

        private Bitmap BitmapFromBitmapSource(BitmapSource bitmapSource)
        {
            MemoryStream memoryStream = new MemoryStream();
            BitmapEncoder enc = new BmpBitmapEncoder();
            enc.Frames.Add(BitmapFrame.Create(bitmapSource));
            enc.Save(memoryStream);
            System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(memoryStream);
            return bitmap;
        }

        private Bitmap BitmapFromBitmapImage(BitmapImage bitmapImage)
        {
            MemoryStream memoryStream = new MemoryStream();
            BitmapEncoder enc = new BmpBitmapEncoder();
            enc.Frames.Add(BitmapFrame.Create(bitmapImage));
            enc.Save(memoryStream);
            System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(memoryStream);
            return bitmap;
        }

        public List<Bitmap> ConvertPagesToBitMaps()
        {
            List<Bitmap> bitmapPages = new List<Bitmap>();
            foreach (TifDocumentPage tifDocumentPage in this.m_Pages)
            {
                bitmapPages.Add(this.BitmapFromBitmapImage(tifDocumentPage.BitmapImage));
            }
            return bitmapPages;
        }

        public void ConvertBitMapsToPages(List<Bitmap> bitmaps)
        {
            this.m_Pages.Clear();
            foreach (Bitmap bitmap in bitmaps)
            {
                BitmapImage bitmapImage = this.BitmapImageFromBitmap(bitmap);
                TifDocumentPage tifDocumentPage = new TifDocumentPage();
                tifDocumentPage.BitmapImage = bitmapImage;
                this.m_Pages.Add(tifDocumentPage);
            }         
        }

        public void Create(List<Bitmap> bitmaps)
        {            
            if (bitmaps.Count == 1)
            {
                Bitmap newBitmap = this.ConvertToBitonal(bitmaps[0]);
                this.SaveSinglePage(newBitmap);
            }
            else
            {
                this.ConvertToBitonal(bitmaps);
                this.SaveMultiPage(bitmaps);
            }
        }

        public void Rotate(int pageIndex)
        {            
            BitmapImage bitmapImage = this.m_Pages[pageIndex].BitmapImage;
            System.Drawing.Bitmap bitmap = this.BitmapFromSource(bitmapImage);
            bitmap.RotateFlip(System.Drawing.RotateFlipType.Rotate180FlipNone);
            System.Windows.Media.Imaging.BitmapImage flippedBitmapImage = this.BitmapImageFromBitmap(bitmap);
            this.m_Pages[pageIndex].BitmapImage = flippedBitmapImage;            
        }

        private void SaveMultiPage(List<Bitmap> bitMaps)
        {
            ImageCodecInfo codecInfo = getCodecForstring("TIFF");
            this.ConvertToBitonal(bitMaps);           

            System.Drawing.Imaging.Encoder saveEncoder;
            System.Drawing.Imaging.Encoder compressionEncoder;
            System.Drawing.Imaging.EncoderParameter SaveEncodeParam;
            System.Drawing.Imaging.EncoderParameter CompressionEncodeParam;
            System.Drawing.Imaging.EncoderParameters EncoderParams = new System.Drawing.Imaging.EncoderParameters(2);

            saveEncoder = System.Drawing.Imaging.Encoder.SaveFlag;
            compressionEncoder = System.Drawing.Imaging.Encoder.Compression;
            
            SaveEncodeParam = new System.Drawing.Imaging.EncoderParameter(saveEncoder, (long)System.Drawing.Imaging.EncoderValue.MultiFrame);
            CompressionEncodeParam = new System.Drawing.Imaging.EncoderParameter(compressionEncoder, (long)System.Drawing.Imaging.EncoderValue.CompressionCCITT4);
            EncoderParams.Param[0] = CompressionEncodeParam;
            EncoderParams.Param[1] = SaveEncodeParam;
            
            bitMaps[0].Save(this.m_FileName, codecInfo, EncoderParams);

            for (int i = 1; i < bitMaps.Count; i++)
            {
                if (bitMaps[i] == null) break;

                SaveEncodeParam = new EncoderParameter(saveEncoder, (long)EncoderValue.FrameDimensionPage);
                CompressionEncodeParam = new EncoderParameter(compressionEncoder, (long)EncoderValue.CompressionCCITT4);
                EncoderParams.Param[0] = CompressionEncodeParam;
                EncoderParams.Param[1] = SaveEncodeParam;
                bitMaps[0].SaveAdd(bitMaps[i], EncoderParams);
            }

            SaveEncodeParam = new EncoderParameter(saveEncoder, (long)EncoderValue.Flush);
            EncoderParams.Param[0] = SaveEncodeParam;
            bitMaps[0].SaveAdd(EncoderParams);                         
        }

        private ImageCodecInfo getCodecForstring(string type)
        {
            ImageCodecInfo[] info = ImageCodecInfo.GetImageEncoders();
            for (int i = 0; i < info.Length; i++)
            {
                string EnumName = type.ToString();
                if (info[i].FormatDescription.Equals(EnumName))
                {
                    return info[i];
                }
            }
            return null;
        }

        protected void ConvertToBitonal(List<Bitmap> bitmaps)
        {
            for (int i = 0; i < bitmaps.Count; i++)
            {
                if (bitmaps[i] == null)
                {
                    break;
                }
                bitmaps[i] = (Bitmap)ConvertToBitonal((Bitmap)bitmaps[i]);
            }
        }

        private System.Drawing.Bitmap BitmapFromSource(BitmapSource bitmapsource)
        {
            System.Drawing.Bitmap bitmap;
            using (MemoryStream outStream = new MemoryStream())
            {
                BitmapEncoder enc = new BmpBitmapEncoder();
                enc.Frames.Add(BitmapFrame.Create(bitmapsource));
                enc.Save(outStream);
                bitmap = new System.Drawing.Bitmap(outStream);
            }
            return bitmap;
        }

        private Bitmap ConvertToBitonal(Bitmap original)
        {
            Bitmap source = null;            
            if (original.PixelFormat != PixelFormat.Format32bppArgb)
            {
                source = new Bitmap(original.Width, original.Height, PixelFormat.Format32bppArgb);
                source.SetResolution(original.HorizontalResolution, original.VerticalResolution);
                using (Graphics g = Graphics.FromImage(source))
                {
                    g.DrawImageUnscaled(original, 0, 0);
                }
            }
            else
            {
                source = original;
            }

            BitmapData sourceData = source.LockBits(new Rectangle(0, 0, source.Width, source.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            int imageSize = sourceData.Stride * sourceData.Height;
            byte[] sourceBuffer = new byte[imageSize];
            Marshal.Copy(sourceData.Scan0, sourceBuffer, 0, imageSize);
            
            source.UnlockBits(sourceData);
            
            Bitmap destination = new Bitmap(source.Width, source.Height, PixelFormat.Format1bppIndexed);
            BitmapData destinationData = destination.LockBits(new Rectangle(0, 0, destination.Width, destination.Height), ImageLockMode.WriteOnly, PixelFormat.Format1bppIndexed);

            imageSize = destinationData.Stride * destinationData.Height;
            byte[] destinationBuffer = new byte[imageSize];

            int sourceIndex = 0;
            int destinationIndex = 0;
            int pixelTotal = 0;
            byte destinationValue = 0;
            int pixelValue = 128;
            int height = source.Height;
            int width = source.Width;
            int threshold = 500;
            
            for (int y = 0; y < height; y++)
            {
                sourceIndex = y * sourceData.Stride;
                destinationIndex = y * destinationData.Stride;
                destinationValue = 0;
                pixelValue = 128;
                
                for (int x = 0; x < width; x++)
                {
                    pixelTotal = sourceBuffer[sourceIndex + 1] + sourceBuffer[sourceIndex + 2] + sourceBuffer[sourceIndex + 3];
                    if (pixelTotal > threshold)
                    {
                        destinationValue += (byte)pixelValue;
                    }
                    if (pixelValue == 1)
                    {
                        destinationBuffer[destinationIndex] = destinationValue;
                        destinationIndex++;
                        destinationValue = 0;
                        pixelValue = 128;
                    }
                    else
                    {
                        pixelValue >>= 1;
                    }
                    sourceIndex += 4;
                }
                if (pixelValue != 128)
                {
                    destinationBuffer[destinationIndex] = destinationValue;
                }
            }
            
            Marshal.Copy(destinationBuffer, 0, destinationData.Scan0, imageSize);            
            destination.UnlockBits(destinationData);
            return destination;
        }

        public List<Bitmap> CreateNewBitmaps(List<Bitmap> bitmapList)
        {
            List<Bitmap> newBitmaps = new List<Bitmap>();
            foreach (Bitmap oldBitmap in bitmapList)
            {
                MemoryStream memoryStream = new MemoryStream();
                oldBitmap.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Bmp);                
                Bitmap newBitmap = new Bitmap(new Bitmap(memoryStream));
                newBitmaps.Add(newBitmap);
            }
            return newBitmaps;
        }

        protected void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
	}    
}
