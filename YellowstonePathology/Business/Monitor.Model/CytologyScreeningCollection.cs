using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace YellowstonePathology.Business.Monitor.Model
{
    public class CytologyScreeningCollection : ObservableCollection<CytologyScreening>
    {        
        public CytologyScreeningCollection()
        {

        }

        public CytologyScreeningCollection SortByState()
        {
            CytologyScreeningCollection result = new CytologyScreeningCollection();

            foreach (CytologyScreening cytologyScreening in this)
            {
                if (cytologyScreening.State == MonitorStateEnum.Critical)
                {
                    result.Add(cytologyScreening);
                }
            }

            foreach (CytologyScreening cytologyScreening in this)
            {
                if (cytologyScreening.State == MonitorStateEnum.Warning)
                {
                    result.Add(cytologyScreening);
                }
            }

            foreach (CytologyScreening cytologyScreening in this)
            {
                if (cytologyScreening.State == MonitorStateEnum.Normal)
                {
                    result.Add(cytologyScreening);
                }
            }

            return result;
        }

        public double AverageHoursSinceAccessioned
        {
            get
            {
                double totalMinutes = 0;
                foreach (CytologyScreening cytologyScreening in this)
                {
                    totalMinutes += cytologyScreening.HoursSinceAccessioned.TotalHours;
                }
                return totalMinutes / this.Count;
            }           
        }

        public void SetState()
        {
            foreach (CytologyScreening cytologyScreening in this)
            {
                cytologyScreening.SetState();
            }
        }
    }
}
