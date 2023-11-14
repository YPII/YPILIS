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
using System.Xml.Linq;
using System.IO;
using System.IO.Packaging;
using System.Windows.Markup;
using System.Reflection;
using System.Windows.Threading;
using System.Windows.Xps;
using System.Windows.Xps.Packaging;

namespace YellowstonePathology.UI
{
	public partial class PackingSlip : FlowDocument
	{
		private XElement m_ShipmentElement;

		public PackingSlip(XElement shipmentElement)
		{
			this.m_ShipmentElement = shipmentElement;
			InitializeComponent();

			this.InjectData();
		}

		public void Print()
		{
			System.Printing.PrintQueue printQueue = new System.Printing.LocalPrintServer().DefaultPrintQueue;

			System.Windows.Controls.PrintDialog printDialog = new System.Windows.Controls.PrintDialog();
			printDialog.PrintTicket.PageOrientation = System.Printing.PageOrientation.Portrait;
			printDialog.PrintQueue = printQueue;
			DocumentPaginator documentPaginator = ((IDocumentPaginatorSource)this).DocumentPaginator;
			printDialog.PrintDocument(documentPaginator, "PackingSlip");
		}

		private void InjectData()
		{
			this.DataContext = this;
			var dispatcher = Dispatcher.CurrentDispatcher;
			dispatcher.Invoke(DispatcherPriority.SystemIdle, new DispatcherOperationCallback(delegate { return null; }), null);
			SetListItems();
		}

		private void SetListItems()
		{
			foreach (XElement clientElement in this.m_ShipmentElement.Element("ClientOrderCollection").Elements("ClientOrder"))
			{
				Run nameblock = new Run("Patient: " + clientElement.Element("PatientName").Value + "   Birthdate: " + clientElement.Element("PBirthdate").Value);
				nameblock.FontSize = 12;
				Paragraph clientParagraph = new Paragraph(nameblock);
				clientParagraph.Padding = new Thickness(50, 10, 0, 10);
				ListItem listitem = new ListItem(clientParagraph);
				this.DetailsList.ListItems.Add(listitem);

				foreach (XElement specimenElement in clientElement.Element("ClientOrderDetailCollection").Elements("ClientOrderDetail"))
				{
					Run lineOne = new Run(specimenElement.Element("ContainerId").Value + "   Collected: " + specimenElement.Element("CollectionDate").Value);
					lineOne.FontSize = 12;
					Paragraph paragraphOne = new Paragraph(lineOne);
					paragraphOne.Padding = new Thickness(55, 10, 0, 10);
					ListItem listOne = new ListItem(paragraphOne);
					this.DetailsList.ListItems.Add(listOne);

					Run lineTwo = new Run(specimenElement.Element("Description").Value + "   By: " + specimenElement.Element("OrderedBy").Value);
					lineTwo.FontSize = 12;
					Paragraph paragraphTwo = new Paragraph(lineTwo);
					paragraphTwo.Padding = new Thickness(55, 10, 0, 10);
					ListItem listTwo = new ListItem(paragraphTwo);
					this.DetailsList.ListItems.Add(listTwo);
				}
			}
		}

		public System.Xml.Linq.XElement ShipmentElement
		{
			get { return this.m_ShipmentElement; }
		}
	}
}
