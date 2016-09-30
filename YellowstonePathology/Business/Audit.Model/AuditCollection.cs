using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Audit.Model
{
    public class AuditCollection : ObservableCollection<Audit>
    {
        private bool m_ActionRequired;
        private string m_Message;

        public AuditCollection()
        {

        }

        public void Run()
        {
            this.m_ActionRequired = false;
            this.m_Message = null;

            foreach (Audit audit in this)
            {
                audit.ActionRequired = false;
                audit.Message = new StringBuilder();

                audit.Run();
                if (audit.ActionRequired == true)
                {
                    this.m_ActionRequired = true;
                    this.m_Message += audit.Message;
                    break; 
                }
            }

            if (this.m_ActionRequired == false)
            {
                this.m_Message = "No action required.";
            }
        }

        public AuditResult Run2()
        {
            AuditResult result = new AuditResult();
            result.Status = AuditStatusEnum.OK;
            result.Message = null;            

            foreach (Audit audit in this)
            {
                audit.ActionRequired = false;
                audit.Message = new StringBuilder();
                audit.Run();

                if (audit.Status == AuditStatusEnum.Failure)
                {
                    result.Status = AuditStatusEnum.Failure;
                    result.Message += audit.Message;
                    break;
                }
                else if (audit.Status == AuditStatusEnum.Warning)
                {
                    result.Status = AuditStatusEnum.Warning;
                    result.Message += audit.Message;
                    break;
                }
            }

            return result;
        }

        public bool ActionRequired
        {
            get { return this.m_ActionRequired; }
        }

        public string Message
        {
            get { return this.m_Message; }
        }
    }
}
