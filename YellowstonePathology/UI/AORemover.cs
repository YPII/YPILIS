using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.UI
{
    public class AORemover
    {
        public AORemover()
        { }

        public static Business.Rules.MethodResult Remove(Business.Test.AccessionOrder accessionOrder, object writer)
        {
            YellowstonePathology.Business.Rules.MethodResult methodResult = new Business.Rules.MethodResult();
            accessionOrder.AccessionLock.ReleaseLock();
            
            YellowstonePathology.Business.ClientOrder.Model.ClientOrder clientOrder = YellowstonePathology.Business.Persistence.DocumentGateway.Instance.PullClientOrder(accessionOrder.ClientOrderId, writer);
            YellowstonePathology.Business.Persistence.DocumentGateway.Instance.DeleteDocument(clientOrder, writer);
            YellowstonePathology.Business.Persistence.DocumentGateway.Instance.DeleteDocument(accessionOrder, writer);
            
            return methodResult;
        }

        public static Business.Rules.MethodResult RemovePanelSet(string reportNo, Business.Test.AccessionOrder accessionOrder, object writer)
        {
            YellowstonePathology.Business.Rules.MethodResult methodResult = new Business.Rules.MethodResult();
            if(accessionOrder.PanelSetOrderCollection.Count > 1)
            {
                Business.Test.PanelSetOrder panelSetOrder = accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(reportNo);

                if ((accessionOrder.PLastName.ToUpper() == "MOUSE" && accessionOrder.PFirstName.ToUpper() == "MICKEY") ||
                    panelSetOrder.Final == false)
                {
                    if(panelSetOrder.PanelSetId == 15)
                    {
                        //delete the thinprep slide;
                        if(accessionOrder.SpecimenOrderCollection.HasThinPrepFluidSpecimen() == true)
                        {
                            Business.Specimen.Model.SpecimenOrder specimenOrder = accessionOrder.SpecimenOrderCollection.GetThinPrep();
                            if(specimenOrder.AliquotOrderCollection.HasThinPrepSlide() == true)
                            {
                                Business.Test.AliquotOrder aliquotOrder = specimenOrder.AliquotOrderCollection.GetThinPrepSlide();
                                specimenOrder.AliquotOrderCollection.Remove(aliquotOrder);
                            }
                        }
                    }

                    Business.Test.Model.TestOrderCollection testOrders = panelSetOrder.GetTestOrders();
                    for (int idx = testOrders.Count - 1; idx > -1; idx--)
                    {
                        Business.Test.Model.TestOrder testOrder = (Business.Test.Model.TestOrder)testOrders[idx];
                        for (int dx = testOrder.SlideOrderCollection.Count - 1; dx > -1; dx--)
                        {
                            YellowstonePathology.Business.Slide.Model.SlideOrder slideOrder = accessionOrder.SpecimenOrderCollection.GetSlideOrder(testOrder.SlideOrderCollection[dx].SlideOrderId);
                            YellowstonePathology.Business.Visitor.RemoveSlideOrderVisitor removeSlideOrderVisitor = new Business.Visitor.RemoveSlideOrderVisitor(slideOrder);
                            accessionOrder.TakeATrip(removeSlideOrderVisitor);
                        }
                    }

                    accessionOrder.PanelSetOrderCollection.Remove(panelSetOrder);
                    accessionOrder.TaskOrderCollection.RemoveTaskOrdersForDeletedTestOrder(reportNo);
                    YellowstonePathology.Business.Persistence.DocumentGateway.Instance.Push(accessionOrder, writer);
                }
                else
                {
                    methodResult.Success = false;
                    methodResult.Message = "Unable to remove a Panel Set that has been finaled.";
                }
            }
            else
            {
                methodResult.Success = false;
                methodResult.Message = "Unable to remove the only Panel Set for the Accession.";
            }
            return methodResult;
        }
    }
}
