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
    /// Interaction logic for SpecimenAdequacyDialog.xaml
    /// </summary>
    public partial class SpecimenAdequacyDialog : Window
    {
		YellowstonePathology.Business.Cytology.Model.SpecimenAdequacyCollection m_SpecimenAdequacyCollection;
		YellowstonePathology.Business.Cytology.Model.SpecimenAdequacyCommentCollection m_SpecimenAdequacyCommentCollection;

        public SpecimenAdequacyDialog()
        {
			this.m_SpecimenAdequacyCollection = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetSpecimenAdequacy();
			this.m_SpecimenAdequacyCommentCollection = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetSpecimenAdequacyComments();

            InitializeComponent();

            this.DataContext = this;
        }

		public YellowstonePathology.Business.Cytology.Model.SpecimenAdequacyCollection SpecimenAdequacyCollection
        {
            get { return this.m_SpecimenAdequacyCollection; }
            set { this.m_SpecimenAdequacyCollection = value; }
        }

		public YellowstonePathology.Business.Cytology.Model.SpecimenAdequacyCommentCollection SpecimenAdequacyCommentCollection
        {
            get { return this.m_SpecimenAdequacyCommentCollection; }
            set { this.m_SpecimenAdequacyCommentCollection = value; }
        }

		public YellowstonePathology.Business.Cytology.Model.SpecimenAdequacy SelectedSpecimenAdequacy
        {
            get
            {
				YellowstonePathology.Business.Cytology.Model.SpecimenAdequacy specimenAdequacy = null;
                if (this.ListBoxSpecimenAdequacy.SelectedItem != null)
                {
					specimenAdequacy = (YellowstonePathology.Business.Cytology.Model.SpecimenAdequacy)this.ListBoxSpecimenAdequacy.SelectedItem;
                }
                return specimenAdequacy;
            }
        }

        public string SelectedSpecimenAdequacyComment
        {
            get
            {
				string result = string.Empty;
				foreach (YellowstonePathology.Business.Cytology.Model.SpecimenAdequacyComment comment in this.ListViewComments.SelectedItems)
				{
					result += comment.Comment + "  ";
				}

                return result.Trim();
            }
        }

        private void ButtonOK_Click(object sender, RoutedEventArgs e)
        {
            if (this.SelectedSpecimenAdequacy != null)
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
