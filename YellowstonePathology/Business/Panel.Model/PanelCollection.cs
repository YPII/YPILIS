using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Xml.Serialization;

namespace YellowstonePathology.Business.Panel.Model
{	
	public class PanelCollection : ObservableCollection<Panel>
	{
        public PanelCollection()
        {

        }

        public YellowstonePathology.Business.Panel.Model.Panel GetPanel(int panelId)
        {
            Panel result = new Panel();
            foreach (YellowstonePathology.Business.Panel.Model.Panel panel in this)
            {
                if (panel.PanelId == panelId)
                {
                    result = panel;
                    break;
                }
            }
            return result;
        }

        public static PanelCollection GetAll()
        {
            PanelCollection result = new PanelCollection();

            result.Add(new Business.Test.LynchSyndrome.LynchSyndromeIHCPanel());
            result.Add(new YellowstonePathology.Business.Test.ErPrSemiQuantitative.ERPRSemiQuantitativePanel());
            result.Add(new YellowstonePathology.Business.Test.HER2AmplificationByISH.HER2AmplificationByISHPanel());

            result.Add(new YellowstonePathology.Business.Test.ThinPrepPap.ThinPrepPapAcidWashPanel());
            result.Add(new YellowstonePathology.Business.Test.ThinPrepPap.ThinPrepPapDotReviewPanel());
            result.Add(new YellowstonePathology.Business.Test.ThinPrepPap.ThinPrepPapQCPanel());
            result.Add(new YellowstonePathology.Business.Test.ThinPrepPap.ThinPrepPapScreeningPanel());
            result.Add(new YellowstonePathology.Business.Test.ThinPrepPap.PrimaryScreeningPanel());

            return result;
        }
	}
}
