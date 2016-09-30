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
    public partial class SpecimenSelectionPage : UserControl, INotifyPropertyChanged 
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public delegate void BackEventHandler(object sender, EventArgs e);
        public event BackEventHandler Back;

        public delegate void TargetSelectedEventHandler(object sender, YellowstonePathology.UI.CustomEventArgs.TestOrderInfoEventArgs e);
        public event TargetSelectedEventHandler TargetSelected;        

        private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
        private List<YellowstonePathology.Business.Test.AccessionOrder> m_AccessionOrderList;
        private YellowstonePathology.Business.Test.TestOrderInfo m_TestOrderInfo;

        public SpecimenSelectionPage(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, YellowstonePathology.Business.Test.TestOrderInfo testOrderInfo)
        {
            this.m_AccessionOrder = accessionOrder;
            this.m_TestOrderInfo = testOrderInfo;

            this.m_AccessionOrderList = new List<Business.Test.AccessionOrder>();
            this.m_AccessionOrderList.Add(accessionOrder);

            InitializeComponent();
            
            this.DataContext = this;            
        }

        public YellowstonePathology.Business.Test.AccessionOrder AccessionOrder
        {
            get { return this.m_AccessionOrder; }
        }

        public List<YellowstonePathology.Business.Test.AccessionOrder> AccessionOrderList
        {
            get { return this.m_AccessionOrderList; }
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
            if (this.TreeViewAccession.SelectedItem != null)
            {
                YellowstonePathology.Business.Interface.IOrderTarget orderTarget = (YellowstonePathology.Business.Interface.IOrderTarget)this.TreeViewAccession.SelectedItem;
                if (this.m_TestOrderInfo.PanelSet.EnforceOrderTarget == true)
                {
                    YellowstonePathology.Business.Rules.MethodResult methodResult = this.m_TestOrderInfo.PanelSet.OrderTargetIsOk(orderTarget);
                    if (methodResult.Success == true)
                    {
                        this.m_TestOrderInfo.OrderTarget = orderTarget;
                        this.m_TestOrderInfo.OrderTargetIsKnown = true;
                        YellowstonePathology.UI.CustomEventArgs.TestOrderInfoEventArgs testOrderInfoEventArgs = new CustomEventArgs.TestOrderInfoEventArgs(this.m_TestOrderInfo);
                        this.TargetSelected(this, testOrderInfoEventArgs);                
                    }
                    else
                    {
                        MessageBox.Show(methodResult.Message);
                    }
                }
                else
                {
                    this.m_TestOrderInfo.OrderTarget = orderTarget;
                    this.m_TestOrderInfo.OrderTargetIsKnown = true;
                    YellowstonePathology.UI.CustomEventArgs.TestOrderInfoEventArgs testOrderInfoEventArgs = new CustomEventArgs.TestOrderInfoEventArgs(this.m_TestOrderInfo);
                    this.TargetSelected(this, testOrderInfoEventArgs);                
                }                
            }
            else
            {
                MessageBox.Show("You must selected an item from the tree before continuing.");
            }
        }               
    }
}
