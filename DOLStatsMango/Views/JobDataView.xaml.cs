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
using Microsoft.Phone.Controls;
using GalaSoft.MvvmLight.Messaging;

namespace DOLStatsMango.Views
{
    public partial class JobDataView : PhoneApplicationPage
    {
        public JobDataView()
        {
            Messenger.Default.Register<bool>(this, "SelectedJobComplete", OnSelectedJob);
            InitializeComponent();
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            // Set the data context
            if (DataContext == null)
                DataContext = App.MainVM;

            ThreadPool.QueueUserWorkItem(new WaitCallback(App.MainVM.GetAllJobs));
        }

        private void OnSelectedJob(bool payload)
        {
            this.Focus();
            if (payload)
            {
                if (App.MainVM.SelectedJob != null)
                {
                    txtJobTitle.Text = App.MainVM.SelectedJob.Title;
                    //Hourly
                    hourly10.Value = App.MainVM.SelectedJob.HourlyPercentile10;
                    hourly25.Value = App.MainVM.SelectedJob.HourlyPercentile25;
                    hourly75.Value = App.MainVM.SelectedJob.HourlyPercentile75;
                    hourly90.Value = App.MainVM.SelectedJob.HourlyPercentile90;

                    //Yearly
                    yearly10.Value = App.MainVM.SelectedJob.YearlyPercentile10;
                    yearly25.Value = App.MainVM.SelectedJob.YearlyPercentile25;
                    yearly75.Value = App.MainVM.SelectedJob.YearlyPercentile75;
                    yearly90.Value = App.MainVM.SelectedJob.YearlyPercentile90;
                }
            }
       }

        private void autoCompleteJobs_GotFocus(object sender, RoutedEventArgs e)
        {
            autoCompleteJobs.Text = string.Empty;
        }
    }
}