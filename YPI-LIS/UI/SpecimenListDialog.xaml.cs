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
using System.ComponentModel;

namespace YellowstonePathology.UI
{
    /// <summary>
    /// Interaction logic for SpecimenListDialog.xaml
    /// </summary>
    public partial class SpecimenListDialog : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public SpecimenListDialog()
        {
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

        public YellowstonePathology.Business.Specimen.Model.SpecimenCollection SpecimenCollection
        {
            get { return YellowstonePathology.Business.Specimen.Model.SpecimenCollection.Instance; }
        }

        private void ButtonOK_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ListBoxSpecimen_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (this.ListBoxSpecimen.SelectedItem != null)
            {
                YellowstonePathology.Business.Specimen.Model.Specimen specimen = (YellowstonePathology.Business.Specimen.Model.Specimen)this.ListBoxSpecimen.SelectedItem;
                if (string.IsNullOrEmpty(specimen.SpecimenId) == false)
                {
                    SpecimenEditDialog dlg = new SpecimenEditDialog(specimen);
                    bool? dialogResult = dlg.ShowDialog();
                    if (dialogResult.HasValue && dialogResult.Value == true)
                    {
                        YellowstonePathology.Business.Specimen.Model.SpecimenCollection.Refresh();
                        this.NotifyPropertyChanged("SpecimenCollection");
                    }
                }
                else
                {
                    MessageBox.Show("The Null Specimen may not be altered.");
                }
            }
        }

        private void ButtonNewSpecimen_Click(object sender, RoutedEventArgs e)
        {
            SpecimenEditDialog dlg = new SpecimenEditDialog(null);
            bool? dialogResult = dlg.ShowDialog();
            if (dialogResult.HasValue && dialogResult.Value == true)
            {
                YellowstonePathology.Business.Specimen.Model.SpecimenCollection.Refresh();
                this.NotifyPropertyChanged("SpecimenCollection");
            }
        }
    }
}
