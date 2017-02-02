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
    /// Interaction logic for SqlServerRefreshDialog.xaml
    /// </summary>
    public partial class SqlServerRefreshDialog : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private MySQLMigration.NonpersistentTableDefCollection m_NonpersistentTableDefCollection;
        private YellowstonePathology.MySQLMigration.SqlServerFromMySqlRefresher m_SqlServerFromMySqlRefresher;
        private string m_StatusMessage;

        public SqlServerRefreshDialog()
        {
            InitializeComponent();
        }

        public MySQLMigration.NonpersistentTableDefCollection NonpersistentTableDefCollection
        {
            get { return this.m_NonpersistentTableDefCollection; }
            private set
            {
                this.m_NonpersistentTableDefCollection = value;
                this.NotifyPropertyChanged("NonpersistentTableDefCollection");
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

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
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

        private void GetStatus(MySQLMigration.NonpersistentTableDef nonpersistentTableDef)
        {
            this.m_SqlServerFromMySqlRefresher.GetStatus(nonpersistentTableDef);
            this.StatusMessage = "Got Status for " + nonpersistentTableDef.TableName;
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

        private void MenuItemRefreshData_Click(object sender, RoutedEventArgs e)
        {
            Business.Rules.MethodResult overallResult = new Business.Rules.MethodResult();
            foreach (MySQLMigration.NonpersistentTableDef nonpersistentTableDef in this.ListViewNonpersistentTableDef.SelectedItems)
            {
                Business.Rules.MethodResult methodResult = m_SqlServerFromMySqlRefresher.Refresh(nonpersistentTableDef);
                if (methodResult.Success == false)
                {
                    overallResult.Success = false;
                    overallResult.Message += nonpersistentTableDef.TableName + " ";
                }
            }
            this.SetStatusMessage(overallResult);
        }
    }
}
