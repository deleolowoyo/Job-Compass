﻿using System;
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
using Microsoft.Phone.Controls;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.Phone.Shell;
using System.Device.Location;
using Microsoft.Phone.Controls.Maps;
using DOLStatsMango.Resources.en;
using DOLStatsMango.Models;
using Telerik.Windows.Controls;

namespace DOLStatsMango.Views
{
    public partial class SearchResultsView : PhoneApplicationPage
    {
        private string mapJobDID;
        private string mapJobRank;
        private bool isRoadMode;
        private Button getMoreButton;
        private bool isListView = true;
        private JobSearchEnum.SortingCriteria sortCriteria;
        private string orderDirection = string.Empty;

        public SearchResultsView()
        {
            Messenger.Default.Register<Uri>(this, "GoToJobDetailsRequest", (uri) => NavigationService.Navigate(uri));
            Messenger.Default.Register<bool>(this, "LoadMoreCompleteRequest", OnLoadMoreComplete);
            Messenger.Default.Register<bool>(this, "LoadMapPinsCompleteRequest", OnLoadMapPinsComplete);
            BuildAppBar();
            InitializeComponent();
            Loaded += new RoutedEventHandler(SearchResultsView_Loaded);
        }

        void SearchResultsView_Loaded(object sender, RoutedEventArgs e)
        {
            mapJobs.CredentialsProvider = new ApplicationIdCredentialsProvider(AppResources.BingMapsApiKey);
            mapJobs.Mode = new RoadMode();
            isRoadMode = true;
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            InteractionEffectManager.AllowedTypes.Add(typeof(RadDataBoundListBoxItem));

            // Set the data context
            if (DataContext == null)
                DataContext = App.SearchVM;

            if (this.NavigationContext.QueryString.ContainsKey("doSavedSearch"))
            {
                bool doSavedSearch = Convert.ToBoolean(this.NavigationContext.QueryString["doSavedSearch"]);
                App.SearchVM.IsFromSavedSearch = true;

                if (doSavedSearch)
                {
                    if (App.SearchVM.AdvancedSearchCommand.CanExecute(null))
                    {
                        App.SearchVM.AdvancedSearchCommand.Execute(null);
                    }
                }
            }

            if (this.NavigationContext.QueryString.ContainsKey("keyword"))
            {
                string keyword = Convert.ToString(this.NavigationContext.QueryString["keyword"]);
                App.SearchVM.IsFullSearch = true;
                App.SearchVM.IsFromSavedSearch = false;

                if (!String.IsNullOrWhiteSpace(keyword))
                {
                    if (App.SearchVM.QuickSearchCommand.CanExecute(null))
                    {
                        App.SearchVM.QuickSearchCommand.Execute(keyword);
                    }
                }
            }
        }

        private void btnLoadMore_Click(object sender, RoutedEventArgs e)
        {
            if (App.SearchVM.SearchMainInfo.TotalCount > 25)
            {
                this.getMoreButton = sender as Button;
                this.getMoreButton.IsEnabled = false;
                this.getMoreButton.Content = "loading...";
                if (App.SearchVM.LoadMoreJobsCommand.CanExecute(null))
                {
                    App.SearchVM.LoadMoreJobsCommand.Execute(null);
                }
            }
            else
            {
                MessageBox.Show("No more records available to be loaded");
            }
        }

        private void OnLoadMoreComplete(bool payload)
        {
            if (payload)
            {
                this.getMoreButton.IsEnabled = true;
                this.getMoreButton.Content = "load more";
            }
        }

        private void OnLoadMapPinsComplete(bool payload)
        {
            if (payload)
            {
                // Set the center coordinate and zoom level
                GeoCoordinate mapCenter = new GeoCoordinate(App.SearchVM.JobSearchResults[0].LocationLatitude, App.SearchVM.JobSearchResults[0].LocationLongitude);
                int zoom = 5;

                // Create a pushpin to put at the center of the view
                //Pushpin pin1 = new Pushpin();
                //pin1.Location = mapCenter;
                //pin1.Content = App.SearchVM.JobSearchResults[0].Ranking;
                //mapJobs.Children.Add(pin1);

                // Set the map style to Aerial
                mapJobs.Mode = new Microsoft.Phone.Controls.Maps.RoadMode();

                // Set the view and put the map on the page
                mapJobs.SetView(mapCenter, zoom);
            }
        }

        private void BuildAppBar()
        {
            //Set the page's ApplicationBar to a new instance of ApplicationBar
            ApplicationBar = new ApplicationBar();

            if (isListView)
            {
                ApplicationBarIconButton appBarBtn_Sort = new ApplicationBarIconButton(new Uri("/Resources/Images/AppBarSort.png", UriKind.Relative));
                appBarBtn_Sort.Text = "sort";
                appBarBtn_Sort.Click += new EventHandler(appBarBtn_Sort_Click);
                ApplicationBar.Buttons.Add(appBarBtn_Sort);

                ApplicationBarIconButton appBarBtn_Refine = new ApplicationBarIconButton(new Uri("/Resources/Images/AppBarFunnel.png", UriKind.Relative));
                appBarBtn_Refine.Text = "refine";
                appBarBtn_Refine.Click += new EventHandler(appBarBtn_Refine_Click);
                ApplicationBar.Buttons.Add(appBarBtn_Refine);

                ApplicationBarIconButton appBarBtn_MapView = new ApplicationBarIconButton(new Uri("/Resources/Images/AppBarLocation.png", UriKind.Relative));
                appBarBtn_MapView.Text = "map view";
                appBarBtn_MapView.Click += new EventHandler(appBarBtn_MapView_Click);
                ApplicationBar.Buttons.Add(appBarBtn_MapView);

                ApplicationBarIconButton appBarBtn_SaveSearch = new ApplicationBarIconButton(new Uri("/icons/appbar.save.rest.png", UriKind.Relative));
                appBarBtn_SaveSearch.Text = "save search";
                appBarBtn_SaveSearch.Click += new EventHandler(appBarBtn_SaveSearch_Click);
                ApplicationBar.Buttons.Add(appBarBtn_SaveSearch);
            }
            else
            {
                ApplicationBarIconButton appBarBtn_MapTypeView = new ApplicationBarIconButton(new Uri("/Resources/Images/Eye.png", UriKind.Relative));
                appBarBtn_MapTypeView.Text = "map type";
                appBarBtn_MapTypeView.Click += new EventHandler(appBarBtn_MapTypeView_Click);
                ApplicationBar.Buttons.Add(appBarBtn_MapTypeView);

                ApplicationBarIconButton appBarBtn_ListView = new ApplicationBarIconButton(new Uri("/Resources/Images/AppBarBulletList.png", UriKind.Relative));
                appBarBtn_ListView.Text = "list view";
                appBarBtn_ListView.Click += new EventHandler(appBarBtn_ListView_Click);
                ApplicationBar.Buttons.Add(appBarBtn_ListView);
            }
        }

        private void appBarBtn_Sort_Click(object sender, EventArgs e)
        {
            radWindowSortBy.IsOpen = true;
        }

        private void appBarBtn_Refine_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri(String.Format("/SearchAdvancedPage/{0}", "refine"), UriKind.Relative));
        }

        private void appBarBtn_MapView_Click(object sender, EventArgs e)
        {
            isListView = false;
            BuildAppBar();
            mapJobs.Visibility = Visibility.Visible;
            lbJobSearchResults.Visibility = Visibility.Collapsed;
        }

        private void appBarBtn_ListView_Click(object sender, EventArgs e)
        {
            isListView = true;
            BuildAppBar();
            mapJobs.Visibility = Visibility.Collapsed;
            lbJobSearchResults.Visibility = Visibility.Visible;
        }

        private void appBarBtn_SaveSearch_Click(object sender, EventArgs e)
        {
            if (App.SearchVM.SaveSearchCommand.CanExecute(null))
            {
                App.SearchVM.SaveSearchCommand.Execute(null);
            }
        }

        private void appBarBtn_MapTypeView_Click(object sender, EventArgs e)
        {
            //Switch between map views
            if (isRoadMode)
            {
                mapJobs.Mode = new AerialMode();
                isRoadMode = false;
            }
            else
            {
                mapJobs.Mode = new RoadMode();
                isRoadMode = true;
            }
        }

        private void lpSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            radWindowSortBy.IsOpen = false;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            radWindowSortBy.IsOpen = false;
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            radWindowSortBy.IsOpen = false;
            Messenger.Default.Send<JobSearchEnum.SortingCriteria>(sortCriteria, "SortSearchResultsRequest");
        }

        private void rbPayHigh_Click(object sender, RoutedEventArgs e)
        {
            if (rbPayHigh.IsChecked.HasValue && rbPayHigh.IsChecked.Value)
            {
                sortCriteria = JobSearchEnum.SortingCriteria.PayHigh;
            }
        }

        private void rbPayLow_Click(object sender, RoutedEventArgs e)
        {
            if (rbPayLow.IsChecked.HasValue && rbPayLow.IsChecked.Value)
            {
                sortCriteria = JobSearchEnum.SortingCriteria.PayLow;
            }
        }

        private void rbJobTitle_Click(object sender, RoutedEventArgs e)
        {
            if (rbJobTitle.IsChecked.HasValue && rbJobTitle.IsChecked.Value)
            {
                sortCriteria = JobSearchEnum.SortingCriteria.Title;
            }
        }

        private void rbCompanyName_Click(object sender, RoutedEventArgs e)
        {
            if (rbCompanyName.IsChecked.HasValue && rbCompanyName.IsChecked.Value)
            {
                sortCriteria = JobSearchEnum.SortingCriteria.Company;
            }
        }

        private void rbDistance_Click(object sender, RoutedEventArgs e)
        {
            if (rbDistance.IsChecked.HasValue && rbDistance.IsChecked.Value)
            {
                sortCriteria = JobSearchEnum.SortingCriteria.Distance;
            }
        }

        private void rbRelevance_Click(object sender, RoutedEventArgs e)
        {
            if (rbRelevance.IsChecked.HasValue && rbRelevance.IsChecked.Value)
            {
                sortCriteria = JobSearchEnum.SortingCriteria.Relevance;
            }
        }

        private void rbPostedDate_Click(object sender, RoutedEventArgs e)
        {
            if (rbPostedDate.IsChecked.HasValue && rbPostedDate.IsChecked.Value)
            {
                sortCriteria = JobSearchEnum.SortingCriteria.Date;
            }
        }

        private void Pushpin_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            var pushPin = sender as Pushpin;

            string tag = (string)pushPin.Tag;

            string[] values = tag.Split(';');

            txtJobTitle.Text = values[0];
            mapJobDID = values[1];
            txtLocation.Text = values[2];
            txtJobRank.Text = values[3];
            mapJobRank = values[3];

            radMapPinInfo.IsOpen = true;
        }

        private void btnMapJobDetails_Click(object sender, RoutedEventArgs e)
        {
            radMapPinInfo.IsOpen = false;
            NavigationService.Navigate(new Uri(String.Format("/JobDetailsPage/{0}/{1}", mapJobDID, mapJobRank), UriKind.Relative));
        }

        private void txtJobTitle_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            radMapPinInfo.IsOpen = false;
        }

        private void txtLocation_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            radMapPinInfo.IsOpen = false;
        }
    }
}
