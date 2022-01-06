using Autofac;

using Foustiga.WPF.Scheduler.Foundation.IoC;

using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Foustiga.WPF.Scheduler.API
{
    public  class Scheduler
    {
        public Scheduler()
        {
            //CultureInfo _culture = (CultureInfo)CultureInfo.CurrentCulture.Clone();
            //CultureInfo _uiculture = (CultureInfo)CultureInfo.CurrentUICulture.Clone();
            //_culture.DateTimeFormat.FirstDayOfWeek = DayOfWeek.Monday;
            //_uiculture.DateTimeFormat.FirstDayOfWeek = DayOfWeek.Monday;

            //System.Threading.Thread.CurrentThread.CurrentCulture = _culture;
            //System.Threading.Thread.CurrentThread.CurrentUICulture = _uiculture;
        }

        //public IRecurrence_ViewModel Scheduler()
        //{
        //    return ViewModel;
        //}
        private IRecurrence_ViewModel viewModel;
        //public IRecurrence_ViewModel ViewModel
        //{
        //    get
        //    {
        //        if (viewModel == null) { viewModel = ModuleCompositionRoot.BeginLifetimeScope().Resolve<IRecurrence_ViewModel>(); }
        //        return viewModel;
        //    }
        //}


        public  IRecurrence_ViewModel CreateViewModel(IRecurrenceInfo dataModel)
        {
            if (viewModel == null) { viewModel = ModuleCompositionRoot.BeginLifetimeScope().Resolve<IRecurrence_ViewModel>(); }
            viewModel.DataModel = dataModel;
            return viewModel;

        }

        public static IRecurrenceInfo CreateRecurrenceInfo()
        {
            return DataModel;
        }
        private static IRecurrenceInfo dataModel;
        private static IRecurrenceInfo DataModel
        {
            get
            {
                if (dataModel == null) { dataModel = ModuleCompositionRoot.BeginLifetimeScope().Resolve<IRecurrenceInfo>(); }
                return dataModel;
            }
        }


        #region Serialize/Deserialize
        public string Serialize(IRecurrenceInfo objectToSerialize)
        {
            string returnValue = string.Empty;
            returnValue = JsonConvert.SerializeObject(objectToSerialize);
            return returnValue;
        }
        public IRecurrenceInfo Deserialize(string stringToDeserialize)
        {
            return JsonConvert.DeserializeObject<DataModel.RecurrenceInfo>(stringToDeserialize);
        }
        #endregion Serialize/Deserialize




        public List<DateTime> GetNextOccurrences(IRecurrenceInfo recurrenceInfo, int maxNbOccurrences = 20)
        {
            List<DateTime> returnValues = new();

            BusinessLogic businessLogic = new();
            DateTime lastDate = recurrenceInfo.Start;
            DateTime nextOccurrence;
            int nbOccurrences = maxNbOccurrences;
            if (recurrenceInfo.Range == RecurrenceRange.NoEndDate)
            {
                nbOccurrences = maxNbOccurrences;
                for (int i = 1; i <= nbOccurrences; i++)
                {
                    nextOccurrence = GetNextOccurrence(recurrenceInfo, lastDate);
                    returnValues.Add(nextOccurrence);
                    lastDate = nextOccurrence;
                }
            }
            else if (recurrenceInfo.Range == RecurrenceRange.OccurrenceCount)
            {
                nbOccurrences = recurrenceInfo.OccurrenceCount;
                for (int i = 1; i <= nbOccurrences; i++)
                {
                    nextOccurrence = GetNextOccurrence(recurrenceInfo, lastDate);
                    returnValues.Add(nextOccurrence);
                    lastDate = nextOccurrence;
                }
            }
            else if (recurrenceInfo.Range == RecurrenceRange.EndByDate)
            {
                DateTime maxDate = recurrenceInfo.End;
                while (lastDate < maxDate)
                {
                    nextOccurrence = GetNextOccurrence(recurrenceInfo, lastDate);
                    if (nextOccurrence <= maxDate) { returnValues.Add(nextOccurrence); }
                    lastDate = nextOccurrence;
                }
            }


            //DateTime nextOccurrence;
            //for (int i = 1; i <= nbOccurrences; i++)
            //{
            //    nextOccurrence = businessLogic.GetNextOccurrence(recurrenceInfo, lastDate);
            //    returnValues.Add(nextOccurrence);
            //    lastDate = nextOccurrence;
            //}


            return returnValues;
        }

        private DateTime GetNextOccurrence(IRecurrenceInfo recurrenceInfo, DateTime lastDate)
        {
            BusinessLogic businessLogic = new();
            DateTime nextOccurrence;

            nextOccurrence = businessLogic.GetNextOccurrence(recurrenceInfo, lastDate);
            return nextOccurrence;

        }
    }

}
