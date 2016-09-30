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
    public partial class ReportOrderInfoPage : UserControl, INotifyPropertyChanged 
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public delegate void ReturnEventHandler(object sender, EventArgs e);
        public event ReturnEventHandler Return;
                
        YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
        YellowstonePathology.Business.Test.PanelSetOrder m_PanelSetOrder;		

        public ReportOrderInfoPage(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder)
        {
            this.m_AccessionOrder = accessionOrder;
			this.m_PanelSetOrder = panelSetOrder;			

            InitializeComponent();
            this.DataContext = this;            
        }

        public YellowstonePathology.Business.Test.AccessionOrder AccessionOrder
        {
            get { return this.m_AccessionOrder; }
            set { this.m_AccessionOrder = value; }
        }

        public YellowstonePathology.Business.Test.PanelSetOrder PanelSetOrder
        {
            get { return this.m_PanelSetOrder; }
            set { this.m_PanelSetOrder = value; }
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

        private void ButtonNext_Click(object sender, RoutedEventArgs e)
        {
            this.Return(this, new EventArgs());
        }        
    }
}
