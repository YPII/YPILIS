using System;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Test.ExtractAndHoldForMolecular
{
    [PersistentClass("tblExtractAndHoldForMolecularTestOrder", "tblPanelSetOrder", "YPIDATA")]
    public class ExtractAndHoldForMolecularTestOrder : YellowstonePathology.Business.Test.PanelSetOrder
    {
        private string m_Comment;

        public ExtractAndHoldForMolecularTestOrder()
        { }

        public ExtractAndHoldForMolecularTestOrder(string masterAccessionNo, string reportNo, string objectId,
            YellowstonePathology.Business.PanelSet.Model.PanelSet panelSet,
            YellowstonePathology.Business.Interface.IOrderTarget orderTarget,
            bool distribute)
            : base(masterAccessionNo, reportNo, objectId, panelSet, orderTarget, distribute)
        { }

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

        public override string ToResultString(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {
            return this.m_Comment;
        }
    }
}
