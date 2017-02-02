using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Reflection;

namespace YellowstonePathology.MySQLMigration
{
    public class NonpersistentTableDef : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected string m_TableName;
        protected string m_SelectStatement;
        protected string m_InsertColumnsStatement;
        private string m_KeyField;
        private bool m_HasTable;
        private bool m_HasAllColumns;
        private int m_SqlServerRowCount;
        private int m_MySqlRowCount;
        protected bool m_IsAutoIncrement;
        protected List<NonpersistentColumnDef> m_ColumnDefinitions;
        protected TableIndexCollection m_TableIndexCollection;
        protected TableForeignKeyCollection m_TableForeignKeyCollection;
        protected List<string> m_ModifiedKeys;
        protected List<string> m_CreatedKeys;
        protected List<string> m_DeletedKeys;

        public NonpersistentTableDef()
        {
            this.m_ColumnDefinitions = new List<NonpersistentColumnDef>();
            this.m_TableIndexCollection = new TableIndexCollection();
            m_TableForeignKeyCollection = new TableForeignKeyCollection();
            this.m_ModifiedKeys = new List<string>();
            this.m_CreatedKeys = new List<string>();
            this.m_DeletedKeys = new List<string>();
        }

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        public string TableName
        {
            get { return this.m_TableName; }
        }

        public string SelectStatement
        {
            get { return this.m_SelectStatement; }
        }

        public string InsertColumnsStatement
        {
            get { return this.m_InsertColumnsStatement; }
        }

        public string KeyField
        {
            get { return this.m_KeyField; }
        }

        public List<NonpersistentColumnDef> ColumnDefinitions
        {
            get { return this.m_ColumnDefinitions; }
        }

        public bool HasTable
        {
            get { return this.m_HasTable; }
            set
            {
                this.m_HasTable = value;
                NotifyPropertyChanged("HasTable");
            }
        }

        public bool HasAllColumns
        {
            get { return this.m_HasAllColumns; }
            set
            {
                this.m_HasAllColumns = value;
                NotifyPropertyChanged("HasAllColumns");
            }
        }

        public int SqlServerRowCount
        {
            get { return this.m_SqlServerRowCount; }
            set
            {
                this.m_SqlServerRowCount = value;
                NotifyPropertyChanged("SqlServerRowCount");
            }
        }

        public int MySqlRowCount
        {
            get { return this.m_MySqlRowCount; }
            set
            {
                this.m_MySqlRowCount = value;
                NotifyPropertyChanged("MySqlRowCount");
            }
        }

        public int ModifiedCount
        {
            get { return this.m_ModifiedKeys.Count; }
        }

        public int CreatedCount
        {
            get { return this.m_CreatedKeys.Count; }
        }

        public int DeletedCount
        {
            get { return this.m_DeletedKeys.Count; }
        }

        public bool IsAutoIncrement
        {
            get { return this.m_IsAutoIncrement; }
            set
            {
                this.m_IsAutoIncrement = value;
                NotifyPropertyChanged("IsAutoIncrement");
            }
        }

        public TableIndexCollection TableIndexCollection
        {
            get { return this.m_TableIndexCollection; }
            set
            {
                this.m_TableIndexCollection = value;
                NotifyPropertyChanged("TableIndexCollection");
            }
        }

        public TableForeignKeyCollection TableForeignKeyCollection
        {
            get { return this.m_TableForeignKeyCollection; }
            set
            {
                this.m_TableForeignKeyCollection = value;
                NotifyPropertyChanged("TableForeignKeyCollection");
            }
        }

        public List<string> ModifiedKeys
        {
            get { return this.m_ModifiedKeys; }
            set
            {
                this.m_ModifiedKeys = value;
                NotifyPropertyChanged("ModifiedKeys");
                NotifyPropertyChanged("ModifiedCount");
            }
        }

        public List<string> CreatedKeys
        {
            get { return this.m_CreatedKeys; }
            set
            {
                this.m_CreatedKeys = value;
                NotifyPropertyChanged("CreatedKeys");
                NotifyPropertyChanged("CreatedCount");
            }
        }

        public List<string> DeletedKeys
        {
            get { return this.m_DeletedKeys; }
            set
            {
                this.m_DeletedKeys = value;
                NotifyPropertyChanged("DeletedKeys");
                NotifyPropertyChanged("DeletedCount");
            }
        }

        protected void SetKeyField(string keyField)
        {
            this.m_KeyField = keyField;
        }

        protected void SetSelectStatement()
        {
            StringBuilder result = new StringBuilder();
            result.Append("Select ");
            foreach(NonpersistentColumnDef columnDef in this.m_ColumnDefinitions)
            {
                result.Append("[");
                result.Append(columnDef.ColumnName);
                result.Append("], ");
            }
            result.Remove(result.Length - 2, 2);

            result.Append(" from ");
            result.Append(this.m_TableName);

            this.m_SelectStatement = result.ToString();
        }

        protected void SetInsertColumnsStatement()
        {
            StringBuilder result = new StringBuilder();
            result.Append("Insert into ");
            result.Append(this.m_TableName);
            result.Append("(");
            foreach (NonpersistentColumnDef columnDef in this.m_ColumnDefinitions)
            {
                result.Append(columnDef.ColumnName);
                result.Append(", ");
            }
            result.Remove(result.Length - 2, 2);
            result.Append(") Values (");

            this.m_InsertColumnsStatement = result.ToString();
        }

        public string GetCreateTableCommand()
        {
            StringBuilder result = new StringBuilder();
            result.Append("Create Table If Not Exists ");
            result.Append(this.TableName);
            result.Append("(");
            foreach(NonpersistentColumnDef columnDef in this.m_ColumnDefinitions)
            {
                result.Append(columnDef.ColumnDefinition);
                result.Append(", ");
            }
            result.Remove(result.Length - 2, 2);
            result.Append(");");

            return result.ToString();
        }

        public string GetCreateAutoIncrementOnKeyFieldStatement()
        {
            StringBuilder result = new StringBuilder();
            if (this.m_IsAutoIncrement == true)
            {
                result.Append("ALTER TABLE ");
                result.Append(this.m_TableName);
                result.Append(" MODIFY `");
                result.Append(this.m_KeyField);
                result.Append("` int(11) NOT NULL AUTO_INCREMENT; ");
            }

            return result.ToString();
        }

        public static NonpersistentTableDef FromMigrationStatus(MigrationStatus migrationStatus)
        {
            NonpersistentTableDef result = new NonpersistentTableDef();
            result.m_TableName = migrationStatus.TableName;
            result.m_KeyField = migrationStatus.KeyFieldName;
            {
                foreach(PropertyInfo property in migrationStatus.PersistentProperties)
                {
                    Attribute attribute = property.GetCustomAttribute(typeof(YellowstonePathology.Business.Persistence.PersistentDataColumnProperty));
                    if (attribute != null)
                    {
                        YellowstonePathology.Business.Persistence.PersistentDataColumnProperty persistentDataColumnProperty = (Business.Persistence.PersistentDataColumnProperty)attribute;
                        NonpersistentColumnDef columnDef = new NonpersistentColumnDef(property.Name, persistentDataColumnProperty.DataType, persistentDataColumnProperty.ColumnLength, persistentDataColumnProperty.DefaultValue, persistentDataColumnProperty.IsNullable);
                        result.ColumnDefinitions.Add(columnDef);
                    }
                }
            }
            result.SetSelectStatement();
            result.SetInsertColumnsStatement();
            return result;
        }
    }
}
