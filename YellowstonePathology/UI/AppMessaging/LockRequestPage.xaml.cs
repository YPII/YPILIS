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

namespace YellowstonePathology.UI.AppMessaging
{
    public partial class LockRequestPage : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;        

        public delegate void RequestLockEventHandler(object sender, UI.CustomEventArgs.AccessionOrderReturnEventArgs e);
        public event RequestLockEventHandler RequestLock;

        private Business.Test.AccessionOrder m_AccessionOrder;
        private string m_DisplayMessage;

        public LockRequestPage(Business.Test.AccessionOrder accessionOrder)
        {
            this.m_AccessionOrder = accessionOrder;
            this.m_DisplayMessage = "Ask " + accessionOrder.AccessionLock.Address + " for the lock on " + accessionOrder.MasterAccessionNo + ".";

            InitializeComponent();
            DataContext = this;
        }

        public string DisplayMessage
        {
            get { return this.m_DisplayMessage; }
        }         

		public void NotifyPropertyChanged(String info)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(info));
			}
		}                

        private void ButtonAskToTakeCase_Click(object sender, RoutedEventArgs e)
        {
            if (this.RequestLock != null) this.RequestLock(this, new CustomEventArgs.AccessionOrderReturnEventArgs(this.m_AccessionOrder));         
        }        

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
        	Window.GetWindow(this).Close();
        }
    }
}
