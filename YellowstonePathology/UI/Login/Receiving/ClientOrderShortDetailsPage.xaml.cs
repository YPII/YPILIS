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
	/// Interaction logic for CreateClientOrderPage.xaml
	/// </summary>
	public partial class ClientOrderShortDetailsPage : UserControl, INotifyPropertyChanged
	{
        public event PropertyChangedEventHandler PropertyChanged;

		public delegate void ReturnEventHandler(object sender, UI.Navigation.PageNavigationReturnEventArgs e);
		public event ReturnEventHandler Return;

        private YellowstonePathology.Business.ClientOrder.Model.ClientOrder m_ClientOrder;		
		private string m_PageHeaderText = "Enter the patient information for this client order.";
		
		public ClientOrderShortDetailsPage(YellowstonePathology.Business.ClientOrder.Model.ClientOrder clientOrder)
		{
            this.m_ClientOrder = clientOrder;            
			InitializeComponent();
			DataContext = this;

            this.Loaded += new RoutedEventHandler(CreateClientOrderPage_Loaded);
		}

        private void CreateClientOrderPage_Loaded(object sender, RoutedEventArgs e)
        {			
            this.TextBoxCollectionDate.Focus();
        }

		public string PageHeaderText
		{
			get { return this.m_PageHeaderText; }
		}

		public YellowstonePathology.Business.ClientOrder.Model.ClientOrder ClientOrder
		{
			get { return this.m_ClientOrder; }
		}

		private void TextBoxCollectionDate_KeyUp(object sender, KeyEventArgs e)
		{
			Nullable<DateTime> targetDate = null;
			bool result = YellowstonePathology.Business.Helper.TextBoxHelper.IncrementDate(this.TextBoxCollectionDate.Text, ref targetDate, e);
			if (result == true) this.m_ClientOrder.CollectionDate = targetDate;
		}

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
			UI.Navigation.PageNavigationReturnEventArgs args = new UI.Navigation.PageNavigationReturnEventArgs(UI.Navigation.PageNavigationDirectionEnum.Back, null);
			this.Return(this, args);
		}

        private void ButtonNext_Click(object sender, RoutedEventArgs e)
        {
			this.m_ClientOrder.OrderedBy = YellowstonePathology.Business.User.SystemIdentity.Instance.User.DisplayName;
			UI.Navigation.PageNavigationReturnEventArgs args = new UI.Navigation.PageNavigationReturnEventArgs(UI.Navigation.PageNavigationDirectionEnum.Next, null);
			this.Return(this, args);
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this).Close();
        }		

        private void ButtonEnterTestPatient_Click(object sender, RoutedEventArgs e)
        {
            this.m_ClientOrder.PFirstName = "Mickey";
            this.m_ClientOrder.PLastName = "Mouse";
            this.m_ClientOrder.PMiddleInitial = "M";
            this.m_ClientOrder.PBirthdate = DateTime.Parse("08/10/1966");
            this.NotifyPropertyChanged("ClientOrder");
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
