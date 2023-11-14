using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.HL7View
{
    public class VentanaStainOrder
    {
        public VentanaStainOrder()
        {

        }

        public void HandleOrder(Business.Test.AccessionOrder accessionOrder, YellowstonePathology.Business.Slide.Model.SlideOrder slideOrder)
        {
            if (slideOrder.LabelType == Business.Slide.Model.SlideLabelTypeEnum.PaperLabel.ToString())
            {
                Business.Stain.Model.Stain stain = Business.Stain.Model.StainCollection.Instance.GetStainByTestId(slideOrder.TestId);
                if (slideOrder.PerformedByHand == false || stain.PerformedByHand == false)
                {
                    if (this.CanBuild(accessionOrder, slideOrder.TestOrderId, slideOrder.SlideOrderId) == true)
                    {
                        string result = this.Build(accessionOrder, slideOrder.TestOrderId, slideOrder.SlideOrderId);
                        slideOrder.OrderSentToVentana = true;

                        YellowstonePathology.Business.Test.Model.TestOrder testOrder = accessionOrder.PanelSetOrderCollection.GetTestOrderByTestOrderId(slideOrder.TestOrderId);
                        testOrder.TestStatus = "CUTTING";
                        testOrder.TestStatusUpdateTime = DateTime.Now;

                        string objectId = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
                        System.IO.File.WriteAllText(@"\\10.1.2.31\ChannelData\Outgoing\Ventana\" + objectId + ".hl7", result.ToString());
                        //System.IO.File.WriteAllText(@"d:\testing\ventana.hl7", result.ToString());
                    }
                }
                else
                {
                    YellowstonePathology.Business.Test.Model.TestOrder testOrder = accessionOrder.PanelSetOrderCollection.GetTestOrderByTestOrderId(slideOrder.TestOrderId);
                    testOrder.TestStatus = "PERFORMEDBYHAND";
                    testOrder.TestStatusUpdateTime = DateTime.Now;
                }

                string printerName = "ZDesigner GX430t";
                Business.Label.Model.ZPLPrinterUSB zplPrinterUSB = new Business.Label.Model.ZPLPrinterUSB(printerName);
                Business.Label.Model.HistologySlidePaperZPLLabelV1 zplCommand = new Label.Model.HistologySlidePaperZPLLabelV1(slideOrder.SlideOrderId, slideOrder.ReportNo, slideOrder.PatientFirstName, slideOrder.PatientLastName, slideOrder.TestAbbreviation, slideOrder.Label, slideOrder.AccessioningFacility, slideOrder.UseWetProtocol, slideOrder.PerformedByHand);
                zplPrinterUSB.Print(zplCommand);

                slideOrder.Printed = true;
                slideOrder.PrintedBy = Business.User.SystemIdentity.Instance.User.UserName;
                slideOrder.PrintedById = Business.User.SystemIdentity.Instance.User.UserId;
                slideOrder.Status = "Validated";
                slideOrder.Validated = true;
                slideOrder.ValidatedBy = Business.User.SystemIdentity.Instance.User.UserName;
                slideOrder.ValidatedById = Business.User.SystemIdentity.Instance.User.UserId;
            }
        }

        public bool CanBuild(Business.Test.AccessionOrder accessionOrder, string testOrderId, string slideOrderId)
        {
            bool result = false;

            Business.Test.PanelOrder panelOrder = accessionOrder.PanelSetOrderCollection.GetPanelOrderByTestOrderId(testOrderId);
            Business.User.SystemUser orderedBy = Business.User.SystemUserCollectionInstance.Instance.SystemUserCollection.GetSystemUserById(panelOrder.OrderedById);

            Business.Test.AliquotOrder aliquotOrder = accessionOrder.SpecimenOrderCollection.GetAliquotOrderByTestOrderId(testOrderId);
            Business.Test.Model.TestOrder testOrder = panelOrder.TestOrderCollection.Get(testOrderId);

            Business.Slide.Model.SlideOrder slideOrder = aliquotOrder.SlideOrderCollection.GetSlideOrderByTestOrderId(testOrderId);
            Business.Specimen.Model.SpecimenOrder specimenOrder = accessionOrder.SpecimenOrderCollection.GetSpecimenOrderByAliquotOrderId(aliquotOrder.AliquotOrderId);

            Business.Stain.Model.Stain stain = Business.Stain.Model.StainCollection.Instance.GetStainByTestId(testOrder.TestId);
            if (stain != null)
            {
                result = true;
                if (slideOrder.UseWetProtocol == true)
                {
                    result = false;
                    if (stain.HasWetProtocol == true || stain.UseWetProtocol == true)
                    {
                        result = true;
                    }
                }
            }
            return result;
        }

        public string Build(Business.Test.AccessionOrder accessionOrder, string testOrderId, string slideOrderId)
        {           
            Business.Test.PanelOrder panelOrder = accessionOrder.PanelSetOrderCollection.GetPanelOrderByTestOrderId(testOrderId);            

            Business.Test.AliquotOrder aliquotOrder = accessionOrder.SpecimenOrderCollection.GetAliquotOrderByTestOrderId(testOrderId);
            Business.Test.Model.TestOrder testOrder = panelOrder.TestOrderCollection.Get(testOrderId);

            Business.User.SystemUser orderedBy = Business.User.SystemUserCollectionInstance.Instance.SystemUserCollection.GetSystemUserByInitials(testOrder.OrderedBy);

            Business.Slide.Model.SlideOrder slideOrder = aliquotOrder.SlideOrderCollection.Get(slideOrderId);
            Business.Specimen.Model.SpecimenOrder specimenOrder = accessionOrder.SpecimenOrderCollection.GetSpecimenOrderByAliquotOrderId(aliquotOrder.AliquotOrderId);

            Business.Stain.Model.Stain stain = Business.Stain.Model.StainCollection.Instance.GetStainByTestId(testOrder.TestId);
            string ventanaBarcode = stain.VentanaBenchMarkId.ToString();
            string ventanaProtocolName = stain.VentanaBenchMarkProtocolName;
            if (slideOrder.UseWetProtocol == true)
            {
                if (stain.HasWetProtocol == true)
                {
                    ventanaBarcode = stain.VentanaBenchMarkWetId.ToString();
                    ventanaProtocolName = stain.VentanaBenchMarkWetProtocolName;
                }
            }
            
            string dateTimeOfMessage = DateTime.Now.ToString("yyyyMMddHHmmss");
            string messageControlId = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
            string msh = $@"MSH|^~\&|YPILIS|YPI|Ventana|YPI|{dateTimeOfMessage}||OML^O21|{messageControlId}|P|2.4|{Environment.NewLine}";
            
            string firstName = accessionOrder.PFirstName;
            string lastName = accessionOrder.PLastName;
            string middleInitial = string.IsNullOrEmpty(accessionOrder.PMiddleInitial) ? string.Empty : accessionOrder.PMiddleInitial;
            string birthdate = accessionOrder.PBirthdate.Value.ToString("yyyyMMdd");
            string sex = accessionOrder.PSex;
            string patientId = accessionOrder.PatientId;
            string pid = $@"PID|||||{lastName}^{firstName}^{middleInitial}||{birthdate}|{sex}|{Environment.NewLine}";

            string requestingPhysicianFirstname = orderedBy.FirstName;
            string requestingPhysicianLastname = orderedBy.LastName;
            string requestingPhysicianNpi = string.IsNullOrEmpty(orderedBy.NationalProviderId) ? Business.User.SystemIdentity.Instance.User.UserId.ToString() : orderedBy.NationalProviderId;
            string pv1 = $@"PV1|||||||{requestingPhysicianNpi}^{requestingPhysicianLastname}^{requestingPhysicianFirstname}|{Environment.NewLine}";

            string registrationDateTime = accessionOrder.AccessionTime.Value.ToString("yyyyMMddHHmm");
            string sac = $@"SAC|||||||{registrationDateTime}|{Environment.NewLine}";

            string placerOrderNumber = accessionOrder.MasterAccessionNo;
            string orc = $@"ORC|NW|{placerOrderNumber}|||||||||||||||YPI^Yellowstone Pathology||||YPI^Yellowstone Pathology|{Environment.NewLine}";
            
            string placerOrderNumberOBR = accessionOrder.MasterAccessionNo;
            string protocolNumber = ventanaBarcode;
            string protocolName = ventanaProtocolName;
            
            string observationDateTime = DateTime.Now.ToString("yyyyMMddHHmm");

            string specimenName = "NONE";
            if (string.IsNullOrEmpty(specimenOrder.SpecimenId) == false)
            {
                specimenName = specimenOrder.SpecimenId;
            }            

            string specimenDescription = specimenOrder.Description;
            if (specimenDescription != null && specimenDescription.Length > 90) specimenDescription = specimenDescription.Substring(0, 90);
            string pathologistNpi = string.IsNullOrEmpty(orderedBy.NationalProviderId) ? "NOTAPPLICABLE" : orderedBy.NationalProviderId;
            string pathologistLastname = orderedBy.LastName;
            string pathologistFirstname = orderedBy.FirstName;
            string slideId = "HSLD" + slideOrder.SlideOrderId;
            string slideSequence = Business.Specimen.Model.Slide.GetSlideNumber(slideOrder.Label);
            string blockid = aliquotOrder.AliquotOrderId;
            string blockSequence = Business.Specimen.Model.Block.GetBlockLetter(aliquotOrder.Label);
            string specimenId = specimenOrder.SpecimenOrderId;
            string specimenSequence = specimenOrder.SpecimenNumber.ToString();            
            
            string obr = $@"OBR|1|{placerOrderNumber}||{protocolNumber}^{protocolName}^STAIN|||{observationDateTime}||||||||{specimenName}^{specimenDescription}^Surgical Pathology|{pathologistNpi}^{pathologistLastname}^{pathologistFirstname}|||{slideId}^{slideSequence}|{blockid}^{blockSequence}|{specimenId}^{specimenSequence}|||||||||||||||||||||||||||{Environment.NewLine}";

            //MSH| ^~\&| YPILIS | YPI | Ventana | YPI | 20201215135523 || OML ^ O21 | 5fd922bb482e5d16985658bf | P | 2.4 |
            //PID||||| GRIFFITHS ^ JOY ^|| 19640515 | F |
            //PV1||||||| 1740658020 ^ Bibbey ^ Scott |
            //SAC||||||| 202012151155 |
            //ORC|NW| 20 - 35913 ||||||||||||||| YPI ^ Yellowstone Pathology |||| YPI ^ Yellowstone Pathology |
            //OBR|1|20-35913||3008^HER2 DUAL ISH 6^STAIN|||202012151355||||||||CNSLT^Breast, right, needle core biopsy^Surgical Pathology|1740658020^Bibbey^ Scott ||| HSLD20 - 35913.1A7 ^ 7 | 20 - 35913.1A ^ A | 20 - 35913.1 ^ 1 |||||||||||||||||||||||||||

            StringBuilder result = new StringBuilder();
            result.Append(msh);
            result.Append(pid);
            result.Append(pv1);
            result.Append(sac);
            result.Append(orc);
            result.Append(obr);
            
            return result.ToString();
        }
    }
}
