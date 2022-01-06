using Foustiga.WPF.Scheduler.API;
using Foustiga.WPF.Scheduler.Assets;
using Foustiga.WPF.Scheduler.Foundation.WPFViewType;

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace Foustiga.WPF.Scheduler.UI_WPF
{ 
    /// <summary>
    /// Logique d'interaction pour Bank_UnitView.xaml
    /// </summary>
    public partial class Recurrence_View :  IRecurrence_View, IWPFUserControl
    {
        #region ctor
        public Recurrence_View()
        {
            InitializeComponent();
            Loaded += FirstTimeLoadedHandler; ;
        }
        #endregion ctor

        #region View Loaded
        public event EventHandler ViewLoaded; //IView interface implementation
        private void FirstTimeLoadedHandler(object sender, RoutedEventArgs e)
        {
            Loaded -= FirstTimeLoadedHandler;  // Ensure that this handler is called only once.
            Dispatcher.BeginInvoke(new Action(() => //Wait for View loading finished before lauching ViewModel actions.
            {
                ViewLoaded?.Invoke(this, e);

                //Set ViewModel DockTabIcon property to related Image
                if (!((this.DataContext as IRecurrence_ViewModel) is null))
                {
                    (this.DataContext as IRecurrence_ViewModel).InfoIcon = WPF_GetResource.GetDrawingImageFromResource("Info.png");
                }

            }), DispatcherPriority.ContextIdle, null);

        }
        #endregion View Loaded

    }
}
