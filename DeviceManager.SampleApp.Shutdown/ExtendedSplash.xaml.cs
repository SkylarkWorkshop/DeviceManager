using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace DeviceManager.SampleApp.Shutdown
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ExtendedSplash : Page
    {
        MobileDevice device;
        private SplashScreen splash;
        internal Frame rootFrame;
        public ExtendedSplash()
        {
            this.InitializeComponent();
        }
        public ExtendedSplash(SplashScreen splashscreen, bool loadState)
        {
            InitializeComponent();
            splash = splashscreen;
            SetStatusBar(Color.FromArgb(255, 51, 51, 51), Colors.White);
            DismissExtendedSplash();
            rootFrame = new Frame();
        }
        private static async void SetStatusBar(Color bc, Color fc)
        {
            if (Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.UI.ViewManagement.StatusBar"))
            {
                var statusBar = StatusBar.GetForCurrentView();
                statusBar.BackgroundColor = bc;
                statusBar.ForegroundColor = fc;
                statusBar.BackgroundOpacity = 1;
                await statusBar.ShowAsync();
            }
        }
        async void DismissExtendedSplash()
        {
            device = MobileDeviceManager.ConnectLocal();
            while (!device.IsReady)
            {
                await Task.Delay(5);
            }
            if (device.IsAuthed && device.IsConnected)
            {
                rootFrame.Navigate(typeof(StartPage));
                Window.Current.Content = rootFrame;
            }
            else
            {
                rootFrame.Navigate(typeof(ConfigPage));
                Window.Current.Content = rootFrame;
            }
        }
    }
}
