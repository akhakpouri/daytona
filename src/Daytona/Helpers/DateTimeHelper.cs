using System;

namespace Daytona.Helpers
{
    public static class DateTimeHelper
    {
        public static DateTime GetFirstDayOfQuarter(DateTime date)
        {
            var quarter = (date.Month - 1) / 3 + 1;
            return new DateTime(date.Year, (quarter - 1) * 3 + 1, 1);
        }

        public static DateTime GetLastDayOfQuarter(DateTime date)
        {
            var firstDay = GetFirstDayOfQuarter(date);
            return firstDay.AddMonths(3).AddDays(-1);
        }
        public static DateTime MinimumDate = new DateTime(1900, 1, 1);
        public static string MinimumDateToString = MinimumDate.ToString("MM-dd-yyyy");
        public static DateTime MaximumDate = new DateTime(2500, 12, 31);
        public static string MaximumDateToString = MaximumDate.ToString("MM-dd-yyyy");
    }
}
