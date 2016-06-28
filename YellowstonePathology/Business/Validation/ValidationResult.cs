using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Validation
{
	public class ValidationResult
	{
		private string m_Message;
		private bool m_IsValid;

		public ValidationResult()
		{
		}

		public string Message
		{
			get { return this.m_Message; }
			set { this.m_Message = value; }
		}

		public bool IsValid
		{
			get { return this.m_IsValid; }
			set { this.m_IsValid = value; }
		}
	}
}
