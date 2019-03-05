using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.ComponentModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace YellowstonePathology.UI.Billing
{    
    public partial class SimulationDialog : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private const string BaseSerializeFolder = @"D:\BillingSimulation\";

        private SimulationList m_SimulationList;
        private DateTime m_FinalDate;
        private Business.Billing.Model.InsuranceMapCollection m_InsuranceMapCollection;

        public SimulationDialog()
        {
            this.m_InsuranceMapCollection = Business.Billing.Model.InsuranceMapCollection.GetCollection();
            this.m_FinalDate = DateTime.Today;
            this.GetList();

            InitializeComponent();
            this.DataContext = this;
        }

        public void GetList()
        {
            this.m_SimulationList = SimulationList.GetList(this.m_FinalDate);
            this.HandlePrimaryInsuranceSimulation();
            this.m_SimulationList.HandlePatientTypeSimulation();
            this.NotifyPropertyChanged("SimulationList");
        }

        public DateTime FinalDate
        {
            get { return this.m_FinalDate; }
            set
            {
                if(this.m_FinalDate != value)
                {
                    this.m_FinalDate = value;
                    this.NotifyPropertyChanged("FinalDate");
                }                
            }
        }

        public SimulationList SimulationList
        {
            get { return this.m_SimulationList; }
        }        

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }        
                
        private void HandlePrimaryInsuranceSimulation()
        {
            foreach (SimulationListItem item in this.m_SimulationList)
            {
                Business.HL7View.ADTMessages adtMessages = new Business.HL7View.ADTMessages();
                adtMessages = Business.Gateway.AccessionOrderGateway.GetADTMessages(item.MedicalRecord);
                item.PrimaryInsuranceADT = adtMessages.GetPrimaryInsuranceV2();

                if(this.m_InsuranceMapCollection.Exists(item.PrimaryInsuranceADT) == false)
                {
                    item.PrimaryInsuranceSim = "Not Mapped";
                }
                else
                {
                    Business.Billing.Model.InsuranceMap insuranceMap = this.m_InsuranceMapCollection.GetMap(item.PrimaryInsuranceADT);
                    item.PrimaryInsuranceSim = insuranceMap.MapsTo;
                }
            }
        }

        private void UpdateMapping()
        {
            foreach (SimulationListItem item in this.m_SimulationList)
            {
                if (this.m_InsuranceMapCollection.Exists(item.PrimaryInsuranceADT) == false)
                {
                    item.PrimaryInsuranceSim = "Not Mapped";
                }
                else
                {
                    Business.Billing.Model.InsuranceMap insuranceMap = this.m_InsuranceMapCollection.GetMap(item.PrimaryInsuranceADT);
                    item.PrimaryInsuranceSim = insuranceMap.MapsTo;
                }
            }
        }

        private void MenuItemMapToMedicare_Click(object sender, RoutedEventArgs e)
        {
            SimulationListItem item = (SimulationListItem)this.ListViewSimulationList.SelectedItem;
            this.m_InsuranceMapCollection.TryMap(item.PrimaryInsuranceADT, "Medicare");
            this.UpdateMapping();           
        }

        private void MenuItemMapToMedicaid_Click(object sender, RoutedEventArgs e)
        {
            SimulationListItem item = (SimulationListItem)this.ListViewSimulationList.SelectedItem;
            this.m_InsuranceMapCollection.TryMap(item.PrimaryInsuranceADT, "Medicaid");
            this.UpdateMapping();
        }

        private void MenuItemMapToGovernmental_Click(object sender, RoutedEventArgs e)
        {
            SimulationListItem item = (SimulationListItem)this.ListViewSimulationList.SelectedItem;
            this.m_InsuranceMapCollection.TryMap(item.PrimaryInsuranceADT, "Governmental");
            this.UpdateMapping();
        }

        private void MenuItemMapToCommercial_Click(object sender, RoutedEventArgs e)
        {
            SimulationListItem item = (SimulationListItem)this.ListViewSimulationList.SelectedItem;
            this.m_InsuranceMapCollection.TryMap(item.PrimaryInsuranceADT, "Commercial");
            this.UpdateMapping();
        }

        private void MenuItemUnmap_Click(object sender, RoutedEventArgs e)
        {
            SimulationListItem item = (SimulationListItem)this.ListViewSimulationList.SelectedItem;
            Business.Billing.Model.InsuranceMap insuranceMap = this.m_InsuranceMapCollection.GetMap(item.PrimaryInsuranceADT);
            this.m_InsuranceMapCollection.UnMap(insuranceMap);
            this.UpdateMapping();
        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            this.m_FinalDate = this.m_FinalDate.AddDays(-1);
            this.GetList();
            this.NotifyPropertyChanged("FinalDate");
        }

        private void ButtonForward_Click(object sender, RoutedEventArgs e)
        {
            this.m_FinalDate = this.m_FinalDate.AddDays(1);
            this.GetList();
            this.NotifyPropertyChanged("FinalDate");
        }

        private void MenuItemSimulateSetPost_Click(object sender, RoutedEventArgs e)
        {
            
        }

        public string HandleSerializeFolderFileName(DateTime finalDate, string reportNo)
        {
            string folderName = BaseSerializeFolder + finalDate.ToString("MMddyyy");
            if(System.IO.Directory.Exists(folderName) == false)
            {
                System.IO.Directory.CreateDirectory(folderName);
            }
            return folderName + "\\" + reportNo + ".json";
        }

        public void Serialize(Business.Test.AccessionOrder ao, string reportNo, string fileName)
        {
            JsonSerializer serializer = new JsonSerializer();
            serializer.Converters.Add(new Newtonsoft.Json.Converters.JavaScriptDateTimeConverter());
            serializer.NullValueHandling = NullValueHandling.Ignore;

            using (System.IO.StreamWriter sw = new System.IO.StreamWriter(fileName))
            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                Business.Test.PanelSetOrder pso = ao.PanelSetOrderCollection.GetPanelSetOrder(reportNo);
                serializer.Serialize(writer, pso.PanelSetOrderCPTCodeBillCollection);
            }
        }

        private void ButtonWriteCodesToFile_Click(object sender, RoutedEventArgs e)
        {
            foreach (SimulationListItem item in this.ListViewSimulationList.Items)
            {
                Business.Test.AccessionOrder ao = Business.Persistence.DocumentGateway.Instance.GetAccessionOrderByMasterAccessionNo(item.MasterAccessionNo);

                YellowstonePathology.Business.Billing.Model.BillableObject billableObject = Business.Billing.Model.BillableObjectFactory.GetBillableObject(ao, item.ReportNo);
                YellowstonePathology.Business.Rules.MethodResult setResult = billableObject.Set();
                if (setResult.Success == false)
                {
                    MessageBox.Show(setResult.Message);
                }
                else
                {
                    YellowstonePathology.Business.Rules.MethodResult postResult = billableObject.Post();
                    if (postResult.Success == false)
                    {
                        MessageBox.Show(postResult.Message);
                    }
                    string fileName = HandleSerializeFolderFileName(this.m_FinalDate, item.ReportNo);
                    Business.Billing.Model.SimulationSerializer.Serialize(ao, item.ReportNo, fileName);
                }
            }
            MessageBox.Show("Completed writing CPT Billing Codes");
        }
    }
}
