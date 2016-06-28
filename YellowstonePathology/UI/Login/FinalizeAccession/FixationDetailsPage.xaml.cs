using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace YellowstonePathology.UI.Login.FinalizeAccession
{
	/// <summary>
	/// Interaction logic for FixationDetailsPage.xaml
	/// </summary>
	public partial class FixationDetailsPage : UserControl, INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;
		public delegate void ReturnEventHandler(object sender, UI.Navigation.PageNavigationReturnEventArgs e);
		public event ReturnEventHandler Return;
		
		private string m_PageHeaderText = "Fixation Details Page";
		private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
		private ObservableCollection<string> m_FixationTypeCollection;		
        private YellowstonePathology.Business.Surgical.ProcessorRunCollection m_ProcessorRunCollection;
        private ObservableCollection<string> m_TimeToFixationTypeCollection;
        

		public FixationDetailsPage(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
		{			
			this.m_AccessionOrder = accessionOrder;

            this.m_ProcessorRunCollection = Business.Surgical.ProcessorRunCollection.GetAll(true);
            this.m_FixationTypeCollection = YellowstonePathology.Business.Specimen.Model.FixationType.GetFixationTypeCollection();

            this.m_TimeToFixationTypeCollection = YellowstonePathology.Business.Specimen.Model.TimeToFixationType.GetTimeToFixationTypeCollection();

			YellowstonePathology.Business.ClientOrder.Model.ClientOrderCollection clientOrderCollection = YellowstonePathology.Business.Gateway.ClientOrderGateway.GetClientOrdersByMasterAccessionNo(this.m_AccessionOrder.MasterAccessionNo);			
			this.m_PageHeaderText = this.m_AccessionOrder.MasterAccessionNo + ": " + this.m_AccessionOrder.PFirstName + " " + this.m_AccessionOrder.PLastName;            

			InitializeComponent();

			DataContext = this;
			Loaded += new RoutedEventHandler(FixationDetailsPage_Loaded);
		}        

        public ObservableCollection<string> TimeToFixationTypeCollection
        {
            get { return this.m_TimeToFixationTypeCollection; }
        }

        public YellowstonePathology.Business.Surgical.ProcessorRunCollection ProcessorRunCollection
        {
            get { return this.m_ProcessorRunCollection; }
        }

		private void FixationDetailsPage_Loaded(object sender, RoutedEventArgs e)
		{
            
		}        

		public string PageHeaderText
		{
			get { return this.m_PageHeaderText; }
		}

		public void NotifyPropertyChanged(String info)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(info));
			}
		}

		public YellowstonePathology.Business.Test.AccessionOrder AccessionOrder
		{
			get { return this.m_AccessionOrder; }
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

        private void ButtonSet_Click(object sender, RoutedEventArgs e)
        {
            if (ListBoxProcessorRun.SelectedItem != null)
            {
                YellowstonePathology.Business.Surgical.ProcessorRun processorRun = (YellowstonePathology.Business.Surgical.ProcessorRun)this.ListBoxProcessorRun.SelectedItem;
                foreach (YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder in this.ListViewSpecimen.SelectedItems)
                {
                    specimenOrder.SetFixationStartTime();
                    specimenOrder.SetTimeToFixation();
                    specimenOrder.SetProcessor(processorRun);
                    specimenOrder.SetFixationEndTime();
                    specimenOrder.SetFixationDuration();                    
                }
            }           
        }        
	}
}
