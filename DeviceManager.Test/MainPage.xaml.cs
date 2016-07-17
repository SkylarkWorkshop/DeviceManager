using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using DeviceManager;
using System.Diagnostics;
using System.Threading.Tasks;
// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace DeviceManager.Test
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }
        MobileDevice device;
        private void conn_btn_Click(object sender, RoutedEventArgs e)
        {
            device=MobileDeviceManager.ConnectLocal();
        }

        private async void shut_btn_Click(object sender, RoutedEventArgs e)
        {
            if (device.IsAuthed == false)
            {
                device.Auth(new ConnectCredential() { UserName="Administrator",Pin="gao20011106" });
            }
            await Task.Delay(100);
            var list=await device.GetProcessesInfoAsync();
            foreach(var i in list)
            {
                Debug.WriteLine(i.ImageName);
            }
        }
    }
}
