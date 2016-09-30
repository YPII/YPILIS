using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Rules.Surgical
{
	public class WordSearchList : List<WordSearchListItem>
	{
		private ExecutionStatus m_ExecutionStatus;

		public WordSearchList()
		{
			this.m_ExecutionStatus = new ExecutionStatus();
		}

		public bool Search(string sourceString)
		{
			bool result = false;
			string searchString = sourceString.ToUpper();
			if (this.FindExcludedWords(searchString) == false)
			{
				result = this.FindIncludedWords(searchString);
			}
			return result;
		}

		private bool FindExcludedWords(string sourceString)
		{
			bool result = false;
			foreach (WordSearchListItem item in this)
			{
				if (item.Logic == false && sourceString.IndexOf(item.Word.ToUpper()) > -1)
				{                    
					result = true;
					break;
				}
			}
			return result;
		}

		private bool FindIncludedWords(string sourceString)
		{
			bool result = false;
			foreach (WordSearchListItem item in this)
			{
				if (item.Logic == true && sourceString.IndexOf(item.Word.ToUpper()) > -1)
				{
					this.ExecutionStatus.AddMessage(item.Message, false);
					result = true;
				}
			}
			return result;
		}

		public ExecutionStatus ExecutionStatus
		{
			get { return this.m_ExecutionStatus; }
		}
	}

}
