using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace YellowstonePathology.UI.Surgical
{
	public partial class SpecimenSelectionV2 : Window
	{
		YellowstonePathology.Business.Test.Surgical.SurgicalSpecimenCollection m_SurgicalSpecimenCollection;

		public SpecimenSelectionV2(YellowstonePathology.Business.Test.Surgical.SurgicalSpecimenCollection surgicalSpecimenCollection)
        {
			this.m_SurgicalSpecimenCollection = surgicalSpecimenCollection;
            InitializeComponent();
			this.DataContext = this.m_SurgicalSpecimenCollection;
        }

		public YellowstonePathology.Business.Test.Surgical.SurgicalSpecimen SurgicalSpecimen
        {
            get
            {
                if (this.ListViewSpecimen.SelectedItem != null)
                {
					return (YellowstonePathology.Business.Test.Surgical.SurgicalSpecimen)this.ListViewSpecimen.SelectedItem;
                }
                else
                {
                    return null;
                }
            }
        }

        public void ButtonCancel_Click(object sender, RoutedEventArgs args)
        {
            this.DialogResult = false;
        }

        public void ButtonOk_Click(object sender, RoutedEventArgs args)
        {
            this.DialogResult = true;
        }
    }
}
