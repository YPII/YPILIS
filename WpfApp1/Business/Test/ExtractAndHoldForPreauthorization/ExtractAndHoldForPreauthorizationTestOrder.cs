using System;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Test.ExtractAndHoldForPreauthorization
{
    [PersistentClass("tblExtractAndHoldForPreauthorizationTestOrder", "tblPanelSetOrder", "YPIDATA")]
    public class ExtractAndHoldForPreauthorizationTestOrder : YellowstonePathology.Business.Test.PanelSetOrder
    {
        private string m_Comment;
        private int? m_TestId;
        private string m_CPTCodes;
        private string m_Fax;
        private string m_InternalComment;

        public ExtractAndHoldForPreauthorizationTestOrder()
        { }

        public ExtractAndHoldForPreauthorizationTestOrder(string masterAccessionNo, string reportNo, string objectId,
            YellowstonePathology.Business.PanelSet.Model.PanelSet panelSet,
            YellowstonePathology.Business.Interface.IOrderTarget orderTarget,
            bool distribute)
            : base(masterAccessionNo, reportNo, objectId, panelSet, orderTarget, distribute)
        { }

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

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "50", "null", "varchar")]
        public string InternalComment
        {
            get { return this.m_InternalComment; }
            set
            {
                if (this.m_InternalComment != value)
                {
                    this.m_InternalComment = value;
                    this.NotifyPropertyChanged("InternalComment");
                }
            }
        }

        [PersistentProperty()]
        public string CPTCodes
        {
            get { return this.m_CPTCodes; }
            set
            {
                if (this.m_CPTCodes != value)
                {
                    this.m_CPTCodes = value;
                    this.NotifyPropertyChanged("CPTCodes");
                }
            }
        }

        [PersistentProperty()]
        public int? TestId
        {
            get { return this.m_TestId; }
            set
            {
                if (this.m_TestId != value)
                {
                    this.m_TestId = value;
                    this.NotifyPropertyChanged("TestId");
                }
            }
        }

        [PersistentProperty()]
        public string Fax
        {
            get { return this.m_Fax; }
            set
            {
                if (this.m_Fax != value)
                {
                    this.m_Fax = value;
                    this.NotifyPropertyChanged("Fax");
                }
            }
        }

        public override string ToResultString(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {
            return this.m_Comment;
        }
    }
}
