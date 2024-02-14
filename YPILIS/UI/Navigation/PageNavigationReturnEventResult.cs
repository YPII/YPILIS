using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI.Navigation
{
    public class PageNavigationReturnEventArgs : EventArgs
    {        
		private PageNavigationDirectionEnum m_PageNavigationDirectionEnum;
		private object m_Data;

		public PageNavigationReturnEventArgs()
		{

		}

        public PageNavigationReturnEventArgs(PageNavigationDirectionEnum pageNavigationDirectionEnum, object data)
		{			
			this.m_PageNavigationDirectionEnum = pageNavigationDirectionEnum;
			this.m_Data = data;
		}		

		public PageNavigationDirectionEnum PageNavigationDirectionEnum
		{
			get { return this.m_PageNavigationDirectionEnum; }
			set { this.m_PageNavigationDirectionEnum = value; }
		}

		public object Data
		{
			get { return this.m_Data; }
			set { this.m_Data = value; }
		}
    }
}
