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
            PageScannerFI8170 pageScannerFI8160 = new PageScannerFI8170();
            this.Add(pageScannerFI8160);

            PageScannerFI7160 pageScannerFI7160 = new PageScannerFI7160();
            this.Add(pageScannerFI7160);
		}

		public PageScannerCollection(YellowstonePathology.Business.Twain.Twain twain)
		{
			List<string> printerNames = twain.SourceNames.ToList();
			
            PageScannerFI7160 pageScannerFI7160 = new PageScannerFI7160();
            if (printerNames.Contains(pageScannerFI7160.ScannerName))
            {
                this.Add(pageScannerFI7160);
            }

            PageScannerFI8170 pageScannerFI8170 = new PageScannerFI8170();
            if (printerNames.Contains(pageScannerFI8170.ScannerName))
            {
                this.Add(pageScannerFI8170);
            }
        }

		public PageScanner SelectedPageScanner
		{
			get
			{
				PageScanner result = null;
				string scannerName = Business.User.UserPreferenceInstance.Instance.UserPreference.PageScanner;
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
