
using Foustiga.WPF.Scheduler.API;
using Foustiga.WPF.Scheduler.Foundation.NotifyChanged;

using System;

namespace Foustiga.WPF.Scheduler.DataModel
{
    public class RecurrenceInfo : AutoBindable, IRecurrenceInfo
    {
        public RecurrenceInfo() 
        {
            //Set default values:

            //Recurrence pattern
            Type = ReccurrenceType.Weekly;
            WeekDays = WeekDays.Monday;
            DayNumber = 1;
            Month = 1;

            //Reccurrence range
            Start = DateTime.Today;
            Range = RecurrenceRange.NoEndDate;
            //Range = RecurrenceRange.OccurrenceCount;
            //OccurrenceCount = 10;
            End = Start.AddDays(1);
        }
        public ReccurrenceType Type { get => GetProperty<ReccurrenceType>();
            set
            {
                if (SetProperty(value))
                {
                    if (
                        (Type == ReccurrenceType.Monthly || Type == ReccurrenceType.Yearly) && (
                        WeekDays.HasFlag(WeekDays.WorkDays) || WeekDays.HasFlag(WeekDays.WeekendDays)  || WeekDays.HasFlag(WeekDays.EveryDay)))
                        { WeekDays = WeekDays.ClearFlags(); }
                }
            }
        }

        public int DayNumber { get => GetProperty<int>(); set => SetProperty(value); }
        public TimeSpan Duration { get => GetProperty<TimeSpan>(); set => SetProperty(value); }
        public DateTime End { get => GetProperty<DateTime>(); set => SetProperty(value); }
        public DayOfWeek FirstDayOfWeek { get => GetProperty<DayOfWeek>(); set => SetProperty(value); }
        public int Id { get => GetProperty<int>(); set => SetProperty(value); }
        public int Month { get => GetProperty<int>(); set => SetProperty(value); }
        public int OccurrenceCount { get => GetProperty<int>(); set => SetProperty(value); }
        public int Periodicity { get => GetProperty<int>(); set => SetProperty(value); }
        public RecurrenceRange Range { get => GetProperty<RecurrenceRange>(); set => SetProperty(value); }
        public DateTime Start { get => GetProperty<DateTime>(); set => SetProperty(value); }
        public WeekDays WeekDays { get => GetProperty<WeekDays>(); set => SetProperty(value); }
        public WeekOfMonth WeekOfMonth { get => GetProperty<WeekOfMonth>(); set => SetProperty(value); }


    }



}
