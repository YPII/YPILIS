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

namespace YellowstonePathology.UI
{
    /// <summary>
    /// Interaction logic for ASCCPRulesDialog.xaml
    /// </summary>
    public partial class ASCCPRulesDialog : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        Business.ASCCPRule.SimulationCollection m_SimulationCollection;
        Business.ASCCPRule.Simulation m_CurrentSimulation;
        Business.ASCCPRule.Woman m_CurrentWoman;        

        Business.Cytology.Model.SpecimenAdequacyCollection m_SpecimenAdequacyCollection;
        Business.Cytology.Model.ScreeningImpressionCollection m_ScreeningImpressionCollection;
        Business.Cytology.Model.OrderTypeCollection m_OrderTypeCollection;

        public ASCCPRulesDialog()
        {            
            this.m_SimulationCollection = new Business.ASCCPRule.SimulationCollection();
            this.m_OrderTypeCollection = new Business.Cytology.Model.OrderTypeCollection();
            this.m_SpecimenAdequacyCollection = Business.Gateway.AccessionOrderGateway.GetSpecimenAdequacy();
            this.m_ScreeningImpressionCollection = Business.Gateway.AccessionOrderGateway.GetScreeningImpressions();            

            InitializeComponent();
            this.DataContext = this;            
        }

        public Business.ASCCPRule.SimulationCollection SimulationCollection
        {
            get { return this.m_SimulationCollection; }
        }

        public Business.Cytology.Model.OrderTypeCollection OrderTypeCollection
        {
            get { return this.m_OrderTypeCollection; }
        }

        public Business.Cytology.Model.ScreeningImpressionCollection ScreeningImpressionCollection
        {
            get { return this.m_ScreeningImpressionCollection; }
            set { this.m_ScreeningImpressionCollection = value; }
        }

        public YellowstonePathology.Business.Cytology.Model.SpecimenAdequacyCollection SpecimenAdequacyCollection
        {
            get { return this.m_SpecimenAdequacyCollection; }
            set { this.m_SpecimenAdequacyCollection = value; }
        }

        public Business.ASCCPRule.Simulation CurrentSimulation
        {
            get { return this.m_CurrentSimulation; }
            set
            {
                if (this.m_CurrentSimulation != value)
                {
                    this.m_CurrentSimulation = value;
                    this.NotifyPropertyChanged("CurrentSimulation");
                }
            }
        }

        public Business.ASCCPRule.Woman CurrentWoman
        {
            get { return this.m_CurrentWoman; }
            set
            {
                if(this.CurrentWoman != value)
                {
                    this.m_CurrentWoman = value;
                    this.NotifyPropertyChanged("CurrentWoman");
                }                
            }
        }        

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }        

        private void ButtonCreateWoman_Click(object sender, RoutedEventArgs e)
        {
            if (this.ComboBoxSimulation.SelectedItem != null)
            {
                this.m_CurrentSimulation = (Business.ASCCPRule.Simulation)this.ComboBoxSimulation.SelectedItem;
                this.m_CurrentSimulation.Run();
                this.NotifyPropertyChanged(string.Empty);
            }
        }

        private void ListViewWoman_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(this.ListViewWoman.SelectedItem != null)
            {
                this.m_CurrentWoman = (Business.ASCCPRule.Woman)this.ListViewWoman.SelectedItem;
                this.NotifyPropertyChanged(string.Empty);
            }
        }
    }
}
