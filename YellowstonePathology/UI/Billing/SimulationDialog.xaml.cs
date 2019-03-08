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
            this.m_SimulationList = SimulationList.GetList(this.m_FinalDate);

            InitializeComponent();
            this.DataContext = this;
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
            this.m_SimulationList = SimulationList.GetList(this.m_FinalDate);
            this.NotifyPropertyChanged("SimulationList");
            this.NotifyPropertyChanged("FinalDate");
        }

        private void ButtonForward_Click(object sender, RoutedEventArgs e)
        {
            this.m_FinalDate = this.m_FinalDate.AddDays(1);
            this.m_SimulationList = SimulationList.GetList(this.m_FinalDate);
            this.NotifyPropertyChanged("SimulationList");
            this.NotifyPropertyChanged("FinalDate");
        }        

        public void HandleMakeFolders()
        {            
            if (System.IO.Directory.Exists(this.GetDateFolderName()) == false)
            {
                System.IO.Directory.CreateDirectory(this.GetDateFolderName());
            }            
            if (System.IO.Directory.Exists(this.GetCPTCodeFolderName()) == false)
            {
                string x = this.GetCPTCodeFolderName();
                System.IO.Directory.CreateDirectory(this.GetCPTCodeFolderName());
            }
            if (System.IO.Directory.Exists(this.GetCPTCodeBillFolderName()) == false)
            {
                System.IO.Directory.CreateDirectory(this.GetCPTCodeBillFolderName());
            }            
        }

        public string GetDateFolderName()
        {
            return BaseSerializeFolder + this.m_FinalDate.ToString("MMddyyy") + "\\";
        }

        public string GetCPTCodeFolderName()
        {
            return this.GetDateFolderName() + "cpt-code\\";
        }

        public string GetCPTCodeBillFolderName()
        {
            return this.GetDateFolderName() + "cpt-code-bill\\";
        }

        public bool SetAndPost(Business.Test.AccessionOrder ao, string reportNo)
        {
            bool result = true;
            YellowstonePathology.Business.Billing.Model.BillableObject billableObject = Business.Billing.Model.BillableObjectFactory.GetBillableObject(ao, reportNo);
            YellowstonePathology.Business.Rules.MethodResult setResult = billableObject.Set();
            if (setResult.Success == false)
            {
                Console.WriteLine(setResult.Message);
                result = false;
            }
            else
            {
                YellowstonePathology.Business.Rules.MethodResult postResult = billableObject.Post();
                if (postResult.Success == false)
                {
                    Console.WriteLine(postResult.Message);
                    result = false;
                }                
            }
            return result;
        }

        public void SerializeCPTCodeBillSimulation()
        {
            foreach (SimulationListItem item in this.ListViewSimulationList.Items)
            {
                Business.Test.AccessionOrder ao = Business.Persistence.DocumentGateway.Instance.GetAccessionOrderByMasterAccessionNo(item.MasterAccessionNo);
                ao.PrimaryInsurance = item.GetAdjustedPrimaryInsurance();
                ao.PatientType = item.PatientTypeSim;

                this.SetAndPost(ao, item.ReportNo);
                string fileName = this.GetCPTCodeBillFolderName() + item.ReportNo + ".json";
                Business.Billing.Model.SimulationSerializer.SerializePanelSetOrderCPTCodeBill(ao, item.ReportNo, fileName);                
            }            
        }       
        
        private void SerializeCPTCodeManual()
        {
            foreach (SimulationListItem item in this.ListViewSimulationList.Items)
            {
                Business.Test.AccessionOrder ao = Business.Persistence.DocumentGateway.Instance.GetAccessionOrderByMasterAccessionNo(item.MasterAccessionNo);
                this.SetAndPost(ao, item.ReportNo);
                string fileName = this.GetCPTCodeFolderName() + item.ReportNo + ".json";
                Business.Billing.Model.SimulationSerializer.SerializePanelSetOrderCPTCode(ao, item.ReportNo, fileName);
            }
        }              

        private void ButtonSerialize_Click(object sender, RoutedEventArgs e)
        {
            this.HandleMakeFolders();
            this.SerializeCPTCodeBillSimulation();
            this.SerializeCPTCodeManual();
            MessageBox.Show("Serialization is complete.");
        }

        private void ButtonDeserialize_Click(object sender, RoutedEventArgs e)
        {
            string folderName = this.GetCPTCodeBillFolderName();
            string[] files = System.IO.Directory.GetFiles(folderName);

            Business.Test.PanelSetOrderCPTCodeBillCollection panelSetOrderCPTCodeBillCollection = Business.Billing.Model.SimulationSerializer.Deserialize();
        }

        private void ButtonInsuranceNotMatched_Click(object sender, RoutedEventArgs e)
        {            
            this.m_SimulationList.SetInsuranceBackgrounColor();
            this.NotifyPropertyChanged("SimulationList");
        }

        private void ButtonRun_Click(object sender, RoutedEventArgs e)
        {
            this.HandlePrimaryInsuranceSimulation();
            this.m_SimulationList.HandlePatientTypeSimulation();
            this.NotifyPropertyChanged("SimulationList");
        }
    }
}
