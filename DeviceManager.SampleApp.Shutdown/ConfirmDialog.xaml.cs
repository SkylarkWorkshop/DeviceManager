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

// The Content Dialog item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace DeviceManager.SampleApp.Shutdown
{
    public sealed partial class ConfirmDialog : ContentDialog
    {

        public ConfirmDialog()
        {
            this.InitializeComponent();
        }
        public ConfirmDialog(string content,string title,string primaryBtnText,string secondaryBtnText):this()
        {
            this.Title = title;
            this.Content = content;
            this.PrimaryButtonText = primaryBtnText;
            this.SecondaryButtonText = secondaryBtnText;
        }
        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }
    }
}
