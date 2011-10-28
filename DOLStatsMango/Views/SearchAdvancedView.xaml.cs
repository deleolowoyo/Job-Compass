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
using Microsoft.Phone.Controls;
using GalaSoft.MvvmLight.Messaging;
using System.Threading;

namespace DOLStatsMango.Views
{
    public partial class SearchAdvancedView : PhoneApplicationPage
    {
        public SearchAdvancedView()
        {
            Messenger.Default.Register<Uri>(this, "AdvancedSearchComplete", (uri) => NavigationService.Navigate(uri));
            Messenger.Default.Register<int>(this, "PreviewSearchCompleteRequest", OnPreviewSearchComplete);
            Messenger.Default.Register<bool>(this, "ResetSearchCompleteRequest", OnResetSearchComplete);
            InitializeComponent();
            Loaded += new RoutedEventHandler(SearchAdvancedView_Loaded);
        }

        void SearchAdvancedView_Loaded(object sender, RoutedEventArgs e)
        {
            ThreadPool.QueueUserWorkItem(new WaitCallback(App.SearchVM.GetSavedSearches));
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            // Set the data context
            if (DataContext == null)
                DataContext = App.SearchVM;

            ThreadPool.QueueUserWorkItem(new WaitCallback(App.SearchVM.LoadDropDowns));
            ThreadPool.QueueUserWorkItem(new WaitCallback(App.SearchVM.LoadSettings));
            
            if (this.NavigationContext.QueryString.ContainsKey("pageHeading"))
            {
                string pageHeader = Convert.ToString(this.NavigationContext.QueryString["pageHeading"]);

                if (String.IsNullOrWhiteSpace(pageHeader))
                {
                    PageTitle.Text = "search";
                    App.SearchVM.IsFromSavedSearch = false;
                }
                else
                {
                    PageTitle.Text = pageHeader;
                }
            }
        }

        private void btnFindLocation_Click(object sender, RoutedEventArgs e)
        {
            Messenger.Default.Send<bool>(true, "FindMyLocationRequest");
        }

        private void btnAdvancedSearch_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri(String.Format("/SearchResultsPage/{0}/{1}", string.Empty, false.ToString()), UriKind.Relative));
        }

        private void OnPreviewSearchComplete(int payload)
        {
            txtPreviewSearch.Text = String.Format("{0:0,0} result(s) match your search criteria", payload);
            txtPreviewSearch.Visibility = Visibility.Visible;
            btnPreviewSearch.Visibility = Visibility.Collapsed;
        }

        private void OnResetSearchComplete(bool payload)
        {
            if (payload)
            {
                txtPreviewSearch.Text = string.Empty;
                txtPreviewSearch.Visibility = Visibility.Collapsed;
                btnPreviewSearch.Visibility = Visibility.Visible;
            }
        }
    }
}
