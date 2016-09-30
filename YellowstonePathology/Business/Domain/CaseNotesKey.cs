using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Domain
{
	public class CaseNotesKey
	{
		private string m_Key;
		private CaseNotesKeyNameEnum m_CaseNotesKeyNameEnum;

		public CaseNotesKey(CaseNotesKeyNameEnum caseNotesKeyNameEnum, string key)
		{
			this.m_CaseNotesKeyNameEnum = caseNotesKeyNameEnum;
			this.m_Key = key;
		}

		public string Key
		{
			get { return this.m_Key; }
		}

		public CaseNotesKeyNameEnum CaseNotesKeyName
		{
			get { return this.m_CaseNotesKeyNameEnum; }
		}
	}
}
