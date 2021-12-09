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

namespace YellowstonePathology.UI
{
    /// <summary>
    /// Interaction logic for RetrospectiveReviews.xaml
    /// </summary>
    public partial class RetrospectiveReviews : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private YellowstonePathology.UI.RetrospectiveReviewList m_RetrospectiveReviewList;
        private DateTime m_WorkDate;

        public RetrospectiveReviews()
        {            
            if (DateTime.Today.DayOfWeek == DayOfWeek.Monday)
            {
                this.m_WorkDate = DateTime.Today.AddDays(-3);
            }
            else
            {
                this.m_WorkDate = DateTime.Today.AddDays(-1);
            }

            this.m_RetrospectiveReviewList = Business.Gateway.ReportSearchGateway.GetRetrospectiveReviews(this.m_WorkDate);
            this.NotifyPropertyChanged("RetrospectiveReviewList");

            InitializeComponent();

            this.DataContext = this;            
        }

        public DateTime WorkDate
        {
            get { return this.m_WorkDate; }
            set { this.m_WorkDate = value; }
        }

        private void AddRandomTest(DateTime workDate)
        {
            List<int> exclusionList = new List<int>();
            exclusionList.Add(262);
            exclusionList.Add(197);

            Business.Search.ReportSearchList list = Business.Gateway.ReportSearchGateway.GetPossibleRetrospectiveReviews(workDate);
            if(list.Count != 0)
            {
                int count = list.Count;

                double tenPercentOfCount = Math.Round((count * .1), 0);

                Random rnd = new Random();
                int i = 0;
                while (true)
                {
                    int nextRnd = rnd.Next(0, count - 1);
                    string nextMasterAccessionNo = list[nextRnd].MasterAccessionNo;

                    Business.Test.AccessionOrder ao = Business.Persistence.DocumentGateway.Instance.PullAccessionOrder(nextMasterAccessionNo, this);
                    
                    if (ao.PanelSetOrderCollection.HasPanelSetBeenOrdered(exclusionList) == false)
                    {
                        YellowstonePathology.Business.Test.RetrospectiveReview.RetrospectiveReviewTest retrospectiveReviewTest = new YellowstonePathology.Business.Test.RetrospectiveReview.RetrospectiveReviewTest();
                        YellowstonePathology.Business.Test.TestOrderInfo testOrderInfo = new Business.Test.TestOrderInfo(retrospectiveReviewTest, null, false);
                        YellowstonePathology.Business.Visitor.OrderTestOrderVisitor orderTestOrderVisitor = new Business.Visitor.OrderTestOrderVisitor(testOrderInfo);
                        ao.TakeATrip(orderTestOrderVisitor);
                        Business.Test.PanelSetOrder pso = ao.PanelSetOrderCollection.GetPanelSetOrder(262);
                        pso.AssignedToId = 0;                         
                    }

                    i += 1;
                    if (i == tenPercentOfCount)
                    {
                        Business.Persistence.DocumentGateway.Instance.Push(this);
                        break;
                    }
                }
            }
            else
            {
                MessageBox.Show("There are no cases to choose from for this day.");
            }            
        }

        private void ContextMenuDeleteReview_Click(object sender, RoutedEventArgs e)
        {
            if(this.ListViewReviews.SelectedItems.Count != 0)
            {
                foreach (UI.RetrospectiveReviewListItem item in this.ListViewReviews.SelectedItems)
                {
                    Business.Test.AccessionOrder ao = Business.Persistence.DocumentGateway.Instance.PullAccessionOrder(item.MasterAccessionNo, this);
                    Business.Test.PanelSetOrder pso = ao.PanelSetOrderCollection.GetPanelSetOrder(item.ReportNo);
                    ao.PanelSetOrderCollection.Remove(pso);
                    Business.Persistence.DocumentGateway.Instance.Push(this);
                }

                this.m_RetrospectiveReviewList = Business.Gateway.ReportSearchGateway.GetRetrospectiveReviews(this.m_WorkDate);
                this.NotifyPropertyChanged("RetrospectiveReviewList");
            }            
        }

        public YellowstonePathology.UI.RetrospectiveReviewList RetrospectiveReviewList
        {
            get { return this.m_RetrospectiveReviewList; }
        }                       

        private void ButtonAddRandom_Click(object sender, RoutedEventArgs e)
        {
            if (this.m_RetrospectiveReviewList.Count == 0)
            {
                this.AddRandomTest(this.m_WorkDate);
                this.m_RetrospectiveReviewList = Business.Gateway.ReportSearchGateway.GetRetrospectiveReviews(this.m_WorkDate);
                this.NotifyPropertyChanged("RetrospectiveReviewList");
            }
            else
            {
                MessageBox.Show("Unable to add random test because some already exist.");
            }
        }

        private void ButtonKillList_Click(object sender, RoutedEventArgs e)
        {
            this.m_RetrospectiveReviewList = Business.Gateway.ReportSearchGateway.GetRetrospectiveReviewKillList();
            this.NotifyPropertyChanged("RetrospectiveReviewList");
        }

        private void ButtonAccessionOrderBack_Click(object sender, RoutedEventArgs e)
        {
            this.m_WorkDate = this.m_WorkDate.AddDays(-1);
            this.m_RetrospectiveReviewList = Business.Gateway.ReportSearchGateway.GetRetrospectiveReviews(this.m_WorkDate);
            this.NotifyPropertyChanged(string.Empty);
        }

        private void ButtonAccessionOrderForward_Click(object sender, RoutedEventArgs e)
        {
            this.m_WorkDate = this.m_WorkDate.AddDays(1);
            this.m_RetrospectiveReviewList = Business.Gateway.ReportSearchGateway.GetRetrospectiveReviews(this.m_WorkDate);
            this.NotifyPropertyChanged(string.Empty);
        }

        private void ButtonAccessionOrderRefresh_Click(object sender, RoutedEventArgs e)
        {
            this.m_RetrospectiveReviewList = Business.Gateway.ReportSearchGateway.GetRetrospectiveReviews(this.m_WorkDate);
            this.NotifyPropertyChanged(string.Empty);
        }

        private void ButtonPrintList_Click(object sender, RoutedEventArgs e)
        {
            UI.RetrospectiveReviewReport report = new UI.RetrospectiveReviewReport(this.m_RetrospectiveReviewList);
            YellowstonePathology.UI.XpsDocumentViewer xpsDocumentViewer = new XpsDocumentViewer();
            xpsDocumentViewer.LoadDocument(report.FixedDocument);
            xpsDocumentViewer.ShowDialog();
        }

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }        
    }
}
