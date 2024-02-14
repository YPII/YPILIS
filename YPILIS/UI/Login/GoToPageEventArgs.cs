using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace YellowstonePathology.UI.Login
{
	public class GoToPageEventArgs : EventArgs
	{
		Page m_PageToGoTo;

		public GoToPageEventArgs()
		{
		}

		public GoToPageEventArgs(Page pageToGoTo)
		{
			this.m_PageToGoTo = pageToGoTo;
		}

		public Page PageToGoTo
		{
			get { return this.m_PageToGoTo; }
			set { this.m_PageToGoTo = value; }
		}
	}
}
