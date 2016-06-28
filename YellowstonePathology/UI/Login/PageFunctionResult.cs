using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI.Login
{
	public class PageFunctionResult
	{
		private bool m_Success;
		private YellowstonePathology.UI.Navigation.PageNavigationDirectionEnum m_PageNavigationDirectionEnum;
		private object m_Data;

		public PageFunctionResult()
		{
		}

        public PageFunctionResult(bool success, YellowstonePathology.UI.Navigation.PageNavigationDirectionEnum pageNavigationDirectionEnum, object data)
		{
			this.m_Success = success;
			this.m_PageNavigationDirectionEnum = pageNavigationDirectionEnum;
			this.m_Data = data;
		}

		public bool Success
		{
			get { return this.m_Success; }
			set { this.m_Success = value; }
		}

        public YellowstonePathology.UI.Navigation.PageNavigationDirectionEnum PageNavigationDirectionEnum
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
