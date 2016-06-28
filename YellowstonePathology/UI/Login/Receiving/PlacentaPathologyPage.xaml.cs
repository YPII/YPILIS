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
using System.Collections.ObjectModel;
using System.Xml.Linq;

namespace YellowstonePathology.UI.Login.Receiving
{
	public partial class PlacentalPathologyPage : UserControl
	{
		public delegate void ReturnEventHandler(object sender, UI.Navigation.PageNavigationReturnEventArgs e);
		public event ReturnEventHandler Return;

		private string m_PageHeaderText = "Placental Pathology Questionnaire Page";
		private YellowstonePathology.Business.ClientOrder.Model.ClientOrder m_ClientOrder;

		public PlacentalPathologyPage(YellowstonePathology.Business.ClientOrder.Model.ClientOrder clientOrder)
		{
            this.m_ClientOrder = clientOrder;

			InitializeComponent();
            
			DataContext = this;
			Loaded += new RoutedEventHandler(PlacentalFormDisplayPage_Loaded);
		}

		private void PlacentalFormDisplayPage_Loaded(object sender, RoutedEventArgs e)
		{
			XElement clientOrderElement = this.m_ClientOrder.ToXML(false);
			foreach (YellowstonePathology.Business.ClientOrder.Model.ClientOrderDetail clientOrderDetail in this.m_ClientOrder.ClientOrderDetailCollection)
			{
				if (clientOrderDetail.OrderTypeCode == "PLCNT")
				{
					XElement clientOrderDetailElement = new XElement("ClientOrderDetail");
					YellowstonePathology.Business.Persistence.XmlPropertyReader clientOrderDetailPropertyWriter = new YellowstonePathology.Business.Persistence.XmlPropertyReader(clientOrderDetail, clientOrderDetailElement);
					clientOrderDetailPropertyWriter.Write();
					clientOrderElement.Add(clientOrderDetailElement);
				}
			}
			YellowstonePathology.Document.Result.Data.PlacentalPathologyQuestionnaireData placentalPathologyQuestionnaireData = new YellowstonePathology.Document.Result.Data.PlacentalPathologyQuestionnaireData(clientOrderElement);
			YellowstonePathology.Document.PlacentalPathologyQuestionnaire placentalPathologyQuestionnare = new Document.PlacentalPathologyQuestionnaire(placentalPathologyQuestionnaireData);
			this.Viewer.Document = placentalPathologyQuestionnare.FixedDocument;
		}

		public string PageHeaderText
		{
			get { return this.m_PageHeaderText; }
		}

		private void ButtonBack_Click(object sender, RoutedEventArgs e)
		{
			UI.Navigation.PageNavigationReturnEventArgs args = new UI.Navigation.PageNavigationReturnEventArgs(UI.Navigation.PageNavigationDirectionEnum.Back, null);
			this.Return(this, args);
		}

		private void ButtonNext_Click(object sender, RoutedEventArgs e)
		{
			UI.Navigation.PageNavigationReturnEventArgs args = new UI.Navigation.PageNavigationReturnEventArgs(UI.Navigation.PageNavigationDirectionEnum.Next, null);
			this.Return(this, args);
		}		
	}
}
