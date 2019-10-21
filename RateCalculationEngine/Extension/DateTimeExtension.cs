using System;
namespace RateCalculationEngine.Extension
{
    public static class DateTimeExtension
    {
        public static bool InRange(this DateTime dateToCheck, DateTime startTime, DateTime endTime)
        {
            return dateToCheck >= startTime && dateToCheck <= endTime;
        }

        public static bool IsWeekend(this DateTime dateToCheck)
        {
            return (dateToCheck.DayOfWeek == DayOfWeek.Saturday)
                || (dateToCheck.DayOfWeek == DayOfWeek.Sunday);
        }

        public static DateTime Next(this DateTime from, DayOfWeek dayOfWeek)
        {
            int start = (int)from.DayOfWeek;
            int target = (int)dayOfWeek;
            if (target <= start)
                target += 7;
            return from.AddDays(target - start);
        }
    }
}
