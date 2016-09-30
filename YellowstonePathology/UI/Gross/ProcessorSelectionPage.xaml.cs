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

namespace YellowstonePathology.UI.Gross
{
	/// <summary>
	/// Interaction logic for BlockOptionsPage.xaml
	/// </summary>
	public partial class ProcessorSelectionPage : UserControl
	{
        public delegate void NextEventHandler(object sender, YellowstonePathology.UI.CustomEventArgs.SpecimenOrderReturnEventArgs e);
        public event NextEventHandler Next;

        private YellowstonePathology.Business.Specimen.Model.SpecimenOrder m_SpecimenOrder;
        private YellowstonePathology.Business.Surgical.ProcessorRunCollection m_ProcessorRunCollection;

        public ProcessorSelectionPage(YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder)
		{
            this.m_SpecimenOrder = specimenOrder;
            this.m_ProcessorRunCollection = Business.Surgical.ProcessorRunCollection.GetAll(false);
			InitializeComponent();
			DataContext = this;
            this.ListBoxProcessorRun.SelectionChanged += new SelectionChangedEventHandler(ListBoxProcessorRun_SelectionChanged);
		}

        private void ListBoxProcessorRun_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.ListBoxProcessorRun.SelectedItem != null)
            {
                YellowstonePathology.Business.Surgical.ProcessorRun processorRun = (YellowstonePathology.Business.Surgical.ProcessorRun)this.ListBoxProcessorRun.SelectedItem;
                this.m_SpecimenOrder.SetProcessor(processorRun);
            }
        }

        public YellowstonePathology.Business.Specimen.Model.SpecimenOrder SpecimenOrder
        {
            get { return this.m_SpecimenOrder; }
        }

        public YellowstonePathology.Business.Surgical.ProcessorRunCollection ProcessorRunCollection
        {
            get { return this.m_ProcessorRunCollection; }
        }
           

        private void ButtonNext_Click(object sender, RoutedEventArgs e)
        {
            if (this.Next != null) this.Next(this, new YellowstonePathology.UI.CustomEventArgs.SpecimenOrderReturnEventArgs(this.m_SpecimenOrder));
        }
	}
}
