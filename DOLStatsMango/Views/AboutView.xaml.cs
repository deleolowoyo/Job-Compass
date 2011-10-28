using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Tasks;
using DOLStatsMango.Resources.en;

namespace DOLStatsMango.Views
{
    public partial class AboutView : PhoneApplicationPage
    {
        public AboutView()
        {
            InitializeComponent();
            Loaded += new RoutedEventHandler(AboutView_Loaded);
        }

        void AboutView_Loaded(object sender, RoutedEventArgs e)
        {
            GetVersionNumber();
            txtDOLInfo.Text = AppResources.About_DOLInfo;
            txtZillow.Text = AppResources.About_Zillow;
            txtRestSharp.Text = AppResources.About_RestSharpInfo;
            txtToolkit.Text = AppResources.About_SilverlightToolkit;
            txtYahoo.Text = AppResources.About_YahooSearchInfo;
        }

        private void GetVersionNumber()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            var name = new AssemblyName(assembly.FullName);
            txtVersion.Text = String.Format("Version: {0}", name.Version.ToString(3));
        }

        private void btnSendEmail_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder sbEmail = new StringBuilder();
            sbEmail.AppendLine("Hi there,");
            sbEmail.AppendLine();
            sbEmail.AppendLine("I'm using your Job Compass app for #wp7 and I'd just like to say that...");

            EmailComposeTask emailTask = new EmailComposeTask
            {
                To = "support@dcubeapps.com",
                Subject = "Job Compass app for #WP7 feedback",
                Body = sbEmail.ToString()
            };

            emailTask.Show();
        }

        private void btnRate_Click(object sender, RoutedEventArgs e)
        {
            MarketplaceReviewTask reviewTask = new MarketplaceReviewTask();
            reviewTask.Show();
        }

        private void btnTwitter_Click(object sender, RoutedEventArgs e)
        {
            WebBrowserTask webBrowseTask = new WebBrowserTask
            {
                Uri = new Uri("http://mobile.twitter.com/dcubeapps", UriKind.Absolute)
            };

            webBrowseTask.Show();
        }

        private void btnWebsite_Click(object sender, RoutedEventArgs e)
        {
            WebBrowserTask webBrowseTask = new WebBrowserTask
            {
                Uri = new Uri("http://www.dcubeapps.com", UriKind.Absolute)
            };

            webBrowseTask.Show();
        }
    }
}