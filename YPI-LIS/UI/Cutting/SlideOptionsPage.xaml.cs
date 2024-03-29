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
using System.ComponentModel;

namespace YellowstonePathology.UI.Cutting
{    
	public partial class SlideOptionsPage : UserControl, INotifyPropertyChanged 
	{
		public event PropertyChangedEventHandler PropertyChanged;

        public delegate void DeleteSlideOrderEventHandler(object sender, YellowstonePathology.UI.CustomEventArgs.SlideOrderReturnEventArgs eventArgs);
        public event DeleteSlideOrderEventHandler DeleteSlideOrder;

        public delegate void PrintSlideEventHandler(object sender, YellowstonePathology.UI.CustomEventArgs.SlideOrderReturnEventArgs eventArgs);
        public event PrintSlideEventHandler PrintSlide;

        public delegate void PrintPaperLabelEventHandler(object sender, YellowstonePathology.UI.CustomEventArgs.SlideOrderReturnEventArgs eventArgs);
        public event PrintPaperLabelEventHandler PrintPaperLabel;

        public delegate void CombineNextSlideEventHandler(object sender, YellowstonePathology.UI.CustomEventArgs.SlideOrderReturnEventArgs eventArgs);
        public event CombineNextSlideEventHandler CombineNextSlide;

        public delegate void UncombineEventHandler(object sender, YellowstonePathology.UI.CustomEventArgs.SlideOrderReturnEventArgs eventArgs);
        public event UncombineEventHandler Uncombine;

        public delegate void CloseEventHandler(object sender, EventArgs eventArgs);
        public event CloseEventHandler Close;
        
        private YellowstonePathology.Business.Slide.Model.SlideOrder m_SlideOrder;

        public SlideOptionsPage(YellowstonePathology.Business.Slide.Model.SlideOrder slideOrder)
        {
            this.m_SlideOrder = slideOrder;
			InitializeComponent();
			DataContext = this;            
		}       		           

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        private void ButtonDeleteSlide_Click(object sender, RoutedEventArgs e)
        {
            this.DeleteSlideOrder(this, new CustomEventArgs.SlideOrderReturnEventArgs(this.m_SlideOrder));
        }        
        
        private void ButtonPrintSlide_Click(object sender, RoutedEventArgs e)
        {
            if(this.m_SlideOrder.Printed == false)
            {
                this.PrintSlide(this, new CustomEventArgs.SlideOrderReturnEventArgs(this.m_SlideOrder));
            }
            else
            {
                MessageBoxResult messageBoxResult = MessageBox.Show("This slide has already been printed, are you sure you want to print it again.", "Print Slide Again?", MessageBoxButton.OKCancel);
                if(messageBoxResult == MessageBoxResult.OK)
                {
                    this.PrintSlide(this, new CustomEventArgs.SlideOrderReturnEventArgs(this.m_SlideOrder));
                }
            }
        }

        private void ButtonPrintPaperLabel_Click(object sender, RoutedEventArgs e)
        {
            this.PrintPaperLabel(this, new CustomEventArgs.SlideOrderReturnEventArgs(this.m_SlideOrder));
        }  

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close(this, new EventArgs());
        }

        private void ButtonCombineNextSlide_Click(object sender, RoutedEventArgs e)
        {
            this.CombineNextSlide(this, new CustomEventArgs.SlideOrderReturnEventArgs(this.m_SlideOrder));
        }

        private void ButtonUncombine_Click(object sender, RoutedEventArgs e)
        {
            this.Uncombine(this, new CustomEventArgs.SlideOrderReturnEventArgs(this.m_SlideOrder));
        }

        private void ButtonPrintAllPaperLabel_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
