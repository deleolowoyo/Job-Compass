using System.ComponentModel;
using GalaSoft.MvvmLight.Messaging;
using System;
using Microsoft.Phone.Net.NetworkInformation;
using System.Windows;

namespace DOLStatsMango.ViewModels
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        public ViewModelBase()
        {

        }
        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void NotifyPropertyChanged(string propName)
        {
            PropertyChangedEventHandler eh = this.PropertyChanged;
            if (eh != null)
            {
                eh(this, new PropertyChangedEventArgs(propName));
            }
        }

        protected virtual void SendNavigationRequestMessage(Uri uri, string token)
        {
            Messenger.Default.Send<Uri>(uri, token);
        }

        protected virtual bool IsNetworkAvailable()
        {
            //NetworkInterface.GetIsNetworkAvailable()
            if (DeviceNetworkInformation.IsNetworkAvailable)
            {
                return true;
            }
            MessageBox.Show("Network connection failed.  This app requires a connection to the internet.  Please check your device settings and ensure that you have internet access.");
            return false;
        }

        protected virtual void ShowErrorMessage()
        {
            MessageBox.Show("We're sorry, but it looks like we are having problems performing some current tasks.  Please contact us if this keeps happening via our settings page.");
        }
    }
}
