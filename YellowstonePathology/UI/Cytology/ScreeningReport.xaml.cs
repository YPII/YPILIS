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

namespace YellowstonePathology.UI.Cytology
{
    /// <summary>
    /// Interaction logic for ScreeningReport.xaml
    /// </summary>
    public partial class ScreeningReport : FixedDocument
    {
        List<YellowstonePathology.Business.Search.CytologyScreeningSearchResult> m_ScreeningList;

        public ScreeningReport(List<YellowstonePathology.Business.Search.CytologyScreeningSearchResult> screeningList)
        {
            this.m_ScreeningList = screeningList;
            InitializeComponent();
            this.ListViewReportItems.DataContext = this;            
        }

        public List<YellowstonePathology.Business.Search.CytologyScreeningSearchResult> ScreeningList
        {
            get { return this.m_ScreeningList; }
            set { this.m_ScreeningList = value; }
        }

        public string CurrentDateTime
        {
            get { return DateTime.Now.ToString(); }
        }
    }
}
