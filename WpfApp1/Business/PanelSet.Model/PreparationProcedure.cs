using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.PanelSet.Model
{
    public class PreparationProcedure
    {
        public const string Microdissection = "Microdissection";
        public const string ParaffinCurls = "ParaffinCurls";
        public const string UnstainedSlide = "Unstained Slide(s)";
        public const string Aliquot = "Aliquot";
		public const string Undefined = "Undefined";		

        public static List<string> GetList()
        {
            List<string> list = new List<string>();
            list.Add(Microdissection);
            list.Add(ParaffinCurls);
            list.Add(UnstainedSlide);
            list.Add(Aliquot);
			list.Add(Undefined);
            return list;
        }

        public static int GetIndexInList(List<string> list, string item)
        {
            int result = -1;
            for (int i=0; i<list.Count; i++)
            {
                if (list[i] == item)
                {
                    result = i;
                    break;
                }
            }
            return result;
        }
    }
}
