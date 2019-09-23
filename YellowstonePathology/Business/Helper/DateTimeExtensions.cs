using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Helper
{
    public static class DateTimeExtensions
    {
        public static Nullable<DateTime> DateTimeNowNoSeconds()
        {
            Nullable<DateTime> result = DateTime.Parse(DateTime.Now.ToString("MM/dd/yyy HH:mm"));
            return result;
        }

        public static string AddSlashesSpacesAndColon(string input)
        {
            string result = input;
            if (string.IsNullOrEmpty(input) == false  && input.Length >= 6)
            {
                result = input.Trim();
                if (result.IndexOf("/") == -1)
                {
                    result = result.Insert(2, "/");
                    result = result.Insert(5, "/");
                }
                else if (result.IndexOf(" ") != -1)
                {
                    int indexOfSpace = result.IndexOf(" ");
                    if (result.IndexOf(":") == -1)
                    {
                        string timeSubstring = result.Substring(indexOfSpace + 1);
                        if (timeSubstring.Length == 4)
                        {
                            result = result.Insert(indexOfSpace + 3, ":");
                        }
                    }
                }
                else if (result.IndexOf(" ") == -1)
                {
                    result = result + " 00:00";
                }
            }            
            return result;
        }

        public static bool IsDateElementInRange(DateTime inputDate, DateTime startDate, DateTime endDate)
        {
            bool result = false;            
            if (inputDate >= startDate && inputDate <= endDate)
            {
                result = true;
            }
            return result;
        }

        public static string FormatDateTimeString(string shortcutString)
        {            
            string result = shortcutString;
            if (string.IsNullOrEmpty(shortcutString) == false)
            {
                string dateTimePattern1 = @"^\d{8}$";
                string dateTimePattern2 = @"^\d{12}$";
                string dateTimePattern3 = @"^\d{2}/\d{2}/\d{4} \d{4}$";
                string dateTimePattern4 = @"^\d{2}/\d{2}/\d{8}$";

                if (DoesFormatMatch(dateTimePattern1, shortcutString) == true)
                {
                    result = result.Insert(2, "/");
                    result = result.Insert(5, "/");
                }
                else if(DoesFormatMatch(dateTimePattern2, shortcutString) == true)
                {
                    result = result.Insert(2, "/");
                    result = result.Insert(5, "/");
                    result = result.Insert(10, " ");
                    result = result.Insert(13, ":");                    
                }
                else if(DoesFormatMatch(dateTimePattern3, shortcutString) == true)
                {
                    result = result.Insert(13, ":");
                }
                else if (DoesFormatMatch(dateTimePattern4, shortcutString) == true)
                {
                    result = result.Insert(10, " ");
                    result = result.Insert(13, ":");
                }                                                                
            }
            return result;
        }

        private static bool DoesFormatMatch(string dateFormat, string dateStringToMatch)
        {            
            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(dateFormat);
            System.Text.RegularExpressions.Match match = regex.Match(dateStringToMatch);
            return match.Success;
        }

        public static bool IsDateToday(DateTime dt)
        {
            bool result = false;
            if (dt.Day == DateTime.Today.Day && dt.Year == DateTime.Today.Year) result = true;
            return result;
        }

        public static bool IsDateYesterday(DateTime dt)
        {
            bool result = false;
            if (dt.Day == DateTime.Today.AddDays(-1).Day && dt.Year == DateTime.Today.AddDays(-1).Year) result = true;
            return result;
        }

        public static bool IsCorrectFormat(string formattedDateString)
        {
            bool result = false;
            if (string.IsNullOrEmpty(formattedDateString) == false)
            {
                if (formattedDateString.Length == 10)
                {
                    string dateTimePattern = @"^\d\d/\d\d/\d\d\d\d";
                    System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(dateTimePattern);
                    System.Text.RegularExpressions.Match match = regex.Match(formattedDateString);
                    result = match.Success;
                }
                else if(formattedDateString.Length == 16)
                {
                    string datePattern = @"^\d\d/\d\d/\d\d\d\d \d\d:\d\d";
                    System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(datePattern);
                    System.Text.RegularExpressions.Match match = regex.Match(formattedDateString);
                    result = match.Success;
                }
            }            
            return result;
        }

        public static bool IsStringAValidDate(string dateTime)
        {
            bool result = false;
            if (string.IsNullOrEmpty(dateTime) == false)
            {
                DateTime date = new DateTime();
                bool isDateValid = DateTime.TryParse(dateTime, out date);
                if (isDateValid == true)
                {
                    result = true;
                }
            }            
            return result;
        }

        public static bool DoesDateHaveTime(Nullable<DateTime> dateTime)
        {
            bool result = false;            
            if (dateTime.HasValue == true && dateTime.Value.TimeOfDay.Ticks != 0)
            {
                result = true;                
            }
            return result;
        }

        public static DateTime StartOfWeek(this DateTime dt, DayOfWeek startOfWeek)
        {
            int diff = dt.DayOfWeek - startOfWeek;
            if (diff < 0)
            {
                diff += 7;
            }
            return dt.AddDays(-1 * diff).Date;
        }

        public static DateTime StartOfLastWeek(this DateTime dt, DayOfWeek startOfWeek)
        {
            dt = dt.AddDays(-7);
            return DateTimeExtensions.StartOfWeek(dt, startOfWeek);            
        } 

        public static Nullable<DateTime> FromXmlString(string xmlDateString)
        {
            string dateStringNormalized = xmlDateString;
            if (string.IsNullOrEmpty(dateStringNormalized) == false && dateStringNormalized.IndexOf('Z') == dateStringNormalized.Length - 1)
            {
                dateStringNormalized = dateStringNormalized.Remove(dateStringNormalized.Length - 1, 1);
            }
            Nullable<DateTime> nullableResult = null;
            DateTime result = new DateTime();
            bool parsed = DateTime.TryParse(dateStringNormalized, out result);
            if (parsed == true)
            {
                nullableResult = result;
            }
            return nullableResult;
        }

        public static string ToXmlString(DateTime resultDate)
        {
            return resultDate.ToString("yyyy-MM-ddTHH:mm:ssZ");
        }        

        public static Nullable<DateTime> NullableDateTimeFromString(string dt)
        {
            Nullable<DateTime> result = new DateTime();
            if (dt != "null") result = DateTime.Parse(dt);
            return result;
        }

        public static Nullable<TimeSpan> NullableTimeSpanFromString(string ts)
        {
            Nullable<TimeSpan> result = new TimeSpan();
            if (ts != "null") result = TimeSpan.FromTicks(Convert.ToInt64(ts.ToString()));
            return result;
        }

        public static void ToXmlString(XElement element, Nullable<DateTime> value)
        {
            if (value.HasValue && value.Value.Year > 1900)
            {
                element.RemoveAttributes();
                element.Value = value.Value.ToString("yyyy-MM-ddTHH:mm:ssZ");
            }
            else
            {
                DateTimeExtensions.SetNilNameSpace(element);
            }
        }

        public static void ToXmlString(XElement element, Nullable<int> value)
        {
            if (value.HasValue)
            {
                element.RemoveAttributes();
                element.Value = value.Value.ToString();
            }
            else
            {
                DateTimeExtensions.SetNilNameSpace(element);
            }
        }

		public static void ToXmlString(XElement element, Nullable<decimal> value)
		{
			if (value.HasValue)
			{
				element.RemoveAttributes();
				element.Value = value.Value.ToString();
			}
			else
			{
				DateTimeExtensions.SetNilNameSpace(element);
			}
		}

		public static Nullable<int> NullableIntFromXmlElement(XElement element)
		{
			foreach (XAttribute attribute in element.Attributes())
			{
				if (attribute.Name.ToString().Contains("nil") && attribute.Value == "true")
				{
					return null;
				}
			}
			return Convert.ToInt32(element.Value);
		}

		public static Nullable<decimal> FromXmlString(XElement element)
		{
			foreach (XAttribute attribute in element.Attributes())
			{
				if (attribute.Name.ToString().Contains("nil") && attribute.Value == "true")
				{
					return null;
				}
			}
			return Convert.ToDecimal(element.Value);
		}

        public static void SetNilNameSpace(XElement element)
        {
            XNamespace xsi = "http://www.w3.org/2001/XMLSchema-instance";
            XAttribute nil = new XAttribute(xsi + "nil", true);
            element.ReplaceAll(nil);
        }

        public static string DateStringFromNullable(Nullable<DateTime> source)
        {
            string returnValue = source.HasValue ? source.Value.ToShortDateString() : string.Empty;
            return returnValue;
        }

        public static string DateAndTimeStringFromNullable(Nullable<DateTime> source)
        {
            string returnValue = source.HasValue ? source.Value.ToShortDateString() + " " + source.Value.ToLongTimeString() : string.Empty;
            return returnValue;
        }

        public static string DateAndTimeStringModifiedFromNullable(Nullable<DateTime> source)
        {
            string result = string.Empty;
            if (source.HasValue == true)
            {
                result += source.Value.ToShortDateString();
                if (source.Value.ToShortTimeString() != "12:00 AM")
                {
                    result += " " + source.Value.ToShortTimeString();
                }
            }
            return result;
        }

        public static Nullable<DateTime> FromDateTimeString(string source)
        {
            Nullable<DateTime> returnValue = null;
            if (source.Length > 0)
            {
                source = source.Replace('-', '/');
                int idx = source.LastIndexOf(':');
                if (idx == source.Length - 4)
                {
                    source = source.Substring(0, source.Length - 4);
                }
                returnValue = DateTime.Parse(source);
            }
            return returnValue;
        }

        public static Nullable<DateTime> FromMMDDYYYYString(string source)
        {
            Nullable<DateTime> returnValue = null;
            if (source.Length != 8)
            {
                return returnValue;
            }
            string fmtdate = source.Substring(0, 2) + "/" + source.Substring(2, 2) + "/" + source.Substring(4, 4);

            DateTime date = DateTime.Today;
            bool canParse = DateTime.TryParse(fmtdate, out date);
            if (canParse)
            {
                returnValue = date;
            }
            return returnValue;
        }

        public static bool FromMMDDYYYYString(string source, ref DateTime date)
        {
            if (source.Length != 8)
            {
                return false;
            }
            string fmtdate = source.Substring(0, 2) + "/" + source.Substring(2, 2) + "/" + source.Substring(4, 4);

            bool canParse = DateTime.TryParse(fmtdate, out date);
            if (canParse)
            {
                return canParse;
            }
            return canParse;
        }

        public static string ShortTimeStringFromNullable(Nullable<DateTime> source)
        {
            string returnValue = source.HasValue ? source.Value.ToString("H:mm") : string.Empty;
            return returnValue;
        }

		public static string ReportDateTimeFormat(Nullable<DateTime> date)
		{
			if (date == null)
			{
				return string.Empty;
			}

			string dateString = date.Value.ToShortDateString();
			string timeString = date.Value.ToShortTimeString();
			if (timeString != "12:00 AM")
			{
				dateString += " " + timeString;
			}
			return dateString;
		}

        public static string CombineDateAndTime(Nullable<DateTime> date, Nullable<DateTime> time)
        {
            string result = string.Empty;
            if (time.HasValue == true)
            {
                result = time.Value.ToString("MM/dd/yyyy HH:mm");
            }
            else
            {
                if (date.HasValue == true)
                {
                    result = date.Value.ToString("MM/dd/yyyy");
                }
            }
            return result;
        }

		public static void DateTimeToDateAndTime(Nullable<DateTime> sourceTime, ref Nullable<DateTime> dateField, ref Nullable<DateTime> timeField)
		{
			Nullable<DateTime> dt = sourceTime;
			timeField = dt;
			if (dt.HasValue)
			{
				dt = dt.Value.AddMinutes(-dt.Value.Hour * 60 - dt.Value.Minute);
			}
			dateField = dt;
		}

		public static Nullable<DateTime> DateTimeFromDateAndTime(Nullable<DateTime> dateField, Nullable<DateTime> timeField)
		{
			Nullable<DateTime> dt = dateField;
			if (dt.HasValue)
			{
				if (timeField.HasValue)
				{
					dt = dt.Value.AddMinutes(-dt.Value.Hour * 60 - dt.Value.Minute);
					dt = dt.Value.AddMinutes(timeField.Value.Hour * 60 + timeField.Value.Minute);
				}
			}
			return dt;
		}

        public static DateTime GetEndDate(DateTime startDate, TimeSpan timeSpan)
        {
            DateTime rollingDate = startDate;
            TimeSpan timeSpanOneHour = new TimeSpan(1, 0, 0);
            TimeSpan timeSpanMatch = new TimeSpan();
            YellowstonePathology.Business.HolidayCollection holidays = YellowstonePathology.Business.HolidayCollection.GetByDateRange(new DateTime(startDate.Year, 1, 1), new DateTime(startDate.Add(timeSpan).Year + 1, 12, 31));
            bool isHoliday = holidays.IsDateAHoliday(startDate);

            int dow = (int)startDate.DayOfWeek;
            if (dow == 0 || dow == 6 || isHoliday == true)
            {
                rollingDate = new DateTime(startDate.Year, startDate.Month, startDate.Day);
            }

            while (timeSpanMatch < timeSpan)
            {
                dow = (int)rollingDate.DayOfWeek;
                if (rollingDate.Hour == 0)
                {
                    isHoliday = holidays.IsDateAHoliday(rollingDate);
                    if (dow == 0 || dow == 6 || isHoliday == true)
                    {
                        rollingDate = rollingDate.AddHours(24);
                        continue;
                    }
                }
                if (dow != 0 && dow != 6 && isHoliday == false)
                {
                    timeSpanMatch = timeSpanMatch.Add(timeSpanOneHour);
                }
                rollingDate = rollingDate.AddHours(1);
            };

            if(rollingDate.Hour == 0)
            {
                rollingDate = new DateTime(rollingDate.Year, rollingDate.Month, rollingDate.Day, 17, 0, 0);
            }
            return rollingDate;
        }

        public static DateTime GetExpectedFinalTime(DateTime startDate, TimeSpan timeSpan)
        {
            DateTime result = GetEndDate(startDate, timeSpan);
			result = DateTime.Parse(result.Month + "/" + result.Day.ToString() + "/" + result.Year.ToString() + " 17:00");
			return result;
		}

        public static TimeSpan GetHoursBetween(DateTime startDate, DateTime endDate, HolidayCollection holidays)
        {
            if (startDate >= endDate) throw new Exception("GetWeekendAndHolidayHoursBetween: startDate is >= endDate");
            TimeSpan result = new TimeSpan();
            TimeSpan timeSpanOneHour = new TimeSpan(1, 0, 0);
            bool isHoliday = holidays.IsDateAHoliday(startDate);

            for (DateTime rollingDate = startDate; rollingDate < endDate; rollingDate = rollingDate.Add(timeSpanOneHour))
            {
                int dow = (int)rollingDate.DayOfWeek;
                if(rollingDate.Hour == 0)
                {
                    isHoliday = holidays.IsDateAHoliday(rollingDate);
                    if (dow == 0 || dow == 6 || isHoliday == true)
                    {
                        rollingDate = rollingDate.AddHours(23);
                        continue;
                    }
                }
                if (dow != 0 && dow != 6 && isHoliday == false)
                {
                    result = result.Add(timeSpanOneHour);
                }
            }

            return result;
        }

        public static DateTime? RemoveSeconds(DateTime? dateToSet)
        {
            if (dateToSet.HasValue)
            {
                return new DateTime(dateToSet.Value.Year, dateToSet.Value.Month, dateToSet.Value.Day, dateToSet.Value.Hour, dateToSet.Value.Minute, 0);
            }
            return dateToSet;
        }

        public static DateTime? DateFromNullableDateTime(DateTime? dateAndTime)
        {
            if (dateAndTime.HasValue)
            {
                return new DateTime(dateAndTime.Value.Year, dateAndTime.Value.Month, dateAndTime.Value.Day);
            }
            return dateAndTime;
        }

        public static HolidayCollection GetHolidays(int year)
        {
            HolidayCollection holidays = new HolidayCollection();
            //NEW YEARS
            DateTime newYearsDate = AdjustForWeekendHoliday(new DateTime(year, 1, 1).Date);
            Holiday newYear = new Business.Holiday("New Years", newYearsDate, false);
            holidays.Add(newYear);

            //MEMORIAL DAY  -- last monday in May
            DateTime memorialDay = new DateTime(year, 5, 31);
            DayOfWeek dayOfWeek = memorialDay.DayOfWeek;
            while (dayOfWeek != DayOfWeek.Monday)
            {
                memorialDay = memorialDay.AddDays(-1);
                dayOfWeek = memorialDay.DayOfWeek;
            }
            Holiday memorial = new Business.Holiday("Memorial Day", memorialDay.Date, false);
            holidays.Add(memorial);

            //INDEPENCENCE DAY
            DateTime independenceDay = AdjustForWeekendHoliday(new DateTime(year, 7, 4).Date);
            Holiday july4 = new Business.Holiday("Independence Day", independenceDay, false);
            holidays.Add(july4);

            //LABOR DAY -- 1st Monday in September
            DateTime laborDay = new DateTime(year, 9, 1);
            dayOfWeek = laborDay.DayOfWeek;
            while (dayOfWeek != DayOfWeek.Monday)
            {
                laborDay = laborDay.AddDays(1);
                dayOfWeek = laborDay.DayOfWeek;
            }
            Holiday labor = new Business.Holiday("Labor Day", laborDay.Date, false);
            holidays.Add(labor);

            //THANKSGIVING DAY - 4th Thursday in November
            var thanksgiving = (from day in Enumerable.Range(1, 30)
                                where new DateTime(year, 11, day).DayOfWeek == DayOfWeek.Thursday
                                select day).ElementAt(3);
            DateTime thanksgivingDay = new DateTime(year, 11, thanksgiving);
            Holiday turkey = new Business.Holiday("Thanksgiving Day", thanksgivingDay.Date, false);
            holidays.Add(turkey);

            //CHRISTMAS December 25th
            DateTime christmasDay = AdjustForWeekendHoliday(new DateTime(year, 12, 25).Date);
            Holiday christmas = new Business.Holiday("Christmas Day", christmasDay, false);
            holidays.Add(christmas);
            return holidays;
        }

        public static DateTime AdjustForWeekendHoliday(DateTime holiday)
        {
            if (holiday.DayOfWeek == DayOfWeek.Saturday)
            {
                return holiday.AddDays(-1);
            }
            else if (holiday.DayOfWeek == DayOfWeek.Sunday)
            {
                return holiday.AddDays(1);
            }
            else
            {
                return holiday;
            }
        }
    }
}