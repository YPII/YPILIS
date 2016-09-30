using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI.CustomEventArgs
{
	public class FacilitySelectionReturnEventArgs : System.EventArgs
	{
        private string m_Action;

		public FacilitySelectionReturnEventArgs(string action)
        {
            this.m_Action = action;
        }

        public string Action
        {
            get { return this.m_Action; }
        }
	}
}
