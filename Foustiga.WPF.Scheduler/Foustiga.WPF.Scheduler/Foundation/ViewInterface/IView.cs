using System;

namespace Foustiga.WPF.Scheduler.Foundation.ViewInterface
{
    /// <summary>Represents a view</summary>
    public interface IView
    {
        /// <summary>Gets or sets the data context of the view.</summary>
        object DataContext { get; set; }

        /// <summary>
        /// Event raised once the View is Loaded.
        /// </summary>
        event EventHandler ViewLoaded;

       // object View { get; }

    }
}
