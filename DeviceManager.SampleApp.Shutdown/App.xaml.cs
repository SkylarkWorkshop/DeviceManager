using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Popups;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace DeviceManager.SampleApp.Shutdown
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    sealed partial class App : Application
    {
        
        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            Microsoft.ApplicationInsights.WindowsAppInitializer.InitializeAsync(
                Microsoft.ApplicationInsights.WindowsCollectors.Metadata |
                Microsoft.ApplicationInsights.WindowsCollectors.Session);
            this.InitializeComponent();
            this.Suspending += OnSuspending;
            this.UnhandledException += OnUnhandledException;
            
        }
        private async void OnUnhandledException(object sender, Windows.UI.Xaml.UnhandledExceptionEventArgs e)
        {
            e.Handled = true;
            await new MessageDialog("Make sure you have configured your device correctly.\n\n" + e.Exception.Message, "Error")
                .ShowAsync();
            (Window.Current.Content as Frame).Navigate(typeof(ConfigPage));
        }
        private void RegisterExceptionHandlingSynchronizationContext()
        {
            ExceptionHandlingSynchronizationContext
                .Register()
                .UnhandledException += SynchronizationContext_UnhandledException;
        }

        private async void SynchronizationContext_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            e.Handled = true;
            await new MessageDialog("Make sure you have configured your device correctly.\n\n" + e.Exception.Message, "Error")
                 .ShowAsync();
            (Window.Current.Content as Frame).Navigate(typeof(ConfigPage));
        }
        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used such as when the application is launched to open a specific file.
        /// </summary>
        /// <param name="e">Details about the launch request and process.</param>
        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {
            RegisterExceptionHandlingSynchronizationContext();
#if DEBUG
            if (System.Diagnostics.Debugger.IsAttached)
            {
                this.DebugSettings.EnableFrameRateCounter = true;
            }
#endif
            Frame rootFrame = Window.Current.Content as Frame;

            
            // Do not repeat app initialization when the Window already has content,
            // just ensure that the window is active
            if (rootFrame == null)
            {
                // Create a Frame to act as the navigation context and navigate to the first page
                rootFrame = new Frame();

                rootFrame.NavigationFailed += OnNavigationFailed;
                if (e.PreviousExecutionState != ApplicationExecutionState.Running)
                {
                    bool loadState = (e.PreviousExecutionState == ApplicationExecutionState.Terminated);
                    ExtendedSplash extendedSplash = new ExtendedSplash(e.SplashScreen, loadState);
                    rootFrame.Content = extendedSplash;
                    Window.Current.Content = rootFrame;
                }
                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    //TODO: Load state from previously suspended application
                }

                // Place the frame in the current Window
                Window.Current.Content = rootFrame;
            }
            
            if (e.PrelaunchActivated == false)
            {
                if (rootFrame.Content == null)
                {
                    // When the navigation stack isn't restored navigate to the first page,
                    // configuring the new page by passing required information as a navigation
                    // parameter
                }
                // Ensure the current window is active
                Window.Current.Activate();
            }
        }

        /// <summary>
        /// Invoked when Navigation to a certain page fails
        /// </summary>
        /// <param name="sender">The Frame which failed navigation</param>
        /// <param name="e">Details about the navigation failure</param>
        void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        /// <summary>
        /// Invoked when application execution is being suspended.  Application state is saved
        /// without knowing whether the application will be terminated or resumed with the contents
        /// of memory still intact.
        /// </summary>
        /// <param name="sender">The source of the suspend request.</param>
        /// <param name="e">Details about the suspend request.</param>
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            //TODO: Save application state and stop any background activity
            deferral.Complete();
        }
        protected override void OnActivated(IActivatedEventArgs args)
        {
            RegisterExceptionHandlingSynchronizationContext();
        }
    }
    /// <summary>
    /// Wrapper around a standard synchronization context, that catches any unhandled exceptions.
    /// Acts as a facade passing calls to the original SynchronizationContext
    /// </summary>
    /// <example>
    /// Set this up inside your App.xaml.cs file as follows:
    /// <code>
    /// protected override void OnActivated(IActivatedEventArgs args)
    /// {
    ///     EnsureSyncContext();
    ///     ...
    /// }
    /// 
    /// protected override void OnLaunched(LaunchActivatedEventArgs args)
    /// {
    ///     EnsureSyncContext();
    ///     ...
    /// }
    /// 
    /// private void EnsureSyncContext()
    /// {
    ///     var exceptionHandlingSynchronizationContext = ExceptionHandlingSynchronizationContext.Register();
    ///     exceptionHandlingSynchronizationContext.UnhandledException += OnSynchronizationContextUnhandledException;
    /// }
    /// 
    /// private void OnSynchronizationContextUnhandledException(object sender, UnhandledExceptionEventArgs args)
    /// {
    ///     args.Handled = true;
    /// }
    /// </code>
    /// </example>
    public class ExceptionHandlingSynchronizationContext : SynchronizationContext
    {
        /// <summary>
        /// Registration method.  Call this from OnLaunched and OnActivated inside the App.xaml.cs
        /// </summary>
        /// <returns></returns>
        public static ExceptionHandlingSynchronizationContext Register()
        {
            var syncContext = Current;
            if (syncContext == null)
                throw new InvalidOperationException("Ensure a synchronization context exists before calling this method.");


            var customSynchronizationContext = syncContext as ExceptionHandlingSynchronizationContext;


            if (customSynchronizationContext == null)
            {
                customSynchronizationContext = new ExceptionHandlingSynchronizationContext(syncContext);
                SetSynchronizationContext(customSynchronizationContext);
            }


            return customSynchronizationContext;
        }

        /// <summary>
        /// Links the synchronization context to the specified frame
        /// and ensures that it is still in use after each navigation event
        /// </summary>
        /// <param name="rootFrame"></param>
        /// <returns></returns>
        public static ExceptionHandlingSynchronizationContext RegisterForFrame(Frame rootFrame)
        {
            if (rootFrame == null)
                throw new ArgumentNullException(nameof(rootFrame));

            var synchronizationContext = Register();

            rootFrame.Navigating += (sender, args) => EnsureContext(synchronizationContext);
            rootFrame.Loaded += (sender, args) => EnsureContext(synchronizationContext);

            return synchronizationContext;
        }

        private static void EnsureContext(SynchronizationContext context)
        {
            if (Current != context)
                SetSynchronizationContext(context);
        }


        private readonly SynchronizationContext _syncContext;


        public ExceptionHandlingSynchronizationContext(SynchronizationContext syncContext)
        {
            _syncContext = syncContext;
        }


        public override SynchronizationContext CreateCopy()
        {
            return new ExceptionHandlingSynchronizationContext(_syncContext.CreateCopy());
        }


        public override void OperationCompleted()
        {
            _syncContext.OperationCompleted();
        }


        public override void OperationStarted()
        {
            _syncContext.OperationStarted();
        }


        public override void Post(SendOrPostCallback d, object state)
        {
            _syncContext.Post(WrapCallback(d), state);
        }


        public override void Send(SendOrPostCallback d, object state)
        {
            _syncContext.Send(d, state);
        }


        private SendOrPostCallback WrapCallback(SendOrPostCallback sendOrPostCallback)
        {
            return state =>
            {
                try
                {
                    sendOrPostCallback(state);
                }
                catch (Exception ex)
                {
                    if (!HandleException(ex))
                        throw;
                }
            };
        }

        private bool HandleException(Exception exception)
        {
            if (UnhandledException == null)
                return false;

            var exWrapper = new UnhandledExceptionEventArgs
            {
                Exception = exception
            };

            UnhandledException(this, exWrapper);

#if DEBUG && !DISABLE_XAML_GENERATED_BREAK_ON_UNHANDLED_EXCEPTION
            if (System.Diagnostics.Debugger.IsAttached) System.Diagnostics.Debugger.Break();
#endif

            return exWrapper.Handled;
        }


        /// <summary>
        /// Listen to this event to catch any unhandled exceptions and allow for handling them
        /// so they don't crash your application
        /// </summary>
        public event EventHandler<UnhandledExceptionEventArgs> UnhandledException;
    }

    public class UnhandledExceptionEventArgs : EventArgs
    {
        public bool Handled { get; set; }
        public Exception Exception { get; set; }
    }
}
