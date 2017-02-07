using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using UWPBFIDE.Views;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Foundation.Metadata;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace UWPBFIDE
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private ApplicationViewTitleBar titleBar;
        public static MainPage Current; 
        public MainPage()
        {
            this.InitializeComponent();
            Loaded += MainPage_Loaded;
            Current = this;
        }
        private async void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            if (ApiInformation.IsApiContractPresent("Windows.Phone.PhoneContract", 1, 0))
            {
                var statusBar = StatusBar.GetForCurrentView();
                await statusBar.HideAsync();
                tooler.Margin = new Thickness(0, 7, 20, 0);
            }

            // tanzime title bar
            CoreApplicationViewTitleBar coreTitleBar = CoreApplication.GetCurrentView().TitleBar;
            coreTitleBar.ExtendViewIntoTitleBar = true;
            coreTitleBar.IsVisibleChanged += CoreTitleBar_IsVisibleChanged;
            coreTitleBar.LayoutMetricsChanged += CoreTitleBar_LayoutMetricsChanged;

            Window.Current.SetTitleBar(BackgroundElement);

            // tagheire range dokmehaye title bar (dokmehaye exit, minimize, maximum)
            titleBar = ApplicationView.GetForCurrentView().TitleBar;
            titleBar.BackgroundColor = Colors.Transparent;
            titleBar.ForegroundColor = new Color { A = 150, R = 0, G = 0, B = 0 };

            titleBar.ButtonBackgroundColor = Colors.Transparent;
            titleBar.ButtonForegroundColor = new Color { A = 150, R = 0, G = 0, B = 0 };
        }

        void CoreTitleBar_IsVisibleChanged(CoreApplicationViewTitleBar titleBar, object args)
        {
            TitleBar.Visibility = titleBar.IsVisible ? Visibility.Visible : Visibility.Collapsed;
        }
        private void CoreTitleBar_LayoutMetricsChanged(CoreApplicationViewTitleBar sender, object args)
        {
            if (FlowDirection == FlowDirection.LeftToRight)
                RightMask.Width = sender.SystemOverlayRightInset;
            else
                RightMask.Width = sender.SystemOverlayLeftInset;
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            frameme.Navigate(typeof(MainF));
            base.OnNavigatedTo(e);
            if (frameme.CanGoBack)
            {
                Back.Visibility = Visibility.Visible;
            }
            else
            {
                Back.Visibility = Visibility.Collapsed;
            }

        }
        private void menubutton_Click(object sender, RoutedEventArgs e)
        {
            RootSplitView.IsPaneOpen = !RootSplitView.IsPaneOpen;
        }

        private void run_Click(object sender, RoutedEventArgs e)
        {
            MainF.Current.runer();
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            MainF.Current.cleaner();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            if (frameme.CanGoBack)
                frameme.GoBack();
            if (frameme.CanGoBack)
            {
                Back.Visibility = Visibility.Visible;
            }
            else
            {
                Back.Visibility = Visibility.Collapsed;
            }
        }

        private void about_Click(object sender, RoutedEventArgs e)
        {
            frameme.Navigate(typeof(fAbout));
        }

        private async void save_Click(object sender, RoutedEventArgs e)
        {
            var savePicker = new Windows.Storage.Pickers.FileSavePicker();
            savePicker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.DocumentsLibrary;
            // Dropdown of file types the user can save the file as
            savePicker.FileTypeChoices.Add("Brain Fuck File", new List<string>() { ".bf" });
            // Default file name if the user does not type one in or select a file to replace
            savePicker.SuggestedFileName = "BrainProject";
            Windows.Storage.StorageFile file = await savePicker.PickSaveFileAsync();
            if (file != null)
            {
                Windows.Storage.CachedFileManager.DeferUpdates(file);
                Windows.Storage.CachedFileManager.DeferUpdates(file);
                await Windows.Storage.FileIO.WriteTextAsync(file, MainF.Current.textBox.Text);
                Windows.Storage.Provider.FileUpdateStatus status =
           await Windows.Storage.CachedFileManager.CompleteUpdatesAsync(file);
                if (status == Windows.Storage.Provider.FileUpdateStatus.Complete)
                {
                    ContentDialog noWifiDialog = new ContentDialog()
                    {
                        Title = "Success!",
                        Content = "Backup had been saved .",
                        PrimaryButtonText = "Nice!"
                    };
                    noWifiDialog.ShowAsync();
                }
                else
                {
                    ContentDialog noWifiDialog = new ContentDialog()
                    {
                        Title = ":(",
                        Content = "Something went wrong",
                        PrimaryButtonText = "OK"
                    };
                    noWifiDialog.ShowAsync();
                }

            }
            else
            {
                ContentDialog noWifiDialog = new ContentDialog()
                {
                    Title = ":/",
                    Content = "Operation canceled",
                    PrimaryButtonText = "OK"
                };
                noWifiDialog.ShowAsync();
            }

        }
    }

}
