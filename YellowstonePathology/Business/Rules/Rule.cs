using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Rules
{
	public class Rule : YellowstonePathology.Business.Interface.IRule
	{
		protected List<Action> m_ActionList;
		protected YellowstonePathology.Business.Rules.ExecutionStatus m_ExecutionStatus;

		protected string m_Name;
		protected bool m_Halted;
		protected string m_ResultComment;

		public Rule()
		{
			this.m_ActionList = new List<Action>();
		}

        public List<Action> ActionList
        {
            get { return this.m_ActionList; }
            set { this.m_ActionList = value; }
        }

        public ExecutionStatus ExecutionStatus
        {
            get { return this.m_ExecutionStatus; }
            set { this.m_ExecutionStatus = value; }
        }

		public string Name
		{
			get { return this.m_Name; }
			set { this.m_Name = value; }
		}

		public bool Halted
		{
			get { return this.m_Halted; }
		}

		public string ResultComment
		{
			get { return this.m_ResultComment; }
			set { this.m_ResultComment = value; }
		}

		public void Execute(YellowstonePathology.Business.Rules.ExecutionStatus executionStatus)
		{
			this.m_ExecutionStatus = executionStatus;
			this.Execute();
		}

		public void Execute()
		{
			foreach (Action action in this.m_ActionList)
			{
				action.Invoke();
				if (m_ExecutionStatus.IsExecutionHalted())
				{
					m_Halted = true;
					break;
				}
			}
		}

		public virtual T Execute<T>()
		{
			foreach (Action action in this.m_ActionList)
			{
				action.Invoke();
				if (m_ExecutionStatus.IsExecutionHalted() == true)
				{
					m_Halted = true;
					break;
				}
			}
			return default(T);
		}
	}
}
