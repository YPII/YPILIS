using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Domain
{
    [Table(Name = "tblOrderableTestIcd9Code")]
	public partial class OrderableTestIcd9Code : BaseData
	{
        public OrderableTestIcd9Code()
        {

        }

        public XElement ToIcd9CodeElement()
        {
            XElement element = new XElement("Icd9Code", this.m_Icd9Code);
            return element;
        }
	}
}
