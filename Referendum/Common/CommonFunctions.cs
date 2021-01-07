using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Referendum
{
    public class CommonFunctions
    {
        public static string Filter(string str)
        {
            char[] removeChar = { ' ', '(', ')' };
            foreach (var c in removeChar)
            {
                str = str.Replace(c.ToString(), String.Empty);
            }
            return str;
        }
        public static string GetArmenianDateString(DateTime electionDay)
        {
            CultureInfo culture = new CultureInfo("hy-AM");

            return electionDay.Year + " թվականի " + electionDay.ToString("MMMM", culture) + "ի " +  electionDay.Day;
        }

        public static string GetDateString(DateTime? day)
        {
            if (day != null) {
                CultureInfo culture = new CultureInfo("hy-AM");

                DateTime noNullDay = (DateTime)day;

                return noNullDay.Day + " " + noNullDay.ToString("MMMM", culture) + ", " + noNullDay.Year;
            }
            else
            {
                return "";
            }
            
        }
        public static string GetEngDateString(DateTime? day)
        {
            if (day != null)
            {
                CultureInfo culture = new CultureInfo("en-EN");

                DateTime noNullDay = (DateTime)day;

                return noNullDay.Day + " " + noNullDay.ToString("MMMM", culture) + ", " + noNullDay.Year;
            }
            else
            {
                return "";
            }

        }
    }
}
