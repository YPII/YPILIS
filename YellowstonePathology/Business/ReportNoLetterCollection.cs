using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace YellowstonePathology.Business
{
    public class ReportNoLetterCollection : ObservableCollection<ReportNoLetter>
    {
        private static ReportNoLetterCollection instance;

        public ReportNoLetterCollection() { }

        public static ReportNoLetterCollection Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = GetAll();
                }
                return instance;
            }
        }

        private static ReportNoLetterCollection GetAll()
        {
            ReportNoLetterCollection result = new Business.ReportNoLetterCollection();
            result.Add(new ReportNoLetterS());
            result.Add(new ReportNoLetterP());
            result.Add(new ReportNoLetterT());
            result.Add(new ReportNoLetterY());
            result.Add(new ReportNoLetterM());
            result.Add(new ReportNoLetterQ());
            result.Add(new ReportNoLetterR());
            result.Add(new ReportNoLetterA());
            result.Add(new ReportNoLetterF());
            result.Add(new ReportNoLetterI());

            return result;
        }

        public ReportNoLetter GetByLetter(string letter)
        {
            ReportNoLetter result = Instance.FirstOrDefault(r => r.Letter == letter);
            return result;
        }
    }
}
