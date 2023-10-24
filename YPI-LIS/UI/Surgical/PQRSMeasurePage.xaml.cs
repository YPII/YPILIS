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

namespace YellowstonePathology.UI.Surgical
{    
	public partial class PQRSMeasurePage : UserControl
    {
		public delegate void AddPQRSCodeEventHandler(object sender, CustomEventArgs.AddPQRSReturnEventArgs e);
		public event AddPQRSCodeEventHandler AddPQRSCode;

		public delegate void PQRSCodeNotApplicableEventHandler(object sender, EventArgs e);
		public event PQRSCodeNotApplicableEventHandler PQRSCodeNotApplicable;

		public delegate void CancelEventHandler(object sender, EventArgs e);
        public event CancelEventHandler Cancel;

        private YellowstonePathology.Business.Surgical.PQRSMeasure m_PQRSMeasure;
		private YellowstonePathology.Business.Test.Surgical.SurgicalSpecimen m_SurgicalSpecimen;

		public PQRSMeasurePage(YellowstonePathology.Business.Surgical.PQRSMeasure pqrsMeasure, YellowstonePathology.Business.Test.Surgical.SurgicalSpecimen surgicalSpecimen)
        {
            this.m_PQRSMeasure = pqrsMeasure;
			this.m_SurgicalSpecimen = surgicalSpecimen;

            InitializeComponent();
			this.DataContext = this;
        }
        
        private void ButtonOK_Click(object sender, RoutedEventArgs e)
        {
			if (this.CheckBoxNotApplicable.IsChecked.HasValue && this.CheckBoxNotApplicable.IsChecked.Value == true)
			{
				this.PQRSCodeNotApplicable(this, new EventArgs());
			}
			else if (this.RadioButtonList.SelectedItem != null)
			{
				YellowstonePathology.Business.Billing.Model.PQRSCode pqrsCode = (YellowstonePathology.Business.Billing.Model.PQRSCode)this.RadioButtonList.SelectedItem;
				this.AddPQRSCode(this, new CustomEventArgs.AddPQRSReturnEventArgs(pqrsCode, this.m_SurgicalSpecimen));
			}
			else
			{
				MessageBox.Show("Please select an option from the list.");
			}
		}		

		public YellowstonePathology.Business.Surgical.PQRSMeasure PQRSMeasure
		{
			get { return this.m_PQRSMeasure; }
		}

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Cancel(this, new EventArgs());
        }

		private void CheckBoxNotApplicable_Checked(object sender, RoutedEventArgs e)
		{
			this.RadioButtonList.SelectedIndex = -1;
		}

		private void RadioButtonList_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if(this.RadioButtonList.SelectedIndex != -1) this.CheckBoxNotApplicable.IsChecked = false;
		}
	}
}
