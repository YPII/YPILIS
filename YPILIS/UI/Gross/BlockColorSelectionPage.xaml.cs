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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace YellowstonePathology.UI.Gross
{
	/// <summary>
	/// Interaction logic for BlockColorSelectionPage.xaml
	/// </summary>
	public partial class BlockColorSelectionPage : UserControl
	{
		public delegate void NextEventHandler(object sender, UI.CustomEventArgs.SpecimenOrderReturnEventArgs e);
		public event NextEventHandler Next;

		private YellowstonePathology.Business.Specimen.Model.SpecimenOrder m_SpecimenOrder;
		private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;        
        private Business.Label.Model.Carousel m_Carousel;

		public BlockColorSelectionPage(YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder, YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
		{
            this.m_SpecimenOrder = specimenOrder;
			this.m_AccessionOrder = accessionOrder;

            Business.Label.Model.CassettePrinterCollection printers = new Business.Label.Model.CassettePrinterCollection();
            Business.Label.Model.CassettePrinter printer = printers.GetPrinter(Business.User.UserPreferenceInstance.Instance.UserPreference.CassettePrinter);
            this.m_Carousel = printer.Carousel;

			InitializeComponent();
			DataContext = this;
		}

		public Business.Label.Model.Carousel Carousel
		{
			get { return this.m_Carousel; }
		}		

		private void ListBoxColor_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (this.ListBoxColor.SelectedItem != null)
			{
				Business.Label.Model.CarouselColumn column = (Business.Label.Model.CarouselColumn)this.ListBoxColor.SelectedItem;
                this.m_AccessionOrder.CassetteColor = column.CassetteColor;

                YellowstonePathology.UI.CustomEventArgs.SpecimenOrderReturnEventArgs specimenOrderReturnEventArgs = new CustomEventArgs.SpecimenOrderReturnEventArgs(this.m_SpecimenOrder);
                this.Next(this, specimenOrderReturnEventArgs);
			}
		}				
	}
}
