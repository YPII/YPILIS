using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HidSharp.Experimental;
using HidSharp.Reports;
using HidSharp.Reports.Encodings;
using HidSharp.Utility;
using HidSharp;
using HidSharp.Reports.Input;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace YellowstonePathology.UI
{
    public class FootPedal
    {
        private static FootPedal instance;

        private byte[] m_InputReportBuffer;
        private HidStream m_HidStream;
        private HidDeviceInputReceiver m_InputReceiver;
        private DeviceItemInputParser m_DevideInputParser;

        private string m_Input;

        public static FootPedal Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = Initialize();
                }
                return instance;
            }
        }

        public string Input
        {
            get { return this.m_Input; }
        }

        private static FootPedal Initialize()
        {            
            Console.WriteLine("Initializing the footpedal");
            FootPedal instance = new FootPedal();

            var list = DeviceList.Local;
            HidDevice vecFootPedal = null;
            list.TryGetHidDevice(out vecFootPedal, Business.User.UserPreferenceInstance.Instance.UserPreference.FootPedalVendorId,
                Business.User.UserPreferenceInstance.Instance.UserPreference.FootPedalProductId);
            if (vecFootPedal == null) return instance;
            var reportDescriptor = vecFootPedal.GetReportDescriptor();

            if (vecFootPedal.TryOpen(out instance.m_HidStream))
            {                
                instance.m_HidStream.ReadTimeout = Timeout.Infinite;

                instance.m_InputReportBuffer = new byte[vecFootPedal.GetMaxInputReportLength()];
                instance.m_InputReceiver = reportDescriptor.CreateHidDeviceInputReceiver();
                instance.m_DevideInputParser = reportDescriptor.DeviceItems[0].CreateDeviceItemInputParser();

                instance.m_InputReceiver.Received += (sender, e) =>
                {
                    Report report;
                    while (instance.m_InputReceiver.TryRead(instance.m_InputReportBuffer, 0, out report))
                    {
                        // Parse the report if possible.
                        // This will return false if (for example) the report applies to a different DeviceItem.
                        if (instance.m_DevideInputParser.TryParseReport(instance.m_InputReportBuffer, 0, report))
                        {
                            // If you are using Windows Forms, you could call BeginInvoke here to marshal the results
                            // to your main thread.
                            instance.WriteDeviceItemInputParserResult();
                        }
                    }
                };
                instance.m_InputReceiver.Start(instance.m_HidStream);
            }
            else
            {
                Console.WriteLine("Failed to open device.");
            }

            return instance;
        }

        private void WriteDeviceItemInputParserResult()
        {
            while (instance.m_DevideInputParser.HasChanged)
            {
                int changedIndex = instance.m_DevideInputParser.GetNextChangedIndex();
                var previousDataValue = instance.m_DevideInputParser.GetPreviousValue(changedIndex);
                var dataValue = instance.m_DevideInputParser.GetValue(changedIndex);
                string result = string.Format("  {0}: {1} -> {2}", (Usage)dataValue.Usages.FirstOrDefault(), previousDataValue.GetPhysicalValue(), dataValue.GetPhysicalValue());
                this.m_Input = result;                
                Console.WriteLine(result);
            }
        }
    }
}
