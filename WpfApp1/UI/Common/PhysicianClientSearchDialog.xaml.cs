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

namespace YellowstonePathology.UI.Common
{
	/// <summary>
	/// Interaction logic for PhysicianClientSearch.xaml
	/// </summary>
	public partial class PhysicianClientSearchDialog : Window
	{
		private Login.FinalizeAccession.PhysicianClientSearchPage m_PhysicianClientSearchPage;
		private YellowstonePathology.Business.Client.Model.PhysicianClient m_PhysicianClient;

		public PhysicianClientSearchDialog(YellowstonePathology.Business.Interface.IOrder accessionOrder)
		{
			this.m_PhysicianClientSearchPage = new Login.FinalizeAccession.PhysicianClientSearchPage(accessionOrder, false);
			InitializeComponent();
			this.Loaded += new RoutedEventHandler(PhysicianClientSearchDialog_Loaded);
		}		

        public PhysicianClientSearchDialog(YellowstonePathology.Business.Test.AccessionOrder accessionOrderOld, int clientId)
        {
			this.m_PhysicianClientSearchPage = new Login.FinalizeAccession.PhysicianClientSearchPage(accessionOrderOld, clientId, false);
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(PhysicianClientSearchDialog_Loaded);
        }

		private void PhysicianClientSearchDialog_Loaded(object sender, RoutedEventArgs e)
		{
			//this.m_PhysicianClientSearchPage.Return += new Login.FinalizeAccession.PhysicianClientSearchPage.ReturnEventHandler(PhysicianClientSearchPage_Return);
			//this.MainContent.Content = this.m_PhysicianClientSearchPage;
		}

		public YellowstonePathology.Business.Client.Model.PhysicianClient PhysicianClient
		{
			get { return this.m_PhysicianClient; }
		}

		private void PhysicianClientSearchPage_Return(object sender, UI.Navigation.PageNavigationReturnEventArgs e)
		{
			switch (e.PageNavigationDirectionEnum)
			{
				case UI.Navigation.PageNavigationDirectionEnum.Back:
					this.DialogResult = false;
					break;
				case UI.Navigation.PageNavigationDirectionEnum.Next:
					this.m_PhysicianClient = (YellowstonePathology.Business.Client.Model.PhysicianClient)e.Data;
					if (this.m_PhysicianClient == null)
					{
						this.DialogResult = false;
					}
					else
					{
						this.DialogResult = true;
					}
					break;
			}
			this.Close();
		}        
	}
}
