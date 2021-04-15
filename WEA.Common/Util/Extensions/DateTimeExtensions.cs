using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace WEA.Common.Util
{
    public static class DateTimeExtensions
    {
        public static DateTime GetMonthLastDate(this DateTime dateTime)
        {
            return new DateTime(dateTime.Year, dateTime.Month, DateTime.DaysInMonth(dateTime.Year, dateTime.Month));
        }

        public static DateTime GetDayLastTime(this DateTime dateTime)
        {
            return dateTime.Date.AddHours(23).AddMinutes(59).AddSeconds(59);
        }

        public static DateTime GetMonthStartDate(this DateTime dateTime)
        {
            return new DateTime(dateTime.Year, dateTime.Month, 1);
        }

        public static DateTime GetYearStartDat(this DateTime dateTime)
        {
            return new DateTime(dateTime.Year, 1, 1);
        }

        public static DateTime GetYearLastDate(this DateTime dateTime)
        {
            return new DateTime(dateTime.Year, 12, 31);
        }
        public static string GetDateToString(this DateTime dateTime)
        {
            int month = dateTime.Month;
            switch (month)
            {
                case 1:
                    return $"{dateTime.Day}-JAN-{dateTime.Year}";
                case 2:
                    return $"{dateTime.Day}-FEB-{dateTime.Year}";
                case 3:
                    return $"{dateTime.Day}-MAR-{dateTime.Year}";
                case 4:
                    return $"{dateTime.Day}-APR-{dateTime.Year}";
                case 5:
                    return $"{dateTime.Day}-MAY-{dateTime.Year}";
                case 6:
                    return $"{dateTime.Day}-JUN-{dateTime.Year}";
                case 7:
                    return $"{dateTime.Day}-JUL-{dateTime.Year}";
                case 8:
                    return $"{dateTime.Day}-AUG-{dateTime.Year}";
                case 9:
                    return $"{dateTime.Day}-SEPT-{dateTime.Year}";
                case 10:
                    return $"{dateTime.Day}-OCT-{dateTime.Year}";
                case 11:
                    return $"{dateTime.Day}-NOV-{dateTime.Year}";
                case 12:
                    return $"{dateTime.Day}-DEC-{dateTime.Year}";

            }

            return String.Empty;
        }

        public static string GetDateToStringCulture(this DateTime dateTime)
        {
            string month = dateTime.ToString("MMMM", new CultureInfo("az-Latn-AZ"));
            return $" \"{dateTime.Day}\"  \"{month}\" {GetYearWithPostfix(dateTime)} il";
        }

        public static string GetLongDateToStringCulture(this DateTime dateTime)
        {
            string month = dateTime.ToString("MMMM", new CultureInfo("az-Latn-AZ"));
            return $" {dateTime.Day}  {month} {GetYearWithPostfix(dateTime)} il, saat {dateTime.ToString("HH:mm")}";
        }



        public static string GetYearWithPostfix(this DateTime datetime)
        {
            int year = datetime.Year;
            switch (year % 10)
            {
                case 1:
                case 2:
                case 5:
                case 7:
                case 8:
                    return $"{year}-ci";
                case 3:
                case 4:
                    return $"{year}-cü";

                case 6:
                    return $"{year}-cı";

                case 9:
                    return $"{year}-cu";

                case 0:
                    switch (year % 100)
                    {
                        case 10:
                        case 30:
                            return $"{year}-cu";
                        case 20:
                        case 50:
                        case 70:
                            return $"{year}-ci";
                        case 80:
                        case 40:
                        case 60:
                        case 90:
                            return $"{year}-cı";
                        case 0:
                            if (year % 1000 != 0)
                                return $"{year}-cü";
                            else
                                return $"{year}-ci";
                    }
                    break;
            }

            return String.Empty;
        }
    }
}
