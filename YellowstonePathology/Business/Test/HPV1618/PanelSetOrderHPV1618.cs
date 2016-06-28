using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Test.HPV1618
{
	[PersistentClass("tblPanelSetOrderHPV1618", "tblPanelSetOrder", "YPIDATA")]
	public class PanelSetOrderHPV1618 : PanelSetOrder
	{        
		private string m_HPV16Result;
		private string m_HPV16ResultCode;
        private string m_HPV18Result;
        private string m_HPV18ResultCode;
        private string m_References;
		private string m_Method;		        
        private string m_Comment;

		public PanelSetOrderHPV1618()
		{

		}

		public PanelSetOrderHPV1618(string masterAccessionNo, string reportNo, string objectId,
			YellowstonePathology.Business.PanelSet.Model.PanelSet panelSet,
			YellowstonePathology.Business.Interface.IOrderTarget orderTarget,
			bool distribute)
			: base(masterAccessionNo, reportNo, objectId, panelSet, orderTarget, distribute)
		{
            this.m_TechnicalComponentInstrumentId = Instrument.HOLOGICPANTHERID;
            this.m_Method = HPV1618Result.Method;
            this.m_References = HPV1618Result.References;
        }               

		[PersistentProperty()]
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
        public string HPV16ResultCode
        {
            get { return this.m_HPV16ResultCode; }
            set
            {
                if (this.m_HPV16ResultCode != value)
                {
                    this.m_HPV16ResultCode = value;
                    this.NotifyPropertyChanged("HPV16ResultCode");
                }
            }
        }

        [PersistentProperty()]
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
        public string HPV18ResultCode
        {
            get { return this.m_HPV18ResultCode; }
            set
            {
                if (this.m_HPV18ResultCode != value)
                {
                    this.m_HPV18ResultCode = value;
                    this.NotifyPropertyChanged("HPV18ResultCode");
                }
            }
        }

        [PersistentProperty()]
		public string References
		{
            get { return this.m_References; }
			set
			{
                if (this.m_References != value)
				{
                    this.m_References = value;
                    this.NotifyPropertyChanged("References");
				}
			}
		}

		[PersistentProperty()]
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

        public override YellowstonePathology.Business.Rules.MethodResult IsOkToAccept()
        {
            YellowstonePathology.Business.Rules.MethodResult result = base.IsOkToAccept();
            if (result.Success == true)
            {
                if (string.IsNullOrEmpty(this.m_HPV16ResultCode) == true)
                {
                    result.Success = false;
                    result.Message = "The Genotype 16 result must be set before results may be accepted.";
                }
                else if (string.IsNullOrEmpty(this.m_HPV18ResultCode) == true)
                {
                    result.Success = false;
                    result.Message = "The Genotype 18 result must be set before results may be accepted.";
                }
            }
            return result;
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
