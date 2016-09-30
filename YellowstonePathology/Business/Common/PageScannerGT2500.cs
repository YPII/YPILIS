using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Common
{
	public class PageScannerGT2500 : PageScanner
	{
		public PageScannerGT2500() : base("EPSON GT-2500")
		{
			this.SetScanSettings();
		}

		protected override void SetScanSettings()
		{			
            this.ScanSettings.UseDocumentFeeder = true;
            this.ScanSettings.Resolution = new Business.Twain.ResolutionSettings();
            this.ScanSettings.Resolution.ColourSetting = Business.Twain.ColourSetting.BlackAndWhite;
            this.ScanSettings.Resolution.Dpi = 150;            
		}
	}
}
