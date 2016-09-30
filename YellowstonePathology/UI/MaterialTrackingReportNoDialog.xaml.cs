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
using System.ComponentModel;

namespace YellowstonePathology.UI
{
	/// <summary>
	/// Interaction logic for MaterialTrackingReportNoDialog.xaml
	/// </summary>
	public partial class MaterialTrackingReportNoDialog : Window, INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		private YellowstonePathology.Business.Slide.Model.SlideOrderCollection m_SlideOrderCollection;
		private YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingLogCollection m_SlideTrackingLogCollection;

		public MaterialTrackingReportNoDialog()
		{
			this.m_SlideOrderCollection = new YellowstonePathology.Business.Slide.Model.SlideOrderCollection();
			this.m_SlideTrackingLogCollection = new YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingLogCollection();

			InitializeComponent();
			DataContext = this;

            this.Loaded += new RoutedEventHandler(MaterialTrackingReportNoDialog_Loaded);
		}

        private void MaterialTrackingReportNoDialog_Loaded(object sender, RoutedEventArgs e)
        {
            this.TextBoxReportNo.Focus();
        }

		public YellowstonePathology.Business.Slide.Model.SlideOrderCollection SlideOrderCollection
		{
			get{ return this.m_SlideOrderCollection; }
		}

		public YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingLogCollection SlideTrackingLogCollection
		{
			get { return this.m_SlideTrackingLogCollection; }
		}

		private void ButtonSearch_Click(object sender, RoutedEventArgs e)
		{
			this.GetSlides();
		}

		private void ButtonOK_Click(object sender, RoutedEventArgs e)
		{
			Close();
		}

		private void GetSlides()
		{
			if (!string.IsNullOrEmpty(this.TextBoxReportNo.Text))
			{
				this.m_SlideOrderCollection = YellowstonePathology.Business.Gateway.SlideAccessionGateway.GetSlideOrdersByReportNo(this.TextBoxReportNo.Text);
				NotifyPropertyChanged("SlideOrderCollection");
			}
		}

		private void TextBoxReportNo_KeyUp(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Enter)
			{
				this.GetSlides();
			}
		}	

		private void ListBoxSlides_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			if (this.ListBoxSlides.SelectedItem != null)
			{
				YellowstonePathology.Business.Slide.Model.SlideOrder slideOrder = (YellowstonePathology.Business.Slide.Model.SlideOrder)this.ListBoxSlides.SelectedItem;
				this.m_SlideTrackingLogCollection = YellowstonePathology.Business.Gateway.SlideAccessionGateway.GetMaterialTrackingLogCollectionByMaterialId(slideOrder.SlideOrderId.ToString());
				NotifyPropertyChanged("SlideTrackingLogCollection");
			}
		}

		public void NotifyPropertyChanged(String info)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(info));
			}
		}

        private void ListBoxSlides_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.ListBoxSlides.SelectedItem != null)
            {
                YellowstonePathology.Business.Slide.Model.SlideOrder slideOrder = (YellowstonePathology.Business.Slide.Model.SlideOrder)this.ListBoxSlides.SelectedItem;
				this.m_SlideTrackingLogCollection = YellowstonePathology.Business.Gateway.SlideAccessionGateway.GetMaterialTrackingLogCollectionByMaterialId(slideOrder.SlideOrderId.ToString());
                NotifyPropertyChanged("SlideTrackingLogCollection");
            }
        }
	}
}
