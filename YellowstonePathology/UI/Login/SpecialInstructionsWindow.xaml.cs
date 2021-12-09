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
using System.ComponentModel;
using System.Windows.Xps.Packaging;
using System.IO;
using System.Xml.Linq;

namespace YellowstonePathology.UI.Login
{
	/// <summary>
	/// Interaction logic for DocumentWindow.xaml
	/// </summary>
	public partial class SpecialInstructionsWindow : Window, INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		public Object m_Order;

		public SpecialInstructionsWindow(object order)
		{
			this.m_Order =order;
			InitializeComponent();
			this.DataContext = this;						
		}

        public Object Order
		{
            get { return this.m_Order; }
            set
            {
                if (this.m_Order != value)
                {
                    this.m_Order = value;
                    this.NotifyPropertyChanged("Order");
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
	}
}
