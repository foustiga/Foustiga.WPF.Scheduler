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
    public class BusinessLogicTests_CaseYearly
    {
        public BusinessLogicTests_CaseYearly() { }
        private API.Scheduler scheduler = new();


        [TestMethod]
        public void Yearly_CheckDefaultValues()
        {
            // Arrange
            IRecurrenceInfo recurrenceInfo = API.Scheduler.CreateRecurrenceInfo();
            recurrenceInfo.Type = ReccurrenceType.Yearly; //Change only Type

            // Act
            List<DateTime> occurrences = scheduler.GetNextOccurrences(recurrenceInfo);

            // Assert
            using (new AssertionScope())
            {
                //Check recurrence pattern defaults
                recurrenceInfo.Type.Should().Be(ReccurrenceType.Yearly);
                //recurrenceInfo.WeekDays.Should().Be(WeekDays.Monday);
                recurrenceInfo.Month.Should().Be(1);

                //Check recurrence range defaults
                recurrenceInfo.Start.Should().Be(DateTime.Today);
                recurrenceInfo.Range.Should().Be(RecurrenceRange.NoEndDate, "default Range to be NoEndDate");
                recurrenceInfo.End.Should().Be(DateTime.Today.AddDays(1), "default End to be tomorrow");

                //Check that we get occurrences
                occurrences.Count.Should().BeGreaterThan(0, "no occurrence given");
                occurrences.Should().HaveCount(20, "must have 20 occurrences"); //NoEndDate range is limited to 20 occurrences.
            }
        }

        //[TestMethod]
        //public void Yearly_DayNumber15_Periodicity1()
        //{
        //    // Arrange
        //    IRecurrenceInfo recurrenceInfo = API.Scheduler.CreateRecurrenceInfo();
        //    recurrenceInfo.Type = ReccurrenceType.Yearly;
        //    recurrenceInfo.DayNumber = 15;
        //    recurrenceInfo.Periodicity = 1;

        //    recurrenceInfo.Start = new DateTime(2022, 01, 05);

        //    // Act
        //    List<DateTime> occurrences = scheduler.GetNextOccurrences(recurrenceInfo);

        //    // Assert
        //    using (new AssertionScope())
        //    {
        //        //Check that we get occurrences
        //        occurrences[0].Should().Be(15.January(2022));

        //        for (int i = 1; i < occurrences.Count; i++)
        //        {
        //            occurrences[i].Should().Be(new DateTime(2022, 01, 15).AddMonths(i));
        //        }
        //    }
        //}

        //[TestMethod]
        //public void Yearly_DayNumber15_Periodicity3()
        //{
        //    // Arrange
        //    IRecurrenceInfo recurrenceInfo = API.Scheduler.CreateRecurrenceInfo();
        //    recurrenceInfo.Type = ReccurrenceType.Yearly;
        //    recurrenceInfo.DayNumber = 15;
        //    recurrenceInfo.Periodicity = 3;

        //    recurrenceInfo.Start = new DateTime(2022, 01, 05);

        //    // Act
        //    List<DateTime> occurrences = scheduler.GetNextOccurrences(recurrenceInfo);

        //    // Assert
        //    using (new AssertionScope())
        //    {
        //        //Check that we get occurrences
        //        occurrences[0].Should().Be(15.January(2022));

        //        for (int i = 1; i < occurrences.Count; i++)
        //        {
        //            occurrences[i].Should().Be(new DateTime(2022, 01, 15).AddMonths(i*3));
        //        }
        //    }
        //}

        //[TestMethod]
        //public void Yearly_DayNumber31_Periodicity1()
        //{
        //    // Arrange
        //    IRecurrenceInfo recurrenceInfo = API.Scheduler.CreateRecurrenceInfo();
        //    recurrenceInfo.Type = ReccurrenceType.Yearly;
        //    recurrenceInfo.DayNumber = 31;
        //    recurrenceInfo.Periodicity = 1;

        //    recurrenceInfo.Start = new DateTime(2022, 01, 05);

        //    // Act
        //    List<DateTime> occurrences = scheduler.GetNextOccurrences(recurrenceInfo);

        //    // Assert
        //    using (new AssertionScope())
        //    {
        //        //Check that we get occurrences
        //        occurrences[0].Should().Be(31.January(2022));
        //        occurrences[1].Should().Be(28.February(2022));
        //        occurrences[2].Should().Be(31.March(2022));
        //        occurrences[3].Should().Be(30.April(2022));
        //        occurrences[4].Should().Be(31.May(2022));
        //        occurrences[5].Should().Be(30.June(2022));
        //        occurrences[6].Should().Be(31.July(2022));
        //        occurrences[7].Should().Be(31.August(2022));
        //        occurrences[11].Should().Be(31.December(2022));
        //        occurrences[12].Should().Be(31.January(2023));
        //        occurrences[13].Should().Be(28.February(2023));
        //    }
        //}

        //[TestMethod]
        //public void Yearly_DayNumber31_Periodicity3()
        //{
        //    // Arrange
        //    IRecurrenceInfo recurrenceInfo = API.Scheduler.CreateRecurrenceInfo();
        //    recurrenceInfo.Type = ReccurrenceType.Yearly;
        //    recurrenceInfo.DayNumber = 31;
        //    recurrenceInfo.Periodicity = 3;

        //    recurrenceInfo.Start = new DateTime(2022, 01, 05);

        //    // Act
        //    List<DateTime> occurrences = scheduler.GetNextOccurrences(recurrenceInfo);

        //    // Assert
        //    using (new AssertionScope())
        //    {
        //        //Check that we get occurrences
        //        occurrences[0].Should().Be(31.January(2022));
        //        occurrences[1].Should().Be(30.April(2022));
        //        occurrences[2].Should().Be(31.July(2022));
        //        occurrences[3].Should().Be(31.October(2022));
        //        occurrences[4].Should().Be(31.January(2023));
        //        occurrences[5].Should().Be(30.April(2023));
        //    }
        //}

        //[TestMethod]
        //public void Yearly_FirstMonday_Periodicity1()
        //{
        //    // Arrange
        //    IRecurrenceInfo recurrenceInfo = API.Scheduler.CreateRecurrenceInfo();
        //    recurrenceInfo.Type = ReccurrenceType.Yearly;
        //    recurrenceInfo.WeekOfMonth = WeekOfMonth.First;
        //    recurrenceInfo.WeekDays = recurrenceInfo.WeekDays.ClearFlags().SetFlag(WeekDays.Monday);
        //    recurrenceInfo.Periodicity = 1;

        //    recurrenceInfo.Start = new DateTime(2022, 01, 05);

        //    // Act
        //    List<DateTime> occurrences = scheduler.GetNextOccurrences(recurrenceInfo);

        //    // Assert
        //    using (new AssertionScope())
        //    {
        //        //Check that we get occurrences
        //        occurrences[0].Should().Be(7.February(2022));
        //        occurrences[1].Should().Be(7.March(2022));
        //        occurrences[2].Should().Be(4.April(2022));
        //        occurrences[3].Should().Be(2.May(2022));
        //        occurrences[4].Should().Be(6.June(2022));
        //    }
        //}
        
        //[TestMethod]
        //public void Yearly_FirstThursday_Periodicity1()
        //{
        //    // Arrange
        //    IRecurrenceInfo recurrenceInfo = API.Scheduler.CreateRecurrenceInfo();
        //    recurrenceInfo.Type = ReccurrenceType.Yearly;
        //    recurrenceInfo.WeekOfMonth = WeekOfMonth.First;
        //    recurrenceInfo.WeekDays = recurrenceInfo.WeekDays.ClearFlags().SetFlag(WeekDays.Thursday);
        //    recurrenceInfo.Periodicity = 1;

        //    recurrenceInfo.Start = new DateTime(2022, 01, 05);

        //    // Act
        //    List<DateTime> occurrences = scheduler.GetNextOccurrences(recurrenceInfo);

        //    // Assert
        //    using (new AssertionScope())
        //    {
        //        //Check that we get occurrences
        //        occurrences[0].Should().Be(6.January(2022));
        //        occurrences[1].Should().Be(3.February(2022));
        //        occurrences[2].Should().Be(3.March(2022));
        //        occurrences[3].Should().Be(7.April(2022));
        //        occurrences[4].Should().Be(5.May(2022));
        //        occurrences[5].Should().Be(2.June(2022));
        //    }
        //}

        //[TestMethod]
        //public void Yearly_FirstMonday_Periodicity3()
        //{
        //    // Arrange
        //    IRecurrenceInfo recurrenceInfo = API.Scheduler.CreateRecurrenceInfo();
        //    recurrenceInfo.Type = ReccurrenceType.Yearly;
        //    recurrenceInfo.WeekOfMonth = WeekOfMonth.First;
        //    recurrenceInfo.WeekDays = recurrenceInfo.WeekDays.ClearFlags().SetFlag(WeekDays.Monday);
        //    recurrenceInfo.Periodicity = 3;

        //    recurrenceInfo.Start = new DateTime(2022, 01, 05);

        //    // Act
        //    List<DateTime> occurrences = scheduler.GetNextOccurrences(recurrenceInfo);

        //    // Assert
        //    using (new AssertionScope())
        //    {
        //        //Check that we get occurrences
        //        occurrences[0].Should().Be(4.April(2022));
        //        occurrences[1].Should().Be(4.July(2022));
        //        occurrences[2].Should().Be(3.October(2022));
        //        occurrences[3].Should().Be(2.January(2023));
        //        occurrences[4].Should().Be(3.April(2023));
        //    }
        //}

        //[TestMethod]
        //public void Yearly_SecondMonday_Periodicity1()
        //{
        //    // Arrange
        //    IRecurrenceInfo recurrenceInfo = API.Scheduler.CreateRecurrenceInfo();
        //    recurrenceInfo.Type = ReccurrenceType.Yearly;
        //    recurrenceInfo.WeekOfMonth = WeekOfMonth.Second;
        //    recurrenceInfo.WeekDays = recurrenceInfo.WeekDays.ClearFlags().SetFlag(WeekDays.Monday);
        //    recurrenceInfo.Periodicity = 1;

        //    recurrenceInfo.Start = new DateTime(2022, 01, 05);

        //    // Act
        //    List<DateTime> occurrences = scheduler.GetNextOccurrences(recurrenceInfo);

        //    // Assert
        //    using (new AssertionScope())
        //    {
        //        //Check that we get occurrences
        //        occurrences[0].Should().Be(10.January(2022));
        //        occurrences[1].Should().Be(14.February(2022));
        //        occurrences[2].Should().Be(14.March(2022));
        //        occurrences[3].Should().Be(11.April(2022));
        //        occurrences[4].Should().Be(9.May(2022));
        //    }
        //}

        //[TestMethod]
        //public void Yearly_SecondMonday_Periodicity3()
        //{
        //    // Arrange
        //    IRecurrenceInfo recurrenceInfo = API.Scheduler.CreateRecurrenceInfo();
        //    recurrenceInfo.Type = ReccurrenceType.Yearly;
        //    recurrenceInfo.WeekOfMonth = WeekOfMonth.Second;
        //    recurrenceInfo.WeekDays = recurrenceInfo.WeekDays.ClearFlags().SetFlag(WeekDays.Monday);
        //    recurrenceInfo.Periodicity = 3;

        //    recurrenceInfo.Start = new DateTime(2022, 01, 05);

        //    // Act
        //    List<DateTime> occurrences = scheduler.GetNextOccurrences(recurrenceInfo);

        //    // Assert
        //    using (new AssertionScope())
        //    {
        //        //Check that we get occurrences
        //        occurrences[0].Should().Be(10.January(2022));
        //        occurrences[1].Should().Be(11.April(2022));
        //        occurrences[2].Should().Be(11.July(2022));
        //        occurrences[3].Should().Be(10.October(2022));
        //        occurrences[4].Should().Be(9.January(2023));
        //    }
        //}

        //[TestMethod]
        //public void Yearly_ThirdMonday_Periodicity1()
        //{
        //    // Arrange
        //    IRecurrenceInfo recurrenceInfo = API.Scheduler.CreateRecurrenceInfo();
        //    recurrenceInfo.Type = ReccurrenceType.Yearly;
        //    recurrenceInfo.WeekOfMonth = WeekOfMonth.Third;
        //    recurrenceInfo.WeekDays = recurrenceInfo.WeekDays.ClearFlags().SetFlag(WeekDays.Monday);
        //    recurrenceInfo.Periodicity = 1;

        //    recurrenceInfo.Start = new DateTime(2022, 01, 05);

        //    // Act
        //    List<DateTime> occurrences = scheduler.GetNextOccurrences(recurrenceInfo);

        //    // Assert
        //    using (new AssertionScope())
        //    {
        //        //Check that we get occurrences
        //        occurrences[0].Should().Be(17.January(2022));
        //        occurrences[1].Should().Be(21.February(2022));
        //        occurrences[2].Should().Be(21.March(2022));
        //        occurrences[3].Should().Be(18.April(2022));
        //        occurrences[4].Should().Be(16.May(2022));
        //    }
        //}

        //[TestMethod]
        //public void Yearly_ThirdMonday_Periodicity3()
        //{
        //    // Arrange
        //    IRecurrenceInfo recurrenceInfo = API.Scheduler.CreateRecurrenceInfo();
        //    recurrenceInfo.Type = ReccurrenceType.Yearly;
        //    recurrenceInfo.WeekOfMonth = WeekOfMonth.Third;
        //    recurrenceInfo.WeekDays = recurrenceInfo.WeekDays.ClearFlags().SetFlag(WeekDays.Monday);
        //    recurrenceInfo.Periodicity = 3;

        //    recurrenceInfo.Start = new DateTime(2022, 01, 05);

        //    // Act
        //    List<DateTime> occurrences = scheduler.GetNextOccurrences(recurrenceInfo);

        //    // Assert
        //    using (new AssertionScope())
        //    {
        //        //Check that we get occurrences
        //        occurrences[0].Should().Be(17.January(2022));
        //        occurrences[1].Should().Be(18.April(2022));
        //        occurrences[2].Should().Be(18.July(2022));
        //        occurrences[3].Should().Be(17.October(2022));
        //        occurrences[4].Should().Be(16.January(2023));
        //    }
        //}

        //[TestMethod]
        //public void Yearly_FourthMonday_Periodicity1()
        //{
        //    // Arrange
        //    IRecurrenceInfo recurrenceInfo = API.Scheduler.CreateRecurrenceInfo();
        //    recurrenceInfo.Type = ReccurrenceType.Yearly;
        //    recurrenceInfo.WeekOfMonth = WeekOfMonth.Fourth;
        //    recurrenceInfo.WeekDays = recurrenceInfo.WeekDays.ClearFlags().SetFlag(WeekDays.Monday);
        //    recurrenceInfo.Periodicity = 1;

        //    recurrenceInfo.Start = new DateTime(2022, 01, 05);

        //    // Act
        //    List<DateTime> occurrences = scheduler.GetNextOccurrences(recurrenceInfo);

        //    // Assert
        //    using (new AssertionScope())
        //    {
        //        //Check that we get occurrences
        //        occurrences[0].Should().Be(24.January(2022));
        //        occurrences[1].Should().Be(28.February(2022));
        //        occurrences[2].Should().Be(28.March(2022));
        //        occurrences[3].Should().Be(25.April(2022));
        //        occurrences[4].Should().Be(23.May(2022));
        //    }
        //}

        //[TestMethod]
        //public void Yearly_FourthMonday_Periodicity3()
        //{
        //    // Arrange
        //    IRecurrenceInfo recurrenceInfo = API.Scheduler.CreateRecurrenceInfo();
        //    recurrenceInfo.Type = ReccurrenceType.Yearly;
        //    recurrenceInfo.WeekOfMonth = WeekOfMonth.Fourth;
        //    recurrenceInfo.WeekDays = recurrenceInfo.WeekDays.ClearFlags().SetFlag(WeekDays.Monday);
        //    recurrenceInfo.Periodicity = 3;

        //    recurrenceInfo.Start = new DateTime(2022, 01, 05);

        //    // Act
        //    List<DateTime> occurrences = scheduler.GetNextOccurrences(recurrenceInfo);

        //    // Assert
        //    using (new AssertionScope())
        //    {
        //        //Check that we get occurrences
        //        occurrences[0].Should().Be(24.January(2022));
        //        occurrences[1].Should().Be(25.April(2022));
        //        occurrences[2].Should().Be(25.July(2022));
        //        occurrences[3].Should().Be(24.October(2022));
        //        occurrences[4].Should().Be(23.January(2023));
        //    }
        //}

        //[TestMethod]
        //public void Yearly_LasthMonday_Periodicity1()
        //{
        //    // Arrange
        //    IRecurrenceInfo recurrenceInfo = API.Scheduler.CreateRecurrenceInfo();
        //    recurrenceInfo.Type = ReccurrenceType.Yearly;
        //    recurrenceInfo.WeekOfMonth = WeekOfMonth.Last;
        //    recurrenceInfo.WeekDays = recurrenceInfo.WeekDays.ClearFlags().SetFlag(WeekDays.Monday);
        //    recurrenceInfo.Periodicity = 1;

        //    recurrenceInfo.Start = new DateTime(2022, 01, 05);

        //    // Act
        //    List<DateTime> occurrences = scheduler.GetNextOccurrences(recurrenceInfo);

        //    // Assert
        //    using (new AssertionScope())
        //    {
        //        //Check that we get occurrences
        //        occurrences[0].Should().Be(31.January(2022));
        //        occurrences[1].Should().Be(28.February(2022));
        //        occurrences[2].Should().Be(28.March(2022));
        //        occurrences[3].Should().Be(25.April(2022));
        //        occurrences[4].Should().Be(30.May(2022));
        //        occurrences[5].Should().Be(27.June(2022));
        //    }
        //}

        //[TestMethod]
        //public void Yearly_LastMonday_Periodicity3()
        //{
        //    // Arrange
        //    IRecurrenceInfo recurrenceInfo = API.Scheduler.CreateRecurrenceInfo();
        //    recurrenceInfo.Type = ReccurrenceType.Yearly;
        //    recurrenceInfo.WeekOfMonth = WeekOfMonth.Last;
        //    recurrenceInfo.WeekDays = recurrenceInfo.WeekDays.ClearFlags().SetFlag(WeekDays.Monday);
        //    recurrenceInfo.Periodicity = 3;

        //    recurrenceInfo.Start = new DateTime(2022, 01, 05);

        //    // Act
        //    List<DateTime> occurrences = scheduler.GetNextOccurrences(recurrenceInfo);

        //    // Assert
        //    using (new AssertionScope())
        //    {
        //        //Check that we get occurrences
        //        occurrences[0].Should().Be(31.January(2022));
        //        occurrences[1].Should().Be(25.April(2022));
        //        occurrences[2].Should().Be(25.July(2022));
        //        occurrences[3].Should().Be(31.October(2022));
        //        occurrences[4].Should().Be(30.January(2023));
        //    }
        //}

    }
}
