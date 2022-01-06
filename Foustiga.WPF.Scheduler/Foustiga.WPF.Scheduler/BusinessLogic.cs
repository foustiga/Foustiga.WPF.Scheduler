using Foustiga.WPF.Scheduler.API;

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Foustiga.WPF.Scheduler
{
    internal class BusinessLogic
    {
        public DateTime GetNextOccurrence(IRecurrenceInfo recurrenceInfo, DateTime lastDate)
        {
            DateTime nextOccurrence = lastDate;// DateTime.Today;

            if (recurrenceInfo.Type == ReccurrenceType.Daily)
            {
                if (recurrenceInfo.WeekDays == WeekDays.EveryDay)
                { nextOccurrence = lastDate.AddDays(recurrenceInfo.Periodicity); }
                else if (recurrenceInfo.WeekDays == WeekDays.WorkDays)
                {
                    if (lastDate.DayOfWeek == System.DayOfWeek.Friday) { nextOccurrence = lastDate.AddDays(3); }
                    else if (lastDate.DayOfWeek == System.DayOfWeek.Saturday) { nextOccurrence = lastDate.AddDays(2); }
                    else if (lastDate.DayOfWeek == System.DayOfWeek.Sunday) { nextOccurrence = lastDate.AddDays(1); }
                    else { nextOccurrence = lastDate.AddDays(1); }
                }
             //   else { nextOccurrence = lastDate; } //Unknown WeekDays
            }
            else if (recurrenceInfo.Type == ReccurrenceType.Weekly)
            {//Calculate next date in the same week and in the next periodic week, and keep the lowest date.
                int currentWeekNumber = GetWeekNumberInYear(lastDate);
                List<DateTime> nextOccurrences = new();
                if (recurrenceInfo.WeekDays.HasFlag(WeekDays.Monday))
                {
                    nextOccurrence = lastDate.Next(DayOfWeek.Monday);
                    if (GetWeekNumberInYear(nextOccurrence) > currentWeekNumber) { nextOccurrence = nextOccurrence.AddDays(7 * (recurrenceInfo.Periodicity - 1)); }
                    nextOccurrences.Add(nextOccurrence);
                }
                if (recurrenceInfo.WeekDays.HasFlag(WeekDays.Tuesday))
                {
                    nextOccurrence = lastDate.Next(DayOfWeek.Tuesday);
                    if (GetWeekNumberInYear(nextOccurrence) > currentWeekNumber) { nextOccurrence = nextOccurrence.AddDays(7 * (recurrenceInfo.Periodicity - 1)); }
                    nextOccurrences.Add(nextOccurrence);
                }
                if (recurrenceInfo.WeekDays.HasFlag(WeekDays.Wednesday))
                {
                    nextOccurrence = lastDate.Next(DayOfWeek.Wednesday);
                    if (GetWeekNumberInYear(nextOccurrence) > currentWeekNumber) { nextOccurrence = nextOccurrence.AddDays(7 * (recurrenceInfo.Periodicity - 1)); }
                    nextOccurrences.Add(nextOccurrence);
                }
                if (recurrenceInfo.WeekDays.HasFlag(WeekDays.Thursday))
                {
                    nextOccurrence = lastDate.Next(DayOfWeek.Thursday);
                    if (GetWeekNumberInYear(nextOccurrence) > currentWeekNumber) { nextOccurrence = nextOccurrence.AddDays(7 * (recurrenceInfo.Periodicity - 1)); }
                    nextOccurrences.Add(nextOccurrence);
                }
                if (recurrenceInfo.WeekDays.HasFlag(WeekDays.Friday))
                {
                    nextOccurrence = lastDate.Next(DayOfWeek.Friday);
                    if (GetWeekNumberInYear(nextOccurrence) > currentWeekNumber) { nextOccurrence = nextOccurrence.AddDays(7 * (recurrenceInfo.Periodicity - 1)); }
                    nextOccurrences.Add(nextOccurrence);
                }
                if (recurrenceInfo.WeekDays.HasFlag(WeekDays.Saturday))
                {
                    nextOccurrence = lastDate.Next(DayOfWeek.Saturday);
                    if (GetWeekNumberInYear(nextOccurrence) > currentWeekNumber) { nextOccurrence = nextOccurrence.AddDays(7 * (recurrenceInfo.Periodicity - 1)); }
                    nextOccurrences.Add(nextOccurrence);
                }
                if (recurrenceInfo.WeekDays.HasFlag(WeekDays.Sunday))
                {
                    nextOccurrence = lastDate.Next(DayOfWeek.Sunday);
                    if (GetWeekNumberInYear(nextOccurrence) > currentWeekNumber) { nextOccurrence = nextOccurrence.AddDays(7 * (recurrenceInfo.Periodicity - 1)); }
                    nextOccurrences.Add(nextOccurrence);
                }
                nextOccurrence = (from DateTime oc in nextOccurrences orderby oc where oc > lastDate select oc).FirstOrDefault();
            }
            else if (recurrenceInfo.Type == ReccurrenceType.Monthly)
            {
                if (recurrenceInfo.WeekOfMonth == WeekOfMonth.None)
                {
                    int maxDaysInMonth = DateTime.DaysInMonth(lastDate.Year, lastDate.Month);
                    if (maxDaysInMonth > recurrenceInfo.DayNumber) { nextOccurrence = new DateTime(lastDate.Year, lastDate.Month, recurrenceInfo.DayNumber); }
                    else
                    {
                        nextOccurrence = new DateTime(lastDate.Year, lastDate.Month, maxDaysInMonth);
                    }
                    if (nextOccurrence <= lastDate) 
                        { 
                            nextOccurrence = nextOccurrence.AddMonths(recurrenceInfo.Periodicity);
                            maxDaysInMonth = DateTime.DaysInMonth(nextOccurrence.Year, nextOccurrence.Month);
                            if (maxDaysInMonth > recurrenceInfo.DayNumber) { nextOccurrence = new DateTime(nextOccurrence.Year, nextOccurrence.Month, recurrenceInfo.DayNumber); }
                            else { nextOccurrence = new DateTime(nextOccurrence.Year, nextOccurrence.Month, maxDaysInMonth); }
                    }
                        //nextOccurrence = nextOccurrence.AddDays(1).AddMonths(recurrenceInfo.Periodicity).AddDays(-1);
                        //if (nextOccurrence.Day > recurrenceInfo.DayNumber) { nextOccurrence = new DateTime(nextOccurrence.Year, nextOccurrence.Month, recurrenceInfo.DayNumber); }


                    //if (recurrenceInfo.DayNumber > lastDate.Day)
                    //{//Check that the DayNumber is not out of range for shorter months.
                    //    int maxDaysInMonth = DateTime.DaysInMonth(lastDate.Year, lastDate.Month);
                    //    if (maxDaysInMonth >= recurrenceInfo.DayNumber) { nextOccurrence = new DateTime(lastDate.Year, lastDate.Month, recurrenceInfo.DayNumber); }
                    //    else
                    //    {
                    //        nextOccurrence = new DateTime(lastDate.Year, lastDate.Month, maxDaysInMonth);
                    //        nextOccurrence = nextOccurrence.AddDays(1).AddMonths(recurrenceInfo.Periodicity).AddDays(-1);
                    //        if (nextOccurrence.Day > recurrenceInfo.DayNumber) { nextOccurrence = new DateTime(nextOccurrence.Year, nextOccurrence.Month, recurrenceInfo.DayNumber); }
                    //    }
                    //}
                    //else
                    //{
                    //    nextOccurrence = new DateTime(lastDate.Year, lastDate.Month, recurrenceInfo.DayNumber);
                    //    nextOccurrence = nextOccurrence.AddMonths(recurrenceInfo.Periodicity);
                    //}
                }
                else
                {
                    int weekNumberInTheMonth = 1;
                    if (recurrenceInfo.WeekOfMonth == WeekOfMonth.First) { weekNumberInTheMonth = 1; }
                    else if (recurrenceInfo.WeekOfMonth == WeekOfMonth.Second) { weekNumberInTheMonth = 2; }
                    else if (recurrenceInfo.WeekOfMonth == WeekOfMonth.Third) { weekNumberInTheMonth = 3; }
                    else if (recurrenceInfo.WeekOfMonth == WeekOfMonth.Fourth) { weekNumberInTheMonth = 4; }
                    else if (recurrenceInfo.WeekOfMonth == WeekOfMonth.Last) { weekNumberInTheMonth = 5; }

                    nextOccurrence = GetNextWeekDayInNthWeekInTheMonth(recurrenceInfo: recurrenceInfo, fromDate: lastDate, weekNumberInTheMonth: weekNumberInTheMonth);

                    if (nextOccurrence <= lastDate) //if we got back a former date, go to next periodicity
                    {
                        lastDate = lastDate.AddMonths(recurrenceInfo.Periodicity);
                        nextOccurrence = GetNextWeekDayInNthWeekInTheMonth(recurrenceInfo: recurrenceInfo, fromDate: lastDate, weekNumberInTheMonth: weekNumberInTheMonth);
                    }
                }
            }
            else if (recurrenceInfo.Type == ReccurrenceType.Yearly)
            {
                if (recurrenceInfo.WeekOfMonth == WeekOfMonth.None)
                {
                    // Check that the DayNumber is not out of range for shorter months.
                    int maxDaysInMonth = DateTime.DaysInMonth(lastDate.Year, recurrenceInfo.Month);
                    int actualDay = recurrenceInfo.DayNumber;
                    if (recurrenceInfo.DayNumber > maxDaysInMonth) { actualDay = maxDaysInMonth; }

                    nextOccurrence = new DateTime(lastDate.Year, recurrenceInfo.Month, actualDay);
                    if (nextOccurrence <= lastDate) 
                    { 
                        nextOccurrence = nextOccurrence.AddYears(1);
                        // Check that the DayNumber is not out of range for shorter months.
                        maxDaysInMonth = DateTime.DaysInMonth(nextOccurrence.Year, nextOccurrence.Month);
                        actualDay = recurrenceInfo.DayNumber;
                        if (recurrenceInfo.DayNumber > maxDaysInMonth) 
                        { 
                            actualDay = maxDaysInMonth;
                            nextOccurrence = new DateTime(nextOccurrence.Year, nextOccurrence.Month, actualDay);
                        }
                    }
                }
                else
                {
                    int weekNumberInTheMonth = 1;
                    if (recurrenceInfo.WeekOfMonth == WeekOfMonth.First) { weekNumberInTheMonth = 1; }
                    else if (recurrenceInfo.WeekOfMonth == WeekOfMonth.Second) { weekNumberInTheMonth = 2; }
                    else if (recurrenceInfo.WeekOfMonth == WeekOfMonth.Third) { weekNumberInTheMonth = 3; }
                    else if (recurrenceInfo.WeekOfMonth == WeekOfMonth.Fourth) { weekNumberInTheMonth = 4; }
                    else if (recurrenceInfo.WeekOfMonth == WeekOfMonth.Last) { weekNumberInTheMonth = 5; }

                    DateTime monthDate = new DateTime(lastDate.Year, recurrenceInfo.Month, 1);
                    nextOccurrence = GetNextWeekDayInNthWeekInTheMonth(recurrenceInfo: recurrenceInfo, fromDate: monthDate, weekNumberInTheMonth: weekNumberInTheMonth);

                    if (nextOccurrence <= lastDate) //if we got back a former date, go to next periodicity
                    {
                        nextOccurrence = nextOccurrence.AddYears(1);
                        monthDate = new DateTime(nextOccurrence.Year, nextOccurrence.Month, 1);
                        nextOccurrence = GetNextWeekDayInNthWeekInTheMonth(recurrenceInfo: recurrenceInfo, fromDate: monthDate, weekNumberInTheMonth: weekNumberInTheMonth);
                    }
                }
            }

            return nextOccurrence;
        }


        #region Private helper methods
        private DateTime GetNextWeekDayInNthWeekInTheMonth(IRecurrenceInfo recurrenceInfo, DateTime fromDate, int weekNumberInTheMonth)
        {
            DateTime nextOccurrence = fromDate;
            if (recurrenceInfo.WeekDays.HasFlag(WeekDays.Monday)) { nextOccurrence = nthDay(nextOccurrence, weekNumberInTheMonth, DayOfWeek.Monday); }
            else if (recurrenceInfo.WeekDays.HasFlag(WeekDays.Tuesday)) { nextOccurrence = nthDay(nextOccurrence, weekNumberInTheMonth, DayOfWeek.Tuesday); }
            else if (recurrenceInfo.WeekDays.HasFlag(WeekDays.Wednesday)) { nextOccurrence = nthDay(nextOccurrence, weekNumberInTheMonth, DayOfWeek.Wednesday); }
            else if (recurrenceInfo.WeekDays.HasFlag(WeekDays.Thursday)) { nextOccurrence = nthDay(nextOccurrence, weekNumberInTheMonth, DayOfWeek.Thursday); }
            else if (recurrenceInfo.WeekDays.HasFlag(WeekDays.Friday)) { nextOccurrence = nthDay(nextOccurrence, weekNumberInTheMonth, DayOfWeek.Friday); }
            else if (recurrenceInfo.WeekDays.HasFlag(WeekDays.Saturday)) { nextOccurrence = nthDay(nextOccurrence, weekNumberInTheMonth, DayOfWeek.Saturday); }
            else if (recurrenceInfo.WeekDays.HasFlag(WeekDays.Sunday)) { nextOccurrence = nthDay(nextOccurrence, weekNumberInTheMonth, DayOfWeek.Sunday); }

            return nextOccurrence;
        }
        private enum DayType
        {
            WorkDay,
            WeekendDay
        }

        private int GetWeekNumberInYear(DateTime date)
        {
            DayOfWeek firstDayOfWeek = CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek;
           // firstDayOfWeek = DayOfWeek.Sunday;
            int wk = CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(date, CalendarWeekRule.FirstFourDayWeek, firstDayOfWeek);
            return wk;


            //int wk = ISOWeek.GetWeekOfYear(date);
            //return wk;

        }

        private int GetWeekNumberOfMonth(DateTime date)
        {
            date = date.Date;
            DateTime firstMonthDay = new DateTime(date.Year, date.Month, 1);
            int weekNumberInTheMonth = 0;

            int a = GetWeekNumberInYear(date);
            int b = GetWeekNumberInYear(firstMonthDay);
            if (b > a) { weekNumberInTheMonth = a; }// Means we are in January and 1st of January is in the previous year week.
            else { weekNumberInTheMonth = a - b + 1; }

            return weekNumberInTheMonth;
        }


        /// <summary>
        /// Find the Nth DayOfWeek in a specified month and year
        /// </summary>
        /// <param name="nthWeek">1 = first or last, 2 = second/next to last, etc.</param>
        private DateTime nthDay(DateTime fromDate, int nthWeek, DayOfWeek dayOfWeek)
        {
            //Step 1: get first dayOfWeek in the month
            DateTime startDate = new DateTime(fromDate.Year, fromDate.Month, 1);
            DateTime startDatePreviousDay = startDate.AddDays(-1);
            DateTime firstDayOfWeekInTheMonth = GetNextDateByWeekDay(startDatePreviousDay, dayOfWeek);

            //Step 2 : get nth week of DayOfWeek in the month
            DateTime nextDayOfInTheMonth = firstDayOfWeekInTheMonth.AddDays(7 * (nthWeek - 1));

            //Step 3 : if we reached the next month, get back to stay on the same month
            while (nextDayOfInTheMonth.Month > startDate.Month || nextDayOfInTheMonth.Year > startDate.Year)
            {//Too far, we are on the next month. Get back.
                nextDayOfInTheMonth = nextDayOfInTheMonth.AddDays(-7); //Get back 1 week.
            }

            return nextDayOfInTheMonth;
        }
        private DateTime GetNextDateByWeekDay(DateTime from, DayOfWeek dayOfWeek)
        {
            var date = from.Date.AddDays(1);
            var days = ((int)dayOfWeek - (int)date.DayOfWeek + 7) % 7;
            return date.AddDays(days);

        }
        #endregion Private helper methods

    }

    #region DateTime Extensions
    public static class DateTimeExtensions
    {

        public static DateTime Next(this DateTime from, DayOfWeek dayOfTheWeek)
        {
            var date = from.Date.AddDays(1);
            var days = ((int)dayOfTheWeek - (int)date.DayOfWeek + 7) % 7;
            return date.AddDays(days);
        }

        public static DateTime Next(this DateTime from, int dayOfTheWeek)
        {
            var date = from.Date.AddDays(1);
            var days = (dayOfTheWeek - (int)date.DayOfWeek + 7) % 7;
            return date.AddDays(days);
        }

        public static IReadOnlyList<DateTime> Next(
            this DateTime from,
            params DayOfWeek[] days
        )
        {
            return Next(from, DateCalculationKind.And, days);
        }

        public static IReadOnlyList<DateTime> Next(
            this DateTime from,
            DateCalculationKind calculationKind,
            params DayOfWeek[] days
        )
        {
            if (days == null)
                return new DateTime[0];

            var results = new List<DateTime>();

            // And means, we don't want to duplicate data
            days = calculationKind == DateCalculationKind.And
                ? days.Distinct().ToArray()
                : days;

            DateTime? result = null;
            foreach (var dayOfWeek in days)
            {
                result = calculationKind == DateCalculationKind.And || result is null
                    ? Next(from, dayOfWeek)
                    : Next(result.Value, dayOfWeek);

                results.Add(result.Value);
            }

            return results;
        }
    }

    public enum DateCalculationKind
    {
        /// <summary>
        /// Will uniquely calculate the next day of the
        /// week. This will perform a `distinct` on the
        /// collection of days, and give back a unique result
        /// set with each calculation performed with
        /// the `from` parameter.
        /// </summary>
        And,
        /// <summary>
        /// Will use the previous result to calculate the
        /// next date. This allows for duplicate days of
        /// the week and will be calculated in the order
        /// of the days passed in.
        /// </summary>
        AndThen
    }
   
    
    public static class ExtensionsMethods
    {
        public static DayOfWeek DayOfWeek(this DateTime value, DayOfWeek firstDayOfWeek)
        {
            var idx = 7 + (int)value.DayOfWeek - (int)firstDayOfWeek;
            if (idx > 6)
            {
                idx -= 7;
            }
            return (DayOfWeek)idx;
        }
    }
    
    #endregion DateTime Extensions

}
