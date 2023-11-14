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

namespace YellowstonePathology.UI.Cytology
{
    /// <summary>
    /// Interaction logic for CytologyMoreActionsWindow.xaml
    /// </summary>
    public partial class CytologyMoreActionsDialog : Window
    {
        CytologyMoreActionsEnum m_MoreActions;

        public CytologyMoreActionsDialog()
        {            
            InitializeComponent();
        }

        public CytologyMoreActionsEnum MoreActions
        {
            get { return this.m_MoreActions; }
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        private void ButtonOrderAcidWash_Click(object sender, RoutedEventArgs e)
        {
            this.m_MoreActions = CytologyMoreActionsEnum.OrderAcidWash;
            this.DialogResult = true;
            this.Close();
        }

        private void ButtonOrderDotReview_Click(object sender, RoutedEventArgs e)
        {
            this.m_MoreActions = CytologyMoreActionsEnum.OrderDotReview;
            this.DialogResult = true;
            this.Close();
        }

        private void ButtonScreeningUnfinal_Click(object sender, RoutedEventArgs e)
        {
            this.m_MoreActions = CytologyMoreActionsEnum.ScreeningUnfinal;
            this.DialogResult = true;
            this.Close();
        }

        private void ButtonDeleteScreening_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("This will permanently delete this Screening. Are you sure?", "Delete Screening?", MessageBoxButton.OKCancel);
            if (result == MessageBoxResult.OK)
            {
                this.m_MoreActions = CytologyMoreActionsEnum.DeleteScreening;
                this.DialogResult = true;
                this.Close();
            }
        }

        private void ButtonDeleteAcidWach_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("This will permanently delete this Acid Wash. Are you sure?", "Delete Acid Wash?", MessageBoxButton.OKCancel);
            if (result == MessageBoxResult.OK)
            {
                this.m_MoreActions = CytologyMoreActionsEnum.DeleteAcidWash;
                this.DialogResult = true;
                this.Close();
            }
        }

        private void ButtonClearCase_Click(object sender, RoutedEventArgs e)
        {
            this.m_MoreActions = CytologyMoreActionsEnum.ClearCase;
            this.DialogResult = true;
            this.Close();
        }
    }
}
