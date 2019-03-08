using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace YellowstonePathology.Business.Persistence
{
    public class DocumentGateway
    {
        private static readonly object locker = new object();

        private static volatile DocumentGateway instance;
        private static object syncRoot = new Object();

        private Stack m_Stack;        

        static DocumentGateway()
        {

        }

        private DocumentGateway() 
        {
            this.m_Stack = new Stack();
        }

        public static DocumentGateway Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new DocumentGateway();
                    }
                }

                return instance;
            }
        }

        public DocumentCollection Documents
        {
            get { return this.m_Stack.Documents; }
        }

        public void Save()
        {
            lock (locker)
            {
                this.m_Stack.Save();
            }
        }        

        public void Push(object writer)
        {
            lock (locker)
            {
                this.m_Stack.Push(writer);
            }
        }

        public void ReleaseLock(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, object writer)
        {
            lock (locker)
            {
                this.m_Stack.ReleaseLock(accessionOrder, writer);
            }
        }

        public void RefreshAccessionOrder(string masterAccessionNo)
        {
            lock (locker)
            {
                AODocumentBuilder documentBuilder = new AODocumentBuilder(masterAccessionNo, true);
                Document document = this.m_Stack.Documents.Get(masterAccessionNo);
                YellowstonePathology.Business.Test.AccessionOrder accessionOrder = (YellowstonePathology.Business.Test.AccessionOrder)document.Value;
                documentBuilder.Refresh(accessionOrder);
                document.ResetClone();
            }
        }

        public void Flush()
        {
            lock (locker)
            {
                this.m_Stack.Flush();
            }
        }

        public void Clear(object writer)
        {
            lock (locker)
            {
                this.m_Stack.Push(writer);
            }
        }

        public void Push(Object o, object writer)
        {
            lock (locker)
            {
                this.m_Stack.Push(o, writer);
            }
        }

        public void DeleteDocument(object o, object writer)
        {
            lock (locker)
            {
                this.m_Stack.DeleteDocument(o, writer);
            }
        }

        public void InsertDocument(object o, Object writer)
        {
            lock (locker)
            {
                this.m_Stack.InsertDocument(o, writer);
            }
        }        

        public YellowstonePathology.Business.Test.AccessionOrder PullAccessionOrder(string masterAccessionNo, object writer)
        {
            lock(locker)
            {
                AODocumentBuilder documentBuilder = new AODocumentBuilder(masterAccessionNo, true);
                DocumentId documentId = new DocumentId(typeof(YellowstonePathology.Business.Test.AccessionOrder), writer, masterAccessionNo);
                Document document = this.m_Stack.Pull(documentId, documentBuilder);
                

                YellowstonePathology.Business.Test.AccessionOrder accessionOrder = (YellowstonePathology.Business.Test.AccessionOrder)document.Value;
                return accessionOrder;
            }            
        }

        public YellowstonePathology.Business.Test.AccessionOrder GetAccessionOrderByMasterAccessionNo(string masterAccessionNo)
        {
            lock (locker)
            {
                AODocumentBuilder documentBuilder = new AODocumentBuilder(masterAccessionNo, false);
                YellowstonePathology.Business.Test.AccessionOrder result = (YellowstonePathology.Business.Test.AccessionOrder)documentBuilder.BuildNew();
                return result;
            }
        }

        public YellowstonePathology.Business.Typing.TypingShortcut PullTypingShortcut(string objectId, object writer)
        {
            lock (locker)
            {
                MySqlCommand cmd = new MySqlCommand();
                TypingShortcutDocumentBuilder builder = new TypingShortcutDocumentBuilder(objectId);
                DocumentId documentId = new DocumentId(typeof(YellowstonePathology.Business.Typing.TypingShortcut), writer, objectId);
                Document document = this.m_Stack.Pull(documentId, builder);
                return (YellowstonePathology.Business.Typing.TypingShortcut)document.Value;
            }
        }

        public YellowstonePathology.Business.Client.Model.Client PullClient(int clientId, object writer)
        {
            lock (locker)
            {                                
                ClientDocumentBuilder documentBuilder = new ClientDocumentBuilder(clientId);
                DocumentId documentId = new DocumentId(typeof(YellowstonePathology.Business.Client.Model.Client), writer, clientId);
                Document document = this.m_Stack.Pull(documentId, documentBuilder);
                return (YellowstonePathology.Business.Client.Model.Client)document.Value;                
            }
        }

        public YellowstonePathology.Business.Domain.Physician PullPhysician(int physicianId, object writer)
        {
            lock (locker)
            {
                ProviderDocumentBuilder documentBuilder = new ProviderDocumentBuilder(physicianId);
                DocumentId documentId = new DocumentId(typeof(YellowstonePathology.Business.Domain.Physician), writer, physicianId);
                Document document = this.m_Stack.Pull(documentId, documentBuilder);
                return (YellowstonePathology.Business.Domain.Physician)document.Value;
            }
        }

        public YellowstonePathology.Business.Client.Model.ClientSupplyOrder PullClientSupplyOrder(string clientSupplyOrderId, object writer)
        {
            lock (locker)
            {
                ClientSupplyOrderDocumentBuilder builder = new ClientSupplyOrderDocumentBuilder(clientSupplyOrderId);

                DocumentId documentId = new DocumentId(typeof(YellowstonePathology.Business.Client.Model.ClientSupplyOrder), writer, clientSupplyOrderId);
                Document document = this.m_Stack.Pull(documentId, builder);
                return (YellowstonePathology.Business.Client.Model.ClientSupplyOrder)document.Value;
            }
        }

        public YellowstonePathology.Business.Task.Model.TaskOrder PullTaskOrder(string taskOrderId, object writer)
        {
            lock (locker)
            {
                TaskOrderDocumentBuilder taskOrderDocumentBuilder = new TaskOrderDocumentBuilder(taskOrderId);
                DocumentId documentId = new DocumentId(typeof(YellowstonePathology.Business.Task.Model.TaskOrder), writer, taskOrderId);
                Document document = this.m_Stack.Pull(documentId, taskOrderDocumentBuilder);
                return (YellowstonePathology.Business.Task.Model.TaskOrder)document.Value;
            }
        }

        public YellowstonePathology.Business.Slide.Model.SlideOrder PullSlideOrder(string slideOrderId, object writer)
        {
            lock (locker)
            {
                SlideOrderDocumentBuilder slideOrderDocumentBuilder = new SlideOrderDocumentBuilder(slideOrderId);
                DocumentId documentId = new DocumentId(typeof(YellowstonePathology.Business.Slide.Model.SlideOrder), writer, slideOrderId);
                Document document = this.m_Stack.Pull(documentId, slideOrderDocumentBuilder);
                return (YellowstonePathology.Business.Slide.Model.SlideOrder)document.Value;
            }
        }

        public YellowstonePathology.Business.Test.AliquotOrder PullAliquotOrder(string aliquotOrderId, object writer)
        {
            lock (locker)
            {
                AliquotOrderDocumentBuilder aliquotOrderDocumentBuilder = new AliquotOrderDocumentBuilder(aliquotOrderId);
                DocumentId documentId = new DocumentId(typeof(YellowstonePathology.Business.Test.AliquotOrder), writer, aliquotOrderId);
                Document document = this.m_Stack.Pull(documentId, aliquotOrderDocumentBuilder);
                return (YellowstonePathology.Business.Test.AliquotOrder)document.Value;
            }
        }

        public YellowstonePathology.Business.Specimen.Model.SpecimenOrder PullSpecimenOrderByContainerId(string containerId, object writer)
        {
            lock (locker)
            {
                SpecimenOrderDocumentBuilder specimenOrderDocumentBuilder = new SpecimenOrderDocumentBuilder();
                specimenOrderDocumentBuilder.SetSqlByContainerId(containerId);
                DocumentId documentId = new DocumentId(typeof(YellowstonePathology.Business.Specimen.Model.SpecimenOrder), writer, containerId);
                Document document = this.m_Stack.Pull(documentId, specimenOrderDocumentBuilder);
                return (YellowstonePathology.Business.Specimen.Model.SpecimenOrder)document.Value;
            }
        }

        public YellowstonePathology.Business.Specimen.Model.SpecimenOrder PullSpecimenOrder(string specimenOrderId, object writer)
        {
            lock (locker)
            {
                SpecimenOrderDocumentBuilder specimenOrderDocumentBuilder = new SpecimenOrderDocumentBuilder();
                specimenOrderDocumentBuilder.SetSqlBySpecimenOrderId(specimenOrderId);
                DocumentId documentId = new DocumentId(typeof(YellowstonePathology.Business.Specimen.Model.SpecimenOrder), writer, specimenOrderId);
                Document document = this.m_Stack.Pull(documentId, specimenOrderDocumentBuilder);
                return (YellowstonePathology.Business.Specimen.Model.SpecimenOrder)document.Value;
            }
        }

        public YellowstonePathology.Business.User.UserPreference PullUserPreference(string hostName, object writer)
        {
            lock (locker)
            {
                MySqlCommand cmd = new MySqlCommand("Select * from tblUserPreference where tblUserPreference.HostName = @HostName;");
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("@HostName", hostName);
                GenericDocumentBuilder builder = new GenericDocumentBuilder(cmd, typeof(YellowstonePathology.Business.User.UserPreference));

                DocumentId documentId = new DocumentId(typeof(YellowstonePathology.Business.User.UserPreference), writer, hostName);
                documentId.IsGlobal = true;
                Document document = this.m_Stack.Pull(documentId, builder);

                return (YellowstonePathology.Business.User.UserPreference)document.Value;
            }
        }

        public YellowstonePathology.Business.User.SystemUser PullSystemUser(int userId, object writer)
        {
            lock (locker)
            {
                SystemUserDocumentBuilder builder = new Persistence.SystemUserDocumentBuilder(userId);
                DocumentId documentId = new DocumentId(typeof(YellowstonePathology.Business.User.SystemUser), writer, userId);
                Document document = this.m_Stack.Pull(documentId, builder);
                return (YellowstonePathology.Business.User.SystemUser)document.Value;
            }
        }

        public YellowstonePathology.Business.ApplicationVersion GetApplicationVersion(object writer)
        {
            lock (locker)
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandText = "Select * from tblApplicationVersion;";
                cmd.CommandType = CommandType.Text;
                GenericDocumentBuilder builder = new GenericDocumentBuilder(cmd, typeof(YellowstonePathology.Business.ApplicationVersion));
                YellowstonePathology.Business.ApplicationVersion result = (YellowstonePathology.Business.ApplicationVersion)builder.BuildNew();
                return result;
            }
        }

        public YellowstonePathology.Business.ClientOrder.Model.ClientOrder PullClientOrder(string clientOrderId, object writer)
        {
            lock (locker)
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandText = "gwGetClientOrderByClientOrderId_1";
                cmd.Parameters.AddWithValue("ClientOrderId", clientOrderId);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                ClientOrderDocumentBuilder builder = new ClientOrderDocumentBuilder(cmd);

                DocumentId documentId = new DocumentId(typeof(YellowstonePathology.Business.ClientOrder.Model.ClientOrder), writer, clientOrderId);
                Document document = this.m_Stack.Pull(documentId, builder);

                return (YellowstonePathology.Business.ClientOrder.Model.ClientOrder)document.Value;
            }
        }              

        public YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingBatch PullMaterialTrackingBatch(string materialTrackingBatchId, object writer)
        {
            lock (locker)
            {
                MaterialTrackingBatchDocumentBuilder builder = new MaterialTrackingBatchDocumentBuilder(materialTrackingBatchId);
                DocumentId documentId = new DocumentId(typeof(YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingBatch), writer, materialTrackingBatchId);
                Document document = this.m_Stack.Pull(documentId, builder);
                return (YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingBatch)document.Value;
            }
        }

        public YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingBatch PullMaterialTrackingBatchWithMasterAccessionNo(string materialTrackingBatchId, string masterAccessionNo, object writer)
        {
            lock (locker)
            {
                MaterialTrackingBatchDocumentBuilder builder = new MaterialTrackingBatchDocumentBuilder(materialTrackingBatchId, masterAccessionNo);
                DocumentId documentId = new DocumentId(typeof(YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingBatch), writer, materialTrackingBatchId);
                Document document = this.m_Stack.Pull(documentId, builder);
                return (YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingBatch)document.Value;
            }
        }

        public YellowstonePathology.Business.Facility.Model.Facility PullFacility(string facilityId, object writer)
        {
            lock (locker)
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandText = "Select * from tblFacility f where f.FacilityId = @FacilityId;";
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@FacilityId", facilityId);
                GenericDocumentBuilder builder = new GenericDocumentBuilder(cmd, typeof(YellowstonePathology.Business.Facility.Model.Facility));

                DocumentId documentId = new DocumentId(typeof(YellowstonePathology.Business.Facility.Model.Facility), writer, facilityId);
                Document document = this.m_Stack.Pull(documentId, builder);
                return (YellowstonePathology.Business.Facility.Model.Facility)document.Value;
            }
        }

        public YellowstonePathology.Business.WebService.WebServiceAccount PullWebServiceAccount(int webServiceAccountId, object writer)
        {
            lock (locker)
            {
                WebServiceAccountDocumentBuilder builder = new WebServiceAccountDocumentBuilder(webServiceAccountId);

                DocumentId documentId = new DocumentId(typeof(YellowstonePathology.Business.WebService.WebServiceAccount), writer, webServiceAccountId);
                Document document = this.m_Stack.Pull(documentId, builder);
                return (YellowstonePathology.Business.WebService.WebServiceAccount)document.Value;
            }
        }

        public YellowstonePathology.Business.Domain.OrderCommentLog PullOrderCommentLog(string orderCommentLogId, object writer)
        {
            lock (locker)
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandText = "SELECT * from tblOrderCommentLog where OrderCommentLogId = @OrderCommentLogId;";
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@OrderCommentLogId", orderCommentLogId);
                GenericDocumentBuilder builder = new GenericDocumentBuilder(cmd, typeof(YellowstonePathology.Business.Domain.OrderCommentLog));

                DocumentId documentId = new DocumentId(typeof(YellowstonePathology.Business.Domain.OrderCommentLog), writer, orderCommentLogId);
                Document document = this.m_Stack.Pull(documentId, builder);
                return (YellowstonePathology.Business.Domain.OrderCommentLog)document.Value;
            }
        }
    }
}
