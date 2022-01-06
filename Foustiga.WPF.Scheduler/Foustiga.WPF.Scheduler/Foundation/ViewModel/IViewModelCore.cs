using Foustiga.WPF.Scheduler.Foundation.ViewInterface;

namespace Foustiga.WPF.Scheduler.Foundation.ViewModel
{
    /// <summary>Interface for a ViewModel.</summary>
    public interface IViewModelCore
    {
        /// <summary>Gets the associated view.</summary>
        IView View { get; }

        /// <summary>Initialize the view model.</summary>
        void Initialize();
    }
}
