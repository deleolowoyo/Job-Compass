using System;
using System.Collections.Generic;
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
using Microsoft.Phone.Controls.Maps;
using DOLStatsMango.Resources.en;
using System.Device.Location;
using Microsoft.Phone.Tasks;
using Microsoft.Phone.Shell;
using System.Threading;

namespace DOLStatsMango.Views
{
    public partial class JobDetailsView : PhoneApplicationPage
    {
        public JobDetailsView()
        {
            Messenger.Default.Register<bool>(this, "LoadJobCompleteRequest", OnLoadJobComplete);
            Messenger.Default.Register<bool>(this, "GetSavedJobsComplete", OnGetSavedJobsComplete);
            Messenger.Default.Register<bool>(this, "SaveDeleteJobCompleteRequest", OnSaveDeleteJobComplete);
            InitializeComponent();
            Loaded += new RoutedEventHandler(JobDetailsView_Loaded);
        }

        void JobDetailsView_Loaded(object sender, RoutedEventArgs e)
        {
            mapJobLocation.CredentialsProvider = new ApplicationIdCredentialsProvider(AppResources.BingMapsApiKey);
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            // Set the data context
            if (DataContext == null)
                DataContext = App.JobVM;

            if (this.NavigationContext.QueryString.ContainsKey("jobId"))
            {
                string jobId = Convert.ToString(this.NavigationContext.QueryString["jobId"]);
                string ranking = "X";

                if (!String.IsNullOrWhiteSpace(jobId))
                {
                    ThreadPool.QueueUserWorkItem(new WaitCallback(App.JobVM.GetSavedJobs));
                    if (this.NavigationContext.QueryString.ContainsKey("ranking"))
                    {
                        ranking = Convert.ToString(this.NavigationContext.QueryString["ranking"]);
                    }
                    App.JobVM.GetJobDetails(jobId, ranking);
                }
            }
        }

        private void OnLoadJobComplete(bool payload)
        {
            if (payload)
            {
                BuildAppBar();
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
                    VerticalAlignment = VerticalAlignment.Center
                };
                pin1.Content = pinContent;
                mapJobLocation.Children.Add(pin1);

                // Set the map style to Aerial
                mapJobLocation.Mode = new Microsoft.Phone.Controls.Maps.RoadMode();

                // Set the view and put the map on the page
                mapJobLocation.SetView(mapCenter, zoom);

                //Show or hide application app bar btn
                //appBarApply.IsEnabled = !String.IsNullOrWhiteSpace(App.JobVM.JobInfo.ApplyURL);

            }
        }

        private void OnGetSavedJobsComplete(bool payload)
        {
            if (payload)
            {
                BuildAppBar();
            }
        }

        private void OnSaveDeleteJobComplete(bool payload)
        {
            if (payload)
            {
                BuildAppBar();
            }
        }

        private void btnIndustryStats_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/JobStatsPage", UriKind.Relative));
        }

        private void btnViewLargeMap_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/JobMapPage", UriKind.Relative));
        }

        private void btnNeighborhood_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/NeighborhoodPage", UriKind.Relative));
        }

        private void btnMoreCompanyJobs_Click(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(App.JobVM.JobInfo.Company))
            {
                NavigationService.Navigate(new Uri(String.Format("/CompanySearchResultsPage/{0}", App.JobVM.JobInfo.Company), UriKind.Relative));
            }
        }

        private void BuildAppBar()
        {
            //Set the page's ApplicationBar to a new instance of ApplicationBar
            ApplicationBar = new ApplicationBar();

            ApplicationBarIconButton appBarApply = new ApplicationBarIconButton(new Uri("/Resources/Images/AppBarDocument.png", UriKind.Relative));
            appBarApply.Text = "apply";
            appBarApply.Click += new EventHandler(appBarApply_Click);
            ApplicationBar.Buttons.Add(appBarApply);

            ApplicationBarIconButton appBarShare = new ApplicationBarIconButton(new Uri("/Resources/Images/AppBarShareThis.png", UriKind.Relative));
            appBarShare.Text = "share";
            appBarShare.Click += new EventHandler(appBarShare_Click);
            ApplicationBar.Buttons.Add(appBarShare);

            if (App.JobVM.IsJobInSavedJobs(App.JobVM.JobInfo))
            {
                ApplicationBarIconButton appBarUnFavorite = new ApplicationBarIconButton(new Uri("/Resources/Images/AppBarFavoriteRemove.png", UriKind.Relative));
                appBarUnFavorite.Text = "remove";
                appBarUnFavorite.Click += new EventHandler(appBarUnFavorite_Click);
                ApplicationBar.Buttons.Add(appBarUnFavorite);
            }
            else
            {
                ApplicationBarIconButton appBarFavorite = new ApplicationBarIconButton(new Uri("/Resources/Images/AppBarFavoriteAdd.png", UriKind.Relative));
                appBarFavorite.Text = "add";
                appBarFavorite.Click += new EventHandler(appBarFavorite_Click);
                ApplicationBar.Buttons.Add(appBarFavorite);
            }

            ApplicationBarIconButton appBarPin = new ApplicationBarIconButton(new Uri("/Resources/Images/AppBarPushPin.png", UriKind.Relative));
            appBarPin.Text = "pin to start";
            appBarPin.Click += new EventHandler(appBarPin_Click);
            ApplicationBar.Buttons.Add(appBarPin);

            ApplicationBarMenuItem appMenuEmail = new ApplicationBarMenuItem("email it");
            appMenuEmail.Click += new EventHandler(appMenuEmail_Click);
            ApplicationBar.MenuItems.Add(appMenuEmail);
        }

        void appMenuEmail_Click(object sender, EventArgs e)
        {
            Messenger.Default.Send<bool>(true, "EmailJobRequest");
        }

        private void appBarShare_Click(object sender, EventArgs e)
        {
            ShareLinkTask shareLink = new ShareLinkTask
            {
                Title = App.JobVM.JobInfo.Company,
                Message = "",
                LinkUri = new Uri(App.JobVM.JobInfo.ApplyURL)
            };

            shareLink.Show();
        }

        private void appBarApply_Click(object sender, EventArgs e)
        {
            WebBrowserTask webBrowseTask = new WebBrowserTask
            {
                Uri = new Uri(App.JobVM.JobInfo.ApplyURL, UriKind.Absolute)
            };

            webBrowseTask.Show();
        }

        private void appBarFavorite_Click(object sender, EventArgs e)
        {
            Messenger.Default.Send<bool>(true, "SaveJobRequest");
        }

        private void appBarUnFavorite_Click(object sender, EventArgs e)
        {
            Messenger.Default.Send<bool>(true, "DeleteJobRequest");
        }

        private void appBarPin_Click(object sender, EventArgs e)
        {
            var navUri = String.Format("/Views/JobDetailsView.xaml?jobId={0}&ranking={1}", App.JobVM.JobInfo.DID, App.JobVM.JobInfo.Ranking);
            var foundUri = new Uri(navUri, UriKind.Relative);

            var foundtile = ShellTile.ActiveTiles.FirstOrDefault(x => x.NavigationUri == foundUri);

            if (foundtile == null)
            {
                var jobTile = new StandardTileData()
                {
                    Title = (String.IsNullOrWhiteSpace(App.JobVM.JobInfo.Company) ? "Job Compass" : App.JobVM.JobInfo.Company),
                    BackgroundImage = new Uri("/Resources/Images/Tiles/ShellTileBkrnd.png", UriKind.Relative),
                    BackContent = App.JobVM.JobInfo.JobTitle,
                    BackBackgroundImage = new Uri("/Resources/Images/Tiles/ShellTileBackBkrnd.png", UriKind.Relative)
                };

                ShellTile.Create(new Uri(navUri, UriKind.Relative), jobTile);
            }
        }
    }
}