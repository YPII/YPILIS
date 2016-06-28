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
    /// Interaction logic for OtherConditionDialog.xaml
    /// </summary>
    public partial class OtherConditionDialog : Window
    {
        YellowstonePathology.Business.Domain.Cytology.OtherConditionCollection m_OtherConditionCollection;

        public OtherConditionDialog()
        {
			this.m_OtherConditionCollection = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetOtherConditions();
            InitializeComponent();
            this.DataContext = this;
        }

        public YellowstonePathology.Business.Domain.Cytology.OtherConditionCollection OtherConditionCollection
        {
            get { return this.m_OtherConditionCollection; }            
        }

        public string SelectedOtherCondition
        {
            get
            {
                string result = string.Empty;
                if (this.ListViewOtherConditions.SelectedItems.Count != 0)
                {
                    foreach (YellowstonePathology.Business.Domain.Cytology.OtherCondition otherCondition in this.ListViewOtherConditions.SelectedItems)
                    {
						result += " " + otherCondition.OtherConditionText;
                    }                    
                }
                return result.Trim();
            }
        }

        private void ButtonOK_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedOtherCondition != string.Empty)
            {
                this.DialogResult = true;
            }
            else
            {
                this.DialogResult = false;
            }
            this.Close();
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
    }
}
