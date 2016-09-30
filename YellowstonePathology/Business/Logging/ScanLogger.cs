using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Logging
{
    public class ScanLogger
    {
		YellowstonePathology.Business.BarcodeScanning.BarcodeScanPort m_BarcodeScanPort;

		private YellowstonePathology.Business.User.SystemIdentity m_SystemIdentity;
        //private string m_ScannerPortName;

		public ScanLogger(YellowstonePathology.Business.User.SystemIdentity systemIdentity)
        {
            this.m_SystemIdentity = systemIdentity;
			this.m_BarcodeScanPort = YellowstonePathology.Business.BarcodeScanning.BarcodeScanPort.Instance;
        }

        public void Start()
        {
            //this.m_BarcodeScanPort.BarcodeScanReceived += new YellowstonePathology.Business.BarcodeScanning.BarcodeScanPort.BarcodeScanReceivedHandler(BarcodeScanPort_BarcodeScanReceived);
        }

        public void Stop()
        {
            ///this.m_BarcodeScanPort.BarcodeScanReceived -= BarcodeScanPort_BarcodeScanReceived;
        }

		private void BarcodeScanPort_BarcodeScanReceived(YellowstonePathology.Business.BarcodeScanning.BarcodeScan barcodeScan)
        {
            //barcodeScan.ScannedById = this.m_SystemIdentity.User.UserId;
            //barcodeScan.ScanStationName = this.m_SystemIdentity.StationName;

			//YellowstonePathology.Business.Persistence.ObjectTracker objectTracker = new Persistence.ObjectTracker();
			//objectTracker.RegisterRootInsert(barcodeScan);
			//objectTracker.SubmitChanges(barcodeScan);
        }
    }
}
