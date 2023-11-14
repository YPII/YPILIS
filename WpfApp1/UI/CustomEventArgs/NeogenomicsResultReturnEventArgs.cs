using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI.CustomEventArgs
{
    public class NeogenomicsResultReturnEventArgs : System.EventArgs
    {
		private YellowstonePathology.Business.NeogenomicsResult m_NeogenomicsResult;

		public NeogenomicsResultReturnEventArgs(YellowstonePathology.Business.NeogenomicsResult neogenomicsResult)
        {
            this.m_NeogenomicsResult = neogenomicsResult;
        }

		public YellowstonePathology.Business.NeogenomicsResult NeogenomicsResult
        {
            get { return this.m_NeogenomicsResult; }
        }
    }
}
