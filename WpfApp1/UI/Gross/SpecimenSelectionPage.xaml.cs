using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace YellowstonePathology.UI.Gross
{
    /// <summary>
    /// Interaction logic for SpecimenSelectionPage.xaml
    /// </summary>
    public partial class SpecimenSelectionPage : Window
    {
        private YellowstonePathology.Business.Specimen.Model.SpecimenCollection m_TargetCollection;
        public SpecimenSelectionPage(YellowstonePathology.Business.Specimen.Model.SpecimenCollection specimenCollection)
        {
            this.m_TargetCollection = specimenCollection;
            InitializeComponent();
            DataContext = this;
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ButtonOk_Click(object sender, RoutedEventArgs e)
        {
            if(this.ListViewSpecimenCollection.SelectedItems.Count > 0)
            {
                foreach(YellowstonePathology.Business.Specimen.Model.Specimen specimen in this.ListViewSpecimenCollection.SelectedItems)
                {
                    if(this.m_TargetCollection.Exists(specimen.SpecimenId) == false)
                    {
                        this.m_TargetCollection.Add(specimen);
                    }
                }
            }
            Close();
        }

        public YellowstonePathology.Business.Specimen.Model.SpecimenCollection SpecimenCollection
        {
            get { return YellowstonePathology.Business.Specimen.Model.SpecimenCollection.Instance; }
        }
    }
}
