using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace YellowstonePathology.Business.Test.LynchSyndrome
{
	public class LSERuleCollection : ObservableCollection<LSERule>
	{
		public LSERuleCollection()
		{
			
		}

        public static LSERuleCollection GetMatchCollection(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, YellowstonePathology.Business.Test.LynchSyndrome.PanelSetOrderLynchSyndromeEvaluation panelSetOrderLynchSyndromeEvaluation)
        {
            LSERuleCollection indicationCollection = GetIndicationCollection(accessionOrder, panelSetOrderLynchSyndromeEvaluation);
            LSERuleCollection ihcCollection = GetIHCCollection(indicationCollection, accessionOrder, panelSetOrderLynchSyndromeEvaluation);
            return ihcCollection;
        }

        private static LSERuleCollection GetIndicationCollection(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, YellowstonePathology.Business.Test.LynchSyndrome.PanelSetOrderLynchSyndromeEvaluation panelSetOrderLynchSyndromeEvaluation)
        {
            LSERuleCollection allLSERuleCollection = LSERuleCollection.GetAll();
            LSERuleCollection result = new LynchSyndrome.LSERuleCollection();
            foreach (LSERule lseRule in allLSERuleCollection)
            {
                if (lseRule.IncludeInIndicationCollection(panelSetOrderLynchSyndromeEvaluation) == true)
                {
                    result.Add(lseRule);
                }
            }            
            return result;
        }

        private static LSERuleCollection GetIHCCollection(LSERuleCollection lseRuleCollection, YellowstonePathology.Business.Test.AccessionOrder accessionOrder, YellowstonePathology.Business.Test.LynchSyndrome.PanelSetOrderLynchSyndromeEvaluation panelSetOrderLynchSyndromeEvaluation)
        {
            LSERuleCollection result = new LSERuleCollection();
            YellowstonePathology.Business.Test.LynchSyndrome.LynchSyndromeIHCPanelTest panelSetLynchSyndromeIHCPanel = new YellowstonePathology.Business.Test.LynchSyndrome.LynchSyndromeIHCPanelTest();
            foreach (LSERule lseRule in lseRuleCollection)
            {
                if (accessionOrder.PanelSetOrderCollection.Exists(panelSetLynchSyndromeIHCPanel.PanelSetId, panelSetOrderLynchSyndromeEvaluation.OrderedOnId, false) == true)
                {
                    YellowstonePathology.Business.Test.LynchSyndrome.PanelSetOrderLynchSyndromeIHC panelSetOrderLynchSyndromeIHC = (YellowstonePathology.Business.Test.LynchSyndrome.PanelSetOrderLynchSyndromeIHC)accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(panelSetLynchSyndromeIHCPanel.PanelSetId, panelSetOrderLynchSyndromeEvaluation.OrderedOnId, true);
                    if (panelSetOrderLynchSyndromeIHC.Final == true)
                    {
                        if (lseRule.IncludeInIHCCollection(panelSetOrderLynchSyndromeIHC) == true)
                        {
                            result.Add(lseRule);
                        }
                    }
                    else
                    {
                        result.Add(lseRule);
                    }
                }
                else
                {
                    result.Add(lseRule);
                }
            }
            return result;
        }

        public static LSERuleCollection GetAll()
        {
            LSERuleCollection result = new LSERuleCollection();

            result.Add(new LSEColonAllIntact());
            result.Add(new LSEColonBRAFMeth());
            result.Add(new LSEColonSendOut());

            result.Add(new LSEGYNAllIntact());
            result.Add(new LSEGYNMeth());
            result.Add(new LSEGYNSendOut());

            result.Add(new LSEGeneralAllIntact());
            result.Add(new LSEGeneralSendOut());

            return result;
        }
    }
}
