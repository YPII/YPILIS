using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Monitor.Model
{
    public class PendingTestCollection : ObservableCollection<PendingTest>
    {
        public PendingTestCollection()
        {

        }

        public PendingTestCollection SortByDifference()
        {
            PendingTestCollection result = new PendingTestCollection();
            List<PendingTest> sortedList = this.OrderBy(x => x.State).ThenBy(x => x.Difference.TotalHours).ToList();
            foreach (PendingTest pendingTest in sortedList)
            {
                result.Add(pendingTest);
            }
            return result;
        }

        public void SetState()
        {
            foreach (PendingTest test in this)
            {
                test.SetState();
            }
        }
    }
}
