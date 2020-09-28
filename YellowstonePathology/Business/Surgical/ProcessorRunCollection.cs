using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Surgical
{
    public class ProcessorRunCollection : ObservableCollection<ProcessorRun>
    {
        public ProcessorRunCollection()
        {

        }

        public ProcessorRun Get(string name)
        {
            ProcessorRun result = null;
            foreach (ProcessorRun run in this)
            {
                if (run.Name == name)
                {
                    result = run;
                    break;
                }
            }
            return result;
        }

        public static ProcessorRunCollection GetAll()
        {
            ProcessorRunCollection result = new ProcessorRunCollection();

            DateTime lastFriday = DateTime.Now.AddDays(-1);
            while (lastFriday.DayOfWeek != DayOfWeek.Friday)
            {
                lastFriday = lastFriday.AddDays(-1);
            }


            DateTime yesterdayAt550 = DateTime.Parse(DateTime.Today.AddDays(-1).ToString("yyyy-MM-dd") + "T17:50");
            DateTime yesterdayAt500 = DateTime.Parse(DateTime.Today.AddDays(-1).ToString("yyyy-MM-dd") + "T17:00");
            DateTime todayAtNoon = DateTime.Parse(DateTime.Today.ToString("yyyy-MM-dd") + "T12:00");
            DateTime fridayAt2100 = DateTime.Parse(lastFriday.ToString("yyyy-MM-dd") + "T21:00");
            DateTime todayAt1430 = DateTime.Parse(lastFriday.ToString("yyyy-MM-dd") + "T14:30");

            result.Add(new ProcessorRun("Chong, Overnight", yesterdayAt500, new TimeSpan(2, 30, 0)));
            result.Add(new ProcessorRun("Cheech, Overnight", yesterdayAt500, new TimeSpan(3, 10, 0)));
            result.Add(new ProcessorRun("Last Friday", fridayAt2100, new TimeSpan(2, 30, 0)));
            result.Add(new ProcessorRun("Long Mini", todayAtNoon, new TimeSpan(0, 60, 0)));
            result.Add(new ProcessorRun("Short Mini", todayAtNoon, new TimeSpan(0, 30, 0)));
            result.Add(new ProcessorRun("NMHC", todayAt1430, new TimeSpan(5, 0, 0)));
            return result;
        }
    }
}