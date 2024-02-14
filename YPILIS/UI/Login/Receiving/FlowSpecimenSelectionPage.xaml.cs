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

namespace YellowstonePathology.UI.Login.Receiving
{
    /// <summary>
    /// 
    /// </summary>
    public partial class FlowSpecimenSelectionPage : UserControl, INotifyPropertyChanged 
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public delegate void UseThisSpecimenEventHandler(object sender, YellowstonePathology.UI.CustomEventArgs.SpecimenOrderReturnEventArgs e);
        public event UseThisSpecimenEventHandler UseThisSpecimen;        

        public delegate void BackEventHandler(object sender, EventArgs e);
        public event BackEventHandler Back;

		private YellowstonePathology.Business.Specimen.Model.SpecimenOrderCollection m_SpecimenOrderCollection;

		public FlowSpecimenSelectionPage(YellowstonePathology.Business.Specimen.Model.SpecimenOrderCollection specimenOrderCollection)
        {
            this.m_SpecimenOrderCollection = specimenOrderCollection;
            InitializeComponent();
            this.DataContext = this;            
        }

		public YellowstonePathology.Business.Specimen.Model.SpecimenOrderCollection SpecimenOrderCollection
        {
            get { return this.m_SpecimenOrderCollection; }
            set { this.m_SpecimenOrderCollection = value; }
        }

        public bool OkToSaveOnNavigation(Type pageNavigatingTo)
        {
            return true;
        }

        public bool OkToSaveOnClose()
        {
            return true;
        }

        public void Save(bool releaseLock)
        {
            
        }

        public void UpdateBindingSources()
        {

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
            if (this.Back != null) this.Back(this, new EventArgs());
        }

        private void ButtonNext_Click(object sender, RoutedEventArgs e)
        {            
            if (this.ListBoxSpecimenOrderCollection.SelectedItems.Count != 0)
            {
				YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = (YellowstonePathology.Business.Specimen.Model.SpecimenOrder)this.ListBoxSpecimenOrderCollection.SelectedItem;
                YellowstonePathology.UI.CustomEventArgs.SpecimenOrderReturnEventArgs eventArgs = new CustomEventArgs.SpecimenOrderReturnEventArgs(specimenOrder);
                this.UseThisSpecimen(this, eventArgs);
            }
            else
            {
                MessageBox.Show("You must select a specimen before we can move to the next step.");
            }         
        }                 
    }
}
