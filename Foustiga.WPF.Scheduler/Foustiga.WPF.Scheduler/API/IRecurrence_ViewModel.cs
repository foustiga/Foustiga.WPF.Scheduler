using Foustiga.WPF.Scheduler.API;
using Foustiga.WPF.Scheduler.DataModel;
using Foustiga.WPF.Scheduler.Foundation.ViewModel;

namespace Foustiga.WPF.Scheduler.API

{
    public interface IRecurrence_ViewModel : IUnitViewModel<IRecurrenceInfo>
    {
        System.Drawing.Image InfoIcon { get; set; }
    }
}
