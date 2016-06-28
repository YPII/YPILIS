using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Rules.Surgical
{
	public class WordSearchListItem
	{
		string m_Word;
		bool m_Logic;
		string m_Message;
		bool m_Found;

		public WordSearchListItem(string word, bool logic, string message)
		{
			this.m_Word = word;
			this.m_Logic = logic;
			this.m_Message = message;
		}

		public string Word
		{
			get { return this.m_Word; }
			set { this.m_Word = value; }
		}

		public bool Logic
		{
			get { return this.m_Logic; }
			set { this.m_Logic = value; }
		}

		public string Message
		{
			get { return this.m_Message; }
			set { this.m_Message = value; }
		}

		public bool Found
		{
			get { return this.m_Found; }
			set { this.m_Found = value; }
		}
	}
}
