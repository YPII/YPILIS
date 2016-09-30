using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace YellowstonePathology.Business.XPSDocument.Result.Data
{
    public class ClientSupplyOrderReportData : XElement
    {
        public ClientSupplyOrderReportData(XElement clientSupplyOrderElement)
			: base("ClientSupplyOrderReportData")
		{
            string clientName = clientSupplyOrderElement.Element("ClientName").Value;
            XElement clientNameElement = new XElement("ClientName", clientName);
            this.Add(clientNameElement);

            DateTime orderDate = DateTime.Parse(clientSupplyOrderElement.Element("OrderDate").Value);
            XElement  orderDateElement = new XElement("OrderDate", orderDate.ToShortDateString());
            this.Add(orderDateElement);

            string address = string.Empty;
            if (clientSupplyOrderElement.Element("ClientAddress") != null)
            {
                address = clientSupplyOrderElement.Element("ClientAddress").Value;
            }
            XElement addressElement = new XElement("Address", address);
            this.Add(addressElement);

            string city = string.Empty;
            if (clientSupplyOrderElement.Element("ClientCity") != null)
            {
                city = clientSupplyOrderElement.Element("ClientCity").Value;
            }
            XElement cityElement = new XElement("City", city);
            this.Add(cityElement);

            string state = string.Empty;
            if (clientSupplyOrderElement.Element("ClientState") != null)
            {
                state = clientSupplyOrderElement.Element("ClientState").Value;
            }
            XElement stateElement = new XElement("State", state);
            this.Add(stateElement);

            string zip = string.Empty;
            if (clientSupplyOrderElement.Element("ClientZip") != null)
            {
                zip = clientSupplyOrderElement.Element("ClientZip").Value;
            }
            XElement zipElement = new XElement("Zip", zip);
            this.Add(zipElement);

            string comment = string.Empty;
            if (clientSupplyOrderElement.Element("Comment") != null)
            {
                comment = clientSupplyOrderElement.Element("Comment").Value;
            }
            XElement commentElement = new XElement("Comment", comment);
            this.Add(commentElement);

            List<XElement> detailElements = (from item in clientSupplyOrderElement.Elements("ClientSupplyOrderDetailCollection")
                                                    select item).ToList<XElement>();
            foreach (XElement clientSupplyOrderDetailElement in detailElements.Elements("ClientSupplyOrderDetail"))
            {
                XElement detailElement = new XElement("ClientSupplyOrderDetail");
                string quantity = clientSupplyOrderDetailElement.Element("quantityordered").Value;
                XElement quantityElement = new XElement("QuantityOrdered", quantity);
                detailElement.Add(quantityElement);

                string supplyName = clientSupplyOrderDetailElement.Element("supplyname").Value;
                XElement supplyNameElement = new XElement("SupplyName", supplyName);
                detailElement.Add(supplyNameElement);

                string supplyDescription = clientSupplyOrderDetailElement.Element("supplydescription").Value;
                XElement supplyDescriptionElement = new XElement("SupplyDescription", supplyDescription);
                detailElement.Add(supplyDescriptionElement);

                this.Add(detailElement);
            }
        }
    }
}
