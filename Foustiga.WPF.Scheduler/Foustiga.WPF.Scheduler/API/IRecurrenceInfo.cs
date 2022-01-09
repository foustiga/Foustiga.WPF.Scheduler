using Foustiga.WPF.Scheduler.DataModel;
using Foustiga.WPF.Scheduler.Foundation.Enums;

using System;
using System.ComponentModel;

namespace Foustiga.WPF.Scheduler.API
{
    public interface IRecurrenceInfo : INotifyPropertyChanged
    {
        int DayNumber { get; set; } //Positive integer value that specifies the day number within a month.
        /*If the recurrence pattern is defined on a monthly or yearly time basis (the RecurrenceInfo.Type property is set to the RecurrenceType.Monthly or 
         * RecurrenceType.Yearly value), the DayNumber property specifies the number of the day within a month. This is used to calculate the date on which the 
         * corresponding appointment will reoccur. The effective values for this property are 1 to 31 inclusive which correspond to the days within a month.
         * */
        TimeSpan Duration { get; set; } //A TimeSpan﻿ value that specifies the duration of the recurrence.

        DateTime End { get; set; }  //A DateTime﻿ value that specifies the end date for the recurrence.

        DayOfWeek FirstDayOfWeek { get; set; } //A DayOfWeek﻿ enumeration value specifying the start day of the week
        
        int Id { get; set; }
        
        int Month { get; set; }  //A positive integer value that specifies the month number.

        int OccurrenceCount { get; set; }  //An integer value that specifies the number of occurrences.
        /*If the IRecurrenceInfo.Range property is set to the RecurrenceRange.OccurrenceCount value, 
         * the OccurrenceCount property can be used together with the RecurrenceInfo.Start property to define the range of recurrence - the period of time in which the appointment occurs. 
         * In this case, the range of recurrence can be represented by the number of occurrences that should elapse until the recurrence is no longer in effect.T
         * he OccurrenceCount property specifies the number of occurrences.
            The value of the OccurrenceCount property is ignored if the IRecurrenceInfo.Range property is set to either the RecurrenceRange.EndByDate or RecurrenceRange.NoEndDate value.
        */

        int Periodicity { get; set; }  //An integer value that specifies the frequency with which the corresponding appointment occurs.
        /*The base element that defines the recurrence’s behavior is frequency - the time base used to calculate when the corresponding appointment repeats: 
         * daily, weekly, monthly or yearly (see the Appointment.Type property). The Periodicity property specifies the interval which defines how the recurrence’s frequency 
         * is applied based upon the frequency type specified by the RecurrenceInfo.Type property (for instance, every Nth day; every Nth week, every Nth month, every Nth year).
         */

        RecurrenceRange Range { get; set; }  //A RecurrenceRange enumeration value that specifies the recurrence’s range type.
        
        DateTime Start { get; set; }  //A DateTime﻿ value that specifies the start date for the recurrence.
        
        ReccurrenceType Type { get; set; }  //A RecurrenceType enumeration value that specifies the recurrence frequency type.
        
        WeekDays WeekDays { get; set; }  //The WeekDays enumeration’s value specifying the day/days in a week.
        
        WeekOfMonth WeekOfMonth { get; set; }  //A WeekOfMonth enumeration value that specifies a week in every month.

        //void FromString(string recurrenceInfoString);
        //IRecurrenceInfo FromStringToClass(string recurrenceInfoString);
    }

    //public enum DayOfWeek
    //{
    //    Friday = 5,	//Indicates Friday.
    //    Monday = 1, //Indicates Monday.
    //    Saturday = 6, //Indicates Saturday.
    //    Sunday = 0, //Indicates Sunday.
    //    Thursday = 4, //Indicates Thursday.
    //    Tuesday = 2,  //Indicates Tuesday.
    //    Wednesday = 3, //Indicates Wednesday.
    //}

    public enum RecurrenceRange
    {
        NoEndDate, //recurring appointment will not have an end date.
        OccurrenceCount, //A recurring appointment will end after its recurrence count exceeds the value specified by the RecurrenceInfo.OccurrenceCount property.
        EndByDate //A recurring appointment will end after the date specified by the RecurrenceInfo.End property.
    }

    public enum ReccurrenceType
    {
        [EnumInfo(ResourceType = typeof(Assets.Resources),//where Resources is the Resources file to look into.
            Display = nameof(Assets.Resources.Enum_ReccurrenceType_Daily),
            Tooltip = nameof(Assets.Resources.Enum_ReccurrenceType_Daily_Tooltip))]
        Daily, //The recurring appointment reoccurs on a daily base.

        [EnumInfo(ResourceType = typeof(Assets.Resources),//where Resources is the Resources file to look into.
            Display = nameof(Assets.Resources.Enum_ReccurrenceType_Weekly),
            Tooltip = nameof(Assets.Resources.Enum_ReccurrenceType_Weekly_Tooltip))]
        Weekly, //The recurring appointment reoccurs on a weekly base.

        [EnumInfo(ResourceType = typeof(Assets.Resources),//where Resources is the Resources file to look into.
            Display = nameof(Assets.Resources.Enum_ReccurrenceType_Monthly),
            Tooltip = nameof(Assets.Resources.Enum_ReccurrenceType_Monthly_Tooltip))]
        Monthly, //The recurring appointment reoccurs on a monthly base.

        [EnumInfo(ResourceType = typeof(Assets.Resources),//where Resources is the Resources file to look into.
            Display = nameof(Assets.Resources.Enum_ReccurrenceType_Yearly),
            Tooltip = nameof(Assets.Resources.Enum_ReccurrenceType_Yearly_Tooltip))]
        Yearly//, //The recurring appointment reoccurs on an yearly base.
        //Minutely, //The recurring appointment reoccurs on a minute base.
        //Hourly //The recurring appointment reoccurs on an hourly base.
    }

    [Flags]
    public enum WeekDays
    {
        [EnumInfo(ResourceType = typeof(Assets.Resources),//where Resources is the Resources file to look into.
           Display = nameof(Assets.Resources.Enum_WeekDays_Sunday))] 
        Sunday = 1, //Specifies Sunday.

        [EnumInfo(ResourceType = typeof(Assets.Resources),//where Resources is the Resources file to look into.
            Display = nameof(Assets.Resources.Enum_WeekDays_Monday))]
        Monday = 2, //Specifies Monday.

        [EnumInfo(ResourceType = typeof(Assets.Resources),//where Resources is the Resources file to look into.
            Display = nameof(Assets.Resources.Enum_WeekDays_Tuesday))]
        Tuesday = 4, //Specifies Tuesday.

        [EnumInfo(ResourceType = typeof(Assets.Resources),//where Resources is the Resources file to look into.
            Display = nameof(Assets.Resources.Enum_WeekDays_Wednesday))]
        Wednesday = 8, //Specifies Wednesday.

        [EnumInfo(ResourceType = typeof(Assets.Resources),//where Resources is the Resources file to look into.
            Display = nameof(Assets.Resources.Enum_WeekDays_Thursday))]
        Thursday = 16, //Specifies Thursday.

        [EnumInfo(ResourceType = typeof(Assets.Resources),//where Resources is the Resources file to look into.
            Display = nameof(Assets.Resources.Enum_WeekDays_Friday))]
        Friday = 32, //Specifies Friday.

        [EnumInfo(ResourceType = typeof(Assets.Resources),//where Resources is the Resources file to look into.
            Display = nameof(Assets.Resources.Enum_WeekDays_Saturday))]
        Saturday = 64, //Specifies Saturday.

        [EnumInfo(ResourceType = typeof(Assets.Resources),//where Resources is the Resources file to look into.
            Display = nameof(Assets.Resources.Enum_WeekDays_WeekendDays))]
        WeekendDays = Sunday | Saturday, //Specifies Saturday and Sunday.

        [EnumInfo(ResourceType = typeof(Assets.Resources),//where Resources is the Resources file to look into.
            Display = nameof(Assets.Resources.Enum_WeekDays_WorkDays))]
        WorkDays = Monday | Tuesday | Wednesday | Thursday | Friday, //Specifies work days (Monday, Tuesday, Wednesday, Thursday and Friday).

        [EnumInfo(ResourceType = typeof(Assets.Resources),//where Resources is the Resources file to look into.
            Display = nameof(Assets.Resources.Enum_WeekDays_EveryDay))]
        EveryDay = WeekendDays | WorkDays, //Specifies every day of the week.
    }

    public enum WeekOfMonth
    {
        [EnumInfo(ResourceType = typeof(Assets.Resources),//where Resources is the Resources file to look into.
            Display = nameof(Assets.Resources.Enum_WeekOfMonth_None))]
        None, //There isn’t any recurrence rule based upon the weeks in a month.

        [EnumInfo(ResourceType = typeof(Assets.Resources),//where Resources is the Resources file to look into.
            Display = nameof(Assets.Resources.Enum_WeekOfMonth_First))]
        First, //The recurring event will occur once a month, on the specified day or days of the first week in the month.

        [EnumInfo(ResourceType = typeof(Assets.Resources),//where Resources is the Resources file to look into.
           Display = nameof(Assets.Resources.Enum_WeekOfMonth_Second))]
        Second, //The recurring event will occur once a month, on the specified day or days of the second week in the month.

        [EnumInfo(ResourceType = typeof(Assets.Resources),//where Resources is the Resources file to look into.
           Display = nameof(Assets.Resources.Enum_WeekOfMonth_Third))]
        Third, //The recurring event will occur once a month, on the specified day or days of the third week in the month.

        [EnumInfo(ResourceType = typeof(Assets.Resources),//where Resources is the Resources file to look into.
            Display = nameof(Assets.Resources.Enum_WeekOfMonth_Fourth))]
        Fourth, //The recurring event will occur once a month, on the specified day or days of the fourth week in the month.

        [EnumInfo(ResourceType = typeof(Assets.Resources),//where Resources is the Resources file to look into.
            Display = nameof(Assets.Resources.Enum_WeekOfMonth_Last))]
        Last //The recurring event will occur once a month, on the specified day or days of the last week in the month.
    }


    public static class WeekDaysExtensions //Extensions for Flags enum to help add/remove flag.
    {
        public static WeekDays SetFlag(this WeekDays currentstate, WeekDays state)
        {
            return currentstate | state; //Add flag.
        }
        public static WeekDays RemoveFlag(this WeekDays currentstate, WeekDays state)
        {
            return currentstate &= ~state; //Remove flag.
        }
        public static WeekDays ClearFlags(this WeekDays currentstate)
        {
            return 0;
        }

    }




    /* Properties used with corresponding use cases:
     * 
     * 1: Type is Daily   https://docs.devexpress.com/WindowsForms/6205/controls-and-libraries/scheduler/examples/recurrence/recurrence-daily-examples
     *      WeekDays = EveryDay or WorkDays
     *      if EveryDay -> Peridicity gives 'every x days'
     *      
     * 2: Type is Weekly  https://docs.devexpress.com/WindowsForms/6206/controls-and-libraries/scheduler/examples/recurrence/recurrence-weekly-examples
     *      Periodicity gives 'every x weeks'
     *      WeekDays is a combination of Sunday, Monday, Tuesday, Wednesday, Thursday, Friday, Saturday flags
     *      
     * 3: Type is Monthly  https://docs.devexpress.com/WindowsForms/6207/controls-and-libraries/scheduler/examples/recurrence/recurrence-monthly-examples
     *      WeekOfMonth :
     *      if None, means on a specific day within the month, given by DayNumber; and Periodicity gives 'every x months'
     *      if First, Second, Third, Fourth, Last WeekDays gives the said Day/Days and Periodicity gives 'every x months'
     *      
     * 4: Type is Yearly  https://docs.devexpress.com/WindowsForms/6208/controls-and-libraries/scheduler/examples/recurrence/recurrence-yearly-examples
     *      WeekOfMonth :
     *      if None, means on a specific month within the year, given by Month and DayNumber gives the specific day in the said month
     *      if First, Second, Third, Fourth, Last WeekDays gives the said Day/Days and Month gives the specific month

     
     
     
     */
}
