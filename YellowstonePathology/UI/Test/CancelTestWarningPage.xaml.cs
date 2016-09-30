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

namespace YellowstonePathology.UI.Test
{    
	public partial class CancelTestWarningPage : UserControl, INotifyPropertyChanged 
	{
		public event PropertyChangedEventHandler PropertyChanged;

        public delegate void BackEventHandler(object sender, YellowstonePathology.UI.CustomEventArgs.CancelTestEventArgs e);
        public event BackEventHandler Back;

        public delegate void CancelTestEventHandler(object sender, YellowstonePathology.UI.CustomEventArgs.CancelTestEventArgs e);
        public event CancelTestEventHandler CancelTest;

        private YellowstonePathology.UI.CustomEventArgs.CancelTestEventArgs m_CancelTestEventArgs;
        private string m_Message;

        public CancelTestWarningPage(YellowstonePathology.UI.CustomEventArgs.CancelTestEventArgs cancelTestEventArgs)
        {
            this.m_CancelTestEventArgs = cancelTestEventArgs;            
			InitializeComponent();
            this.SetupPage();
			DataContext = this;            
		}

        private void SetupPage()
        {
            if (this.m_CancelTestEventArgs.PanelSetOrder.Final == true)
            {
                this.m_Message = "The test cannot be canceled becuase it has been finalized.";
                this.ButtonCancelTest.IsEnabled = false;
            }
            else if (this.m_CancelTestEventArgs.PanelSetOrder.Accepted == true)
            {
                this.m_Message = "The test cannot be canceled because it has been accepted.";
                this.ButtonCancelTest.IsEnabled = false;
            }
            else
            {
                this.m_Message = "Canceling this test is irreversible!  Click the Cancel Test button to continue or click the Back button to go back.";
                this.ButtonCancelTest.IsEnabled = true;
            }
        }

        public string Message
        {
            get { return this.m_Message; }
        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            this.Back(this, this.m_CancelTestEventArgs);
        }

        private void ButtonCancelTest_Click(object sender, RoutedEventArgs e)
        {
            this.CancelTest(this, this.m_CancelTestEventArgs);
        }    		         

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }                                   
	}
}
