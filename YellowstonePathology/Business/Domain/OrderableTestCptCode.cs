using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Domain
{
    [Table(Name = "tblOrderableTestCptCode")]
	public partial class OrderableTestCptCode : BaseData
	{
        public OrderableTestCptCode()
        {

        }

        public XElement ToCptCodeElement()
        {
            XElement element = new XElement("CptCode", this.CptCode);
            return element;
        }
	}
}
