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
    /// <summary>
    /// Interaction logic for SpecimenSelection.xaml
    /// </summary>

    /*public partial class SpecimenSelection : System.Windows.Window
    {
        YellowstonePathology.Business.Surgical.SpecimenCollection m_SpecimenCollection;

        public SpecimenSelection(YellowstonePathology.Business.Surgical.SpecimenCollection specimenCollection)
        {
            this.m_SpecimenCollection = specimenCollection;
            InitializeComponent();
            this.DataContext = this.m_SpecimenCollection;
        }

        public YellowstonePathology.Business.Surgical.SpecimenItem SpecimenItem
        {
            get
            {
                if (this.ListViewSpecimen.SelectedItem != null)
                {
                    YellowstonePathology.Business.Surgical.SpecimenItem specimenItem = (YellowstonePathology.Business.Surgical.SpecimenItem)this.ListViewSpecimen.SelectedItem;
                    return specimenItem;
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
    }*/
}