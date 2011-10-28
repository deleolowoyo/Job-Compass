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
using Microsoft.Phone.Controls;
using Microsoft.Phone.Controls.Maps;
using DOLStatsMango.Resources.en;
using Microsoft.Phone.Controls.Maps.Core;
using Microsoft.Phone.Tasks;

namespace DOLStatsMango.Views
{
    public partial class JobMapView : PhoneApplicationPage
    {
        private bool isRoadMode;
        public JobMapView()
        {
            InitializeComponent();
            Loaded += new RoutedEventHandler(JobMapView_Loaded);
        }

        void JobMapView_Loaded(object sender, RoutedEventArgs e)
        {
            mapJobLocation.CredentialsProvider = new ApplicationIdCredentialsProvider(AppResources.BingMapsApiKey);

            // Set the center coordinate and zoom level
            GeoCoordinate mapCenter = new GeoCoordinate(App.JobVM.JobInfo.LocationLatitude, App.JobVM.JobInfo.LocationLongitude);
            int zoom = 15;

            // Create a pushpin to put at the center of the view
            Pushpin pin1 = new Pushpin();
            pin1.Location = mapCenter;
            pin1.Style = (Style)App.Current.Resources["PushpinStyle"];
            TextBlock pinContent = new TextBlock
            {
                Text = App.JobVM.JobInfo.Ranking.ToString(),
                FontSize = 18.667,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
				Style = (Style)App.Current.Resources["PhoneTextYellowStyle"]
            };
            pin1.Content = pinContent;
            mapJobLocation.Children.Add(pin1);

            // Set the map style to Aerial
            mapJobLocation.Mode = new RoadMode();
            isRoadMode = true;

            // Set the view and put the map on the page
            mapJobLocation.SetView(mapCenter, zoom);
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            // Set the data context
            if (DataContext == null)
                DataContext = App.JobVM;
        }

        private void appBarBtnMapView_Click(object sender, EventArgs e)
        {
            //Switch between map views
            if (isRoadMode)
            {
                mapJobLocation.Mode = new AerialMode();
                isRoadMode = false;
            }
            else
            {
                mapJobLocation.Mode = new RoadMode();
                isRoadMode = true;
            }
        }

        private void appBarGetDirections_Click(object sender, EventArgs e)
        {
            BingMapsDirectionsTask mapDirectionTask = new BingMapsDirectionsTask
            {
                //Start = new LabeledMapLocation("My Location", new GeoCoordinate(App.JobVM.JobInfo.LocationLatitude, App.JobVM.JobInfo.)),
                End = new LabeledMapLocation(App.JobVM.JobInfo.Company, new GeoCoordinate(App.JobVM.JobInfo.LocationLatitude, App.JobVM.JobInfo.LocationLongitude))
            };

            mapDirectionTask.Show();
        }
    }
}