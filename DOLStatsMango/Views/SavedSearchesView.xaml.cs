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
using System.Threading;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.Phone.Shell;
using Telerik.Windows.Controls;

namespace DOLStatsMango.Views
{
    public partial class SavedSearchesView : PhoneApplicationPage
    {
        public SavedSearchesView()
        {
            Messenger.Default.Register<Uri>(this, "PerformSavedSearchRequest", (uri) => NavigationService.Navigate(uri));
            Messenger.Default.Register<bool>(this, "DeleteSavedSearchesCompleteRequest", OnDeleteSavedSearchesComplete);
            Messenger.Default.Register<bool>(this, "GetSavedSearchesComplete", OnGetSavedSearchesComplete);
            InitializeComponent();
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            InteractionEffectManager.AllowedTypes.Add(typeof(RadDataBoundListBoxItem));

            // Set the data context
            if (DataContext == null)
                DataContext = App.SearchVM;

            ThreadPool.QueueUserWorkItem(new WaitCallback(App.SearchVM.GetSavedSearches));
        }

        private void lbSavedSearches_IsCheckModeActiveChanged(object sender, Telerik.Windows.Controls.IsCheckModeActiveChangedEventArgs e)
        {

        }

        private void appBarSelectAll_Click(object sender, EventArgs e)
        {
            lbSavedSearches.CheckedItems.CheckAll();
            lbSavedSearches.IsCheckModeActive = true;
            ApplicationBarIconButton btnUnSelectAll = (ApplicationBarIconButton)ApplicationBar.Buttons[1];
            btnUnSelectAll.IsEnabled = true;
        }

        private void appBarUnSelectAll_Click(object sender, EventArgs e)
        {
            lbSavedSearches.CheckedItems.Clear();
            lbSavedSearches.IsCheckModeActive = false;
            ApplicationBarIconButton btnUnSelectAll = (ApplicationBarIconButton)ApplicationBar.Buttons[1];
            btnUnSelectAll.IsEnabled = false;
        }

        private void appBarDelete_Click(object sender, EventArgs e)
        {
            Messenger.Default.Send<bool>(true, "OnDeleteSavedSearchesRequest");
        }

        private void OnDeleteSavedSearchesComplete(bool payload)
        {
            if (payload)
            {
                lbSavedSearches.IsCheckModeActive = false;
                EnableDisableAppBarBtns();
            }
        }

        private void OnGetSavedSearchesComplete(bool payload)
        {
            if (payload)
            {
                EnableDisableAppBarBtns();
            }
        }

        private void EnableDisableAppBarBtns()
        {
            ApplicationBarIconButton btnSelectAll = (ApplicationBarIconButton)ApplicationBar.Buttons[0];
            ApplicationBarIconButton btnUnSelectAll = (ApplicationBarIconButton)ApplicationBar.Buttons[1];
            ApplicationBarIconButton btnDelete = (ApplicationBarIconButton)ApplicationBar.Buttons[2];

            if (App.SearchVM.SavedSearches.Count > 0)
            {
                btnSelectAll.IsEnabled = true;
                btnUnSelectAll.IsEnabled = true;
                btnDelete.IsEnabled = true;
            }
            else
            {
                btnSelectAll.IsEnabled = false;
                btnUnSelectAll.IsEnabled = false;
                btnDelete.IsEnabled = false;
            }
        }
    }
}