using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace YellowstonePathology.UI.Surgical
{
    /// <summary>
    /// Interaction logic for StainStatusDialog.xaml
    /// </summary>
    public partial class StainStatusDialog : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private YellowstonePathology.Business.Test.Model.TestOrderStatusViewCollection m_TestOrderStatusViewCollection;
        private YellowstonePathology.Business.User.SystemUserCollection m_Pathologists;
        private List<string> m_StatusList;
        private int m_PathologistId;
        private DateTime m_OrderDate;
        private string m_Status;

        public StainStatusDialog(int pathologistId)
        {
            this.m_PathologistId = pathologistId;
            this.m_Pathologists = Business.User.SystemUserCollectionInstance.Instance.SystemUserCollection.GetPathologistUsers();
            YellowstonePathology.Business.User.SystemUserCollectionInstance.Instance.SystemUserCollection.AddUnassignedToUserList(this.m_Pathologists, true);
            YellowstonePathology.Business.User.SystemUserCollectionInstance.Instance.SystemUserCollection.AddAllToUserList(this.m_Pathologists, true);
            this.m_Pathologists[0].UserId = -1;
            this.m_OrderDate = DateTime.Today;
            this.m_Status = "ALL";
            this.m_StatusList = new List<string>();
            this.m_StatusList.Add("ALL");
            this.m_StatusList.Add("ORDERED");            
            this.m_StatusList.Add("CUTTING");
            this.m_StatusList.Add("STAINING");
            this.m_StatusList.Add("STAINED");
            this.m_StatusList.Add("PERFORMEDBYHAND");

            InitializeComponent();

            DataContext = this;

            this.Loaded += StainStatusDialog_Loaded;
        }

        private void StainStatusDialog_Loaded(object sender, RoutedEventArgs e)
        {
            this.comboBoxPathologist.SelectionChanged += comboBoxPathologist_SelectionChanged;
            this.comboBoxStatus.SelectionChanged += comboBoxStatus_SelectionChanged;
            this.FillTestOrderStatusViewCollection();
        }

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        public YellowstonePathology.Business.Test.Model.TestOrderStatusViewCollection TestOrderStatusViewCollection
        {
            get { return this.m_TestOrderStatusViewCollection; }
            set
            {
                this.m_TestOrderStatusViewCollection = value;
                this.NotifyPropertyChanged("TestOrderStatusViewCollection");
            }
        }

        public DateTime OrderDate
        {
            get { return this.m_OrderDate; }
            set
            {
                this.m_OrderDate = value;
                this.NotifyPropertyChanged("OrderDate");
            }
        }

        public YellowstonePathology.Business.User.SystemUserCollection Pathologists
        {
            get { return this.m_Pathologists; }
        }
        public int PathologistId
        {
            get { return this.m_PathologistId; }
            set
            {
                this.m_PathologistId = value;
                NotifyPropertyChanged("PathologistId");
            }
        }
        
        public string Status
        {
            get { return this.m_Status; }
            set
            {
                this.m_Status = value;
                NotifyPropertyChanged("Status");
            }
        }

        public List<string> StatusList
        {
            get { return this.m_StatusList; }
            set
            {
                this.m_StatusList = value;
                NotifyPropertyChanged("StatusList");
            }
        }

        private void ButonOk_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ButtonOrderDateBack_Click(object sender, RoutedEventArgs args)
        {
            this.OrderDate = this.m_OrderDate.AddDays(-1);
            this.FillTestOrderStatusViewCollection();
        }

        private void ButtonOrderDateForward_Click(object sender, RoutedEventArgs args)
        {
            this.OrderDate = this.m_OrderDate.AddDays(1);
            this.FillTestOrderStatusViewCollection();
        }

        private void comboBoxStatus_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.FillTestOrderStatusViewCollection();
        }

        private void comboBoxPathologist_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.FillTestOrderStatusViewCollection();
        }

        private void FillTestOrderStatusViewCollection()
        {
            this.TestOrderStatusViewCollection = Business.Gateway.AccessionOrderGateway.GetTestOrderStatusViewCollection(this.m_OrderDate, this.m_PathologistId, this.m_Status);
        }
    }
}
