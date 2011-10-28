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
using Microsoft.Phone.Tasks;
using Telerik.Windows.Data;
using DOLStatsMango.Models.Zillow;

namespace DOLStatsMango.Views
{
    public partial class NeighborhoodView : PhoneApplicationPage
    {
        public NeighborhoodView()
        {
            Messenger.Default.Register<bool>(this, "LoadedDemographicComplete", OnLoadedDemographic);
            InitializeComponent();
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            // Set the data context
            if (DataContext == null)
                DataContext = App.JobVM;

            Messenger.Default.Send<bool>(true, "GetNeighborhoodInfoRequest");
        }

        private void OnLoadedDemographic(bool payload)
        {
            if (payload)
            {
                /*
                demographicJumpList.GroupPickerItemsSource = App.JobVM.DemographicGroupHeaders;

                GenericGroupDescriptor<string, string> groupDesc = new GenericGroupDescriptor<string, string>(demo => demo);
                groupDesc.KeySelector = (string element) => { return element; };
                this.demographicJumpList.GroupDescriptors.Add(groupDesc);

                //GenericGroupDescriptor<DemographicPageInfoData, string> groupDesc = new GenericGroupDescriptor<DemographicPageInfoData, string>();

                //groupDesc.KeySelector = (DemographicPageInfoData element) => { return element.N; };

                //demographicJumpList.GroupDescriptors.Add(groupDesc);

                List<string> groupPickerItems = new List<string>(32);
                foreach (char c in this.alphabet)
                {
                    groupPickerItems.Add(new string(c, 1));
                }
                this.demographicJumpList.GroupPickerItemsSource = groupPickerItems;

                GenericGroupDescriptor<string, string> groupByGenre = new GenericGroupDescriptor<string, string>(demo => demo);
                this.demographicJumpList.GroupDescriptors.Add(groupByGenre);

                //GenericSortDescriptor<string, string> sort = new GenericSortDescriptor<string, string>(demo => demo);
                //this.demographicJumpList.SortDescriptors.Add(sort);

                demographicJumpList.ItemsSource = App.JobVM.DemographicDetails;

                //demographicJumpList.GroupPickerItemsSource = App.JobVM.DemographicGroupHeaders;

                //GenericGroupDescriptor<string, string> groupByGenre = new GenericGroupDescriptor<string, string>(demo => demo);
                //this.demographicJumpList.GroupDescriptors.Add(groupByGenre);

                //demographicJumpList.ItemsSource = App.JobVM.DemographicDetails;
                //demographicJumpList.
                //demographicJumpList.ItemsSource = App.JobVM.DemographicDetails;
                 * */
            }
        }

        private void hypZillowTerms_Click(object sender, RoutedEventArgs e)
        {
            WebBrowserTask browser = new WebBrowserTask
            {
                Uri = new Uri("http://www.zillow.com/corp/Terms.htm", UriKind.Absolute)
            };

            browser.Show();
        }
    }
}