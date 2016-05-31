using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Persistence
{
    public static class JSONIndenter
    {        
        public static void AddTabs(StringBuilder stringToIndent, int indentCount)
		{
            for (int idx = 0; idx < indentCount; idx++)
            {
                //stringToIndent.Append("\t");
                stringToIndent.Append("   ");
            }
        }        
    }
}
