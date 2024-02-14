using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Rules
{
    public class ExecutionStatus
    {
        private bool m_Halted;
        private bool m_ShowMessage;
        protected bool m_ContinueExecutionOnHalt;		
        private List<ExecutionMessage> m_ExecutionMessages;
        private string m_SuccessMessage;
        private string m_FailureMessage;
        private object m_ReturnValue;

        public ExecutionStatus()
        {
            this.m_ExecutionMessages = new List<ExecutionMessage>();
            this.m_ShowMessage = false;
        }

        public bool Halted
        {
            get { return this.m_Halted; }
            set { this.m_Halted = value; }
        }

        public bool ContinueExecutionOnHalt
        {
            get { return this.m_ContinueExecutionOnHalt; }
            set { this.m_ContinueExecutionOnHalt = value; }
        }         

        public string SuccessMessage
        {
            get { return this.m_SuccessMessage; }
            set { this.m_SuccessMessage = value; }
        }

        public string FailureMessage
        {
            get { return this.m_FailureMessage; }
            set { this.m_FailureMessage = value; }
        }

        public bool ShowMessage
        {
            get { return this.m_ShowMessage; }
            set { this.m_ShowMessage = value; }
        }

        public object ReturnValue
        {
            get { return this.m_ReturnValue; }
            set { this.m_ReturnValue = value; }
        }

        public void AddMessage(string message, bool halted)
        {
            ExecutionMessage executionMessage = new ExecutionMessage();            
            executionMessage.Message = message;
            executionMessage.Halted = halted;
            this.m_ExecutionMessages.Add(executionMessage);
        }

        public string GetResultMessage()
        {
            string result = string.Empty;
            if (this.m_Halted == false)
            {
                result = this.m_SuccessMessage;
            }
            else
            {
                result = this.m_FailureMessage;
            }
            return result;
        }

        public string ExecutionMessagesString
        {
            get
            {
                string result = string.Empty;
                foreach (YellowstonePathology.Business.Rules.ExecutionMessage message in this.m_ExecutionMessages)
                {
                    result += message.Message;
                }
                return result;
            }
        }

        public List<ExecutionMessage> ExecutionMessages
        {
            get { return this.m_ExecutionMessages; }
        }        

		public bool IsExecutionHalted()
		{
			if (!m_Halted)
			{
				foreach (ExecutionMessage executionMessage in this.m_ExecutionMessages)
				{
					if (executionMessage.Halted == true)
					{
						m_Halted = true;
						break;
					}
				}
			}
			return m_Halted;
		}
	}
}
