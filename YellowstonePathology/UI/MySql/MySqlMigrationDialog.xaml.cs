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
        private bool m_HaveDBSelection;
        private string m_DBIndicator;

        public MySqlMigrationDialog()
        {
            m_MigrationStatusCollection = new MySQLMigration.MigrationStatusCollection();
            this.m_NumberOfItemsInQuery = "1000";
            this.m_NumberOfTimesToQuery = "15";
            this.m_StatusMessage = "Idle";

            InitializeComponent();

            DataContext = this;
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
            private set
            {
                this.m_MigrationStatusCollection = value;
                this.NotifyPropertyChanged("MigrationStatusCollection");
                this.HaveDBSelection = true;
            }
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
                this.TextBlockStatusMessage.Text = this.m_StatusMessage;
                this.NotifyPropertyChanged("StatusMessage");
            }
        }

        public bool HaveDBSelection
        {
            get { return this.m_HaveDBSelection; }
            set
            {
                this.m_HaveDBSelection = value;
                this.NotifyPropertyChanged("HaveDBSelection");
            }
        }

        public string DBIndicator
        {
            get { return this.m_DBIndicator; }
            set
            {
                this.m_DBIndicator = value;
                this.NotifyPropertyChanged("DBIndicator");
            }
        }

        private void SetStatusMessage(Business.Rules.MethodResult methodResult)
        {
            if(string.IsNullOrEmpty(methodResult.Message) == true)
            {
                this.StatusMessage = "Success";
            }
            else
            {
                this.StatusMessage = methodResult.Message;
            }
        }

        private void MenuItemSetDBToLIS_Click(object sender, RoutedEventArgs e)
        {
            m_MySQLDatabaseBuilder = new MySQLMigration.MySQLDatabaseBuilder("LIS");
            this.MigrationStatusCollection = MySQLMigration.MigrationStatusCollection.GetAll();
            this.DBIndicator = "LIS";
        }

        private void MenuItemSetDBToTemp_Click(object sender, RoutedEventArgs e)
        {
            m_MySQLDatabaseBuilder = new MySQLMigration.MySQLDatabaseBuilder("temp");
            this.MigrationStatusCollection = MySQLMigration.MigrationStatusCollection.GetAll();
            this.DBIndicator = "temp";
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

        private void MenuItemCompareTables_Click(object sender, RoutedEventArgs e)
        {
            if (this.ListViewMigrationStatus.SelectedItems.Count > 1)
            {
                this.StatusMessage = "Working on it.";
                Business.Rules.MethodResult overallResult = new Business.Rules.MethodResult();
                overallResult.Message = "Tables missing Columns: ";
                foreach (MySQLMigration.MigrationStatus migrationStatus in this.ListViewMigrationStatus.SelectedItems)
                {
                    Business.Rules.MethodResult methodResult = m_MySQLDatabaseBuilder.CompareTable(migrationStatus);
                    if(methodResult.Success == false)
                    {
                        overallResult.Success = false;
                        overallResult.Message += migrationStatus.Name;
                    }
                }

                this.SetStatusMessage(overallResult);
            }
            else if(this.ListViewMigrationStatus.SelectedItems.Count == 1)
            {
                MySQLMigration.MigrationStatus migrationStatus = (MySQLMigration.MigrationStatus)this.ListViewMigrationStatus.SelectedItems[0];
                this.CompareSingleTable(migrationStatus);
            }
            else
            {
                MessageBox.Show("Select a single class to check.");
            }
        }

        private void CompareSingleTable(MySQLMigration.MigrationStatus migrationStatus)
        {
            Business.Rules.MethodResult methodResult = m_MySQLDatabaseBuilder.CompareTable(migrationStatus);
            this.SetStatusMessage(methodResult);
        }

        private void MenuItemAddMissingColumns_Click(object sender, RoutedEventArgs e)
        {
            this.StatusMessage = "Working on it.";
            if (this.ListViewMigrationStatus.SelectedItems.Count > 1)
            {
                Business.Rules.MethodResult overallResult = new Business.Rules.MethodResult();
                overallResult.Message = "Error in: ";
                foreach (MySQLMigration.MigrationStatus migrationStatus in this.ListViewMigrationStatus.SelectedItems)
                {
                    Business.Rules.MethodResult methodResult = m_MySQLDatabaseBuilder.AddMissingColumns(migrationStatus);
                    if(methodResult.Success == false)
                    {
                        overallResult.Message += migrationStatus.Name + ", ";
                    }
                }
                this.SetStatusMessage(overallResult);
            }
            else if(this.ListViewMigrationStatus.SelectedItems.Count == 1)
            {
                MySQLMigration.MigrationStatus migrationStatus = (MySQLMigration.MigrationStatus)this.ListViewMigrationStatus.SelectedItems[0];
                this.AddMissingColumnsToSingleTable(migrationStatus);
            }
            else
            {
                MessageBox.Show("Select a class.");
            }
        }

        private void MenuItemAddIndexes_Click(object sender, RoutedEventArgs e)
        {
            this.StatusMessage = "Working on it.";
            if (this.ListViewMigrationStatus.SelectedItems.Count > 0)
            {
                Business.Rules.MethodResult overallResult = new Business.Rules.MethodResult();

                foreach (MySQLMigration.MigrationStatus migrationStatus in this.ListViewMigrationStatus.SelectedItems)
                {
                    Business.Rules.MethodResult methodResult = m_MySQLDatabaseBuilder.AddMissingIndexes(migrationStatus);
                    if (methodResult.Success == false)
                    {
                        overallResult.Success = false;
                        overallResult.Message += methodResult.Message;
                    }
                }
                this.SetStatusMessage(overallResult);
            }
            else
            {
                MessageBox.Show("Select a class.");
            }
        }

        private void MenuItemAddForeignKeys_Click(object sender, RoutedEventArgs e)
        {
            this.StatusMessage = "Working on it.";
            if (this.ListViewMigrationStatus.SelectedItems.Count > 0)
            {
                Business.Rules.MethodResult overallResult = new Business.Rules.MethodResult();

                foreach (MySQLMigration.MigrationStatus migrationStatus in this.ListViewMigrationStatus.SelectedItems)
                {
                    Business.Rules.MethodResult methodResult = m_MySQLDatabaseBuilder.AddMissingForeignKeys(migrationStatus);
                    if (methodResult.Success == false)
                    {
                        overallResult.Success = false;
                        overallResult.Message += methodResult.Message;
                    }
                }
                this.SetStatusMessage(overallResult);
            }
            else
            {
                MessageBox.Show("Select a class.");
            }
        }

        private void MenuItemDropForeignKeys_Click(object sender, RoutedEventArgs e)
        {
                Business.Rules.MethodResult overallResult = new Business.Rules.MethodResult();

                    Business.Rules.MethodResult methodResult = m_MySQLDatabaseBuilder.DropMySqlForeignKeys();
                    if (methodResult.Success == false)
                    {
                        overallResult.Success = false;
                        overallResult.Message += methodResult.Message;
                    }
                this.SetStatusMessage(overallResult);
        }

        private void AddMissingColumnsToSingleTable(MySQLMigration.MigrationStatus migrationStatus)
        {
            Business.Rules.MethodResult methodResult = m_MySQLDatabaseBuilder.AddMissingColumns(migrationStatus);
            this.SetStatusMessage(methodResult);
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
                        this.m_MySQLDatabaseBuilder.AddSQLTimestampColumn(migrationStatus.TableName);
                        this.m_MySQLDatabaseBuilder.AddTransferDBTSAttribute(migrationStatus.TableName);
                        this.m_MySQLDatabaseBuilder.AddTransferStraightAcrossAttribute(migrationStatus.TableName, false);
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
                    Business.Rules.MethodResult methodResult = m_MySQLDatabaseBuilder.BulkLoadData(migrationStatus, countToMove, numberofReps);
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
                    Business.Rules.MethodResult methodResult = m_MySQLDatabaseBuilder.SynchronizeData(migrationStatus);
                    this.SetStatusMessage(methodResult);
                }
            }
            else
            {
                MessageBox.Show("Select a class to Sync.");
            }
        }

        private void MenuItemDailySync_Click(object sender, RoutedEventArgs e)
        {
            this.StatusMessage = "Working on it.";
            Business.Rules.MethodResult overallResult = new Business.Rules.MethodResult();
            foreach (MySQLMigration.MigrationStatus migrationStatus in this.ListViewMigrationStatus.SelectedItems)
            {
                Business.Rules.MethodResult methodResult = m_MySQLDatabaseBuilder.DailySync(migrationStatus);
                overallResult.Message += methodResult.Message;
                if (methodResult.Success == false)
                {
                    overallResult.Success = false;
                }
            }
            this.SetStatusMessage(overallResult);
        }

        private void MenuItemRemoveUnneededData_Click(object sender, RoutedEventArgs e)
        {
            this.StatusMessage = "Working on it.";
            Business.Rules.MethodResult overallResult = new Business.Rules.MethodResult();
            foreach (MySQLMigration.MigrationStatus migrationStatus in this.ListViewMigrationStatus.SelectedItems)
            {
                this.m_MySQLDatabaseBuilder.RemoveFromMySqlNoLongerInSqlServer(migrationStatus);
            }
            this.SetStatusMessage(overallResult);
        }

        private void MenuItemAddMissingTransferredData_Click(object sender, RoutedEventArgs e)
        {
            this.StatusMessage = "Working on it.";
            Business.Rules.MethodResult overallResult = new Business.Rules.MethodResult();
            foreach (MySQLMigration.MigrationStatus migrationStatus in this.ListViewMigrationStatus.SelectedItems)
            {
                Business.Rules.MethodResult result = this.m_MySQLDatabaseBuilder.InsertMySqlTransferredButMissing(migrationStatus);
                if (result.Success == false)
                {
                    overallResult.Success = false;
                    overallResult.Message += result.Message;
                }
            }
            this.SetStatusMessage(overallResult);
        }

        private void MenuItemCompareData_Click(object sender, RoutedEventArgs e)
        {
            this.StatusMessage = "Working on it.";
            if (this.ListViewMigrationStatus.SelectedItem != null)
            {
                MySQLMigration.MigrationStatus migrationStatus = (MySQLMigration.MigrationStatus)this.ListViewMigrationStatus.SelectedItem;
                Business.Rules.MethodResult methodResult = m_MySQLDatabaseBuilder.CompareTables(migrationStatus);
                this.SetStatusMessage(methodResult);
            }
            else
            {
                MessageBox.Show("Select a class to compare.");
            }
        }

        private void MenuItemNonpersistentTables_Click(object sender, RoutedEventArgs e)
        {
            NonpersistentTableDialog dialog = new NonpersistentTableDialog();
            dialog.ShowDialog();
        }

        private void GetStatus(MySQLMigration.MigrationStatus migrationStatus)
        {
            this.m_MySQLDatabaseBuilder.GetStatus(migrationStatus);
            this.StatusMessage = "Got Status for " + migrationStatus.Name;
        }
    }
}
