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

namespace YellowstonePathology.UI.MySql
{
    /// <summary>
    /// Interaction logic for MySqlMigrationDialog.xaml
    /// </summary>
    public partial class MySqlMigrationDialog : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private MySQLMigration.MigrationStatusCollection m_MigrationStatusCollection;
        private YellowstonePathology.MySQLMigration.MySQLDatabaseBuilder m_MySQLDatabaseBuilder;
        private string m_NumberOfTimesToQuery;
        private string m_NumberOfItemsInQuery;
        private string m_StatusMessage;

        public MySqlMigrationDialog()
        {
            m_MySQLDatabaseBuilder = new MySQLMigration.MySQLDatabaseBuilder();
            m_MigrationStatusCollection = MySQLMigration.MigrationStatusCollection.GetAll();
            this.m_NumberOfItemsInQuery = "500";
            this.m_NumberOfTimesToQuery = "10";
            this.m_StatusMessage = "Idle";

            InitializeComponent();

            DataContext = this;

            Loaded += MySqlMigrationDialog_Loaded;
        }

        private void MySqlMigrationDialog_Loaded(object sender, RoutedEventArgs e)
        {
            //foreach(MySQLMigration.MigrationStatus migrationStatus in this.m_MigrationStatusCollection)
            //{
            //    this.m_MySQLDatabaseBuilder.GetStatus(migrationStatus);
            //}
        }

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        public MySQLMigration.MigrationStatusCollection MigrationStatusCollection
        {
            get { return this.m_MigrationStatusCollection; }
        }

        public string NumberOfItemsInQuery
        {
            get { return this.m_NumberOfItemsInQuery; }
            set { this.m_NumberOfItemsInQuery = value; }
        }

        public string NumberOfTimesToQuery
        {
            get { return this.m_NumberOfTimesToQuery; }
            set { this.m_NumberOfTimesToQuery = value; }
        }

        public string StatusMessage
        {
            get { return this.m_StatusMessage; }
            set
            {
                this.m_StatusMessage = value;
                this.NotifyPropertyChanged("StatusMessage");
            }
        }

        private void SetStatusMessage(Business.Rules.MethodResult methodResult)
        {
            if(methodResult.Success == true)
            {
                this.StatusMessage = "Success";
            }
            else
            {
                this.StatusMessage = methodResult.Message;
            }
        }

        private void MenuItemGetStatus_Click(object sender, RoutedEventArgs e)
        {
            if (this.ListViewMigrationStatus.SelectedItems.Count > 0)
            {
                this.StatusMessage = "Working on it.";
                foreach (MySQLMigration.MigrationStatus migrationStatus in this.ListViewMigrationStatus.SelectedItems)
                {
                    this.GetStatus(migrationStatus);
                }
            }
            else
            {
                MessageBox.Show("Select a class.");
            }
        }

        private void MenuItemCreateTable_Click(object sender, RoutedEventArgs e)
        {
            if(this.ListViewMigrationStatus.SelectedItems.Count > 0)
            {
                this.StatusMessage = "Working on it.";
                foreach (MySQLMigration.MigrationStatus migrationStatus in this.ListViewMigrationStatus.SelectedItems)
                {
                    Business.Rules.MethodResult methodResult = m_MySQLDatabaseBuilder.BuildTable(migrationStatus);
                    this.SetStatusMessage(methodResult);
                    this.GetStatus(migrationStatus);
                }
            }
            else
            {
                MessageBox.Show("Select a class.");
            }
        }

        private void MenuItemAddTransferColumn_Click(object sender, RoutedEventArgs e)
        {
            this.StatusMessage = "Working on it.";
            if (this.ListViewMigrationStatus.SelectedItems.Count > 0)
            {
                foreach (MySQLMigration.MigrationStatus migrationStatus in this.ListViewMigrationStatus.SelectedItems)
                {
                    Business.Rules.MethodResult methodResult = m_MySQLDatabaseBuilder.AddTransferColumn(migrationStatus.TableName);
                    this.SetStatusMessage(methodResult);
                }
            }
            else
            {
                MessageBox.Show("Select a class.");
            }
        }

        private void MenuItemAddTimestampColumn_Click(object sender, RoutedEventArgs e)
        {
            this.StatusMessage = "Working on it.";
            if (this.ListViewMigrationStatus.SelectedItems.Count > 0)
            {
                foreach (MySQLMigration.MigrationStatus migrationStatus in this.ListViewMigrationStatus.SelectedItems)
                {
                    if(migrationStatus.HasTimestampColumn == false)
                    {
                        Business.Mongo.Gateway.AddSQLTimestampColumn(migrationStatus.TableName);
                        Business.Mongo.Gateway.AddTransferDBTSAttribute(migrationStatus.TableName);
                        Business.Mongo.Gateway.AddTransferStraightAcrossAttribute(migrationStatus.TableName, false);
                    }
                    this.StatusMessage = "Timestamp Column added.";
                }
            }
            else
            {
                MessageBox.Show("Select a class.");
            }
        }

        private void MenuItemAddDBTS_Click(object sender, RoutedEventArgs e)
        {
            this.StatusMessage = "Working on it.";
            if (this.ListViewMigrationStatus.SelectedItems.Count > 0)
            {
                foreach (MySQLMigration.MigrationStatus migrationStatus in this.ListViewMigrationStatus.SelectedItems)
                {
                    Business.Rules.MethodResult methodResult = m_MySQLDatabaseBuilder.AddDBTS(migrationStatus.TableName);
                    this.SetStatusMessage(methodResult);
                }
            }
            else
            {
                MessageBox.Show("Select a class.");
            }
        }

        private void MenuItemLoadData_Click(object sender, RoutedEventArgs e)
        {
            this.StatusMessage = "Working on it.";
            if (this.ListViewMigrationStatus.SelectedItems.Count > 0)
            {
                int countToMove = 0;
                bool result = int.TryParse(this.m_NumberOfItemsInQuery, out countToMove);
                if (result == false)
                {
                    MessageBox.Show("Enter the number or items in each query.");
                    return;
                }

                int numberofReps = 0;
                result = int.TryParse(this.m_NumberOfTimesToQuery, out numberofReps);
                if (result == false)
                {
                    MessageBox.Show("Enter the number or times to run the query.");
                    return;
                }

                foreach (MySQLMigration.MigrationStatus migrationStatus in this.ListViewMigrationStatus.SelectedItems)
                {
                    Business.Rules.MethodResult methodResult = m_MySQLDatabaseBuilder.LoadData(migrationStatus, countToMove, numberofReps);
                    this.SetStatusMessage(methodResult);
                }
            }
            else
            {
                MessageBox.Show("Select a class to Load.");
            }
        }

        private void MenuItemSynchronizeData_Click(object sender, RoutedEventArgs e)
        {
            if (this.ListViewMigrationStatus.SelectedItems.Count > 0)
            {
                this.StatusMessage = "Working on it.";
                foreach (MySQLMigration.MigrationStatus migrationStatus in this.ListViewMigrationStatus.SelectedItems)
                {
                    Business.Rules.MethodResult methodResult = m_MySQLDatabaseBuilder.Synchronize(migrationStatus);
                    this.SetStatusMessage(methodResult);
                }
            }
            else
            {
                MessageBox.Show("Select a class to Sync.");
            }
        }

        private void GetStatus(MySQLMigration.MigrationStatus migrationStatus)
        {
            this.m_MySQLDatabaseBuilder.GetStatus(migrationStatus);
            this.StatusMessage = "Got Status for " + migrationStatus.Name;
        }
    }
}
