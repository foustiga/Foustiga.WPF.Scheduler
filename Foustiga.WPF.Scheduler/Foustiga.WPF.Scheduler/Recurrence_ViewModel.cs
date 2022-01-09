
using Foustiga.WPF.Scheduler.API;
using Foustiga.WPF.Scheduler.Foundation.Enums;
using Foustiga.WPF.Scheduler.Foundation.ViewModel;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

namespace Foustiga.WPF.Scheduler
{
    internal class RecurrenceViewModel : ViewModel<IRecurrence_View>, IRecurrence_ViewModel
    {

        #region ctor
        public RecurrenceViewModel(
            IRecurrence_View view   //View used for this UVM
          ) : base(view)
        {
            //this.BankActivity_ComboBoxViewModel = partnerActivity_ComboBoxViewModel ?? throw new ArgumentNullException(nameof(partnerActivity_ComboBoxViewModel));
            //this.ParentBank_ComboBoxViewModel= parentBank_ComboBoxViewModel ?? throw new ArgumentNullException(nameof(parentBank_ComboBoxViewModel));
            base.View.ViewLoaded += View_Loaded; //ViewLoaded event triggered in the View.
        }
        private void View_Loaded(object sender, EventArgs e)
        {
            //View loaded: Monitor inner views
            //MonitorInnerViewModels();
            //MonitorInnerViewModels();
            base.View.ViewLoaded -= View_Loaded; //Once triggered, cancel the event.
        }
        #endregion ctor

        #region Set View Images/Icons
        //public System.Drawing.Image MenuOthers_Image { get => GetProperty<System.Drawing.Image>(); set => SetProperty(value); }
        public System.Drawing.Image InfoIcon { get => GetProperty<System.Drawing.Image>(); set => SetProperty(value); }
        #endregion Set View Images/Icons

        #region DataModel

        public IRecurrenceInfo DataModel { 
            get => GetProperty<IRecurrenceInfo>();
            set {
                if (SetProperty(value))
                {
                    RaisePropertyChanged(nameof(IsEnabled));
                    Task task = MonitorDataModelAsync(DataModel);
                }

            }
        }
        #region Monitor DataModel
        private Task MonitorDataModelAsync(IRecurrenceInfo dataModel)
        {
            if (dataModel != null)
            {
                Dictionary<string, string[]> propsForCascadeChanged = new Dictionary<string, string[]>()   //Tuples of to-listen ppty <-> dependant ppty/ies
                {
                    //DataModel properties change to listen to
                    { nameof(dataModel.Type), new string[] { nameof(IsType_Daily), nameof(IsType_Weekly), nameof(IsType_Monthly), nameof(IsType_Yearly) } },
                    { nameof(dataModel.WeekDays), new string[] { nameof(IsWeekDays_Monday), nameof(IsWeekDays_Tuesday), nameof(IsWeekDays_Wednesday)
                        , nameof(IsWeekDays_Thursday), nameof(IsWeekDays_Friday), nameof(IsWeekDays_Saturday), nameof(IsWeekDays_Sunday), nameof(Sub_WeekDays)
                        , nameof(WeekDays_IsEveryDay), nameof(WeekDays_IsWeekDays) } },
                    { nameof(dataModel.WeekOfMonth), new string[] { nameof(WeekOfMonth_IsNone), nameof(WeekOfMonth_IsNotNone) } },
                    { nameof(dataModel.Month), new string[] { nameof(MonthNumber) } },

                    //{ nameof(dataModel.IsDirty), new string[] { nameof(IsDirty) } },
                    //{ nameof(dataModel.IsCreating), new string[] { nameof(IsCreating) } },
                };
                foreach (KeyValuePair<string, string[]> item in propsForCascadeChanged)   //Force Raise event before any change
                { foreach (string prop in item.Value) { RaisePropertyChanged(prop); } }
                dataModel.PropertyChanged += delegate (object sender, PropertyChangedEventArgs e)
                {//When a property changed....
                    //Raises dependant properties change
                    if (propsForCascadeChanged.TryGetValue(e.PropertyName, out string[] dependantProperties))
                    { foreach (string prop in dependantProperties) { RaisePropertyChanged(prop); } }
                };


                Dictionary<string, Action> propsForCascadeAction = new Dictionary<string, Action>()   //Tuples of to-listen ppty <-> dependant Action
                {
                    { nameof(dataModel.Start), new Action( () => BusinessRuleFrom_Start() ) }, //Updates FiEntry AmountInAccountCurrency.
                };
                dataModel.PropertyChanged += delegate (object sender, PropertyChangedEventArgs e)
                {//When a property changed launch dependant Action
                    if (propsForCascadeAction.TryGetValue(e.PropertyName, out Action dependantAction)) { dependantAction.Invoke(); }
                };

            }
            return Task.CompletedTask;
        }
        #endregion Monitor DataModel

        #endregion DataModel

        #region View
        public bool IsEnabled { get => GetProperty<bool>(defaultValue: false); private set => SetProperty(value); }

        //#region interface IDockedTab implementation
        //public string DockedTabTitle { set; get; } //Actually set in Bank_ContainerViewModel
        //                                           //public ImageSource DockedTabIcon { set; get; }//Actually set in Bank_ContainerViewModel
        //#endregion interface IDockedTab implementation

        #endregion View


        #region View regions visibility
        public bool IsType_Daily { get => DataModel.Type == ReccurrenceType.Daily; }
        public bool IsType_Weekly { get => DataModel.Type == ReccurrenceType.Weekly; }
        public bool IsType_Monthly { get => DataModel.Type == ReccurrenceType.Monthly; }
        public bool IsType_Yearly { get => DataModel.Type == ReccurrenceType.Yearly; }
        #endregion View regions visibility

        #region WeekDays flags <-> checkbox
        public bool IsWeekDays_Monday
        {
            get => DataModel.WeekDays.HasFlag(WeekDays.Monday);
            set { if (value) DataModel.WeekDays = DataModel.WeekDays.SetFlag(WeekDays.Monday); else DataModel.WeekDays = DataModel.WeekDays.RemoveFlag(WeekDays.Monday); }
        }
        public bool IsWeekDays_Tuesday
        {
            get => DataModel.WeekDays.HasFlag(WeekDays.Tuesday);
            set { if (value) DataModel.WeekDays = DataModel.WeekDays.SetFlag(WeekDays.Tuesday); else DataModel.WeekDays = DataModel.WeekDays.RemoveFlag(WeekDays.Tuesday); }
        }
        public bool IsWeekDays_Wednesday
        {
            get => DataModel.WeekDays.HasFlag(WeekDays.Wednesday);
            set { if (value) DataModel.WeekDays = DataModel.WeekDays.SetFlag(WeekDays.Wednesday); else DataModel.WeekDays = DataModel.WeekDays.RemoveFlag(WeekDays.Wednesday); }
        }
        public bool IsWeekDays_Thursday
        {
            get => DataModel.WeekDays.HasFlag(WeekDays.Thursday);
            set { if (value) DataModel.WeekDays = DataModel.WeekDays.SetFlag(WeekDays.Thursday); else DataModel.WeekDays = DataModel.WeekDays.RemoveFlag(WeekDays.Thursday); }
        }
        public bool IsWeekDays_Friday
        {
            get => DataModel.WeekDays.HasFlag(WeekDays.Friday);
            set { if (value) DataModel.WeekDays = DataModel.WeekDays.SetFlag(WeekDays.Friday); else DataModel.WeekDays = DataModel.WeekDays.RemoveFlag(WeekDays.Friday); }
        }
        public bool IsWeekDays_Saturday
        {
            get => DataModel.WeekDays.HasFlag(WeekDays.Saturday);
            set { if (value) DataModel.WeekDays = DataModel.WeekDays.SetFlag(WeekDays.Saturday); else DataModel.WeekDays = DataModel.WeekDays.RemoveFlag(WeekDays.Saturday); }
        }
        public bool IsWeekDays_Sunday
        {
            get => DataModel.WeekDays.HasFlag(WeekDays.Sunday);
            set { if (value) DataModel.WeekDays = DataModel.WeekDays.SetFlag(WeekDays.Sunday); else DataModel.WeekDays = DataModel.WeekDays.RemoveFlag(WeekDays.Sunday); }
        }
        #endregion WeekDays flags <-> checkbox

        #region Daily WeekDays/EveryDay
        public bool WeekDays_IsEveryDay
        {
            get => DataModel.WeekDays == WeekDays.EveryDay;
            set { if (value) { DataModel.WeekDays = DataModel.WeekDays.ClearFlags(); DataModel.WeekDays = DataModel.WeekDays.SetFlag(WeekDays.EveryDay); } }
        }
        public bool WeekDays_IsWeekDays
        {
            get => DataModel.WeekDays == WeekDays.WorkDays;
            set { if (value) { DataModel.WeekDays = DataModel.WeekDays.ClearFlags(); DataModel.WeekDays = DataModel.WeekDays.SetFlag(WeekDays.WorkDays); } }
        }

        #endregion Daily WeekDays/EveryDay


        #region WeekOfMonth
        public bool WeekOfMonth_IsNone
        {
            get => DataModel.WeekOfMonth == WeekOfMonth.None;
            set { if (value) { DataModel.WeekOfMonth = WeekOfMonth.None; }  }
        }
        public bool WeekOfMonth_IsNotNone
        {
            get => DataModel.WeekOfMonth != WeekOfMonth.None;
            set { if (value) { DataModel.WeekOfMonth = WeekOfMonth.First; } }
        }
        #endregion WeekOfMonth

        #region Month Number
        public MonthsList MonthNumber
        {
            get { return (MonthsList)Enum.Parse(typeof(MonthsList), ((int)DataModel.Month).ToString()); }
            set { DataModel.Month = (int)value; }
        }
        #endregion Month Number

        #region Subset of WeekDays
        public Sub_WeekDays Sub_WeekDays
        {
            get
            {
                int flags = (int)DataModel.WeekDays;
                if (flags > 64) { return Sub_WeekDays.Monday; }
                else { return (Sub_WeekDays)Enum.Parse(typeof(Sub_WeekDays), ((int)DataModel.WeekDays).ToString()); }
            }
            set { DataModel.WeekDays = (WeekDays)Enum.Parse(typeof(WeekDays), ((int)value).ToString()); }
        }
        #endregion Subset of WeekDays


        #region Inner ViewModels
        //Monitor inner view models is done in 2 steps:
        //  1 - get view models and related views from the UI thread,
        // 2 - launch monitoring async in a task.
        private void MonitorInnerViewModels() 
        {
            //this.PartnerComboBox = Common.CreateComboBoxInstance<Common.IPartnerComboBox>.GetInstance(); //Create instance in main STA.
            _ = MonitorInnerViewModelsAsync();

        }
        private Task MonitorInnerViewModelsAsync()
        {
           // Task.Run(() => { MonitorPartnerComboBox(); });
            //Task.Run(() => { MonitorBankActivityComboBox(); });
            //Task.Run(() => { MonitorParentBankComboBox(); });
            return Task.CompletedTask;
        }
        #endregion Inner ViewModels


        #region Commands
        public List<DateTime> NextOccurrences { get => GetProperty<List<DateTime>>(); set => SetProperty(value); }
        private ICommand commandShowRecurrenceInfo; public ICommand CommandShowRecurrenceInfo //Change DataModel mode to IsEditing.
        {
            get => this.commandShowRecurrenceInfo ??= new Foundation.CommandPattern.RelayCommand(
                    execute => //Execute command
                    {
                        var a = DataModel;
                      //  DateTime lastDate = DateTime.Today;
                        NextOccurrences = new API.Scheduler().GetNextOccurrences(DataModel);
                       
                    },
                    canExecute => //CanExecute command
                    {
                        return true;
                    });
        }

        //private ICommand commandEdit; public ICommand CommandEdit //Change DataModel mode to IsEditing.
        //{
        //    get => this.commandEdit ??= new RelayCommand(
        //            execute => //Execute command
        //            {
        //                DataModel.SetDataModelIsEditing();
        //            },
        //            canExecute => //CanExecute command
        //            {
        //                return DataModel != null
        //                    ? !DataModel.IsReadOnly && !DataModel.IsEditing
        //                    : false;
        //            });
        //}
        //private ICommand commandSave; public ICommand CommandSave //Show Currencies list.
        //{
        //    get => this.commandSave ??= new RelayCommand(
        //            execute => //Execute command
        //            {
        //                bool succeed = DataModel.PersistDataModel();
        //                if (!succeed && !DataModel.HasErrors)
        //                {//Database issue while writing.
        //                 // IMessageBox msgBox = Foundation.IoC.ModuleCompositionRoot.BeginLifetimeScope().Resolve<IMessageBox>();
        //                    msgBox.ShowMessage(
        //                        title: "Bank data model save",
        //                        message: "An error occurred. Data model not saved.",
        //                        button: MessageBoxButton.OK,
        //                        icon: MessageBoxImage.Error);
        //                    //MessageBox.Show(
        //                    //    caption: "Bank data model save",
        //                    //    messageBoxText: "An error occurred. Data model not saved.",
        //                    //    icon: MessageBoxImage.Error,
        //                    //    button: MessageBoxButton.OK);
        //                }

        //            },
        //            canExecute => //CanExecute command
        //            {
        //                return DataModel != null
        //                    ? DataModel.IsDirty
        //                    : false;
        //            }
        //        );
        //}
        //private ICommand commandUndo; public ICommand CommandUndo //Undo changes not yet saved.
        //{
        //    get => this.commandUndo ??= new RelayCommand(
        //            execute => //Execute command
        //            {
        //                DataModel.DataModelUndo();
        //                RaisePropertyChanged(nameof(DataModel));
        //            },
        //            canExecute => //CanExecute command
        //            {
        //                return DataModel != null
        //                        ? !DataModel.IsReadOnly && DataModel.IsDirty
        //                        : false;
        //            });
        //}
        //private ICommand commandDelete; public ICommand CommandDelete //Delete the DataModel and request persisting delete.
        //{
        //    get => this.commandDelete ??= new RelayCommand(
        //            execute => //Execute command
        //            {
        //                MessageBoxResult answer = msgBox.ShowMessage(
        //                   title: Assets.Resources.Msg_Delete_Title,
        //                   message: Assets.Resources.Msg_Delete_Text,
        //                   button: MessageBoxButton.YesNo,
        //                   icon: MessageBoxImage.Question);
        //                //MessageBoxResult answer = MessageBox.Show("Sure to delete?", "Delete", MessageBoxButton.YesNo, MessageBoxImage.Question);
        //                if (answer == MessageBoxResult.Yes)
        //                {
        //                    bool succeed = this.bankController.DeleteBank(DataModel).Result;
        //                    if (succeed)
        //                    {
        //                        //     this.DataModel = null;
        //                    }
        //                }
        //            },
        //            canExecute => //CanExecute command
        //            {
        //                return DataModel != null
        //                        ? !DataModel.IsReadOnly && DataModel.IsEditing
        //                        : false;
        //            });
        //}


        #endregion Commands

        #region Business rules
        private void BusinessRuleFrom_Start()
        {
            if (DataModel.End <= DataModel.Start) { DataModel.End = DataModel.Start.AddDays(1); }
        }
        #endregion Business rules
    }


    #region Converters
    public class RecurrenceInfo_Type_Converter : IValueConverter
    { 
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ReccurrenceType type = (ReccurrenceType)value;
            ReccurrenceType parameterConverted = (ReccurrenceType)Enum.Parse(typeof(ReccurrenceType), parameter.ToString());
            if (type == parameterConverted) { return true; } else { return false; }
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)  {  return parameter;  }
    }

    public class RecurrenceInfo_WeekDays_Converter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            WeekDays type = (WeekDays)value;
            WeekDays parameterConverted = (WeekDays)Enum.Parse(typeof(WeekDays), parameter.ToString());
            if ((type & parameterConverted) == parameterConverted) { return true; } else { return false; }
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) { return parameter; }
    }

    public class RecurrenceInfo_WeekOfMonth_Converter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            WeekOfMonth type = (WeekOfMonth)value;
            if (parameter.ToString() == "Not_None" )
                if (type != WeekOfMonth.None) { return true; } else { return false; }
            else
            {
                WeekOfMonth parameterConverted = (WeekOfMonth)Enum.Parse(typeof(WeekOfMonth), parameter.ToString());
                if (type == parameterConverted) { return true; } else { return false; }
            }
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) { return parameter; }
    }

    public class RecurrenceInfo_RecurrenceRange_Converter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            RecurrenceRange type = (RecurrenceRange)value;
            RecurrenceRange parameterConverted = (RecurrenceRange)Enum.Parse(typeof(RecurrenceRange), parameter.ToString());
            if (type == parameterConverted) { return true; } else { return false; }
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) { return parameter; }
    }


    #endregion Converters



    public enum Sub_WeekDays
    {
        [EnumInfo(ResourceType = typeof(Assets.Resources),//where Resources is the Resources file to look into.
            Display = nameof(Assets.Resources.Enum_WeekDays_Monday))]
        Monday = WeekDays.Monday,

        [EnumInfo(ResourceType = typeof(Assets.Resources),//where Resources is the Resources file to look into.
           Display = nameof(Assets.Resources.Enum_WeekDays_Tuesday))]
        Tuesday = WeekDays.Tuesday,

        [EnumInfo(ResourceType = typeof(Assets.Resources),//where Resources is the Resources file to look into.
            Display = nameof(Assets.Resources.Enum_WeekDays_Wednesday))]
        Wednesday = WeekDays.Wednesday,

        [EnumInfo(ResourceType = typeof(Assets.Resources),//where Resources is the Resources file to look into.
            Display = nameof(Assets.Resources.Enum_WeekDays_Thursday))]
        Thursday = WeekDays.Thursday,

        [EnumInfo(ResourceType = typeof(Assets.Resources),//where Resources is the Resources file to look into.
            Display = nameof(Assets.Resources.Enum_WeekDays_Friday))]
        Friday = WeekDays.Friday,

        [EnumInfo(ResourceType = typeof(Assets.Resources),//where Resources is the Resources file to look into.
            Display = nameof(Assets.Resources.Enum_WeekDays_Saturday))]
        Saturday = WeekDays.Saturday,

        [EnumInfo(ResourceType = typeof(Assets.Resources),//where Resources is the Resources file to look into.
            Display = nameof(Assets.Resources.Enum_WeekDays_Sunday))]
        Sunday = WeekDays.Sunday,
        //WeekendDay = WeekDays.WeekendDays,
        //WorkDay = WeekDays.WorkDays,
    }

    public enum MonthsList
    {
        [EnumInfo(ResourceType = typeof(Assets.Resources),//where Resources is the Resources file to look into.
            Display = nameof(Assets.Resources.Enum_Month_January))]
        January = 1,

        [EnumInfo(ResourceType = typeof(Assets.Resources),//where Resources is the Resources file to look into.
            Display = nameof(Assets.Resources.Enum_Month_February))]
        February,

        [EnumInfo(ResourceType = typeof(Assets.Resources),//where Resources is the Resources file to look into.
            Display = nameof(Assets.Resources.Enum_Month_March))]
        March,

        [EnumInfo(ResourceType = typeof(Assets.Resources),//where Resources is the Resources file to look into.
            Display = nameof(Assets.Resources.Enum_Month_April))]
        April,

        [EnumInfo(ResourceType = typeof(Assets.Resources),//where Resources is the Resources file to look into.
            Display = nameof(Assets.Resources.Enum_Month_May))]
        May,

        [EnumInfo(ResourceType = typeof(Assets.Resources),//where Resources is the Resources file to look into.
            Display = nameof(Assets.Resources.Enum_Month_June))]
        June,

        [EnumInfo(ResourceType = typeof(Assets.Resources),//where Resources is the Resources file to look into.
            Display = nameof(Assets.Resources.Enum_Month_July))]
        July,

        [EnumInfo(ResourceType = typeof(Assets.Resources),//where Resources is the Resources file to look into.
            Display = nameof(Assets.Resources.Enum_Month_August))]
        August,

        [EnumInfo(ResourceType = typeof(Assets.Resources),//where Resources is the Resources file to look into.
            Display = nameof(Assets.Resources.Enum_Month_September))]
        September,

        [EnumInfo(ResourceType = typeof(Assets.Resources),//where Resources is the Resources file to look into.
            Display = nameof(Assets.Resources.Enum_Month_October))]
        October,

        [EnumInfo(ResourceType = typeof(Assets.Resources),//where Resources is the Resources file to look into.
            Display = nameof(Assets.Resources.Enum_Month_November))]
        November,

        [EnumInfo(ResourceType = typeof(Assets.Resources),//where Resources is the Resources file to look into.
            Display = nameof(Assets.Resources.Enum_Month_December))]
        December,
    }

}
