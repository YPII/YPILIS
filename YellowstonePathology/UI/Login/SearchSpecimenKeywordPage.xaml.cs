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
	public partial class SearchSpecimenKeywordPage : UserControl, INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;
		public delegate void ReturnEventHandler(object sender, UI.Navigation.PageNavigationReturnEventArgs e);
		public event ReturnEventHandler Return;

		private string m_PageHeaderText = "Specimen Keyword Search";
        private string m_SpecimenDescription;

        public SearchSpecimenKeywordPage()
		{			
			InitializeComponent();

			this.DataContext = this;

			Loaded += new RoutedEventHandler(SearchSpecimenKeywordPage_Loaded);
		}

        private void SearchSpecimenKeywordPage_Loaded(object sender, RoutedEventArgs e)
		{
			this.TextBoxSpecimenDescription.Focus();
		}

		public string PageHeaderText
		{
			get { return this.m_PageHeaderText; }
		}

		public string SpecimenDescription
		{
            get { return this.m_SpecimenDescription; }
			set
			{
				this.m_SpecimenDescription = value;
				this.NotifyPropertyChanged("SpecimenDescription");
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
			UI.Navigation.PageNavigationReturnEventArgs args = new UI.Navigation.PageNavigationReturnEventArgs(UI.Navigation.PageNavigationDirectionEnum.Next, this.m_SpecimenDescription);
			this.Return(this, args);
		}		
	}
}
