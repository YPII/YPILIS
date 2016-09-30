using System;
using System.Collections.Generic;
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
using System.IO;

namespace YellowstonePathology.UI
{
    /// <summary>
    /// Interaction logic for Patient.xaml
    /// </summary>

    public partial class PatientWorkspace : System.Windows.Controls.UserControl
    {
        //WPF.BusinessObjects.BaseCase m_BaseCase;

        public PatientWorkspace()
        {
            //this.m_BaseCase = new YellowstonePathology.WPF.BusinessObjects.BaseCase();

            InitializeComponent();
        }

        //public WPF.BusinessObjects.BaseCase BaseCase
        //{
        //    get { return this.m_BaseCase; }
        //    set { this.m_BaseCase = value; }
        //}

        public void ListViewPatientHistory_DoubleClick(object sender, RoutedEventArgs args)
        {
            /*
            if (this.listViewPatientHistory.SelectedItems.Count != 0)
            {
                WPF.BusinessObjects.Patient.PatientHistoryListItem item = (WPF.BusinessObjects.Patient.PatientHistoryListItem)this.listViewPatientHistory.SelectedItem;
                string reportNo = item.AccessionNo;
                string fileName = Routines.CommonRoutines.getCaseFileNameDoc(reportNo);
                if (File.Exists(fileName))
                {
                    Routines.CommonRoutines.OpenFileInWordViewer(fileName);
                }
                else
                {
                    MessageBox.Show("File does not exist.");
                }
            }
            */
        }

    }
}