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

namespace YellowstonePathology.UI.Cutting
{    
	public partial class AliquotOrderSelectionPage : UserControl, INotifyPropertyChanged 
	{
		public event PropertyChangedEventHandler PropertyChanged;

        public delegate void AliquotOrderSelectedEventHandler(object sender, YellowstonePathology.UI.CustomEventArgs.AliquotOrderReturnEventArgs eventArgs);
        public event AliquotOrderSelectedEventHandler AliquotOrderSelected;

        private YellowstonePathology.Business.Test.AliquotOrderCollection m_AliquotOrderCollection;
        private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;

        public delegate void BackEventHandler(object sender, YellowstonePathology.UI.CustomEventArgs.MasterAccessionNoReturnEventArgs eventArgs);
        public event BackEventHandler Back;

        public AliquotOrderSelectionPage(YellowstonePathology.Business.Test.AliquotOrderCollection aliquotOrderCollection, YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {
            this.m_AliquotOrderCollection = aliquotOrderCollection;
            this.m_AccessionOrder = accessionOrder;
			InitializeComponent();
			DataContext = this;            
		}                      

        public YellowstonePathology.Business.Test.AliquotOrderCollection AliquotOrderCollection
        {
            get { return this.m_AliquotOrderCollection; }
        }

        public YellowstonePathology.Business.Test.AccessionOrder AccessionOrder
        {
            get { return this.m_AccessionOrder; }
        }

        private void ListBoxAliquotOrderCollection_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.ListViewAliquotOrderCollection.SelectedItem != null)
            {
                YellowstonePathology.Business.Test.AliquotOrder aliquotOrder = (YellowstonePathology.Business.Test.AliquotOrder)this.ListViewAliquotOrderCollection.SelectedItem;
                this.AliquotOrderSelected(this, new CustomEventArgs.AliquotOrderReturnEventArgs(aliquotOrder));
            }
        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            this.Back(this, new YellowstonePathology.UI.CustomEventArgs.MasterAccessionNoReturnEventArgs(this.m_AccessionOrder.MasterAccessionNo));
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
