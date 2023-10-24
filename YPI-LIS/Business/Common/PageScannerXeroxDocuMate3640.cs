using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Common
{
	class PageScannerXeroxDocuMate3640 : PageScanner
	{
		public PageScannerXeroxDocuMate3640() : base("Xerox DocuMate 3640")
		{
			this.SetScanSettings();
		}

		protected override void SetScanSettings()
		{
		}
	}
}
