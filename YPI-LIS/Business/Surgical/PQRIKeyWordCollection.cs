using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Surgical
{
    public class PQRIKeyWordCollection : ObservableCollection<string>
    {
        public PQRIKeyWordCollection()
        {

        }

        public bool WordsExistIn(string text)
        {
            bool result = false;
            foreach (string keyWord in this)
            {
                if (string.IsNullOrEmpty(text) == false)
                {
                    if (text.ToUpper().Contains(keyWord.ToUpper()) == true)
                    {
                        result = true;
                        break;
                    }
                }
            }
            return result;
        }
    }
}
