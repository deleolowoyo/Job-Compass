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
using Microsoft.Phone.Info;
using Microsoft.Phone.Tasks;
using System.Text;

namespace DOLStatsMango.Views
{
    public partial class ErrorView : PhoneApplicationPage
    {
        public ErrorView()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            // Set the data context
            if (DataContext == null)
            {
                DataContext = App.MainVM;
            }
        }

        private void btnEmailError_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("[TYPE MESSAGE HERE]");
            sb.AppendLine();
            sb.AppendLine();
            sb.AppendLine("||ERROR DETAILS||");
            sb.AppendLine(App.MainVM.ErrorString);
            sb.AppendLine();
            sb.AppendLine("||DEVICE INFORMATION||");
            sb.AppendLine("Manufacturer: " + DeviceStatus.DeviceManufacturer);
            sb.AppendLine("Name: " + DeviceStatus.DeviceName);
            sb.AppendLine("Firmware: " + DeviceStatus.DeviceFirmwareVersion);
            sb.AppendLine("Hardware: " + DeviceStatus.DeviceHardwareVersion);
            sb.AppendLine("Total Memory: " + DeviceStatus.DeviceTotalMemory.ToString());
            sb.AppendLine("Current Memory: " + DeviceStatus.ApplicationCurrentMemoryUsage.ToString());
            sb.AppendLine("Memory Usage Limit: " + DeviceStatus.ApplicationMemoryUsageLimit.ToString());
            sb.AppendLine("Peak Memory Usage: " + DeviceStatus.ApplicationPeakMemoryUsage.ToString());
            sb.AppendLine("Is Keyboard Present: " + DeviceStatus.IsKeyboardPresent.ToString());
            sb.AppendLine("Is Keyboard Deployed: " + DeviceStatus.IsKeyboardDeployed.ToString());
            sb.AppendLine("Power source: " + DeviceStatus.PowerSource.ToString());

            EmailComposeTask emailTask = new EmailComposeTask
            {
                To = "support@dcubeapps.com",
                Subject = "Job Companion - Error Feedback",
                Body = sb.ToString()
            };

            emailTask.Show();
        }
    }
}