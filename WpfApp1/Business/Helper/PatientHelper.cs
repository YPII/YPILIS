using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Helper
{
    public class PatientHelper
    {
        public static int GetAge(DateTime birthDate)
        {
            DateTime today = DateTime.Today;
            int age = today.Year - birthDate.Year;

            if (birthDate > today.AddYears(-age))
            {
                age--;
            }

            return age;
        }

        public static string GetAccessionAge(Nullable<DateTime> birthDate, DateTime accessionDate)
        {
            string ageString = string.Empty;
            if (birthDate.HasValue)
            {
                System.TimeSpan timeSpan = new TimeSpan(accessionDate.Ticks - birthDate.Value.Ticks);
                long days = timeSpan.Days;
                if (days < 14)
                {
                    ageString = days.ToString() + "DO";
                }
                else if (days < 56)
                {
                    long weeks = days / 7;
                    ageString = weeks.ToString() + "WO";
                }
                else
                {
                    DateTime dt = birthDate.Value.AddMonths(24);
                    DateTime newdt = accessionDate;
                    if (dt.CompareTo(accessionDate) > 0)
                    {
                        int mos = 0;
                        newdt = newdt.AddMonths(-1);
                        while (newdt.CompareTo(birthDate.Value) >= 0)
                        {
                            mos++;
                            newdt = newdt.AddMonths(-1);
                        }
                        ageString = mos.ToString() + "MO";
                    }
                    else
                    {
                        int years = 0;
                        newdt = newdt.AddYears(-1);
                        while (newdt.CompareTo(birthDate.Value) >= 0)
                        {
                            years++;
                            newdt = newdt.AddYears(-1);
                        }
                        ageString = years.ToString() + "YO";
                    }
                }
            }
            return ageString;
        }

        public static string GetPatientName(string lastName, string firstName, string middleInitial)
        {
            // First Name Middle Initial Last Name
            StringBuilder result = new StringBuilder();
            if (!string.IsNullOrEmpty(firstName))
            {
                result.Append(firstName);
            }

            if (!string.IsNullOrEmpty(middleInitial))
            {
                if (result.Length > 0)
                {
                    result.Append(" ");
                }
                result.Append(middleInitial);
            }

            if (!string.IsNullOrEmpty(lastName))
            {
                if (result.Length > 0 && result[result.Length - 1] != ' ')
                {
                    result.Append(" ");
                }
                result.Append(lastName);
            }
            return result.ToString();
        }

        public static string GetPatientDisplayName(string lastName, string firstName, string middleInitial)
        {
            //Last Name, First Name Middle Initial 
            StringBuilder result = new StringBuilder();
            if (!string.IsNullOrEmpty(lastName))
            {
                result.Append(lastName);
            }

            if (!string.IsNullOrEmpty(firstName) || !string.IsNullOrEmpty(middleInitial))
            {
                if (result.Length > 0)
                {
                    result.Append(", ");
                }
            }

            if (!string.IsNullOrEmpty(firstName))
            {
                result.Append(firstName);
            }

            if (!string.IsNullOrEmpty(middleInitial))
            {
                if (result[result.Length - 1] != ' ')
                {
                    result.Append(" ");
                }
                result.Append(middleInitial);
            }
            return result.ToString();
        }

        public static string GetPatientInitials(string firstName, string lastName)
        {
            StringBuilder initials = new StringBuilder();
            if (!string.IsNullOrEmpty(firstName))
            {
                initials.Append(firstName[0]);
            }
            if (!string.IsNullOrEmpty(lastName))
            {
                initials.Append(lastName[0]);
            }
            return initials.ToString();
        }
    }
}
