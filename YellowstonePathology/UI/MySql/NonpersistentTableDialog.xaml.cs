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
    /// Interaction logic for NonpersistentTableDialog.xaml
    /// </summary>
    public partial class NonpersistentTableDialog : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private MySQLMigration.NonpersistentTableDefCollection m_NonpersistentTableDefCollection;
        private YellowstonePathology.MySQLMigration.MySQLDatabaseBuilder m_MySQLDatabaseBuilder;
        private string m_StatusMessage;
        private bool m_HaveDBSelection;
        private string m_DBIndicator;

        public NonpersistentTableDialog()
        {
            m_NonpersistentTableDefCollection = new MySQLMigration.NonpersistentTableDefCollection();
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

        public MySQLMigration.NonpersistentTableDefCollection NonpersistentTableDefCollection
        {
            get { return this.m_NonpersistentTableDefCollection; }
            private set
            {
                this.m_NonpersistentTableDefCollection = value;
                this.NotifyPropertyChanged("NonpersistentTableDefCollection");
                this.HaveDBSelection = true;
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
            if (string.IsNullOrEmpty(methodResult.Message) == true)
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
            this.NonpersistentTableDefCollection = MySQLMigration.NonpersistentTableDefCollection.GetAll();
            this.DBIndicator = "LIS";
        }

        private void MenuItemSetDBToTemp_Click(object sender, RoutedEventArgs e)
        {
            m_MySQLDatabaseBuilder = new MySQLMigration.MySQLDatabaseBuilder("temp");
            this.NonpersistentTableDefCollection = MySQLMigration.NonpersistentTableDefCollection.GetAll();
            this.DBIndicator = "temp";
        }

        private void MenuItemGetStatus_Click(object sender, RoutedEventArgs e)
        {
            if (this.ListViewNonpersistentTableDef.SelectedItems.Count > 0)
            {
                this.StatusMessage = "Working on it.";
                foreach (MySQLMigration.NonpersistentTableDef nonpersistentTableDef in this.ListViewNonpersistentTableDef.SelectedItems)
                {
                    this.GetStatus(nonpersistentTableDef);
                }
            }
            else
            {
                MessageBox.Show("Select a class.");
            }
        }

        private void MenuItemCreateTable_Click(object sender, RoutedEventArgs e)
        {
            if (this.ListViewNonpersistentTableDef.SelectedItems.Count > 0)
            {
                this.StatusMessage = "Working on it.";
                foreach (MySQLMigration.NonpersistentTableDef nonpersistentTableDef in this.ListViewNonpersistentTableDef.SelectedItems)
                {
                    Business.Rules.MethodResult methodResult = m_MySQLDatabaseBuilder.BuildTable(nonpersistentTableDef);
                    this.GetStatus(nonpersistentTableDef);
                    this.SetStatusMessage(methodResult);
                }
            }
            else
            {
                MessageBox.Show("Select a table.");
            }
        }

        private void MenuItemCompareTables_Click(object sender, RoutedEventArgs e)
        {
            if (this.ListViewNonpersistentTableDef.SelectedItems.Count == 1)
            {
                this.StatusMessage = "Working on it.";
                Business.Rules.MethodResult overallResult = new Business.Rules.MethodResult();
                foreach (MySQLMigration.NonpersistentTableDef nonpersistentTableDef in this.ListViewNonpersistentTableDef.SelectedItems)
                {
                    Business.Rules.MethodResult methodResult = m_MySQLDatabaseBuilder.CompareTable(nonpersistentTableDef);
                    if (methodResult.Success == false)
                    {
                        overallResult.Success = false;
                        overallResult.Message += "Error in " + nonpersistentTableDef.TableName;
                    }
                    this.GetStatus(nonpersistentTableDef);
                }

                this.SetStatusMessage(overallResult);
            }
            else
            {
                MessageBox.Show("Select a single table to check.");
            }
        }

        private void MenuItemAddMissingColumns_Click(object sender, RoutedEventArgs e)
        {
            this.StatusMessage = "Working on it.";
            if (this.ListViewNonpersistentTableDef.SelectedItems.Count > 0)
            {
                Business.Rules.MethodResult overallResult = new Business.Rules.MethodResult();
                foreach (MySQLMigration.NonpersistentTableDef nonpersistentTableDef in this.ListViewNonpersistentTableDef.SelectedItems)
                {
                    Business.Rules.MethodResult methodResult = m_MySQLDatabaseBuilder.AddMissingColumns(nonpersistentTableDef);
                    if (methodResult.Success == false)
                    {
                        overallResult.Message += nonpersistentTableDef.TableName + ", ";
                    }
                    this.GetStatus(nonpersistentTableDef);
                }
                this.SetStatusMessage(overallResult);
            }
            else
            {
                MessageBox.Show("Select a table.");
            }
        }

        private void MenuItemAddIndexes_Click(object sender, RoutedEventArgs e)
        {
            this.StatusMessage = "Working on it.";
            if (this.ListViewNonpersistentTableDef.SelectedItems.Count > 0)
            {
                Business.Rules.MethodResult overallResult = new Business.Rules.MethodResult();

                foreach (MySQLMigration.NonpersistentTableDef nonpersistentTableDef in this.ListViewNonpersistentTableDef.SelectedItems)
                {
                    Business.Rules.MethodResult methodResult = m_MySQLDatabaseBuilder.AddMissingIndexes(nonpersistentTableDef);
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
            if (this.ListViewNonpersistentTableDef.SelectedItems.Count > 0)
            {
                Business.Rules.MethodResult overallResult = new Business.Rules.MethodResult();

                foreach (MySQLMigration.NonpersistentTableDef nonpersistentTableDef in this.ListViewNonpersistentTableDef.SelectedItems)
                {
                    Business.Rules.MethodResult methodResult = m_MySQLDatabaseBuilder.AddMissingForeignKeys(nonpersistentTableDef);
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

        private void MenuItemLoadData_Click(object sender, RoutedEventArgs e)
        {
            this.StatusMessage = "Working on it.";
            if (this.ListViewNonpersistentTableDef.SelectedItems.Count > 0)
            {
                foreach (MySQLMigration.NonpersistentTableDef nonpersistentTableDef in this.ListViewNonpersistentTableDef.SelectedItems)
                {
                    Business.Rules.MethodResult methodResult = m_MySQLDatabaseBuilder.LoadNonpersistentData(nonpersistentTableDef);
                    this.SetStatusMessage(methodResult);
                }
            }
            else
            {
                MessageBox.Show("Select a table to Load.");
            }
        }

        private void MenuItemCreateAutoIncrements_Click(object sender, RoutedEventArgs e)
        {
            this.StatusMessage = "Working on it.";
            Business.Rules.MethodResult overallResult = new Business.Rules.MethodResult();
            foreach (MySQLMigration.NonpersistentTableDef nonpersistentTableDef in this.ListViewNonpersistentTableDef.SelectedItems)
            {
                Business.Rules.MethodResult methodResult = m_MySQLDatabaseBuilder.CreateMySqlAutoIncrement(nonpersistentTableDef);
                if (methodResult.Success == false)
                {
                    overallResult.Success = false;
                    overallResult.Message += nonpersistentTableDef.TableName + " ";
                }
            }
            this.SetStatusMessage(overallResult);
        }

        private void GetStatus(MySQLMigration.NonpersistentTableDef nonpersistentTableDef)
        {
            this.m_MySQLDatabaseBuilder.GetStatus(nonpersistentTableDef);
            this.StatusMessage = "Got Status for " + nonpersistentTableDef.TableName;
        }
    }
}
