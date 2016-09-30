using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Reflection;

namespace YellowstonePathology.MySQLMigration
{
    public class MigrationStatus : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private Type m_Type;
        private PropertyInfo m_KeyFieldProperty;
        private List<PropertyInfo> m_PersistentProperties;
        private string m_TableName;
        private bool m_HasTable;
        private bool m_HasTransferredColumn;
        private bool m_HasTimestampColumn;
        private bool m_HasDBTS;
        private int m_OutOfSyncCount;
        private int m_UnLoadedDataCount;
        private string m_FileName;

        public MigrationStatus(Type type)
        {
            this.m_Type = type;
            this.GetTableName();
            this.GetPrimaryKey();
            this.GetPersistentProperties();
            this.GetFileName();
        }

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        public string Name
        {
            get { return this.m_Type.Name; }
        }

        public string TableName
        {
            get { return this.m_TableName; }
        }

        public string KeyFieldName
        {
            get { return this.m_KeyFieldProperty.Name; }
        }

        public PropertyInfo KeyFieldProperty
        {
            get { return this.m_KeyFieldProperty; }
        }

        public List<PropertyInfo> PersistentProperties
        {
            get { return this.m_PersistentProperties; }
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

        public bool HasTransferredColumn
        {
            get { return this.m_HasTransferredColumn; }
            set
            {
                this.m_HasTransferredColumn = value;
                NotifyPropertyChanged("HasTransferredColumn");
            }
        }

        public bool HasTimestampColumn
        {
            get { return this.m_HasTimestampColumn; }
            set
            {
                this.m_HasTimestampColumn = value;
                NotifyPropertyChanged("HasTimestampColumn");
            }
        }

        public bool HasDBTS
        {
            get { return this.m_HasDBTS; }
            set
            {
                this.m_HasDBTS = value;
                NotifyPropertyChanged("HasDBTS");
            }
        }

        public int OutOfSyncCount
        {
            get { return this.m_OutOfSyncCount; }
            set
            {
                this.m_OutOfSyncCount = value;
                NotifyPropertyChanged("OutOfSyncCount");
            }
        }
        public int UnLoadedDataCount
        {
            get { return this.m_UnLoadedDataCount; }
            set
            {
                this.m_UnLoadedDataCount = value;
                NotifyPropertyChanged("UnLoadedDataCount");
            }
        }

        public string FileName
        {
            get { return this.m_FileName; }
        }
        private void GetTableName()
        {
            //string typeName = this.m_Type.Name;
            //if (typeName == "FlowMarkerItem") typeName = "FlowMarkers";
            //typeName = typeName.Replace("_Base", "");
            //typeName = typeName.Replace("Item", "");
            //this.m_TableName = "tbl" + typeName;


            object[] customAttributes = this.m_Type.GetCustomAttributes(typeof(YellowstonePathology.Business.Persistence.PersistentClass), false);
            if (customAttributes.Length > 0)
            {
                foreach (object o in customAttributes)
                {
                    if (o is YellowstonePathology.Business.Persistence.PersistentClass)
                    {
                        YellowstonePathology.Business.Persistence.PersistentClass persistentClass = (YellowstonePathology.Business.Persistence.PersistentClass)o;
                        if (string.IsNullOrEmpty(persistentClass.StorageName) == false)
                        {
                            this.m_TableName = persistentClass.StorageName;
                        }
                    }
                }
            }

        }

        private void GetPrimaryKey()
        {
            PropertyInfo[] primaryKeyProperties = this.m_Type.GetProperties().
                Where(prop => Attribute.IsDefined(prop, typeof(Business.Persistence.PersistentPrimaryKeyProperty))).ToArray();

            if (primaryKeyProperties.Length > 0)
            {
                this.m_KeyFieldProperty = primaryKeyProperties[0];
            }
        }

        private void GetPersistentProperties()
        {
            this.m_PersistentProperties = new List<PropertyInfo>();
            PropertyInfo[] baseProperties;
            PropertyInfo[] classProperties;
            Type baseType = this.m_Type.BaseType;

            baseProperties = baseType.GetProperties().Where(prop => Attribute.IsDefined(prop, typeof(Business.Persistence.PersistentProperty))).ToArray();
            classProperties = this.m_Type.GetProperties().Where(prop => Attribute.IsDefined(prop, typeof(Business.Persistence.PersistentProperty))).ToArray();

            foreach (PropertyInfo property in classProperties)
            {
                bool matched = false;
                foreach (PropertyInfo baseProperty in baseProperties)
                {
                    if (property.Name == baseProperty.Name)
                    {
                        matched = true;
                        break;
                    }
                }
                if (matched == false)
                {
                    this.m_PersistentProperties.Add(property);
                }
            }

            if (this.m_PersistentProperties.Count > 0)
            {
                if (this.m_PersistentProperties.Contains(this.m_KeyFieldProperty) == true)
                {
                    this.m_PersistentProperties.Remove(this.m_KeyFieldProperty);
                }
                this.m_PersistentProperties.Insert(0, this.m_KeyFieldProperty);

                /*if (this.HasObjectId() == false)
                {
                    this.GetObjectIdProperty();
                }*/
            }
        }

        private bool HasObjectId()
        {
            bool result = false;
            foreach (PropertyInfo property in this.m_PersistentProperties)
            {
                if (property.Name == "ObjectId")
                {
                    result = true;
                    break;
                }
            }
            return result;
        }
        private void GetObjectIdProperty()
        {
            PropertyInfo[] properties = this.m_Type.GetProperties().Where(prop => Attribute.IsDefined(prop, typeof(Business.Persistence.PersistentDocumentIdProperty))).ToArray();
            if (properties.Length > 0) this.m_PersistentProperties.Add(properties[0]);
        }

        private void GetFileName()
        {
            string name = this.m_Type.AssemblyQualifiedName;
            int idx = name.IndexOf(",");
            name = name.Substring(0, idx);
            name = name.Replace('.', '\\');
            name = name.Replace("\\Model", ".Model");
            name = name + ".cs";
            this.m_FileName = name;
        }
    }
}
