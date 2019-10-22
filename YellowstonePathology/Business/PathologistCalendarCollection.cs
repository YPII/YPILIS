using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Data;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;

namespace YellowstonePathology.Business
{
    public class PathologistCalendarCollection : ObservableCollection<PathologistCalendar>
    {

        private PathologistCalendarCollection()
        {

        }

        private static PathologistCalendarCollection Load(DateTime startDate, DateTime endDate)
        {
            PathologistCalendarCollection result = new PathologistCalendarCollection();
            MySqlCommand cmd = new MySqlCommand("Select JSONValue from tbPathologistCalendarCollection where CalendarDate between @StartDate and @EndDate;");
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@StartDate", startDate);
            cmd.Parameters.AddWithValue("@EndDate", endDate);

            using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        YellowstonePathology.Business.PathologistCalendar pathologistCalendar = JsonConvert.DeserializeObject<PathologistCalendar>(dr[0].ToString(), new JsonSerializerSettings
                        {
                            TypeNameHandling = TypeNameHandling.All,
                            ObjectCreationHandling = ObjectCreationHandling.Replace,
                        });

                        result.Add(pathologistCalendar);
                    }
                }
            }
            result = Sort(result);
            return result;
        }

        private static PathologistCalendarCollection Sort(PathologistCalendarCollection unSortedCollection)
        {
            PathologistCalendarCollection result = new PathologistCalendarCollection();
            IOrderedEnumerable<PathologistCalendar> orderedResult = unSortedCollection.OrderBy(i => i.CalendarDate).ThenBy(i => i.PathologistName);
            foreach (PathologistCalendar pathologistCalendar in orderedResult)
            {
                result.Add(pathologistCalendar);
            }
            return result;
        }
    }
}
