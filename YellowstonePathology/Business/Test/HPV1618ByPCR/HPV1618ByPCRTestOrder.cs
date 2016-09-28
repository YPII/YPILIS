using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Test.HPV1618ByPCR
{
	[PersistentClass("tblHPV1618ByPCRTestOrder", "tblPanelSetOrder", "YPIDATA")]
	public class HPV1618ByPCRTestOrder : PanelSetOrder
	{
        private string m_Indication;
		private string m_HPV16Result;
		private string m_HPV18Result;
		private string m_Method;		
        private string m_Interpretation;
        private string m_Comment;

		public HPV1618ByPCRTestOrder()
		{

		}

		public HPV1618ByPCRTestOrder(string masterAccessionNo, string reportNo, string objectId,
			YellowstonePathology.Business.PanelSet.Model.PanelSet panelSet,
			YellowstonePathology.Business.Interface.IOrderTarget orderTarget,
			bool distribute)
			: base(masterAccessionNo, reportNo, objectId, panelSet, orderTarget, distribute)
		{
            
        }
        
        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "50", "null", "varchar")]
        public string Indication
        {
            get { return this.m_Indication; }
            set
            {
                if (this.m_Indication != value)
                {
                    this.m_Indication = value;
                    this.NotifyPropertyChanged("Indication");
                }
            }
        }

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "100", "null", "varchar")]
		public string HPV16Result
		{
			get { return this.m_HPV16Result; }
			set
			{
				if (this.m_HPV16Result != value)
				{
					this.m_HPV16Result = value;
					this.NotifyPropertyChanged("HPV16Result");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "100", "null", "varchar")]
		public string HPV18Result
		{
			get { return this.m_HPV18Result; }
			set
			{
				if (this.m_HPV18Result != value)
				{
					this.m_HPV18Result = value;
					this.NotifyPropertyChanged("HPV18Result");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "500", "null", "varchar")]
		public string Method
		{
			get { return this.m_Method; }
			set
			{
				if (this.m_Method != value)
				{
					this.m_Method = value;
					this.NotifyPropertyChanged("Method");
				}
			}
		}

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "5000", "null", "varchar")]
        public string Interpretation
        {
            get { return this.m_Interpretation; }
            set
            {
                if (this.m_Interpretation != value)
                {
                    this.m_Interpretation = value;
                    this.NotifyPropertyChanged("Interpretation");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "50", "null", "varchar")]
        public string Comment
        {
            get { return this.m_Comment; }
            set
            {
                if (this.m_Comment != value)
                {
                    this.m_Comment = value;
                    this.NotifyPropertyChanged("Comment");
                }
            }
        }

        public override bool IsOkToAddTasks()
        {
            bool result = false;
            if (this.m_OrderedOn == "Aliquot") result = true;
            return result;
        }

		public override string GetResultWithTestName()
		{
			StringBuilder result = new StringBuilder();
			result.Append("HPV16: ");
			result.Append(this.m_HPV16Result);
			result.Append(" HPV18: ");
			result.Append(this.m_HPV18Result);
			return result.ToString();
		}

		public override string ToResultString(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
		{
			StringBuilder result = new StringBuilder();
			result.Append("HPV16: ");
			result.AppendLine(this.m_HPV16Result);
			result.Append("HPV18: ");
			result.AppendLine(this.m_HPV18Result);
			return result.ToString();
		}
	}
}
