using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace YellowstonePathology.Business.Mongo
{
    public class DocumentCollectionTracker
    {        
        private List<DocumentTracker> m_DocumentTrackerList;
        private YellowstonePathology.Business.Mongo.Server m_MongoServer;

        public DocumentCollectionTracker(YellowstonePathology.Business.Mongo.TransferCollection collection, YellowstonePathology.Business.Mongo.Server mongoServer)
        {
            this.m_MongoServer = mongoServer;
            this.m_DocumentTrackerList = new List<DocumentTracker>();

            foreach (INotifyPropertyChanged o in collection)
            {
                DocumentTracker documentTracker = new DocumentTracker(this.m_MongoServer);
                documentTracker.Register(o);
                this.m_DocumentTrackerList.Add(documentTracker);
            }

            collection.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(CollectionChanged);
        }

        public DocumentCollectionTracker(YellowstonePathology.Business.ReportDistribution.Model.TestOrderReportDistributionCollection collection, YellowstonePathology.Business.Mongo.Server mongoServer)
        {
            this.m_MongoServer = mongoServer;
            this.m_DocumentTrackerList = new List<DocumentTracker>();

            foreach (INotifyPropertyChanged o in collection)
            {
                DocumentTracker documentTracker = new DocumentTracker(this.m_MongoServer);
                documentTracker.Register(o);
                this.m_DocumentTrackerList.Add(documentTracker);
            }

            collection.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(CollectionChanged);
        }

        public DocumentCollectionTracker(YellowstonePathology.Business.ReportDistribution.Model.ReportDistributionLogEntryCollection collection, YellowstonePathology.Business.Mongo.Server mongoServer)
        {
            this.m_MongoServer = mongoServer;
            this.m_DocumentTrackerList = new List<DocumentTracker>();

            foreach (INotifyPropertyChanged o in collection)
            {
                DocumentTracker documentTracker = new DocumentTracker(this.m_MongoServer);
                documentTracker.Register(o);
                this.m_DocumentTrackerList.Add(documentTracker);
            }

            collection.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(CollectionChanged);
        }        

        private void CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {                
            switch (e.Action)
            {
                case System.Collections.Specialized.NotifyCollectionChangedAction.Add:                    
                    this.HandleNewItems(e.NewItems);
                    break;                
                case System.Collections.Specialized.NotifyCollectionChangedAction.Remove:                                        
                    this.HandleOldItems(e.OldItems);
                    break;                
                case System.Collections.Specialized.NotifyCollectionChangedAction.Replace:
                    throw new Exception("Not Implemented.");                    
                case System.Collections.Specialized.NotifyCollectionChangedAction.Reset:
                    throw new Exception("Not Supported.");                    
            }               
        }

        private void HandleNewItems(IList newItems)
        {
            foreach (object o in newItems)
            {
                DocumentTracker documentTracker = new DocumentTracker(this.m_MongoServer);                
                documentTracker.Register(o);                
                documentTracker.Status = DocumentStatusEnum.Inserted;
                this.m_DocumentTrackerList.Add(documentTracker);
            }
        }

        private void HandleOldItems(IList oldItems)
        {            
            foreach (object o in oldItems)
            {
                DocumentTracker documentTracker = new DocumentTracker(this.m_MongoServer);
                documentTracker.Register(o);
                documentTracker.Status = DocumentStatusEnum.Deleted;
                this.m_DocumentTrackerList.Add(documentTracker);
            }            
        }

        public void SubmitChanges()
        {
            foreach (DocumentTracker documentTracker in this.m_DocumentTrackerList)
            {
                documentTracker.SubmitChanges();
            }
        }
    }
}
