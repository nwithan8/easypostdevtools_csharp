using System;
using System.Collections.Generic;

namespace EasyPostDevTools.utils
{
    public class Dates
    {
        private static readonly DateTime now = DateTime.Now;

        private static bool IsLastDayOfMonth(DateTime date)
        {
            // determine if the date is the last day of the month
            return date.Day == DateTime.DaysInMonth(date.Year, date.Month);
        }

        private static bool IsLastMonthOfYear(DateTime date)
        {
            return date.Month == 12;
        }

        private static bool IsLastDayOfYear(DateTime date)
        {
            return date.Month == 12 && date.Day == 31;
        }

        public static DateTime GetFutureDateThisYear()
        {
            // will return a date in the future, this year
            if (IsLastDayOfYear(now))
            {
                throw new Exception("This year is over.");
            }

            var month = IsLastDayOfMonth(now)
                ? Random.RandomIntInRange(now.Month + 1, 12)
                : Random.RandomIntInRange(now.Month, 12);

            var daysInMonth = DateTime.DaysInMonth(now.Year, month);
            var day = month == now.Month
                ? Random.RandomIntInRange(now.Day + 1, daysInMonth)
                : Random.RandomIntInRange(1, daysInMonth);

            return new DateTime(now.Year, month, day);
        }

        public static DateTime GetFutureDateThisMonth()
        {
            // will return a date in the future, this month
            if (IsLastDayOfMonth(now))
            {
                throw new Exception("This month is over.");
            }

            var day = Random.RandomIntInRange(now.Day + 1, DateTime.DaysInMonth(now.Year, now.Month));
            return new DateTime(now.Year, now.Month, day);
        }

        public static DateTime GetDateAfter(DateTime date)
        {
            if (date.Month == 12)
            {
                // if it's December, set up the next date to be in January
                date = new DateTime(date.Year + 1, 1, 1);
            }

            date = date.Add(new TimeSpan(Random.RandomIntInRange(1, 30), 0, 0, 0));
            return date;
        }

        public static DateTime GetDateBefore(DateTime date)
        {
            date = date.Subtract(new TimeSpan(Random.RandomIntInRange(1, 30), 0, 0, 0));
            return date;
        }

        public static List<DateTime> GetFutureDates(int numberOfDates)
        {
            // returns a list of dates in chronnological order
            var dates = new List<DateTime>();
            var currentDate = now;
            for (int i = 0; i < numberOfDates; i++)
            {
                currentDate = GetDateAfter(currentDate);
                dates.Add(currentDate);
            }

            return dates;
        }

        public static List<DateTime> GetPastDates(int numberOfDates)
        {
            // return a list of dates in reverse chronnological order
            var dates = new List<DateTime>();
            var currentDate = now;
            for (int i = 0; i < numberOfDates; i++)
            {
                currentDate = GetDateBefore(currentDate);
                dates.Add(currentDate);
            }

            return dates;
        }
    }
}