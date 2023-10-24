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
using System.IO;
using System.Windows.Threading;
using System.Threading.Tasks;

namespace YellowstonePathology.UI
{
    /// <summary>
    /// Interaction logic for WatchBitmap.xaml
    /// </summary>
    public partial class WatchBitmap : Window
    {
        BitmapSource m_Bitmap; 
        PixelFormat m_PixelFormat = PixelFormats.Rgb24; 
        int m_Width;
        int m_Height;
        int m_RawStride; 
        byte[] m_PixelData; 

        DispatcherTimer m_Timer;


        int m_Turn = 0;

        public WatchBitmap()
        {            
            InitializeComponent();

            this.m_Width = (int)this.Image1.Width;
            this.m_Height = (int)this.Image1.Height;
            this.m_RawStride = (this.m_Width * this.m_PixelFormat.BitsPerPixel + 7) / 8;
            this.m_PixelData = new byte[this.m_RawStride * this.m_Height];
        }

        private void SetPixel(int x, int y, Color c, byte[] buffer, int rawStride) 
        { 
            int xIndex = x * 3; 
            int yIndex = y * rawStride; 
            buffer[xIndex + yIndex] = c.R; 
            buffer[xIndex + yIndex + 1] = c.G; 
            buffer[xIndex + yIndex + 2] = c.B; 
        }

        private void Render() 
        {
            for (int y = 0 + this.m_Turn; y < this.m_Height + m_Turn; y++)
            {
                for (int x = 0; x < this.m_Width + this.m_Turn; x++)
                {
                    SetPixel(x, y, Colors.Blue, this.m_PixelData, this.m_RawStride);
                }
            }
            m_Turn += 10;
        }

        private void ButtonGo_Click(object sender, RoutedEventArgs e) 
        {
            this.m_Timer = new DispatcherTimer();
            this.m_Timer.Interval = TimeSpan.FromMilliseconds(5000);
            this.m_Timer.Tick += UpdateScreen;
            this.m_Timer.Start(); 
        }        

        private void UpdateScreen(object o, EventArgs e) 
        {
            this.Render();
            this.m_Bitmap = BitmapSource.Create(this.m_Width, this.m_Height, 96, 96, this.m_PixelFormat, null, this.m_PixelData, this.m_RawStride); 
            this.Image1.Source = this.m_Bitmap; 
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e) 
        { 
            this.m_Timer.Stop(); 
        }        
    }
}
