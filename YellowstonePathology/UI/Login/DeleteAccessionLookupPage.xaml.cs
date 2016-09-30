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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;

namespace YellowstonePathology.UI.Login
{
    /// <summary>
    /// Interaction logic for DeleteAccessionLookupPage.xaml
    /// </summary>
    public partial class DeleteAccessionLookupPage : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public delegate void NextEventHandler(object sender, CustomEventArgs.MasterAccessionNoReturnEventArgs e);
        public event NextEventHandler Next;
        public delegate void BackEventHandler(object sender, EventArgs e);
        public event BackEventHandler Back;

        private string m_PageHeaderText = "Lookup Accession to Delete";

        public DeleteAccessionLookupPage()
        {
            InitializeComponent();

            DataContext = this;
            Loaded += DeleteAccessionLookupPage_Loaded;
        }

        private void DeleteAccessionLookupPage_Loaded(object sender, RoutedEventArgs e)
        {
            this.TextBoxMasterAccessionNo.Focus();
        }

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            this.Back(this, new EventArgs());
        }

        private void ButtonNext_Click(object sender, RoutedEventArgs e)
        {
            if (this.TextBoxMasterAccessionNo.Text.Length >= 1)
            {
                Surgical.TextSearchHandler textSearchHandler = new Surgical.TextSearchHandler(this.TextBoxMasterAccessionNo.Text);
                object textSearchObject = textSearchHandler.GetSearchObject();
                if (textSearchObject is YellowstonePathology.Business.MasterAccessionNo)
                {
                    YellowstonePathology.Business.MasterAccessionNo masterAccessionNo = (YellowstonePathology.Business.MasterAccessionNo)textSearchObject;
                    YellowstonePathology.Business.Search.ReportSearchList reportSearchList = YellowstonePathology.Business.Gateway.ReportSearchGateway.GetReportSearchListByMasterAccessionNo(masterAccessionNo.Value);

                    if (reportSearchList.Count > 0)
                    {
                        this.Next(this, new CustomEventArgs.MasterAccessionNoReturnEventArgs(masterAccessionNo.Value));
                    }
                    else
                    {
                        MessageBox.Show("Unable to find the Accession.");
                    }
                }
                else
                {
                    MessageBox.Show("Enter a Master Accession Number.");
                }
            }
            else
            {
                MessageBox.Show("Enter a Master Accession Number.");
            }
        }

        public string PageHeaderText
        {
            get { return this.m_PageHeaderText; }
        }
    }
}
