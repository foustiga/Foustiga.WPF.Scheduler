using FluentAssertions;
using FluentAssertions.Execution;
using FluentAssertions.Extensions;

using Foustiga.WPF.Scheduler.API;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Markup;

namespace Foustiga.WPF.Scheduler.Tests
{
    [TestClass]
    public class BusinessLogicTests_CaseDaily
    {
        public BusinessLogicTests_CaseDaily() { }
        private API.Scheduler scheduler = new();


        [TestMethod]
        public void Daily_CheckDefaultValues()
        {
            // Arrange
            IRecurrenceInfo recurrenceInfo = API.Scheduler.CreateRecurrenceInfo();
            recurrenceInfo.Type = ReccurrenceType.Daily; //Change only Type

            // Act
            List<DateTime> occurrences = scheduler.GetNextOccurrences(recurrenceInfo);

            // Assert
            using (new AssertionScope())
            {
                //Check recurrence pattern defaults
                recurrenceInfo.Type.Should().Be(ReccurrenceType.Daily);
                recurrenceInfo.WeekDays.Should().Be(WeekDays.Monday);

                //Check recurrence range defaults
                recurrenceInfo.Start.Should().Be(DateTime.Today);
                recurrenceInfo.Range.Should().Be(RecurrenceRange.NoEndDate);
                recurrenceInfo.End.Should().Be(DateTime.Today.AddDays(1), "default End to be tomorrow");

                //Check that we get occurrences
                occurrences.Count.Should().BeGreaterThan(0, "no occurrence given");
                occurrences.Should().HaveCount(20, "must have 20 occurrences"); //NoEndDate range is limited to 20 occurrences.
            }
        }

        [TestMethod]
        public void Daily_WeekDaysEveryDay_Periodicity1()
        {
            // Arrange
            IRecurrenceInfo recurrenceInfo = API.Scheduler.CreateRecurrenceInfo();
            recurrenceInfo.Type = ReccurrenceType.Daily;
            recurrenceInfo.WeekDays = recurrenceInfo.WeekDays.SetFlag(WeekDays.EveryDay);
            recurrenceInfo.Periodicity = 1;

            // Act
            List<DateTime> occurrences = scheduler.GetNextOccurrences(recurrenceInfo);

            // Assert
            using (new AssertionScope())
            {
                //Check that we get occurrences
                for (int i = 0; i < occurrences.Count; i++)
                {
                    occurrences[i].Should().Be(DateTime.Today.AddDays(i+1), $"occurrence #{i+1} to be {DateTime.Today.AddDays(i+1)}");
                }
            }
        }

        [TestMethod]
        public void Daily_WeekDaysEveryDay_Periodicity2()
        {
            // Arrange
            IRecurrenceInfo recurrenceInfo = API.Scheduler.CreateRecurrenceInfo();
            recurrenceInfo.Type = ReccurrenceType.Daily;
            recurrenceInfo.WeekDays = recurrenceInfo.WeekDays.SetFlag(WeekDays.EveryDay);
            recurrenceInfo.Periodicity = 2;

            // Act
            List<DateTime> occurrences = scheduler.GetNextOccurrences(recurrenceInfo);

            // Assert
            using (new AssertionScope())
            {
                //Check that we get occurrences
                for (int i = 0; i < occurrences.Count; i++)
                {
                    occurrences[i].Should().Be(DateTime.Today.AddDays((i*2) + 2), $"occurrence #{i+1} to be {DateTime.Today.AddDays((i*2) + 2)}");
                }
            }
        }

        [TestMethod]
        public void Daily_WeekDaysEveryDay_Periodicity5()
        {
            // Arrange
            IRecurrenceInfo recurrenceInfo = API.Scheduler.CreateRecurrenceInfo();
            recurrenceInfo.Type = ReccurrenceType.Daily;
            recurrenceInfo.WeekDays = recurrenceInfo.WeekDays.SetFlag(WeekDays.EveryDay);
            recurrenceInfo.Periodicity = 5;

            // Act
            List<DateTime> occurrences = scheduler.GetNextOccurrences(recurrenceInfo);

            // Assert
            using (new AssertionScope())
            {
                //Check that we get occurrences
                for (int i = 0; i < occurrences.Count; i++)
                {
                    occurrences[i].Should().Be(DateTime.Today.AddDays((i * 5) + 5), $"occurrence #{i+1} to be {DateTime.Today.AddDays((i * 5) + 5)}");
                }
            }
        }

        [TestMethod]
        public void Daily_WeekDaysWorkDays_Start20220105()
        {
            // Arrange
            IRecurrenceInfo recurrenceInfo = API.Scheduler.CreateRecurrenceInfo();
            recurrenceInfo = API.Scheduler.CreateRecurrenceInfo(); 
            recurrenceInfo.Type = ReccurrenceType.Daily;
            recurrenceInfo.WeekDays = recurrenceInfo.WeekDays.SetFlag(WeekDays.WorkDays);

            recurrenceInfo.Start = new DateTime(2022, 01, 05);

            // Act
            List<DateTime> occurrences = scheduler.GetNextOccurrences(recurrenceInfo);

            // Assert
            using (new AssertionScope())
            {
                //Check that we get the correct dates
                occurrences[0].Should().Be(6.January(2022));
                occurrences[1].Should().Be(7.January(2022));
                occurrences[2].Should().Be(10.January(2022));
                occurrences[3].Should().Be(11.January(2022));
                occurrences[4].Should().Be(12.January(2022));
                occurrences[5].Should().Be(13.January(2022));
                occurrences[6].Should().Be(14.January(2022));
                occurrences[7].Should().Be(17.January(2022));
            }
        }


    }
}
