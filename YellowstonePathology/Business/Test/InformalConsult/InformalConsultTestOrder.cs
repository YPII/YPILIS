using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Test.InformalConsult
{
	[PersistentClass("tblInformalConsultTestOrder", "tblPanelSetOrder", "YPIDATA")]
	public class InformalConsultTestOrder : PanelSetOrder
	{
        private string m_Request;
        private string m_Result;

        public InformalConsultTestOrder()
		{
            
		}

		public InformalConsultTestOrder(string masterAccessionNo, string reportNo, string objectId,
            YellowstonePathology.Business.PanelSet.Model.PanelSet panelSet,
			bool distribute)
            : base(masterAccessionNo, reportNo, objectId, panelSet, distribute)
		{
            
		}

        [PersistentProperty()]
        public string Request
        {
            get { return this.m_Request; }
            set
            {
                if (this.m_Request != value)
                {
                    this.m_Request = value;
                    this.NotifyPropertyChanged("Request");
                }
            }
        }

        [PersistentProperty()]
        public string Result
        {
            get { return this.m_Result; }
            set
            {
                if (this.m_Result != value)
                {
                    this.m_Result = value;
                    this.NotifyPropertyChanged("Result");
                }
            }
        }

        public override string ToResultString(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
		{
			return this.m_Result;
		}
	}
}
