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
using System.Diagnostics;
using System.Net.Http;
using System.IO;

namespace YellowstonePathology.UI.Gross
{
    public partial class DictationTemplatePage : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string m_GrossDescription;                

        private YellowstonePathology.Business.Specimen.Model.SpecimenOrder m_SpecimenOrder;
        private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
        private YellowstonePathology.Business.User.SystemIdentity m_SystemIdentity;
        private DictationTemplate m_DictationTemplate;
        private string m_DictationMode;
        
        private string m_FootPedalInput = "None";        

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

        public string FootPedalInput
        {
            get { return this.m_FootPedalInput; }
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
            if(this.m_DictationMode == "Dictation Mode: Express Dictate")
            {
                this.KillExpressDictate();
                this.m_DictationMode = "Dication Mode: LIS";
            }
            else
            {
                this.StartExpressDictate();
                this.m_DictationMode = "Dictation Mode: Express Dictate";
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
            string fileName = @"C:\Program Files (x86)\NCH Software\Express\epress.exe";
            if (System.IO.File.Exists(@"C:\Program Files (x86)\NCH Software\Express\epress.exe") == true)
            {
                System.Diagnostics.Process process = new System.Diagnostics.Process();
                process.StartInfo.FileName = fileName;
                process.StartInfo.CreateNoWindow = true;
                process.Start();
            }
            else
            {
                MessageBox.Show("Express Dictate not found.");
            }
        }

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }                
    }
}
