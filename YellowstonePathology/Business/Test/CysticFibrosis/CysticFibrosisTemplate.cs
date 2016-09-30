using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.CysticFibrosis
{
	public class CysticFibrosisTemplate
	{
		private string m_TemplateName;
		private int m_TemplateId;

		public CysticFibrosisTemplate()
		{
		}

		public CysticFibrosisTemplate(string templateName, int templateId)
		{
			this.m_TemplateName = templateName;
			this.m_TemplateId = templateId;
		}

		public string TemplateName
		{
			get { return this.m_TemplateName; }
		}

		public int TemplateId
		{
			get { return this.m_TemplateId; }
		}
	}
}
