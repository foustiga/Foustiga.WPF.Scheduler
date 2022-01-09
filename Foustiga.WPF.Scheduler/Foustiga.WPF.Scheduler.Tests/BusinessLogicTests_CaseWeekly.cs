using FluentAssertions;
using FluentAssertions.Execution;
using FluentAssertions.Extensions;

using Foustiga.WPF.Scheduler.API;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using System;
using System.Collections.Generic;

namespace Foustiga.WPF.Scheduler.Tests
{
    [TestClass]
    public class BusinessLogicTests_CaseWeekly
    {
        public BusinessLogicTests_CaseWeekly() { }
        private API.Scheduler scheduler = new();


        [TestMethod]
        public void Weekly_CheckDefaultValues()
        {
            // Arrange
            IRecurrenceInfo recurrenceInfo = API.Scheduler.CreateRecurrenceInfo();
            recurrenceInfo.Type = ReccurrenceType.Weekly; //Change only Type

            // Act
            List<DateTime> occurrences = scheduler.GetNextOccurrences(recurrenceInfo);

            // Assert
            using (new AssertionScope())
            {
                //Check recurrence pattern defaults
                recurrenceInfo.Type.Should().Be(ReccurrenceType.Weekly);
                recurrenceInfo.WeekDays.Should().Be(WeekDays.Monday);

                //Check recurrence range defaults
                recurrenceInfo.Start.Should().Be(DateTime.Today);
                recurrenceInfo.Range.Should().Be(RecurrenceRange.NoEndDate, "default Range to be NoEndDate");
                recurrenceInfo.End.Should().Be(DateTime.Today.AddDays(1), "default End to be tomorrow");

                //Check that we get occurrences
                occurrences.Count.Should().BeGreaterThan(0, "no occurrence given");
                occurrences.Should().HaveCount(20, "must have 20 occurrences"); //NoEndDate range is limited to 20 occurrences.
            }
        }

        [TestMethod]
        public void Weekly_WeekDaysMonday_Periodicity1()
        {
            // Arrange
            IRecurrenceInfo recurrenceInfo = API.Scheduler.CreateRecurrenceInfo();
            recurrenceInfo.Type = ReccurrenceType.Weekly;
            recurrenceInfo.WeekDays = recurrenceInfo.WeekDays.ClearFlags().SetFlag(WeekDays.Monday);
            recurrenceInfo.Periodicity = 1;

            recurrenceInfo.Start = new DateTime(2022, 01, 05);

            // Act
            List<DateTime> occurrences = scheduler.GetNextOccurrences(recurrenceInfo);

            // Assert
            using (new AssertionScope())
            {
                //Check that we get occurrences
                occurrences[0].Should().Be(10.January(2022));

                for (int i = 1; i < occurrences.Count; i++)
                {
                    occurrences[i].Should().Be(new DateTime(2022, 01, 10).AddDays(i * 7));
                }
            }
        }

        [TestMethod]
        public void Weekly_WeekDaysMondayAndWednesday_Periodicity1()
        {
            // Arrange
            IRecurrenceInfo recurrenceInfo = API.Scheduler.CreateRecurrenceInfo();
            recurrenceInfo.Type = ReccurrenceType.Weekly;
            recurrenceInfo.WeekDays = recurrenceInfo.WeekDays.ClearFlags().SetFlag(WeekDays.Monday).SetFlag(WeekDays.Wednesday);
            recurrenceInfo.Periodicity = 1;

            recurrenceInfo.Start = new DateTime(2022, 01, 05);

            // Act
            List<DateTime> occurrences = scheduler.GetNextOccurrences(recurrenceInfo);

            // Assert
            using (new AssertionScope())
            {
                //Check that we get occurrences
                occurrences[0].Should().Be(10.January(2022));
                occurrences[1].Should().Be(12.January(2022));
                occurrences[2].Should().Be(17.January(2022));
                occurrences[3].Should().Be(19.January(2022));
                occurrences[4].Should().Be(24.January(2022));
                occurrences[5].Should().Be(26.January(2022));
                occurrences[6].Should().Be(31.January(2022));
                occurrences[7].Should().Be(2.February(2022));
            }
        }

        [TestMethod]
        public void Weekly_WeekDaysMondayAndWednesdayAndFriday_Periodicity1()
        {
            // Arrange
            IRecurrenceInfo recurrenceInfo = API.Scheduler.CreateRecurrenceInfo();
            recurrenceInfo.Type = ReccurrenceType.Weekly;
            recurrenceInfo.WeekDays = recurrenceInfo.WeekDays.ClearFlags().SetFlag(WeekDays.Monday).SetFlag(WeekDays.Wednesday).SetFlag(WeekDays.Friday);
            recurrenceInfo.Periodicity = 1;

            recurrenceInfo.Start = new DateTime(2022, 01, 05);

            // Act
            List<DateTime> occurrences = scheduler.GetNextOccurrences(recurrenceInfo);

            // Assert
            using (new AssertionScope())
            {
                //Check that we get occurrences
                occurrences[0].Should().Be(7.January(2022));
                occurrences[1].Should().Be(10.January(2022));
                occurrences[2].Should().Be(12.January(2022));
                occurrences[3].Should().Be(14.January(2022));
                occurrences[4].Should().Be(17.January(2022));
                occurrences[5].Should().Be(19.January(2022));
                occurrences[6].Should().Be(21.January(2022));
                occurrences[7].Should().Be(24.January(2022));
                occurrences[8].Should().Be(26.January(2022));
                occurrences[9].Should().Be(28.January(2022));
                occurrences[10].Should().Be(31.January(2022));
                occurrences[11].Should().Be(2.February(2022));
                occurrences[12].Should().Be(4.February(2022));
                occurrences[13].Should().Be(7.February(2022));
            }
        }

        [TestMethod]
        public void Weekly_WeekDaysMonday_Periodicity3()
        {
            // Arrange
            IRecurrenceInfo recurrenceInfo = API.Scheduler.CreateRecurrenceInfo();
            recurrenceInfo.Type = ReccurrenceType.Weekly;
            recurrenceInfo.WeekDays = recurrenceInfo.WeekDays.ClearFlags().SetFlag(WeekDays.Monday);
            recurrenceInfo.Periodicity = 3;

            recurrenceInfo.Start = new DateTime(2022, 01, 05);

            // Act
            List<DateTime> occurrences = scheduler.GetNextOccurrences(recurrenceInfo);

            // Assert
            using (new AssertionScope())
            {
                //Check that we get occurrences : every next 3rd Monday
                occurrences[0].Should().Be(24.January(2022));
                for (int i = 1; i < occurrences.Count; i++)
                {
                    occurrences[i].Should().Be(new DateTime(2022, 01, 24).AddDays(i * 7 * 3));
                }
            }
        }

        [TestMethod]
        public void Weekly_WeekDaysMondayAndWednesdayAndFridayAndSunday_Periodicity3()
        {
            // Arrange
            IRecurrenceInfo recurrenceInfo = API.Scheduler.CreateRecurrenceInfo();
            recurrenceInfo.Type = ReccurrenceType.Weekly;
            recurrenceInfo.WeekDays = recurrenceInfo.WeekDays.ClearFlags().SetFlag(WeekDays.Monday).SetFlag(WeekDays.Wednesday).SetFlag(WeekDays.Friday).SetFlag(WeekDays.Sunday);
            recurrenceInfo.Periodicity = 3;

            recurrenceInfo.Start = new DateTime(2022, 01, 05);

            // Act
            List<DateTime> occurrences = scheduler.GetNextOccurrences(recurrenceInfo);

            // Assert
            using (new AssertionScope())
            {
                //Check that we get occurrences
                occurrences[0].Should().Be(7.January(2022));
                occurrences[1].Should().Be(9.January(2022));
                occurrences[2].Should().Be(24.January(2022));
                occurrences[3].Should().Be(26.January(2022));
                occurrences[4].Should().Be(28.January(2022));
                occurrences[5].Should().Be(30.January(2022));
                occurrences[6].Should().Be(14.February(2022));
                occurrences[7].Should().Be(16.February(2022));
                occurrences[8].Should().Be(18.February(2022));
                occurrences[9].Should().Be(20.February(2022));
            }
        }

        [TestMethod]
        public void Weekly_WeekDaysMondayAndWednesdayAndFridayAndSunday_Periodicity5()
        {
            // Arrange
            IRecurrenceInfo recurrenceInfo = API.Scheduler.CreateRecurrenceInfo();
            recurrenceInfo.Type = ReccurrenceType.Weekly;
            recurrenceInfo.WeekDays = recurrenceInfo.WeekDays.ClearFlags().SetFlag(WeekDays.Monday).SetFlag(WeekDays.Wednesday).SetFlag(WeekDays.Friday).SetFlag(WeekDays.Sunday);
            recurrenceInfo.Periodicity = 5;

            recurrenceInfo.Start = new DateTime(2022, 01, 05);

            // Act
            List<DateTime> occurrences = scheduler.GetNextOccurrences(recurrenceInfo);

            // Assert
            using (new AssertionScope())
            {
                //Check that we get occurrences
                occurrences[0].Should().Be(7.January(2022));
                occurrences[1].Should().Be(9.January(2022));
                occurrences[2].Should().Be(7.February(2022));
                occurrences[3].Should().Be(9.February(2022));
                occurrences[4].Should().Be(11.February(2022));
                occurrences[5].Should().Be(13.February(2022));
                occurrences[6].Should().Be(14.March(2022));
                occurrences[7].Should().Be(16.March(2022));
                occurrences[8].Should().Be(18.March(2022));
                occurrences[9].Should().Be(20.March(2022));
            }
        }
    }
}
