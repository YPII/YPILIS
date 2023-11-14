using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.ClientOrder.Model
{
	public class FixationTypes : List<string>
	{
		public static FixationTypes m_Instance;
		
		private FixationTypes()
		{
			this.Add("Fresh");
			this.Add("Formalin");
			this.Add("B+ Fixative");
			this.Add("Cytolyt");
			this.Add("PreserveCyt");
			this.Add("Not Applicable");
			this.Add("95% IPA");
		}

		public static FixationTypes Instance
		{
			get
			{
				if (m_Instance == null)
				{
					m_Instance = new FixationTypes();
				}
				return m_Instance;
			}
		}
	}
}
