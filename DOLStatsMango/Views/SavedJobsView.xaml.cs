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
using GalaSoft.MvvmLight.Messaging;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Telerik.Windows.Controls;

namespace DOLStatsMango.Views
{
    public partial class SavedJobsView : PhoneApplicationPage
    {
        public SavedJobsView()
        {
            Messenger.Default.Register<bool>(this, "DeleteSavedJobsCompleteRequest", OnDeleteSavedJobsComplete);
            Messenger.Default.Register<bool>(this, "GetSavedJobsComplete", OnGetSavedJobsComplete);
            Messenger.Default.Register<Uri>(this, "GoToJobDetailsRequest", (uri) => NavigationService.Navigate(uri));
            InitializeComponent();
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            InteractionEffectManager.AllowedTypes.Add(typeof(RadDataBoundListBoxItem));

            // Set the data context
            if (DataContext == null)
                DataContext = App.JobVM;

            ThreadPool.QueueUserWorkItem(new WaitCallback(App.JobVM.GetSavedJobs));
        }

        private void lbSavedJobs_IsCheckModeActiveChanged(object sender, Telerik.Windows.Controls.IsCheckModeActiveChangedEventArgs e)
        {

        }

        private void appBarSelectAll_Click(object sender, EventArgs e)
        {
            lbSavedJobs.CheckedItems.CheckAll();
            lbSavedJobs.IsCheckModeActive = true;
            ApplicationBarIconButton btnUnSelectAll = (ApplicationBarIconButton)ApplicationBar.Buttons[1];
            btnUnSelectAll.IsEnabled = true;
        }

        private void appBarUnSelectAll_Click(object sender, EventArgs e)
        {
            lbSavedJobs.CheckedItems.Clear();
            lbSavedJobs.IsCheckModeActive = false;
            ApplicationBarIconButton btnUnSelectAll = (ApplicationBarIconButton)ApplicationBar.Buttons[1];
            btnUnSelectAll.IsEnabled = false;
        }

        private void appBarDelete_Click(object sender, EventArgs e)
        {
            Messenger.Default.Send<bool>(true, "OnDeleteSavedJobsRequest");
        }

        private void OnDeleteSavedJobsComplete(bool payload)
        {
            if (payload)
            {
                lbSavedJobs.IsCheckModeActive = false;
                EnableDisableAppBarBtns();
            }
        }

        private void OnGetSavedJobsComplete(bool payload)
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

            if (App.JobVM.SavedJobs.Count > 0)
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