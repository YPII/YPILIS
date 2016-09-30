using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using System.Threading;
using Microsoft.Win32;

namespace YellowstonePathology.Business.BarcodeScanning
{	
	public class BarcodeScanPort
	{                
        public event HistologyBlockScanReceivedHandler HistologyBlockScanReceived;
		public delegate void HistologyBlockScanReceivedHandler(Barcode barcode);

        public event SecurityBadgeScanReceivedHandler SecurityBadgeScanReceived;
		public delegate void SecurityBadgeScanReceivedHandler(Barcode barcode);

        public event CytologySlideScanReceivedHandler CytologySlideScanReceived;
		public delegate void CytologySlideScanReceivedHandler(CytycBarcode barcode);

        public event HistologySlideScanReceivedHandler HistologySlideScanReceived;
		public delegate void HistologySlideScanReceivedHandler(Barcode barcode);

        public event ThinPrepSlideScanReceivedHandler ThinPrepSlideScanReceived;
		public delegate void ThinPrepSlideScanReceivedHandler(Barcode barcode);         

        public event ContainerScanReceivedHandler ContainerScanReceived;
        public delegate void ContainerScanReceivedHandler(ContainerBarcode containerBarcode);

        public event SvhAccountNoReceiveHandler SvhAccountNoReceived;
        public delegate void SvhAccountNoReceiveHandler(string svhAccountNo);

        public event SvhMedicalRecordNoReceiveHandler SvhMedicalRecordNoReceived;
        public delegate void SvhMedicalRecordNoReceiveHandler(string svhMedicalRecordNo);		

		public event ClientScanReceivedHandler ClientScanReceived;
		public delegate void ClientScanReceivedHandler(Barcode barcode);

        public event USPostalServiceCertifiedMailReceivedHandler USPostalServiceCertifiedMailReceived;
        public delegate void USPostalServiceCertifiedMailReceivedHandler(string scanData);

        public event FedexOvernightScanReceivedHandler FedexOvernightScanReceived;
        public delegate void FedexOvernightScanReceivedHandler(string scanData);

        public event AliquotOrderIdReceivedHandler AliquotOrderIdReceived;
        public delegate void AliquotOrderIdReceivedHandler(string scanData);

		private static BarcodeScanPort m_Instance;				
		private SerialPort m_SerialPort;		
 
		private BarcodeScanPort()
		{            
            this.m_SerialPort = new SerialPort();
            this.m_SerialPort.PortName = YellowstonePathology.Business.User.UserPreferenceInstance.Instance.UserPreference.BarcodeScanPort;
            this.m_SerialPort.NewLine = "\n";
            this.m_SerialPort.DataReceived += new SerialDataReceivedEventHandler(SerialPort_DataReceived);
            this.m_SerialPort.ReadTimeout = 500;
            this.m_SerialPort.DtrEnable = true;
            this.Start();            
		}        

		public static BarcodeScanPort Instance
		{
            get
            {
                if (m_Instance == null)
                {
                    m_Instance = new BarcodeScanPort();
                }
                return m_Instance;
            }
		}        
        
		private void Start()
		{
            try
            {
                this.m_SerialPort.Open();
            }
            catch { }            
		}        
        
		private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
		{                        
            string scanData = this.m_SerialPort.ReadExisting();
            string[] scans = scanData.Trim().Split('\r');

            foreach (string scan in scans)
            {
                BarcodeScanVersionEnum version = BarcodeScan.GetVersion(scan);

                switch (version)
                {
                    case BarcodeScanVersionEnum.V1:
                        this.HandleVersion1Scans(scan.Trim());
                        break;
                    case BarcodeScanVersionEnum.V2:
                        this.HandleVersion2Scans(scan.Trim());
                        break;
                }
            }                        
		}

        private void HandleVersion2Scans(string scanData)
        {
            BarcodeVersion2 barcode = new BarcodeVersion2(scanData);            

            switch (barcode.Prefix)
            {
                case BarcodePrefixEnum.PSLD:                    
                    if (ThinPrepSlideScanReceived != null) ThinPrepSlideScanReceived(barcode);
                    break;
                case BarcodePrefixEnum.HSLD:

                    if (HistologySlideScanReceived != null) HistologySlideScanReceived(barcode);
                    break;                
            }
        }

        private void HandleVersion1Scans(string scanData)
        {
            BarcodeVersion1 barcode = new BarcodeVersion1(scanData);
            switch (barcode.Prefix)
            {
                case BarcodePrefixEnum.HBLK:
                case BarcodePrefixEnum.ALQ:                    
                    if (HistologyBlockScanReceived != null) HistologyBlockScanReceived(barcode);
                    break;
                case BarcodePrefixEnum.SPN:
                case BarcodePrefixEnum.HSLD:
                case BarcodePrefixEnum.SLD:                    
                    if (HistologySlideScanReceived != null) HistologySlideScanReceived(barcode);
                    break;
                case BarcodePrefixEnum.SBDG:
                case BarcodePrefixEnum.BDG:
                case BarcodePrefixEnum.YSU:                                        
                    if (SecurityBadgeScanReceived != null) SecurityBadgeScanReceived(barcode);
                    break;
                case BarcodePrefixEnum.CTNR:
                    ContainerBarcode containerBarCode = new ContainerBarcode(scanData);
                    if (ContainerScanReceived != null) ContainerScanReceived(containerBarCode);
                    break;
                case BarcodePrefixEnum.CLNT:                    
                    if (ClientScanReceived != null) ClientScanReceived(barcode);
                    break;
                case BarcodePrefixEnum.UNDEFINED:
                    this.HandleSpecialScans(scanData);
                    break;
            }    
        }        

        private void HandleSpecialScans(string scanData)
        {                        
            if (scanData.Contains(",") == true)
            {
                CytycBarcode cytycBarcode = new CytycBarcode(scanData);
                if (CytologySlideScanReceived != null) this.CytologySlideScanReceived(cytycBarcode);
            }
            else if (scanData.StartsWith("00") == true || scanData.StartsWith("12") == true) // SVH Medical Record Number
            {                
                if (SvhMedicalRecordNoReceived != null) this.SvhMedicalRecordNoReceived(scanData);                
            }			
			else if (scanData.StartsWith("700") == true) // SVH Account Number
			{                
                if (SvhAccountNoReceived != null) this.SvhAccountNoReceived(scanData);                				
			}
            else if (scanData.Trim().Length == 20 && scanData.StartsWith("701")) // US Postal Service Certified Mail
            {
                if (USPostalServiceCertifiedMailReceived != null) this.USPostalServiceCertifiedMailReceived(scanData);
            }
            else if(scanData.Trim().Length == 34 && scanData.StartsWith("100"))
            {
                if (FedexOvernightScanReceived != null) this.FedexOvernightScanReceived(scanData);
            }
            else
            {
                YellowstonePathology.Business.OrderIdParser orderIdParser = new OrderIdParser(scanData);
                if (orderIdParser.IsValidAliquotOrderId == true)
                {
                    if (AliquotOrderIdReceived != null) this.AliquotOrderIdReceived(scanData);
                }
            }
        }  
        
        public int GetContainerScanReceivedInvocationCount()
        {
            return this.ContainerScanReceived.GetInvocationList().Length;
        }      

        public string GetContainerScanReceivedTargetString()
        {
            Delegate[] delegateList = this.ContainerScanReceived.GetInvocationList();
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < delegateList.Length; i++)
            {
                result.AppendLine(delegateList[i].Target.ToString());
            }
            return result.ToString();
        }
	}	
}
