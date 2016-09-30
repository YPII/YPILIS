using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.UI
{
    public class AOBuilder
    {
        private YellowstonePathology.UI.Login.Receiving.ClientOrderReceivingHandler m_ClientOrderReceivingHandler;

        public AOBuilder()
        {
            this.m_ClientOrderReceivingHandler = new Login.Receiving.ClientOrderReceivingHandler(this);
        }

        public YellowstonePathology.Business.Test.AccessionOrder Build()
        {
            this.BuildClientOrder();
            this.BuildClientOrderDetail();
            this.CreateAccession();
            this.LinkPatient();
            this.SetProvider();
            this.CreateSurgicalTestOrder();
            this.AddAliquot();

            YellowstonePathology.Business.Persistence.DocumentGateway.Instance.Save();
            return this.m_ClientOrderReceivingHandler.AccessionOrder;
        }

        private void BuildClientOrder()
        {
            YellowstonePathology.Business.Client.Model.Client client = YellowstonePathology.Business.Gateway.PhysicianClientGateway.GetClientByClientId(280);
            client.ClientLocationCollection.SetCurrentLocationToMedicalRecordsOrFirst();
            this.m_ClientOrderReceivingHandler.IFoundAClient(client);
            this.m_ClientOrderReceivingHandler.LetsUseANewClientOrder();
            this.m_ClientOrderReceivingHandler.ClientOrder.PFirstName = "Mickey";
            this.m_ClientOrderReceivingHandler.ClientOrder.PLastName = "Mouse";
            this.m_ClientOrderReceivingHandler.ClientOrder.PMiddleInitial = "M";
            this.m_ClientOrderReceivingHandler.ClientOrder.PSex = "M";
            this.m_ClientOrderReceivingHandler.ClientOrder.PBirthdate = DateTime.Parse("08/10/1966");
            this.m_ClientOrderReceivingHandler.SetPatientAsVerified();
            this.m_ClientOrderReceivingHandler.ConfirmTheClientOrder();
        }

        private void BuildClientOrderDetail()
        {
            YellowstonePathology.Business.BarcodeScanning.ContainerBarcode containerBarcode = YellowstonePathology.Business.BarcodeScanning.ContainerBarcode.Parse();
            YellowstonePathology.UI.Login.Receiving.IFoundAContainerResult result = this.m_ClientOrderReceivingHandler.IFoundAContainer(containerBarcode.ToString());

            result.ClientOrderDetail.DescriptionToAccessionBinding = "Speciment 1";
            result.ClientOrderDetail.FixationStartTimeBinding = DateTime.Now.AddHours(-3).ToString("dd/MM/yyyy HH:mm");
            result.ClientOrderDetail.ClientFixationBinding = YellowstonePathology.Business.Specimen.Model.FixationType.Formalin;
            result.ClientOrderDetail.LabFixationBinding = YellowstonePathology.Business.Specimen.Model.FixationType.Formalin;
            result.ClientOrderDetail.SetFixationStartTime();
        }

        private void CreateAccession()
        {
            this.m_ClientOrderReceivingHandler.CreateNewAccessionOrder(Business.Test.AccessionTypeEnum.Surgical);
            this.m_ClientOrderReceivingHandler.AccessionClientOrder();
        }

        private void LinkPatient()
        {
            this.m_ClientOrderReceivingHandler.AccessionOrder.PatientId = YellowstonePathology.Business.Gateway.PatientLinkingGateway.GetNewPatientId();
        }

        private void SetProvider()
        {
            YellowstonePathology.Business.Client.Model.PhysicianClientDistributionList physicianClientDistributionCollection = YellowstonePathology.Business.Gateway.PhysicianClientGateway.GetPhysicianClientDistributionByClientPhysicianLastNameV2("Yellowstone Pathologists, P.C.", "Schultz");
            this.m_ClientOrderReceivingHandler.AccessionOrder.SetPhysicianClient(physicianClientDistributionCollection[0]);
        }

        private void CreateSurgicalTestOrder()
        {
            YellowstonePathology.Business.Test.Surgical.SurgicalTest surgicalTest = new Business.Test.Surgical.SurgicalTest();
            YellowstonePathology.Business.Test.TestOrderInfo testOrderInfo = new Business.Test.TestOrderInfo();
            testOrderInfo.PanelSet = surgicalTest;
            testOrderInfo.Distribute = true;

            YellowstonePathology.UI.CustomEventArgs.TestOrderInfoEventArgs reportOrderInfoEventArgs = new CustomEventArgs.TestOrderInfoEventArgs(testOrderInfo);
            Login.Receiving.ReportOrderPath reportOrderPath = new Login.Receiving.ReportOrderPath(this.m_ClientOrderReceivingHandler.AccessionOrder, this.m_ClientOrderReceivingHandler.ClientOrder, null, PageNavigationModeEnum.Standalone, null);
            reportOrderPath.Finish += new Login.Receiving.ReportOrderPath.FinishEventHandler(ReportOrderPath_Finish);
            reportOrderPath.Start(testOrderInfo);
        }

        private void ReportOrderPath_Finish(object sender, CustomEventArgs.TestOrderInfoEventArgs e)
        {
        }

        private void AddAliquot()
        {
            YellowstonePathology.Business.Test.Model.HandE handeTest = new Business.Test.Model.HandE();
            this.m_ClientOrderReceivingHandler.AccessionOrder.PrintMateColumnNumber = 1;
            string reportNo = this.m_ClientOrderReceivingHandler.AccessionOrder.PanelSetOrderCollection.GetSurgical().ReportNo;
            foreach (YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder in this.m_ClientOrderReceivingHandler.AccessionOrder.SpecimenOrderCollection)
            {
                YellowstonePathology.Business.Test.AliquotOrder aliquotOrder = specimenOrder.AliquotOrderCollection.AddBlock(specimenOrder, "Direct Print", this.m_ClientOrderReceivingHandler.AccessionOrder.AccessionDate.Value);
                specimenOrder.AliquotRequestCount = specimenOrder.AliquotOrderCollection.Count;
                YellowstonePathology.Business.Visitor.OrderTestVisitor orderTestVisitor = new Business.Visitor.OrderTestVisitor(reportNo, handeTest, null, null, false, aliquotOrder, false, false, this.m_ClientOrderReceivingHandler.AccessionOrder.TaskOrderCollection);
                this.m_ClientOrderReceivingHandler.AccessionOrder.TakeATrip(orderTestVisitor);

                YellowstonePathology.Business.Visitor.AddSlideOrderVisitor addSlideOrderVisitor = new Business.Visitor.AddSlideOrderVisitor(aliquotOrder, orderTestVisitor.TestOrder);
                this.m_ClientOrderReceivingHandler.AccessionOrder.TakeATrip(addSlideOrderVisitor);
            }
        }      
    }
}
