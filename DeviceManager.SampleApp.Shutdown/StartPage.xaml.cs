using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Notifications;
using Windows.UI.Popups;
using Windows.UI.StartScreen;
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
    public sealed partial class StartPage : Page
    {
        MobileDevice device;
        public StartPage()
        {
            this.InitializeComponent();
            device = MobileDeviceManager.ConnectLocal();
            SetStatusBar(Color.FromArgb(255, 51, 51, 51), Colors.White);
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
        private async void shut_btn_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                var msg = new MessageDialog("Shutdown?", "Confirm");
                msg.Commands.Add(new UICommand("Yes", command =>
                {
                    try
                    {
                        device.Shutdown();
                    }
                    catch
                    {
                        Frame.Navigate(typeof(ConfigPage));
                    }
                }));
                msg.Commands.Add(new UICommand("No", command =>
                {
                    return;
                }));
                await msg.ShowAsync();

            }
            catch
            {
                Frame.Navigate(typeof(ConfigPage));
            }
            
        }

        private async void restart_btn_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                var msg = new MessageDialog("Restart?", "Confirm");
                msg.Commands.Add(new UICommand("Yes", command =>
                {
                    try
                    {
                        device.Reboot();
                    }
                    catch
                    {
                        Frame.Navigate(typeof(ConfigPage));
                    }
                }));
                msg.Commands.Add(new UICommand("No", command =>
                {
                    return;
                }));
                await msg.ShowAsync();

            }
            catch
            {
                Frame.Navigate(typeof(ConfigPage));
            }
        }
    }
}
