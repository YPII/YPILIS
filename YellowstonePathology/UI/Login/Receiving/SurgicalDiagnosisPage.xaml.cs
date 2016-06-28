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
using System.Xml;
using System.Xml.Linq;

namespace YellowstonePathology.UI.Login.Receiving
{
    /// <summary>
    /// 
    /// </summary>
    public partial class SurgicalDiagnosisPage : UserControl, INotifyPropertyChanged 
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public delegate void ReturnEventHandler(object sender, UI.Navigation.PageNavigationReturnEventArgs e);
        public event ReturnEventHandler Return;
                
        YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
        YellowstonePathology.Business.View.SpecimenSurgicalDiagnosisViewCollection m_SpecimenSurgicalDiagnosisViewCollection;

        public SurgicalDiagnosisPage(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {
            this.m_AccessionOrder = accessionOrder;
            this.m_SpecimenSurgicalDiagnosisViewCollection = new Business.View.SpecimenSurgicalDiagnosisViewCollection(this.m_AccessionOrder);            
                        
            InitializeComponent();
            this.DataContext = this;

            Loaded += SurgicalDiagnosisPage_Loaded;
            Unloaded += SurgicalDiagnosisPage_Unloaded;            
        }

        private void SurgicalDiagnosisPage_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void SurgicalDiagnosisPage_Unloaded(object sender, RoutedEventArgs e)
        {
            
        }

        public YellowstonePathology.Business.Test.AccessionOrder AccessionOrder
        {
            get { return this.m_AccessionOrder; }
            set { this.m_AccessionOrder = value; }
        }        

        public YellowstonePathology.Business.View.SpecimenSurgicalDiagnosisViewCollection SpecimenSurgicalDiagnosisViewCollection
        {
            get { return this.m_SpecimenSurgicalDiagnosisViewCollection; }
        }        

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }                

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            UI.Navigation.PageNavigationReturnEventArgs args = new UI.Navigation.PageNavigationReturnEventArgs(UI.Navigation.PageNavigationDirectionEnum.Back, null);
            this.Return(this, args);
        }

        private void ButtonFinish_Click(object sender, RoutedEventArgs e)
        {
            UI.Navigation.PageNavigationReturnEventArgs args = new UI.Navigation.PageNavigationReturnEventArgs(UI.Navigation.PageNavigationDirectionEnum.Finish, null);
            this.Return(this, args);
        }

        private void ButtonRemoveSurgicalDiagnosis_Click(object sender, RoutedEventArgs e)
        {
            if (this.ListViewSpecimenSurgicalDiagnosis.SelectedItem != null)
            {
                YellowstonePathology.Business.View.SpecimenSurgicalDiagnosisView specimenSurgicalDiagnosisView = (YellowstonePathology.Business.View.SpecimenSurgicalDiagnosisView)this.ListViewSpecimenSurgicalDiagnosis.SelectedItem;
                if (specimenSurgicalDiagnosisView.SurgicalDiagnosisIsOrdered == true)
                {
                    if (string.IsNullOrEmpty(specimenSurgicalDiagnosisView.SurgicalSpecimen.Diagnosis) == true)
                    {
						YellowstonePathology.Business.Test.Surgical.SurgicalTestOrder panelSetOrderSurgical = this.m_AccessionOrder.PanelSetOrderCollection.GetSurgical();
						panelSetOrderSurgical.SurgicalSpecimenCollection.Remove(specimenSurgicalDiagnosisView.SurgicalSpecimen);
                        this.m_SpecimenSurgicalDiagnosisViewCollection.Refresh(this.m_AccessionOrder);
                        this.NotifyPropertyChanged("SpecimenSurgicalDiagnosisViewCollection");
                        MessageBox.Show("The surgical diagnosis has been removed.");
                    }
                    else
                    {
                        MessageBox.Show("The diagnosis cannot be removed because it has text in it.");
                    }
                }
                else
                {
                    MessageBox.Show("A surgical diagnosis is not currently ordered.");
                }
            }                        
        }

        private void ButtonAddSurgicalDiagnosis_Click(object sender, RoutedEventArgs e)
        {
            if (this.ListViewSpecimenSurgicalDiagnosis.SelectedItem != null)
            {
                YellowstonePathology.Business.View.SpecimenSurgicalDiagnosisView specimenSurgicalDiagnosisView = (YellowstonePathology.Business.View.SpecimenSurgicalDiagnosisView)this.ListViewSpecimenSurgicalDiagnosis.SelectedItem;
				YellowstonePathology.Business.Test.Surgical.SurgicalTestOrder panelSetOrderSurgical = this.m_AccessionOrder.PanelSetOrderCollection.GetSurgical();

				if (panelSetOrderSurgical.SurgicalSpecimenCollection.SpecimenOrderExists(specimenSurgicalDiagnosisView.SpecimenOrder.SpecimenOrderId) == false)
                {
					YellowstonePathology.Business.Test.Surgical.SurgicalSpecimen surgicalSpecimen = panelSetOrderSurgical.SurgicalSpecimenCollection.GetNextItem(panelSetOrderSurgical.ReportNo);
                    surgicalSpecimen.SpecimenOrderId = specimenSurgicalDiagnosisView.SpecimenOrder.SpecimenOrderId;
                    surgicalSpecimen.DiagnosisId = specimenSurgicalDiagnosisView.SpecimenOrder.SpecimenNumber;
					panelSetOrderSurgical.SurgicalSpecimenCollection.Add(surgicalSpecimen);
                }      

                this.m_SpecimenSurgicalDiagnosisViewCollection.Refresh(this.m_AccessionOrder);
                this.NotifyPropertyChanged("SpecimenSurgicalDiagnosisViewCollection");
                MessageBox.Show("The surgical diagnosis has been added.");                
            }                        
        }        
    }
}
