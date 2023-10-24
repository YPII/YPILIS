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

namespace YellowstonePathology.UI.CustomControls
{
    /// <summary>
    /// Interaction logic for ListBox.xaml
    /// </summary>
    public partial class RadioListBox : ListBox
    {
        public RadioListBox() 
        { 

        }

        public new SelectionMode SelectionMode 
        {
            get { return System.Windows.Controls.SelectionMode.Single;  }
        }

        public bool IsTransparent 
        {
            get { return false; }
        }
       
        public void ItemRadioClick(object sender, RoutedEventArgs e)
        {
            ListBoxItem sel = (e.Source as RadioButton).TemplatedParent as ListBoxItem;
            int newIndex = this.ItemContainerGenerator.IndexFromContainer(sel); 
            this.SelectedIndex = newIndex;
        }

        protected override void OnSelectionChanged(SelectionChangedEventArgs e)
        {
            base.OnSelectionChanged(e);

            CheckRadioButtons(e.RemovedItems, false);
            CheckRadioButtons(e.AddedItems, true);
        }

        private void CheckRadioButtons(System.Collections.IList radioButtons, bool isChecked) 
        {
            foreach (object item in radioButtons)
            {
                ListBoxItem lbi = 
                  this.ItemContainerGenerator.ContainerFromItem(item) as ListBoxItem;

                if (lbi != null)
                {
                    RadioButton radio = lbi.Template.FindName("radio", lbi) as RadioButton;
                    if (radio != null)
                        radio.IsChecked = isChecked;
                }
            }
        }        
    }
}
