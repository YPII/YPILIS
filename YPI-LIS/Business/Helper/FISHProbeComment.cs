using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Helper
{
    public class FISHProbeComment
    {
        private YellowstonePathology.Business.Test.PanelSetOrderCPTCodeCollection m_PanelSetOrderCPTCodeCollection;
        private string m_Comment;
        private bool m_Success;
        private string m_Message;
        private Business.Billing.Model.CptCodeCollection m_FISHCPTCodes;

        public FISHProbeComment(YellowstonePathology.Business.Test.PanelSetOrderCPTCodeCollection panelSetOrderCPTCodeCollection)
        {
            this.m_PanelSetOrderCPTCodeCollection = panelSetOrderCPTCodeCollection;            
            Business.Billing.Model.CptCodeCollection allCPTCodes = Store.AppDataStore.Instance.CPTCodeCollection.Clone();
            this.m_FISHCPTCodes = allCPTCodes.GetFISHCPTCodeCollection();

            if (this.IsOKToCreateComment() == true)
            {
                StringBuilder result = new StringBuilder();
                StringBuilder temp = new StringBuilder();

                int quantity88374 = panelSetOrderCPTCodeCollection.GetCodeQuantity("88374");
                int quantity88377 = panelSetOrderCPTCodeCollection.GetCodeQuantity("88377");
                int quantity88368 = panelSetOrderCPTCodeCollection.GetCodeQuantity("88368");
                int quantity88369 = panelSetOrderCPTCodeCollection.GetCodeQuantity("88369");
                int quantity88367 = panelSetOrderCPTCodeCollection.GetCodeQuantity("88367");
                int quantity88373 = panelSetOrderCPTCodeCollection.GetCodeQuantity("88373");

                GetCodeResult(temp, quantity88374, " of computer assisted, multiplex stains or probe sets");
                GetCodeResult(temp, quantity88377, " of manual, multiplex stains or probe sets");
                GetCodeResult(temp, quantity88368 + quantity88369, " of manual, single stains or probe sets");
                GetCodeResult(temp, quantity88367 + quantity88373, " of computer assisted, single stains or probe sets");

                if (temp.Length > 0)
                {
                    result.Append("This analysis was performed using ");
                    result.Append(temp);
                    result.Append(".");
                }

                this.m_Comment = result.ToString();
            }            
        }        

        public string Comment
        {
            get { return this.m_Comment; }            
        }

        public bool Success
        {
            get { return this.m_Success; }
        }

        public string Message
        {
            get { return this.m_Message; }
        }

        private bool IsOKToCreateComment()
        {
            bool result = false;
            if (this.m_PanelSetOrderCPTCodeCollection.Exists(this.m_FISHCPTCodes) == true)
            {
                result = true;
                this.m_Success = true;    
            }
            else
            {
                this.m_Message = "FISH CPT Codes need to be added prior to generating the probe set comment.";
                this.m_Success = false;
            }
            return result;
        }        

        private void GetCodeResult(StringBuilder temp, int quantity, string comment)
        {
            if (quantity > 0)
            {
                if (temp.Length > 0) temp.Append("; ");
                temp.Append(quantity.ToString());
                temp.Append(quantity == 1 ? " set" : " sets");
                temp.Append(comment);
            }
        }
    }
}
