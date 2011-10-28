using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using DOLStatsMango.Framework;
using DOLStatsMango.Resources.en;
using Microsoft.Phone.Controls;
using DOLStatsMango.Framework.Services.CareerBuilder;
using GalaSoft.MvvmLight.Threading;
using GalaSoft.MvvmLight.Messaging;
using Telerik.Windows.Controls;
using DOLStatsMango.Framework.Helpers;
using System.IO.IsolatedStorage;

namespace DOLStatsMango
{
    public partial class MainPage : PhoneApplicationPage
    {
        IsolatedStorageSettings store = IsolatedStorageSettings.ApplicationSettings;
        private PersistentDataStorage persistentStore = new PersistentDataStorage();
        // Constructor
        public MainPage()
        {
            Messenger.Default.Register<Uri>(this, "QuickSearchComplete", (uri) => NavigationService.Navigate(uri));
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(MainPage_Loaded);
        }

        // Load data for the ViewModel Items
        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            //DOL Disclaimer
            if (!persistentStore.DoesExist(IsolatedStorageKeys.SHOW_DOL_DISCLAIMER))
            {
                var result = MessageBox.Show(AppResources.DOLDisclaimer_Text, AppResources.DOLDisclaimer_Heading, MessageBoxButton.OK);
                if (result == MessageBoxResult.OK)
                {
                    persistentStore.Backup(IsolatedStorageKeys.SHOW_DOL_DISCLAIMER, true);
                }
            }

            //Location Disclaimer
            if (!store.Contains(IsolatedStorageKeys.SHOW_LOCATION_DISCLAIMER))
            {
                var result = MessageBox.Show(AppResources.LocationDisclaimer_Text, AppResources.LocationDisclaimer_Heading, MessageBoxButton.OKCancel);
                if (result == MessageBoxResult.OK)
                {
                    //Allow access to location
                    App.MainVM.SaveLocationSettingsFromDisclaimer(true);
                    persistentStore.Backup(IsolatedStorageKeys.SHOW_LOCATION_DISCLAIMER, true);
                }
                else
                {
                    //don't allow access to location
                    App.MainVM.SaveLocationSettingsFromDisclaimer(false);
                }
            }
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            InteractionEffectManager.AllowedTypes.Add(typeof(RadDataBoundListBoxItem));

            // Set the data context
            if (DataContext == null)
                DataContext = App.MainVM;

            if (!App.MainVM.IsLoaded)
            {
                ThreadPool.QueueUserWorkItem(new WaitCallback(App.MainVM.LoadJobTrendsandSettings));
            }
        }

        private void btnAdvancedSearch_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri(String.Format("/SearchAdvancedPage/{0}", "search"), UriKind.Relative));
        }

        private void txtSearchKeyword_ActionIconTapped(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(txtSearchKeyword.Text))
            {
                MessageBox.Show("A search keyword is required to perform job searches.  Please specify at least one keyword and try again.");
                return;
            }
            NavigationService.Navigate(new Uri(String.Format("/SearchResultsPage/{0}/{1}", txtSearchKeyword.Text, false.ToString()), UriKind.Relative));
        }

        private void txtSearchKeyword_KeyDown(object sender, KeyEventArgs e)
        {
            if ((Key.Enter == e.Key) || (e.PlatformKeyCode == 0x0A))
            {
                this.Focus();
                if (String.IsNullOrEmpty(txtSearchKeyword.Text))
                {
                    MessageBox.Show("A search keyword is required to perform job searches.  Please specify at least one keyword and try again.");
                    return;
                }
                NavigationService.Navigate(new Uri(String.Format("/SearchResultsPage/{0}/{1}", txtSearchKeyword.Text, false.ToString()), UriKind.Relative));
            }
        }

        private void btnEmployment_Click(object sender, RoutedEventArgs e)
        {
            btnEmployment.IsEnabled = false;
            btnYearly.IsEnabled = true;
            btnHourly.IsEnabled = true;

            radChartEmployment.Visibility = Visibility.Visible;
            radChartYearly.Visibility = Visibility.Collapsed;
            radChartHourly.Visibility = Visibility.Collapsed;

            txtChartHeading.Text = "TOP EMPLOYED OCCUPATIONS IN 2010";
        }

        private void btnYearly_Click(object sender, RoutedEventArgs e)
        {
            btnEmployment.IsEnabled = true;
            btnYearly.IsEnabled = false;
            btnHourly.IsEnabled = true;

            radChartEmployment.Visibility = Visibility.Collapsed;
            radChartYearly.Visibility = Visibility.Visible;
            radChartHourly.Visibility = Visibility.Collapsed;

            txtChartHeading.Text = "TOP PAID OCCUPATIONS IN 2010 (YEARLY AVG.)";
        }

        private void btnHourly_Click(object sender, RoutedEventArgs e)
        {
            btnEmployment.IsEnabled = true;
            btnYearly.IsEnabled = true;
            btnHourly.IsEnabled = false;

            radChartEmployment.Visibility = Visibility.Collapsed;
            radChartYearly.Visibility = Visibility.Collapsed;
            radChartHourly.Visibility = Visibility.Visible;

            txtChartHeading.Text = "TOP PAID OCCUPATIONS IN 2010 (HOURLY AVG.)";
        }

        private void btnJobStats_Click(object sender, RoutedEventArgs e)
        {
            //JobDataPage
            NavigationService.Navigate(new Uri("/JobDataPage", UriKind.Relative));
        }

        private void btnSettings_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/SettingsPage", UriKind.Relative));
        }

        private void btnAbout_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/AboutPage", UriKind.Relative));
        }

        private void lbSavedSearches_IsCheckModeActiveChanged(object sender, Telerik.Windows.Controls.IsCheckModeActiveChangedEventArgs e)
        {

        }

        private void btnSavedJobs_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/SavedJobsPage", UriKind.Relative));
        }

        private void btnSavedSearches_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/SavedSearchesPage", UriKind.Relative));
        }
    }
}
