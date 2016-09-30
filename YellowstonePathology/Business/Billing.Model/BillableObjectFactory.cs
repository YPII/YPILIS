using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Billing.Model
{
    public class BillableObjectFactory
    {
        public static BillableObject GetBillableObject(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo)
        {
            BillableObject result = null;
            if (IsMountainViewNeo(accessionOrder, reportNo) == true)
            {
                result = new BillableObjectMountainViewNeo(accessionOrder, reportNo);
            }
            else if(IsMedicareProstateNeedleBiopsy(accessionOrder, reportNo) == true)
            {
                result = new BillableObjectMedicareProstateNeedleBiopsy(accessionOrder, reportNo);
            }
            else if(IsAutopsyTechnicalOnly(accessionOrder, reportNo) == true)
            {
                result = new BillableObjectTechnicalOnlyAutopsy(accessionOrder, reportNo);
            }
            else
            {
                result = GetStandardBillableObject(accessionOrder, reportNo);
            }            
            return result;
        }

        public static bool IsAutopsyTechnicalOnly(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo)
        {
            bool result = false;

            YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder = accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(reportNo);
            YellowstonePathology.Business.PanelSet.Model.PanelSetCollection panelSetCollection = YellowstonePathology.Business.PanelSet.Model.PanelSetCollection.GetAll();
            YellowstonePathology.Business.PanelSet.Model.PanelSet panelSet = panelSetCollection.GetPanelSet(panelSetOrder.PanelSetId);

            if (panelSet is YellowstonePathology.Business.Test.TechnicalOnly.TechnicalOnlyTest == true)
            {
                if(accessionOrder.ClientId == 1520) //Montana Department of Justice Forensic Science Division
                {
                    result = true;
                }
            }
            return result;
        }

        public static bool IsMedicareProstateNeedleBiopsy(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo)
        {
            bool result = false;
            YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder = accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(reportNo);
            if (panelSetOrder.PanelSetId == 13)
            {
                if (accessionOrder.PrimaryInsurance == "Medicare")
                {
                    YellowstonePathology.Business.Specimen.Model.SpecimenDefinition.ProstateNeedleBiopsy prostateNeedleBiopsy = new YellowstonePathology.Business.Specimen.Model.SpecimenDefinition.ProstateNeedleBiopsy();
					foreach (YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder in accessionOrder.SpecimenOrderCollection)
                    {
                        if (specimenOrder.SpecimenId == prostateNeedleBiopsy.SpecimenId)
                        {
                            result = true;
                            break;
                        }
                    }
                }
            }
            return result;
        }

        public static bool IsMountainViewNeo(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo)
        {
            bool result = false;
            YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder = accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(reportNo);
            YellowstonePathology.Business.Client.Model.ClientGroupClientCollection mountainViewGroup = YellowstonePathology.Business.Gateway.PhysicianClientGateway.GetClientGroupClientCollectionByClientGroupId(44);
            if(mountainViewGroup.ClientIdExists(accessionOrder.ClientId) == true)
            {
                YellowstonePathology.Business.Facility.Model.NeogenomicsIrvine neogenomicsIrvine = new YellowstonePathology.Business.Facility.Model.NeogenomicsIrvine();
                if(panelSetOrder.TechnicalComponentFacilityId == neogenomicsIrvine.FacilityId)
                {
                    result = true;
                }
            }
            
            return result;
        }        

        public static BillableObject GetStandardBillableObject(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo)
        {
            BillableObject result = null;

            YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder = accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(reportNo);
            YellowstonePathology.Business.PanelSet.Model.PanelSetCollection panelSetCollection = YellowstonePathology.Business.PanelSet.Model.PanelSetCollection.GetAll();
            YellowstonePathology.Business.PanelSet.Model.PanelSet panelSet = panelSetCollection.GetPanelSet(panelSetOrder.PanelSetId);

			if (panelSetOrder is YellowstonePathology.Business.Test.Surgical.SurgicalTestOrder == true)
            {
                result = new BillableObjectSurgicalPathology(accessionOrder, reportNo);
            }
			else if (panelSetOrder is YellowstonePathology.Business.Test.ThinPrepPap.PanelSetOrderCytology == true)
            {
                if (accessionOrder.PrimaryInsurance == "Medicare")
                {
                    result = new BillableObjectThinPrepPapMedicare(accessionOrder, reportNo);
                }
                else
                {
                    result = new BillableObjectThinPrepPap(accessionOrder, reportNo);
                }
            }
            else if (panelSet is YellowstonePathology.Business.PanelSet.Model.PanelSetMolecularTest == true)
            {
                result = new BillableObjectInHouseMolecular(accessionOrder, reportNo);
            }
            else if (panelSet is YellowstonePathology.Business.Test.KRASStandardReflex.KRASStandardReflexTest == true)
            {
                result = new BillableObjectKRASWithBRAFReflex(accessionOrder, reportNo);
            }
            else if (panelSet is YellowstonePathology.Business.Test.LLP.LeukemiaLymphomaTest == true)
            {
                result = new BillableObjectLeukemiaLymphoma(accessionOrder, reportNo);
            }
            else if (panelSet is YellowstonePathology.Business.Test.TechnicalOnly.TechnicalOnlyTest == true || panelSet is YellowstonePathology.Business.Test.IHCQC.IHCQCTest == true)
            {                
                result = new BillableObjectTechnicalOnly(accessionOrder, reportNo);
            }
            else
            {
                result = new BillableObject(accessionOrder, reportNo);
            }

            return result;
        }        
    }
}
