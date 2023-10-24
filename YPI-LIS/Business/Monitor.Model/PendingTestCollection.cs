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

        public void SetState(YellowstonePathology.Business.Calendar.HolidayCollection holidays)
        {
            foreach (PendingTest test in this)
            {
                test.SetState(holidays);
            }
        }

        public PendingTestCollection GetCriticalTestsForMonitorPriority()
        {
            PendingTestCollection result = new Model.PendingTestCollection();
            YellowstonePathology.Business.PanelSet.Model.PanelSetCollection pendingCollection = Business.PanelSet.Model.PanelSetCollection.GetCriticalMonitorPriorityTests();
            foreach (PendingTest pendingTest in this)
            {
                if (pendingTest.State == MonitorStateEnum.Critical && pendingTest.PanelSetId == 13)
                {
                    if (pendingCollection.Exists(pendingTest.PanelSetId) == true)
                    {
                        result.Add(pendingTest);
                    }
                }
            }
            return result;
        }

        public PendingTestCollection GetNonCriticalTestsForMonitorPriority()
        {
            PendingTestCollection result = new Model.PendingTestCollection();
            YellowstonePathology.Business.PanelSet.Model.PanelSetCollection pendingCollection = Business.PanelSet.Model.PanelSetCollection.GetCriticalMonitorPriorityTests();
            foreach (PendingTest pendingTest in this)
            {
                if (pendingTest.State == MonitorStateEnum.Critical)
                {
                    if (pendingCollection.Exists(pendingTest.PanelSetId) == false)
                    {
                        result.Add(pendingTest);
                    }
                }
            }
            return result;
        }
    }
}
