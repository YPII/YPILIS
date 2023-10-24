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

namespace YellowstonePathology.Business.Reports
{
	public partial class POCRetensionReport : FixedDocument
	{		
        YellowstonePathology.Business.DataContext.YpiData m_DataContext;        
        System.Xml.Linq.XElement m_Document;

		public POCRetensionReport(DateTime startDate, DateTime endDate)
		{            
            this.m_DataContext = new YellowstonePathology.Business.DataContext.YpiData();
            YellowstonePathology.Business.Domain.XElementFromSql xelement = this.m_DataContext.GetPOCRetensionReport(startDate, endDate).Single<Domain.XElementFromSql>();
            this.m_Document = xelement.Document;

			InitializeComponent();

			this.DataContext = this;
		}

        public System.Xml.Linq.XElement Document
        {
            get { return this.m_Document; }
        }        	      
	}
}
