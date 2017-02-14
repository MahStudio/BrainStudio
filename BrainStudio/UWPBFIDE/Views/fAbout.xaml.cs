using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.ApplicationModel.DataTransfer;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace UWPBFIDE.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class fAbout : Page
    {
        private DataTransferManager dataTransferManager;
        public fAbout()
        {
            this.InitializeComponent();
            ApplicationName.Text = Package.Current.DisplayName;
            var v = Package.Current.Id.Version;
            ApplicationVersion.Text = "V " + string.Format("{0}.{1}.{2}.{3}", v.Major, v.Minor, v.Build, v.Revision);
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (MainPage.Current.frameme.CanGoBack)
            {
                MainPage.Current.Back.Visibility = Visibility.Visible;
            }
            else
            {
                MainPage.Current.Back.Visibility = Visibility.Collapsed;
            }

            // register this page as share source
            this.dataTransferManager = DataTransferManager.GetForCurrentView();
            this.dataTransferManager.DataRequested += new TypedEventHandler<DataTransferManager, DataRequestedEventArgs>(this.DataRequested);
        }
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            // unregister as share source
            this.dataTransferManager.DataRequested -= new TypedEventHandler<DataTransferManager, DataRequestedEventArgs>(this.DataRequested);
        }
        private void DataRequested(DataTransferManager sender, DataRequestedEventArgs e)
        {
            Uri dataPackageUri = new Uri("https://www.microsoft.com/store/apps/9nk2p1vzmq23");
            DataPackage requestData = e.Request.Data;
            requestData.Properties.Title = "Brain Studio | Code everywhere!";
            requestData.SetWebLink(dataPackageUri);
            requestData.Properties.Description = "Check out the UWP IDE for BrainFuck Programming Language";
        }
        private async void RateApp(object sender, RoutedEventArgs e)
        {
            await Windows.System.Launcher.LaunchUriAsync(new Uri(string.Format("ms-windows-store:REVIEW?PFN={0}", Windows.ApplicationModel.Package.Current.Id.FamilyName)));
        }

        private async void TelegramChannel(object sender, RoutedEventArgs e)
        {
            await Windows.System.Launcher.LaunchUriAsync(new Uri("http://t.me/irmahgraphic", UriKind.RelativeOrAbsolute));
        }
        private async void InstaPage(object sender, RoutedEventArgs e)
        {
            await Windows.System.Launcher.LaunchUriAsync(new Uri("https://www.instagram.com/mah_graphic_studio", UriKind.RelativeOrAbsolute));
        }
        private void share_Click(object sender, RoutedEventArgs e)
        {
            Windows.ApplicationModel.DataTransfer.DataTransferManager.ShowShareUI();
        }
        private async void rate_Click(object sender, RoutedEventArgs e)
        {
            await Windows.System.Launcher.LaunchUriAsync(new Uri(string.Format("ms-windows-store:REVIEW?PFN={0}", Windows.ApplicationModel.Package.Current.Id.FamilyName)));
        }
        private async void fback_Click(object sender, RoutedEventArgs e)
        {
            if (Microsoft.Services.Store.Engagement.StoreServicesFeedbackLauncher.IsSupported())
            {
                var launcher = Microsoft.Services.Store.Engagement.StoreServicesFeedbackLauncher.GetDefault();
                await launcher.LaunchAsync();
            }
            else
            {
                await Windows.System.Launcher.LaunchUriAsync(new Uri("mailto:ngame1390@live.com?subject=WikiSeda_U3_FeedBack&cc=mohsens22@outlook.com"));
            }

        }
    }
}
