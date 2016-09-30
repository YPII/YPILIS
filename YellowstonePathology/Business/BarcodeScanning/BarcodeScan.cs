using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.ComponentModel;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.BarcodeScanning
{	
	public class BarcodeScan
	{        
        public static BarcodeScanVersionEnum GetVersion(string scanData)
        {
            BarcodeScanVersionEnum result = BarcodeScanVersionEnum.V2;                        
            if (scanData.Substring(0, 1) != "^")
            {
                result = BarcodeScanVersionEnum.V1;
            }
            return result;
        }        
	}
}
