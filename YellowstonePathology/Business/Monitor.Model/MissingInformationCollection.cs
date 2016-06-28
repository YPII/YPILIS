using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Monitor.Model
{
    public class MissingInformationCollection : ObservableCollection<MissingInformation>
    {
        public MissingInformationCollection()
        {

        }

        public MissingInformationCollection SortByState()
        {
            MissingInformationCollection result = new MissingInformationCollection();
            List<MissingInformation> sortedList = this.OrderBy(x => x.State).ThenBy(x => x.OrderTime).ToList();
            foreach (MissingInformation missingInformation in sortedList)
            {
                result.Add(missingInformation);
            }
            return result;
        }

        public void SetState()
        {
            foreach (MissingInformation missingInformation in this)
            {
                missingInformation.SetState();
            }
        }
    }
}
