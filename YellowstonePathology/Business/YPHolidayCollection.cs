using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Data;
using MySql.Data.MySqlClient;

namespace YellowstonePathology.Business
{
    public class YPHolidayCollection : ObservableCollection<YPHoliday>
    {
        public YPHolidayCollection()
        {

        }

        public bool Exists(DateTime holidayDate)
        {
            YPHoliday result = this.FirstOrDefault(y => y.HolidayDate == holidayDate);
            return result == null ? false : true;
        }

        public YPHoliday Get(DateTime holidayDate)
        {
            YPHoliday result = this.FirstOrDefault(y => y.HolidayDate == holidayDate);
            return result;
        }

        public static YPHolidayCollection GetAll()
        {
            YPHolidayCollection result = new Business.YPHolidayCollection();
            DateTime startDate = new DateTime(2018, 1, 1);
            DateTime endDate = new DateTime(2120, 12, 31);
            result.Load(startDate, endDate);
            return result;
        }

        public static YPHolidayCollection GetByDateRange(DateTime startDate, DateTime endDate)
        {
            YPHolidayCollection result = new Business.YPHolidayCollection();
            result.Load(startDate, endDate);
            return result;
        }

        private void Load(DateTime startDate, DateTime endDate)
        {
            this.ClearItems();
            MySqlCommand cmd = new MySqlCommand("Select JSONValue from tblYPHoliday where HolidayDate between @StartDate and @EndDate order by HolidayDate;");
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
                        YPHoliday ypHoliday = JsonConvert.DeserializeObject<Business.YPHoliday>(dr[0].ToString(), new JsonSerializerSettings
                        {
                            TypeNameHandling = TypeNameHandling.All,
                            ObjectCreationHandling = ObjectCreationHandling.Replace
                        });
                        this.Add(ypHoliday);
                    }
                }
            }
        }

        public void DeleteHoliday(YPHoliday holiday)
        {
            MySqlCommand cmd = new MySqlCommand("Delete from tblYPHoliday where HolidayDate = @HolidayDate;");
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@HolidayDate", holiday.HolidayDate);
            using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                cmd.ExecuteNonQuery();
            }

            this.Remove(holiday);
        }
    }
}
