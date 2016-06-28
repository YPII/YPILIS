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
    public partial class CytologyReportCommentDialog : Window
    {
        YellowstonePathology.Business.Domain.Cytology.CytologyReportCommentCollection m_CytologyReportComments;

        public CytologyReportCommentDialog()
        {
			this.m_CytologyReportComments = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetCytologyReportComments();

            InitializeComponent();

            this.DataContext = this;
        }

        public YellowstonePathology.Business.Domain.Cytology.CytologyReportCommentCollection CytologyReportComments
        {
            get { return this.m_CytologyReportComments; }            
        }

        public string SelectedReportComment
        {
            get
            {
                StringBuilder result = new StringBuilder();
                if (this.ListViewReportComments.SelectedItem != null)
                {
                    for (int i = 0; i < this.ListViewReportComments.SelectedItems.Count; i++ )
                    {
                        YellowstonePathology.Business.Domain.Cytology.CytologyReportComment reportComment = (YellowstonePathology.Business.Domain.Cytology.CytologyReportComment)this.ListViewReportComments.SelectedItems[i];
                        result.Append(reportComment.Comment);
                        if (i != this.ListViewReportComments.SelectedItems.Count - 1)
                        {
                            result.Append("  ");
                        }
                    }                       
                }
                return result.ToString();
            }
        }

        private void ButtonOK_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedReportComment != null)
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
