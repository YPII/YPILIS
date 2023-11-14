using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace YellowstonePathology.Business.Slide.Model
{
    public class VantageSlideView : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private VantageSlide m_VantageSlide;
        private BarcodeScanning.VantageBarcode m_VantageBarcode;
        private System.Windows.Media.Brush m_ScanStatusColor;

        public VantageSlideView(VantageSlide vantageSlide, System.Windows.Media.Brush brush)
        {
            this.m_VantageSlide = vantageSlide;
            this.m_VantageBarcode = new BarcodeScanning.VantageBarcode(this.m_VantageSlide.VantageSlideId);
            this.m_ScanStatusColor = brush;
        }

        public VantageSlide VantageSlide
        {
            get { return this.m_VantageSlide; }
        }

        public BarcodeScanning.VantageBarcode VantageBarcode
        {
            get { return this.m_VantageBarcode; }
        }

        public System.Windows.Media.Brush ScanStatusColor
        {
            get { return this.m_ScanStatusColor; }
            set
            {
                this.m_ScanStatusColor = value;
                this.NotifyPropertyChanged("ScanStatusColor");
            }
        }

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
    }
}
