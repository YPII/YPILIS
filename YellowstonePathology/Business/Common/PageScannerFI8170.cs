using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Common
{
	public class PageScannerFI8170 : PageScanner
	{
        public PageScannerFI8170()
            : base("fi-8170")
		{
			this.SetScanSettings();
		}

		protected override void SetScanSettings()
		{			
            this.ScanSettings.UseDocumentFeeder = true;
            this.ScanSettings.Resolution = new Business.Twain.ResolutionSettings();
            this.ScanSettings.Resolution.ColourSetting = Business.Twain.ColourSetting.BlackAndWhite;
            this.ScanSettings.Resolution.Dpi = 150;
			this.ScanSettings.UseDuplex = true;
		}
	}
}
