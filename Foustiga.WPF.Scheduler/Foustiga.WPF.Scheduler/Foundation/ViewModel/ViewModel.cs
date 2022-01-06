using Foustiga.WPF.Scheduler.Foundation.ViewInterface;
//using System.Windows.Threading;

namespace Foustiga.WPF.Scheduler.Foundation.ViewModel
{
    /// <summary>Abstract base class for a ViewModel implementation.</summary>
    /// <typeparam name="TView">The type of the view. Do provide an interface as type and not the concrete type itself.</typeparam>
    internal abstract class ViewModel<TView> : ViewModelCore<TView> where TView : IView//, IPartImportsSatisfiedNotification where TView : IView
    {
        /// <summary>Initializes a new instance of the <see cref="ViewModel{TView}"/> class and attaches itself as DataContext to the view.</summary>
        /// <param name="view">The view.</param>
        protected ViewModel(TView view) : base(view, false)
        {
            // When the code runs outside of the WPF application model then we set the DataContext immediately.
            view.DataContext = this;
        }
    }
}
