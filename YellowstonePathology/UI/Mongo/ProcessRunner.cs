using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Data;
using System.Data.SqlClient;
using System.ComponentModel;
using System.Reflection;

namespace YellowstonePathology.UI.Mongo
{
    public class ProcessRunner
    {
        //public const string ConnectionString = "Data Source=YPIDBS2;Initial Catalog=YPIData;Integrated Security=True";
        public const string ConnectionString = "Data Source=TestSQL;Initial Catalog=YPIData;Integrated Security=True";

        private YellowstonePathology.Business.Mongo.LocalServer m_LocalServer;

        public ProcessRunner()
        {
            this.m_LocalServer = new Business.Mongo.LocalServer("LIS");
        }

        public void Run()
        {                                                
            BackgroundWorker transferTablesWorker = new BackgroundWorker();
            transferTablesWorker.DoWork += new DoWorkEventHandler(TransferTablesWorker_DoWork);
            transferTablesWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(TransferTablesWorker_RunWorkerCompleted);
            transferTablesWorker.RunWorkerAsync();                                  
        }

        private void CreateIndexes()
        {
            //MongoCollection mongoCollection = this.m_Database.Tables.GetCollection<BsonDocument>(transfer.CollectionName);
            //mongoCollection.CreateIndex(transfer.IndexKeyList.ToArray());
        }

        private void TransferPSOTablesWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            //TransferCollection transferCollection = TransferCollection.GetPSOInheritedTables();
            //this.TranserferTables(transferCollection);
        }

        private void TransferPSOTablesWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            System.Windows.MessageBox.Show("PSO Tables ALL DONE.");
        }             

        private void TransferTablesWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            System.Windows.MessageBox.Show("ALL DONE.");
        }

        private void TransferTablesWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            //TransferCollection transferCollection = TransferCollection.GetMainTableCollection();
            //this.TranserferTables(transferCollection);                                     
        }        

        private void TranserferTables(YellowstonePathology.Business.Mongo.TransferCollection transferCollection)
        {             
            foreach (YellowstonePathology.Business.Mongo.Transfer transfer in transferCollection)
            {               
                MongoCollection mongoCollection = this.m_LocalServer.Database.GetCollection<BsonDocument>(transfer.TableName);
                mongoCollection.Drop();                    

                SqlCommand cmd = new SqlCommand();
                //cmd.CommandText = transfer.GetSQL();
                cmd.CommandType = CommandType.Text;

                using (SqlConnection cn = new SqlConnection(ConnectionString))
                {
                    cn.Open();
                    cmd.Connection = cn;
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            /*
                            object o = Activator.CreateInstance(transfer.Type);
                            YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(o, dr);
                            sqlDataReaderPropertyWriter.WriteProperties();

                            BsonDocument document = YellowstonePathology.Business.Mongo.BSONBuilder.Build(o);                                
                            mongoCollection.Insert(document);
                            mongoCollection.Save(document);
                             */
                        }
                    }
                }

                /*
                foreach (string indexKey in transfer.IndexKeyList)
                {
                    mongoCollection.CreateIndex(indexKey);
                } 
                */
            }
        }                                
    }
}
