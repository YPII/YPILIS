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
using System.Windows.Interop;
using System.Runtime.InteropServices;
using YellowstonePathology.Business.Twain;
using System.ComponentModel;
using System.IO;

namespace YellowstonePathology.UI.Login.FinalizeAccession
{	
    public partial class DocumentScanningPage : UserControl, INotifyPropertyChanged
	{
        public event PropertyChangedEventHandler PropertyChanged;

		public delegate void ReturnEventHandler(object sender, UI.Navigation.PageNavigationReturnEventArgs e);
		public event ReturnEventHandler Return;

        private Twain m_Twain;
		private YellowstonePathology.Business.Common.PageScannerCollection m_PageScannerCollection;

        private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;        
        private string m_PageHeaderText;

        private List<Image> m_ImageList;                
        private string m_FileName;

        public DocumentScanningPage(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {
            this.m_AccessionOrder = accessionOrder;
            this.m_PageHeaderText = "Master Accession: " + accessionOrder.MasterAccessionNo;
			this.m_PageScannerCollection = new Business.Common.PageScannerCollection();

            InitializeComponent();

            this.DataContext = this;
            this.Loaded += new RoutedEventHandler(DocumentScanningPage_Loaded);
        }

        private void DocumentScanningPage_Loaded(object o, RoutedEventArgs e)
        {            
            this.m_ImageList = new List<Image>();
            this.m_Twain = new Twain(new WpfWindowMessageHook(Window.GetWindow(this)));
            this.m_Twain.TransferImage += new EventHandler<TransferImageEventArgs>(Twain_TransferImage);
            this.m_Twain.ScanningComplete += new EventHandler<ScanningCompleteEventArgs>(Twain_ScanningComplete);
            this.ButtonNext.Focus();
        }

        public string FileName
        {
            get { return this.m_FileName; }
            set
            {
                if (this.m_FileName != value)
                {
                    this.m_FileName = value;
                    this.NotifyPropertyChanged("FileName");
                }
            }
        }                

        private void Twain_ScanningComplete(object sender, ScanningCompleteEventArgs e)
        {
            IsEnabled = true;
			YellowstonePathology.Business.OrderIdParser orderIdParser = new Business.OrderIdParser(this.m_AccessionOrder.MasterAccessionNo);            
            this.m_FileName = YellowstonePathology.Business.Requisition.GetNextFileName(orderIdParser);
            this.NotifyPropertyChanged("FileName");
            YellowstonePathology.Business.Requisition.Save(this.m_FileName, this.m_ImageList);
            //this.SavePNG();
        }

        private void SavePNG()
        {
            string filePath = @"\\CFILESERVER\Documents\Scanning\png\" + this.m_AccessionOrder.MasterAccessionNo;
            System.Windows.MessageBox.Show(this.m_ImageList.Count.ToString());
            foreach (System.Windows.Controls.Image visual in this.m_ImageList)
            {
                var encoder = new PngBitmapEncoder();
                RenderTargetBitmap bitmap = new RenderTargetBitmap((int)visual.ActualWidth, (int)visual.ActualHeight, 96, 96, PixelFormats.Pbgra32);
                Size visualSize = new Size(visual.ActualWidth, visual.ActualHeight);
                visual.Measure(visualSize);
                visual.Arrange(new Rect(visualSize));
                bitmap.Render(visual);
                BitmapFrame frame = BitmapFrame.Create(bitmap);
                encoder.Frames.Add(frame);

                Random rnd = new Random();
                using (FileStream stream = File.Create(filePath + rnd.Next(1000, 9000).ToString() + ".png"))
                {
                    encoder.Save(stream);
                }
            }
        }

        private void Twain_TransferImage(object sender, TransferImageEventArgs e)
        {
            if (e.Image != null)
            {
                Image image = new Image();                
                image.Source = Imaging.CreateBitmapSourceFromHBitmap(
                        new System.Drawing.Bitmap(e.Image).GetHbitmap(),
                        IntPtr.Zero,
                        Int32Rect.Empty,
                        BitmapSizeOptions.FromEmptyOptions());
                image.Margin = new Thickness(2);
                this.StackPanelImages.Children.Add(image);
                this.m_ImageList.Add(image);                
            }
        }        

        private void ButtonScan_Click(object sender, RoutedEventArgs e)
        {                        
            try
            {
                YellowstonePathology.Business.Common.PageScanner pageScanner = this.m_PageScannerCollection.SelectedPageScanner;                    
                this.m_Twain.SelectSource(pageScanner.ScannerName);
                this.m_Twain.StartScanning(pageScanner.ScanSettings);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }            
        }

		public string PageHeaderText
		{
			get { return this.m_PageHeaderText; }
		}		

		private void ButtonBack_Click(object sender, RoutedEventArgs e)
		{            
			UI.Navigation.PageNavigationReturnEventArgs args = new UI.Navigation.PageNavigationReturnEventArgs(UI.Navigation.PageNavigationDirectionEnum.Back, null);
			this.Return(this, args);
		}

		private void ButtonNext_Click(object sender, RoutedEventArgs e)
		{
			UI.Navigation.PageNavigationReturnEventArgs args = new UI.Navigation.PageNavigationReturnEventArgs(UI.Navigation.PageNavigationDirectionEnum.Next, null);
			this.Return(this, args);
		}				

        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {            
            if (string.IsNullOrEmpty(this.m_FileName) == false)
            {
                if (System.IO.File.Exists(this.m_FileName) == true)
                {
                    try
                    {
                        System.IO.File.Delete(this.m_FileName);
                        foreach (UIElement element in this.m_ImageList)
                        {
                            this.StackPanelImages.Children.Remove(element);
                        }
                        this.m_ImageList.Clear();
                    }
                    catch (Exception ioerror)
                    {
                        MessageBox.Show("Unable to delete the file: " + ioerror.Message);
                    }
                }
                this.m_FileName = null;
                this.NotifyPropertyChanged("FileName");
            }            
        }               

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }		

        private void ButtonPrint_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process p = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo info = new System.Diagnostics.ProcessStartInfo(this.m_FileName);
            p.StartInfo = info;
            p.StartInfo.Verb = "print";
            p.Start(); 
        }        
	}
}
