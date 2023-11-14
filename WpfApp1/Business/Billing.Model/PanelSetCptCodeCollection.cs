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
        
        public string GetCommaSeparatedString()
        {
            string result = null;
            foreach(PanelSetCptCode panelSetCptCode in this)
            {
                if(result == null)
                {
                    result = panelSetCptCode.Quantity + " - " + panelSetCptCode.CptCode.Code;
                }
                else
                {
                    result = result + ", " + panelSetCptCode.Quantity + " - " + panelSetCptCode.CptCode.Code;
                }
            }            
            return result;
        }      
	}
}