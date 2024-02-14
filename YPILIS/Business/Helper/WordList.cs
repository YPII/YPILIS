using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.DataHelper
{
	public class WordList
	{
        public static List<string> ClientNameList()
        {
            List<string> list = new List<string>();
            list.Add("Metropolis Clinic");
            list.Add("John Hopkins Hospital");
            list.Add("Dr Mickey Mouse");
            list.Add("Plastic Surgery R Us");
            return list;
        }

        public static List<string> FirstNameList()
        {
            List<string> list = new List<string>();
            list.Add("Bob");
            list.Add("Sara");
            list.Add("Sandy");
            list.Add("George");
            list.Add("Linda");
            list.Add("Vicki");
            list.Add("Sam");
            list.Add("Duane");
            list.Add("Lisa");
            return list;
        }

        public static List<string> LastNameList()
        {
            List<string> list = new List<string>();
            list.Add("Smith");
            list.Add("Jones");
            list.Add("Williams");
            list.Add("Brooks");
            list.Add("Jackson");
            list.Add("Tiller");
            list.Add("Woods");
            list.Add("Palmer");
            list.Add("Oneil");
            return list;
        }
	}
}
