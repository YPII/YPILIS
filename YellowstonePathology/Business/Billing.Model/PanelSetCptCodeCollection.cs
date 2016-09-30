using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace YellowstonePathology.Business.Billing.Model
{
	public class PanelSetCptCodeCollection : ObservableCollection<PanelSetCptCode>
	{        
		public PanelSetCptCodeCollection()
		{

		}

        /*
        public static PanelSetCptCodeCollection GetByPanelSetId(int panelSetId)
        {
            PanelSetCptCodeCollection allCollection = GetAll();
            PanelSetCptCodeCollection result = new PanelSetCptCodeCollection();
            foreach (PanelSetCptCode panelSetCptCode in allCollection)
            {
                if (panelSetCptCode.PanelSetId == panelSetId)
                {
                    result.Add(panelSetCptCode);
                }
            }
            return result;
        }
        */

        /*
        public static PanelSetCptCodeCollection GetAll()
        {
            PanelSetCptCodeCollection result = new PanelSetCptCodeCollection();
            result.Add(new PanelSetCptCode(1, new CptCodeDefinition.CPT81270(), 1));
            result.Add(new PanelSetCptCode(1, new CptCodeDefinition.CPT8127026(), 1));
            result.Add(new PanelSetCptCode(2, new CptCodeDefinition.CPT81220(), 1));
            result.Add(new PanelSetCptCode(2, new CptCodeDefinition.CPT8122026(), 1));
            result.Add(new PanelSetCptCode(3, new CptCodeDefinition.CPT87491(), 1));
            result.Add(new PanelSetCptCode(3, new CptCodeDefinition.CPT87591(), 1));
            result.Add(new PanelSetCptCode(4, new CptCodeDefinition.CPT81275(), 1));
            result.Add(new PanelSetCptCode(4, new CptCodeDefinition.CPT8127526(), 1));
            result.Add(new PanelSetCptCode(10, new CptCodeDefinition.CPT87621(), 1));
            result.Add(new PanelSetCptCode(12, new CptCodeDefinition.CPT88368(), 2));
            result.Add(new PanelSetCptCode(14, new CptCodeDefinition.CPT87621(), 1));
            result.Add(new PanelSetCptCode(18, new CptCodeDefinition.CPT81210(), 1));
            result.Add(new PanelSetCptCode(18, new CptCodeDefinition.CPT8121026(), 1));
            result.Add(new PanelSetCptCode(19, new CptCodeDefinition.CPT88184(), 1));
            result.Add(new PanelSetCptCode(19, new CptCodeDefinition.CPT88185(), 8));
            result.Add(new PanelSetCptCode(19, new CptCodeDefinition.CPT88188(), 1));
            result.Add(new PanelSetCptCode(25, new CptCodeDefinition.CPT87621(), 1));
            result.Add(new PanelSetCptCode(27, new CptCodeDefinition.CPT81275(), 1));
            result.Add(new PanelSetCptCode(27, new CptCodeDefinition.CPT8127526(), 1));
            result.Add(new PanelSetCptCode(32, new CptCodeDefinition.CPT81241(), 1));
            result.Add(new PanelSetCptCode(32, new CptCodeDefinition.CPT8124126(), 1));
            result.Add(new PanelSetCptCode(33, new CptCodeDefinition.CPT81240(), 1));
            result.Add(new PanelSetCptCode(33, new CptCodeDefinition.CPT81240(), 1));
            result.Add(new PanelSetCptCode(34, new CptCodeDefinition.CPT81291(), 1));
            result.Add(new PanelSetCptCode(34, new CptCodeDefinition.CPT8129126(), 1));
            result.Add(new PanelSetCptCode(36, new CptCodeDefinition.CPT81261(), 1));
            result.Add(new PanelSetCptCode(36, new CptCodeDefinition.CPT8126126(), 1));
            result.Add(new PanelSetCptCode(44, new CptCodeDefinition.CPT81241(), 1));
            result.Add(new PanelSetCptCode(44, new CptCodeDefinition.CPT8124126(), 1));
            result.Add(new PanelSetCptCode(46, new CptCodeDefinition.CPT88368(), 2));
            result.Add(new PanelSetCptCode(46, new CptCodeDefinition.CPT8124026(), 1));
            result.Add(new PanelSetCptCode(48, new CptCodeDefinition.CPT8124026(), 1));
            result.Add(new PanelSetCptCode(48, new CptCodeDefinition.CPT81235(), 1));
            result.Add(new PanelSetCptCode(60, new CptCodeDefinition.CPT8123526(), 1));
            result.Add(new PanelSetCptCode(61, new CptCodeDefinition.CPT87798(), 1));
            result.Add(new PanelSetCptCode(62, new CptCodeDefinition.CPT87621(), 2));
            return result;
        }
        */
	}
}
