using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Xml.Serialization;

namespace YellowstonePathology.Business.Panel.Model
{	
	public class SpecialStainPanel : Panel
    {
        public SpecialStainPanel()
        {
            this.m_PanelId = 19;
            this.m_PanelName = "Stain Orders";
            this.m_AcknowledgeOnOrder = false;
		}	
	}
}
