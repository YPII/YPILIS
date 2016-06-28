using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Specimen.Model
{
    public class TimeToFixationType : ObservableCollection<string>
    {
        public const string LessThanOneHour = "< 1 hr";
        public const string GreaterThanOneHour = "> 1 hr";
        public const string Unknown = "Unknown";
        public const string NotApplicable = "Not Applicable";        


        public static ObservableCollection<string> GetTimeToFixationTypeCollection()
        {
            ObservableCollection<string> result = new ObservableCollection<string>();
            result.Add(LessThanOneHour);
            result.Add(GreaterThanOneHour);
            result.Add(Unknown);
            result.Add(NotApplicable);                       
            return result;
        }
    }
}
