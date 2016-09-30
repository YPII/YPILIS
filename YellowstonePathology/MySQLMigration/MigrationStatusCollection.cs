using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Reflection;

namespace YellowstonePathology.MySQLMigration
{
    public class MigrationStatusCollection : ObservableCollection<MigrationStatus>
    {
        public MigrationStatusCollection() { }

        public static MigrationStatusCollection GetAll()
        {
            MigrationStatusCollection result = new MigrationStatusCollection();

            List<Type> types = new List<Type>();
            string assemblyName = @"C:\GIT\William\YPILIS\YellowstonePathology\bin\Debug\UserInterface.exe";
            //string assemblyName = @"C:\GIT\Sid\YPILIS\YellowstonePathology\bin\Debug\UserInterface.exe";
            Assembly assembly = Assembly.LoadFile(assemblyName);
            Type[] persistentTypes = assembly.GetTypes();

            foreach (Type type in persistentTypes)
            {
                object[] customAttributes = type.GetCustomAttributes(typeof(YellowstonePathology.Business.Persistence.PersistentClass), false);
                if (customAttributes.Length > 0)
                {
                    foreach (object o in customAttributes)
                    {
                        if (o is YellowstonePathology.Business.Persistence.PersistentClass)
                        {
                            YellowstonePathology.Business.Persistence.PersistentClass persistentClass = (YellowstonePathology.Business.Persistence.PersistentClass)o;
                            if (string.IsNullOrEmpty(persistentClass.StorageName) == false)
                            {
                                MigrationStatus migrationStatus = new MigrationStatus(type);
                                result.Add(migrationStatus);
                            }
                        }
                    }
                }
            }
            result = Sort(result);
            return result;
        }

        private static MigrationStatusCollection Sort(MigrationStatusCollection migrationStatusCollection)
        {
            MigrationStatusCollection result = new MigrationStatusCollection();
            IOrderedEnumerable<MigrationStatus> orderedResult = migrationStatusCollection.OrderBy(i => i.Name);
            foreach (MigrationStatus migrationStatus in orderedResult)
            {
                result.Add(migrationStatus);
            }
            return result;
        }
    }
}
