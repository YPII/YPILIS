using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace YellowstonePathology.Business.Helper
{
    public class TextBoxHelper
    {
        public static bool IncrementDate(string inputString, ref Nullable<DateTime> targetDate, KeyEventArgs e)
        {
            bool result = false;
            if (e.Key == Key.Up || e.Key == Key.Down)
            {
                if (inputString.Length > 0)
                {
                    result = true;

                    DateTime dt;
                    bool ok = DateTime.TryParse(inputString, out dt);
                    if (ok)
                    {
                        if (e.Key == Key.Up)
                        {
                            dt = dt.AddDays(1);
                        }
                        else if (e.Key == Key.Down)
                        {
                            dt = dt.AddDays(-1);
                        }
                        Nullable<DateTime> ndt = dt;
                        targetDate = ndt;
                    }
                }                
            }
            return result;
        }
    }
}
