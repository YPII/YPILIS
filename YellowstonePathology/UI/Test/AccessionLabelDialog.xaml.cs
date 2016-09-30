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
using System.Windows.Shapes;
using System.Collections.ObjectModel;

namespace YellowstonePathology.UI.Test
{    
    public partial class AccessionLabelDialog : Window
    {
        private ObservableCollection<YellowstonePathology.Business.Label.Model.AccessionLabelQuantity> m_AccessionLabelQuantityCollection;

        public AccessionLabelDialog()
        {
            this.m_AccessionLabelQuantityCollection = new ObservableCollection<Business.Label.Model.AccessionLabelQuantity>();            

            for (int i = 0; i < 10; i++)
            {
                YellowstonePathology.Business.Label.Model.AccessionLabel accessionLabel = new Business.Label.Model.AccessionLabel();
                YellowstonePathology.Business.Label.Model.AccessionLabelQuantity labelQuantity = new Business.Label.Model.AccessionLabelQuantity(1, accessionLabel, false);
                this.m_AccessionLabelQuantityCollection.Add(labelQuantity);
            }

            InitializeComponent();

            this.DataContext = this;
        }

        public AccessionLabelDialog(string masterAccessionNo, string pFirstName, string pLastName)
        {
            this.m_AccessionLabelQuantityCollection = new ObservableCollection<Business.Label.Model.AccessionLabelQuantity>();
            YellowstonePathology.Business.Label.Model.AccessionLabel accessionLabelIn = new Business.Label.Model.AccessionLabel(pFirstName, pLastName, masterAccessionNo);
            YellowstonePathology.Business.Label.Model.AccessionLabelQuantity labelQuantityIn = new Business.Label.Model.AccessionLabelQuantity(1, accessionLabelIn, true);
            this.m_AccessionLabelQuantityCollection.Add(labelQuantityIn);
            
            for (int i = 0; i < 10; i++)
            {
                YellowstonePathology.Business.Label.Model.AccessionLabel accessionLabel = new Business.Label.Model.AccessionLabel();
                YellowstonePathology.Business.Label.Model.AccessionLabelQuantity labelQuantity = new Business.Label.Model.AccessionLabelQuantity(1, accessionLabel, false);
                this.m_AccessionLabelQuantityCollection.Add(labelQuantity);
            }

            InitializeComponent();

            this.DataContext = this;
        }

        public ObservableCollection<YellowstonePathology.Business.Label.Model.AccessionLabelQuantity> AccessionLabelQuantityCollection
        {
            get { return this.m_AccessionLabelQuantityCollection; }
        }

        private void ButtonPrint_Click(object sender, RoutedEventArgs e)
        {
            if (IsThereSomthingToPrint() == true)
            {
                YellowstonePathology.Business.Label.Model.HistologySlidePaperLabelPrinter histologySlidePaperLablePrinter = new Business.Label.Model.HistologySlidePaperLabelPrinter();
                foreach (YellowstonePathology.Business.Label.Model.AccessionLabelQuantity accessionLabelQuantity in this.m_AccessionLabelQuantityCollection)
                {
                    if (accessionLabelQuantity.IsValid == true)
                    {
                        for (int i = 0; i < accessionLabelQuantity.Quantity; i++)
                        {
                            histologySlidePaperLablePrinter.Queue.Enqueue(accessionLabelQuantity.AccessionLabel);
                        }
                    }
                }
                histologySlidePaperLablePrinter.Print();
                this.Close();
            }
        }

        private bool IsThereSomthingToPrint()
        {
            bool result = false;
            foreach (YellowstonePathology.Business.Label.Model.AccessionLabelQuantity accessionLabelQuantity in this.m_AccessionLabelQuantityCollection)
            {
                if (accessionLabelQuantity.IsValid == true)
                {
                    result = true;
                }
            }
            if (result == false)
            {
                MessageBox.Show("There is nothing to print.");
            }
            return result;
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {            
            this.Close();
        }

        private void ButtonUpdate_Click(object sender, RoutedEventArgs e)
        {
            foreach (YellowstonePathology.Business.Label.Model.AccessionLabelQuantity accessionLabelQuantity in this.m_AccessionLabelQuantityCollection)
            {
                if (accessionLabelQuantity.IsValid == false)
                {
                    if(string.IsNullOrEmpty(accessionLabelQuantity.AccessionLabel.MasterAccessionNo) == false)
                    {
						YellowstonePathology.Business.Test.AccessionOrderView accessionOrderView = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetAccessionOrderView(accessionLabelQuantity.AccessionLabel.MasterAccessionNo);
                        if (accessionOrderView != null)
                        {
                            accessionLabelQuantity.AccessionLabel.PatientFirstName = accessionOrderView.PFirstName;
                            accessionLabelQuantity.AccessionLabel.PatientLastName = accessionOrderView.PLastName;
                            accessionLabelQuantity.IsValid = true;
                        }
                        else
                        {
                            MessageBox.Show(accessionLabelQuantity.AccessionLabel.MasterAccessionNo + " is not a valid Master Accession No");
                        }
                    }                    
                }
            }
        }                
    }
}
