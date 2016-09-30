using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Surgical
{
    public class TimeToFixationExceptionCollection : ObservableCollection<TimeToFixationException>
    {
        public TimeToFixationExceptionCollection()
        {
            TimeToFixationException ttf1 = new TimeToFixationException();
            ttf1.TimeToFixationExceptionId = "";
            ttf1.Comment = "The specimen was picked (fresh) up as part of a normal run and the Time To Fixation is greater than 2 hours.";

            TimeToFixationException ttf2 = new TimeToFixationException();
            ttf2.TimeToFixationExceptionId = "";
            ttf2.Comment = "The specimen was not accessioned withing 15 minutes of formalin being added. Time to fixation is less than 1 hour.";

            TimeToFixationException ttf3 = new TimeToFixationException();
            ttf3.TimeToFixationExceptionId = "";
            ttf3.Comment = "The specimen was not accessioned withing 15 minutes of formalin being added. Time to fixation is greater than 1 hour.";
        }
    }
}
