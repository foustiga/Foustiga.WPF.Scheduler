
using Foustiga.WPF.Scheduler.Foundation.ViewInterface;

namespace Foustiga.WPF.Scheduler.Foundation.WPFViewType
{
    //WPF specialization of UserInterface ViewContract IView, which requests to implement DataContext and  event ViewLoaded
    //All UserControls in this WPF project to reference this interface.
    public interface IWPFUserControl : IView
    {
    }
}
