using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Markup;

using Foustiga.WPF.Scheduler;
using Foustiga.WPF.Scheduler.API;
using Foustiga.WPF.Scheduler.Demo.Helpers;

namespace Foustiga.WPF.Scheduler.Demo
{
    public class MainWindow_ViewModel : AutoBindable
    {
        public MainWindow_ViewModel()
        {
            //To allow WPF strings to be formatted correctly according to current culture. (http://stackoverflow.com/questions/1062160/wpf-xaml-stringformat-datetime-output-in-wrong-culture)
            FrameworkElement.LanguageProperty.OverrideMetadata(
               typeof(FrameworkElement),
               new FrameworkPropertyMetadata(
                   XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.IetfLanguageTag)));


            recurrenceInfo.Type = API.ReccurrenceType.Daily;
            recurrenceInfo.WeekDays = recurrenceInfo.WeekDays.SetFlag(WeekDays.EveryDay);
            recurrenceInfo.Periodicity = 2;
            Recurrence_ViewModel = scheduler.CreateViewModel(recurrenceInfo);


        }

        private API.Scheduler scheduler = new ();
        private IRecurrenceInfo recurrenceInfo = API.Scheduler.CreateRecurrenceInfo();

        //{
        //    Type = ReccurrenceType.Yearly
        //};
        public IRecurrence_ViewModel Recurrence_ViewModel { get => GetProperty<IRecurrence_ViewModel>(); private set => SetProperty(value); }
        //    private set
        //    {
        //        if (SetProperty(value))
        //        { Recurrence_ViewModel.DataModel = recurrenceInfo; }
        //    }
        //}

    }
}
