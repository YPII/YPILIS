using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Text;

namespace YellowstonePathology.Business.Domain.Core
{	
	public class Template : DomainBase
	{
		private int m_TemplateId;
		private string m_TemplateName;		
		private string m_FileName;
        private bool m_Retired;

		public Template()
		{
			
		}		
		
		public int TemplateId
		{
			get { return this.m_TemplateId; }
			set
			{
				if (this.m_TemplateId != value)
				{
					this.m_TemplateId = value;
					this.NotifyPropertyChanged("TemplateId");
				}
			}
		}

		public string TemplateName
		{
			get { return this.m_TemplateName; }
			set
			{
				if (this.m_TemplateName != value)
				{
					this.m_TemplateName = value;
					this.NotifyPropertyChanged("TemplateName");
				}
			}
		}		

		public string FileName
		{
			get { return this.m_FileName; }
			set
			{
				if (this.m_FileName != value)
				{
					this.m_FileName = value;
					this.NotifyPropertyChanged("FileName");
				}
			}
		}

        public bool Retired
        {
            get { return this.m_Retired; }
            set
            {
                if (this.m_Retired != value)
                {
                    this.m_Retired = value;
                    this.NotifyPropertyChanged("Retired");
                }
            }
        }
	}
}
