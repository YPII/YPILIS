using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Test.GrossOnly
{
    [PersistentClass("tblGrossOnlyResult", "tblPanelSetOrder", "YPIDATA")]
    public class GrossOnlyTestOrder : PanelSetOrder
	{
        private string m_GrossX;

		public GrossOnlyTestOrder()
		{
            
		}

        public GrossOnlyTestOrder(string masterAccessionNo, string reportNo, string objectId,
            YellowstonePathology.Business.PanelSet.Model.PanelSet panelSet,
            bool distribute)
			: base(masterAccessionNo, reportNo, objectId, panelSet, distribute)
		{
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "-1", "null", "text")]
        public string GrossX
        {
            get { return this.m_GrossX; }
            set
            {
                if (this.m_GrossX != value)
                {
                    this.m_GrossX = value;
                    this.NotifyPropertyChanged("GrossX");
                }
            }
        }

        public override string ToResultString(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
		{
			return string.Empty;
		}
	}
}
