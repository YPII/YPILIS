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
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace YellowstonePathology.UI.Login.Receiving
{	
	public partial class TumorNucleiPercentageEntryPage : UserControl, INotifyPropertyChanged 
	{
		public event PropertyChangedEventHandler PropertyChanged;

		public delegate void NextEventHandler(object sender, EventArgs e);
		public event NextEventHandler Next;

		public delegate void BackEventHandler(object sender, EventArgs e);
		public event BackEventHandler Back;		
				
		private YellowstonePathology.Business.Interface.ISolidTumorTesting m_SolidTumorTesting;
        private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;

        public TumorNucleiPercentageEntryPage(YellowstonePathology.Business.Interface.ISolidTumorTesting solidTumorTesting, YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
		{
            this.m_SolidTumorTesting = solidTumorTesting;
            this.m_AccessionOrder = accessionOrder;

			InitializeComponent();			
			DataContext = this;

            Loaded += TumorNucleiPercentageEntryPage_Loaded;
            Unloaded += TumorNucleiPercentageEntryPage_Unloaded;
		}

        private void TumorNucleiPercentageEntryPage_Loaded(object sender, RoutedEventArgs e)
        {
             
        }

        private void TumorNucleiPercentageEntryPage_Unloaded(object sender, RoutedEventArgs e)
        {
             
        }

        public void NotifyPropertyChanged(String info)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(info));
			}
		}		        

		public YellowstonePathology.Business.Interface.ISolidTumorTesting SolidTumorTesting
		{
			get { return this.m_SolidTumorTesting; }
		}				    

		private void ButtonNext_Click(object sender, RoutedEventArgs e)
		{
            this.m_AccessionOrder.PanelSetOrderCollection.UpdateTumorNucleiPercentage(this.m_SolidTumorTesting);
			if (this.Next != null) this.Next(this, new EventArgs());
		}

		private void ButtonBack_Click(object sender, RoutedEventArgs e)
		{
			if (this.Back != null) this.Back(this, new EventArgs());
		}		
	}
}
