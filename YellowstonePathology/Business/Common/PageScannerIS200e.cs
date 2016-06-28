using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Common
{
	public class PageScannerIS200e : PageScanner
	{
		public PageScannerIS200e() : base("IMAGE SCANNER IS200e(USB)")
		{
			this.SetScanSettings();
		}

		protected override void SetScanSettings()
		{
		}
	}
}
