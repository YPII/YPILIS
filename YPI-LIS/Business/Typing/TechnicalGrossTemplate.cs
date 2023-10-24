using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Typing
{
	public class TechnicalGrossTemplate : ParagraphTemplate
	{
		public TechnicalGrossTemplate()
		{
			this.Description = "Technical Gross Template";
			this.Text = "Specimen No: *SPECIMENNO*\r\nProcedure: *PROCEDURE* \r\nDimensions: *DIMENSION*\r\nSectioned: *SECTIONED*\r\n\r\n";

            this.WordCollection.Add(new TemplateWord("SPECIMENNO", "Specimen No:"));
			this.WordCollection.Add(new TemplateWord("PROCEDURE", "Procedure: Punch / Oriented Excision or Un-oriented Excision / Shave"));
			this.WordCollection.Add(new TemplateWord("DIMENSION", "Dimension: __X__X__ cm/mm"));
			this.WordCollection.Add(new TemplateWord("SECTIONED", "Sectioned: Bisected / Trisected / Serially sectioned / entirely submitted"));
		}
	}
}
