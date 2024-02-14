using System;
using System.IO;
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

namespace YellowstonePathology.UI.Client
{
    /// <summary>
    /// Interaction logic for HPVRuleDialog.xaml
    /// </summary>
    public partial class HPVRuleDialog : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private YellowstonePathology.Business.Client.Model.StandingOrderViewCollection m_StandingOrderViewCollection;
        private Visibility m_CompoundRuleVisibility;
        private Visibility m_HPV1618RuleVisibility;
        private Visibility m_CompoundRuleHPV1618Visibility;

        public HPVRuleDialog(YellowstonePathology.Business.Client.Model.StandingOrderViewTypeEnum viewType, string standingOrderCode)
        {
            this.m_StandingOrderViewCollection = new Business.Client.Model.StandingOrderViewCollection(viewType);
            this.m_HPV1618RuleVisibility = viewType == Business.Client.Model.StandingOrderViewTypeEnum.HPV1618Only ? Visibility.Visible : Visibility.Collapsed;
            this.m_CompoundRuleVisibility = Visibility.Collapsed;
            this.m_CompoundRuleHPV1618Visibility = Visibility.Collapsed;
            InitializeComponent();
            DataContext = this;
        }

        public YellowstonePathology.Business.Client.Model.StandingOrderViewCollection StandingOrderViewCollection
        {
            get { return this.m_StandingOrderViewCollection; }
        }

        public Visibility CompoundRuleVisibility
        {
            get { return this.m_CompoundRuleVisibility; }
        }

        public Visibility HPV1618RuleVisibility
        {
            get { return this.m_HPV1618RuleVisibility; }
        }

        public Visibility CompoundRuleHPV1618Visibility
        {
            get { return this.m_CompoundRuleVisibility == Visibility.Visible && m_HPV1618RuleVisibility == Visibility.Visible ? Visibility.Visible : Visibility.Collapsed; }
        }

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        private void ListBoxStandingOrders_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if(this.ListViewStandingOrders.SelectedItem != null)
            {
                YellowstonePathology.Business.Client.Model.StandingOrderView view = (Business.Client.Model.StandingOrderView)this.ListViewStandingOrders.SelectedItem;
                MessageBox.Show(view.ReflexDescription);
            }
        }

        private void ListViewStandingOrders_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.ListViewStandingOrders.SelectedItem != null)
            {
                YellowstonePathology.Business.Client.Model.StandingOrderView view = (Business.Client.Model.StandingOrderView)this.ListViewStandingOrders.SelectedItem;
                this.SetCompoundRuleVisibility(view);
            }
        }

        private void ButtonOK_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void SetCompoundRuleVisibility(YellowstonePathology.Business.Client.Model.StandingOrderView view)
        {
            this.m_CompoundRuleVisibility = view.IsCompoundRule == true ? Visibility.Visible : Visibility.Collapsed;
            this.NotifyPropertyChanged("CompoundRuleVisibility");
            this.NotifyPropertyChanged("CompoundRuleHPV1618Visibility");
        }

        private void ButtonRule_Click(object sender, RoutedEventArgs e)
        {
            int cnt = 0;
            using (StreamWriter sw = new StreamWriter(@"C:\ProgramData\ypi\lisdata\HPVStandingOrder.txt", false))
            {
                foreach(YellowstonePathology.Business.Client.Model.StandingOrderView view in this.m_StandingOrderViewCollection)
                {
                    if (view.IsCompoundRule == false)
                    {
                        cnt++;
                        string id = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
                        string insertString = "Insert into `tblHPVStandingOrder` (`HPVStandingOrderId`, `Age`, `PAPResult`, `HPVResult`, `HPVTesting`, " +
                            "`Endocervical`, `HPVStandingOrderName`) values('" + id + "', '" + view.PatientAge + "', '" + view.PAPResult +
                            "', '" + view.HPVResult + "', '" + view.HPVTesting + "', '" + view.Endocervical + "', 'HPVStandingOrderRule" + cnt.ToString() + "')";
                        sw.WriteLine(insertString + ";");
                    }
                }
            }
            MessageBox.Show("Done");
        }
    }
}
