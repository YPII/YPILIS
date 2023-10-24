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

namespace YellowstonePathology.UI.Cytology
{	
	public partial class EQCLabelPage : UserControl, INotifyPropertyChanged 
	{
		public event PropertyChangedEventHandler PropertyChanged;

		public delegate void FinishedEventHandler(object sender, EventArgs e);
		public event FinishedEventHandler Finished;

        public delegate void PageTimedOutEventHandler(object sender, EventArgs e);
        public event PageTimedOutEventHandler PageTimedOut;

        private System.Windows.Threading.DispatcherTimer m_PageTimeOutTimer;
        private List<string> m_Labels;       

		public EQCLabelPage()
		{            
            this.m_PageTimeOutTimer = new System.Windows.Threading.DispatcherTimer();
            this.m_PageTimeOutTimer.Interval = new TimeSpan(0, 20, 0);
            this.m_PageTimeOutTimer.Tick += new EventHandler(PageTimeOutTimer_Tick);

            this.m_Labels = new List<string>();
            this.m_Labels.Add("NegEQC");
            this.m_Labels.Add("Pos1EQC");
            this.m_Labels.Add("Pos2EQC");

            InitializeComponent();

			DataContext = this;            

            this.Loaded += new RoutedEventHandler(EQCLabelPage_Loaded);
            this.Unloaded += new RoutedEventHandler(EQCLabelPage_Unloaded);
		}

        public List<string> Labels
        {
            get { return this.m_Labels; }
        }

        private void EQCLabelPage_Loaded(object sender, RoutedEventArgs e)
        {                        
            this.m_PageTimeOutTimer.Start();
        }

        private void EQCLabelPage_Unloaded(object sender, RoutedEventArgs e)
        {            
            this.m_PageTimeOutTimer.Stop();
        }

        private void PageTimeOutTimer_Tick(object sender, EventArgs e)
        {
            this.m_PageTimeOutTimer.Stop();

            EventArgs eventArgs = new EventArgs();
            this.PageTimedOut(this, eventArgs);
        }                	

		public void NotifyPropertyChanged(String info)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(info));
			}
		}

        private void ButtonFinished_Click(object sender, RoutedEventArgs e)
        {
            this.Finished(this, new EventArgs());
        }

        private void ButtonPrint_Click(object sender, RoutedEventArgs e)
        {
            if (this.ListBoxLabels.SelectedItem != null)
            {
                string labelContent = (string)this.ListBoxLabels.SelectedItem;                
                string zplCommands = Business.Label.Model.PantherZPLLabel.GetCommands(labelContent, DateTime.Today, "EQC", labelContent);
                Business.Label.Model.ZPLPrinterTCP zplPrinter = new Business.Label.Model.ZPLPrinterTCP("10.1.1.19");
                zplPrinter.Print(zplCommands);
            }
        }
    }
}
