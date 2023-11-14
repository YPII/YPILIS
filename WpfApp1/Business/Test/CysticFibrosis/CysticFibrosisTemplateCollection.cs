using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace YellowstonePathology.Business.Test.CysticFibrosis
{
	public class CysticFibrosisTemplateCollection : ObservableCollection<CysticFibrosisTemplate>
	{
		public CysticFibrosisTemplateCollection()
		{
			this.Add(new CysticFibrosisTemplate("Ethnic Group Known Template", 1));
			this.Add(new CysticFibrosisTemplate("Ethnic Group UnKnown Template", 2));
			this.Add(new CysticFibrosisTemplate(null, 0));
		}        

		public CysticFibrosisTemplate GetById(int templateId)
		{
			CysticFibrosisTemplate result = null;
			foreach (CysticFibrosisTemplate template in this)
			{
				if (template.TemplateId == templateId)
				{
					result = template;
					break;
				}
			}
			return result;
		}
	}
}
