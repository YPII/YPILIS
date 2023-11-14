using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace YellowstonePathology.Business.XPSDocument.Result.Data
{
    public class CytologyScreeningListReportData : XElement
    {
        public CytologyScreeningListReportData(XElement cytologyScreeningListElement)
            : base("CytologyScreeningListReportData")
        {
            List<XElement> cytologyScreeningElements = (from item in cytologyScreeningListElement.Elements("CytologyScreeningSearchResult")
                                                    select item).ToList<XElement>();
            foreach (XElement cytologyScreeningElement in cytologyScreeningElements)
            {
                XElement reportElement = new XElement("CytologyScreening",                
                new XElement("ReportNo", cytologyScreeningElement.Element("ReportNo").Value),
                new XElement("PatientName", cytologyScreeningElement.Element("PatientName").Value),
                new XElement("OrderedBy", cytologyScreeningElement.Element("OrderedByName").Value),
                new XElement("AssignedTo", cytologyScreeningElement.Element("AssignedToName").Value),
                new XElement("Accessioned", cytologyScreeningElement.Element("AccessionTime").Value),
                new XElement("Screened", cytologyScreeningElement.Element("ScreeningFinalTime").Value),
                new XElement("Finaled", cytologyScreeningElement.Element("CaseFinalTime").Value));

                this.Add(reportElement);
            }
        }
    }
}
