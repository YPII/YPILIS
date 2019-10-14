using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Test.AuthorizationForVerbalTestRequest
{
    [PersistentClass("tblAuthorizationForVerbalTestRequestTestOrder", "tblPanelSetOrder", "YPIDATA")]
    public class AuthorizationForVerbalTestRequestTestOrder : YellowstonePathology.Business.Test.PanelSetOrder
    {
        private string m_Comment;

        public AuthorizationForVerbalTestRequestTestOrder()
        { }

        public AuthorizationForVerbalTestRequestTestOrder(string masterAccessionNo, string reportNo, string objectId,
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

        public override string ToResultString(Business.Test.AccessionOrder accessionOrder)
        {
            string result = "Comment: " + this.Comment;
            return result;
        }
    }
}
