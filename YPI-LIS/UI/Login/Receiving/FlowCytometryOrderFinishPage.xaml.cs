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
    public partial class FlowCytometryOrderFinishPage : UserControl, INotifyPropertyChanged 
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public delegate void ReturnEventHandler(object sender, UI.Navigation.PageNavigationReturnEventArgs e);
        public event ReturnEventHandler Return;

		YellowstonePathology.Business.Specimen.Model.SpecimenOrder m_SpecimenOrder;
        YellowstonePathology.Business.PanelSet.Model.PanelSet m_PanelSet;
        YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;

		public FlowCytometryOrderFinishPage(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder, YellowstonePathology.Business.PanelSet.Model.PanelSet panelSet)
        {
            this.m_AccessionOrder = accessionOrder;
            this.m_SpecimenOrder = specimenOrder;
            this.m_PanelSet = panelSet;

            InitializeComponent();
            this.DataContext = this;            
        }

        public YellowstonePathology.Business.Test.AccessionOrder AccessionOrder
        {
            get { return this.m_AccessionOrder; }
            set { this.m_AccessionOrder = value; }
        }

		public YellowstonePathology.Business.Specimen.Model.SpecimenOrder SpecimenOrder
        {
            get { return this.m_SpecimenOrder; }
            set { this.m_SpecimenOrder = value; }
        }

        public YellowstonePathology.Business.PanelSet.Model.PanelSet PanelSet
        {
            get { return this.m_PanelSet; }
            set { this.m_PanelSet = value; }
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
            UI.Navigation.PageNavigationReturnEventArgs args = new UI.Navigation.PageNavigationReturnEventArgs(UI.Navigation.PageNavigationDirectionEnum.Back, null);
            this.Return(this, args);
        }

        private void ButtonFinish_Click(object sender, RoutedEventArgs e)
        {
            UI.Navigation.PageNavigationReturnEventArgs args = new UI.Navigation.PageNavigationReturnEventArgs(UI.Navigation.PageNavigationDirectionEnum.Finish, null);
            this.Return(this, args);
        }        
    }
}
