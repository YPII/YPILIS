using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace YellowstonePathology.Business.Common
{
    public class AmendmentSpellCheckList : List<SpellCheckListItem>
    {
        public AmendmentSpellCheckList(YellowstonePathology.Business.Amendment.Model.Amendment amendment)
        {
            PropertyInfo amendmentProperty = typeof(YellowstonePathology.Business.Amendment.Model.Amendment).GetProperty("Text");
            this.Add(SpellCheckListItem.CreateSpellCheckListItem(amendmentProperty, amendment));
        }		
	}
}
