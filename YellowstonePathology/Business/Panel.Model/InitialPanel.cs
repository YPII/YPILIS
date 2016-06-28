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
	public class InitialPanel : Panel
    {		
        public InitialPanel()
        {
            this.m_PanelId = 54;
            this.m_PanelName = "Initial Panel";
            this.m_AcknowledgeOnOrder = true;
		}	
	}
}
