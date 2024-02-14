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
using System.ComponentModel;

namespace YellowstonePathology.UI.Gross
{
    /// <summary>
    /// Interaction logic for DictationTemplateListPage.xaml
    /// </summary>
    public partial class DictationTemplateListPage : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public DictationTemplateListPage()
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

        public DictationTemplateCollection DictationTemplates
        {
            get { return DictationTemplateCollection.Instance; }
        }

        private void ListViewDictationTemplates_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (this.ListViewDictationTemplates.SelectedItem != null)
            {
                string id = ((DictationTemplate)this.ListViewDictationTemplates.SelectedItem).TemplateId;
                DictationTemplate dictationTemplate = DictationTemplateCollection.Instance.GetCloneByTemplateId(id);
                DictationTemplateEditPage dlg = new DictationTemplateEditPage(dictationTemplate);
                bool? dialogResult = dlg.ShowDialog();
                if (dialogResult.HasValue && dialogResult.Value == true)
                {
                    DictationTemplateCollection.Refresh();
                    NotifyPropertyChanged("DictationTemplates");
                }
            }
        }

        private void ButtonOk_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            DictationTemplateEditPage dlg = new DictationTemplateEditPage(null);
            bool? dialogResult = dlg.ShowDialog();
            if (dialogResult.HasValue && dialogResult.Value == true)
            {
                DictationTemplateCollection.Refresh();
                NotifyPropertyChanged("DictationTemplates");
            }
        }
    }
}
