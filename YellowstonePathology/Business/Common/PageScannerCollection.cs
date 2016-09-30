using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Windows;


namespace YellowstonePathology.Business.Common
{
	public class PageScannerCollection : ObservableCollection<PageScanner>
	{

		public PageScannerCollection()
		{
			PageScannerGT2500 pageScannerGT2500 = new PageScannerGT2500();
			this.Add(pageScannerGT2500);

			PageScannerIS200e pageScannerIS200e = new PageScannerIS200e();
			this.Add(pageScannerIS200e);

			PageScannerXeroxDocuMate3640 pageScannerXeroxDocuMate3640 = new PageScannerXeroxDocuMate3640();
			this.Add(pageScannerXeroxDocuMate3640);

            PageScannerFI7160 pageScannerFI7160 = new PageScannerFI7160();
            this.Add(pageScannerFI7160);
		}

		public PageScannerCollection(YellowstonePathology.Business.Twain.Twain twain)
		{
			List<string> printerNames = twain.SourceNames.ToList();
			PageScannerGT2500 pageScannerGT2500 = new PageScannerGT2500();
			if(printerNames.Contains(pageScannerGT2500.ScannerName))
			{
				this.Add(pageScannerGT2500);
			}
			PageScannerIS200e pageScannerIS200e = new PageScannerIS200e();
			if (printerNames.Contains(pageScannerIS200e.ScannerName))
			{
				this.Add(pageScannerIS200e);
			}
			PageScannerXeroxDocuMate3640 pageScannerXeroxDocuMate3640 = new PageScannerXeroxDocuMate3640();
			if (printerNames.Contains(pageScannerXeroxDocuMate3640.ScannerName))
			{
				this.Add(pageScannerXeroxDocuMate3640);
			}
            PageScannerFI7160 pageScannerFI7160 = new PageScannerFI7160();
            if (printerNames.Contains(pageScannerFI7160.ScannerName))
            {
                this.Add(pageScannerFI7160);
            }
		}

		public PageScanner SelectedPageScanner
		{
			get
			{
				PageScanner result = null;
				string scannerName = YellowstonePathology.Business.User.UserPreferenceInstance.Instance.UserPreference.PageScanner;
				foreach (PageScanner pageScanner in this)
				{
					if (pageScanner.ScannerName == scannerName)
					{
						result = pageScanner;
						break;
					}
				}
				return result;
			}
		}
	}
}
