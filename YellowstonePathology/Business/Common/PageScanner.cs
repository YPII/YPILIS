using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Common
{
	public class PageScanner
	{
		private YellowstonePathology.Business.Twain.ScanSettings m_ScanSettings;
		private string m_ScannerName;

		public PageScanner(string scannerName)
		{
			this.m_ScannerName = scannerName;
			this.m_ScanSettings = new Business.Twain.ScanSettings();
			this.SetScanSettings();
		}

		public YellowstonePathology.Business.Twain.ScanSettings ScanSettings
		{
			get { return this.m_ScanSettings; }
		}

		public string ScannerName
		{
			get { return this.m_ScannerName; }
		}

		protected virtual void SetScanSettings()
		{
			this.m_ScanSettings.UseDocumentFeeder = true;
		}
	}
}
