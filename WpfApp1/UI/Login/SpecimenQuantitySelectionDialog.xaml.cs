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

namespace YellowstonePathology.UI.Login
{
    /// <summary>
    /// Interaction logic for SpecimenQuantitySelectionDialog.xaml
    /// </summary>
    public partial class SpecimenQuantitySelectionDialog : Window
    {			
        private int m_Quantity;

		public SpecimenQuantitySelectionDialog()
        {			
            InitializeComponent();
        }

        public int Quantity
        {
            get { return this.m_Quantity; }            
        }

        private void ButtonQuantity_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
			this.m_Quantity = Convert.ToInt32(button.Tag.ToString());
            this.DialogResult = true;      
			Close();            
        }		

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
	}
}
