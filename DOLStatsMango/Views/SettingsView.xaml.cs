using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.Phone.Controls;
using DOLStatsMango.Resources.en;

namespace DOLStatsMango.Views
{
    public partial class SettingsView : PhoneApplicationPage
    {
        private GeoCoordinateWatcher watcher;

        public SettingsView()
        {
            Messenger.Default.Register<bool>(this, "SaveAppSettingsComplete", OnSaveAppSettingsComplete);
            InitializeComponent();
            Loaded += new RoutedEventHandler(SettingsView_Loaded);
        }

        void SettingsView_Loaded(object sender, RoutedEventArgs e)
        {
            txtLocationMessage.Text = AppResources.Settings_LocationMessage;
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            // Set the data context
            if (DataContext == null)
                DataContext = App.MainVM;

            // The watcher variable was previously declared as type GeoCoordinateWatcher.
            if (watcher == null)
            {
                watcher = new GeoCoordinateWatcher(GeoPositionAccuracy.Default); // using default accuracy
                watcher.MovementThreshold = 20; // use MovementThreshold to ignore noise in the signal
                watcher.StatusChanged += new EventHandler<GeoPositionStatusChangedEventArgs>(watcher_StatusChanged);
            }
            Deployment.Current.Dispatcher.BeginInvoke(() => watcher.Start());
        }

        protected override void OnNavigatedFrom(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);

            //App.MainVM.CurrentSettings = null;
        }

        private void appBarIcon_Save_Click(object sender, EventArgs e)
        {
            this.Focus();

            Messenger.Default.Send<bool>(true, "SaveSettingsRequest");
        }

        private void appBarIcon_Cancel_Click(object sender, EventArgs e)
        {
            //App.MainVM.CurrentSettings = null;

            if (NavigationService.CanGoBack)
                NavigationService.GoBack();
        }

        void watcher_StatusChanged(object sender, GeoPositionStatusChangedEventArgs e)
        {
            Deployment.Current.Dispatcher.BeginInvoke(() => MyStatusChanged(e));
        }

        private void MyStatusChanged(GeoPositionStatusChangedEventArgs e)
        {
            switch (e.Status)
            {
                case GeoPositionStatus.Disabled:
                    txtGpsEnabled.Text = "Disabled";
                    //Stop the Location Service to conserve battery power.
                    watcher.Stop();
                    break;

                case GeoPositionStatus.Initializing:
                    // The Location Service is initializing.
                    txtGpsEnabled.Text = "Enabled";
                    //Stop the Location Service to conserve battery power.
                    watcher.Stop();
                    break;

                case GeoPositionStatus.NoData:
                    // The Location Service is working, but it cannot get location data.
                    txtGpsEnabled.Text = "Enabled";
                    //Stop the Location Service to conserve battery power.
                    watcher.Stop();
                    break;

                case GeoPositionStatus.Ready:
                    // The Location Service is working and is receiving location data.
                    txtGpsEnabled.Text = "Enabled";
                    //Stop the Location Service to conserve battery power.
                    watcher.Stop();
                    break;
            }
        }

        private void OnSaveAppSettingsComplete(bool payload)
        {
            if (payload)
            {
                if (NavigationService.CanGoBack)
                    NavigationService.GoBack();
            }
        }
    }
}