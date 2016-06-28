using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.ComponentModel;
using System.Reflection;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using MongoDB.Driver.Builders;
using MongoDB.Driver.GridFS;

namespace YellowstonePathology.UI.Mongo
{
    public partial class MongoMigrationWindow : Window
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private BackgroundWorker m_TransferTableWorker;
        private BackgroundWorker m_AOTransferWorker;
        private YellowstonePathology.Business.Mongo.Server m_SQLTransferServer;
        private YellowstonePathology.Business.Mongo.DocumentCollectionTracker m_DocumentCollectionTracker;
        private YellowstonePathology.Business.Mongo.TransferCollection m_TransferCollection;
        private string m_StatusMessage;

        public MongoMigrationWindow()
        {
            this.m_SQLTransferServer = new Business.Mongo.TestServer(Business.Mongo.TestServer.SQLTransferDatabasename);
            this.m_TransferCollection = YellowstonePathology.Business.Mongo.Gateway.GetTransferCollection();
            this.m_DocumentCollectionTracker = new Business.Mongo.DocumentCollectionTracker(this.m_TransferCollection, this.m_SQLTransferServer);
            this.m_StatusMessage = "Idle";

            InitializeComponent();
            this.DataContext = this;
        }

        public string StatusMessage
        {
            get { return this.m_StatusMessage; }
        }

        public YellowstonePathology.Business.Mongo.TransferCollection TransferCollection
        {
            get { return this.m_TransferCollection; }
        }

        private void ButtonView_Click(object sender, RoutedEventArgs e)
        {
            if (this.ListViewTransferCollection.SelectedItem != null)
            {
                YellowstonePathology.Business.Mongo.Transfer transfer = (YellowstonePathology.Business.Mongo.Transfer)this.ListViewTransferCollection.SelectedItem;
                TransferWindow transferWindow = new TransferWindow(transfer);
                transferWindow.ShowDialog();
                this.NotifyPropertyChanged("TransferCollection");
            }
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            this.m_DocumentCollectionTracker.SubmitChanges();
            this.Close();
        }

        private void AddTransferDocuments(string assemblyName)
        {
            Assembly assembly = Assembly.LoadFile(assemblyName);
            Type[] types = assembly.GetTypes();

            foreach (Type type in types)
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
                                string collectionName = persistentClass.StorageName.Substring(3);
                                string tableName = persistentClass.StorageName;

                                YellowstonePathology.Business.Mongo.Transfer transfer = YellowstonePathology.Business.Mongo.Transfer.New(tableName, collectionName);
                                transfer.AssemblyQualifiedName = type.AssemblyQualifiedName;
                                transfer.HasBaseClass = persistentClass.HasPersistentBaseClass;

                                if (persistentClass.HasPersistentBaseClass == true)
                                {
                                    transfer.BaseTableName = persistentClass.BaseStorageName;
                                }

                                List<PropertyInfo> propertyList = type.GetProperties().Where(prop => Attribute.IsDefined(prop, typeof(YellowstonePathology.Business.Persistence.PersistentPrimaryKeyProperty))).ToList();
                                transfer.PrimaryKeyName = propertyList[0].Name;

                                if (persistentClass.HasPersistentBaseClass == true) transfer.HasBaseClass = true;
                                this.m_TransferCollection.Add(transfer);
                            }
                        }
                    }
                }
            }

            this.m_DocumentCollectionTracker.SubmitChanges();
            this.NotifyPropertyChanged("TransferCollection");
        }

        private void ButtonAddTransfers_Click(object sender, RoutedEventArgs e)
        {
            this.AddTransferDocuments(@"C:\SVN\LIS\Business\bin\Debug\BusinessObjects.dll");
            this.AddTransferDocuments(@"C:\SVN\LIS\YellowstonePathology.Domain\bin\Debug\YellowstonePathology.Domain.dll");
        }

        private void ButtonDeleteTransfers_Click(object sender, RoutedEventArgs e)
        {
            this.m_TransferCollection.Clear();
            this.m_DocumentCollectionTracker.SubmitChanges();
            this.NotifyPropertyChanged("TransferCollection");
        }


        private void ButtonUpdateObjectIDs_Click(object sender, RoutedEventArgs e)
        {
            if (this.ListViewTransferCollection.SelectedItems != null)
            {
                foreach (YellowstonePathology.Business.Mongo.Transfer transfer in this.ListViewTransferCollection.SelectedItems)
                {
                    transfer.UpdateSQLObjectIDs(100000);
                }
            }
        }

        private void ButtonAddSQLFields_Click(object sender, RoutedEventArgs e)
        {
            if (this.ListViewTransferCollection.SelectedItems != null)
            {
                foreach (YellowstonePathology.Business.Mongo.Transfer transfer in this.ListViewTransferCollection.SelectedItems)
                {
                    transfer.AddSQLObjectIdColumn();
                    transfer.AddSQLTimeStampColumn();
                    transfer.AddTransferDBTSAttribute();
                }
                this.m_DocumentCollectionTracker.SubmitChanges();
            }
        }

        private void ButtonAddSQLDeleteTrigger_Click(object sender, RoutedEventArgs e)
        {
            if (this.ListViewTransferCollection.SelectedItems != null)
            {
                foreach (YellowstonePathology.Business.Mongo.Transfer transfer in this.ListViewTransferCollection.SelectedItems)
                {
                    transfer.AddSQLDeleteTrigger();
                }
            }
        }

        private void ButtonDropSQLDeleteTrigger_Click(object sender, RoutedEventArgs e)
        {
            if (this.ListViewTransferCollection.SelectedItems != null)
            {
                foreach (YellowstonePathology.Business.Mongo.Transfer transfer in this.ListViewTransferCollection.SelectedItems)
                {
                    transfer.DropSQLDeleteTrigger();
                }
            }
        }

        private void ButtonTransfer_Click(object sender, RoutedEventArgs e)
        {
            if (this.ListViewTransferCollection.SelectedItems != null)
            {
                List<YellowstonePathology.Business.Mongo.Transfer> transferList = new List<Business.Mongo.Transfer>();
                foreach (YellowstonePathology.Business.Mongo.Transfer transfer in this.ListViewTransferCollection.SelectedItems)
                {
                    transferList.Add(transfer);
                }

                BackgroundWorker transferTableWorker = new BackgroundWorker();
                transferTableWorker.DoWork += new DoWorkEventHandler(TransferTableWorker_DoWork);
                transferTableWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(TransferTableWorker_RunWorkerCompleted);
                transferTableWorker.RunWorkerAsync(transferList);
            }
        }

        private void ButtonDeleteDocuments_Click(object sender, RoutedEventArgs e)
        {
            if (this.ListViewTransferCollection.SelectedItems != null)
            {
                YellowstonePathology.Business.Mongo.Transfer transfer = (YellowstonePathology.Business.Mongo.Transfer)this.ListViewTransferCollection.SelectedItem;
                transfer.DropMongoCollection();
                transfer.UpdateMongoDocumentCount();
            }
        }

        private void ButtonFixTimestamp_Click(object sender, RoutedEventArgs e)
        {
            if (this.ListViewTransferCollection.SelectedItems != null)
            {
                foreach (YellowstonePathology.Business.Mongo.Transfer transfer in this.ListViewTransferCollection.SelectedItems)
                {
                    transfer.DropTimeStampField();
                    transfer.AddSQLTimeStampColumn();
                    transfer.UpdateTimestampColumnName();
                }
                MessageBox.Show("All done.");
            }
        }

        private void ButtonFixZeroX_Click(object sender, RoutedEventArgs e)
        {
            if (this.ListViewTransferCollection.SelectedItems != null)
            {
                foreach (YellowstonePathology.Business.Mongo.Transfer transfer in this.ListViewTransferCollection.SelectedItems)
                {
                    transfer.FixZeroX();
                    transfer.UpdateZeroXCount();
                }
                MessageBox.Show("All done.");
            }
        }

        private void ButtonSynchronize_Click(object sender, RoutedEventArgs e)
        {
            if (this.ListViewTransferCollection.SelectedItems != null)
            {
                foreach (YellowstonePathology.Business.Mongo.Transfer transfer in this.ListViewTransferCollection.SelectedItems)
                {
                    transfer.UpdateSQLObjectIDs(1000);
                    transfer.Synchronize();
                }

                this.m_DocumentCollectionTracker.SubmitChanges();
                MessageBox.Show("All done.");
            }
        }

        private void ButtonExtendDocuments_Click(object sender, RoutedEventArgs e)
        {
            foreach (YellowstonePathology.Business.Mongo.Transfer transfer in this.ListViewTransferCollection.SelectedItems)
            {
                transfer.ExtendDocuments();
                transfer.UpdateExtendedDocumentCount();
            }
            this.m_DocumentCollectionTracker.SubmitChanges();
            MessageBox.Show("All done.");
        }

        private void ButtonDeleteDerivedObjectId_Click(object sender, RoutedEventArgs e)
        {
            foreach (YellowstonePathology.Business.Mongo.Transfer transfer in this.m_TransferCollection)
            {
                if (transfer.HasBaseClass == true)
                {
                    transfer.DropObjectId();
                }
            }
        }

        private void ButtonBuildIndexes_Click(object sender, RoutedEventArgs e)
        {
            if (this.ListViewTransferCollection.SelectedItems != null)
            {
                foreach (YellowstonePathology.Business.Mongo.Transfer transfer in this.ListViewTransferCollection.SelectedItems)
                {
                    transfer.BuildMongoIndexes();
                }
                MessageBox.Show("All done.");
            }
        }

        private void ButtonWriteDeleteOrphanedRowsSQL_Click(object sender, RoutedEventArgs e)
        {
            List<string> panelSetOrderDerivedList = ClassHelper.GetPanelSetOrderDerivedTableNames();
            foreach (string str in panelSetOrderDerivedList)
            {
                string sql = "delete " + str + " where reportNo in " +
                    "(select psoc.ReportNo " +
                    "from " + str + " psoc " +
                    "left outer join tblPanelSetORder pso on psoc.ReportNo = pso.ReportNo " +
                    "where pso.ReportNo is null)";
                Console.WriteLine(sql);
            }
        }

        private void ButtonDeletePSODerivedCollections_Click(object sender, RoutedEventArgs e)
        {
            List<string> panelSetOrderDerivedList = ClassHelper.GetPanelSetOrderDerivedTableNames();
            YellowstonePathology.Business.Mongo.LocalServer localServer = new Business.Mongo.LocalServer("LocalLIS");
            foreach (string str in panelSetOrderDerivedList)
            {
                MongoCollection collection = localServer.Database.GetCollection<BsonDocument>(str);
                collection.Drop();
            }
        }

        private void MenuItemUpdateSQLNullObjectIDCount_Click(object sender, RoutedEventArgs e)
        {
            foreach (YellowstonePathology.Business.Mongo.Transfer transfer in this.ListViewTransferCollection.SelectedItems)
            {
                transfer.UpdateSQLNullObjectIdCount();
            }
            this.m_DocumentCollectionTracker.SubmitChanges();
            MessageBox.Show("All done.");
        }

        private void MenuItemUpdateSQLNullObjectIDs_Click(object sender, RoutedEventArgs e)
        {
            foreach (YellowstonePathology.Business.Mongo.Transfer transfer in this.ListViewTransferCollection.SelectedItems)
            {
                if (transfer.SQLNullObjectIdCount > 0)
                {
                    transfer.UpdateSQLObjectIDs(10000);
                }
            }
            this.m_DocumentCollectionTracker.SubmitChanges();
            MessageBox.Show("All done.");
        }

        private void MenuItemUpdateSQLRowCount_Click(object sender, RoutedEventArgs e)
        {
            foreach (YellowstonePathology.Business.Mongo.Transfer transfer in this.ListViewTransferCollection.SelectedItems)
            {
                transfer.UpdateSQLRowCount();
            }
            this.m_DocumentCollectionTracker.SubmitChanges();
            MessageBox.Show("All done.");
        }

        private void MenuItemUpdateMongoDocumentCount_Click(object sender, RoutedEventArgs e)
        {
            foreach (YellowstonePathology.Business.Mongo.Transfer transfer in this.ListViewTransferCollection.SelectedItems)
            {
                transfer.UpdateMongoDocumentCount();
            }
            this.m_DocumentCollectionTracker.SubmitChanges();
            MessageBox.Show("All done.");
        }

        private void MenuItemDropMongoCollection_Click(object sender, RoutedEventArgs e)
        {
            foreach (YellowstonePathology.Business.Mongo.Transfer transfer in this.ListViewTransferCollection.SelectedItems)
            {
                transfer.DropMongoCollection();
            }
            this.m_DocumentCollectionTracker.SubmitChanges();
            MessageBox.Show("All done.");
        }

        private void MenuItemTransferMongoCollection_Click(object sender, RoutedEventArgs e)
        {
            List<YellowstonePathology.Business.Mongo.Transfer> transferList = new List<Business.Mongo.Transfer>();
            foreach (YellowstonePathology.Business.Mongo.Transfer transfer in this.ListViewTransferCollection.SelectedItems)
            {
                transferList.Add(transfer);
            }

            this.m_TransferTableWorker = new BackgroundWorker();
            this.m_TransferTableWorker.WorkerReportsProgress = true;
            this.m_TransferTableWorker.DoWork += new DoWorkEventHandler(TransferTableWorker_DoWork);
            this.m_TransferTableWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(TransferTableWorker_RunWorkerCompleted);
            this.m_TransferTableWorker.ProgressChanged += new ProgressChangedEventHandler(TransferTableWorker_ProgressChanged);
            this.m_TransferTableWorker.RunWorkerAsync(transferList);
        }

        private void MenuItemUpdateSQLIndexCount_Click(object sender, RoutedEventArgs e)
        {
            foreach (YellowstonePathology.Business.Mongo.Transfer transfer in this.ListViewTransferCollection.SelectedItems)
            {
                transfer.UpdateSQLIndexCount();
            }
            this.m_DocumentCollectionTracker.SubmitChanges();
            MessageBox.Show("All done.");
        }

        private void MenuItemUpdateMongoIndexCount_Click(object sender, RoutedEventArgs e)
        {
            foreach (YellowstonePathology.Business.Mongo.Transfer transfer in this.ListViewTransferCollection.SelectedItems)
            {
                transfer.UpdateMongoIndexCount();
            }
            this.m_DocumentCollectionTracker.SubmitChanges();
            MessageBox.Show("All done.");
        }

        private void MenuItemCreateMongoIndexes_Click(object sender, RoutedEventArgs e)
        {
            foreach (YellowstonePathology.Business.Mongo.Transfer transfer in this.ListViewTransferCollection.SelectedItems)
            {
                if (transfer.MongoIndexCount <= transfer.SQLIndexCount)
                {
                    transfer.BuildMongoIndexes();
                }
            }
            this.m_DocumentCollectionTracker.SubmitChanges();
            MessageBox.Show("All done.");
        }

        private void MenuItemUpdateExtendedDocumentCount_Click(object sender, RoutedEventArgs e)
        {
            foreach (YellowstonePathology.Business.Mongo.Transfer transfer in this.ListViewTransferCollection.SelectedItems)
            {
                transfer.UpdateExtendedDocumentCount();
            }
            this.m_DocumentCollectionTracker.SubmitChanges();
            MessageBox.Show("All done.");
        }

        private void MenuItemExtendDocuments_Click(object sender, RoutedEventArgs e)
        {
            foreach (YellowstonePathology.Business.Mongo.Transfer transfer in this.ListViewTransferCollection.SelectedItems)
            {
                transfer.ExtendDocuments();
            }
            this.m_DocumentCollectionTracker.SubmitChanges();
            MessageBox.Show("All done.");
        }

        private void TransferTableWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            List<YellowstonePathology.Business.Mongo.Transfer> transferList = (List<YellowstonePathology.Business.Mongo.Transfer>)e.Argument;
            foreach (YellowstonePathology.Business.Mongo.Transfer transfer in transferList)
            {
                transfer.DropMongoCollection();
                transfer.UpdateSQLObjectIDs(1000);
                transfer.SetTransferDBTS();
                transfer.TransferTableToMongo(this.m_TransferTableWorker);
                transfer.UpdateTransferDBTS();
                transfer.UpdateMongoDocumentCount();
                transfer.BuildMongoIndexes();
                transfer.UpdateMongoIndexCount();

                this.m_DocumentCollectionTracker.SubmitChanges();
            }
        }

        private void TransferTableWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.TextBlockStatusMessage.Text = "Transfer of table complete.";
        }

        private void TransferTableWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.TextBlockStatusMessage.Text = e.UserState.ToString();
        }

        private void MenuItemDeleteTransfers_Click(object sender, RoutedEventArgs e)
        {
            if (this.ListViewTransferCollection.SelectedItem != null)
            {
                while (this.ListViewTransferCollection.SelectedItems.Count != 0)
                {
                    YellowstonePathology.Business.Mongo.Transfer transfer = (YellowstonePathology.Business.Mongo.Transfer)this.ListViewTransferCollection.SelectedItems[0];
                    this.m_TransferCollection.Remove(transfer);
                }
                this.m_DocumentCollectionTracker.SubmitChanges();
                MessageBox.Show("Transfers have been removed.");
            }
        }

        private void MenuItemAddTransfers_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Mongo.Gateway.AddAllSQLTables(this.m_TransferCollection);
            this.m_DocumentCollectionTracker.SubmitChanges();
            this.NotifyPropertyChanged("TransferCollection");
        }

        private void MenuItemUpdateHasObjectIDColumn_Click(object sender, RoutedEventArgs e)
        {
            foreach (YellowstonePathology.Business.Mongo.Transfer transfer in this.ListViewTransferCollection.SelectedItems)
            {
                transfer.UpdateHasSQLObjectId();
            }
            this.m_DocumentCollectionTracker.SubmitChanges();
            MessageBox.Show("All done.");
        }

        private void MenuItemAddObjectIdColumn_Click(object sender, RoutedEventArgs e)
        {
            foreach (YellowstonePathology.Business.Mongo.Transfer transfer in this.ListViewTransferCollection.SelectedItems)
            {
                transfer.AddSQLObjectIdColumn();
            }
            this.m_DocumentCollectionTracker.SubmitChanges();
            MessageBox.Show("All done.");
        }

        private void MenuItemUpdateHasTimestampColumn_Click(object sender, RoutedEventArgs e)
        {
            foreach (YellowstonePathology.Business.Mongo.Transfer transfer in this.ListViewTransferCollection.SelectedItems)
            {
                transfer.UpdateHasSQLTimestamp();
            }
            this.m_DocumentCollectionTracker.SubmitChanges();
            MessageBox.Show("All done.");
        }

        private void MenuItemAddTimestampColumn_Click(object sender, RoutedEventArgs e)
        {
            foreach (YellowstonePathology.Business.Mongo.Transfer transfer in this.ListViewTransferCollection.SelectedItems)
            {
                transfer.AddSQLTimeStampColumn();
            }
            this.m_DocumentCollectionTracker.SubmitChanges();
            MessageBox.Show("All done.");
        }

        private void MenuItemUpdateTransferDBTSAttribute_Click(object sender, RoutedEventArgs e)
        {
            foreach (YellowstonePathology.Business.Mongo.Transfer transfer in this.ListViewTransferCollection.SelectedItems)
            {
                transfer.UpdateTransferDBTSAttribute();
            }
            this.m_DocumentCollectionTracker.SubmitChanges();
            MessageBox.Show("All done.");
        }

        private void MenuItemAddTransferDBTSAttribute_Click(object sender, RoutedEventArgs e)
        {
            foreach (YellowstonePathology.Business.Mongo.Transfer transfer in this.ListViewTransferCollection.SelectedItems)
            {
                transfer.AddTransferDBTSAttribute();
            }
            this.m_DocumentCollectionTracker.SubmitChanges();
            MessageBox.Show("All done.");
        }

        private void MenuItemBuildAccessionOrderDocuments_Click(object sender, RoutedEventArgs e)
        {
            MongoDatabase lis = YellowstonePathology.Business.Mongo.MongoTestServer.Instance.LIS;
            lis.DropCollection("AccessionOrder");

            YellowstonePathology.Business.Mongo.AOTransferBuilder aoTransferBuilder = new Business.Mongo.AOTransferBuilder();
            aoTransferBuilder.Build();

            MessageBox.Show("All Done.");
        }

        private void MenuItemBuildAccessionOrderObject_Click(object sender, RoutedEventArgs e)
        {
            //YellowstonePathology.Business.Test.AccessionOrder accessionOrder = YellowstonePathology.Business.Persistence.DocumentGateway.Instance.PullAccessionOrder("14-24");
        }

        private void MenuItemBuildPanelSetCollection_Click(object sender, RoutedEventArgs e)
        {
            MongoDatabase mongoDatabase = YellowstonePathology.Business.Mongo.MongoTestServer.Instance.LIS;
            mongoDatabase.DropCollection("PanelSet");

            MongoCollection mongoPanelSetCollection = mongoDatabase.GetCollection<BsonDocument>("PanelSet");

            YellowstonePathology.Business.PanelSet.Model.PanelSetCollection psCollection = YellowstonePathology.Business.PanelSet.Model.PanelSetCollection.GetAll();
            foreach (YellowstonePathology.Business.PanelSet.Model.PanelSet panelSet in psCollection)
            {
                panelSet.ObjectId = BsonObjectId.GenerateNewId().ToString();
                YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder = YellowstonePathology.Business.Test.PanelSetOrderFactory.CreatePanelSetOrder(panelSet);
                YellowstonePathology.Business.Persistence.PersistentClass persistentClassAttribute = (YellowstonePathology.Business.Persistence.PersistentClass)panelSetOrder.GetType().GetCustomAttributes(typeof(YellowstonePathology.Business.Persistence.PersistentClass), false).Single();
                string assemblyQualifiedName = panelSetOrder.GetType().AssemblyQualifiedName;
                panelSet.PanelSetOrderClassName = assemblyQualifiedName;
                panelSet.PanelSetOrderTableName = persistentClassAttribute.StorageName;

                BsonDocument bsonDocument = YellowstonePathology.Business.Mongo.BSONBuilder.Build(panelSet);
                mongoPanelSetCollection.Insert(bsonDocument);
            }
        }

        private void MenuItemUpdateOutOfSyncCount_Click(object sender, RoutedEventArgs e)
        {
            foreach (YellowstonePathology.Business.Mongo.Transfer transfer in this.ListViewTransferCollection.SelectedItems)
            {
                transfer.UpdateOutOfSyncCount();
            }
            this.m_DocumentCollectionTracker.SubmitChanges();
            MessageBox.Show("All done.");
        }

        private void MenuItemSetDBTSTransferAttribute_Click(object sender, RoutedEventArgs e)
        {
            foreach (YellowstonePathology.Business.Mongo.Transfer transfer in this.ListViewTransferCollection.SelectedItems)
            {
                transfer.AddTransferDBTSAttribute();
            }
            this.m_DocumentCollectionTracker.SubmitChanges();
            MessageBox.Show("All done.");
        }

        private void MenuItemUpdateDBTSTransferAttribute_Click(object sender, RoutedEventArgs e)
        {
            foreach (YellowstonePathology.Business.Mongo.Transfer transfer in this.ListViewTransferCollection.SelectedItems)
            {
                transfer.UpdateTransferDBTSAttribute();
            }
            this.m_DocumentCollectionTracker.SubmitChanges();
            MessageBox.Show("All done.");
        }

        private void MenuItemUpdateHasDBTSTransferAttribute_Click(object sender, RoutedEventArgs e)
        {
            foreach (YellowstonePathology.Business.Mongo.Transfer transfer in this.ListViewTransferCollection.SelectedItems)
            {
                transfer.UpdateTransferDBTSAttribute();
            }
            this.m_DocumentCollectionTracker.SubmitChanges();
            MessageBox.Show("All done.");
        }

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        private void MenuItemSetTransferStraightAttributeTrue_Click(object sender, RoutedEventArgs e)
        {
            foreach (YellowstonePathology.Business.Mongo.Transfer transfer in this.ListViewTransferCollection.SelectedItems)
            {
                transfer.SetTransferStraightAccrossAttribute(true);
            }
            this.m_DocumentCollectionTracker.SubmitChanges();
            MessageBox.Show("All done.");
        }

        private void MenuItemSetTransferStraightAttributeFalse_Click(object sender, RoutedEventArgs e)
        {
            foreach (YellowstonePathology.Business.Mongo.Transfer transfer in this.ListViewTransferCollection.SelectedItems)
            {
                transfer.SetTransferStraightAccrossAttribute(false);
            }
            this.m_DocumentCollectionTracker.SubmitChanges();
            MessageBox.Show("All done.");
        }

        private void MenuItemUpdateTransferStraightAttribute_Click(object sender, RoutedEventArgs e)
        {
            foreach (YellowstonePathology.Business.Mongo.Transfer transfer in this.ListViewTransferCollection.SelectedItems)
            {
                transfer.UpdateTransferStraightAccrossAttribute();
            }
            this.m_DocumentCollectionTracker.SubmitChanges();
            MessageBox.Show("All done.");
        }

        private void MenuItemSynchronize_Click(object sender, RoutedEventArgs e)
        {
            if (this.ListViewTransferCollection.SelectedItems != null)
            {
                foreach (YellowstonePathology.Business.Mongo.Transfer transfer in this.ListViewTransferCollection.SelectedItems)
                {
                    transfer.UpdateSQLObjectIDs(1000);
                    transfer.Synchronize();
                    transfer.UpdateMongoDocumentCount();
                }
                this.m_DocumentCollectionTracker.SubmitChanges();
                MessageBox.Show("All done.");
            }
        }

        private void MenuItemBuildClientDocuments_Click(object sender, RoutedEventArgs e)
        {
            MongoDatabase lis = YellowstonePathology.Business.Mongo.MongoTestServer.Instance.LIS;
            lis.DropCollection("Client");

            YellowstonePathology.Business.Mongo.ClientTransferBuilder clientTransferBuilder = new Business.Mongo.ClientTransferBuilder();
            clientTransferBuilder.Build();
        }

        private void MenuItemStartAOTransfer_Click(object sender, RoutedEventArgs e)
        {
            List<YellowstonePathology.Business.Mongo.Transfer> transferList = new List<Business.Mongo.Transfer>();
            foreach (YellowstonePathology.Business.Mongo.Transfer transfer in this.ListViewTransferCollection.SelectedItems)
            {
                transferList.Add(transfer);
            }

            this.m_AOTransferWorker = new BackgroundWorker();
            this.m_AOTransferWorker.WorkerReportsProgress = true;
            this.m_AOTransferWorker.WorkerSupportsCancellation = true;
            this.m_AOTransferWorker.DoWork += new DoWorkEventHandler(AOTransferWorker_DoWork);
            this.m_AOTransferWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(AOTransferWorker_RunWorkerCompleted);
            this.m_AOTransferWorker.ProgressChanged += new ProgressChangedEventHandler(AOTransferWorker_ProgressChanged);            
            this.m_AOTransferWorker.RunWorkerAsync(transferList);
        }

        private void AOTransferWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            YellowstonePathology.Business.Mongo.Server server = new Business.Mongo.TestServer(YellowstonePathology.Business.Mongo.MongoTestServer.LISDatabaseName);
            MongoCollection mongoCollection = server.Database.GetCollection<BsonDocument>("AccessionOrderCollection");

            DateTime lastTransferredDate = YellowstonePathology.Business.Gateway.AccessionOrderGatewayMongo.GetLastDateTransferred();
            DateTime currentTransferDate = lastTransferredDate.AddDays(1);            

            while (true)
            {
                List<Business.MasterAccessionNo> masterAccessionNos = Business.Gateway.AccessionOrderGateway.GetMasterAccessionNoList(currentTransferDate);                
                foreach (Business.MasterAccessionNo masterAccessionNo in masterAccessionNos)
                {
                    Business.Test.AccessionOrder ao = YellowstonePathology.Business.Persistence.DocumentGateway.Instance.PullAccessionOrder(masterAccessionNo.Value, Window.GetWindow(this));

                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    System.IO.StringWriter sw = new System.IO.StringWriter(sb);                        

                    YellowstonePathology.Business.Persistence.JSONObjectStreamWriter.Write(sw, ao);
                    sw.Flush();
                    sw.Close();

                    BsonDocument bsonDocument = BsonDocument.Parse(sb.ToString());
                    mongoCollection.Update(Query.EQ("MasterAccessionNo", ao.MasterAccessionNo), Update.Replace(bsonDocument), UpdateFlags.Upsert);
                    
                    this.m_AOTransferWorker.ReportProgress(0, currentTransferDate.ToShortDateString() + " - " + masterAccessionNo.Value);
                }
                
                YellowstonePathology.Business.Gateway.AccessionOrderGatewayMongo.InsertLastDateTransferrred(currentTransferDate);
                currentTransferDate = currentTransferDate.AddDays(1);

                if(this.m_AOTransferWorker.CancellationPending == true)
                {
                    break;
                }
                if (currentTransferDate > DateTime.Today)
                {
                    break;
                }                
            }            
        }

        private void AOTransferWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.TextBlockStatusMessage.Text = "AO Transfer Stopped.";
        }

        private void AOTransferWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.TextBlockStatusMessage.Text = e.UserState.ToString();
        }

        private void MenuItemStopAOTransfer_Click(object sender, RoutedEventArgs e)
        {
            this.m_AOTransferWorker.CancelAsync();
        }
    }
}
