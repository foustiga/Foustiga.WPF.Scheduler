using System;
using System.Collections.Generic;
using System.Text;

namespace Foustiga.WPF.Scheduler.Foundation.ViewInterface
{
    public interface IViewClose
    {
        /// <summary>
        /// On tab closing action, this method is launched. The ViewModel is supposed to check for any unsaved data.
        /// </summary>
        bool RequestClosingFromView();

        /// <summary>
        /// Request coming from the ViewModel to close the View as DockedTab.
        /// Ensure to listen to this property change event and duplicate in the Container View Model.
        /// </summary>
        bool IsRequestingCloseFromViewModel { get; }

    }
}
