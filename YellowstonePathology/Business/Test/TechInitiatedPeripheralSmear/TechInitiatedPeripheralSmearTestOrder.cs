using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Test.TechInitiatedPeripheralSmear
{
	[PersistentClass("tblTechInitiatedPeripheralSmearTestOrder", "tblPanelSetOrder", "YPIDATA")]
	public class TechInitiatedPeripheralSmearTestOrder : YellowstonePathology.Business.Test.PanelSetOrder
	{

        private string m_TechnologistsQuestion;
        private string m_PathologistFeedback;
        private string m_CBCComment;

		public TechInitiatedPeripheralSmearTestOrder()
		{
            
		}

		public TechInitiatedPeripheralSmearTestOrder(string masterAccessionNo, string reportNo, string objectId,
            YellowstonePathology.Business.PanelSet.Model.PanelSet panelSet,
			bool distribute)
            : base(masterAccessionNo, reportNo, objectId, panelSet, distribute)
		{
            
		}

        public TechInitiatedPeripheralSmearTestOrder(string masterAccessionNo, string reportNo, string objectId,
            YellowstonePathology.Business.PanelSet.Model.PanelSet panelSet,
            YellowstonePathology.Business.Interface.IOrderTarget orderTarget,
            bool distribute,
            YellowstonePathology.Business.User.SystemIdentity systemIdentity)
            :base(masterAccessionNo, reportNo, objectId, panelSet, orderTarget, distribute)
        {

        }

		public override string ToResultString(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
		{
			return string.Empty;
		}

        public override YellowstonePathology.Business.Rules.MethodResult IsOkToFinalize()
        {
            YellowstonePathology.Business.Rules.MethodResult result = new Rules.MethodResult();
            if (this.Final == true)
            {
                result.Success = false;
                result.Message = "This case cannot be finalized because it is already finalized.";
            }
            return result;
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "500", "null", "varchar")]
        public string TechnologistsQuestion
        {
            get { return this.m_TechnologistsQuestion; }
            set
            {
                if (this.m_TechnologistsQuestion != value)
                {
                    this.m_TechnologistsQuestion = value;
                    this.NotifyPropertyChanged("TechnologistsQuestion");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "500", "null", "varchar")]
        public string PathologistFeedback
        {
            get { return this.m_PathologistFeedback; }
            set
            {
                if (this.m_PathologistFeedback != value)
                {
                    this.m_PathologistFeedback = value;
                    this.NotifyPropertyChanged("PathologistFeedback");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "500", "null", "varchar")]
        public string CBCComment
        {
            get { return this.m_CBCComment; }
            set
            {
                if (this.m_CBCComment != value)
                {
                    this.m_CBCComment = value;
                    this.NotifyPropertyChanged("CBCComment");
                }
            }
        }
    }
}
