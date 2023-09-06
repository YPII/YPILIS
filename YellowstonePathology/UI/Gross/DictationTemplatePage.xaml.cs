using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Threading;
using HidSharp.Experimental;
using HidSharp.Reports;
using HidSharp.Reports.Encodings;
using HidSharp.Utility;
using HidSharp;
using HidSharp.Reports.Input;
using System.Diagnostics;
using System.Net.Http;
using System.IO;

namespace YellowstonePathology.UI.Gross
{
    public partial class DictationTemplatePage : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string m_GrossDescription;

        private HidDeviceInputReceiver m_InputReceiver;
        private DeviceItemInputParser m_DevideInputParser;
        private byte[] m_InputReportBuffer;
        private HidStream m_HidStream;

        private YellowstonePathology.Business.Specimen.Model.SpecimenOrder m_SpecimenOrder;
        private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
        private YellowstonePathology.Business.User.SystemIdentity m_SystemIdentity;
        private DictationTemplate m_DictationTemplate;
        private string m_DictationMode;

        private int m_VendorId = 0x05F3;
        private int m_ProductId = 0x00FF;
        private string m_FootPedal = "None";

        public DictationTemplatePage(Business.Specimen.Model.SpecimenOrder specimenOrder, Business.Test.AccessionOrder accessionOrder, YellowstonePathology.Business.User.SystemIdentity systemIdentity)
        {
            this.m_DictationMode = "Dication Mode: Express Dictate";
            this.m_SpecimenOrder = specimenOrder;
            this.m_AccessionOrder = accessionOrder;
            this.m_SystemIdentity = systemIdentity;

            this.m_DictationTemplate = DictationTemplateCollection.Instance.GetClone(m_SpecimenOrder);
            this.SetGrossDescription();

            InitializeComponent();

            DataContext = this;
            this.SetupFootPedal();
        }

        public void SetGrossDescription()
        {
            if(string.IsNullOrEmpty(this.m_DictationTemplate.Text) == false)
            {
                this.m_GrossDescription = this.m_DictationTemplate.BuildResultText(this.m_SpecimenOrder, this.m_AccessionOrder, this.m_SystemIdentity);
            }            
        }

        public string DictationMode
        {
            get { return this.m_DictationMode; }
        }

        public string GrossDescription
        {
            get { return this.m_GrossDescription; }
            set { this.m_GrossDescription = value; }
        }

        public YellowstonePathology.Business.Specimen.Model.SpecimenOrder SpecimenOrder
        {
            get { return this.m_SpecimenOrder; }
        }

        public DictationTemplate DictationTemplate
        {
            get { return this.m_DictationTemplate; }
        }        

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ButtonCreateParagraph_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void ToggleButtonDictationMode_Checked(object sender, RoutedEventArgs e)
        {
            this.ChangeDictationMode();
        }

        private void ToggleButtonDictationMode_Unchecked(object sender, RoutedEventArgs e)
        {
            this.ChangeDictationMode();
        }

        private void ChangeDictationMode()
        {
            if(this.m_DictationMode == "Dication Mode: Express Dictate")
            {
                this.KillExpressDictate();
                this.m_DictationMode = "Dication Mode: LIS";
            }
            else
            {
                this.m_DictationMode = "Dication Mode: Express Dicatate";
            }

            this.NotifyPropertyChanged(string.Empty);
        }

        private void KillExpressDictate()
        {
            System.Diagnostics.Process[] workers = System.Diagnostics.Process.GetProcessesByName("Express");
            foreach (System.Diagnostics.Process worker in workers)
            {
                worker.Kill();
                worker.WaitForExit();
                worker.Dispose();
            }
        }

        private void StartExpressDictate()
        {
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            process.StartInfo.FileName = @"C:\Program Files (x86)\NCH Software\Express\epress.exe";
            process.StartInfo.CreateNoWindow = true;            
            process.Start();
        }

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        public void SetupFootPedal()
        {
            var list = DeviceList.Local;
            HidDevice vecFootPedal = null;
            list.TryGetHidDevice(out vecFootPedal, this.m_VendorId, this.m_ProductId);
            if (vecFootPedal == null) return;
            var reportDescriptor = vecFootPedal.GetReportDescriptor();

            if (vecFootPedal.TryOpen(out this.m_HidStream))
            {
                Debug.WriteLine("Opened device.");
                this.m_HidStream.ReadTimeout = Timeout.Infinite;

                this.m_InputReportBuffer = new byte[vecFootPedal.GetMaxInputReportLength()];
                this.m_InputReceiver = reportDescriptor.CreateHidDeviceInputReceiver();
                this.m_DevideInputParser = reportDescriptor.DeviceItems[0].CreateDeviceItemInputParser();

                this.m_InputReceiver.Received += (sender, e) =>
                {
                    Report report;
                    while (this.m_InputReceiver.TryRead(this.m_InputReportBuffer, 0, out report))
                    {
                        // Parse the report if possible.
                        // This will return false if (for example) the report applies to a different DeviceItem.
                        if (this.m_DevideInputParser.TryParseReport(this.m_InputReportBuffer, 0, report))
                        {
                            // If you are using Windows Forms, you could call BeginInvoke here to marshal the results
                            // to your main thread.
                            WriteDeviceItemInputParserResult(this.m_DevideInputParser);
                        }
                    }
                };
                this.m_InputReceiver.Start(this.m_HidStream);
            }
            else
            {
                Debug.WriteLine("Failed to open device.");
            }
        }

        private void WriteDeviceItemInputParserResult(HidSharp.Reports.Input.DeviceItemInputParser parser)
        {
            while (parser.HasChanged)
            {
                int changedIndex = parser.GetNextChangedIndex();
                var previousDataValue = parser.GetPreviousValue(changedIndex);
                var dataValue = parser.GetValue(changedIndex);
                string result = string.Format("  {0}: {1} -> {2}", (Usage)dataValue.Usages.FirstOrDefault(), previousDataValue.GetPhysicalValue(), dataValue.GetPhysicalValue());
                this.m_FootPedal = result;
                this.NotifyPropertyChanged(string.Empty);
                Debug.WriteLine(result);
            }
        }
    }
}
