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

namespace YellowstonePathology.UI.Login
{
	/// <summary>
	/// Interaction logic for SearchAccessionDatePage.xaml
	/// </summary>
	public partial class SearchAccessionDatePage : UserControl, INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;
		public delegate void ReturnEventHandler(object sender, UI.Navigation.PageNavigationReturnEventArgs e);
		public event ReturnEventHandler Return;

		private string m_PageHeaderText = "Accession Date Search";
		private DateTime m_AccessionDate;

		public SearchAccessionDatePage()
		{
			this.m_AccessionDate = DateTime.Today;
			InitializeComponent();

			this.DataContext = this;

			Loaded += new RoutedEventHandler(SearchAccessionDatePage_Loaded);
		}

		private void SearchAccessionDatePage_Loaded(object sender, RoutedEventArgs e)
		{
			this.TextBoxAccessionDate.Focus();
		}

		public string PageHeaderText
		{
			get { return this.m_PageHeaderText; }
		}

		public DateTime AccessionDate
		{
			get { return this.m_AccessionDate; }
			set
			{
				this.m_AccessionDate = value;
				this.NotifyPropertyChanged("AccessionDate");
			}
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
			UI.Navigation.PageNavigationReturnEventArgs args = new UI.Navigation.PageNavigationReturnEventArgs(UI.Navigation.PageNavigationDirectionEnum.Back, null);
			this.Return(this, args);
		}

		private void ButtonNext_Click(object sender, RoutedEventArgs e)
		{
			UI.Navigation.PageNavigationReturnEventArgs args = new UI.Navigation.PageNavigationReturnEventArgs(UI.Navigation.PageNavigationDirectionEnum.Next, this.AccessionDate);
			this.Return(this, args);
		}		

		private void TextBoxAccessionDate_KeyUp(object sender, KeyEventArgs e)
		{
			Nullable<DateTime> targetDate = null;
			bool result = YellowstonePathology.Business.Helper.TextBoxHelper.IncrementDate(this.TextBoxAccessionDate.Text, ref targetDate, e);
			if (result == true) this.AccessionDate = targetDate.Value;
		}		
	}
}
