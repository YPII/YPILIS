using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace YellowstonePathology.Business
{
    public class PathologistCalendarCollection
    {
        public PathologistCalendarCollection()
        {
            
        }

        public void InsertThisMonth()
        {
            PathologyCalendar pathologistCalendar = new PathologyCalendar();
        }

        public void Insert()
        {
            //MySqlCommand cmd = new MySqlCommand("Select JSONValue from tblHoliday where HolidayDate between @StartDate and @EndDate order by HolidayDate;");
            //cmd.CommandType = System.Data.CommandType.Text;
            //cmd.Parameters.AddWithValue("@StartDate", startDate);            
        }

    }    
}
